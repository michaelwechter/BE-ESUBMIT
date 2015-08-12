using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.IO;
using System.Xml;

namespace CertnaBase
    {
    public class Certna
        {
        public DataRow drOrder;
        public DataRow drCust;
        public DataRow drCounty;
        public DataSet dsPayload;


        private XmlDocument doc;
        private string sTifFileLocation = "Q:\\esubmitFiles\\goingToCRT\\";
        private string sXMLPackageLocation = "Q:\\esubmitFiles\\PackagesOutbound\\";
        private string sFileFormat = "yy-MMM-dd-HH-mm-ss-ffff";

        SqlLib.SQLLib Sql = new SqlLib.SQLLib();

        public enum Errors
            {
            Done,
            NoPayloadRow,
            WritePackage,
            GeneralBuildXML,
            PayloadLoop,
            Tiffload
            };

        public Errors BuildXML( )
            {
            try
                {
                doc = XmlHelper.CreateXmlDocument();

                XmlNode newNode = doc.CreateElement( "REQUEST_GROUP", "" );
                XmlNode rootNode = doc.AppendChild( newNode );

                newNode = doc.CreateElement( "REQUESTING_PARTY" );
                XmlHelper.CreateAttribute( newNode, "_Name", drCust["name"].ToString() );
                //TODO need ADDress info
                XmlHelper.CreateAttribute( newNode, "_StreetAddress", "ADDRESS" ); // drCust["Address"].ToString ());
                XmlHelper.CreateAttribute( newNode, "_City", "Austin" );
                XmlHelper.CreateAttribute( newNode, "_State", "TX" );
                XmlHelper.CreateAttribute( newNode, "_PostalCode", "12345" );
                XmlHelper.CreateAttribute( newNode, "_Identifier", drCust["ID"].ToString() );
                rootNode.AppendChild( newNode );

                newNode = doc.CreateElement( "SUBMITTING_PARTY" );
                XmlHelper.CreateAttribute( newNode, "LoginAccountIdentifer", "resherman" );
                XmlHelper.CreateAttribute( newNode, "_Name", "Document Processing Solutions,Inc" );
                rootNode.AppendChild( newNode );

                XmlNode requestNode = doc.CreateElement( "REQUEST" );
                XmlHelper.CreateAttribute( requestNode, "RequestDateTime", DateTime.Now.ToString( "s" ) );
                rootNode.AppendChild( requestNode );

                XmlNode key = doc.CreateElement( "KEY" );
                XmlHelper.CreateAttribute( key, "Comment", "" );
                XmlHelper.CreateAttribute( key, "_Value", "" );
                requestNode.AppendChild( key );

                key = doc.CreateElement( "PRIA_REQUEST" );
                XmlHelper.CreateAttribute( key, "_RelatedDocumentsIndication", "true" );
                requestNode.AppendChild( key );

                XmlNode package = doc.CreateElement( "PACKAGE" );
                XmlHelper.CreateAttribute( package, "CountyFIPSCode", "-COUNTYFIPSCODE" );
                XmlHelper.CreateAttribute( package, "StateFIPSCode", "-statefipscode" );
                XmlHelper.CreateAttribute( package, "SecurityType", "-securitytype" );
                XmlHelper.CreateAttribute( package, "Priority", "-Standard" );
                key.AppendChild( package );

                if (dsPayload.Tables[0].Rows.Count == 0)
                    {
                    System.Diagnostics.Debug.Print( "BuildXML no payload/document records" );
                    Sql.WriteEvent( "CertnaBase.BuildXML Payload has no row.", true );
                    return Errors.NoPayloadRow ;
                    }

                string curPayload = dsPayload.Tables[0].Rows[0]["payloadID1"].ToString().Trim();

                for (int rec = 0; rec < dsPayload.Tables[0].Rows.Count; rec++)
                    {
                    try
                        {
                        DataRow drp = dsPayload.Tables[0].Rows[rec];
                        string thispayloadid = drp["payloadID1"].ToString().Trim();

                        // dont do records without a tiff ???????
                        if (drp["esubmitFileName"].ToString().Trim().Length > 0 && thispayloadid.Length > 0)
                            {
                            if (thispayloadid != curPayload)
                                {
                                string sfile = sXMLPackageLocation + drCust["ID"].ToString() + "_" + drp["orderNumber"].ToString() + DateTime.Now.ToString( sFileFormat) + ".xml";
                                if (WritePackage( doc, sfile ) == false) 
                                    {
                                    System.Diagnostics.Debug.Print( "WriteDocument Err:" );
                                    Sql.WriteEvent( "CertnaBase.BuildXML WritePackage3: [" + sfile + "] ", true );
                                    }
                                }

                            curPayload = thispayloadid;
                            var imgdata = "";
                            try
                                {
                                imgdata = Convert.ToBase64String( File.ReadAllBytes( sTifFileLocation + drp["esubmitFileName"].ToString().Trim() ) );
                                }
                            catch (Exception ex)
                                {
                                Sql.WriteEvent( "CertnaBase.BuildXML Tiff [" + sTifFileLocation + drp["esubmitFileName"].ToString().Trim() + "] " + ex.Message, true );
                                return Errors.Tiffload ;
                                }

                            XmlNode pria_document = doc.CreateElement( "PRIA_DOCUMENT" );
                            XmlHelper.CreateAttribute( pria_document, "Code", drp["documentType"].ToString() );
                            XmlHelper.CreateAttribute( pria_document, "DocumentSequenceIdentifier", rec.ToString() );
                            XmlHelper.CreateAttribute( pria_document, "_UniqueIdentifier", drp["id"].ToString() );
                            package.AppendChild( pria_document );

                            XmlNode embedded_file = doc.CreateElement( "EMBEDDED_FILE" );
                            XmlHelper.CreateAttribute( embedded_file, "_PagesCount", drp["pageCount"].ToString() );
                            pria_document.AppendChild( embedded_file );

                            XmlNode docum = doc.CreateElement( "DOCUMENT" );
                            docum.InnerText = imgdata.ToString();
                            embedded_file.AppendChild( docum );

                            }
                        }
                    catch (Exception ex)
                        {
                        Sql.WriteEvent( "CertnaBase.BuildXML Payload loop: " + ex.Message, true );
                        return Errors.PayloadLoop ;
                        }

                    } //end rec loop

                //write package if one DOCUMENT
                try
                    {
                    if (dsPayload.Tables[0].Rows.Count > 0)
                        {
                        string sfile = sXMLPackageLocation + drCust["ID"].ToString() + "_" + dsPayload.Tables[0].Rows[dsPayload.Tables[0].Rows.Count - 1]["orderNumber"].ToString() + "_" + DateTime.Now.ToString( sFileFormat ) + ".xml";
                        if (WritePackage( doc, sfile ) == false)
                            {
                            System.Diagnostics.Debug.Print( "WriteDocument Err:" );
                            Sql.WriteEvent( "CertnaBase.BuildXML WritePackage1: ", true );
                            return Errors.WritePackage ;
                            }
                        }
                    }
                catch (Exception ex)
                    {
                    System.Diagnostics.Debug.Print( "WriteDocument Err:" + ex.Message );
                    Sql.WriteEvent( "CertnaBase.BuildXML WritePackage2: " + ex.Message, true );
                    return    Errors.WritePackage;
                    }

                return 0;
                } //end process
            catch (Exception ex)
                {
                System.Diagnostics.Debug.Print( "BuildXML Err:" + ex.Message );
                Sql.WriteEvent( "CertnaBase.BuildXML " + ex.Message, true );
                }
            return Errors.GeneralBuildXML   ;
            }


        bool WritePackage( XmlDocument doc, string sfile )
            {
            try
                {
                if (System.IO.File.Exists( sfile ) == true)
                    { System.IO.File.Delete( sfile ); }

                StringBuilder xmlxoc = new StringBuilder( 20000 );
                xmlxoc.Append( XmlHelper.DocumentToString( doc ) );
                xmlxoc.Remove( 0, 41 );
                StreamWriter strm = new StreamWriter( sfile );
                strm.Write( xmlxoc.ToString() );
                strm.Close();
                return true;
                }
            catch (Exception ex)
                {
                System.Diagnostics.Debug.Print( "WritePackage Err:" + ex.Message );
                Sql.WriteEvent( "CertnaBase.WritePackage Err:" + ex.Message, true );
                }
            return false;
            }

        }
    }

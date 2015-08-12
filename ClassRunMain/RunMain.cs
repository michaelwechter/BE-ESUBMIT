using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;

namespace ClassRunMain
    {
    public class RunMain
        {
        public delegate void ProgressUpdate( string data );
        public event ProgressUpdate OnProgressUpdate;

        public bool RunStatus = false;
        public string sLastFile = "none";
        SqlLib.SQLLib Sql = new SqlLib.SQLLib();

        private System.Data.DataSet dsOrders;
        private System.Data.DataSet dsCust;
        private System.Data.DataSet dsCounty;
        private System.Data.DataSet dsPayload;

        public void Run( )
            {

            RunStatus = true;
            try
                {

                while (RunStatus)
                    {
                    //process new orders
                    dsOrders = new System.Data.DataSet();
                    object[] param1 = { "3" };
                    if (Sql.ExecRsA( "esubmit_internal.es_getopenorders", CommandType.StoredProcedure, ref dsOrders, param1, "dbMySql", false ) == false)
                        {
                        Sql.WriteEvent( "RunMain.Run.es_getopenOrders [" + Sql.sErr + "]", true );
                        RunStatus = false;
                        continue;
                        }

                    for (int rec = 0; rec < dsOrders.Tables[0].Rows.Count; rec++)
                        {
                        try
                            {
                            DataRow dr = dsOrders.Tables[0].Rows[rec];

                            dsCust = new System.Data.DataSet();
                            object[] param = { dr["customerID"].ToString() };
                            if (Sql.ExecRsA( "1--core.es_getCustomer", CommandType.StoredProcedure, ref dsCust, param, "dbMySql", false ) == false)
                                {
                                Sql.WriteEvent( "RunMain.Run.es_getCustomer [" + Sql.sErr + "]", true );
                                continue;
                                }
                            DataRow drcust = dsCust.Tables[0].Rows[0];

                            dsCounty = new System.Data.DataSet();
                            object[] param2 = { Convert.ToInt32( dr["countyID"] ) };
                            if (Sql.ExecRsA( "1--core.es_getCounty", CommandType.StoredProcedure, ref dsCounty, param2, "dbMySql", false ) == false)
                                {
                                Sql.WriteEvent( "RunMain.Run.es_getCounty [" + Sql.sErr + "]", true );
                                continue;
                                }
                            DataRow drcounty = dsCounty.Tables[0].Rows[0];

                            String ssql = "SELECT qc_acmedev_1.Payload.*,qc_acmedev_1.Document.* FROM qc_acmedev_1.Payload INNER JOIN qc_acmedev_1.Document ON qc_acmedev_1.Payload.orderNumber = qc_acmedev_1.Document.orderNumber WHERE qc_acmedev_1.Payload.orderNumber = 'testzzz' AND qc_acmedev_1.Payload.isActive = 1 and Length(rtrim(qc_acmedev_1.Document.esubmitFileName))> 0 Order by qc_acmedev_1.Payload.payloadID, qc_acmedev_1.Document.ProcessingOrder ";
                            ssql = ssql.Replace( "acmedev", drcust["shortcode"].ToString().Trim() );
                            ssql = ssql.Replace( "testzzz", dr["OrderNumber"].ToString().Trim() );
                            dsPayload = new System.Data.DataSet();
                            if (Sql.ExecRsA( ssql, CommandType.Text, ref dsPayload, null, "dbMySql", false ) == false)
                                {
                                Sql.WriteEvent( "RunMain.Run.getpayload documents [" + Sql.sErr + "]", true );
                                continue;
                                }

                            //decide if Certna/Secure/Other ?
                            string sProc = "1"; //drcounty["processor"].Tostring()
                            CertnaBase.Certna.Errors RetErr = CertnaBase.Certna.Errors.Done;
                            switch (sProc)
                                {
                                case "1":
                                    CertnaBase.Certna cbase = new CertnaBase.Certna();
                                    cbase.drCounty = drcounty;
                                    cbase.drCust = drcust;
                                    cbase.drOrder = dr;
                                    cbase.dsPayload = dsPayload;
                                    RetErr = cbase.BuildXML();
                                    //maybe do something base on errors
                                    break;

                                case "2":
                                    SecureBase.Secure bbase = new SecureBase.Secure();
                                    bbase.drCounty = drcounty;
                                    bbase.drCust = drcust;
                                    bbase.drOrder = dr;
                                    bbase.dsPayload = dsPayload;
                                    //RetErr = bbase.BuildXML();
                                    //maybe do something base on errors
                                    break;

                                case "3":
                                    break;
                                }

                            //Data update
                            switch (RetErr)
                                {
                                case CertnaBase.Certna.Errors.Done: 
                                    RetErr = 0;
                                    break;

                                case CertnaBase.Certna.Errors.GeneralBuildXML:
                                    break;

                                case CertnaBase.Certna.Errors.NoPayloadRow:
                                    break;

                                case CertnaBase.Certna.Errors.PayloadLoop:
                                    break;

                                case CertnaBase.Certna.Errors.Tiffload:
                                    break;

                                case CertnaBase.Certna.Errors.WritePackage:
                                    break;

                                }

                            SendProgress( "# " + (rec + 1).ToString() + "  [Order: " + dr["OrderNumber"].ToString() + "]  [" + dr["customerID"].ToString() + " " + drcust["name"].ToString().Trim() + "]  [" + dr["countyID"].ToString() + " " + drcounty["Name"].ToString().Trim() + "]" );
                            }
                        catch (Exception ex)
                            {
                            Sql.WriteEvent( "RunMain.Run Orders loop [" + ex.Message + "]", true );
                            RunStatus = false;
                            continue;
                            }
                        }

                    System.Threading.Thread.Sleep( 10000 );

                    } //while loop
                }
            catch (Exception ex)
                {
                Sql.WriteEvent( "ClassRunMain:" + ex.Message, true );
                }
            }

        private void SendProgress( string msg )
            {
            if (OnProgressUpdate != null)
                {
                OnProgressUpdate( msg );
                }
            }

        }
    }

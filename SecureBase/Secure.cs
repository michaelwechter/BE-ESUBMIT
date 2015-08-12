using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.IO;


namespace SecureBase
{
    public class Secure
        {
        public DataRow drOrder;
        public DataRow drCust;
        public DataRow drCounty;
        public DataSet dsPayload;

        public bool BuildXML( )
            {
            try
                {

                //var data = File.ReadAllBytes( "image.tiff" );
               // var result = Convert.ToBase64String( data );


                }
            catch (Exception ex)
                {

                }
            return false;
            }


        }
    }

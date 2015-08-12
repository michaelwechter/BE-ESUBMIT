using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace ESubmitService
    {
    class ClassRunMain
        {
        public bool RunStatus = false;
        SqlLib.SQLLib Sql = new SqlLib.SQLLib();

        public void Run( )
            {

            RunStatus = true;
            try
                {
                while (RunStatus)
                    {




                    System.Threading.Thread.Sleep( 1000 );

                    }
                }
            catch (Exception ex)
                {
               Sql.WriteEvent ("ClassRunMain:" + ex.Message, true );
                }
            }




        }
    }

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureGUI
    {
    public partial class frmMain : Form
        {
        SqlLib.SQLLib Sql = new SqlLib.SQLLib() ;
        DataSet dsData;
        public frmMain( )
            {
            InitializeComponent();
            }

        private void button1_Click( object sender, EventArgs e )
            {
            if (Sql.dbOpenA( "dbmySQL" )) 
                {
                dsData = new DataSet() ;
              if (  Sql.ExecRsA( "Select documentId,documentcode,countyid from Document ", CommandType.Text, ref dsData,null,"",false ))
                    {
                    int xx = 0;
                    }   
              else
                    {
                    System.Diagnostics.Debug.Print( "Error: " + Sql.sErr );
                    }           
                }

            }
        }
    }

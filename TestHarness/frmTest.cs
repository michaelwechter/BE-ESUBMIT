using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlLib;

using System.Data.Sql;


namespace TestHarness
    {
    public partial class frmTest : Form
        {
        ClassRunMain.RunMain runmain = new ClassRunMain.RunMain();
        public frmTest( )
            {
            InitializeComponent();
            runmain.OnProgressUpdate += runmain_onProgressUpdate;
            }

        private void runmain_onProgressUpdate( string data )
            {
            base.Invoke( (Action)delegate
            {
                listBox1.Items.Insert( 0, data );
                } );
            }

        private void button1_Click( object sender, EventArgs e )
            {

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.RunWorkerAsync();
            }

        private void button2_Click( object sender, EventArgs e )
            {
            //backgroundWorker1.CancelAsync();
            if (runmain != null)
                {
                runmain.RunStatus = false;
                listBox1.Items.Insert( 0, "stopping at: " + DateTime.Now.ToString() );
                }
            }

        private void backgroundWorker1_DoWork( object sender, DoWorkEventArgs e )
            {

            runmain.Run();
            }

        private void backgroundWorker1_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
            {
            if (e.Cancelled)
                {
                listBox1.Items.Insert( 0, "The task has been cancelled" );
                }
            else if (e.Error != null)
                {
                listBox1.Items.Insert( 0, "Error. Details: " + (e.Error as Exception).ToString() );
                }
            else
                {
                listBox1.Items.Insert( 0, "The task has been completed. Results: " );
                }
            }

        private void backgroundWorker1_ProgressChanged( object sender, ProgressChangedEventArgs e )
            {
            listBox1.Items.Insert( 0, "Progress:" + e.UserState + "  " + e.ProgressPercentage.ToString() );
            }

        private void button3_Click( object sender, EventArgs e )
            {

            try
                {
                SqlLib.SQLLib sql = new SQLLib();

                ////read in customer
                //System.Data.DataSet dscust = new DataSet();
                //if (sql.ExecRsNB ("Select * from Customer",CommandType.Text ,ref dscust,null,"dbMSSql",false) == false)
                //    {
                //    int asdf = 0;
                //    }
                ////write customer to mysql
                //try
                //    {
                //    StringBuilder ssql = new StringBuilder();
                //    ssql.Append( "Insert into Customer (eSubmitId,CustName,CustAddress,CustCity,CustState,CustZip) Values " );
                //    for (int rec = 0; rec < dscust.Tables[0].Rows.Count; rec++)
                //        {
                //        if (rec > 0) { ssql.Append( "," ); } //add comma between multiple insert row
                //        ssql.Append( "(" ); // + dscust.Tables[0].Rows[rec]["CustomerId"] + "," );
                //        ssql.Append("'"+ dscust.Tables[0].Rows[rec]["CertnaId"] + "'," );
                //        ssql.Append( "'" + dscust.Tables[0].Rows[rec]["CustName"].ToString().Replace( "'", "" ) + "'," );
                //        ssql.Append( "'"+dscust.Tables[0].Rows[rec]["CustAddress"].ToString().Replace("'","") + "'," );
                //        ssql.Append( "'"+dscust.Tables[0].Rows[rec]["CustCity"] + "'," );
                //        ssql.Append("'"+ dscust.Tables[0].Rows[rec]["CustState"] + "'," );
                //        ssql.Append("'"+ dscust.Tables[0].Rows[rec]["CustZip"] +"'");
                //       // ssql.Append( dscust.Tables[0].Rows[rec]["BillingCode"] + "," );
                //       // ssql.Append( dscust.Tables[0].Rows[rec]["SecondaryValue"] );
                //        ssql.Append( ")" );
                //        }
                //    string sinsert = ssql.ToString();
                //    string ready = "";
                //    sql.ExecA( ssql.ToString(), CommandType.Text, "dbMySql", false );
                //    }
                //catch (Exception ex)
                //    {
                //    int zz = 0;
                //    }

                //read in recorder fee

                //insert into mysql recorder fee
                //System.Data.DataSet dsfees = new DataSet();
                //if (sql.ExecRsNB( "Select * from RecordingFees", CommandType.Text, ref dsfees, null, "dbMSSql", false ) == false)
                //    {
                //    int asdf = 0;
                //    }
                ////write customer to mysql
                //try
                //    {
                //    StringBuilder ssql = new StringBuilder();
                //    ssql.Append( "Insert into RecordingFees (DocRecId,UniqueOrderId,FeeDescription,FeeAmount) Values " );
                //    for (int rec = 0; rec < dsfees.Tables[0].Rows.Count; rec++)
                //        {
                //        if (rec > 0) { ssql.Append( "," ); } //add comma between multiple insert row
                //        ssql.Append( "(" ); // + dscust.Tables[0].Rows[rec]["CustomerId"] + "," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["DocRecId"] + "'," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["UniqueOrderId"].ToString().Replace( "'", "" ) + "'," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["FeeDescription"].ToString().Replace( "'", "" ) + "'," );
                //        ssql.Append( dsfees.Tables[0].Rows[rec]["FeeAmount"] );
                //        ssql.Append( ")" );
                //        }
                //    string sinsert = ssql.ToString();
                //    string ready = "";
                //    sql.ExecA( ssql.ToString(), CommandType.Text, "dbMySql", false );
                //    }
                //catch (Exception ex)
                //    {
                //    int zz = 0;
                //    }


                //read in rejected codes
                // insert in rejected codes
                //System.Data.DataSet dsfees = new DataSet();
                //if (sql.ExecRsNB( "Select * from RejectedErrors", CommandType.Text, ref dsfees, null, "dbMSSql", false ) == false)
                //    {
                //    int asdf = 0;
                //    }
                ////write customer to mysql
                //try
                //    {
                //    StringBuilder ssql = new StringBuilder();
                //    ssql.Append( "Insert into RejectedErrors (DocRecId,UniqueOrderId,ErrorDescription) Values " );
                //    for (int rec = 0; rec < dsfees.Tables[0].Rows.Count; rec++)
                //        {
                //        if (rec > 0) { ssql.Append( "," ); } //add comma between multiple insert row
                //        ssql.Append( "(" ); // + dscust.Tables[0].Rows[rec]["CustomerId"] + "," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["DocRecId"] + "'," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["UniqueOrderId"].ToString().Replace( "'", "" ) + "'," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["ErrorDescription"].ToString().Replace( "'", "" ) + "'" );
                //        ssql.Append( ")" );
                //        }
                //    string sinsert = ssql.ToString();
                //    string ready = "";
                //    sql.ExecA( ssql.ToString(), CommandType.Text, "dbMySql", false );
                //    }
                //catch (Exception ex)
                //    {
                //    int zz = 0;
                //    }

                //read fips_codes
                ////insert fips_codes
                //System.Data.DataSet dsfees = new DataSet();
                //if (sql.ExecRsNB( "Select * from Fips_codes", CommandType.Text, ref dsfees, null, "dbMSSql", false ) == false)
                //    {
                //    int asdf = 0;
                //    }
                ////write customer to mysql
                //try
                //    {
                //    StringBuilder ssql = new StringBuilder();
                //    ssql.Append( "Insert into FIPS_CODES (St_abbr,St_name,St_fips,County_name,County_FIPS) Values " );
                //    for (int rec = 0; rec < dsfees.Tables[0].Rows.Count; rec++)
                //        {
                //        if (rec > 0) { ssql.Append( "," ); } //add comma between multiple insert row
                //        ssql.Append( "(" ); // + dscust.Tables[0].Rows[rec]["CustomerId"] + "," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["ST_ABBR"] + "'," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["State_name"].ToString().Replace( "'", "" ) + "'," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["State_FIPS"].ToString().Replace( "'", "" ) + "'," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["County_name"].ToString().Replace( "'", "" ) + "'," );
                //        ssql.Append( "'" + dsfees.Tables[0].Rows[rec]["County_FIPS"].ToString().Replace( "'", "" ) + "'" );
                //        ssql.Append( ")" );
                //        }
                //    string sinsert = ssql.ToString();
                //    string ready = "";
                //    sql.ExecA( ssql.ToString(), CommandType.Text, "dbMySql", false );
                //    }
                //catch (Exception ex)
                //    {
                //    int zz = 0;
                //    }

                }
            catch (Exception ex)
                {
                int xx = 0;
                }
            }
        }
    }

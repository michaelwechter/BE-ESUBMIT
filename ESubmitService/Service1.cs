using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;



namespace ESubmitService
    {
    public partial class Service1 : ServiceBase
        {
        ClassRunMain.RunMain  RunMain;

        public Service1( )
            {

            InitializeComponent();
            }

        protected override void OnStart( string[] args )
            {
            RunMain = new ClassRunMain.RunMain ();
            RunMain.Run();
            }

        protected override void OnStop( )
            {
            RunMain.RunStatus = false;
            }
        }
    }

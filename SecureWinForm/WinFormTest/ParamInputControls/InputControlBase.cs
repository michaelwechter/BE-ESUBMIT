using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest.ParamInputControls
{
    public partial class InputControlBase : UserControl
    {
        protected string _paramVALUE;

        protected string _paramNAME;
        public virtual string ParameterName
        {
            get
            {
                return this._paramNAME;
            }
            set
            {
                this._paramNAME = value;
            }
        }

        public virtual string ParameterValue
        {
            get
            {
                return this._paramVALUE;
            }
            set
            {
                this._paramVALUE = value;
            }
        }

        public InputControlBase()
        {
            InitializeComponent();
        }
    }
}

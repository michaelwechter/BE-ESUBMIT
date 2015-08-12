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
    public partial class TextInput : InputControlBase
    {
        public override string ParameterName
        {
            get
            {
                return this.lblParameterName.Text;
            }
            set
            {
                this.lblParameterName.Text = value;
            }
        }

        public override string ParameterValue
        {
            get
            {
                return this.txtParameterValue.Text;
            }
            set
            {
                this.txtParameterValue.Text = value;
            }
        }

        public TextInput()
        {
            InitializeComponent();
        }
        public TextInput(string parameterName)
            : this()
        {
            this.lblParameterName.Text = parameterName;
        }
    }
}

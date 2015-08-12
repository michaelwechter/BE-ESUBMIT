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
    public partial class EnumSelector : InputControlBase
    {public override string ParameterName
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
                return this.comboBox1.SelectedItem.ToString();
            }
            set
            {
                this.comboBox1.SelectedItem = value;
            }
        }

        //public TextInput()
        //{
        //    InitializeComponent();
        //}
        //public TextInput(string parameterName)
        //    : this()
        //{
        //    this.lblParameterName.Text = parameterName;
        //}
        public EnumSelector()
        {
            InitializeComponent();
        }
        public EnumSelector(string parameterName)
            : this()
        {
            this.lblParameterName.Text = parameterName;
            switch (parameterName)
            {
                case "DocumentStatus":
                    {
                        this.comboBox1.Items.Clear();
                        this.comboBox1.Items.AddRange( Enum.GetNames(typeof(DocumentMediaStatusCode)));
                        break;
                    }
                case "BatchStatus":
                    {
                        this.comboBox1.Items.Clear();
                        this.comboBox1.Items.AddRange(Enum.GetNames(typeof(BatchMediaStatusCode)));
                        break;
                    }
            }
        }
    }
}

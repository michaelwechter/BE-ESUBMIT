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
    public partial class FileSelector : InputControlBase
    {
        private string _EncodedValue = string.Empty;

        public override string ParameterName
        {
            get
            {
                return this.lblParameterName.Text;
            }
        }
        public override string ParameterValue
        {
            get
            {
                //return this.txtParameterValue.Text;
                return this._EncodedValue;
            }
        }
        public FileSelector()
        {
            InitializeComponent();
        }
        public FileSelector(string parameterName)
            : this()
        {
            this.lblParameterName.Text = parameterName;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult DR = ofd.ShowDialog(this);
            if (DR == System.Windows.Forms.DialogResult.OK)
            {
                this.txtParameterValue.Text = ofd.FileName;
                // Perform encode
                this.PerformEncode();
            }
        }
        private async void PerformEncode()
        {
            //====================================
            LoadingForm loadingform = new LoadingForm("Encoding File into Base64");
            this.Enabled = false;
            loadingform.Show(this);
            //====================================
            this._EncodedValue = await TestController.EncodeBase64Async(this.txtParameterValue.Text);
            //====================================
            loadingform.Close();
            this.Enabled = true;
            this.Focus();
        }
    }
}

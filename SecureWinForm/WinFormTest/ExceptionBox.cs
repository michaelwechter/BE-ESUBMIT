using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class ExceptionBox : Form
    {
        public ExceptionBox()
        {
            InitializeComponent();
        }
        public string ExceptionMessage
        {
            get
            {
                return this.txtMessage.Text;
            }
            set
            {
                this.txtMessage.Text = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

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
    /// <summary>
    /// Form that displays a progress bar and text to the user while an operation is ongoing
    /// </summary>
    public partial class LoadingForm : Form
    {
        private string _displayText = string.Empty;
        private int ellipses_count = 0;
        /// <summary>
        /// constructor for LoadingForm
        /// </summary>
        public LoadingForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// constructor for LoadingForm
        /// </summary>
        /// <param name="displayText">string, containing the text to display to the user</param>
        public LoadingForm(string displayText):this()
        {
            this._displayText = displayText.Trim();
            this.lblDisplay.Text = this._displayText;
            this.progLoadingBar.Enabled = true;
        }
        public string DisplayText
        {
            set
            {
                this._displayText = value;
                this.lblDisplay.Text = this._displayText;
            }
            get
            {
                return this._displayText;
            }
        }

        private string _ellipses
        {
            get
            {
                string ee = string.Empty;
                this.ellipses_count = (this.ellipses_count > 20) ? 0 : this.ellipses_count+1;
                for(int i = 0; i < this.ellipses_count; i++)
                {
                    ee += ".";
                    //ee += "☆";
                }
                //ee += "(っ≧ω≦)っ";
                return ee;
            }
        }
        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            string ellipses = this._ellipses;
            this.lblDisplay.Text = string.Format("{0}  {1}", this._displayText, ellipses);
        }
    }
}

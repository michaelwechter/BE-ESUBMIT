using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest.ClientControls
{
    public partial class CountyInfoControl : UserControl
    {

        #region Declarations

        private CountyInfo _CountyInfo;
        /// <summary>
        /// CountyInfo object displayed in control
        /// </summary>
        public CountyInfo CountyInfo
        {
            get
            {
                return this._CountyInfo;
            }
            set
            {
                this._CountyInfo = value;
                if (this._CountyInfo == null)
                {
                    this._CountyInfo = new CountyInfo();
                    this.txtID.Text = string.Empty;
                    this.txtName.Text = string.Empty;
                    this.txtFipsStateCode.Text = string.Empty;
                    this.txtFipsCountyCode.Text = string.Empty;
                }
                else
                {
                    this.txtID.Text = this._CountyInfo._ID;
                    this.txtName.Text = this._CountyInfo.Name;
                    this.txtFipsStateCode.Text = this._CountyInfo.FipsStateCode;
                    this.txtFipsCountyCode.Text = this._CountyInfo.FipsCountyCode;
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Enable ID
        /// </summary>
        /// <param name="state">bool, true to enable</param>
        public void EnableID(bool state)
        {
            this.txtID.Enabled = state;
        }
        /// <summary>
        /// Enable Name
        /// </summary>
        /// <param name="state">bool, true to enable</param>
        public void EnableName(bool state)
        {
            this.txtName.Enabled = state;
        }
        /// <summary>
        /// Enable the FIPS State Code
        /// </summary>
        /// <param name="state">bool, true to enable</param>
        public void EnableFipsStateCode(bool state)
        {
            this.txtFipsStateCode.Enabled = state;
        }
        /// <summary>
        /// Enable the FIPS County Code
        /// </summary>
        /// <param name="state">bool, true to enable</param>
        public void EnableFipsCountyCode(bool state)
        {
            this.txtFipsCountyCode.Enabled = state;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CountyInfoControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// TextChanged event for ID TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            this._CountyInfo._ID = txtID.Text;
        }
        /// <summary>
        /// TextChanged event for Name TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this._CountyInfo.Name = txtName.Text;
        }
        /// <summary>
        /// TextChanged event for FipsStateCode TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFipsStateCode_TextChanged(object sender, EventArgs e)
        {
            this._CountyInfo.FipsStateCode = txtFipsStateCode.Text;
        }
        /// <summary>
        /// TextChanged event for FipsCountyCode TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFipsCountyCode_TextChanged(object sender, EventArgs e)
        {
            this._CountyInfo.FipsCountyCode = txtFipsCountyCode.Text;
        }

        #endregion
    }
}

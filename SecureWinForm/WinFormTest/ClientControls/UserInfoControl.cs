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
    public partial class UserInfoControl : UserControl
    {
        #region Declarations

        private UserInfo _UserInfo;
        public UserInfo UserInfo
        {
            get
            {
                return this._UserInfo;
            }
            set
            {
                _UserInfo = value;
                if (_UserInfo == null)
                {
                    _UserInfo = new UserInfo();
                    txtUserName.Text = string.Empty;
                    txtFirstName.Text = string.Empty;
                    txtLastName.Text = string.Empty;
                }
                else
                {
                    txtUserName.Text = this._UserInfo.UserName;
                    txtFirstName.Text = this._UserInfo.FirstName;
                    txtLastName.Text = this._UserInfo.LastName;
                }
            }
        }

        #endregion

        #region Properties

        public void EnableUserName(bool state)
        {
            this.txtUserName.Enabled = state;
        }
        public void EnableFirstName(bool state)
        {
            this.txtFirstName.Enabled = state;
        }
        public void EnableLastName(bool state)
        {
            this.txtLastName.Enabled = state;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public UserInfoControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// TextChanged event for UserName TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            this._UserInfo.UserName = this.txtUserName.Text;
        }

        /// <summary>
        /// TextChanged event for FirstName TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            this._UserInfo.FirstName = this.txtFirstName.Text;
        }

        /// <summary>
        /// TextChanged event for LastName TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            this._UserInfo.LastName = this.txtLastName.Text;
        }

        #endregion
    }
}

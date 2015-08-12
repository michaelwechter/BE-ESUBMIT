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
    public partial class RequestingPartyInfoControl : UserControl
    {
        #region Declarations

        private RequestingPartyInfo _RequestingPartyInfo;
        public RequestingPartyInfo RequestingPartyInfo
        {
            get
            {
                return this._RequestingPartyInfo;
            }
            set
            {
                this._RequestingPartyInfo = value;
                if (_RequestingPartyInfo == null)
                {
                    this._RequestingPartyInfo = new RequestingPartyInfo();
                    this.txtID.Text = string.Empty;
                    this.txtName.Text = string.Empty;
                    this.chkIsChangingParty.Checked = false;
                }
                else
                {
                    this.txtID.Text = this._RequestingPartyInfo._ID;
                    this.txtName.Text = this._RequestingPartyInfo.Name;
                    this.chkIsChangingParty.Checked = this._RequestingPartyInfo.IsChargingParty;
                }
            }
        }

        #endregion

        #region Properties

        public void EnableID(bool state)
        {
            this.txtID.Enabled = state;
        }
        public void EnableName(bool state)
        {
            this.txtName.Enabled = state;
        }
        public void EnableIsChangingParty(bool state)
        {
            this.chkIsChangingParty.Enabled = state;
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public RequestingPartyInfoControl()
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
            int anInt;
            if (int.TryParse(this.txtID.Text, out anInt))
            {
                this._RequestingPartyInfo._ID = this.txtID.Text;
            }
            else
            {
                this.txtID.Text = string.Empty;
            }
        }

        /// <summary>
        /// TextChanged event for Name TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this._RequestingPartyInfo.Name = this.txtName.Text;
        }

        /// <summary>
        /// CheckChanged event for Charging Party CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsChangingParty_CheckedChanged(object sender, EventArgs e)
        {
            this._RequestingPartyInfo.IsChargingParty = this.chkIsChangingParty.Checked;
        }

        #endregion
    }
}

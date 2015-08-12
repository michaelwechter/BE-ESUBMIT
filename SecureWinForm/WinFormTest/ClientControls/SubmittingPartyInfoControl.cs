using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class SubmittingPartyInfoControl : UserControl
    {
        #region Declarations

        private SubmittingPartyInfo _SubmittingPartyInfo;
        public SubmittingPartyInfo SubmittingPartyInfo
        {
            get
            {
                return this._SubmittingPartyInfo;
            }
            set
            {
                this._SubmittingPartyInfo = value;
                if (this._SubmittingPartyInfo == null)
                {
                    this._SubmittingPartyInfo = new SubmittingPartyInfo();
                    this.txtID.Text = string.Empty;
                    this.txtName.Text = string.Empty;
                    this.txtPartyType.Text = string.Empty;
                }
                else
                {
                    this.txtID.Text = this._SubmittingPartyInfo._ID;
                    this.txtName.Text = this._SubmittingPartyInfo.Name;
                    this.txtPartyType.Text = this._SubmittingPartyInfo.PartyTypeSpecified ? this._SubmittingPartyInfo.PartyType.ToString() : string.Empty;
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

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SubmittingPartyInfoControl()
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
                this._SubmittingPartyInfo._ID = this.txtID.Text;
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
            this._SubmittingPartyInfo.Name = this.txtName.Text;
        }

        #endregion
    }
}

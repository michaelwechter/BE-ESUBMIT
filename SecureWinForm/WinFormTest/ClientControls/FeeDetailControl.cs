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
    public partial class FeeDetailControl : UserControl
    {
        #region Declarations

        public enum ControlState
        {
            NewFee,
            ViewFee
        }
        private FeeDetail _FeeDetail;
        public FeeDetail FeeDetail
        {
            get
            {
                return this._FeeDetail;
            }
            set
            {
                this._FeeDetail = value;
                if (this._FeeDetail == null)
                {
                    this._FeeDetail = new FeeDetail();
                    this.txtID.Text = string.Empty;
                    this.txtCode.Text = string.Empty;
                    this.txtDescription.Text = string.Empty;
                    this.txtAmount.Text = string.Empty;
                    this.txtActionCode.Text = string.Empty;
                    this.txtCreateTimeStamp.Text = string.Empty;
                    this.txtEditTimeStamp.Text = string.Empty;
                }
                else
                {
                    this.txtID.Text = this._FeeDetail._ID;
                    this.txtCode.Text = this._FeeDetail.Code;
                    this.txtDescription.Text = this._FeeDetail.Description;
                    this.txtAmount.Text = this._FeeDetail.Amount.ToString();
                    this.txtActionCode.Text = this._FeeDetail.ActionCode.ToString();
                    this.txtCreateTimeStamp.Text = this._FeeDetail.CreateTimeStampSpecified ? this._FeeDetail.CreateTimeStamp.ToString() : string.Empty;
                    this.txtEditTimeStamp.Text = this._FeeDetail.EditTimeStamp.ToString();
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FeeDetailControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Set state of control
        /// </summary>
        /// <param name="controlState"></param>
        public void SetControlState(ControlState controlState)
        {
            switch (controlState)
            {
                case ControlState.NewFee:
                    {
                        this.txtCode.Enabled = true;
                        this.txtDescription.Enabled = true;
                        this.txtAmount.Enabled = true;
                        
                        break;
                    }
                case ControlState.ViewFee:
                    {
                        this.txtCode.Enabled = false;
                        this.txtDescription.Enabled = false;
                        this.txtAmount.Enabled = false;
                        break;
                    }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// TextChanged event for Code TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            this._FeeDetail.Code = this.txtCode.Text;
        }

        /// <summary>
        /// TextChanged event for Description TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            this._FeeDetail.Description = this.txtDescription.Text;
        }

        /// <summary>
        /// TextChanged event for Amount TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            //make sure entry in txtAmount is a decimal
            decimal newAmount;
            bool formatOK = decimal.TryParse(this.txtAmount.Text.ToString(), out newAmount);
            if (formatOK == true)
            {
                this._FeeDetail.Amount = newAmount;
            }
            else
            {
                this.txtAmount.Text = string.Empty;
            }
        }

        #endregion
    }
}

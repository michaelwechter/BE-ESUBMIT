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
    public partial class MemoDetailControl : UserControl
    {
        #region Declarations

        public enum MemoState
        {
            NewMemo,
            ViewMemo,
            EditMemo
        }
        private MemoDetail _MemoDetail;
        public MemoDetail MemoDetail
        {
            get
            {
                return this._MemoDetail;
            }
            set
            {
                this._MemoDetail = value;

                if (this._MemoDetail == null)
                {
                    this._MemoDetail = new MemoDetail();
                    this.txtMemo.Text = string.Empty;
                    this.txtUserDisplayName.Text = string.Empty;
                    this.txtUserName.Text = string.Empty;
                    this.txtActionCode.Text = string.Empty;
                    this.txtCreateTimeStamp.Text = string.Empty;
                    this.txtEditTimeStamp.Text = string.Empty;
                    this.cboMemoType.SelectedItem = null;
                }
                else
                {
                    this.txtMemo.Text = this._MemoDetail.Memo;
                    this.txtUserDisplayName.Text = this._MemoDetail.UserDisplayName;
                    this.txtUserName.Text = this._MemoDetail.UserName;
                    this.txtActionCode.Text = this._MemoDetail.ActionCode.ToString();
                    this.txtCreateTimeStamp.Text = this._MemoDetail.CreateTimeStamp.ToString();
                    this.txtEditTimeStamp.Text = this._MemoDetail.EditTimeStamp.ToString();
                    this.cboMemoType.SelectedItem = this._MemoDetail.TypeSpecified ? this._MemoDetail.Type.ToString() : null;
                }
            }
        }
   
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MemoDetailControl()
        {
            InitializeComponent();
            this.cboMemoType.Items.AddRange(Enum.GetNames(typeof(MemoTypes)));
        }

        #endregion

        #region Properties

        private MemoState _CurrentMemoState;
        public MemoState CurrentMemostate
        {
            get
            {
                return this._CurrentMemoState;
            }
            set
            {
                this._CurrentMemoState = value;
                switch (this._CurrentMemoState)
                {
                    case MemoState.NewMemo:
                        {
                            this.txtMemo.Enabled = true;
                            this.cboMemoType.Enabled = true;
                            break;
                        }
                    case MemoState.ViewMemo:
                        {
                            this.txtMemo.Enabled = false;
                            this.cboMemoType.Enabled = false;
                            break;
                        }
                    case MemoState.EditMemo:
                        {
                            this.txtMemo.Enabled = true;
                            this.cboMemoType.Enabled = true;
                            break;
                        }
                }
            }
        }
        
        #endregion 

        #region Event Methods

        /// <summary>
        /// TextChanged event for Memo TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMemo_TextChanged(object sender, EventArgs e)
        {
            this._MemoDetail.Memo = this.txtMemo.Text;
        }

        /// <summary>
        /// Selection Changed event for MemoType ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboMemoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboMemoType.SelectedItem != null)
            {
                this.MemoDetail.Type = (MemoTypes)Enum.Parse(typeof(MemoTypes), this.cboMemoType.SelectedItem.ToString());
                this.MemoDetail.TypeSpecified = true;
            }
            else
            {
                this.MemoDetail.TypeSpecified = false;
            }
        }

        #endregion
    }
}
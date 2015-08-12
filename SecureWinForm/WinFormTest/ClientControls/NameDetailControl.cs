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
    public partial class NameDetailControl : UserControl
    {
        #region Declarations

        public enum ControlState
        {
            NewName,
            ViewName
        }
        private NameDetail _NameDetail;
        public NameDetail NameDetail
        {
            get
            {
                return this._NameDetail;
            }
            set
            {
                this._NameDetail = value;
                if (this._NameDetail == null)
                {
                    this.txtID.Text = string.Empty;
                    this.cmbNameType.SelectedValue = null;
                    this.txtValue.Text = string.Empty;
                    this.txtActionCode.Text = string.Empty;
                    this.txtCreateTimeStamp.Text = string.Empty;
                    this.txtEditTimeStamp.Text = string.Empty;
                }
                else
                {
                    this.txtID.Text = this._NameDetail._ID;
                    this.cmbNameType.SelectedItem = this._NameDetail.Type.ToString();
                    this.txtValue.Text = this._NameDetail.Value;
                    this.txtActionCode.Text = this._NameDetail.ActionCode.ToString();
                    this.txtCreateTimeStamp.Text = this._NameDetail.CreateTimeStampSpecified ? this._NameDetail.CreateTimeStamp.ToString() : string.Empty;
                    this.txtEditTimeStamp.Text = this._NameDetail.EditTimeStamp.ToString();
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public NameDetailControl()
        {
            InitializeComponent();
            this.cmbNameType.Items.Clear();
            foreach (string nameTypeDesc in Enum.GetNames(typeof(NameTypes)))
            {
                this.cmbNameType.Items.Add(nameTypeDesc);
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Set state of Control
        /// </summary>
        /// <param name="controlState"></param>
        public void SetControlState(ControlState controlState)
        {
            switch (controlState)
            {
                case ControlState.NewName:
                    {
                        this.txtValue.Enabled = true;
                        this.cmbNameType.Enabled = true;
                        break;
                    }
                case ControlState.ViewName:
                    {
                        this.txtValue.Enabled = false;
                        this.cmbNameType.Enabled = false;
                        break;
                    }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// TextChanged event for Value TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            this.NameDetail.Value = this.txtValue.Text;
        }

        /// <summary>
        /// Selection Changed event for NameType ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNameType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbNameType.SelectedItem != null)
            {
                this._NameDetail.Type = (NameTypes)Enum.Parse(typeof(NameTypes), this.cmbNameType.SelectedItem.ToString());
            }
        }
       
        #endregion

        private void txtPrefix_TextChanged(object sender, EventArgs e)
        {
            this.NameDetail.Prefix = this.txtPrefix.Text;
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            this.NameDetail.FirstName = this.txtFirstName.Text;
        }

        private void txtMiddleName_TextChanged(object sender, EventArgs e)
        {
            this.NameDetail.MiddleName = this.txtMiddleName.Text;
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            this.NameDetail.LastName = this.txtLastName.Text;
        }

        private void txtSuffix_TextChanged(object sender, EventArgs e)
        {
            this.NameDetail.Suffix = this.txtSuffix.Text;
        }
    }
}

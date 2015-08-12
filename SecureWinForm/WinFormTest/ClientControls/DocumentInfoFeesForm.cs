using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest.ClientControls
{
    public partial class DocumentInfoFeesForm : Form
    {

        #region Declarations

        public enum FormState
        {
            NewFee,
            ViewFee
        }
        public FeeDetail FeeDetail
        {
            get
            {
                return this.ctrlFeeDetail.FeeDetail;
            }
            set
            {
                this.ctrlFeeDetail.FeeDetail = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="feeDetail"></param>
        /// <param name="formState"></param>
        public DocumentInfoFeesForm(FeeDetail feeDetail, FormState formState)
            : this()
        {
            this.FeeDetail = feeDetail;
            this.SetFormState(formState);
            switch (formState)
            {
                case FormState.NewFee:
                    this.ctrlFeeDetail.SetControlState(FeeDetailControl.ControlState.NewFee);
                    break;
                case FormState.ViewFee:
                    this.ctrlFeeDetail.SetControlState(FeeDetailControl.ControlState.ViewFee);
                    break;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentInfoFeesForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Configure form
        /// </summary>
        /// <param name="formState">FormState, state to configure form</param>
        public void SetFormState(FormState formState)
        {
            switch (formState)
            {
                case FormState.NewFee:
                    this.btnSave.Visible = true;
                    this.btnSave.Enabled = true;
                    break;
                case FormState.ViewFee:
                    this.btnSave.Visible = false;
                    this.btnSave.Enabled = false;
                    break;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Click event for Save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Click event for Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion
    }
}

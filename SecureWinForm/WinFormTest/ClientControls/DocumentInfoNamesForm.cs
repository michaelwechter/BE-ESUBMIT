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
    public partial class DocumentInfoNamesForm : Form
    {

        #region Declarations

        public enum FormState
        {
            NewName,
            ViewName
        }
        public NameDetail NameDetail
        {
            get
            {
                return this.ctrlNameDetail.NameDetail;
            }
            set
            {
                this.ctrlNameDetail.NameDetail = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nameDetail"></param>
        /// <param name="formState"></param>
        public DocumentInfoNamesForm(NameDetail nameDetail, FormState formState)
            : this()
        {
            this.NameDetail = nameDetail;
            this.SetFormState(formState);
            switch (formState)
            {
                case FormState.NewName:
                    this.ctrlNameDetail.SetControlState(NameDetailControl.ControlState.NewName);
                    break;
                case FormState.ViewName:
                    this.ctrlNameDetail.SetControlState(NameDetailControl.ControlState.ViewName);
                    break;
            }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentInfoNamesForm()
        {
            InitializeComponent();
            
        }

        #endregion

        #region Functions

        /// <summary>
        /// Configure form state
        /// </summary>
        /// <param name="formState">FormState, to configure form</param>
        public void SetFormState(FormState formState)
        {
            switch (formState)
            {
                case FormState.NewName:
                    this.btnSave.Visible = true;
                    this.btnSave.Enabled = true;
                    break;
                case FormState.ViewName:
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

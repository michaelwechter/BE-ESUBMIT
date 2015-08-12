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
    public partial class MemoDetailForm : Form
    {
        #region Declarations

        public enum FormState
        {
            NewMemo,
            ViewMemo,
            EditMemo
        }
        public MemoDetail MemoDetail
        {
            get
            {
                return ctrlMemoDetail.MemoDetail;
            }

            set
            {
                this.ctrlMemoDetail.MemoDetail = value;
            }
        }
        private string originalMemoXML = string.Empty;
        public FormState MemoFormState;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoDetail">MemoDetail, memo to be displayed in form</param>
        /// <param name="formState">FormState, describing the current form state</param>
        public MemoDetailForm(MemoDetail memoDetail, FormState formState):this()
        {
            this.MemoDetail = memoDetail;
            this.originalMemoXML = TestController.SerializeXML(memoDetail, false);
            this.SetFormState(formState);
            switch (formState)
            {
                case FormState.NewMemo:
                    {
                        this.ctrlMemoDetail.CurrentMemostate = MemoDetailControl.MemoState.NewMemo;
                        break;
                    }
                case FormState.ViewMemo:
                    {
                        this.ctrlMemoDetail.CurrentMemostate = MemoDetailControl.MemoState.ViewMemo;
                        break;
                    }
                case FormState.EditMemo:
                    {
                        this.ctrlMemoDetail.CurrentMemostate = MemoDetailControl.MemoState.EditMemo;
                        break;
                    }
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public MemoDetailForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Set form state
        /// </summary>
        /// <param name="formState">FormState, describing the current form state</param>
        public void SetFormState(FormState formState)
        {
            this.MemoFormState = formState;
            switch (formState)
            {
                case FormState.NewMemo:
                case FormState.EditMemo:
                    {
                        this.btnSave.Visible = true;
                        this.btnSave.Enabled = true;
                        break;
                    }
                case FormState.ViewMemo:
                    {
                        this.btnSave.Visible = false;
                        this.btnSave.Enabled = false;
                        break;
                    }
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
            switch (this.MemoFormState)
            {
                case FormState.EditMemo:
                    {
                        this.MemoDetail.ActionCode = (this.originalMemoXML != TestController.SerializeXML(this.MemoDetail, false) && this.MemoDetail.ActionCode != ActionCode.New) ? ActionCode.Edit : this.MemoDetail.ActionCode;
                        break;
                    }
                case FormState.NewMemo:
                    {
                        this.MemoDetail.ActionCode = ActionCode.New;
                        break;
                    }
                case FormState.ViewMemo:
                    {
                        this.MemoDetail.ActionCode = ActionCode.None;
                        break;
                    }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Click event for Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.MemoFormState == FormState.EditMemo)
            {
                // set object back to original values
                MemoDetail originalMemo = TestController.DeserializeXML<MemoDetail>(this.originalMemoXML, false);
                this.MemoDetail.Memo = originalMemo.Memo;
                this.MemoDetail.Type = originalMemo.Type;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #endregion
    }
}

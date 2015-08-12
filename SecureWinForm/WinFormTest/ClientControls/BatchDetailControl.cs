using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SECURE.Common.Controller.Media.Schema.CodeGen;
using System.Windows;
using System.Collections;

namespace WinFormTest.ClientControls
{
    public partial class BatchDetailControl : UserControl
    {

        #region Declarations

        private BatchDetail _BatchDetail;
        private bool _allowEdit = false;

        #region Properties

        /// <summary>
        /// BatchDetail object being displayed in this control
        /// </summary>
        public BatchDetail BatchDetail
        {
            get
            {
                return this._BatchDetail;
            }
            set
            {
                this._BatchDetail = value;
                if (this._BatchDetail == null)
                {
                    this._BatchDetail = new BatchDetail();
                    this.txtBatchID.Text = string.Empty;
                    this.txtSubmissionID.Text = string.Empty;
                    this.txtName.Text = string.Empty;
                    this.txtTransactionNumber.Text = string.Empty;
                    this.txtStatusCode.Text = string.Empty;
                    this.txtCheckedOutBy.Text = string.Empty;
                    this.txtCheckedOutTS.Text = string.Empty;
                    this.txtCreateTS.Text = string.Empty;
                    this.txtEditTS.Text = string.Empty;

                    this.cmbCounty.SelectedItem = null;
                    this.cmbProcessQueue.SelectedItem = null;
                    this.txtSubmittingParty_Name.Text = string.Empty;
                    this.cmbRequestingParty.SelectedItem = null;
                    this.txtSubmittingParty_PartyType.Text = string.Empty;
                    this.chkIsConcurrent.Checked = false;
                    this.UpdateMemoList();
                }
                else
                {
                    this.txtBatchID.Text = this._BatchDetail._ID;
                    this.txtBatchID.Visible = this._BatchDetail._ID != null && (int.Parse(this._BatchDetail._ID) > 0);
                    this.txtSubmissionID.Text = this._BatchDetail._SubmissionID;
                    this.txtName.Text = this._BatchDetail.Name;
                    this.txtTransactionNumber.Text = this._BatchDetail.TransactionNumber;
                    this.txtStatusCode.Text = this._BatchDetail.StatusCode.ToString();
                    this.txtCheckedOutBy.Text = (this._BatchDetail.CheckedOutBy == null) ? string.Empty : this._BatchDetail.CheckedOutBy.UserName;
                    this.txtCheckedOutTS.Text = this._BatchDetail.CheckedOutTimeStampSpecified ? this._BatchDetail.CheckedOutTimeStamp.ToString() : string.Empty;
                    this.chkIsConcurrent.Checked = this._BatchDetail.IsConcurrent;
                    this.txtCreateTS.Text = this._BatchDetail.CreateTimeStampSpecified ? this._BatchDetail.CreateTimeStamp.ToString() : string.Empty;
                    this.txtEditTS.Text = this._BatchDetail.EditTimeStamp.ToString();
                    if (this._BatchDetail.Memos == null)
                    {
                        this._BatchDetail.Memos = new MemoDetail[0];
                    }
                    this.UpdateMemoList();
                    if (this._BatchDetail.County != null)
                    {
                        this.cmbCounty.Text = this._BatchDetail.County.Name;
                    }
                    if (this._BatchDetail.ProcessQueue != null)
                    {
                        this.cmbProcessQueue.Text = this._BatchDetail.ProcessQueue.Name;
                    }
                    if (this._BatchDetail.SubmittingParty != null)
                    {
                        this.txtSubmittingParty_Name.Text = this._BatchDetail.SubmittingParty.Name;
                        this.txtSubmittingParty_PartyType.Text = this._BatchDetail.SubmittingParty.PartyTypeSpecified ? this._BatchDetail.SubmittingParty.PartyType.ToString() : string.Empty;
                    }
                    if (this._BatchDetail.RequestingParty != null)
                    {
                        this.cmbRequestingParty.Text = this._BatchDetail.RequestingParty.Name;
                    }
                }
            }
        }
        /// <summary>
        /// Array of CountyInfo objects that can be used in this control
        /// </summary>
        public CountyInfo[] CountyInfo
        {
            set
            {
                this.cmbCounty.Items.Clear();
                this.cmbCounty.SelectedItem = null;
                if (value == null)
                {
                    this.cmbCounty.Text = string.Empty;
                    this.cmbCounty.Enabled = false;
                }
                else
                {
                    this.cmbCounty.Enabled = true;
                    this.cmbCounty.Items.AddRange(value);
                    this.SetComboBox(this.cmbCounty, "_ID", "Name");
                }
            }
        }
       
        private ProcessQueueDetail[] _ProcessQueueDetail;
        /// <summary>
        /// Array of ProcessQueueDetail objects that can be used in this control
        /// </summary>
        public ProcessQueueDetail[] ProcessQueueDetail
        {
            set
            {
                this.cmbProcessQueue.Items.Clear();
                this.cmbProcessQueue.SelectedItem = null;
                this._ProcessQueueDetail = value;
                if (value == null)
                {
                    this.cmbProcessQueue.Enabled = false;
                    this.cmbProcessQueue.Text = string.Empty;
                }
                else
                {
                    this.cmbProcessQueue.Enabled = true;
                    this.cmbProcessQueue.Items.AddRange(value);
                    this.SetComboBox(this.cmbProcessQueue, "_ID", "Name");
                }
            }
        }
        /// <summary>
        /// Array of RequestingPartyInfo that can be used in this control
        /// </summary>
        public RequestingPartyInfo[] RequestingPartyInfo
        {
            set
            {
                this.cmbRequestingParty.Items.Clear();
                this.cmbRequestingParty.SelectedItem = null;
                if (value == null)
                {
                    this.cmbRequestingParty.Enabled = false;
                    this.cmbRequestingParty.Text = string.Empty;
                    this.cmbRequestingParty.Enabled = false;
                }
                else
                {
                    this.cmbRequestingParty.Enabled = this.EnableRequestingParty;
                    this.cmbRequestingParty.Items.AddRange(value);
                    this.SetComboBox(this.cmbRequestingParty, "_ID", "Name");
                }
            }
        }

        private bool _enableConcurrentIndex = false;
        /// <summary>
        /// Enable/disable the ConcurrentIndex field
        /// </summary>
        public bool EnableConcurrentIndex
        {
            get
            {
                return this._enableConcurrentIndex;
            }
            set
            {
                this._enableConcurrentIndex = value;
                this.chkIsConcurrent.Enabled = this._enableConcurrentIndex;
            }
        }
        private bool _enableRequestingParty = false;
        /// <summary>
        /// Enable/disable the Requesting Party field
        /// </summary>
        public bool EnableRequestingParty
        {
            get
            {
                return this._enableRequestingParty;
            }
            set
            {
                this.cmbRequestingParty.Enabled = this._enableRequestingParty = value;
            }
        }
        /// <summary>
        /// Update the list of Memos
        /// </summary>
        public void UpdateMemoList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index", typeof(int));
            dt.Columns.Add("EditTS", typeof(string));
            if (this._BatchDetail.Memos != null)
            {
                for (int i = 0; i <= this._BatchDetail.Memos.Length - 1; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Index"] = i;
                    dr["EditTS"] = this._BatchDetail.Memos[i].EditTimeStamp;
                    dt.Rows.Add(dr);
                }
            }
            this.lstMemos.DataSource = dt;
            this.SetListBox(this.lstMemos, "Index", "EditTS");
        }
        /// <summary>
        /// Enable/disable data editing on this control
        /// </summary>
        public bool EnableEdit
        {
            get
            {
                return this._allowEdit;
            }
            set
            {
                this._allowEdit = value;
                this.txtName.Enabled =                    
                    this.btnAdd.Enabled = this._allowEdit;
                this.cmbRequestingParty.Enabled = this._enableRequestingParty && this._allowEdit;
                this.chkIsConcurrent.Enabled = this._enableConcurrentIndex && this._allowEdit;
                this.cmbCounty.Enabled = this._allowEdit && int.Parse(this._BatchDetail._ID) < 0;
                this.cmbProcessQueue.Enabled = this._allowEdit && this._BatchDetail.StatusCode == BatchMediaStatusCode.New;
            }
        }
        #endregion

        #endregion

        #region Private Functions

        /// <summary>
        /// Configure ListBox
        /// </summary>
        /// <param name="lstObj">ListBox to be configured</param>
        /// <param name="valueMember">string, the property of the object to be used as value</param>
        /// <param name="displayMember">string, the property of the object to be used as display</param>
        private void SetListBox(ListBox lstObj, string valueMember, string displayMember)
        {
            lstObj.ValueMember = valueMember;
            lstObj.DisplayMember = displayMember;
        }

        /// <summary>
        /// Configure ComboBox
        /// </summary>
        /// <param name="cmbObj">ComboBox to be configured</param>
        /// <param name="valueMember">string, the property of the object to be used as value</param>
        /// <param name="displayMember">string, the property of the object to be used as display</param>
        private void SetComboBox(ComboBox cmbObj, string valueMember, string displayMember)
        {
            cmbObj.ValueMember = valueMember;
            cmbObj.DisplayMember = displayMember;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BatchDetailControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        #region TextBox Events
        
        /// <summary>
        /// TextChanged event for Name TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this._BatchDetail.Name = this.txtName.Text;
        }
        
        /// <summary>
        /// TextChanged event for TransactionNumber TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTransactionNumber_TextChanged(object sender, EventArgs e)
        {
            this._BatchDetail.TransactionNumber = this.txtTransactionNumber.Text;
        }

        #endregion

        #region ComboBox Events 
        
        /// <summary>
        /// Selection changed event for County ComboBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._BatchDetail.County = (CountyInfo)cmbCounty.SelectedItem;
            if (this._BatchDetail.County != null)
            {
                if (this._BatchDetail.ProcessQueue != null && this._BatchDetail.ProcessQueue._CountyID != this._BatchDetail.County._ID)
                {
                    this._BatchDetail.ProcessQueue = null;
                    this.cmbProcessQueue.SelectedItem = null;
                }
                this.SetupProcessQueueForCounty(this._BatchDetail.County._ID);
            }
        }

        /// <summary>
        /// Selection changed event for Process Queue ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProcessQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._BatchDetail.ProcessQueue = (ProcessQueueDetail)cmbProcessQueue.SelectedItem;
            this.txtProcessQueue_Description.Text = this._BatchDetail.ProcessQueue != null ? (this._BatchDetail.ProcessQueue.Description != null ? this._BatchDetail.ProcessQueue.Description : "(no description)") : "(no description)";
            this.txtProcessQueue_Open.Text = this._BatchDetail.ProcessQueue != null ? (this._BatchDetail.ProcessQueue.OpenTimeSpecified ? this._BatchDetail.ProcessQueue.OpenTime.ToShortTimeString() : "(none)") : "(none)";
            this.txtProcessQueue_Close.Text = this._BatchDetail.ProcessQueue != null ? (this._BatchDetail.ProcessQueue.CloseTimeSpecified ? this._BatchDetail.ProcessQueue.CloseTime.ToShortTimeString() : "(none)") : "(none)";
        }       
                
        /// <summary>
        /// Selection changed event for Requesting Party ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRequestingParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._BatchDetail.RequestingParty = (RequestingPartyInfo)this.cmbRequestingParty.SelectedItem;
        }

        #endregion

        #region CheckBox Events

        /// <summary>
        /// Check event method for IsConcurrent Checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsConcurrent_CheckedChanged(object sender, EventArgs e)
        {
            this._BatchDetail.IsConcurrent = this.chkIsConcurrent.Checked;
        }

        #endregion

        /// <summary>
        /// Double Click method for ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstMemos_DoubleClick(object sender, EventArgs e)
        {
            if (this._allowEdit)
            {
                if (this.lstMemos.SelectedItem == null)
                {
                    MemoDetail newMemo = new MemoDetail();
                    newMemo._ID = (new Random(DateTime.Now.Millisecond)).Next(-999, 0).ToString();
                    newMemo.CreateTimeStamp = System.DateTime.Now;
                    newMemo.EditTimeStamp = newMemo.CreateTimeStamp;
                    newMemo.Type = MemoTypes.Note;
                    newMemo.ActionCode = ActionCode.New;

                    MemoDetailForm newMemoDetailForm = new MemoDetailForm(newMemo, MemoDetailForm.FormState.NewMemo);

                    if (newMemoDetailForm.ShowDialog(this) == DialogResult.OK)
                    {
                        MemoDetail[] newMemoArray = new MemoDetail[this._BatchDetail.Memos.Length + 1];
                        Array.Copy(this._BatchDetail.Memos, 0, newMemoArray, 0, this._BatchDetail.Memos.Length);
                        newMemoArray[this._BatchDetail.Memos.Length] = newMemoDetailForm.MemoDetail;
                        this.BatchDetail.Memos = newMemoArray;
                        this.UpdateMemoList();
                    }
                }
                else
                {
                    MemoDetail selectedMemo = this._BatchDetail.Memos[this.lstMemos.SelectedIndex];
                    MemoDetailForm newMemoDetailForm = new MemoDetailForm(selectedMemo, MemoDetailForm.FormState.EditMemo);
                    newMemoDetailForm.ShowDialog(this);
                }
            }
            else
            {
                if (this.lstMemos.SelectedItem != null)
                {
                    MemoDetail selectedMemo = this._BatchDetail.Memos[this.lstMemos.SelectedIndex];
                    MemoDetailForm newMemoDetailForm = new MemoDetailForm(selectedMemo, MemoDetailForm.FormState.ViewMemo);
                    newMemoDetailForm.ShowDialog(this);
                }
            }
        }

        /// <summary>
        /// Click event for Add buttom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            MemoDetail newMemo = new MemoDetail();
            newMemo._ID = (new Random(DateTime.Now.Millisecond)).Next(-999, 0).ToString();
            newMemo.CreateTimeStamp = System.DateTime.Now;
            newMemo.EditTimeStamp = System.DateTime.Now;
            newMemo.ActionCode = ActionCode.New;

            MemoDetailForm newMemoDetailForm = new MemoDetailForm(newMemo, MemoDetailForm.FormState.NewMemo);

            if (newMemoDetailForm.ShowDialog(this) == DialogResult.OK)
            {
                MemoDetail[] newMemoArray = new MemoDetail[this._BatchDetail.Memos.Length + 1];
                Array.Copy(
                    this._BatchDetail.Memos, 
                    0, 
                    newMemoArray, 
                    0, 
                    this._BatchDetail.Memos.Length);
                newMemoArray[this._BatchDetail.Memos.Length] = newMemoDetailForm.MemoDetail;
                this.BatchDetail.Memos = newMemoArray;
                this.UpdateMemoList();
            }
        }

        /// <summary>
        /// Change ProcessQueue ComboBox items for specific county
        /// </summary>
        /// <param name="countyID">string, containing the County's ID</param>
        private void SetupProcessQueueForCounty(string countyID)
        {
            this.cmbProcessQueue.Items.Clear();
            this.cmbProcessQueue.Items.AddRange(this.GetProcessQueueForCounty(countyID));
            this.SetComboBox(this.cmbProcessQueue, "_ID", "Name");
        }

        /// <summary>
        /// Select ProcessQueueDetails for a specific county
        /// </summary>
        /// <param name="countyID">string, containing the County's ID</param>
        /// <returns>ProcessQueueDetail array</returns>
        private ProcessQueueDetail[] GetProcessQueueForCounty(string countyID)
        {
            ArrayList countyPQ = new ArrayList();
            if (this._ProcessQueueDetail != null)
            {
                foreach (ProcessQueueDetail processQueueDetail in this._ProcessQueueDetail)
                {
                    if (processQueueDetail._CountyID == countyID)
                    {
                        countyPQ.Add(processQueueDetail);
                    }
                }
            }
            return (ProcessQueueDetail[])countyPQ.ToArray(typeof(ProcessQueueDetail));
        }

        #endregion

        #region Public Functions 

        /// <summary>
        /// Refresh UI with data
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            // Force load display controls from this._BatchDetail
            this.BatchDetail = this._BatchDetail;
        }

        #endregion

    }
}

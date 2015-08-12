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
    public partial class DocumentInfoControl : UserControl
    {
        #region Declarations

        public List<TitleDetail> AllTitles = new List<TitleDetail>();
        public List<CityDetail> AllCities = new List<CityDetail>();

        public List<TitleDetail> CurrentTitles;
        public List<CityDetail> CurrentCities;
        public List<FeeDetail> CurrentFees;
        public List<NameDetail> CurrentNames;
        public List<MemoDetail> CurrentMemos;

        public int MemoCount
        {
            get
            {
                if (this._DocumentInfo == null)
                {
                    return 0;
                }
                else
                {
                    if (this._DocumentInfo.Memos == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return this._DocumentInfo.Memos.Length;
                    }
                }
            }
        }
        public int FeeCount
        {
            get
            {
                if (this._DocumentInfo == null)
                {
                    return 0;
                }
                else
                {
                    if (this._DocumentInfo.Fees == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return this._DocumentInfo.Fees.Length;
                    }
                }
            }
        }
        public int NameCount
        {
            get
            {
                if (this._DocumentInfo == null)
                {
                    return 0;
                }
                else
                {
                    if (this._DocumentInfo.Names == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return this._DocumentInfo.Names.Length;
                    }
                }
            }
        }
        private DocumentInfo _DocumentInfo;
        public DocumentInfo DocumentInfo
        {
            get
            {
                return this._DocumentInfo;
            }
            set
            {
                this._DocumentInfo = value;

                if (this._DocumentInfo == null)
                {
                    this._DocumentInfo = new DocumentInfo()
                    {
                        Cities = new CityDetail[0],
                        Titles = new TitleDetail[0],
                        Fees = new FeeDetail[0],
                        Names = new NameDetail[0],
                        Memos = new MemoDetail[0]
                    };
                    this.txtNumber.Text = string.Empty;
                    //this.txtRecordedTimeStamp.Text = string.Empty;
                    this.dtpRecordedTS.Visible = false;
                    this.txtStatusCode.Text = string.Empty;
                    this.txtActionCode.Text = string.Empty;
                    this.txtSequence.Text = string.Empty;
                    this.txtAssessorParcelNumber.Text = string.Empty;
                    this.txtTransferTaxAmount.Text = string.Empty;
                    this.txtSaleAmount.Text = string.Empty;
                    this.lstTitles.DataSource = null;
                    this.lstTitles.Items.Clear();
                    this.lstCities.DataSource = null;
                    this.lstCities.Items.Clear();
                    this.lstFees.DataSource = null;
                    this.lstFees.Items.Clear();
                    this.lstNames.DataSource = null;
                    this.lstNames.Items.Clear();
                    this.txtVersion.Text = string.Empty;
                    this.txtCreateTimeStamp.Text = string.Empty;
                    this.txtEditTimeStamp.Text = string.Empty;
                    this.lstMemos.Text = string.Empty;
                    this.txtID.Visible = false;
                }
                else
                {
                    this.txtID.Visible = this._DocumentInfo._ID != null && int.Parse(this._DocumentInfo._ID) > 0;
                    this.txtID.Text = this._DocumentInfo._ID;
                    this.txtNumber.Text = this._DocumentInfo.Number.ToString();
                    //this.txtRecordedTimeStamp.Text = this._DocumentInfo.RecordedTimeStampSpecified ? this._DocumentInfo.RecordedTimeStamp.ToString() : string.Empty;
                    this.dtpRecordedTS.Visible = this._DocumentInfo.RecordedTimeStampSpecified;
                    if (this._DocumentInfo.RecordedTimeStampSpecified)
                    {
                        this.dtpRecordedTS.Value = this._DocumentInfo.RecordedTimeStamp;
                    }
                    this.txtStatusCode.Text = this._DocumentInfo.StatusCode.ToString();
                    this.txtActionCode.Text = this._DocumentInfo.ActionCode.ToString();
                    this.txtSequence.Text = this._DocumentInfo.Sequence.ToString();
                    this.txtAssessorParcelNumber.Text = this._DocumentInfo.AssessorParcelNumber;
                    this.txtTransferTaxAmount.Text = this._DocumentInfo.TransferTaxAmount.ToString();
                    this.txtSaleAmount.Text = this._DocumentInfo.SaleAmount.ToString();
                    this.txtVersion.Text = this._DocumentInfo.Version.ToString();
                    this.txtCreateTimeStamp.Text = this._DocumentInfo.CreateTimeStampSpecified ? _DocumentInfo.CreateTimeStamp.ToString() : string.Empty;
                    this.txtEditTimeStamp.Text = this._DocumentInfo.EditTimeStamp.ToString();
                    #region Titles
                    if (this._DocumentInfo.Titles == null)
                    {
                        this._DocumentInfo.Titles = new TitleDetail[] { };
                        this.UpdateTitlesList();
                    }
                    else
                    {
                        this.CurrentTitles = this._DocumentInfo.Titles.ToList<TitleDetail>();
                        this.UpdateTitlesList();
                    }
                    #endregion
                    #region Cities
                    if (this._DocumentInfo.Cities == null)
                    {
                        this._DocumentInfo.Cities = new CityDetail[] { };
                        this.UpdateCitiesList();
                    }
                    else
                    {
                        this.CurrentCities = this._DocumentInfo.Cities.ToList<CityDetail>();
                        this.UpdateCitiesList();
                    }
                    #endregion
                    #region Fees
                    if (this._DocumentInfo.Fees == null)
                    {
                        this._DocumentInfo.Fees = new FeeDetail[] { };
                        this.UpdateFeesList();
                    }
                    else
                    {
                        this.CurrentFees = this._DocumentInfo.Fees.ToList<FeeDetail>();
                        this.UpdateFeesList();
                    }
                    #endregion
                    #region Names
                    if (this._DocumentInfo.Names == null)
                    {
                        this._DocumentInfo.Names = new NameDetail[] { };
                        this.UpdateNamesList();
                    }
                    else
                    {
                        this.CurrentNames = new List<NameDetail>(this._DocumentInfo.Names.ToList<NameDetail>());
                        this.UpdateNamesList();
                    }
                    #endregion
                    #region Memos
                    if (this._DocumentInfo.Memos == null)
                    {
                        this._DocumentInfo.Memos = new MemoDetail[] { };
                        this.UpdateMemosList();
                    }
                    else
                    {
                        this.CurrentMemos = new List<MemoDetail>(this._DocumentInfo.Memos.ToList<MemoDetail>());
                        this.UpdateMemosList();
                    }
                    //this.btnAddMemo.Enabled = this._DocumentInfo.StatusCode != DocumentMediaStatusCode.Rejected && this._DocumentInfo.StatusCode != DocumentMediaStatusCode.Recorded;
                    //this.btnDeleteMemo.Enabled = this._DocumentInfo.StatusCode != DocumentMediaStatusCode.Rejected && this._DocumentInfo.StatusCode != DocumentMediaStatusCode.Recorded;
                    #endregion
                }
            }
        }
        
        #endregion

        #region Properties

        private bool _EnableTitles;
        public bool EnableTitles
        {
            get
            {
                return this._EnableTitles;
            }
            set
            {
                this._EnableTitles = value;
                this.btnEditTitles.Enabled = this._EnableEdit && this._EnableTitles;
            }
        }

        private bool _EnableNames;
        public bool EnableNames
        {
            get
            {
                return this._EnableNames;
            }
            set
            {
                this._EnableNames = value;
                this.btnAddNames.Enabled = this._EnableEdit && this._EnableNames;
                this.btnDeleteName.Enabled = this._EnableEdit && this._EnableNames && (this.NameCount > 0);
            }
        }

        private bool _EnableAssessorParcelNumber;
        public bool EnableAssessorParcelNumber
        {
            get
            {
                return this._EnableAssessorParcelNumber;
            }
            set
            {
                this._EnableAssessorParcelNumber = value;
                this.txtAssessorParcelNumber.ReadOnly = !(this._EnableEdit && this._EnableAssessorParcelNumber);
            }
        }

        private bool _EnableTransferTaxAmount;
        public bool EnableTransferTaxAmount
        {
            get
            {
                return this._EnableTransferTaxAmount;
            }
            set
            {
                this._EnableTransferTaxAmount = value;
                this.txtTransferTaxAmount.ReadOnly = !(this._EnableTransferTaxAmount && this._EnableEdit);
            }
        }

        private bool _EnableSaleAmount;
        public bool EnableSaleAmount
        {
            get
            {
                return this._EnableSaleAmount;
            }
            set
            {
                this._EnableSaleAmount = value;
                this.txtSaleAmount.ReadOnly = !(this._EnableEdit && this._EnableSaleAmount);
            }
        }

        private bool _EnableCities;
        public bool EnableCities
        {
            get
            {
                return this._EnableCities;
            }
            set
            {
                this._EnableCities = value;
                this.btnEditCities.Enabled = this._EnableEdit && this._EnableCities;
            }
        }

        private bool _EnableDocumentNumber;
        public bool EnableDocumentNumber
        {
            get
            {
                return this._EnableDocumentNumber;
            }
            set
            {
                this._EnableDocumentNumber = value;
                this.txtNumber.ReadOnly = !(this._EnableEdit && this._EnableDocumentNumber);
            }
        }

        private bool _EnableFees;
        public bool EnableFees
        {
            get
            {
                return this._EnableFees;
            }
            set
            {
                this._EnableFees = value;
                this.btnAddFees.Enabled = this._EnableFees && this._EnableEdit;
                this.btnDeleteFee.Enabled = this._EnableFees && this._EnableEdit && (this.FeeCount > 0);
            }
        }

        private bool _EnableRecordedTS;
        public bool EnableRecordedTS
        {
            get
            {
                return this._EnableRecordedTS;
            }
            set
            {
                this._EnableRecordedTS = value;
                this.dtpRecordedTS.Enabled = this._EnableRecordedTS && this._EnableEdit;
                this.dtpRecordedTS.Visible = this._DocumentInfo.RecordedTimeStampSpecified;
            }
        }

        private bool _EnableSequence;
        public bool EnableSequence
        {
            get
            {
                return this._EnableSequence;
            }
            set
            {
                this._EnableSequence = value;
                this.txtSequence.ReadOnly = !(this._EnableSequence && this._EnableEdit);
            }
        }
        private bool _EnableMemo;
        public bool EnableMemo
        {
            get
            {
                return this._EnableMemo;
            }
            set
            {
                this._EnableMemo = value;
                this.btnAddMemo.Enabled = this._EnableMemo && this._EnableEdit;
                this.btnDeleteMemo.Enabled = this._EnableMemo && this._EnableEdit && (this.MemoCount > 0);
            }
        }
        private bool _EnableEdit;
        public bool EnableEdit
        {
            get
            {
                return this._EnableEdit;
            }
            set
            {
                this._EnableEdit = value;
                this.EnableTitles = this._EnableTitles;
                this.EnableNames = this._EnableNames;
                this.EnableAssessorParcelNumber = this._EnableAssessorParcelNumber;
                this.EnableTransferTaxAmount = this._EnableTransferTaxAmount;
                this.EnableSaleAmount = this._EnableSaleAmount;
                this.EnableCities = this._EnableCities;
                this.EnableDocumentNumber = this._EnableDocumentNumber;
                this.EnableFees = this._EnableFees;
                this.EnableRecordedTS = this._EnableRecordedTS;
                this.EnableMemo = this._EnableMemo;
                this.EnableExternalID = this._EnableExternalID;
            }
        }

        private bool _EnableExternalID;
        public bool EnableExternalID
        {
            get
            {
                return this._EnableExternalID;
            }
            set
            {
                this._EnableExternalID = value;
                this.txtExternalID.Enabled = this._EnableExternalID && this._EnableEdit;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentInfoControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Functions

        #region Titles

        /// <summary>
        /// Update ListBox of Titles
        /// </summary>
        private void UpdateTitlesList()
        {
            this.lstTitles.DataSource = null;
            this.lstTitles.DataSource = CurrentTitles;
            this.lstTitles.DisplayMember = "Description";
        }

        #endregion

        #region Cities

        /// <summary>
        /// Update ListBox of Cities
        /// </summary>
        private void UpdateCitiesList()
        {
            this.lstCities.DataSource = null;
            this.lstCities.DataSource = CurrentCities;
            this.lstCities.DisplayMember = "Description";
        }

        #endregion

        #region Fees

        /// <summary>
        /// Update ListBox of Fees
        /// </summary>
        private void UpdateFeesList()
        {
            this.lstFees.DataSource = null;
            this.lstFees.DataSource = CurrentFees;
            this.lstFees.DisplayMember = "Description";
        }

        /// <summary>
        /// Add Fee
        /// </summary>
        private void AddFee()
        {
            FeeDetail newFee = new FeeDetail();
            newFee._ID = "-" + (new Random(DateTime.Now.Millisecond)).Next(0, 999).ToString();
            newFee.CreateTimeStamp = System.DateTime.Now;
            newFee.EditTimeStamp = System.DateTime.Now;
            newFee.ActionCode = ActionCode.New;

            DocumentInfoFeesForm newDocumentInfoFeesForm = new DocumentInfoFeesForm(newFee, DocumentInfoFeesForm.FormState.NewFee);
            if (newDocumentInfoFeesForm.ShowDialog(this) == DialogResult.OK)
            {
                this.CurrentFees.Add(newFee);
                this._DocumentInfo.Fees = this.CurrentFees.ToArray();
                this.UpdateFeesList();
            }
        }

        /// <summary>
        /// Open form to View selected Fee
        /// </summary>
        private void ViewSelectedFee()
        {
            FeeDetail selectedFee = (FeeDetail)this.lstFees.SelectedItem;
            DocumentInfoFeesForm newDocumentInfoFeesForm = new DocumentInfoFeesForm(selectedFee, DocumentInfoFeesForm.FormState.ViewFee);
            newDocumentInfoFeesForm.ShowDialog();
        }

        /// <summary>
        /// Delete selected Fee
        /// </summary>
        private void DeleteSelectedFee()
        {
            FeeDetail selectedFee = (FeeDetail)this.lstFees.SelectedItem;
            if (selectedFee.ActionCode == ActionCode.New)
            {
                this.CurrentFees.Remove(selectedFee);
                this._DocumentInfo.Fees = this.CurrentFees.ToArray();
            }
            else
            {
                selectedFee.ActionCode = ActionCode.Delete;
            }
            this.UpdateFeesList();
        }

        #endregion

        #region Names

        /// <summary>
        /// Update ListBox of Names
        /// </summary>
        private void UpdateNamesList()
        {
            this.lstNames.DataSource = null;
            this.lstNames.DataSource = this.CurrentNames;
            this.lstNames.DisplayMember = "Value";
        }

        /// <summary>
        /// Add name
        /// </summary>
        private void AddName()
        {
            NameDetail newName = new NameDetail();
            newName._ID = "-" + (new Random(DateTime.Now.Millisecond)).Next(0, 999).ToString();
            newName.CreateTimeStamp = System.DateTime.Now;
            newName.EditTimeStamp = System.DateTime.Now;
            newName.ActionCode = ActionCode.New;

            DocumentInfoNamesForm newDocumentInfoNamesForm = new DocumentInfoNamesForm(newName, DocumentInfoNamesForm.FormState.NewName);

            if (newDocumentInfoNamesForm.ShowDialog(this) == DialogResult.OK)
            {
                this.CurrentNames.Add(newName);
                this._DocumentInfo.Names = this.CurrentNames.ToArray();
                this.UpdateNamesList();
            }
        }

        /// <summary>
        /// Open form to view selected Name
        /// </summary>
        private void ViewSelectedName()
        {
            NameDetail selectedName = (NameDetail)this.lstNames.SelectedItem;
            DocumentInfoNamesForm newDocumentInfoNamesForm = new DocumentInfoNamesForm(selectedName, DocumentInfoNamesForm.FormState.ViewName);
            newDocumentInfoNamesForm.ShowDialog();
        }

        /// <summary>
        /// Delete selected Name
        /// </summary>
        private void DeleteSelectedName()
        {
            NameDetail selectedName = (NameDetail)this.lstNames.SelectedItem;
            if (selectedName.ActionCode == ActionCode.New)
            {
                this.CurrentNames.Remove(selectedName);
                this._DocumentInfo.Names = this.CurrentNames.ToArray();
            }
            else
            {
                selectedName.ActionCode = ActionCode.Delete;
            }
            this.UpdateNamesList();
        }

        #endregion

        #region Memos

        /// <summary>
        /// Update Memo ListBox
        /// </summary>
        private void UpdateMemosList()
        {
            this.lstMemos.DataSource = null;
            this.lstMemos.DataSource = CurrentMemos;
            this.lstMemos.DisplayMember = "EditTimeStamp";
        }

        /// <summary>
        /// Add Memo
        /// </summary>
        private void AddMemo()
        {
            MemoDetail newMemo = new MemoDetail();
            newMemo._ID = "-" + (new Random(DateTime.Now.Millisecond)).Next(1, 1000).ToString();
            newMemo.CreateTimeStamp = System.DateTime.Now;
            newMemo.EditTimeStamp = newMemo.CreateTimeStamp;
            newMemo.Type = MemoTypes.Note;
            newMemo.ActionCode = ActionCode.New;

            MemoDetailForm newMemoDetailForm = new MemoDetailForm(newMemo, MemoDetailForm.FormState.NewMemo);

            if (newMemoDetailForm.ShowDialog(this) == DialogResult.OK)
            {
                this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
                this.CurrentMemos.Add(newMemo);
                this._DocumentInfo.Memos = CurrentMemos.ToArray();
                this.UpdateMemosList();
            }
        }

        /// <summary>
        /// Open form to view selected memo
        /// </summary>
        private void ViewSelectedMemo()
        {
            MemoDetail selectedMemo = (MemoDetail)lstMemos.SelectedItem;
            MemoDetailForm newMemoDetailForm = new MemoDetailForm(selectedMemo, MemoDetailForm.FormState.ViewMemo);
            newMemoDetailForm.ShowDialog();
        }
        private void EditMemo()
        {
            MemoDetail selectedMemo = (MemoDetail)lstMemos.SelectedItem;
            MemoDetailForm newMemoDetailForm = new MemoDetailForm(selectedMemo, MemoDetailForm.FormState.EditMemo);
            newMemoDetailForm.ShowDialog();
        }
        /// <summary>
        /// Delete selected memo
        /// </summary>
        private void DeleteSelectedMemo()
        {
            MemoDetail selectedMemo = (MemoDetail)lstMemos.SelectedItem;
            if (selectedMemo.ActionCode == ActionCode.New)
            {
                this.CurrentMemos.Remove(selectedMemo);
                this._DocumentInfo.Memos = this.CurrentMemos.ToArray();
            }
            else
            {
                selectedMemo.ActionCode = ActionCode.Delete;
            }
            this.UpdateMemosList();
        }

        #endregion

        public override void Refresh()
        {
            base.Refresh();
            // Refresh all controls with data from DocumentInfo
            this.DocumentInfo = this._DocumentInfo;

            this.EnableMemo = this.EnableMemo;
            this.EnableNames = this.EnableNames;
            this.EnableFees = this.EnableFees;
        }

        #endregion

        #region Events

        #region Titles

        /// <summary>
        /// Click event for Edit Titles button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditTitles_Click(object sender, EventArgs e)
        {
            DocumentInfoTitlesForm newDocumentInfoTitlesForm = new DocumentInfoTitlesForm(AllTitles, CurrentTitles);
            newDocumentInfoTitlesForm.ShowDialog(this);
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            this._DocumentInfo.Titles = newDocumentInfoTitlesForm.CurrentTitles.ToArray();
            this.UpdateTitlesList();
            this.Refresh();
        }

        #endregion

        #region Cities
        
        /// <summary>
        /// Click event for Edit Cities button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditCities_Click(object sender, EventArgs e)
        {
            DocumentInfoCitiesForm newDocumentInfoCitiesForm = new DocumentInfoCitiesForm(AllCities, CurrentCities);
            newDocumentInfoCitiesForm.ShowDialog(this);
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            this._DocumentInfo.Cities = newDocumentInfoCitiesForm.CurrentCities.ToArray();
            this.UpdateCitiesList();
            this.Refresh();
        }

        #endregion

        #region Fees

        /// <summary>
        /// Double Click event for Fees ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFees_DoubleClick(object sender, EventArgs e)
        {
            if (this.lstFees.SelectedItem == null)
            {
                if (this._EnableFees)
                {
                    this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
                    this.AddFee();
                }
            }
            else
            {
                this.ViewSelectedFee();
            }
            this.Refresh();
        }

        /// <summary>
        /// Click event for Add Fee Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddFees_Click(object sender, EventArgs e)
        {
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            this.AddFee();
            this.Refresh();
        }

        /// <summary>
        /// Click event for Delete Fee Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteFee_Click(object sender, EventArgs e)
        {
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            this.DeleteSelectedFee();
            this.Refresh();
        }

        #endregion

        #region Names

        /// <summary>
        /// Double Click event for Names ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstNames_DoubleClick(object sender, EventArgs e)
        {
            if (this.lstNames.SelectedItem == null)
            {
                this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
                this.AddName();
            }
            else
            {
                this.ViewSelectedName();
            }
            this.Refresh();
        }

        /// <summary>
        /// Click event for Add Name button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNames_Click(object sender, EventArgs e)
        {
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            this.AddName();
            this.Refresh();
        }

        /// <summary>
        ///  Click event for Delete Name button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteName_Click(object sender, EventArgs e)
        {
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            this.DeleteSelectedName();
            this.Refresh();
        }

        #endregion

        #region Memos

        /// <summary>
        /// Double Click event for Memo ListBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstMemos_DoubleClick(object sender, EventArgs e)
        {
            if (this._EnableEdit)
            {
                if (this.lstMemos.SelectedItem == null)
                {
                    this.AddMemo();
                }
                else
                {
                    this.EditMemo();
                }
            }
            else
            {
                this.ViewSelectedMemo();
            }
        }
       
        /// <summary>
        /// Click event for Add Memo button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMemo_Click(object sender, EventArgs e)
        {
            this.AddMemo();
            this.Refresh();
        }
       
        /// <summary>
        /// Click event for Delete Memo button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteMemo_Click(object sender, EventArgs e)
        {
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            this.DeleteSelectedMemo();
            this.Refresh();
        }

        #endregion

        /// <summary>
        /// Text Changed event for assessor Parcel Number TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAssessorParcelNumber_TextChanged(object sender, EventArgs e)
        {
            this._DocumentInfo.AssessorParcelNumber = txtAssessorParcelNumber.Text;
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            this.Refresh();
        }

        /// <summary>
        /// TextChanged event for Transfer Tax TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTransferTaxAmount_TextChanged(object sender, EventArgs e)
        {
            //make sure entry is a decimal
            decimal newAmount;
            bool formatOK = decimal.TryParse(this.txtTransferTaxAmount.Text.ToString(), out newAmount);
            if (formatOK == true)
            {
                this._DocumentInfo.TransferTaxAmount = newAmount;
                this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
                this._DocumentInfo.TransferTaxAmountSpecified = true;
            }
            else
            {
                txtSaleAmount.Text = string.Empty;
            }
            //this.Refresh();
        }

        /// <summary>
        /// TextChanged event for Sale amount TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSaleAmount_TextChanged(object sender, EventArgs e)
        {
            //make sure entry is a decimal
            decimal newAmount;
            bool formatOK = decimal.TryParse(txtSaleAmount.Text.ToString(), out newAmount);
            if (formatOK == true)
            {
                this._DocumentInfo.SaleAmount = newAmount;
                this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
                this._DocumentInfo.SaleAmountSpecified = true;
            }
            else
            {
                txtSaleAmount.Text = string.Empty;
            }
            //this.Refresh();
        }

        /// <summary>
        /// TextChanged event for sequence TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSequence_TextChanged(object sender, EventArgs e)
        {
            int sequenceNumber;
            if (int.TryParse(this.txtSequence.Text, out sequenceNumber))
            {
                this._DocumentInfo.Sequence = sequenceNumber;
            }
            this.Refresh();
        }

        /// <summary>
        /// KeyPress event for Sequence TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSequence_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                if (!char.IsNumber(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// KeyPress event for Sale Amount TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSaleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                if (!(char.IsNumber(e.KeyChar) || (e.KeyChar == '.')))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// KeyPress event for TransferTax TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTransferTaxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                if (!(char.IsNumber(e.KeyChar) || (e.KeyChar == '.')))
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Leave event for Transfer Tax TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTransferTaxAmount_Leave(object sender, EventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// Leave event for Sale Amount TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSaleAmount_Leave(object sender, EventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// TextChanged event for Number TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            if (this.txtNumber.Text.Trim() != string.Empty)
            {
                this._DocumentInfo.Number = long.Parse(this.txtNumber.Text);
                this._DocumentInfo.NumberSpecified = true;
                this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            }
        }

        /// <summary>
        /// ValueChanged event for Recorded Time Stamp DateTimePicker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpRecordedTS_ValueChanged(object sender, EventArgs e)
        {
            this._DocumentInfo.RecordedTimeStamp = this.dtpRecordedTS.Value;
            this._DocumentInfo.RecordedTimeStampSpecified = true;
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
        }

        /// <summary>
        /// KeyPress event for Number TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                if (!char.IsNumber(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        #endregion

        private void txtExternalID_TextChanged(object sender, EventArgs e)
        {
            this._DocumentInfo._ExternalID = this.txtExternalID.Text;
            this._DocumentInfo.ActionCode = this._DocumentInfo.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.Edit;
            this.Refresh();
        }
    }
}

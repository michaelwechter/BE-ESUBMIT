namespace WinFormTest
{
    partial class SubmitterPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubmitterPanel));
            this.btnCache_LoadCache = new System.Windows.Forms.Button();
            this.btnAuth_Login = new System.Windows.Forms.Button();
            this.btnOP_ViewBatch = new System.Windows.Forms.Button();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.lblSessionTokenID = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCache_ViewCache = new System.Windows.Forms.Button();
            this.btnAuth_Logout = new System.Windows.Forms.Button();
            this.grpAPIButtons = new System.Windows.Forms.GroupBox();
            this.btnAPI_BatchWithDocuments_ReadyByID = new System.Windows.Forms.Button();
            this.btnAPI_BatchWithDocuments_Upload = new System.Windows.Forms.Button();
            this.btnAPI_BatchDownload = new System.Windows.Forms.Button();
            this.btnAPI_BatchDownloadComplete = new System.Windows.Forms.Button();
            this.btnAPI_DocumentDownload = new System.Windows.Forms.Button();
            this.btnAPI_BatchCreate = new System.Windows.Forms.Button();
            this.btnAPI_BatchUpload = new System.Windows.Forms.Button();
            this.btnAPI_BatchUploadComplete = new System.Windows.Forms.Button();
            this.btnAPI_DocumentUpload = new System.Windows.Forms.Button();
            this.btnOP_Clear = new System.Windows.Forms.Button();
            this.cboViewBatch_Status = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkBatchWithDocumentsDownload = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboCounty = new System.Windows.Forms.ComboBox();
            this.grpSearchOptions = new System.Windows.Forms.GroupBox();
            this.chkSearch_Status = new System.Windows.Forms.CheckBox();
            this.chkSearch_UserName = new System.Windows.Forms.CheckBox();
            this.chkSearch_County = new System.Windows.Forms.CheckBox();
            this.chkSearch_BatchName = new System.Windows.Forms.CheckBox();
            this.chkSearch_SubmissionID = new System.Windows.Forms.CheckBox();
            this.chkSearch_BatchID = new System.Windows.Forms.CheckBox();
            this.lblBatchSearchInpu = new System.Windows.Forms.Label();
            this.txtViewBatchSearch_input = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpBatchSearch_end = new System.Windows.Forms.DateTimePicker();
            this.dtpBatchSearch_start = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.grpAuthorization = new System.Windows.Forms.GroupBox();
            this.btnAuth_Ping = new System.Windows.Forms.Button();
            this.btnAuth_ChangePassword = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkBatchDownload_CheckOut = new System.Windows.Forms.CheckBox();
            this.chkBatchDownload_SetComplete = new System.Windows.Forms.CheckBox();
            this.chkBatchDownload_IncludeCheckedOut = new System.Windows.Forms.CheckBox();
            this.btnOP_DownloadBatches = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboDownloadBatch_Status = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOP_ValidateBatches = new System.Windows.Forms.Button();
            this.btnOP_MyBatches = new System.Windows.Forms.Button();
            this.btnOP_SaveBatch = new System.Windows.Forms.Button();
            this.btnOP_SubmitBatch = new System.Windows.Forms.Button();
            this.btnOP_SaveBatchUpload = new System.Windows.Forms.Button();
            this.btnOP_CreateBatch = new System.Windows.Forms.Button();
            this.trvBatches = new System.Windows.Forms.TreeView();
            this.cmsTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkOutBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkInBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overrideCheckOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRecordableFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPCORToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addOtherFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imglistTree = new System.Windows.Forms.ImageList(this.components);
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.batchDetailControl = new WinFormTest.ClientControls.BatchDetailControl();
            this.documentInfoControl = new WinFormTest.ClientControls.DocumentInfoControl();
            this.flowFileContent = new System.Windows.Forms.FlowLayoutPanel();
            this.fileContentInfoControl_RECORDABLE = new WinFormTest.ClientControls.FileContentInfoControl();
            this.fileContentInfoControl_PCOR = new WinFormTest.ClientControls.FileContentInfoControl();
            this.fileContentInfoControl_OTHER = new WinFormTest.ClientControls.FileContentInfoControl();
            this.chkAPI = new System.Windows.Forms.CheckBox();
            this.grpURL = new System.Windows.Forms.GroupBox();
            this.btnURLEdit = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chk_XML_Validation = new System.Windows.Forms.CheckBox();
            this.grpCurrentUser = new System.Windows.Forms.GroupBox();
            this.txtCurrentUser_Name = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCurrentUser_PartyType = new System.Windows.Forms.TextBox();
            this.txtCurrentUser_SubmittingParty = new System.Windows.Forms.TextBox();
            this.txtCurrentUser_UserType = new System.Windows.Forms.TextBox();
            this.txtCurrentUser_UserName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkToken = new System.Windows.Forms.CheckBox();
            this.ssStatus.SuspendLayout();
            this.grpAPIButtons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpSearchOptions.SuspendLayout();
            this.grpAuthorization.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cmsTree.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.flowFileContent.SuspendLayout();
            this.grpURL.SuspendLayout();
            this.grpCurrentUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCache_LoadCache
            // 
            this.btnCache_LoadCache.Location = new System.Drawing.Point(6, 19);
            this.btnCache_LoadCache.Name = "btnCache_LoadCache";
            this.btnCache_LoadCache.Size = new System.Drawing.Size(150, 25);
            this.btnCache_LoadCache.TabIndex = 0;
            this.btnCache_LoadCache.Text = "Reload Cache";
            this.btnCache_LoadCache.UseVisualStyleBackColor = true;
            this.btnCache_LoadCache.Click += new System.EventHandler(this.btnCache_LoadCache_Click);
            // 
            // btnAuth_Login
            // 
            this.btnAuth_Login.Location = new System.Drawing.Point(6, 19);
            this.btnAuth_Login.Name = "btnAuth_Login";
            this.btnAuth_Login.Size = new System.Drawing.Size(150, 25);
            this.btnAuth_Login.TabIndex = 1;
            this.btnAuth_Login.Text = "Login";
            this.btnAuth_Login.UseVisualStyleBackColor = true;
            this.btnAuth_Login.Click += new System.EventHandler(this.btnAuth_Login_Click);
            // 
            // btnOP_ViewBatch
            // 
            this.btnOP_ViewBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOP_ViewBatch.Location = new System.Drawing.Point(6, 274);
            this.btnOP_ViewBatch.Name = "btnOP_ViewBatch";
            this.btnOP_ViewBatch.Size = new System.Drawing.Size(306, 23);
            this.btnOP_ViewBatch.TabIndex = 4;
            this.btnOP_ViewBatch.Text = "Search";
            this.btnOP_ViewBatch.UseVisualStyleBackColor = true;
            this.btnOP_ViewBatch.Click += new System.EventHandler(this.btnOP_ViewBatch_Click);
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSessionTokenID});
            this.ssStatus.Location = new System.Drawing.Point(0, 1246);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(1814, 22);
            this.ssStatus.TabIndex = 5;
            this.ssStatus.Text = "statusStrip1";
            // 
            // lblSessionTokenID
            // 
            this.lblSessionTokenID.Name = "lblSessionTokenID";
            this.lblSessionTokenID.Size = new System.Drawing.Size(0, 17);
            // 
            // btnCache_ViewCache
            // 
            this.btnCache_ViewCache.Location = new System.Drawing.Point(159, 19);
            this.btnCache_ViewCache.Name = "btnCache_ViewCache";
            this.btnCache_ViewCache.Size = new System.Drawing.Size(150, 25);
            this.btnCache_ViewCache.TabIndex = 6;
            this.btnCache_ViewCache.Text = "View Cache";
            this.btnCache_ViewCache.UseVisualStyleBackColor = true;
            this.btnCache_ViewCache.Click += new System.EventHandler(this.btnCache_ViewCache_Click);
            // 
            // btnAuth_Logout
            // 
            this.btnAuth_Logout.Location = new System.Drawing.Point(6, 48);
            this.btnAuth_Logout.Name = "btnAuth_Logout";
            this.btnAuth_Logout.Size = new System.Drawing.Size(150, 25);
            this.btnAuth_Logout.TabIndex = 7;
            this.btnAuth_Logout.Text = "Logout";
            this.btnAuth_Logout.UseVisualStyleBackColor = true;
            this.btnAuth_Logout.Click += new System.EventHandler(this.btnAuth_Logout_Click);
            // 
            // grpAPIButtons
            // 
            this.grpAPIButtons.Controls.Add(this.btnAPI_BatchWithDocuments_ReadyByID);
            this.grpAPIButtons.Controls.Add(this.btnAPI_BatchWithDocuments_Upload);
            this.grpAPIButtons.Controls.Add(this.btnAPI_BatchDownload);
            this.grpAPIButtons.Controls.Add(this.btnAPI_BatchDownloadComplete);
            this.grpAPIButtons.Controls.Add(this.btnAPI_DocumentDownload);
            this.grpAPIButtons.Controls.Add(this.btnAPI_BatchCreate);
            this.grpAPIButtons.Controls.Add(this.btnAPI_BatchUpload);
            this.grpAPIButtons.Controls.Add(this.btnAPI_BatchUploadComplete);
            this.grpAPIButtons.Controls.Add(this.btnAPI_DocumentUpload);
            this.grpAPIButtons.Location = new System.Drawing.Point(0, 925);
            this.grpAPIButtons.Name = "grpAPIButtons";
            this.grpAPIButtons.Size = new System.Drawing.Size(323, 298);
            this.grpAPIButtons.TabIndex = 9;
            this.grpAPIButtons.TabStop = false;
            this.grpAPIButtons.Text = "SECURE API";
            this.grpAPIButtons.Visible = false;
            // 
            // btnAPI_BatchWithDocuments_ReadyByID
            // 
            this.btnAPI_BatchWithDocuments_ReadyByID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAPI_BatchWithDocuments_ReadyByID.Location = new System.Drawing.Point(6, 263);
            this.btnAPI_BatchWithDocuments_ReadyByID.Name = "btnAPI_BatchWithDocuments_ReadyByID";
            this.btnAPI_BatchWithDocuments_ReadyByID.Size = new System.Drawing.Size(306, 25);
            this.btnAPI_BatchWithDocuments_ReadyByID.TabIndex = 11;
            this.btnAPI_BatchWithDocuments_ReadyByID.Text = "/api/BatchWithDocuments/ReadByID";
            this.btnAPI_BatchWithDocuments_ReadyByID.UseVisualStyleBackColor = true;
            this.btnAPI_BatchWithDocuments_ReadyByID.Click += new System.EventHandler(this.btnAPI_BatchWithDocuments_ReadyByID_Click);
            // 
            // btnAPI_BatchWithDocuments_Upload
            // 
            this.btnAPI_BatchWithDocuments_Upload.Location = new System.Drawing.Point(6, 232);
            this.btnAPI_BatchWithDocuments_Upload.Name = "btnAPI_BatchWithDocuments_Upload";
            this.btnAPI_BatchWithDocuments_Upload.Size = new System.Drawing.Size(306, 25);
            this.btnAPI_BatchWithDocuments_Upload.TabIndex = 10;
            this.btnAPI_BatchWithDocuments_Upload.Text = "/api/BatchWithDocuments/Upload";
            this.btnAPI_BatchWithDocuments_Upload.UseVisualStyleBackColor = true;
            this.btnAPI_BatchWithDocuments_Upload.Click += new System.EventHandler(this.btnAPI_BatchWithDocuments_Upload_Click);
            // 
            // btnAPI_BatchDownload
            // 
            this.btnAPI_BatchDownload.Location = new System.Drawing.Point(6, 139);
            this.btnAPI_BatchDownload.Name = "btnAPI_BatchDownload";
            this.btnAPI_BatchDownload.Size = new System.Drawing.Size(306, 25);
            this.btnAPI_BatchDownload.TabIndex = 9;
            this.btnAPI_BatchDownload.Text = "/api/Batch/Download";
            this.btnAPI_BatchDownload.UseVisualStyleBackColor = true;
            this.btnAPI_BatchDownload.Click += new System.EventHandler(this.btnAPI_BatchDownload_Click);
            // 
            // btnAPI_BatchDownloadComplete
            // 
            this.btnAPI_BatchDownloadComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAPI_BatchDownloadComplete.Location = new System.Drawing.Point(6, 201);
            this.btnAPI_BatchDownloadComplete.Name = "btnAPI_BatchDownloadComplete";
            this.btnAPI_BatchDownloadComplete.Size = new System.Drawing.Size(306, 25);
            this.btnAPI_BatchDownloadComplete.TabIndex = 8;
            this.btnAPI_BatchDownloadComplete.Text = "/api/Batch/DownloadComplete";
            this.btnAPI_BatchDownloadComplete.UseVisualStyleBackColor = true;
            this.btnAPI_BatchDownloadComplete.Click += new System.EventHandler(this.btnAPI_BatchDownloadComplete_Click);
            // 
            // btnAPI_DocumentDownload
            // 
            this.btnAPI_DocumentDownload.Location = new System.Drawing.Point(6, 170);
            this.btnAPI_DocumentDownload.Name = "btnAPI_DocumentDownload";
            this.btnAPI_DocumentDownload.Size = new System.Drawing.Size(306, 25);
            this.btnAPI_DocumentDownload.TabIndex = 7;
            this.btnAPI_DocumentDownload.Text = "/api/Document/Download";
            this.btnAPI_DocumentDownload.UseVisualStyleBackColor = true;
            this.btnAPI_DocumentDownload.Click += new System.EventHandler(this.btnAPI_DocumentDownload_Click);
            // 
            // btnAPI_BatchCreate
            // 
            this.btnAPI_BatchCreate.Location = new System.Drawing.Point(6, 15);
            this.btnAPI_BatchCreate.Name = "btnAPI_BatchCreate";
            this.btnAPI_BatchCreate.Size = new System.Drawing.Size(306, 25);
            this.btnAPI_BatchCreate.TabIndex = 5;
            this.btnAPI_BatchCreate.Text = "/api/Batch/Create";
            this.btnAPI_BatchCreate.UseVisualStyleBackColor = true;
            this.btnAPI_BatchCreate.Click += new System.EventHandler(this.btnAPI_BatchCreate_Click);
            // 
            // btnAPI_BatchUpload
            // 
            this.btnAPI_BatchUpload.Location = new System.Drawing.Point(6, 46);
            this.btnAPI_BatchUpload.Name = "btnAPI_BatchUpload";
            this.btnAPI_BatchUpload.Size = new System.Drawing.Size(306, 25);
            this.btnAPI_BatchUpload.TabIndex = 4;
            this.btnAPI_BatchUpload.Text = "/api/Batch/Upload";
            this.btnAPI_BatchUpload.UseVisualStyleBackColor = true;
            this.btnAPI_BatchUpload.Click += new System.EventHandler(this.btnAPI_BatchUpload_Click);
            // 
            // btnAPI_BatchUploadComplete
            // 
            this.btnAPI_BatchUploadComplete.Location = new System.Drawing.Point(6, 108);
            this.btnAPI_BatchUploadComplete.Name = "btnAPI_BatchUploadComplete";
            this.btnAPI_BatchUploadComplete.Size = new System.Drawing.Size(306, 25);
            this.btnAPI_BatchUploadComplete.TabIndex = 3;
            this.btnAPI_BatchUploadComplete.Text = "/api/Batch/UploadComplete";
            this.btnAPI_BatchUploadComplete.UseVisualStyleBackColor = true;
            this.btnAPI_BatchUploadComplete.Click += new System.EventHandler(this.btnAPI_BatchUploadComplete_Click);
            // 
            // btnAPI_DocumentUpload
            // 
            this.btnAPI_DocumentUpload.Location = new System.Drawing.Point(6, 77);
            this.btnAPI_DocumentUpload.Name = "btnAPI_DocumentUpload";
            this.btnAPI_DocumentUpload.Size = new System.Drawing.Size(306, 25);
            this.btnAPI_DocumentUpload.TabIndex = 2;
            this.btnAPI_DocumentUpload.Text = "/api/Document/Upload";
            this.btnAPI_DocumentUpload.UseVisualStyleBackColor = true;
            this.btnAPI_DocumentUpload.Click += new System.EventHandler(this.btnAPI_DocumentUpload_Click);
            // 
            // btnOP_Clear
            // 
            this.btnOP_Clear.Location = new System.Drawing.Point(159, 20);
            this.btnOP_Clear.Name = "btnOP_Clear";
            this.btnOP_Clear.Size = new System.Drawing.Size(153, 25);
            this.btnOP_Clear.TabIndex = 6;
            this.btnOP_Clear.Text = "Clear";
            this.btnOP_Clear.UseVisualStyleBackColor = true;
            this.btnOP_Clear.Click += new System.EventHandler(this.btnOP_Clear_Click);
            // 
            // cboViewBatch_Status
            // 
            this.cboViewBatch_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboViewBatch_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboViewBatch_Status.DropDownWidth = 250;
            this.cboViewBatch_Status.FormattingEnabled = true;
            this.cboViewBatch_Status.Location = new System.Drawing.Point(108, 172);
            this.cboViewBatch_Status.MaxDropDownItems = 50;
            this.cboViewBatch_Status.Name = "cboViewBatch_Status";
            this.cboViewBatch_Status.Size = new System.Drawing.Size(204, 21);
            this.cboViewBatch_Status.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkBatchWithDocumentsDownload);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboCounty);
            this.groupBox1.Controls.Add(this.grpSearchOptions);
            this.groupBox1.Controls.Add(this.lblBatchSearchInpu);
            this.groupBox1.Controls.Add(this.txtViewBatchSearch_input);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpBatchSearch_end);
            this.groupBox1.Controls.Add(this.dtpBatchSearch_start);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnOP_ViewBatch);
            this.groupBox1.Controls.Add(this.cboViewBatch_Status);
            this.groupBox1.Location = new System.Drawing.Point(0, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(323, 303);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View Batch";
            // 
            // chkBatchWithDocumentsDownload
            // 
            this.chkBatchWithDocumentsDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBatchWithDocumentsDownload.AutoSize = true;
            this.chkBatchWithDocumentsDownload.Location = new System.Drawing.Point(108, 251);
            this.chkBatchWithDocumentsDownload.Name = "chkBatchWithDocumentsDownload";
            this.chkBatchWithDocumentsDownload.Size = new System.Drawing.Size(163, 17);
            this.chkBatchWithDocumentsDownload.TabIndex = 25;
            this.chkBatchWithDocumentsDownload.Text = "Download Document Images";
            this.chkBatchWithDocumentsDownload.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(6, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 21);
            this.label5.TabIndex = 20;
            this.label5.Text = "County:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCounty
            // 
            this.cboCounty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboCounty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCounty.DropDownWidth = 250;
            this.cboCounty.FormattingEnabled = true;
            this.cboCounty.Location = new System.Drawing.Point(108, 145);
            this.cboCounty.MaxDropDownItems = 50;
            this.cboCounty.Name = "cboCounty";
            this.cboCounty.Size = new System.Drawing.Size(204, 21);
            this.cboCounty.TabIndex = 19;
            // 
            // grpSearchOptions
            // 
            this.grpSearchOptions.Controls.Add(this.chkSearch_Status);
            this.grpSearchOptions.Controls.Add(this.chkSearch_UserName);
            this.grpSearchOptions.Controls.Add(this.chkSearch_County);
            this.grpSearchOptions.Controls.Add(this.chkSearch_BatchName);
            this.grpSearchOptions.Controls.Add(this.chkSearch_SubmissionID);
            this.grpSearchOptions.Controls.Add(this.chkSearch_BatchID);
            this.grpSearchOptions.Location = new System.Drawing.Point(10, 19);
            this.grpSearchOptions.Name = "grpSearchOptions";
            this.grpSearchOptions.Size = new System.Drawing.Size(299, 92);
            this.grpSearchOptions.TabIndex = 18;
            this.grpSearchOptions.TabStop = false;
            this.grpSearchOptions.Text = "Search Options";
            // 
            // chkSearch_Status
            // 
            this.chkSearch_Status.AutoSize = true;
            this.chkSearch_Status.Location = new System.Drawing.Point(149, 20);
            this.chkSearch_Status.Name = "chkSearch_Status";
            this.chkSearch_Status.Size = new System.Drawing.Size(87, 17);
            this.chkSearch_Status.TabIndex = 5;
            this.chkSearch_Status.Text = "Batch Status";
            this.chkSearch_Status.UseVisualStyleBackColor = true;
            this.chkSearch_Status.CheckedChanged += new System.EventHandler(this.chkSearch_Status_CheckedChanged);
            // 
            // chkSearch_UserName
            // 
            this.chkSearch_UserName.AutoSize = true;
            this.chkSearch_UserName.Location = new System.Drawing.Point(149, 66);
            this.chkSearch_UserName.Name = "chkSearch_UserName";
            this.chkSearch_UserName.Size = new System.Drawing.Size(79, 17);
            this.chkSearch_UserName.TabIndex = 4;
            this.chkSearch_UserName.Text = "User Name";
            this.chkSearch_UserName.UseVisualStyleBackColor = true;
            this.chkSearch_UserName.CheckedChanged += new System.EventHandler(this.chkSearch_UserName_CheckedChanged);
            // 
            // chkSearch_County
            // 
            this.chkSearch_County.AutoSize = true;
            this.chkSearch_County.Location = new System.Drawing.Point(149, 43);
            this.chkSearch_County.Name = "chkSearch_County";
            this.chkSearch_County.Size = new System.Drawing.Size(59, 17);
            this.chkSearch_County.TabIndex = 3;
            this.chkSearch_County.Text = "County";
            this.chkSearch_County.UseVisualStyleBackColor = true;
            this.chkSearch_County.CheckedChanged += new System.EventHandler(this.chkSearch_County_CheckedChanged);
            // 
            // chkSearch_BatchName
            // 
            this.chkSearch_BatchName.AutoSize = true;
            this.chkSearch_BatchName.Location = new System.Drawing.Point(6, 43);
            this.chkSearch_BatchName.Name = "chkSearch_BatchName";
            this.chkSearch_BatchName.Size = new System.Drawing.Size(85, 17);
            this.chkSearch_BatchName.TabIndex = 2;
            this.chkSearch_BatchName.Text = "Batch Name";
            this.chkSearch_BatchName.UseVisualStyleBackColor = true;
            this.chkSearch_BatchName.CheckedChanged += new System.EventHandler(this.chkSearch_BatchName_CheckedChanged);
            // 
            // chkSearch_SubmissionID
            // 
            this.chkSearch_SubmissionID.AutoSize = true;
            this.chkSearch_SubmissionID.Location = new System.Drawing.Point(6, 66);
            this.chkSearch_SubmissionID.Name = "chkSearch_SubmissionID";
            this.chkSearch_SubmissionID.Size = new System.Drawing.Size(124, 17);
            this.chkSearch_SubmissionID.TabIndex = 1;
            this.chkSearch_SubmissionID.Text = "Batch Submission ID";
            this.chkSearch_SubmissionID.UseVisualStyleBackColor = true;
            this.chkSearch_SubmissionID.CheckedChanged += new System.EventHandler(this.chkSearch_SubmissionID_CheckedChanged);
            // 
            // chkSearch_BatchID
            // 
            this.chkSearch_BatchID.AutoSize = true;
            this.chkSearch_BatchID.Location = new System.Drawing.Point(6, 20);
            this.chkSearch_BatchID.Name = "chkSearch_BatchID";
            this.chkSearch_BatchID.Size = new System.Drawing.Size(68, 17);
            this.chkSearch_BatchID.TabIndex = 0;
            this.chkSearch_BatchID.Text = "Batch ID";
            this.chkSearch_BatchID.UseVisualStyleBackColor = true;
            this.chkSearch_BatchID.CheckedChanged += new System.EventHandler(this.chkSearch_BatchID_CheckedChanged);
            // 
            // lblBatchSearchInpu
            // 
            this.lblBatchSearchInpu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBatchSearchInpu.Location = new System.Drawing.Point(6, 119);
            this.lblBatchSearchInpu.Name = "lblBatchSearchInpu";
            this.lblBatchSearchInpu.Size = new System.Drawing.Size(96, 20);
            this.lblBatchSearchInpu.TabIndex = 17;
            this.lblBatchSearchInpu.Text = "BatchID:";
            this.lblBatchSearchInpu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtViewBatchSearch_input
            // 
            this.txtViewBatchSearch_input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtViewBatchSearch_input.Location = new System.Drawing.Point(108, 119);
            this.txtViewBatchSearch_input.Name = "txtViewBatchSearch_input";
            this.txtViewBatchSearch_input.Size = new System.Drawing.Size(204, 20);
            this.txtViewBatchSearch_input.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(6, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Date Range End:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(6, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Date Range Start:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpBatchSearch_end
            // 
            this.dtpBatchSearch_end.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpBatchSearch_end.Location = new System.Drawing.Point(108, 225);
            this.dtpBatchSearch_end.Name = "dtpBatchSearch_end";
            this.dtpBatchSearch_end.Size = new System.Drawing.Size(204, 20);
            this.dtpBatchSearch_end.TabIndex = 13;
            // 
            // dtpBatchSearch_start
            // 
            this.dtpBatchSearch_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpBatchSearch_start.Location = new System.Drawing.Point(108, 199);
            this.dtpBatchSearch_start.Name = "dtpBatchSearch_start";
            this.dtpBatchSearch_start.Size = new System.Drawing.Size(204, 20);
            this.dtpBatchSearch_start.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(6, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "Batch Status:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpAuthorization
            // 
            this.grpAuthorization.Controls.Add(this.btnAuth_Ping);
            this.grpAuthorization.Controls.Add(this.btnAuth_ChangePassword);
            this.grpAuthorization.Controls.Add(this.btnAuth_Login);
            this.grpAuthorization.Controls.Add(this.btnAuth_Logout);
            this.grpAuthorization.Location = new System.Drawing.Point(0, 76);
            this.grpAuthorization.Name = "grpAuthorization";
            this.grpAuthorization.Size = new System.Drawing.Size(323, 86);
            this.grpAuthorization.TabIndex = 12;
            this.grpAuthorization.TabStop = false;
            this.grpAuthorization.Text = "Authentication";
            // 
            // btnAuth_Ping
            // 
            this.btnAuth_Ping.Location = new System.Drawing.Point(159, 48);
            this.btnAuth_Ping.Name = "btnAuth_Ping";
            this.btnAuth_Ping.Size = new System.Drawing.Size(150, 25);
            this.btnAuth_Ping.TabIndex = 8;
            this.btnAuth_Ping.Text = "Ping";
            this.btnAuth_Ping.UseVisualStyleBackColor = true;
            this.btnAuth_Ping.Click += new System.EventHandler(this.btnAuth_Ping_Click);
            // 
            // btnAuth_ChangePassword
            // 
            this.btnAuth_ChangePassword.Location = new System.Drawing.Point(159, 19);
            this.btnAuth_ChangePassword.Name = "btnAuth_ChangePassword";
            this.btnAuth_ChangePassword.Size = new System.Drawing.Size(150, 25);
            this.btnAuth_ChangePassword.TabIndex = 9;
            this.btnAuth_ChangePassword.Text = "Change Password";
            this.btnAuth_ChangePassword.UseVisualStyleBackColor = true;
            this.btnAuth_ChangePassword.Click += new System.EventHandler(this.btnAuth_ChangePassword_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCache_LoadCache);
            this.groupBox3.Controls.Add(this.btnCache_ViewCache);
            this.groupBox3.Location = new System.Drawing.Point(0, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(323, 55);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cache Data";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkBatchDownload_CheckOut);
            this.groupBox6.Controls.Add(this.chkBatchDownload_SetComplete);
            this.groupBox6.Controls.Add(this.chkBatchDownload_IncludeCheckedOut);
            this.groupBox6.Controls.Add(this.btnOP_DownloadBatches);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.cboDownloadBatch_Status);
            this.groupBox6.Location = new System.Drawing.Point(0, 538);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(323, 144);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Download Batch";
            // 
            // chkBatchDownload_CheckOut
            // 
            this.chkBatchDownload_CheckOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBatchDownload_CheckOut.AutoSize = true;
            this.chkBatchDownload_CheckOut.Checked = true;
            this.chkBatchDownload_CheckOut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBatchDownload_CheckOut.Location = new System.Drawing.Point(84, 70);
            this.chkBatchDownload_CheckOut.Name = "chkBatchDownload_CheckOut";
            this.chkBatchDownload_CheckOut.Size = new System.Drawing.Size(152, 17);
            this.chkBatchDownload_CheckOut.TabIndex = 17;
            this.chkBatchDownload_CheckOut.Text = "Check Out after Download";
            this.chkBatchDownload_CheckOut.UseVisualStyleBackColor = true;
            this.chkBatchDownload_CheckOut.CheckedChanged += new System.EventHandler(this.chkBatchDownload_CheckOut_CheckedChanged);
            // 
            // chkBatchDownload_SetComplete
            // 
            this.chkBatchDownload_SetComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBatchDownload_SetComplete.AutoSize = true;
            this.chkBatchDownload_SetComplete.Checked = true;
            this.chkBatchDownload_SetComplete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBatchDownload_SetComplete.Location = new System.Drawing.Point(84, 93);
            this.chkBatchDownload_SetComplete.Name = "chkBatchDownload_SetComplete";
            this.chkBatchDownload_SetComplete.Size = new System.Drawing.Size(140, 17);
            this.chkBatchDownload_SetComplete.TabIndex = 16;
            this.chkBatchDownload_SetComplete.Text = "Set Download Complete";
            this.chkBatchDownload_SetComplete.UseVisualStyleBackColor = true;
            // 
            // chkBatchDownload_IncludeCheckedOut
            // 
            this.chkBatchDownload_IncludeCheckedOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBatchDownload_IncludeCheckedOut.AutoSize = true;
            this.chkBatchDownload_IncludeCheckedOut.Location = new System.Drawing.Point(84, 47);
            this.chkBatchDownload_IncludeCheckedOut.Name = "chkBatchDownload_IncludeCheckedOut";
            this.chkBatchDownload_IncludeCheckedOut.Size = new System.Drawing.Size(127, 17);
            this.chkBatchDownload_IncludeCheckedOut.TabIndex = 15;
            this.chkBatchDownload_IncludeCheckedOut.Text = "Include Checked Out";
            this.chkBatchDownload_IncludeCheckedOut.UseVisualStyleBackColor = true;
            this.chkBatchDownload_IncludeCheckedOut.CheckedChanged += new System.EventHandler(this.chkBatchDownload_IncludeCheckedOut_CheckedChanged);
            // 
            // btnOP_DownloadBatches
            // 
            this.btnOP_DownloadBatches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOP_DownloadBatches.Location = new System.Drawing.Point(6, 115);
            this.btnOP_DownloadBatches.Name = "btnOP_DownloadBatches";
            this.btnOP_DownloadBatches.Size = new System.Drawing.Size(306, 23);
            this.btnOP_DownloadBatches.TabIndex = 14;
            this.btnOP_DownloadBatches.Text = "Download Batches";
            this.btnOP_DownloadBatches.UseVisualStyleBackColor = true;
            this.btnOP_DownloadBatches.Click += new System.EventHandler(this.btnOP_DownloadBatches_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Batch Status:";
            // 
            // cboDownloadBatch_Status
            // 
            this.cboDownloadBatch_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboDownloadBatch_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDownloadBatch_Status.DropDownWidth = 250;
            this.cboDownloadBatch_Status.FormattingEnabled = true;
            this.cboDownloadBatch_Status.Location = new System.Drawing.Point(84, 20);
            this.cboDownloadBatch_Status.MaxDropDownItems = 50;
            this.cboDownloadBatch_Status.Name = "cboDownloadBatch_Status";
            this.cboDownloadBatch_Status.Size = new System.Drawing.Size(228, 21);
            this.cboDownloadBatch_Status.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOP_ValidateBatches);
            this.groupBox2.Controls.Add(this.btnOP_MyBatches);
            this.groupBox2.Controls.Add(this.btnOP_SaveBatch);
            this.groupBox2.Controls.Add(this.btnOP_Clear);
            this.groupBox2.Controls.Add(this.btnOP_SubmitBatch);
            this.groupBox2.Controls.Add(this.btnOP_SaveBatchUpload);
            this.groupBox2.Controls.Add(this.btnOP_CreateBatch);
            this.groupBox2.Location = new System.Drawing.Point(0, 688);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 208);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operations";
            // 
            // btnOP_ValidateBatches
            // 
            this.btnOP_ValidateBatches.Location = new System.Drawing.Point(6, 144);
            this.btnOP_ValidateBatches.Name = "btnOP_ValidateBatches";
            this.btnOP_ValidateBatches.Size = new System.Drawing.Size(306, 25);
            this.btnOP_ValidateBatches.TabIndex = 49;
            this.btnOP_ValidateBatches.Text = "Validate Batches";
            this.btnOP_ValidateBatches.UseVisualStyleBackColor = true;
            this.btnOP_ValidateBatches.Click += new System.EventHandler(this.btnOP_ValidateBatches_Click);
            // 
            // btnOP_MyBatches
            // 
            this.btnOP_MyBatches.Location = new System.Drawing.Point(6, 19);
            this.btnOP_MyBatches.Name = "btnOP_MyBatches";
            this.btnOP_MyBatches.Size = new System.Drawing.Size(150, 26);
            this.btnOP_MyBatches.TabIndex = 48;
            this.btnOP_MyBatches.Text = "My Batches";
            this.btnOP_MyBatches.UseVisualStyleBackColor = true;
            this.btnOP_MyBatches.Click += new System.EventHandler(this.btnOP_MyBatches_Click);
            // 
            // btnOP_SaveBatch
            // 
            this.btnOP_SaveBatch.Location = new System.Drawing.Point(6, 82);
            this.btnOP_SaveBatch.Name = "btnOP_SaveBatch";
            this.btnOP_SaveBatch.Size = new System.Drawing.Size(306, 25);
            this.btnOP_SaveBatch.TabIndex = 8;
            this.btnOP_SaveBatch.Text = "Save Batch and Document Changes";
            this.btnOP_SaveBatch.UseVisualStyleBackColor = true;
            this.btnOP_SaveBatch.Click += new System.EventHandler(this.btnOP_SaveBatch_Click);
            // 
            // btnOP_SubmitBatch
            // 
            this.btnOP_SubmitBatch.Location = new System.Drawing.Point(6, 175);
            this.btnOP_SubmitBatch.Name = "btnOP_SubmitBatch";
            this.btnOP_SubmitBatch.Size = new System.Drawing.Size(306, 25);
            this.btnOP_SubmitBatch.TabIndex = 7;
            this.btnOP_SubmitBatch.Text = "Validate Batches, Submit Batches to SECURE";
            this.btnOP_SubmitBatch.UseVisualStyleBackColor = true;
            this.btnOP_SubmitBatch.Click += new System.EventHandler(this.btnOP_SubmitBatch_Click);
            // 
            // btnOP_SaveBatchUpload
            // 
            this.btnOP_SaveBatchUpload.Location = new System.Drawing.Point(6, 113);
            this.btnOP_SaveBatchUpload.Name = "btnOP_SaveBatchUpload";
            this.btnOP_SaveBatchUpload.Size = new System.Drawing.Size(306, 25);
            this.btnOP_SaveBatchUpload.TabIndex = 6;
            this.btnOP_SaveBatchUpload.Text = "Save Batch and Document Changes, Upload Images";
            this.btnOP_SaveBatchUpload.UseVisualStyleBackColor = true;
            this.btnOP_SaveBatchUpload.Click += new System.EventHandler(this.btnOP_SaveBatchUpload_Click);
            // 
            // btnOP_CreateBatch
            // 
            this.btnOP_CreateBatch.Location = new System.Drawing.Point(6, 51);
            this.btnOP_CreateBatch.Name = "btnOP_CreateBatch";
            this.btnOP_CreateBatch.Size = new System.Drawing.Size(303, 25);
            this.btnOP_CreateBatch.TabIndex = 5;
            this.btnOP_CreateBatch.Text = "Create Batch";
            this.btnOP_CreateBatch.UseVisualStyleBackColor = true;
            this.btnOP_CreateBatch.Click += new System.EventHandler(this.btnOP_CreateBatch_Click);
            // 
            // trvBatches
            // 
            this.trvBatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trvBatches.ContextMenuStrip = this.cmsTree;
            this.trvBatches.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvBatches.ImageIndex = 0;
            this.trvBatches.ImageList = this.imglistTree;
            this.trvBatches.Location = new System.Drawing.Point(329, 53);
            this.trvBatches.Name = "trvBatches";
            this.trvBatches.SelectedImageIndex = 0;
            this.trvBatches.Size = new System.Drawing.Size(427, 1185);
            this.trvBatches.TabIndex = 17;
            this.trvBatches.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvBatches_NodeMouseClick);
            this.trvBatches.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trvBatches_MouseClick);
            // 
            // cmsTree
            // 
            this.cmsTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDocumentToolStripMenuItem,
            this.downloadDocumentsToolStripMenuItem,
            this.checkOutBatchToolStripMenuItem,
            this.checkInBatchToolStripMenuItem,
            this.overrideCheckOutToolStripMenuItem,
            this.removeDocumentToolStripMenuItem,
            this.addRecordableFileToolStripMenuItem,
            this.addPCORToolStripMenuItem,
            this.addOtherFileToolStripMenuItem,
            this.downloadDocumentToolStripMenuItem,
            this.deleteBatchToolStripMenuItem});
            this.cmsTree.Name = "cmsTree";
            this.cmsTree.Size = new System.Drawing.Size(229, 246);
            // 
            // addDocumentToolStripMenuItem
            // 
            this.addDocumentToolStripMenuItem.Name = "addDocumentToolStripMenuItem";
            this.addDocumentToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.addDocumentToolStripMenuItem.Text = "Add Document";
            this.addDocumentToolStripMenuItem.Visible = false;
            this.addDocumentToolStripMenuItem.Click += new System.EventHandler(this.addDocumentToolStripMenuItem_Click);
            // 
            // downloadDocumentsToolStripMenuItem
            // 
            this.downloadDocumentsToolStripMenuItem.Name = "downloadDocumentsToolStripMenuItem";
            this.downloadDocumentsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.downloadDocumentsToolStripMenuItem.Text = "Download Document Images";
            this.downloadDocumentsToolStripMenuItem.Visible = false;
            this.downloadDocumentsToolStripMenuItem.Click += new System.EventHandler(this.downloadDocumentsToolStripMenuItem_Click);
            // 
            // checkOutBatchToolStripMenuItem
            // 
            this.checkOutBatchToolStripMenuItem.Name = "checkOutBatchToolStripMenuItem";
            this.checkOutBatchToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.checkOutBatchToolStripMenuItem.Text = "Check Out Batch";
            this.checkOutBatchToolStripMenuItem.Visible = false;
            this.checkOutBatchToolStripMenuItem.Click += new System.EventHandler(this.checkOutBatchToolStripMenuItem_Click);
            // 
            // checkInBatchToolStripMenuItem
            // 
            this.checkInBatchToolStripMenuItem.Name = "checkInBatchToolStripMenuItem";
            this.checkInBatchToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.checkInBatchToolStripMenuItem.Text = "Check In Batch";
            this.checkInBatchToolStripMenuItem.Visible = false;
            this.checkInBatchToolStripMenuItem.Click += new System.EventHandler(this.checkInBatchToolStripMenuItem_Click);
            // 
            // overrideCheckOutToolStripMenuItem
            // 
            this.overrideCheckOutToolStripMenuItem.Name = "overrideCheckOutToolStripMenuItem";
            this.overrideCheckOutToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.overrideCheckOutToolStripMenuItem.Text = "Override CheckOut";
            this.overrideCheckOutToolStripMenuItem.Visible = false;
            this.overrideCheckOutToolStripMenuItem.Click += new System.EventHandler(this.overrideCheckOutToolStripMenuItem_Click);
            // 
            // removeDocumentToolStripMenuItem
            // 
            this.removeDocumentToolStripMenuItem.Name = "removeDocumentToolStripMenuItem";
            this.removeDocumentToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.removeDocumentToolStripMenuItem.Text = "Remove Document";
            this.removeDocumentToolStripMenuItem.Click += new System.EventHandler(this.removeDocumentToolStripMenuItem_Click);
            // 
            // addRecordableFileToolStripMenuItem
            // 
            this.addRecordableFileToolStripMenuItem.Name = "addRecordableFileToolStripMenuItem";
            this.addRecordableFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.addRecordableFileToolStripMenuItem.Text = "Add Recordable File";
            this.addRecordableFileToolStripMenuItem.Visible = false;
            this.addRecordableFileToolStripMenuItem.Click += new System.EventHandler(this.addRecordableFileToolStripMenuItem_Click);
            // 
            // addPCORToolStripMenuItem
            // 
            this.addPCORToolStripMenuItem.Name = "addPCORToolStripMenuItem";
            this.addPCORToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.addPCORToolStripMenuItem.Text = "Add PCOR File";
            this.addPCORToolStripMenuItem.Visible = false;
            this.addPCORToolStripMenuItem.Click += new System.EventHandler(this.addPCORToolStripMenuItem_Click);
            // 
            // addOtherFileToolStripMenuItem
            // 
            this.addOtherFileToolStripMenuItem.Name = "addOtherFileToolStripMenuItem";
            this.addOtherFileToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.addOtherFileToolStripMenuItem.Text = "Add Other File";
            this.addOtherFileToolStripMenuItem.Visible = false;
            this.addOtherFileToolStripMenuItem.Click += new System.EventHandler(this.addOtherFileToolStripMenuItem_Click);
            // 
            // downloadDocumentToolStripMenuItem
            // 
            this.downloadDocumentToolStripMenuItem.Name = "downloadDocumentToolStripMenuItem";
            this.downloadDocumentToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.downloadDocumentToolStripMenuItem.Text = "Download Document Image";
            this.downloadDocumentToolStripMenuItem.Visible = false;
            this.downloadDocumentToolStripMenuItem.Click += new System.EventHandler(this.downloadDocumentToolStripMenuItem_Click);
            // 
            // deleteBatchToolStripMenuItem
            // 
            this.deleteBatchToolStripMenuItem.Name = "deleteBatchToolStripMenuItem";
            this.deleteBatchToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.deleteBatchToolStripMenuItem.Text = "Delete Batch";
            this.deleteBatchToolStripMenuItem.Click += new System.EventHandler(this.deleteBatchToolStripMenuItem_Click);
            // 
            // imglistTree
            // 
            this.imglistTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imglistTree.ImageSize = new System.Drawing.Size(16, 16);
            this.imglistTree.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel.Controls.Add(this.batchDetailControl);
            this.flowLayoutPanel.Controls.Add(this.documentInfoControl);
            this.flowLayoutPanel.Controls.Add(this.flowFileContent);
            this.flowLayoutPanel.Location = new System.Drawing.Point(762, 53);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(1049, 1185);
            this.flowLayoutPanel.TabIndex = 19;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // batchDetailControl
            // 
            this.batchDetailControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.batchDetailControl.BatchDetail = ((BatchDetail)(resources.GetObject("batchDetailControl.BatchDetail")));
            this.batchDetailControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.batchDetailControl.EnableConcurrentIndex = true;
            this.batchDetailControl.EnableEdit = false;
            this.batchDetailControl.EnableRequestingParty = true;
            this.batchDetailControl.Location = new System.Drawing.Point(3, 3);
            this.batchDetailControl.Name = "batchDetailControl";
            this.batchDetailControl.Size = new System.Drawing.Size(371, 995);
            this.batchDetailControl.TabIndex = 0;
            this.batchDetailControl.Visible = false;
            // 
            // documentInfoControl
            // 
            this.documentInfoControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.documentInfoControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.documentInfoControl.DocumentInfo = ((DocumentInfo)(resources.GetObject("documentInfoControl.DocumentInfo")));
            this.documentInfoControl.EnableAssessorParcelNumber = false;
            this.documentInfoControl.EnableCities = false;
            this.documentInfoControl.EnableDocumentNumber = false;
            this.documentInfoControl.EnableEdit = false;
            this.documentInfoControl.EnableExternalID = false;
            this.documentInfoControl.EnableFees = false;
            this.documentInfoControl.EnableMemo = false;
            this.documentInfoControl.EnableNames = false;
            this.documentInfoControl.EnableRecordedTS = false;
            this.documentInfoControl.EnableSaleAmount = false;
            this.documentInfoControl.EnableSequence = false;
            this.documentInfoControl.EnableTitles = false;
            this.documentInfoControl.EnableTransferTaxAmount = false;
            this.documentInfoControl.Location = new System.Drawing.Point(380, 3);
            this.documentInfoControl.Name = "documentInfoControl";
            this.documentInfoControl.Size = new System.Drawing.Size(292, 995);
            this.documentInfoControl.TabIndex = 1;
            this.documentInfoControl.Visible = false;
            // 
            // flowFileContent
            // 
            this.flowFileContent.Controls.Add(this.fileContentInfoControl_RECORDABLE);
            this.flowFileContent.Controls.Add(this.fileContentInfoControl_PCOR);
            this.flowFileContent.Controls.Add(this.fileContentInfoControl_OTHER);
            this.flowFileContent.Location = new System.Drawing.Point(678, 3);
            this.flowFileContent.Name = "flowFileContent";
            this.flowFileContent.Size = new System.Drawing.Size(357, 995);
            this.flowFileContent.TabIndex = 2;
            // 
            // fileContentInfoControl_RECORDABLE
            // 
            this.fileContentInfoControl_RECORDABLE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileContentInfoControl_RECORDABLE.FileContentDetail = null;
            this.fileContentInfoControl_RECORDABLE.Location = new System.Drawing.Point(3, 3);
            this.fileContentInfoControl_RECORDABLE.Name = "fileContentInfoControl_RECORDABLE";
            this.fileContentInfoControl_RECORDABLE.Size = new System.Drawing.Size(351, 325);
            this.fileContentInfoControl_RECORDABLE.TabIndex = 0;
            this.fileContentInfoControl_RECORDABLE.Title = "Recordable Image";
            this.fileContentInfoControl_RECORDABLE.Visible = false;
            // 
            // fileContentInfoControl_PCOR
            // 
            this.fileContentInfoControl_PCOR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileContentInfoControl_PCOR.FileContentDetail = null;
            this.fileContentInfoControl_PCOR.Location = new System.Drawing.Point(3, 334);
            this.fileContentInfoControl_PCOR.Name = "fileContentInfoControl_PCOR";
            this.fileContentInfoControl_PCOR.Size = new System.Drawing.Size(351, 325);
            this.fileContentInfoControl_PCOR.TabIndex = 1;
            this.fileContentInfoControl_PCOR.Title = "PCOR Image";
            this.fileContentInfoControl_PCOR.Visible = false;
            // 
            // fileContentInfoControl_OTHER
            // 
            this.fileContentInfoControl_OTHER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileContentInfoControl_OTHER.FileContentDetail = null;
            this.fileContentInfoControl_OTHER.Location = new System.Drawing.Point(3, 665);
            this.fileContentInfoControl_OTHER.Name = "fileContentInfoControl_OTHER";
            this.fileContentInfoControl_OTHER.Size = new System.Drawing.Size(351, 325);
            this.fileContentInfoControl_OTHER.TabIndex = 2;
            this.fileContentInfoControl_OTHER.Title = "Other Image";
            this.fileContentInfoControl_OTHER.Visible = false;
            // 
            // chkAPI
            // 
            this.chkAPI.AutoSize = true;
            this.chkAPI.Location = new System.Drawing.Point(6, 902);
            this.chkAPI.Name = "chkAPI";
            this.chkAPI.Size = new System.Drawing.Size(129, 17);
            this.chkAPI.TabIndex = 20;
            this.chkAPI.Text = "Show Direct API Calls";
            this.chkAPI.UseVisualStyleBackColor = true;
            this.chkAPI.CheckedChanged += new System.EventHandler(this.chkAPI_CheckedChanged);
            // 
            // grpURL
            // 
            this.grpURL.BackColor = System.Drawing.Color.Transparent;
            this.grpURL.Controls.Add(this.btnURLEdit);
            this.grpURL.Controls.Add(this.txtURL);
            this.grpURL.Controls.Add(this.label6);
            this.grpURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpURL.Location = new System.Drawing.Point(0, 3);
            this.grpURL.Name = "grpURL";
            this.grpURL.Size = new System.Drawing.Size(472, 44);
            this.grpURL.TabIndex = 21;
            this.grpURL.TabStop = false;
            // 
            // btnURLEdit
            // 
            this.btnURLEdit.Location = new System.Drawing.Point(391, 12);
            this.btnURLEdit.Name = "btnURLEdit";
            this.btnURLEdit.Size = new System.Drawing.Size(75, 23);
            this.btnURLEdit.TabIndex = 7;
            this.btnURLEdit.Text = "Edit";
            this.btnURLEdit.UseVisualStyleBackColor = true;
            this.btnURLEdit.Click += new System.EventHandler(this.btnURLEdit_Click);
            // 
            // txtURL
            // 
            this.txtURL.Enabled = false;
            this.txtURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtURL.Location = new System.Drawing.Point(92, 14);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(293, 20);
            this.txtURL.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "SECURE URL:";
            // 
            // chk_XML_Validation
            // 
            this.chk_XML_Validation.AutoSize = true;
            this.chk_XML_Validation.Location = new System.Drawing.Point(6, 53);
            this.chk_XML_Validation.Name = "chk_XML_Validation";
            this.chk_XML_Validation.Size = new System.Drawing.Size(89, 17);
            this.chk_XML_Validation.TabIndex = 22;
            this.chk_XML_Validation.Text = "Validate XML";
            this.chk_XML_Validation.UseVisualStyleBackColor = true;
            this.chk_XML_Validation.CheckedChanged += new System.EventHandler(this.chk_XML_Validation_CheckedChanged);
            // 
            // grpCurrentUser
            // 
            this.grpCurrentUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCurrentUser.BackColor = System.Drawing.Color.Transparent;
            this.grpCurrentUser.Controls.Add(this.txtCurrentUser_Name);
            this.grpCurrentUser.Controls.Add(this.label11);
            this.grpCurrentUser.Controls.Add(this.txtCurrentUser_PartyType);
            this.grpCurrentUser.Controls.Add(this.txtCurrentUser_SubmittingParty);
            this.grpCurrentUser.Controls.Add(this.txtCurrentUser_UserType);
            this.grpCurrentUser.Controls.Add(this.txtCurrentUser_UserName);
            this.grpCurrentUser.Controls.Add(this.label10);
            this.grpCurrentUser.Controls.Add(this.label9);
            this.grpCurrentUser.Controls.Add(this.label8);
            this.grpCurrentUser.Controls.Add(this.label7);
            this.grpCurrentUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCurrentUser.Location = new System.Drawing.Point(478, 3);
            this.grpCurrentUser.Margin = new System.Windows.Forms.Padding(0);
            this.grpCurrentUser.Name = "grpCurrentUser";
            this.grpCurrentUser.Padding = new System.Windows.Forms.Padding(0);
            this.grpCurrentUser.Size = new System.Drawing.Size(1333, 44);
            this.grpCurrentUser.TabIndex = 23;
            this.grpCurrentUser.TabStop = false;
            this.grpCurrentUser.Text = "Current User";
            // 
            // txtCurrentUser_Name
            // 
            this.txtCurrentUser_Name.Location = new System.Drawing.Point(288, 14);
            this.txtCurrentUser_Name.Name = "txtCurrentUser_Name";
            this.txtCurrentUser_Name.ReadOnly = true;
            this.txtCurrentUser_Name.Size = new System.Drawing.Size(176, 20);
            this.txtCurrentUser_Name.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(248, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Name:";
            // 
            // txtCurrentUser_PartyType
            // 
            this.txtCurrentUser_PartyType.Location = new System.Drawing.Point(1153, 14);
            this.txtCurrentUser_PartyType.Name = "txtCurrentUser_PartyType";
            this.txtCurrentUser_PartyType.ReadOnly = true;
            this.txtCurrentUser_PartyType.Size = new System.Drawing.Size(170, 20);
            this.txtCurrentUser_PartyType.TabIndex = 7;
            // 
            // txtCurrentUser_SubmittingParty
            // 
            this.txtCurrentUser_SubmittingParty.Location = new System.Drawing.Point(800, 14);
            this.txtCurrentUser_SubmittingParty.Name = "txtCurrentUser_SubmittingParty";
            this.txtCurrentUser_SubmittingParty.ReadOnly = true;
            this.txtCurrentUser_SubmittingParty.Size = new System.Drawing.Size(283, 20);
            this.txtCurrentUser_SubmittingParty.TabIndex = 6;
            // 
            // txtCurrentUser_UserType
            // 
            this.txtCurrentUser_UserType.Location = new System.Drawing.Point(532, 14);
            this.txtCurrentUser_UserType.Name = "txtCurrentUser_UserType";
            this.txtCurrentUser_UserType.ReadOnly = true;
            this.txtCurrentUser_UserType.Size = new System.Drawing.Size(173, 20);
            this.txtCurrentUser_UserType.TabIndex = 5;
            // 
            // txtCurrentUser_UserName
            // 
            this.txtCurrentUser_UserName.Location = new System.Drawing.Point(72, 14);
            this.txtCurrentUser_UserName.Name = "txtCurrentUser_UserName";
            this.txtCurrentUser_UserName.ReadOnly = true;
            this.txtCurrentUser_UserName.Size = new System.Drawing.Size(170, 20);
            this.txtCurrentUser_UserName.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1089, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "PartyType:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(711, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "SubmittingParty:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(470, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "UserType:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "UserName:";
            // 
            // chkToken
            // 
            this.chkToken.AutoSize = true;
            this.chkToken.Location = new System.Drawing.Point(102, 53);
            this.chkToken.Name = "chkToken";
            this.chkToken.Size = new System.Drawing.Size(151, 17);
            this.chkToken.TabIndex = 24;
            this.chkToken.Text = "Two-Factor Authentication";
            this.chkToken.UseVisualStyleBackColor = true;
            this.chkToken.CheckedChanged += new System.EventHandler(this.chkToken_CheckedChanged);
            // 
            // SubmitterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.chkToken);
            this.Controls.Add(this.grpCurrentUser);
            this.Controls.Add(this.chk_XML_Validation);
            this.Controls.Add(this.grpURL);
            this.Controls.Add(this.chkAPI);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.trvBatches);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpAuthorization);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpAPIButtons);
            this.Controls.Add(this.ssStatus);
            this.Name = "SubmitterPanel";
            this.Size = new System.Drawing.Size(1814, 1268);
            this.Load += new System.EventHandler(this.SubmitterPanel_Load);
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.grpAPIButtons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpSearchOptions.ResumeLayout(false);
            this.grpSearchOptions.PerformLayout();
            this.grpAuthorization.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.cmsTree.ResumeLayout(false);
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowFileContent.ResumeLayout(false);
            this.grpURL.ResumeLayout(false);
            this.grpURL.PerformLayout();
            this.grpCurrentUser.ResumeLayout(false);
            this.grpCurrentUser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCache_LoadCache;
        private System.Windows.Forms.Button btnAuth_Login;
        private System.Windows.Forms.Button btnOP_ViewBatch;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblSessionTokenID;
        private System.Windows.Forms.Button btnCache_ViewCache;
        private System.Windows.Forms.Button btnAuth_Logout;
        private System.Windows.Forms.GroupBox grpAPIButtons;
        private System.Windows.Forms.ComboBox cboViewBatch_Status;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpAuthorization;
        private System.Windows.Forms.Button btnAuth_Ping;
        private System.Windows.Forms.Button btnAuth_ChangePassword;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpBatchSearch_end;
        private System.Windows.Forms.DateTimePicker dtpBatchSearch_start;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnOP_DownloadBatches;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboDownloadBatch_Status;
        private System.Windows.Forms.Label lblBatchSearchInpu;
        private System.Windows.Forms.TextBox txtViewBatchSearch_input;
        private System.Windows.Forms.Button btnAPI_BatchUploadComplete;
        private System.Windows.Forms.Button btnAPI_DocumentUpload;
        private System.Windows.Forms.Button btnAPI_BatchUpload;
        private System.Windows.Forms.Button btnAPI_BatchCreate;
        private System.Windows.Forms.Button btnOP_Clear;
        private System.Windows.Forms.Button btnAPI_DocumentDownload;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOP_SubmitBatch;
        private System.Windows.Forms.Button btnOP_SaveBatchUpload;
        private System.Windows.Forms.Button btnOP_CreateBatch;
        private System.Windows.Forms.TreeView trvBatches;
        private System.Windows.Forms.ContextMenuStrip cmsTree;
        private System.Windows.Forms.ImageList imglistTree;
        private System.Windows.Forms.ToolStripMenuItem addDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRecordableFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPCORToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addOtherFileToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.ToolStripMenuItem downloadDocumentToolStripMenuItem;
        private System.Windows.Forms.Button btnOP_SaveBatch;
        private System.Windows.Forms.ToolStripMenuItem downloadDocumentsToolStripMenuItem;
        private ClientControls.BatchDetailControl batchDetailControl;
        private ClientControls.DocumentInfoControl documentInfoControl;
        private System.Windows.Forms.FlowLayoutPanel flowFileContent;
        private ClientControls.FileContentInfoControl fileContentInfoControl_RECORDABLE;
        private ClientControls.FileContentInfoControl fileContentInfoControl_PCOR;
        private ClientControls.FileContentInfoControl fileContentInfoControl_OTHER;
        private System.Windows.Forms.ToolStripMenuItem checkOutBatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkInBatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overrideCheckOutToolStripMenuItem;
        private System.Windows.Forms.Button btnAPI_BatchDownloadComplete;
        private System.Windows.Forms.CheckBox chkAPI;
        private System.Windows.Forms.Button btnAPI_BatchDownload;
        private System.Windows.Forms.ToolStripMenuItem removeDocumentToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpSearchOptions;
        private System.Windows.Forms.CheckBox chkSearch_UserName;
        private System.Windows.Forms.CheckBox chkSearch_County;
        private System.Windows.Forms.CheckBox chkSearch_BatchName;
        private System.Windows.Forms.CheckBox chkSearch_SubmissionID;
        private System.Windows.Forms.CheckBox chkSearch_BatchID;
        private System.Windows.Forms.CheckBox chkSearch_Status;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboCounty;
        private System.Windows.Forms.ToolStripMenuItem deleteBatchToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkBatchDownload_IncludeCheckedOut;
        private System.Windows.Forms.CheckBox chkBatchDownload_SetComplete;
        private System.Windows.Forms.CheckBox chkBatchDownload_CheckOut;
        private System.Windows.Forms.GroupBox grpURL;
        private System.Windows.Forms.Button btnURLEdit;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOP_MyBatches;
        private System.Windows.Forms.CheckBox chk_XML_Validation;
        private System.Windows.Forms.GroupBox grpCurrentUser;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCurrentUser_PartyType;
        private System.Windows.Forms.TextBox txtCurrentUser_SubmittingParty;
        private System.Windows.Forms.TextBox txtCurrentUser_UserType;
        private System.Windows.Forms.TextBox txtCurrentUser_UserName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCurrentUser_Name;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkToken;
        private System.Windows.Forms.CheckBox chkBatchWithDocumentsDownload;
        private System.Windows.Forms.Button btnAPI_BatchWithDocuments_ReadyByID;
        private System.Windows.Forms.Button btnAPI_BatchWithDocuments_Upload;
        private System.Windows.Forms.Button btnOP_ValidateBatches;

    }
}

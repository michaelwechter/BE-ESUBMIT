namespace WinFormTest.ClientControls
{
    partial class DocumentInfoControl
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
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.tblTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblExternalID = new System.Windows.Forms.Label();
            this.txtExternalID = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.lblRecordedTimeStamp = new System.Windows.Forms.Label();
            this.dtpRecordedTS = new System.Windows.Forms.DateTimePicker();
            this.lblStatusCode = new System.Windows.Forms.Label();
            this.txtStatusCode = new System.Windows.Forms.TextBox();
            this.lblActionCode = new System.Windows.Forms.Label();
            this.txtActionCode = new System.Windows.Forms.TextBox();
            this.lblSequence = new System.Windows.Forms.Label();
            this.txtSequence = new System.Windows.Forms.TextBox();
            this.lblAssessorParcelNumber = new System.Windows.Forms.Label();
            this.txtAssessorParcelNumber = new System.Windows.Forms.TextBox();
            this.lblTransferTaxAmount = new System.Windows.Forms.Label();
            this.txtTransferTaxAmount = new System.Windows.Forms.TextBox();
            this.lblSalesAmount = new System.Windows.Forms.Label();
            this.txtSaleAmount = new System.Windows.Forms.TextBox();
            this.lstTitles = new System.Windows.Forms.ListBox();
            this.tblEditTitles = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitles = new System.Windows.Forms.Label();
            this.btnEditTitles = new System.Windows.Forms.Button();
            this.tblEditCities = new System.Windows.Forms.TableLayoutPanel();
            this.lblCities = new System.Windows.Forms.Label();
            this.btnEditCities = new System.Windows.Forms.Button();
            this.lstCities = new System.Windows.Forms.ListBox();
            this.tblEditFees = new System.Windows.Forms.TableLayoutPanel();
            this.lblFees = new System.Windows.Forms.Label();
            this.btnAddFees = new System.Windows.Forms.Button();
            this.btnDeleteFee = new System.Windows.Forms.Button();
            this.lstFees = new System.Windows.Forms.ListBox();
            this.tblEditNames = new System.Windows.Forms.TableLayoutPanel();
            this.lblNames = new System.Windows.Forms.Label();
            this.btnAddNames = new System.Windows.Forms.Button();
            this.btnDeleteName = new System.Windows.Forms.Button();
            this.lstNames = new System.Windows.Forms.ListBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.lblCreateTimeStamp = new System.Windows.Forms.Label();
            this.txtCreateTimeStamp = new System.Windows.Forms.TextBox();
            this.lblEditTimeStamp = new System.Windows.Forms.Label();
            this.txtEditTimeStamp = new System.Windows.Forms.TextBox();
            this.tblEditMemos = new System.Windows.Forms.TableLayoutPanel();
            this.lblMemos = new System.Windows.Forms.Label();
            this.btnAddMemo = new System.Windows.Forms.Button();
            this.btnDeleteMemo = new System.Windows.Forms.Button();
            this.lstMemos = new System.Windows.Forms.ListBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.tblHeader.SuspendLayout();
            this.tblTableLayout.SuspendLayout();
            this.tblEditTitles.SuspendLayout();
            this.tblEditCities.SuspendLayout();
            this.tblEditFees.SuspendLayout();
            this.tblEditNames.SuspendLayout();
            this.tblEditMemos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblHeader
            // 
            this.tblHeader.ColumnCount = 1;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Controls.Add(this.tblTableLayout, 0, 1);
            this.tblHeader.Controls.Add(this.lblHeader, 0, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeader.Location = new System.Drawing.Point(0, 0);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 2;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Size = new System.Drawing.Size(331, 822);
            this.tblHeader.TabIndex = 0;
            // 
            // tblTableLayout
            // 
            this.tblTableLayout.ColumnCount = 2;
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTableLayout.Controls.Add(this.lblID, 0, 0);
            this.tblTableLayout.Controls.Add(this.txtID, 1, 0);
            this.tblTableLayout.Controls.Add(this.lblExternalID, 0, 1);
            this.tblTableLayout.Controls.Add(this.txtExternalID, 1, 1);
            this.tblTableLayout.Controls.Add(this.lblNumber, 0, 2);
            this.tblTableLayout.Controls.Add(this.txtNumber, 1, 2);
            this.tblTableLayout.Controls.Add(this.lblRecordedTimeStamp, 0, 3);
            this.tblTableLayout.Controls.Add(this.dtpRecordedTS, 1, 3);
            this.tblTableLayout.Controls.Add(this.lblStatusCode, 0, 4);
            this.tblTableLayout.Controls.Add(this.txtStatusCode, 1, 4);
            this.tblTableLayout.Controls.Add(this.lblActionCode, 0, 5);
            this.tblTableLayout.Controls.Add(this.txtActionCode, 1, 5);
            this.tblTableLayout.Controls.Add(this.lblSequence, 0, 6);
            this.tblTableLayout.Controls.Add(this.txtSequence, 1, 6);
            this.tblTableLayout.Controls.Add(this.lblAssessorParcelNumber, 0, 7);
            this.tblTableLayout.Controls.Add(this.txtAssessorParcelNumber, 1, 7);
            this.tblTableLayout.Controls.Add(this.lblTransferTaxAmount, 0, 8);
            this.tblTableLayout.Controls.Add(this.txtTransferTaxAmount, 1, 8);
            this.tblTableLayout.Controls.Add(this.lblSalesAmount, 0, 9);
            this.tblTableLayout.Controls.Add(this.txtSaleAmount, 1, 9);
            this.tblTableLayout.Controls.Add(this.lstTitles, 1, 10);
            this.tblTableLayout.Controls.Add(this.tblEditTitles, 0, 10);
            this.tblTableLayout.Controls.Add(this.tblEditCities, 0, 11);
            this.tblTableLayout.Controls.Add(this.lstCities, 1, 11);
            this.tblTableLayout.Controls.Add(this.tblEditFees, 0, 12);
            this.tblTableLayout.Controls.Add(this.lstFees, 1, 12);
            this.tblTableLayout.Controls.Add(this.tblEditNames, 0, 13);
            this.tblTableLayout.Controls.Add(this.lstNames, 1, 13);
            this.tblTableLayout.Controls.Add(this.lblVersion, 0, 14);
            this.tblTableLayout.Controls.Add(this.txtVersion, 1, 14);
            this.tblTableLayout.Controls.Add(this.lblCreateTimeStamp, 0, 15);
            this.tblTableLayout.Controls.Add(this.txtCreateTimeStamp, 1, 15);
            this.tblTableLayout.Controls.Add(this.lblEditTimeStamp, 0, 16);
            this.tblTableLayout.Controls.Add(this.txtEditTimeStamp, 1, 16);
            this.tblTableLayout.Controls.Add(this.tblEditMemos, 0, 17);
            this.tblTableLayout.Controls.Add(this.lstMemos, 1, 17);
            this.tblTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTableLayout.Location = new System.Drawing.Point(3, 33);
            this.tblTableLayout.Name = "tblTableLayout";
            this.tblTableLayout.RowCount = 18;
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 182F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.tblTableLayout.Size = new System.Drawing.Size(325, 786);
            this.tblTableLayout.TabIndex = 0;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblID.Location = new System.Drawing.Point(3, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(144, 25);
            this.lblID.TabIndex = 44;
            this.lblID.Text = "ID:";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtID.Location = new System.Drawing.Point(153, 3);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(169, 20);
            this.txtID.TabIndex = 43;
            // 
            // lblExternalID
            // 
            this.lblExternalID.AutoSize = true;
            this.lblExternalID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExternalID.Location = new System.Drawing.Point(3, 25);
            this.lblExternalID.Name = "lblExternalID";
            this.lblExternalID.Size = new System.Drawing.Size(144, 25);
            this.lblExternalID.TabIndex = 46;
            this.lblExternalID.Text = "External ID:";
            this.lblExternalID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtExternalID
            // 
            this.txtExternalID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExternalID.Location = new System.Drawing.Point(153, 28);
            this.txtExternalID.Name = "txtExternalID";
            this.txtExternalID.Size = new System.Drawing.Size(169, 20);
            this.txtExternalID.TabIndex = 47;
            this.txtExternalID.TextChanged += new System.EventHandler(this.txtExternalID_TextChanged);
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNumber.Location = new System.Drawing.Point(3, 50);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(144, 25);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "Number:";
            this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumber
            // 
            this.txtNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNumber.Location = new System.Drawing.Point(153, 53);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(169, 20);
            this.txtNumber.TabIndex = 16;
            this.txtNumber.TextChanged += new System.EventHandler(this.txtNumber_TextChanged);
            this.txtNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // lblRecordedTimeStamp
            // 
            this.lblRecordedTimeStamp.AutoSize = true;
            this.lblRecordedTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRecordedTimeStamp.Location = new System.Drawing.Point(3, 75);
            this.lblRecordedTimeStamp.Name = "lblRecordedTimeStamp";
            this.lblRecordedTimeStamp.Size = new System.Drawing.Size(144, 25);
            this.lblRecordedTimeStamp.TabIndex = 1;
            this.lblRecordedTimeStamp.Text = "Recorded Time Stamp";
            this.lblRecordedTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpRecordedTS
            // 
            this.dtpRecordedTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpRecordedTS.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRecordedTS.Location = new System.Drawing.Point(153, 78);
            this.dtpRecordedTS.Name = "dtpRecordedTS";
            this.dtpRecordedTS.Size = new System.Drawing.Size(169, 20);
            this.dtpRecordedTS.TabIndex = 45;
            this.dtpRecordedTS.ValueChanged += new System.EventHandler(this.dtpRecordedTS_ValueChanged);
            // 
            // lblStatusCode
            // 
            this.lblStatusCode.AutoSize = true;
            this.lblStatusCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusCode.Location = new System.Drawing.Point(3, 100);
            this.lblStatusCode.Name = "lblStatusCode";
            this.lblStatusCode.Size = new System.Drawing.Size(144, 25);
            this.lblStatusCode.TabIndex = 2;
            this.lblStatusCode.Text = "Status Code:";
            this.lblStatusCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStatusCode
            // 
            this.txtStatusCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatusCode.Location = new System.Drawing.Point(153, 103);
            this.txtStatusCode.Name = "txtStatusCode";
            this.txtStatusCode.ReadOnly = true;
            this.txtStatusCode.Size = new System.Drawing.Size(169, 20);
            this.txtStatusCode.TabIndex = 18;
            // 
            // lblActionCode
            // 
            this.lblActionCode.AutoSize = true;
            this.lblActionCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActionCode.Location = new System.Drawing.Point(3, 125);
            this.lblActionCode.Name = "lblActionCode";
            this.lblActionCode.Size = new System.Drawing.Size(144, 25);
            this.lblActionCode.TabIndex = 3;
            this.lblActionCode.Text = "Action Code:";
            this.lblActionCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtActionCode
            // 
            this.txtActionCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtActionCode.Location = new System.Drawing.Point(153, 128);
            this.txtActionCode.Name = "txtActionCode";
            this.txtActionCode.ReadOnly = true;
            this.txtActionCode.Size = new System.Drawing.Size(169, 20);
            this.txtActionCode.TabIndex = 19;
            // 
            // lblSequence
            // 
            this.lblSequence.AutoSize = true;
            this.lblSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSequence.Location = new System.Drawing.Point(3, 150);
            this.lblSequence.Name = "lblSequence";
            this.lblSequence.Size = new System.Drawing.Size(144, 25);
            this.lblSequence.TabIndex = 4;
            this.lblSequence.Text = "Sequence:";
            this.lblSequence.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSequence
            // 
            this.txtSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSequence.Location = new System.Drawing.Point(153, 153);
            this.txtSequence.Name = "txtSequence";
            this.txtSequence.Size = new System.Drawing.Size(169, 20);
            this.txtSequence.TabIndex = 20;
            this.txtSequence.TextChanged += new System.EventHandler(this.txtSequence_TextChanged);
            this.txtSequence.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSequence_KeyPress);
            // 
            // lblAssessorParcelNumber
            // 
            this.lblAssessorParcelNumber.AutoSize = true;
            this.lblAssessorParcelNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAssessorParcelNumber.Location = new System.Drawing.Point(3, 175);
            this.lblAssessorParcelNumber.Name = "lblAssessorParcelNumber";
            this.lblAssessorParcelNumber.Size = new System.Drawing.Size(144, 25);
            this.lblAssessorParcelNumber.TabIndex = 5;
            this.lblAssessorParcelNumber.Text = "Assessor Parcel Number:";
            this.lblAssessorParcelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAssessorParcelNumber
            // 
            this.txtAssessorParcelNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAssessorParcelNumber.Location = new System.Drawing.Point(153, 178);
            this.txtAssessorParcelNumber.Name = "txtAssessorParcelNumber";
            this.txtAssessorParcelNumber.Size = new System.Drawing.Size(169, 20);
            this.txtAssessorParcelNumber.TabIndex = 21;
            this.txtAssessorParcelNumber.TextChanged += new System.EventHandler(this.txtAssessorParcelNumber_TextChanged);
            // 
            // lblTransferTaxAmount
            // 
            this.lblTransferTaxAmount.AutoSize = true;
            this.lblTransferTaxAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransferTaxAmount.Location = new System.Drawing.Point(3, 200);
            this.lblTransferTaxAmount.Name = "lblTransferTaxAmount";
            this.lblTransferTaxAmount.Size = new System.Drawing.Size(144, 25);
            this.lblTransferTaxAmount.TabIndex = 6;
            this.lblTransferTaxAmount.Text = "Transfer Tax Amount:";
            this.lblTransferTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTransferTaxAmount
            // 
            this.txtTransferTaxAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTransferTaxAmount.Location = new System.Drawing.Point(153, 203);
            this.txtTransferTaxAmount.Name = "txtTransferTaxAmount";
            this.txtTransferTaxAmount.Size = new System.Drawing.Size(169, 20);
            this.txtTransferTaxAmount.TabIndex = 22;
            this.txtTransferTaxAmount.TextChanged += new System.EventHandler(this.txtTransferTaxAmount_TextChanged);
            this.txtTransferTaxAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTransferTaxAmount_KeyPress);
            this.txtTransferTaxAmount.Leave += new System.EventHandler(this.txtTransferTaxAmount_Leave);
            // 
            // lblSalesAmount
            // 
            this.lblSalesAmount.AutoSize = true;
            this.lblSalesAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSalesAmount.Location = new System.Drawing.Point(3, 225);
            this.lblSalesAmount.Name = "lblSalesAmount";
            this.lblSalesAmount.Size = new System.Drawing.Size(144, 30);
            this.lblSalesAmount.TabIndex = 7;
            this.lblSalesAmount.Text = "Sale Amount:";
            this.lblSalesAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSaleAmount
            // 
            this.txtSaleAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSaleAmount.Location = new System.Drawing.Point(153, 228);
            this.txtSaleAmount.Name = "txtSaleAmount";
            this.txtSaleAmount.Size = new System.Drawing.Size(169, 20);
            this.txtSaleAmount.TabIndex = 23;
            this.txtSaleAmount.TextChanged += new System.EventHandler(this.txtSaleAmount_TextChanged);
            this.txtSaleAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSaleAmount_KeyPress);
            this.txtSaleAmount.Leave += new System.EventHandler(this.txtSaleAmount_Leave);
            // 
            // lstTitles
            // 
            this.lstTitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTitles.FormattingEnabled = true;
            this.lstTitles.Location = new System.Drawing.Point(153, 258);
            this.lstTitles.Name = "lstTitles";
            this.lstTitles.Size = new System.Drawing.Size(169, 67);
            this.lstTitles.TabIndex = 36;
            // 
            // tblEditTitles
            // 
            this.tblEditTitles.ColumnCount = 2;
            this.tblEditTitles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblEditTitles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEditTitles.Controls.Add(this.lblTitles, 1, 0);
            this.tblEditTitles.Controls.Add(this.btnEditTitles, 0, 0);
            this.tblEditTitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEditTitles.Location = new System.Drawing.Point(3, 258);
            this.tblEditTitles.Name = "tblEditTitles";
            this.tblEditTitles.RowCount = 1;
            this.tblEditTitles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblEditTitles.Size = new System.Drawing.Size(144, 67);
            this.tblEditTitles.TabIndex = 32;
            // 
            // lblTitles
            // 
            this.lblTitles.AutoSize = true;
            this.lblTitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitles.Location = new System.Drawing.Point(83, 0);
            this.lblTitles.Name = "lblTitles";
            this.lblTitles.Size = new System.Drawing.Size(58, 67);
            this.lblTitles.TabIndex = 8;
            this.lblTitles.Text = "Titles:";
            this.lblTitles.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnEditTitles
            // 
            this.btnEditTitles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditTitles.Location = new System.Drawing.Point(3, 3);
            this.btnEditTitles.Name = "btnEditTitles";
            this.btnEditTitles.Size = new System.Drawing.Size(74, 61);
            this.btnEditTitles.TabIndex = 9;
            this.btnEditTitles.Text = "Edit";
            this.btnEditTitles.UseVisualStyleBackColor = true;
            this.btnEditTitles.Click += new System.EventHandler(this.btnEditTitles_Click);
            // 
            // tblEditCities
            // 
            this.tblEditCities.ColumnCount = 2;
            this.tblEditCities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblEditCities.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEditCities.Controls.Add(this.lblCities, 1, 0);
            this.tblEditCities.Controls.Add(this.btnEditCities, 0, 0);
            this.tblEditCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEditCities.Location = new System.Drawing.Point(3, 331);
            this.tblEditCities.Name = "tblEditCities";
            this.tblEditCities.RowCount = 1;
            this.tblEditCities.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEditCities.Size = new System.Drawing.Size(144, 51);
            this.tblEditCities.TabIndex = 33;
            // 
            // lblCities
            // 
            this.lblCities.AutoSize = true;
            this.lblCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCities.Location = new System.Drawing.Point(83, 0);
            this.lblCities.Name = "lblCities";
            this.lblCities.Size = new System.Drawing.Size(58, 51);
            this.lblCities.TabIndex = 9;
            this.lblCities.Text = "Cities";
            this.lblCities.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnEditCities
            // 
            this.btnEditCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditCities.Location = new System.Drawing.Point(3, 3);
            this.btnEditCities.Name = "btnEditCities";
            this.btnEditCities.Size = new System.Drawing.Size(74, 45);
            this.btnEditCities.TabIndex = 10;
            this.btnEditCities.Text = "Edit";
            this.btnEditCities.UseVisualStyleBackColor = true;
            this.btnEditCities.Click += new System.EventHandler(this.btnEditCities_Click);
            // 
            // lstCities
            // 
            this.lstCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCities.FormattingEnabled = true;
            this.lstCities.Location = new System.Drawing.Point(153, 331);
            this.lstCities.Name = "lstCities";
            this.lstCities.Size = new System.Drawing.Size(169, 51);
            this.lstCities.TabIndex = 37;
            // 
            // tblEditFees
            // 
            this.tblEditFees.ColumnCount = 2;
            this.tblEditFees.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblEditFees.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEditFees.Controls.Add(this.lblFees, 1, 0);
            this.tblEditFees.Controls.Add(this.btnAddFees, 0, 0);
            this.tblEditFees.Controls.Add(this.btnDeleteFee, 0, 1);
            this.tblEditFees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEditFees.Location = new System.Drawing.Point(3, 388);
            this.tblEditFees.Name = "tblEditFees";
            this.tblEditFees.RowCount = 2;
            this.tblEditFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblEditFees.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblEditFees.Size = new System.Drawing.Size(144, 63);
            this.tblEditFees.TabIndex = 34;
            // 
            // lblFees
            // 
            this.lblFees.AutoSize = true;
            this.lblFees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFees.Location = new System.Drawing.Point(83, 0);
            this.lblFees.Name = "lblFees";
            this.lblFees.Size = new System.Drawing.Size(58, 31);
            this.lblFees.TabIndex = 10;
            this.lblFees.Text = "Fees:";
            this.lblFees.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnAddFees
            // 
            this.btnAddFees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddFees.Location = new System.Drawing.Point(3, 3);
            this.btnAddFees.Name = "btnAddFees";
            this.btnAddFees.Size = new System.Drawing.Size(74, 25);
            this.btnAddFees.TabIndex = 11;
            this.btnAddFees.Text = "Add Fees";
            this.btnAddFees.UseVisualStyleBackColor = true;
            this.btnAddFees.Click += new System.EventHandler(this.btnAddFees_Click);
            // 
            // btnDeleteFee
            // 
            this.btnDeleteFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteFee.Location = new System.Drawing.Point(3, 34);
            this.btnDeleteFee.Name = "btnDeleteFee";
            this.btnDeleteFee.Size = new System.Drawing.Size(74, 26);
            this.btnDeleteFee.TabIndex = 12;
            this.btnDeleteFee.Text = "Delete";
            this.btnDeleteFee.UseVisualStyleBackColor = true;
            this.btnDeleteFee.Click += new System.EventHandler(this.btnDeleteFee_Click);
            // 
            // lstFees
            // 
            this.lstFees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFees.FormattingEnabled = true;
            this.lstFees.Location = new System.Drawing.Point(153, 388);
            this.lstFees.Name = "lstFees";
            this.lstFees.Size = new System.Drawing.Size(169, 63);
            this.lstFees.TabIndex = 38;
            this.lstFees.DoubleClick += new System.EventHandler(this.lstFees_DoubleClick);
            // 
            // tblEditNames
            // 
            this.tblEditNames.ColumnCount = 2;
            this.tblEditNames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblEditNames.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEditNames.Controls.Add(this.lblNames, 1, 0);
            this.tblEditNames.Controls.Add(this.btnAddNames, 0, 0);
            this.tblEditNames.Controls.Add(this.btnDeleteName, 0, 1);
            this.tblEditNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEditNames.Location = new System.Drawing.Point(3, 457);
            this.tblEditNames.Name = "tblEditNames";
            this.tblEditNames.RowCount = 2;
            this.tblEditNames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblEditNames.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblEditNames.Size = new System.Drawing.Size(144, 62);
            this.tblEditNames.TabIndex = 35;
            // 
            // lblNames
            // 
            this.lblNames.AutoSize = true;
            this.lblNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNames.Location = new System.Drawing.Point(83, 0);
            this.lblNames.Name = "lblNames";
            this.lblNames.Size = new System.Drawing.Size(58, 31);
            this.lblNames.TabIndex = 11;
            this.lblNames.Text = "Names:";
            this.lblNames.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnAddNames
            // 
            this.btnAddNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddNames.Location = new System.Drawing.Point(3, 3);
            this.btnAddNames.Name = "btnAddNames";
            this.btnAddNames.Size = new System.Drawing.Size(74, 25);
            this.btnAddNames.TabIndex = 12;
            this.btnAddNames.Text = "Add Names";
            this.btnAddNames.UseVisualStyleBackColor = true;
            this.btnAddNames.Click += new System.EventHandler(this.btnAddNames_Click);
            // 
            // btnDeleteName
            // 
            this.btnDeleteName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteName.Location = new System.Drawing.Point(3, 34);
            this.btnDeleteName.Name = "btnDeleteName";
            this.btnDeleteName.Size = new System.Drawing.Size(74, 25);
            this.btnDeleteName.TabIndex = 13;
            this.btnDeleteName.Text = "Delete";
            this.btnDeleteName.UseVisualStyleBackColor = true;
            this.btnDeleteName.Click += new System.EventHandler(this.btnDeleteName_Click);
            // 
            // lstNames
            // 
            this.lstNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNames.FormattingEnabled = true;
            this.lstNames.Location = new System.Drawing.Point(153, 457);
            this.lstNames.Name = "lstNames";
            this.lstNames.Size = new System.Drawing.Size(169, 62);
            this.lstNames.TabIndex = 39;
            this.lstNames.DoubleClick += new System.EventHandler(this.lstNames_DoubleClick);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Location = new System.Drawing.Point(3, 522);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(144, 27);
            this.lblVersion.TabIndex = 12;
            this.lblVersion.Text = "Version:";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtVersion
            // 
            this.txtVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVersion.Location = new System.Drawing.Point(153, 525);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(169, 20);
            this.txtVersion.TabIndex = 28;
            // 
            // lblCreateTimeStamp
            // 
            this.lblCreateTimeStamp.AutoSize = true;
            this.lblCreateTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreateTimeStamp.Location = new System.Drawing.Point(3, 549);
            this.lblCreateTimeStamp.Name = "lblCreateTimeStamp";
            this.lblCreateTimeStamp.Size = new System.Drawing.Size(144, 28);
            this.lblCreateTimeStamp.TabIndex = 13;
            this.lblCreateTimeStamp.Text = "Create Time Stamp:";
            this.lblCreateTimeStamp.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCreateTimeStamp
            // 
            this.txtCreateTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCreateTimeStamp.Location = new System.Drawing.Point(153, 552);
            this.txtCreateTimeStamp.Name = "txtCreateTimeStamp";
            this.txtCreateTimeStamp.ReadOnly = true;
            this.txtCreateTimeStamp.Size = new System.Drawing.Size(169, 20);
            this.txtCreateTimeStamp.TabIndex = 29;
            // 
            // lblEditTimeStamp
            // 
            this.lblEditTimeStamp.AutoSize = true;
            this.lblEditTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEditTimeStamp.Location = new System.Drawing.Point(3, 577);
            this.lblEditTimeStamp.Name = "lblEditTimeStamp";
            this.lblEditTimeStamp.Size = new System.Drawing.Size(144, 27);
            this.lblEditTimeStamp.TabIndex = 14;
            this.lblEditTimeStamp.Text = "Edit Time Stamp:";
            this.lblEditTimeStamp.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtEditTimeStamp
            // 
            this.txtEditTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditTimeStamp.Location = new System.Drawing.Point(153, 580);
            this.txtEditTimeStamp.Name = "txtEditTimeStamp";
            this.txtEditTimeStamp.ReadOnly = true;
            this.txtEditTimeStamp.Size = new System.Drawing.Size(169, 20);
            this.txtEditTimeStamp.TabIndex = 30;
            // 
            // tblEditMemos
            // 
            this.tblEditMemos.ColumnCount = 2;
            this.tblEditMemos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblEditMemos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEditMemos.Controls.Add(this.lblMemos, 1, 0);
            this.tblEditMemos.Controls.Add(this.btnAddMemo, 0, 0);
            this.tblEditMemos.Controls.Add(this.btnDeleteMemo, 0, 1);
            this.tblEditMemos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEditMemos.Location = new System.Drawing.Point(3, 607);
            this.tblEditMemos.Name = "tblEditMemos";
            this.tblEditMemos.RowCount = 3;
            this.tblEditMemos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblEditMemos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblEditMemos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEditMemos.Size = new System.Drawing.Size(144, 176);
            this.tblEditMemos.TabIndex = 42;
            // 
            // lblMemos
            // 
            this.lblMemos.AutoSize = true;
            this.lblMemos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMemos.Location = new System.Drawing.Point(83, 0);
            this.lblMemos.Name = "lblMemos";
            this.lblMemos.Size = new System.Drawing.Size(58, 40);
            this.lblMemos.TabIndex = 15;
            this.lblMemos.Text = "Memos:";
            this.lblMemos.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnAddMemo
            // 
            this.btnAddMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddMemo.Location = new System.Drawing.Point(3, 3);
            this.btnAddMemo.Name = "btnAddMemo";
            this.btnAddMemo.Size = new System.Drawing.Size(74, 34);
            this.btnAddMemo.TabIndex = 41;
            this.btnAddMemo.Text = "AddMemo";
            this.btnAddMemo.UseVisualStyleBackColor = true;
            this.btnAddMemo.Click += new System.EventHandler(this.btnAddMemo_Click);
            // 
            // btnDeleteMemo
            // 
            this.btnDeleteMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteMemo.Location = new System.Drawing.Point(3, 43);
            this.btnDeleteMemo.Name = "btnDeleteMemo";
            this.btnDeleteMemo.Size = new System.Drawing.Size(74, 34);
            this.btnDeleteMemo.TabIndex = 42;
            this.btnDeleteMemo.Text = "Delete";
            this.btnDeleteMemo.UseVisualStyleBackColor = true;
            this.btnDeleteMemo.Click += new System.EventHandler(this.btnDeleteMemo_Click);
            // 
            // lstMemos
            // 
            this.lstMemos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMemos.FormattingEnabled = true;
            this.lstMemos.Location = new System.Drawing.Point(153, 607);
            this.lstMemos.Name = "lstMemos";
            this.lstMemos.Size = new System.Drawing.Size(169, 176);
            this.lstMemos.TabIndex = 40;
            this.lstMemos.DoubleClick += new System.EventHandler(this.lstMemos_DoubleClick);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(325, 30);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Document Info";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DocumentInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblHeader);
            this.Name = "DocumentInfoControl";
            this.Size = new System.Drawing.Size(331, 822);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.tblTableLayout.ResumeLayout(false);
            this.tblTableLayout.PerformLayout();
            this.tblEditTitles.ResumeLayout(false);
            this.tblEditTitles.PerformLayout();
            this.tblEditCities.ResumeLayout(false);
            this.tblEditCities.PerformLayout();
            this.tblEditFees.ResumeLayout(false);
            this.tblEditFees.PerformLayout();
            this.tblEditNames.ResumeLayout(false);
            this.tblEditNames.PerformLayout();
            this.tblEditMemos.ResumeLayout(false);
            this.tblEditMemos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TableLayoutPanel tblTableLayout;
        private System.Windows.Forms.TableLayoutPanel tblEditNames;
        private System.Windows.Forms.Label lblNames;
        private System.Windows.Forms.Button btnAddNames;
        private System.Windows.Forms.TableLayoutPanel tblEditFees;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.Button btnAddFees;
        private System.Windows.Forms.TableLayoutPanel tblEditCities;
        private System.Windows.Forms.Label lblCities;
        private System.Windows.Forms.Button btnEditCities;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblRecordedTimeStamp;
        private System.Windows.Forms.Label lblStatusCode;
        private System.Windows.Forms.Label lblActionCode;
        private System.Windows.Forms.Label lblSequence;
        private System.Windows.Forms.Label lblAssessorParcelNumber;
        private System.Windows.Forms.Label lblTransferTaxAmount;
        private System.Windows.Forms.Label lblSalesAmount;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCreateTimeStamp;
        private System.Windows.Forms.Label lblEditTimeStamp;
        private System.Windows.Forms.Label lblMemos;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtStatusCode;
        private System.Windows.Forms.TextBox txtActionCode;
        private System.Windows.Forms.TextBox txtSequence;
        private System.Windows.Forms.TextBox txtAssessorParcelNumber;
        private System.Windows.Forms.TextBox txtTransferTaxAmount;
        private System.Windows.Forms.TextBox txtSaleAmount;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtCreateTimeStamp;
        private System.Windows.Forms.TextBox txtEditTimeStamp;
        private System.Windows.Forms.TableLayoutPanel tblEditTitles;
        private System.Windows.Forms.Label lblTitles;
        private System.Windows.Forms.Button btnEditTitles;
        private System.Windows.Forms.ListBox lstTitles;
        private System.Windows.Forms.ListBox lstCities;
        private System.Windows.Forms.ListBox lstFees;
        private System.Windows.Forms.ListBox lstNames;
        private System.Windows.Forms.ListBox lstMemos;
        private System.Windows.Forms.Button btnAddMemo;
        private System.Windows.Forms.TableLayoutPanel tblEditMemos;
        private System.Windows.Forms.Button btnDeleteName;
        private System.Windows.Forms.Button btnDeleteFee;
        private System.Windows.Forms.Button btnDeleteMemo;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.DateTimePicker dtpRecordedTS;
        private System.Windows.Forms.TextBox txtExternalID;
        private System.Windows.Forms.Label lblExternalID;
    }
}

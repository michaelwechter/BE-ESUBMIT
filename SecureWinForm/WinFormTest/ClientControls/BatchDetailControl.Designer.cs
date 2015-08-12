namespace WinFormTest.ClientControls
{
    partial class BatchDetailControl
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
            this.pnl_TableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.txtSubmissionID = new System.Windows.Forms.TextBox();
            this.txtBatchID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtTransactionNumber = new System.Windows.Forms.TextBox();
            this.cmbCounty = new System.Windows.Forms.ComboBox();
            this.cmbRequestingParty = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCounty = new System.Windows.Forms.Label();
            this.lblProcessQueue = new System.Windows.Forms.Label();
            this.lblTransactionNumber = new System.Windows.Forms.Label();
            this.lblStatusCode = new System.Windows.Forms.Label();
            this.lblCheckedOutBy = new System.Windows.Forms.Label();
            this.lblCheckedOutTimeStamp = new System.Windows.Forms.Label();
            this.lblSubmittingParty = new System.Windows.Forms.Label();
            this.lblRequestingParty = new System.Windows.Forms.Label();
            this.lblIsConcurrent = new System.Windows.Forms.Label();
            this.lblCreateTimeStamp = new System.Windows.Forms.Label();
            this.lblEditTimeStamp = new System.Windows.Forms.Label();
            this.chkIsConcurrent = new System.Windows.Forms.CheckBox();
            this.txtCheckedOutTS = new System.Windows.Forms.TextBox();
            this.txtCreateTS = new System.Windows.Forms.TextBox();
            this.txtEditTS = new System.Windows.Forms.TextBox();
            this.txtStatusCode = new System.Windows.Forms.TextBox();
            this.lstMemos = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtProcessQueue_Open = new System.Windows.Forms.TextBox();
            this.txtProcessQueue_Close = new System.Windows.Forms.TextBox();
            this.txtProcessQueue_Description = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbProcessQueue = new System.Windows.Forms.ComboBox();
            this.txtCheckedOutBy = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblMemos = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSubmittingParty_PartyType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSubmittingParty_Name = new System.Windows.Forms.TextBox();
            this.pnl_Header = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Header = new System.Windows.Forms.Label();
            this.pnl_TableLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnl_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_TableLayout
            // 
            this.pnl_TableLayout.ColumnCount = 2;
            this.pnl_TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.pnl_TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_TableLayout.Controls.Add(this.txtSubmissionID, 1, 1);
            this.pnl_TableLayout.Controls.Add(this.txtBatchID, 1, 0);
            this.pnl_TableLayout.Controls.Add(this.txtName, 1, 2);
            this.pnl_TableLayout.Controls.Add(this.txtTransactionNumber, 1, 5);
            this.pnl_TableLayout.Controls.Add(this.cmbCounty, 1, 3);
            this.pnl_TableLayout.Controls.Add(this.cmbRequestingParty, 1, 11);
            this.pnl_TableLayout.Controls.Add(this.lblName, 0, 2);
            this.pnl_TableLayout.Controls.Add(this.lblCounty, 0, 3);
            this.pnl_TableLayout.Controls.Add(this.lblProcessQueue, 0, 4);
            this.pnl_TableLayout.Controls.Add(this.lblTransactionNumber, 0, 5);
            this.pnl_TableLayout.Controls.Add(this.lblStatusCode, 0, 6);
            this.pnl_TableLayout.Controls.Add(this.lblCheckedOutBy, 0, 7);
            this.pnl_TableLayout.Controls.Add(this.lblCheckedOutTimeStamp, 0, 8);
            this.pnl_TableLayout.Controls.Add(this.lblSubmittingParty, 0, 9);
            this.pnl_TableLayout.Controls.Add(this.lblRequestingParty, 0, 11);
            this.pnl_TableLayout.Controls.Add(this.lblIsConcurrent, 0, 12);
            this.pnl_TableLayout.Controls.Add(this.lblCreateTimeStamp, 0, 13);
            this.pnl_TableLayout.Controls.Add(this.lblEditTimeStamp, 0, 14);
            this.pnl_TableLayout.Controls.Add(this.chkIsConcurrent, 1, 12);
            this.pnl_TableLayout.Controls.Add(this.txtCheckedOutTS, 1, 8);
            this.pnl_TableLayout.Controls.Add(this.txtCreateTS, 1, 13);
            this.pnl_TableLayout.Controls.Add(this.txtEditTS, 1, 14);
            this.pnl_TableLayout.Controls.Add(this.txtStatusCode, 1, 6);
            this.pnl_TableLayout.Controls.Add(this.lstMemos, 1, 15);
            this.pnl_TableLayout.Controls.Add(this.label1, 0, 1);
            this.pnl_TableLayout.Controls.Add(this.label2, 0, 0);
            this.pnl_TableLayout.Controls.Add(this.panel1, 1, 4);
            this.pnl_TableLayout.Controls.Add(this.txtCheckedOutBy, 1, 7);
            this.pnl_TableLayout.Controls.Add(this.tableLayoutPanel1, 0, 15);
            this.pnl_TableLayout.Controls.Add(this.tableLayoutPanel2, 1, 10);
            this.pnl_TableLayout.Controls.Add(this.txtSubmittingParty_Name, 1, 9);
            this.pnl_TableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_TableLayout.Location = new System.Drawing.Point(3, 33);
            this.pnl_TableLayout.Name = "pnl_TableLayout";
            this.pnl_TableLayout.RowCount = 16;
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.pnl_TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_TableLayout.Size = new System.Drawing.Size(365, 489);
            this.pnl_TableLayout.TabIndex = 0;
            // 
            // txtSubmissionID
            // 
            this.txtSubmissionID.Location = new System.Drawing.Point(153, 28);
            this.txtSubmissionID.Name = "txtSubmissionID";
            this.txtSubmissionID.ReadOnly = true;
            this.txtSubmissionID.Size = new System.Drawing.Size(209, 20);
            this.txtSubmissionID.TabIndex = 47;
            // 
            // txtBatchID
            // 
            this.txtBatchID.Location = new System.Drawing.Point(153, 3);
            this.txtBatchID.Name = "txtBatchID";
            this.txtBatchID.ReadOnly = true;
            this.txtBatchID.Size = new System.Drawing.Size(209, 20);
            this.txtBatchID.TabIndex = 46;
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(153, 53);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(209, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtTransactionNumber
            // 
            this.txtTransactionNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTransactionNumber.Location = new System.Drawing.Point(153, 183);
            this.txtTransactionNumber.Name = "txtTransactionNumber";
            this.txtTransactionNumber.ReadOnly = true;
            this.txtTransactionNumber.Size = new System.Drawing.Size(209, 20);
            this.txtTransactionNumber.TabIndex = 3;
            this.txtTransactionNumber.TextChanged += new System.EventHandler(this.txtTransactionNumber_TextChanged);
            // 
            // cmbCounty
            // 
            this.cmbCounty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCounty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCounty.FormattingEnabled = true;
            this.cmbCounty.Location = new System.Drawing.Point(153, 78);
            this.cmbCounty.Name = "cmbCounty";
            this.cmbCounty.Size = new System.Drawing.Size(209, 21);
            this.cmbCounty.TabIndex = 5;
            this.cmbCounty.SelectedIndexChanged += new System.EventHandler(this.cmbCounty_SelectedIndexChanged);
            // 
            // cmbRequestingParty
            // 
            this.cmbRequestingParty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbRequestingParty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRequestingParty.FormattingEnabled = true;
            this.cmbRequestingParty.Location = new System.Drawing.Point(153, 333);
            this.cmbRequestingParty.Name = "cmbRequestingParty";
            this.cmbRequestingParty.Size = new System.Drawing.Size(209, 21);
            this.cmbRequestingParty.TabIndex = 10;
            this.cmbRequestingParty.SelectedIndexChanged += new System.EventHandler(this.cmbRequestingParty_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(3, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(144, 25);
            this.lblName.TabIndex = 17;
            this.lblName.Text = "Name:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCounty
            // 
            this.lblCounty.AutoSize = true;
            this.lblCounty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCounty.Location = new System.Drawing.Point(3, 75);
            this.lblCounty.Name = "lblCounty";
            this.lblCounty.Size = new System.Drawing.Size(144, 25);
            this.lblCounty.TabIndex = 18;
            this.lblCounty.Text = "County:";
            this.lblCounty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProcessQueue
            // 
            this.lblProcessQueue.AutoSize = true;
            this.lblProcessQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProcessQueue.Location = new System.Drawing.Point(3, 100);
            this.lblProcessQueue.Name = "lblProcessQueue";
            this.lblProcessQueue.Size = new System.Drawing.Size(144, 80);
            this.lblProcessQueue.TabIndex = 19;
            this.lblProcessQueue.Text = "Process Queue:";
            this.lblProcessQueue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTransactionNumber
            // 
            this.lblTransactionNumber.AutoSize = true;
            this.lblTransactionNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransactionNumber.Location = new System.Drawing.Point(3, 180);
            this.lblTransactionNumber.Name = "lblTransactionNumber";
            this.lblTransactionNumber.Size = new System.Drawing.Size(144, 25);
            this.lblTransactionNumber.TabIndex = 20;
            this.lblTransactionNumber.Text = "Transaction Number:";
            this.lblTransactionNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatusCode
            // 
            this.lblStatusCode.AutoSize = true;
            this.lblStatusCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusCode.Location = new System.Drawing.Point(3, 205);
            this.lblStatusCode.Name = "lblStatusCode";
            this.lblStatusCode.Size = new System.Drawing.Size(144, 25);
            this.lblStatusCode.TabIndex = 21;
            this.lblStatusCode.Text = "Status Code:";
            this.lblStatusCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCheckedOutBy
            // 
            this.lblCheckedOutBy.AutoSize = true;
            this.lblCheckedOutBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCheckedOutBy.Location = new System.Drawing.Point(3, 230);
            this.lblCheckedOutBy.Name = "lblCheckedOutBy";
            this.lblCheckedOutBy.Size = new System.Drawing.Size(144, 25);
            this.lblCheckedOutBy.TabIndex = 22;
            this.lblCheckedOutBy.Text = "Checked Out By:";
            this.lblCheckedOutBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCheckedOutTimeStamp
            // 
            this.lblCheckedOutTimeStamp.AutoSize = true;
            this.lblCheckedOutTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCheckedOutTimeStamp.Location = new System.Drawing.Point(3, 255);
            this.lblCheckedOutTimeStamp.Name = "lblCheckedOutTimeStamp";
            this.lblCheckedOutTimeStamp.Size = new System.Drawing.Size(144, 25);
            this.lblCheckedOutTimeStamp.TabIndex = 23;
            this.lblCheckedOutTimeStamp.Text = "Checked Out Time Stamp:";
            this.lblCheckedOutTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubmittingParty
            // 
            this.lblSubmittingParty.AutoSize = true;
            this.lblSubmittingParty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubmittingParty.Location = new System.Drawing.Point(3, 280);
            this.lblSubmittingParty.Name = "lblSubmittingParty";
            this.lblSubmittingParty.Size = new System.Drawing.Size(144, 25);
            this.lblSubmittingParty.TabIndex = 24;
            this.lblSubmittingParty.Text = "Submitting Party:";
            this.lblSubmittingParty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRequestingParty
            // 
            this.lblRequestingParty.AutoSize = true;
            this.lblRequestingParty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequestingParty.Location = new System.Drawing.Point(3, 330);
            this.lblRequestingParty.Name = "lblRequestingParty";
            this.lblRequestingParty.Size = new System.Drawing.Size(144, 25);
            this.lblRequestingParty.TabIndex = 25;
            this.lblRequestingParty.Text = "Requesting Party:";
            this.lblRequestingParty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIsConcurrent
            // 
            this.lblIsConcurrent.AutoSize = true;
            this.lblIsConcurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIsConcurrent.Location = new System.Drawing.Point(3, 355);
            this.lblIsConcurrent.Name = "lblIsConcurrent";
            this.lblIsConcurrent.Size = new System.Drawing.Size(144, 25);
            this.lblIsConcurrent.TabIndex = 26;
            this.lblIsConcurrent.Text = "Is Concurrent:";
            this.lblIsConcurrent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateTimeStamp
            // 
            this.lblCreateTimeStamp.AutoSize = true;
            this.lblCreateTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreateTimeStamp.Location = new System.Drawing.Point(3, 380);
            this.lblCreateTimeStamp.Name = "lblCreateTimeStamp";
            this.lblCreateTimeStamp.Size = new System.Drawing.Size(144, 25);
            this.lblCreateTimeStamp.TabIndex = 27;
            this.lblCreateTimeStamp.Text = "Create Time Stamp:";
            this.lblCreateTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEditTimeStamp
            // 
            this.lblEditTimeStamp.AutoSize = true;
            this.lblEditTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEditTimeStamp.Location = new System.Drawing.Point(3, 405);
            this.lblEditTimeStamp.Name = "lblEditTimeStamp";
            this.lblEditTimeStamp.Size = new System.Drawing.Size(144, 25);
            this.lblEditTimeStamp.TabIndex = 28;
            this.lblEditTimeStamp.Text = "Edit Time Stamp:";
            this.lblEditTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkIsConcurrent
            // 
            this.chkIsConcurrent.AutoSize = true;
            this.chkIsConcurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkIsConcurrent.Location = new System.Drawing.Point(153, 358);
            this.chkIsConcurrent.Name = "chkIsConcurrent";
            this.chkIsConcurrent.Size = new System.Drawing.Size(209, 19);
            this.chkIsConcurrent.TabIndex = 33;
            this.chkIsConcurrent.UseVisualStyleBackColor = true;
            this.chkIsConcurrent.CheckedChanged += new System.EventHandler(this.chkIsConcurrent_CheckedChanged);
            // 
            // txtCheckedOutTS
            // 
            this.txtCheckedOutTS.Location = new System.Drawing.Point(153, 258);
            this.txtCheckedOutTS.Name = "txtCheckedOutTS";
            this.txtCheckedOutTS.ReadOnly = true;
            this.txtCheckedOutTS.Size = new System.Drawing.Size(209, 20);
            this.txtCheckedOutTS.TabIndex = 37;
            // 
            // txtCreateTS
            // 
            this.txtCreateTS.Location = new System.Drawing.Point(153, 383);
            this.txtCreateTS.Name = "txtCreateTS";
            this.txtCreateTS.ReadOnly = true;
            this.txtCreateTS.Size = new System.Drawing.Size(209, 20);
            this.txtCreateTS.TabIndex = 38;
            // 
            // txtEditTS
            // 
            this.txtEditTS.Location = new System.Drawing.Point(153, 408);
            this.txtEditTS.Name = "txtEditTS";
            this.txtEditTS.ReadOnly = true;
            this.txtEditTS.Size = new System.Drawing.Size(209, 20);
            this.txtEditTS.TabIndex = 39;
            // 
            // txtStatusCode
            // 
            this.txtStatusCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatusCode.Location = new System.Drawing.Point(153, 208);
            this.txtStatusCode.Name = "txtStatusCode";
            this.txtStatusCode.ReadOnly = true;
            this.txtStatusCode.Size = new System.Drawing.Size(209, 20);
            this.txtStatusCode.TabIndex = 40;
            // 
            // lstMemos
            // 
            this.lstMemos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMemos.FormattingEnabled = true;
            this.lstMemos.Location = new System.Drawing.Point(153, 433);
            this.lstMemos.Name = "lstMemos";
            this.lstMemos.Size = new System.Drawing.Size(209, 53);
            this.lstMemos.TabIndex = 41;
            this.lstMemos.DoubleClick += new System.EventHandler(this.lstMemos_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 25);
            this.label1.TabIndex = 44;
            this.label1.Text = "Submission ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 25);
            this.label2.TabIndex = 45;
            this.label2.Text = "ID:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtProcessQueue_Open);
            this.panel1.Controls.Add(this.txtProcessQueue_Close);
            this.panel1.Controls.Add(this.txtProcessQueue_Description);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbProcessQueue);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(153, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 74);
            this.panel1.TabIndex = 48;
            // 
            // txtProcessQueue_Open
            // 
            this.txtProcessQueue_Open.Location = new System.Drawing.Point(39, 49);
            this.txtProcessQueue_Open.Name = "txtProcessQueue_Open";
            this.txtProcessQueue_Open.ReadOnly = true;
            this.txtProcessQueue_Open.Size = new System.Drawing.Size(54, 20);
            this.txtProcessQueue_Open.TabIndex = 13;
            // 
            // txtProcessQueue_Close
            // 
            this.txtProcessQueue_Close.Location = new System.Drawing.Point(141, 49);
            this.txtProcessQueue_Close.Name = "txtProcessQueue_Close";
            this.txtProcessQueue_Close.ReadOnly = true;
            this.txtProcessQueue_Close.Size = new System.Drawing.Size(68, 20);
            this.txtProcessQueue_Close.TabIndex = 12;
            // 
            // txtProcessQueue_Description
            // 
            this.txtProcessQueue_Description.Location = new System.Drawing.Point(66, 24);
            this.txtProcessQueue_Description.Name = "txtProcessQueue_Description";
            this.txtProcessQueue_Description.ReadOnly = true;
            this.txtProcessQueue_Description.Size = new System.Drawing.Size(143, 20);
            this.txtProcessQueue_Description.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(99, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Close:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-3, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Open:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbProcessQueue
            // 
            this.cmbProcessQueue.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbProcessQueue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcessQueue.FormattingEnabled = true;
            this.cmbProcessQueue.Location = new System.Drawing.Point(0, 0);
            this.cmbProcessQueue.Name = "cmbProcessQueue";
            this.cmbProcessQueue.Size = new System.Drawing.Size(209, 21);
            this.cmbProcessQueue.TabIndex = 7;
            this.cmbProcessQueue.SelectedIndexChanged += new System.EventHandler(this.cmbProcessQueue_SelectedIndexChanged);
            // 
            // txtCheckedOutBy
            // 
            this.txtCheckedOutBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCheckedOutBy.Location = new System.Drawing.Point(153, 233);
            this.txtCheckedOutBy.Name = "txtCheckedOutBy";
            this.txtCheckedOutBy.ReadOnly = true;
            this.txtCheckedOutBy.Size = new System.Drawing.Size(209, 20);
            this.txtCheckedOutBy.TabIndex = 49;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMemos, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 430);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(150, 59);
            this.tableLayoutPanel1.TabIndex = 53;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(3, 32);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(144, 24);
            this.btnAdd.TabIndex = 54;
            this.btnAdd.Text = "Add Memo";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblMemos
            // 
            this.lblMemos.AutoSize = true;
            this.lblMemos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMemos.Location = new System.Drawing.Point(3, 0);
            this.lblMemos.Name = "lblMemos";
            this.lblMemos.Size = new System.Drawing.Size(144, 29);
            this.lblMemos.TabIndex = 53;
            this.lblMemos.Text = "Memos:";
            this.lblMemos.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Controls.Add(this.txtSubmittingParty_PartyType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(150, 305);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(215, 25);
            this.tableLayoutPanel2.TabIndex = 54;
            // 
            // txtSubmittingParty_PartyType
            // 
            this.txtSubmittingParty_PartyType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubmittingParty_PartyType.Location = new System.Drawing.Point(89, 3);
            this.txtSubmittingParty_PartyType.Name = "txtSubmittingParty_PartyType";
            this.txtSubmittingParty_PartyType.ReadOnly = true;
            this.txtSubmittingParty_PartyType.Size = new System.Drawing.Size(123, 20);
            this.txtSubmittingParty_PartyType.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 26);
            this.label6.TabIndex = 1;
            this.label6.Text = "Party Type:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubmittingParty_Name
            // 
            this.txtSubmittingParty_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubmittingParty_Name.Location = new System.Drawing.Point(153, 283);
            this.txtSubmittingParty_Name.Name = "txtSubmittingParty_Name";
            this.txtSubmittingParty_Name.ReadOnly = true;
            this.txtSubmittingParty_Name.Size = new System.Drawing.Size(209, 20);
            this.txtSubmittingParty_Name.TabIndex = 55;
            // 
            // pnl_Header
            // 
            this.pnl_Header.ColumnCount = 1;
            this.pnl_Header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 371F));
            this.pnl_Header.Controls.Add(this.pnl_TableLayout, 0, 1);
            this.pnl_Header.Controls.Add(this.lbl_Header, 0, 0);
            this.pnl_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Header.Location = new System.Drawing.Point(0, 0);
            this.pnl_Header.Name = "pnl_Header";
            this.pnl_Header.RowCount = 2;
            this.pnl_Header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.pnl_Header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.pnl_Header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnl_Header.Size = new System.Drawing.Size(371, 525);
            this.pnl_Header.TabIndex = 1;
            // 
            // lbl_Header
            // 
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.Location = new System.Drawing.Point(3, 0);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(365, 30);
            this.lbl_Header.TabIndex = 1;
            this.lbl_Header.Text = "Batch Detail";
            this.lbl_Header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BatchDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_Header);
            this.Name = "BatchDetailControl";
            this.Size = new System.Drawing.Size(371, 525);
            this.pnl_TableLayout.ResumeLayout(false);
            this.pnl_TableLayout.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.pnl_Header.ResumeLayout(false);
            this.pnl_Header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnl_TableLayout;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtTransactionNumber;
        private System.Windows.Forms.ComboBox cmbCounty;
        private System.Windows.Forms.TableLayoutPanel pnl_Header;
        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.ComboBox cmbRequestingParty;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCounty;
        private System.Windows.Forms.Label lblProcessQueue;
        private System.Windows.Forms.Label lblTransactionNumber;
        private System.Windows.Forms.Label lblStatusCode;
        private System.Windows.Forms.Label lblCheckedOutBy;
        private System.Windows.Forms.Label lblCheckedOutTimeStamp;
        private System.Windows.Forms.Label lblSubmittingParty;
        private System.Windows.Forms.Label lblRequestingParty;
        private System.Windows.Forms.Label lblIsConcurrent;
        private System.Windows.Forms.Label lblCreateTimeStamp;
        private System.Windows.Forms.Label lblEditTimeStamp;
        private System.Windows.Forms.CheckBox chkIsConcurrent;
        private System.Windows.Forms.TextBox txtCheckedOutTS;
        private System.Windows.Forms.TextBox txtCreateTS;
        private System.Windows.Forms.TextBox txtEditTS;
        private System.Windows.Forms.TextBox txtStatusCode;
        private System.Windows.Forms.ListBox lstMemos;
        private System.Windows.Forms.TextBox txtSubmissionID;
        private System.Windows.Forms.TextBox txtBatchID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbProcessQueue;
        private System.Windows.Forms.TextBox txtProcessQueue_Open;
        private System.Windows.Forms.TextBox txtProcessQueue_Close;
        private System.Windows.Forms.TextBox txtProcessQueue_Description;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCheckedOutBy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblMemos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtSubmittingParty_PartyType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSubmittingParty_Name;

    }
}

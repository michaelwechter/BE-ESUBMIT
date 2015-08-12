namespace WinFormTest
{
    partial class TestForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpCounty = new System.Windows.Forms.TabPage();
            this.countyPanel1 = new WinFormTest.CountyPanel();
            this.tpSubmitter = new System.Windows.Forms.TabPage();
            this.submitterPanel1 = new WinFormTest.SubmitterPanel();
            this.tpHTTP = new System.Windows.Forms.TabPage();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.urlLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SessionTokenLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusMediaType = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.tsmAuthentication = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.citiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processQueuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readByDocumentIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readByBatchIDSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readByCommunicationIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readByBatchIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.readByUserNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readByUserIDStatusCodeDateRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readByStatusCodeDateRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overrideCheckOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadCompleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestingPartyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClearSendHTTP = new System.Windows.Forms.Button();
            this.txtSubmitHTTP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClearReceivedHTTP = new System.Windows.Forms.Button();
            this.txtReceivedHTTP = new System.Windows.Forms.TextBox();
            this.tabMain.SuspendLayout();
            this.tpCounty.SuspendLayout();
            this.tpSubmitter.SuspendLayout();
            this.tpHTTP.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tpCounty);
            this.tabMain.Controls.Add(this.tpSubmitter);
            this.tabMain.Controls.Add(this.tpHTTP);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(2005, 1054);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            this.tabMain.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabMain_Selecting);
            this.tabMain.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabMain_Deselecting);
            // 
            // tpCounty
            // 
            this.tpCounty.Controls.Add(this.countyPanel1);
            this.tpCounty.Location = new System.Drawing.Point(4, 22);
            this.tpCounty.Name = "tpCounty";
            this.tpCounty.Padding = new System.Windows.Forms.Padding(3);
            this.tpCounty.Size = new System.Drawing.Size(1997, 1028);
            this.tpCounty.TabIndex = 3;
            this.tpCounty.Text = "County";
            this.tpCounty.UseVisualStyleBackColor = true;
            // 
            // countyPanel1
            // 
            this.countyPanel1.AutoScroll = true;
            this.countyPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.countyPanel1.Location = new System.Drawing.Point(3, 3);
            this.countyPanel1.Name = "countyPanel1";
            this.countyPanel1.Size = new System.Drawing.Size(1991, 1022);
            this.countyPanel1.TabIndex = 0;
          // ' this.countyPanel1.Load += new System.EventHandler(this.countyPanel1_Load);
            // 
            // tpSubmitter
            // 
            this.tpSubmitter.Controls.Add(this.submitterPanel1);
            this.tpSubmitter.Location = new System.Drawing.Point(4, 22);
            this.tpSubmitter.Name = "tpSubmitter";
            this.tpSubmitter.Padding = new System.Windows.Forms.Padding(3);
            this.tpSubmitter.Size = new System.Drawing.Size(1997, 1028);
            this.tpSubmitter.TabIndex = 4;
            this.tpSubmitter.Text = "Submitter";
            this.tpSubmitter.UseVisualStyleBackColor = true;
            // 
            // submitterPanel1
            // 
            this.submitterPanel1.AutoScroll = true;
            this.submitterPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.submitterPanel1.Location = new System.Drawing.Point(3, 3);
            this.submitterPanel1.Name = "submitterPanel1";
            this.submitterPanel1.Size = new System.Drawing.Size(1991, 1022);
            this.submitterPanel1.TabIndex = 0;
           // this.submitterPanel1.Load += new System.EventHandler(this.submitterPanel1_Load);
            // 
            // tpHTTP
            // 
            this.tpHTTP.Controls.Add(this.statusStrip2);
            this.tpHTTP.Controls.Add(this.statusStrip1);
            this.tpHTTP.Controls.Add(this.menuStripMain);
            this.tpHTTP.Controls.Add(this.splitContainer1);
            this.tpHTTP.Location = new System.Drawing.Point(4, 22);
            this.tpHTTP.Name = "tpHTTP";
            this.tpHTTP.Padding = new System.Windows.Forms.Padding(3);
            this.tpHTTP.Size = new System.Drawing.Size(1997, 1043);
            this.tpHTTP.TabIndex = 1;
            this.tpHTTP.Tag = "HTTP";
            this.tpHTTP.Text = "Raw HTTP";
            this.tpHTTP.UseVisualStyleBackColor = true;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.urlLabel});
            this.statusStrip2.Location = new System.Drawing.Point(3, 945);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1991, 22);
            this.statusStrip2.TabIndex = 5;
            this.statusStrip2.Text = "statusStrip2";
            this.statusStrip2.Visible = false;
            // 
            // urlLabel
            // 
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(56, 17);
            this.urlLabel.Text = "URLLabel";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SessionTokenLabel,
            this.toolStripStatusLabel1,
            this.lblStatusMediaType});
            this.statusStrip1.Location = new System.Drawing.Point(3, 967);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1991, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // SessionTokenLabel
            // 
            this.SessionTokenLabel.Name = "SessionTokenLabel";
            this.SessionTokenLabel.Size = new System.Drawing.Size(118, 17);
            this.SessionTokenLabel.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // lblStatusMediaType
            // 
            this.lblStatusMediaType.Name = "lblStatusMediaType";
            this.lblStatusMediaType.Size = new System.Drawing.Size(98, 17);
            this.lblStatusMediaType.Text = "MediaType:[RCE]";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAuthentication,
            this.indexToolStripMenuItem,
            this.documentToolStripMenuItem,
            this.batchToolStripMenuItem,
            this.userToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(3, 3);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1991, 24);
            this.menuStripMain.TabIndex = 3;
            this.menuStripMain.Text = "menuStrip1";
            this.menuStripMain.Visible = false;
            // 
            // tsmAuthentication
            // 
            this.tsmAuthentication.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pingToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.logoutToolStripMenuItem,
            this.loginToolStripMenuItem});
            this.tsmAuthentication.Name = "tsmAuthentication";
            this.tsmAuthentication.Size = new System.Drawing.Size(98, 20);
            this.tsmAuthentication.Tag = "SessionTokenID";
            this.tsmAuthentication.Text = "Authentication";
            // 
            // pingToolStripMenuItem
            // 
            this.pingToolStripMenuItem.Name = "pingToolStripMenuItem";
            this.pingToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.pingToolStripMenuItem.Text = "Ping";
            this.pingToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.changePasswordToolStripMenuItem.Text = "ChangePassword";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.HTTPPUT_ToolStripMenuItem_Click);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.titlesToolStripMenuItem,
            this.citiesToolStripMenuItem,
            this.processQueuesToolStripMenuItem,
            this.indexOptionsToolStripMenuItem,
            this.countyToolStripMenuItem});
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.indexToolStripMenuItem.Tag = "SessionTokenID";
            this.indexToolStripMenuItem.Text = "Index";
            // 
            // titlesToolStripMenuItem
            // 
            this.titlesToolStripMenuItem.Name = "titlesToolStripMenuItem";
            this.titlesToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.titlesToolStripMenuItem.Text = "Title";
            this.titlesToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // citiesToolStripMenuItem
            // 
            this.citiesToolStripMenuItem.Name = "citiesToolStripMenuItem";
            this.citiesToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.citiesToolStripMenuItem.Text = "City";
            this.citiesToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // processQueuesToolStripMenuItem
            // 
            this.processQueuesToolStripMenuItem.Name = "processQueuesToolStripMenuItem";
            this.processQueuesToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.processQueuesToolStripMenuItem.Text = "ProcessQueue";
            this.processQueuesToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // indexOptionsToolStripMenuItem
            // 
            this.indexOptionsToolStripMenuItem.Name = "indexOptionsToolStripMenuItem";
            this.indexOptionsToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.indexOptionsToolStripMenuItem.Text = "IndexOption";
            this.indexOptionsToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // countyToolStripMenuItem
            // 
            this.countyToolStripMenuItem.Name = "countyToolStripMenuItem";
            this.countyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.countyToolStripMenuItem.Text = "County";
            this.countyToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // documentToolStripMenuItem
            // 
            this.documentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadToolStripMenuItem,
            this.readByDocumentIDToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.readByBatchIDSequenceToolStripMenuItem});
            this.documentToolStripMenuItem.Name = "documentToolStripMenuItem";
            this.documentToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.documentToolStripMenuItem.Tag = "SessionTokenID";
            this.documentToolStripMenuItem.Text = "Document";
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.uploadToolStripMenuItem.Tag = "DocumentXML";
            this.uploadToolStripMenuItem.Text = "Upload";
            this.uploadToolStripMenuItem.Click += new System.EventHandler(this.HTTPPUT_ToolStripMenuItem_Click);
            // 
            // readByDocumentIDToolStripMenuItem
            // 
            this.readByDocumentIDToolStripMenuItem.Name = "readByDocumentIDToolStripMenuItem";
            this.readByDocumentIDToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.readByDocumentIDToolStripMenuItem.Tag = "DocID";
            this.readByDocumentIDToolStripMenuItem.Text = "ReadByDocumentID";
            this.readByDocumentIDToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.downloadToolStripMenuItem.Tag = "DocID";
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // readByBatchIDSequenceToolStripMenuItem
            // 
            this.readByBatchIDSequenceToolStripMenuItem.Name = "readByBatchIDSequenceToolStripMenuItem";
            this.readByBatchIDSequenceToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.readByBatchIDSequenceToolStripMenuItem.Text = "ReadByBatchIDSequence";
            this.readByBatchIDSequenceToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // batchToolStripMenuItem
            // 
            this.batchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readByCommunicationIDToolStripMenuItem,
            this.readByBatchIDToolStripMenuItem,
            this.downloadToolStripMenuItem1,
            this.readByUserNameToolStripMenuItem,
            this.readByUserIDStatusCodeDateRangeToolStripMenuItem,
            this.readByStatusCodeDateRangeToolStripMenuItem,
            this.createToolStripMenuItem,
            this.uploadToolStripMenuItem1,
            this.deleteToolStripMenuItem,
            this.checkInToolStripMenuItem,
            this.checkOutToolStripMenuItem,
            this.overrideCheckOutToolStripMenuItem,
            this.uploadCompleteToolStripMenuItem});
            this.batchToolStripMenuItem.Name = "batchToolStripMenuItem";
            this.batchToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.batchToolStripMenuItem.Tag = "SessionTokenID";
            this.batchToolStripMenuItem.Text = "Batch";
            // 
            // readByCommunicationIDToolStripMenuItem
            // 
            this.readByCommunicationIDToolStripMenuItem.Name = "readByCommunicationIDToolStripMenuItem";
            this.readByCommunicationIDToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.readByCommunicationIDToolStripMenuItem.Tag = "SubmissionID";
            this.readByCommunicationIDToolStripMenuItem.Text = "ReadBySubmissionID";
            this.readByCommunicationIDToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // readByBatchIDToolStripMenuItem
            // 
            this.readByBatchIDToolStripMenuItem.Name = "readByBatchIDToolStripMenuItem";
            this.readByBatchIDToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.readByBatchIDToolStripMenuItem.Tag = "BatchID";
            this.readByBatchIDToolStripMenuItem.Text = "ReadByBatchID";
            this.readByBatchIDToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // downloadToolStripMenuItem1
            // 
            this.downloadToolStripMenuItem1.Name = "downloadToolStripMenuItem1";
            this.downloadToolStripMenuItem1.Size = new System.Drawing.Size(253, 22);
            this.downloadToolStripMenuItem1.Tag = "BatchStatus";
            this.downloadToolStripMenuItem1.Text = "Download";
            this.downloadToolStripMenuItem1.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // readByUserNameToolStripMenuItem
            // 
            this.readByUserNameToolStripMenuItem.Name = "readByUserNameToolStripMenuItem";
            this.readByUserNameToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.readByUserNameToolStripMenuItem.Tag = "UserName";
            this.readByUserNameToolStripMenuItem.Text = "ReadByUserName";
            this.readByUserNameToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // readByUserIDStatusCodeDateRangeToolStripMenuItem
            // 
            this.readByUserIDStatusCodeDateRangeToolStripMenuItem.Name = "readByUserIDStatusCodeDateRangeToolStripMenuItem";
            this.readByUserIDStatusCodeDateRangeToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.readByUserIDStatusCodeDateRangeToolStripMenuItem.Text = "ReadByUserStatusCodeDateRange";
            this.readByUserIDStatusCodeDateRangeToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // readByStatusCodeDateRangeToolStripMenuItem
            // 
            this.readByStatusCodeDateRangeToolStripMenuItem.Name = "readByStatusCodeDateRangeToolStripMenuItem";
            this.readByStatusCodeDateRangeToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.readByStatusCodeDateRangeToolStripMenuItem.Text = "ReadByStatusCodeDateRange";
            this.readByStatusCodeDateRangeToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.createToolStripMenuItem.Tag = "BatchXML";
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.HTTPPPOST_ToolStripMenuItem_Click);
            // 
            // uploadToolStripMenuItem1
            // 
            this.uploadToolStripMenuItem1.Name = "uploadToolStripMenuItem1";
            this.uploadToolStripMenuItem1.Size = new System.Drawing.Size(253, 22);
            this.uploadToolStripMenuItem1.Tag = "BatchXML";
            this.uploadToolStripMenuItem1.Text = "Upload";
            this.uploadToolStripMenuItem1.Click += new System.EventHandler(this.HTTPPUT_ToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.HTTPDELETE_ToolStripMenuItem_Click);
            // 
            // checkInToolStripMenuItem
            // 
            this.checkInToolStripMenuItem.Name = "checkInToolStripMenuItem";
            this.checkInToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.checkInToolStripMenuItem.Text = "CheckIn";
            this.checkInToolStripMenuItem.Click += new System.EventHandler(this.HTTPPUT_ToolStripMenuItem_Click);
            // 
            // checkOutToolStripMenuItem
            // 
            this.checkOutToolStripMenuItem.Name = "checkOutToolStripMenuItem";
            this.checkOutToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.checkOutToolStripMenuItem.Text = "CheckOut";
            this.checkOutToolStripMenuItem.Click += new System.EventHandler(this.HTTPPUT_ToolStripMenuItem_Click);
            // 
            // overrideCheckOutToolStripMenuItem
            // 
            this.overrideCheckOutToolStripMenuItem.Name = "overrideCheckOutToolStripMenuItem";
            this.overrideCheckOutToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.overrideCheckOutToolStripMenuItem.Text = "OverrideCheckOut";
            this.overrideCheckOutToolStripMenuItem.Click += new System.EventHandler(this.HTTPPUT_ToolStripMenuItem_Click);
            // 
            // uploadCompleteToolStripMenuItem
            // 
            this.uploadCompleteToolStripMenuItem.Name = "uploadCompleteToolStripMenuItem";
            this.uploadCompleteToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.uploadCompleteToolStripMenuItem.Text = "UploadComplete";
            this.uploadCompleteToolStripMenuItem.Click += new System.EventHandler(this.HTTPPUT_ToolStripMenuItem_Click);
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.requestingPartyToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.userToolStripMenuItem.Tag = "SessionTokenID";
            this.userToolStripMenuItem.Text = "User";
            // 
            // requestingPartyToolStripMenuItem
            // 
            this.requestingPartyToolStripMenuItem.Name = "requestingPartyToolStripMenuItem";
            this.requestingPartyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.requestingPartyToolStripMenuItem.Text = "RequestingParty";
            this.requestingPartyToolStripMenuItem.Click += new System.EventHandler(this.HTTPGET_ToolStipMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.DarkGray;
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnClearSendHTTP);
            this.splitContainer1.Panel1.Controls.Add(this.txtSubmitHTTP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.DarkGray;
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.btnClearReceivedHTTP);
            this.splitContainer1.Panel2.Controls.Add(this.txtReceivedHTTP);
            this.splitContainer1.Size = new System.Drawing.Size(1991, 1037);
            this.splitContainer1.SplitterDistance = 935;
            this.splitContainer1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "HTTP Request:";
            // 
            // btnClearSendHTTP
            // 
            this.btnClearSendHTTP.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClearSendHTTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearSendHTTP.Location = new System.Drawing.Point(0, 991);
            this.btnClearSendHTTP.Name = "btnClearSendHTTP";
            this.btnClearSendHTTP.Size = new System.Drawing.Size(935, 46);
            this.btnClearSendHTTP.TabIndex = 4;
            this.btnClearSendHTTP.Text = "Clear";
            this.btnClearSendHTTP.UseVisualStyleBackColor = true;
            this.btnClearSendHTTP.Click += new System.EventHandler(this.btnClearSend_Click);
            // 
            // txtSubmitHTTP
            // 
            this.txtSubmitHTTP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubmitHTTP.Location = new System.Drawing.Point(5, 31);
            this.txtSubmitHTTP.Multiline = true;
            this.txtSubmitHTTP.Name = "txtSubmitHTTP";
            this.txtSubmitHTTP.ReadOnly = true;
            this.txtSubmitHTTP.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSubmitHTTP.Size = new System.Drawing.Size(927, 969);
            this.txtSubmitHTTP.TabIndex = 0;
            this.txtSubmitHTTP.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "HTTP Response:";
            // 
            // btnClearReceivedHTTP
            // 
            this.btnClearReceivedHTTP.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClearReceivedHTTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearReceivedHTTP.Location = new System.Drawing.Point(0, 991);
            this.btnClearReceivedHTTP.Name = "btnClearReceivedHTTP";
            this.btnClearReceivedHTTP.Size = new System.Drawing.Size(1052, 46);
            this.btnClearReceivedHTTP.TabIndex = 4;
            this.btnClearReceivedHTTP.Text = "Clear";
            this.btnClearReceivedHTTP.UseVisualStyleBackColor = true;
            this.btnClearReceivedHTTP.Click += new System.EventHandler(this.btnClearReceived_Click);
            // 
            // txtReceivedHTTP
            // 
            this.txtReceivedHTTP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceivedHTTP.Location = new System.Drawing.Point(3, 31);
            this.txtReceivedHTTP.Multiline = true;
            this.txtReceivedHTTP.Name = "txtReceivedHTTP";
            this.txtReceivedHTTP.ReadOnly = true;
            this.txtReceivedHTTP.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReceivedHTTP.Size = new System.Drawing.Size(1049, 969);
            this.txtReceivedHTTP.TabIndex = 2;
            this.txtReceivedHTTP.WordWrap = false;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2005, 1054);
            this.Controls.Add(this.tabMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SECURE - Windows Form API Test Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.tabMain.ResumeLayout(false);
            this.tpCounty.ResumeLayout(false);
            this.tpSubmitter.ResumeLayout(false);
            this.tpHTTP.ResumeLayout(false);
            this.tpHTTP.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpHTTP;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtSubmitHTTP;
        private System.Windows.Forms.TextBox txtReceivedHTTP;
        private System.Windows.Forms.Button btnClearSendHTTP;
        private System.Windows.Forms.Button btnClearReceivedHTTP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tpCounty;
        private System.Windows.Forms.TabPage tpSubmitter;
        private CountyPanel countyPanel1;
        private SubmitterPanel submitterPanel1;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem tsmAuthentication;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem countyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readByDocumentIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readByBatchIDSequenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readByCommunicationIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readByBatchIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem readByUserNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readByUserIDStatusCodeDateRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readByStatusCodeDateRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overrideCheckOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadCompleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel urlLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel SessionTokenLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusMediaType;
        private System.Windows.Forms.ToolStripMenuItem titlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem citiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processQueuesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestingPartyToolStripMenuItem;
    }
}


namespace WinFormTest.ClientControls
{
    partial class FileContentInfoControl
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
            this.grpFileContent = new System.Windows.Forms.GroupBox();
            this.lblFileContentWarning = new System.Windows.Forms.Label();
            this.lstFileContents = new System.Windows.Forms.ListBox();
            this.btnReplaceFile = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDocumentImageID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtActionCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numPageCount = new System.Windows.Forms.NumericUpDown();
            this.cmbEmbeddedFileType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.grpFileContent.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPageCount)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFileContent
            // 
            this.grpFileContent.Controls.Add(this.lblFileContentWarning);
            this.grpFileContent.Controls.Add(this.lstFileContents);
            this.grpFileContent.Controls.Add(this.btnReplaceFile);
            this.grpFileContent.Controls.Add(this.btnSaveFile);
            this.grpFileContent.Location = new System.Drawing.Point(3, 158);
            this.grpFileContent.Name = "grpFileContent";
            this.grpFileContent.Size = new System.Drawing.Size(340, 157);
            this.grpFileContent.TabIndex = 11;
            this.grpFileContent.TabStop = false;
            this.grpFileContent.Text = "FileContent";
            // 
            // lblFileContentWarning
            // 
            this.lblFileContentWarning.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblFileContentWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFileContentWarning.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileContentWarning.Location = new System.Drawing.Point(15, 19);
            this.lblFileContentWarning.Name = "lblFileContentWarning";
            this.lblFileContentWarning.Size = new System.Drawing.Size(300, 69);
            this.lblFileContentWarning.TabIndex = 3;
            this.lblFileContentWarning.Text = "Image data has not been downloaded or read into memory.";
            this.lblFileContentWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFileContentWarning.Visible = false;
            // 
            // lstFileContents
            // 
            this.lstFileContents.FormattingEnabled = true;
            this.lstFileContents.Location = new System.Drawing.Point(15, 19);
            this.lstFileContents.Name = "lstFileContents";
            this.lstFileContents.Size = new System.Drawing.Size(300, 69);
            this.lstFileContents.TabIndex = 2;
            // 
            // btnReplaceFile
            // 
            this.btnReplaceFile.Location = new System.Drawing.Point(15, 123);
            this.btnReplaceFile.Name = "btnReplaceFile";
            this.btnReplaceFile.Size = new System.Drawing.Size(300, 23);
            this.btnReplaceFile.TabIndex = 1;
            this.btnReplaceFile.Text = "Replace File(s)";
            this.btnReplaceFile.UseVisualStyleBackColor = true;
            this.btnReplaceFile.Click += new System.EventHandler(this.btnReplaceFile_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(15, 94);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(300, 23);
            this.btnSaveFile.TabIndex = 0;
            this.btnSaveFile.Text = "Save to Local File(s)";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDocumentImageID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtActionCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numPageCount);
            this.groupBox1.Controls.Add(this.cmbEmbeddedFileType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 129);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FileContentInfo";
            // 
            // txtDocumentImageID
            // 
            this.txtDocumentImageID.Location = new System.Drawing.Point(132, 24);
            this.txtDocumentImageID.Name = "txtDocumentImageID";
            this.txtDocumentImageID.ReadOnly = true;
            this.txtDocumentImageID.Size = new System.Drawing.Size(183, 20);
            this.txtDocumentImageID.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "ID:";
            // 
            // txtActionCode
            // 
            this.txtActionCode.Location = new System.Drawing.Point(132, 103);
            this.txtActionCode.Name = "txtActionCode";
            this.txtActionCode.ReadOnly = true;
            this.txtActionCode.Size = new System.Drawing.Size(183, 20);
            this.txtActionCode.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Action Code:";
            // 
            // numPageCount
            // 
            this.numPageCount.Location = new System.Drawing.Point(132, 77);
            this.numPageCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPageCount.Name = "numPageCount";
            this.numPageCount.Size = new System.Drawing.Size(183, 20);
            this.numPageCount.TabIndex = 14;
            this.numPageCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numPageCount.ValueChanged += new System.EventHandler(this.numPageCount_ValueChanged);
            // 
            // cmbEmbeddedFileType
            // 
            this.cmbEmbeddedFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmbeddedFileType.FormattingEnabled = true;
            this.cmbEmbeddedFileType.Location = new System.Drawing.Point(132, 50);
            this.cmbEmbeddedFileType.Name = "cmbEmbeddedFileType";
            this.cmbEmbeddedFileType.Size = new System.Drawing.Size(183, 21);
            this.cmbEmbeddedFileType.TabIndex = 13;
            this.cmbEmbeddedFileType.SelectedIndexChanged += new System.EventHandler(this.cmbEmbeddedFileType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Page Count:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Embedded File Type:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(146, 20);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "Document Image";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.lblTitle);
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.grpFileContent);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(351, 331);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // FileContentInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FileContentInfoControl";
            this.Size = new System.Drawing.Size(351, 331);
            this.grpFileContent.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPageCount)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFileContent;
        private System.Windows.Forms.Button btnReplaceFile;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numPageCount;
        private System.Windows.Forms.ComboBox cmbEmbeddedFileType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstFileContents;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox txtActionCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDocumentImageID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFileContentWarning;

    }
}

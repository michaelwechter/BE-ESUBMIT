namespace WinFormTest.ClientControls
{
    partial class MemoDetailControl
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.tblTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblUserDisplayName = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblCreateTimeStamp = new System.Windows.Forms.Label();
            this.lblEditTimeStamp = new System.Windows.Forms.Label();
            this.txtUserDisplayName = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtCreateTimeStamp = new System.Windows.Forms.TextBox();
            this.txtEditTimeStamp = new System.Windows.Forms.TextBox();
            this.lblActionCode = new System.Windows.Forms.Label();
            this.txtActionCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboMemoType = new System.Windows.Forms.ComboBox();
            this.tblHeader.SuspendLayout();
            this.tblTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblHeader
            // 
            this.tblHeader.ColumnCount = 1;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Controls.Add(this.lblHeader, 0, 0);
            this.tblHeader.Controls.Add(this.txtMemo, 0, 1);
            this.tblHeader.Controls.Add(this.tblTableLayout, 0, 2);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeader.Location = new System.Drawing.Point(0, 0);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 3;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tblHeader.Size = new System.Drawing.Size(369, 414);
            this.tblHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(363, 30);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Memo Detail";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMemo
            // 
            this.txtMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMemo.Enabled = false;
            this.txtMemo.Location = new System.Drawing.Point(3, 33);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(363, 213);
            this.txtMemo.TabIndex = 0;
            this.txtMemo.TextChanged += new System.EventHandler(this.txtMemo_TextChanged);
            // 
            // tblTableLayout
            // 
            this.tblTableLayout.ColumnCount = 2;
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTableLayout.Controls.Add(this.lblUserDisplayName, 0, 0);
            this.tblTableLayout.Controls.Add(this.lblUserName, 0, 1);
            this.tblTableLayout.Controls.Add(this.lblCreateTimeStamp, 0, 4);
            this.tblTableLayout.Controls.Add(this.lblEditTimeStamp, 0, 5);
            this.tblTableLayout.Controls.Add(this.txtUserDisplayName, 1, 0);
            this.tblTableLayout.Controls.Add(this.txtUserName, 1, 1);
            this.tblTableLayout.Controls.Add(this.txtCreateTimeStamp, 1, 4);
            this.tblTableLayout.Controls.Add(this.txtEditTimeStamp, 1, 5);
            this.tblTableLayout.Controls.Add(this.lblActionCode, 0, 2);
            this.tblTableLayout.Controls.Add(this.txtActionCode, 1, 2);
            this.tblTableLayout.Controls.Add(this.label1, 0, 3);
            this.tblTableLayout.Controls.Add(this.cboMemoType, 1, 3);
            this.tblTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTableLayout.Location = new System.Drawing.Point(3, 252);
            this.tblTableLayout.Name = "tblTableLayout";
            this.tblTableLayout.RowCount = 6;
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.Size = new System.Drawing.Size(363, 159);
            this.tblTableLayout.TabIndex = 0;
            // 
            // lblUserDisplayName
            // 
            this.lblUserDisplayName.AutoSize = true;
            this.lblUserDisplayName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserDisplayName.Location = new System.Drawing.Point(3, 0);
            this.lblUserDisplayName.Name = "lblUserDisplayName";
            this.lblUserDisplayName.Size = new System.Drawing.Size(144, 25);
            this.lblUserDisplayName.TabIndex = 0;
            this.lblUserDisplayName.Text = "User Display Name:";
            this.lblUserDisplayName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserName.Location = new System.Drawing.Point(3, 25);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(144, 25);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "User Name:";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateTimeStamp
            // 
            this.lblCreateTimeStamp.AutoSize = true;
            this.lblCreateTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreateTimeStamp.Location = new System.Drawing.Point(3, 100);
            this.lblCreateTimeStamp.Name = "lblCreateTimeStamp";
            this.lblCreateTimeStamp.Size = new System.Drawing.Size(144, 25);
            this.lblCreateTimeStamp.TabIndex = 3;
            this.lblCreateTimeStamp.Text = "Create Time Stamp:";
            this.lblCreateTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEditTimeStamp
            // 
            this.lblEditTimeStamp.AutoSize = true;
            this.lblEditTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEditTimeStamp.Location = new System.Drawing.Point(3, 125);
            this.lblEditTimeStamp.Name = "lblEditTimeStamp";
            this.lblEditTimeStamp.Size = new System.Drawing.Size(144, 34);
            this.lblEditTimeStamp.TabIndex = 4;
            this.lblEditTimeStamp.Text = "Edit Time Stamp:";
            this.lblEditTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserDisplayName
            // 
            this.txtUserDisplayName.BackColor = System.Drawing.SystemColors.Control;
            this.txtUserDisplayName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserDisplayName.Enabled = false;
            this.txtUserDisplayName.Location = new System.Drawing.Point(153, 3);
            this.txtUserDisplayName.Name = "txtUserDisplayName";
            this.txtUserDisplayName.ReadOnly = true;
            this.txtUserDisplayName.Size = new System.Drawing.Size(207, 20);
            this.txtUserDisplayName.TabIndex = 5;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.SystemColors.Control;
            this.txtUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(153, 28);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(207, 20);
            this.txtUserName.TabIndex = 6;
            // 
            // txtCreateTimeStamp
            // 
            this.txtCreateTimeStamp.BackColor = System.Drawing.SystemColors.Control;
            this.txtCreateTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCreateTimeStamp.Enabled = false;
            this.txtCreateTimeStamp.Location = new System.Drawing.Point(153, 103);
            this.txtCreateTimeStamp.Name = "txtCreateTimeStamp";
            this.txtCreateTimeStamp.ReadOnly = true;
            this.txtCreateTimeStamp.Size = new System.Drawing.Size(207, 20);
            this.txtCreateTimeStamp.TabIndex = 8;
            // 
            // txtEditTimeStamp
            // 
            this.txtEditTimeStamp.BackColor = System.Drawing.SystemColors.Control;
            this.txtEditTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditTimeStamp.Enabled = false;
            this.txtEditTimeStamp.Location = new System.Drawing.Point(153, 128);
            this.txtEditTimeStamp.Name = "txtEditTimeStamp";
            this.txtEditTimeStamp.ReadOnly = true;
            this.txtEditTimeStamp.Size = new System.Drawing.Size(207, 20);
            this.txtEditTimeStamp.TabIndex = 9;
            // 
            // lblActionCode
            // 
            this.lblActionCode.AutoSize = true;
            this.lblActionCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActionCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionCode.Location = new System.Drawing.Point(3, 50);
            this.lblActionCode.Name = "lblActionCode";
            this.lblActionCode.Size = new System.Drawing.Size(144, 25);
            this.lblActionCode.TabIndex = 10;
            this.lblActionCode.Text = "Action Code:";
            this.lblActionCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtActionCode
            // 
            this.txtActionCode.BackColor = System.Drawing.SystemColors.Control;
            this.txtActionCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtActionCode.HideSelection = false;
            this.txtActionCode.Location = new System.Drawing.Point(153, 53);
            this.txtActionCode.Name = "txtActionCode";
            this.txtActionCode.ReadOnly = true;
            this.txtActionCode.Size = new System.Drawing.Size(207, 20);
            this.txtActionCode.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "MemoType:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMemoType
            // 
            this.cboMemoType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboMemoType.FormattingEnabled = true;
            this.cboMemoType.Location = new System.Drawing.Point(153, 78);
            this.cboMemoType.Name = "cboMemoType";
            this.cboMemoType.Size = new System.Drawing.Size(207, 21);
            this.cboMemoType.TabIndex = 13;
            this.cboMemoType.SelectedIndexChanged += new System.EventHandler(this.cboMemoType_SelectedIndexChanged);
            // 
            // MemoDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblHeader);
            this.Name = "MemoDetailControl";
            this.Size = new System.Drawing.Size(369, 414);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.tblTableLayout.ResumeLayout(false);
            this.tblTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.TableLayoutPanel tblTableLayout;
        private System.Windows.Forms.Label lblUserDisplayName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblCreateTimeStamp;
        private System.Windows.Forms.Label lblEditTimeStamp;
        private System.Windows.Forms.TextBox txtUserDisplayName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtCreateTimeStamp;
        private System.Windows.Forms.TextBox txtEditTimeStamp;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblActionCode;
        private System.Windows.Forms.TextBox txtActionCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboMemoType;

    }
}

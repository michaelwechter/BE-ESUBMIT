namespace WinFormTest.ClientControls
{
    partial class CityDetailControl
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
            this.lblCountyID = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblActionCode = new System.Windows.Forms.Label();
            this.lblCreateTimeStamp = new System.Windows.Forms.Label();
            this.lblEditTimeStamp = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtCountyID = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtActionCode = new System.Windows.Forms.TextBox();
            this.txtCreateTimeStamp = new System.Windows.Forms.TextBox();
            this.txtEditTimeStamp = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.tblHeader.SuspendLayout();
            this.tblTableLayout.SuspendLayout();
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
            this.tblHeader.Size = new System.Drawing.Size(331, 225);
            this.tblHeader.TabIndex = 0;
            // 
            // tblTableLayout
            // 
            this.tblTableLayout.ColumnCount = 2;
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTableLayout.Controls.Add(this.lblID, 0, 0);
            this.tblTableLayout.Controls.Add(this.lblCountyID, 0, 1);
            this.tblTableLayout.Controls.Add(this.lblCode, 0, 2);
            this.tblTableLayout.Controls.Add(this.lblDescription, 0, 3);
            this.tblTableLayout.Controls.Add(this.lblActionCode, 0, 4);
            this.tblTableLayout.Controls.Add(this.lblCreateTimeStamp, 0, 5);
            this.tblTableLayout.Controls.Add(this.lblEditTimeStamp, 0, 6);
            this.tblTableLayout.Controls.Add(this.txtID, 1, 0);
            this.tblTableLayout.Controls.Add(this.txtCountyID, 1, 1);
            this.tblTableLayout.Controls.Add(this.txtCode, 1, 2);
            this.tblTableLayout.Controls.Add(this.txtDescription, 1, 3);
            this.tblTableLayout.Controls.Add(this.txtActionCode, 1, 4);
            this.tblTableLayout.Controls.Add(this.txtCreateTimeStamp, 1, 5);
            this.tblTableLayout.Controls.Add(this.txtEditTimeStamp, 1, 6);
            this.tblTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTableLayout.Location = new System.Drawing.Point(3, 33);
            this.tblTableLayout.Name = "tblTableLayout";
            this.tblTableLayout.RowCount = 8;
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTableLayout.Size = new System.Drawing.Size(325, 189);
            this.tblTableLayout.TabIndex = 0;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblID.Location = new System.Drawing.Point(3, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(119, 25);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID:";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCountyID
            // 
            this.lblCountyID.AutoSize = true;
            this.lblCountyID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCountyID.Location = new System.Drawing.Point(3, 25);
            this.lblCountyID.Name = "lblCountyID";
            this.lblCountyID.Size = new System.Drawing.Size(119, 25);
            this.lblCountyID.TabIndex = 1;
            this.lblCountyID.Text = "County ID:";
            this.lblCountyID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCode.Location = new System.Drawing.Point(3, 50);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(119, 25);
            this.lblCode.TabIndex = 2;
            this.lblCode.Text = "Code:";
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Location = new System.Drawing.Point(3, 75);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(119, 25);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description:";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblActionCode
            // 
            this.lblActionCode.AutoSize = true;
            this.lblActionCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActionCode.Location = new System.Drawing.Point(3, 100);
            this.lblActionCode.Name = "lblActionCode";
            this.lblActionCode.Size = new System.Drawing.Size(119, 25);
            this.lblActionCode.TabIndex = 4;
            this.lblActionCode.Text = "ActionCode:";
            this.lblActionCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreateTimeStamp
            // 
            this.lblCreateTimeStamp.AutoSize = true;
            this.lblCreateTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCreateTimeStamp.Location = new System.Drawing.Point(3, 125);
            this.lblCreateTimeStamp.Name = "lblCreateTimeStamp";
            this.lblCreateTimeStamp.Size = new System.Drawing.Size(119, 25);
            this.lblCreateTimeStamp.TabIndex = 5;
            this.lblCreateTimeStamp.Text = "Create Time Stamp:";
            this.lblCreateTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEditTimeStamp
            // 
            this.lblEditTimeStamp.AutoSize = true;
            this.lblEditTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEditTimeStamp.Location = new System.Drawing.Point(3, 150);
            this.lblEditTimeStamp.Name = "lblEditTimeStamp";
            this.lblEditTimeStamp.Size = new System.Drawing.Size(119, 25);
            this.lblEditTimeStamp.TabIndex = 6;
            this.lblEditTimeStamp.Text = "Edit Time Stamp:";
            this.lblEditTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtID.Location = new System.Drawing.Point(128, 3);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(194, 20);
            this.txtID.TabIndex = 7;
            // 
            // txtCountyID
            // 
            this.txtCountyID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCountyID.Location = new System.Drawing.Point(128, 28);
            this.txtCountyID.Name = "txtCountyID";
            this.txtCountyID.ReadOnly = true;
            this.txtCountyID.Size = new System.Drawing.Size(194, 20);
            this.txtCountyID.TabIndex = 8;
            // 
            // txtCode
            // 
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Location = new System.Drawing.Point(128, 53);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(194, 20);
            this.txtCode.TabIndex = 9;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(128, 78);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(194, 20);
            this.txtDescription.TabIndex = 10;
            // 
            // txtActionCode
            // 
            this.txtActionCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtActionCode.Location = new System.Drawing.Point(128, 103);
            this.txtActionCode.Name = "txtActionCode";
            this.txtActionCode.ReadOnly = true;
            this.txtActionCode.Size = new System.Drawing.Size(194, 20);
            this.txtActionCode.TabIndex = 11;
            // 
            // txtCreateTimeStamp
            // 
            this.txtCreateTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCreateTimeStamp.Location = new System.Drawing.Point(128, 128);
            this.txtCreateTimeStamp.Name = "txtCreateTimeStamp";
            this.txtCreateTimeStamp.ReadOnly = true;
            this.txtCreateTimeStamp.Size = new System.Drawing.Size(194, 20);
            this.txtCreateTimeStamp.TabIndex = 12;
            // 
            // txtEditTimeStamp
            // 
            this.txtEditTimeStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditTimeStamp.Location = new System.Drawing.Point(128, 153);
            this.txtEditTimeStamp.Name = "txtEditTimeStamp";
            this.txtEditTimeStamp.ReadOnly = true;
            this.txtEditTimeStamp.Size = new System.Drawing.Size(194, 20);
            this.txtEditTimeStamp.TabIndex = 13;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(325, 30);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "City Detail";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CityDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblHeader);
            this.Name = "CityDetailControl";
            this.Size = new System.Drawing.Size(331, 225);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.tblTableLayout.ResumeLayout(false);
            this.tblTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.TableLayoutPanel tblTableLayout;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblCountyID;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblActionCode;
        private System.Windows.Forms.Label lblCreateTimeStamp;
        private System.Windows.Forms.Label lblEditTimeStamp;
        protected System.Windows.Forms.TextBox txtID;
        protected System.Windows.Forms.TextBox txtCountyID;
        protected System.Windows.Forms.TextBox txtCode;
        protected System.Windows.Forms.TextBox txtDescription;
        protected System.Windows.Forms.TextBox txtActionCode;
        protected System.Windows.Forms.TextBox txtCreateTimeStamp;
        protected System.Windows.Forms.TextBox txtEditTimeStamp;
    }
}

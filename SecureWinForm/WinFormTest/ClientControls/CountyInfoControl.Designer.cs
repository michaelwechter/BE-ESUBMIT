namespace WinFormTest.ClientControls
{
    partial class CountyInfoControl
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblFipsStateCode = new System.Windows.Forms.Label();
            this.lblFipsCountyCode = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtFipsStateCode = new System.Windows.Forms.TextBox();
            this.txtFipsCountyCode = new System.Windows.Forms.TextBox();
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
            this.tblHeader.Size = new System.Drawing.Size(293, 163);
            this.tblHeader.TabIndex = 0;
            // 
            // tblTableLayout
            // 
            this.tblTableLayout.ColumnCount = 2;
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTableLayout.Controls.Add(this.lblID, 0, 0);
            this.tblTableLayout.Controls.Add(this.lblName, 0, 1);
            this.tblTableLayout.Controls.Add(this.lblFipsStateCode, 0, 2);
            this.tblTableLayout.Controls.Add(this.lblFipsCountyCode, 0, 3);
            this.tblTableLayout.Controls.Add(this.txtID, 1, 0);
            this.tblTableLayout.Controls.Add(this.txtName, 1, 1);
            this.tblTableLayout.Controls.Add(this.txtFipsStateCode, 1, 2);
            this.tblTableLayout.Controls.Add(this.txtFipsCountyCode, 1, 3);
            this.tblTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTableLayout.Location = new System.Drawing.Point(3, 33);
            this.tblTableLayout.Name = "tblTableLayout";
            this.tblTableLayout.RowCount = 5;
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTableLayout.Size = new System.Drawing.Size(287, 127);
            this.tblTableLayout.TabIndex = 0;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblID.Location = new System.Drawing.Point(3, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(124, 25);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "ID:";
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(3, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(124, 25);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFipsStateCode
            // 
            this.lblFipsStateCode.AutoSize = true;
            this.lblFipsStateCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFipsStateCode.Location = new System.Drawing.Point(3, 50);
            this.lblFipsStateCode.Name = "lblFipsStateCode";
            this.lblFipsStateCode.Size = new System.Drawing.Size(124, 25);
            this.lblFipsStateCode.TabIndex = 2;
            this.lblFipsStateCode.Text = "Fips State Code:";
            this.lblFipsStateCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFipsCountyCode
            // 
            this.lblFipsCountyCode.AutoSize = true;
            this.lblFipsCountyCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFipsCountyCode.Location = new System.Drawing.Point(3, 75);
            this.lblFipsCountyCode.Name = "lblFipsCountyCode";
            this.lblFipsCountyCode.Size = new System.Drawing.Size(124, 25);
            this.lblFipsCountyCode.TabIndex = 3;
            this.lblFipsCountyCode.Text = "Fips County Code:";
            this.lblFipsCountyCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtID.Location = new System.Drawing.Point(133, 3);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(151, 20);
            this.txtID.TabIndex = 5;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(133, 28);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(151, 20);
            this.txtName.TabIndex = 6;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtFipsStateCode
            // 
            this.txtFipsStateCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFipsStateCode.Location = new System.Drawing.Point(133, 53);
            this.txtFipsStateCode.Name = "txtFipsStateCode";
            this.txtFipsStateCode.Size = new System.Drawing.Size(151, 20);
            this.txtFipsStateCode.TabIndex = 7;
            this.txtFipsStateCode.TextChanged += new System.EventHandler(this.txtFipsStateCode_TextChanged);
            // 
            // txtFipsCountyCode
            // 
            this.txtFipsCountyCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFipsCountyCode.Location = new System.Drawing.Point(133, 78);
            this.txtFipsCountyCode.Name = "txtFipsCountyCode";
            this.txtFipsCountyCode.Size = new System.Drawing.Size(151, 20);
            this.txtFipsCountyCode.TabIndex = 8;
            this.txtFipsCountyCode.TextChanged += new System.EventHandler(this.txtFipsCountyCode_TextChanged);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(287, 30);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "County Info";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CountyInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblHeader);
            this.Name = "CountyInfoControl";
            this.Size = new System.Drawing.Size(293, 163);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.tblTableLayout.ResumeLayout(false);
            this.tblTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.TableLayoutPanel tblTableLayout;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblFipsStateCode;
        private System.Windows.Forms.Label lblFipsCountyCode;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtFipsStateCode;
        private System.Windows.Forms.TextBox txtFipsCountyCode;
        private System.Windows.Forms.Label lblHeader;
    }
}

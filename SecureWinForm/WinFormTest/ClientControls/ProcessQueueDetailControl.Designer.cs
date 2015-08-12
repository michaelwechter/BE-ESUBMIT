namespace WinFormTest
{
    partial class ProcessQueueDetailControl
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblOpenTime = new System.Windows.Forms.Label();
            this.lblCloseTime = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtCountyID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtOpenTime = new System.Windows.Forms.TextBox();
            this.txtCloseTime = new System.Windows.Forms.TextBox();
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
            this.tblHeader.Size = new System.Drawing.Size(314, 203);
            this.tblHeader.TabIndex = 0;
            // 
            // tblTableLayout
            // 
            this.tblTableLayout.ColumnCount = 2;
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTableLayout.Controls.Add(this.lblID, 0, 0);
            this.tblTableLayout.Controls.Add(this.lblCountyID, 0, 1);
            this.tblTableLayout.Controls.Add(this.lblName, 0, 2);
            this.tblTableLayout.Controls.Add(this.lblDescription, 0, 3);
            this.tblTableLayout.Controls.Add(this.lblOpenTime, 0, 4);
            this.tblTableLayout.Controls.Add(this.lblCloseTime, 0, 5);
            this.tblTableLayout.Controls.Add(this.txtID, 1, 0);
            this.tblTableLayout.Controls.Add(this.txtCountyID, 1, 1);
            this.tblTableLayout.Controls.Add(this.txtName, 1, 2);
            this.tblTableLayout.Controls.Add(this.txtDescription, 1, 3);
            this.tblTableLayout.Controls.Add(this.txtOpenTime, 1, 4);
            this.tblTableLayout.Controls.Add(this.txtCloseTime, 1, 5);
            this.tblTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTableLayout.Location = new System.Drawing.Point(3, 33);
            this.tblTableLayout.Name = "tblTableLayout";
            this.tblTableLayout.RowCount = 7;
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTableLayout.Size = new System.Drawing.Size(308, 167);
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
            // lblCountyID
            // 
            this.lblCountyID.AutoSize = true;
            this.lblCountyID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCountyID.Location = new System.Drawing.Point(3, 25);
            this.lblCountyID.Name = "lblCountyID";
            this.lblCountyID.Size = new System.Drawing.Size(124, 25);
            this.lblCountyID.TabIndex = 1;
            this.lblCountyID.Text = "County ID:";
            this.lblCountyID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblName.Location = new System.Drawing.Point(3, 50);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(124, 25);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Location = new System.Drawing.Point(3, 75);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(124, 25);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description:";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOpenTime
            // 
            this.lblOpenTime.AutoSize = true;
            this.lblOpenTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOpenTime.Location = new System.Drawing.Point(3, 100);
            this.lblOpenTime.Name = "lblOpenTime";
            this.lblOpenTime.Size = new System.Drawing.Size(124, 25);
            this.lblOpenTime.TabIndex = 4;
            this.lblOpenTime.Text = "Open Time:";
            this.lblOpenTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCloseTime
            // 
            this.lblCloseTime.AutoSize = true;
            this.lblCloseTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCloseTime.Location = new System.Drawing.Point(3, 125);
            this.lblCloseTime.Name = "lblCloseTime";
            this.lblCloseTime.Size = new System.Drawing.Size(124, 25);
            this.lblCloseTime.TabIndex = 5;
            this.lblCloseTime.Text = "Close Time:";
            this.lblCloseTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtID.Location = new System.Drawing.Point(133, 3);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(172, 20);
            this.txtID.TabIndex = 6;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // txtCountyID
            // 
            this.txtCountyID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCountyID.Location = new System.Drawing.Point(133, 28);
            this.txtCountyID.Name = "txtCountyID";
            this.txtCountyID.Size = new System.Drawing.Size(172, 20);
            this.txtCountyID.TabIndex = 7;
            this.txtCountyID.TextChanged += new System.EventHandler(this.txtCountyID_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Location = new System.Drawing.Point(133, 53);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(172, 20);
            this.txtName.TabIndex = 8;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(133, 78);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(172, 20);
            this.txtDescription.TabIndex = 9;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // txtOpenTime
            // 
            this.txtOpenTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOpenTime.Location = new System.Drawing.Point(133, 103);
            this.txtOpenTime.Name = "txtOpenTime";
            this.txtOpenTime.Size = new System.Drawing.Size(172, 20);
            this.txtOpenTime.TabIndex = 10;
            // 
            // txtCloseTime
            // 
            this.txtCloseTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCloseTime.Location = new System.Drawing.Point(133, 128);
            this.txtCloseTime.Name = "txtCloseTime";
            this.txtCloseTime.Size = new System.Drawing.Size(172, 20);
            this.txtCloseTime.TabIndex = 11;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(308, 30);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Process Queue Detail";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProcessQueueDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblHeader);
            this.Name = "ProcessQueueDetailControl";
            this.Size = new System.Drawing.Size(314, 203);
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
        private System.Windows.Forms.Label lblCountyID;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblOpenTime;
        private System.Windows.Forms.Label lblCloseTime;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtCountyID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtOpenTime;
        private System.Windows.Forms.TextBox txtCloseTime;
        private System.Windows.Forms.Label lblHeader;
    }
}

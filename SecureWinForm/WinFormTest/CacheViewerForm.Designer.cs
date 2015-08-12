namespace WinFormTest
{
    partial class CacheViewerForm
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
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstDataIndex = new System.Windows.Forms.ListBox();
            this.flpDataContents = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(75, 6);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(440, 21);
            this.cmbDataType.TabIndex = 0;
            this.cmbDataType.SelectedIndexChanged += new System.EventHandler(this.cmbDataType_SelectedIndexChanged);
            this.cmbDataType.SelectionChangeCommitted += new System.EventHandler(this.cmbDataType_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "DataType:";
            // 
            // lstDataIndex
            // 
            this.lstDataIndex.FormattingEnabled = true;
            this.lstDataIndex.Location = new System.Drawing.Point(15, 38);
            this.lstDataIndex.Name = "lstDataIndex";
            this.lstDataIndex.Size = new System.Drawing.Size(120, 433);
            this.lstDataIndex.TabIndex = 2;
            this.lstDataIndex.SelectedIndexChanged += new System.EventHandler(this.lstDataIndex_SelectedIndexChanged);
            // 
            // flpDataContents
            // 
            this.flpDataContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpDataContents.AutoScroll = true;
            this.flpDataContents.Location = new System.Drawing.Point(145, 38);
            this.flpDataContents.Name = "flpDataContents";
            this.flpDataContents.Size = new System.Drawing.Size(507, 433);
            this.flpDataContents.TabIndex = 3;
            // 
            // CacheViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 482);
            this.Controls.Add(this.flpDataContents);
            this.Controls.Add(this.lstDataIndex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDataType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CacheViewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CacheViewerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstDataIndex;
        private System.Windows.Forms.FlowLayoutPanel flpDataContents;
    }
}
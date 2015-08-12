namespace WinFormTest.ClientControls
{
    partial class DocumentInfoTitlesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentInfoTitlesForm));
            this.pnlTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lstSelected = new System.Windows.Forms.ListBox();
            this.pnlButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddAll = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnRemoveAll = new System.Windows.Forms.Button();
            this.lstOptions = new System.Windows.Forms.ListBox();
            this.ctrlTitleDetailSelected = new WinFormTest.TitleDetailControl();
            this.ctrlTitleDetailOptions = new WinFormTest.TitleDetailControl();
            this.pnlHeader = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTableLayout.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTableLayout
            // 
            this.pnlTableLayout.ColumnCount = 5;
            this.pnlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.pnlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.pnlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.pnlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.pnlTableLayout.Controls.Add(this.lstSelected, 1, 0);
            this.pnlTableLayout.Controls.Add(this.pnlButtons, 2, 0);
            this.pnlTableLayout.Controls.Add(this.lstOptions, 3, 0);
            this.pnlTableLayout.Controls.Add(this.ctrlTitleDetailSelected, 0, 0);
            this.pnlTableLayout.Controls.Add(this.ctrlTitleDetailOptions, 4, 0);
            this.pnlTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTableLayout.Location = new System.Drawing.Point(3, 3);
            this.pnlTableLayout.Name = "pnlTableLayout";
            this.pnlTableLayout.RowCount = 1;
            this.pnlTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlTableLayout.Size = new System.Drawing.Size(852, 254);
            this.pnlTableLayout.TabIndex = 0;
            // 
            // lstSelected
            // 
            this.lstSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSelected.FormattingEnabled = true;
            this.lstSelected.Location = new System.Drawing.Point(253, 3);
            this.lstSelected.Name = "lstSelected";
            this.lstSelected.Size = new System.Drawing.Size(119, 248);
            this.lstSelected.TabIndex = 0;
            this.lstSelected.SelectedIndexChanged += new System.EventHandler(this.lstSelected_SelectedIndexChanged);
            // 
            // pnlButtons
            // 
            this.pnlButtons.ColumnCount = 1;
            this.pnlButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlButtons.Controls.Add(this.btnAddAll, 0, 1);
            this.pnlButtons.Controls.Add(this.btnAdd, 0, 2);
            this.pnlButtons.Controls.Add(this.btnRemove, 0, 3);
            this.pnlButtons.Controls.Add(this.btnRemoveAll, 0, 4);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.Location = new System.Drawing.Point(378, 3);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.RowCount = 6;
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.pnlButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlButtons.Size = new System.Drawing.Size(96, 248);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnAddAll
            // 
            this.btnAddAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddAll.Location = new System.Drawing.Point(3, 67);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(90, 24);
            this.btnAddAll.TabIndex = 0;
            this.btnAddAll.Text = "<< Add All";
            this.btnAddAll.UseVisualStyleBackColor = true;
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(3, 97);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 24);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "< Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemove.Location = new System.Drawing.Point(3, 127);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(90, 24);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove >";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemoveAll.Location = new System.Drawing.Point(3, 157);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(90, 24);
            this.btnRemoveAll.TabIndex = 3;
            this.btnRemoveAll.Text = "Remove All >>";
            this.btnRemoveAll.UseVisualStyleBackColor = true;
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // lstOptions
            // 
            this.lstOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOptions.FormattingEnabled = true;
            this.lstOptions.Location = new System.Drawing.Point(480, 3);
            this.lstOptions.Name = "lstOptions";
            this.lstOptions.Size = new System.Drawing.Size(119, 248);
            this.lstOptions.TabIndex = 1;
            this.lstOptions.SelectedIndexChanged += new System.EventHandler(this.lstOptions_SelectedIndexChanged);
            // 
            // ctrlTitleDetailSelected
            // 
            this.ctrlTitleDetailSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlTitleDetailSelected.Location = new System.Drawing.Point(3, 3);
            this.ctrlTitleDetailSelected.Name = "ctrlTitleDetailSelected";
            this.ctrlTitleDetailSelected.Size = new System.Drawing.Size(244, 248);
            this.ctrlTitleDetailSelected.TabIndex = 3;
            this.ctrlTitleDetailSelected.TitleDetail = ((TitleDetail)(resources.GetObject("ctrlTitleDetailSelected.TitleDetail")));
            // 
            // ctrlTitleDetailOptions
            // 
            this.ctrlTitleDetailOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlTitleDetailOptions.Location = new System.Drawing.Point(605, 3);
            this.ctrlTitleDetailOptions.Name = "ctrlTitleDetailOptions";
            this.ctrlTitleDetailOptions.Size = new System.Drawing.Size(244, 248);
            this.ctrlTitleDetailOptions.TabIndex = 4;
            this.ctrlTitleDetailOptions.TitleDetail = ((TitleDetail)(resources.GetObject("ctrlTitleDetailOptions.TitleDetail")));
            // 
            // pnlHeader
            // 
            this.pnlHeader.ColumnCount = 1;
            this.pnlHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlHeader.Controls.Add(this.pnlTableLayout, 0, 0);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.RowCount = 1;
            this.pnlHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlHeader.Size = new System.Drawing.Size(858, 260);
            this.pnlHeader.TabIndex = 1;
            // 
            // DocumentInfoTitlesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 260);
            this.Controls.Add(this.pnlHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DocumentInfoTitlesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title Detail";
            this.pnlTableLayout.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlTableLayout;
        private System.Windows.Forms.ListBox lstSelected;
        private System.Windows.Forms.ListBox lstOptions;
        private System.Windows.Forms.TableLayoutPanel pnlButtons;
        private System.Windows.Forms.Button btnAddAll;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRemoveAll;
        private System.Windows.Forms.TableLayoutPanel pnlHeader;
        private TitleDetailControl ctrlTitleDetailSelected;
        private TitleDetailControl ctrlTitleDetailOptions;
    }
}
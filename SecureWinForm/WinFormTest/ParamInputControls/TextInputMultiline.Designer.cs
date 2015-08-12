namespace WinFormTest.ParamInputControls
{
    partial class TextInputMultiline
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
            this.SuspendLayout();
            // 
            // lblParameterName
            // 
            this.lblParameterName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // txtParameterValue
            // 
            this.txtParameterValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParameterValue.Multiline = true;
            this.txtParameterValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtParameterValue.Size = new System.Drawing.Size(285, 144);
            // 
            // TextInputMultiline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "TextInputMultiline";
            this.Size = new System.Drawing.Size(401, 151);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

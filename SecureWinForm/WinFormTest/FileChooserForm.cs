using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class FileChooserForm : Form
    {
        #region Public Properties

        /// <summary>
        /// EmbeddedFileTypes of displayed FileContentInfo
        /// </summary>
        public EmbededFileTypes EmbeddedFileType
        {
            get
            {
                return (EmbededFileTypes)Enum.Parse(typeof(EmbededFileTypes), (string)this.cmbEmbeddedFileType.SelectedItem);
            }
            set
            {
                this.cmbEmbeddedFileType.SelectedItem = value.ToString();
            }
        }
        /// <summary>
        /// Pagecount of displayed FileContentInfo
        /// </summary>
        public string PageCount
        {
            get
            {
                return this.numPageCount.Value.ToString();
            }
            set
            {
                this.numPageCount.Value = decimal.Parse(value);
            }
        }
        /// <summary>
        /// String Array of file paths of files selected
        /// </summary>
        public string[] SelectedFile;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileContentInfo">FileContentInfo to be displayed</param>
        public FileChooserForm(FileContentInfo fileContentInfo)
            : this()
        {
            this.EmbeddedFileType = fileContentInfo.EmbeddedFileType;
            this.PageCount = fileContentInfo.PageCount;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FileChooserForm()
        {
            InitializeComponent();
            this.cmbEmbeddedFileType.Items.AddRange(Enum.GetNames(typeof(EmbededFileTypes)));
        }

        #endregion

        #region Button Methods

        /// <summary>
        /// Click event for Select File Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.SelectedFile = ofd.FileNames;
                this.txtFilePath.Text = string.Empty;
                foreach (string fileName in this.SelectedFile)
                {
                    this.txtFilePath.Text += (this.txtFilePath.Text == string.Empty) ? fileName : string.Format(", {0}", fileName);
                }
            }
        }
        
        /// <summary>
        /// Click evnet for OK button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Click event for Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}

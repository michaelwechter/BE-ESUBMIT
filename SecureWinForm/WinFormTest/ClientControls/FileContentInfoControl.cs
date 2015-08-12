using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SECURE.Common.Controller.Media.Schema.CodeGen;
using System.Collections;

namespace WinFormTest.ClientControls
{
    public partial class FileContentInfoControl : UserControl
    {
        public string Title
        {
            get
            {
                return this.lblTitle.Text;
            }
            set
            {
                this.lblTitle.Text = value;
            }
        }

        private FileContentDetail _FileContentDetail;
        public FileContentDetail FileContentDetail
        {
            get
            {
                return this._FileContentDetail;
            }
            set
            {
                this._FileContentDetail = value;

                if (this._FileContentDetail == null || this._FileContentDetail.FileContentInfo == null)
                {

                }
                else
                {
                    this.txtDocumentImageID.Text = this._FileContentDetail.FileContentInfo._ID;
                    this.txtDocumentImageID.Visible = int.Parse(this._FileContentDetail.FileContentInfo._ID) > 0;
                    this.grpFileContent.Enabled = true;
                    this.PopulateFileContentList(this._FileContentDetail.FileContent);
                    this.numPageCount.Value = decimal.Parse(this._FileContentDetail.FileContentInfo.PageCount);
                    this.cmbEmbeddedFileType.SelectedItem = this._FileContentDetail.FileContentInfo.EmbeddedFileType.ToString();
                    this.txtActionCode.Text = this._FileContentDetail.FileContentInfo.ActionCode.ToString();
                }
            }
        }

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FileContentInfoControl()
        {
            InitializeComponent();
            foreach (string enumName in Enum.GetNames(typeof(EmbededFileTypes)))
            {
                this.cmbEmbeddedFileType.Items.Add(enumName);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Selection Changed event for EmbeddedFileType ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbEmbeddedFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbEmbeddedFileType.SelectedItem != null)
            {
                if (this._FileContentDetail.FileContentInfo != null)
                {
                    if (this._FileContentDetail.FileContentInfo.EmbeddedFileType != (EmbededFileTypes)Enum.Parse(typeof(EmbededFileTypes), (string)(this.cmbEmbeddedFileType.SelectedItem)))
                    {
                        this._FileContentDetail.FileContentInfo.EmbeddedFileType = (EmbededFileTypes)Enum.Parse(typeof(EmbededFileTypes), (string)(this.cmbEmbeddedFileType.SelectedItem));
                        this._FileContentDetail.FileContentInfo.ActionCode =
                            (this._FileContentDetail.FileContentInfo.ActionCode == ActionCode.New) ? ActionCode.New : ActionCode.Edit;
                        this.txtActionCode.Text = this._FileContentDetail.FileContentInfo.ActionCode.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Value Changed event for the PageCount Numeric TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numPageCount_ValueChanged(object sender, EventArgs e)
        {
            if (this._FileContentDetail.FileContentInfo != null)
            {

                if (numPageCount.Value.ToString() != this._FileContentDetail.FileContentInfo.PageCount)
                {
                    this._FileContentDetail.FileContentInfo.PageCount = numPageCount.Value.ToString();
                    this._FileContentDetail.FileContentInfo.ActionCode = (this._FileContentDetail.FileContentInfo.ActionCode == ActionCode.New) ? ActionCode.New : ActionCode.Edit;
                    this.txtActionCode.Text = this._FileContentDetail.FileContentInfo.ActionCode.ToString();
                }
            }
        }

        /// <summary>
        /// Click event for Replace File button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnReplaceFile_Click(object sender, EventArgs e)
        {
            LoadingForm loadingForm = new LoadingForm();
            try
            {
                // replace all files
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string[] filePaths = ofd.FileNames;
                    this._FileContentDetail.FileContent = new FileContent[filePaths.Length];
                    this.cmbEmbeddedFileType.SelectedItem = this._FileContentDetail.FileContentInfo.EmbeddedFileType.ToString();
                    this.numPageCount.Value = decimal.Parse(this._FileContentDetail.FileContentInfo.PageCount);
                    this._FileContentDetail.FileContentInfo.ActionCode = (this._FileContentDetail.FileContentInfo.ActionCode == ActionCode.New) ? ActionCode.New : ActionCode.Edit;
                    this.txtActionCode.Text = this._FileContentDetail.FileContentInfo.ActionCode.ToString();
                    loadingForm.DisplayText = string.Format("Encoding File{0} into Base 64", filePaths.Length == 1 ? string.Empty : "s");
                    loadingForm.Show(this);
                    int counter = 0;
                    foreach (string filePath in filePaths)
                    {
                        // encode file
                        //====================================
                        string encodedValue = await TestController.EncodeBase64Async(filePath);
                        // shove encoded value into FileContent
                        //====================================
                        await Task.Run(() =>
                            {
                                this._FileContentDetail.FileContent[counter] = new FileContent()
                                {
                                    Encoding = EncodingTypes.Base64,
                                    FileBuffer = encodedValue,
                                    _Sequence = counter++.ToString()
                                };
                            });
                    }
                    this.PopulateFileContentList(this._FileContentDetail.FileContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR");
            }
            finally
            {
                loadingForm.Hide();
                loadingForm.Dispose();
            }
        }

        /// <summary>
        /// Click event for Save File button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSaveFile_Click(object sender, EventArgs e)
        {
            LoadingForm loadingForm = new LoadingForm();
            try
            {
                if (this.lstFileContents.SelectedItems != null && this.lstFileContents.SelectedItems.Count == 1)
                {
                    // only 1 file selected

                    // find selected FileContent in array
                    FileContent selectedFileContent = null;
                    foreach (FileContent fileContent in this._FileContentDetail.FileContent)
                    {
                        if (fileContent._Sequence == this.lstFileContents.SelectedItem.ToString())
                        {
                            selectedFileContent = fileContent;
                            continue;
                        }
                    }

                    // save file dialog
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.DefaultExt = "tif";
                    sfd.Filter = "TIF Image files (*.tif)|*.tif|All files (*.*)|*.*";
                    sfd.AddExtension = true;
                    sfd.OverwritePrompt = true;

                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        string filePath = sfd.FileName;
                        // do decode
                        loadingForm.DisplayText = "Decoding file";
                        await TestController.DecodeBase64File(selectedFileContent.FileBuffer, filePath);
                        loadingForm.Hide();
                        MessageBox.Show(this, string.Format("Successfully saved to file [{0}]", filePath), "File Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR");
            }
            finally
            {
                loadingForm.Hide();
                loadingForm.Dispose();
            }
        }
        
        #endregion

        #region Helper Functions

        /// <summary>
        /// Reread data source
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();
            this.FileContentDetail = this._FileContentDetail;
        }

        /// <summary>
        /// Enable/disable editing
        /// </summary>
        /// <param name="enable">bool, true to enable</param>
        public void EnableEdit(bool enable)
        {
            //this.lstFileContents.Enabled = 
            //    this.btnSaveFile.Enabled = 
            //        (this._FileContentDetail.FileContent != null &&
            //         this._FileContentDetail.FileContent.Length > 0);
            this.cmbEmbeddedFileType.Enabled =
                
                this.numPageCount.Enabled = enable;
        }

        /// <summary>
        /// Enable/disable file addition/replacement
        /// </summary>
        /// <param name="enable">bool, true to enable</param>
        public void EnableFileReplace(bool enable)
        {
            this.btnReplaceFile.Enabled = enable;
        }

        /// <summary>
        /// Add items to FileContent ListBox
        /// </summary>
        /// <param name="fileContentArray"></param>
        private void PopulateFileContentList(FileContent[] fileContentArray)
        {
            this.lstFileContents.Items.Clear();
            if (fileContentArray != null)
            {
                foreach (FileContent fileContent in fileContentArray)
                {
                    this.lstFileContents.Items.Add(fileContent._Sequence);
                }
                this.btnSaveFile.Enabled = fileContentArray.Length > 0;
                this.lstFileContents.Visible = fileContentArray.Length > 0;
                this.lblFileContentWarning.Visible = !(fileContentArray.Length > 0);
            }
            else
            {
                this.btnSaveFile.Enabled = false;
                this.lstFileContents.Visible = false;
                this.lblFileContentWarning.Visible = true;
            }
        }
     
        #endregion
    }
}

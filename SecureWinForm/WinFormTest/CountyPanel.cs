using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using SECURE.Common.Controller;


namespace WinFormTest
{
    public partial class CountyPanel : UserControl
    {
        #region Declarations

        private HttpResponseMessage _ResponseMessage;
        private HttpRequestMessage _RequestMessage;
        private string _ResponseMessageContent = string.Empty;
        private string _RequestMessageContent = string.Empty;
        private string _ResponseMessageString = string.Empty;
        private string _RequestMessageString = string.Empty;
        // Used to keep track of the last node selected in the tree.
        private TreeNode _lastSelectedNode = null;
        // Contains the submission ID of the batch.
        //private string _batch_submissionID = null;
        // Contains the sequence of the document within the batch.
        //private string _document_sequence = null;
        // Controller used  with the form.
        private TestController _testController = new TestController(UserType.County);
        // Contains the data associated with counties.
        private CountiesInfo _cache_countiesInfo;
        // Contains the data associated with cities.
        private CitiesDetail _cache_citiesDetail;
        // Contains the data associated with titles.
        private TitlesDetail _cache_titlesDetail;
        // Contains the data associated with process queues.
        private ProcessQueuesDetail _cache_processQueuesDetail;
        // Contains the data associated with Submitting Parties
        private SubmittingPartiesInfo _cache_submittingPartiesInfo;
        // Contains the detail of the current batch.
        private List<BatchDetail> _workSet_BatchDetail = new List<BatchDetail>();
        // Contains the detail of the current document.
        private List<DocumentDetail> _workSet_DocumentDetail = new List<DocumentDetail>();
        // Enumeration describing the different search options.
        private enum SearchBy
        {
            None,
            BatchID,
            SubmissionID,
            UserName,
            StatusCodeAndDateRange,
            UserNameStatusCodeAndDateRange,
            BatchName,
            UserNameCounty,
            UserNameStatusCodeCounty,
            StatusCodeDateRangeCounty
        }
        private SearchBy _searchBy;
        /// <summary>
        /// Enumeration describing the Document File Type.
        /// </summary>
        private enum DocumentFileType
        {
            Recordable,
            PCOR,
            Other
        }
        private bool _ConfiguringSearch = false;

        private bool _enableValidation = false;

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Public property containing the request message string.
        /// </summary>
        public string RequestMessageString
        {
            get
            {
                return this._RequestMessageString;
            }
        }

        /// <summary>
        /// Public property containing the response message string.
        /// </summary>
        public string ResponseMessageString
        {
            get
            {
                return this._ResponseMessageString;
            }
        }

        /// <summary>
        /// Public property containing the request message content.
        /// </summary>
        public string RequestMessageContent
        {
            get
            {
                return this._RequestMessageContent;
            }
        }

        /// <summary>
        /// Public property containing the response message content.
        /// </summary>
        public string ResponseMessageContent
        {
            get
            {
                return this._ResponseMessageContent;
            }
        }

        /// <summary>
        /// Public property containing the request message.
        /// </summary>
        public HttpRequestMessage RequestMessage
        {
            get
            {
                return this._RequestMessage;
            }
        }

        /// <summary>
        /// Public property containing the response message.
        /// </summary>
        public HttpResponseMessage ResponseMessage
        {
            get
            {
                return this._testController.Last_HTTPResponse;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for the County Panel
        /// </summary>
        public CountyPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Events

        #region SECURE User Operations group buttons

        /// <summary>
        /// Button click event for the Accept Document button
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private async void btnOP_AcceptDocument_Click(object sender, EventArgs e)
        {
            await this.ChangeDocumentStatus(DocumentMediaStatusCode.Recorded);
        }
        /// <summary>
        /// Button click event for the Reject Document button.
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private async void btnOP_RejectDocument_Click(object sender, EventArgs e)
        {
            await this.ChangeDocumentStatus(DocumentMediaStatusCode.Rejected);
        }
        /// <summary>
        /// Button click event for the Submit button.
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private async void btnOP_Submit_Click(object sender, EventArgs e)
        {
            this.LoadingBarShow("Submitting Batches");

            BatchDetail selectedBatch = ((BatchDetail)(this._lastSelectedNode.Tag.GetType() == typeof(BatchDetail) ? this._lastSelectedNode.Tag : this._lastSelectedNode.Parent.Tag));

            // Update in SECURE with status Uploading
            if (await this.SaveBatchToSECURE(
                selectedBatch._SubmissionID,
                true))
            {
                // Send Batch XML on Batch/UploadComplete
                await this.UploadBatchComplete(selectedBatch._SubmissionID);

                // Done, set up form
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
                this.LoadingBarHide();

                MessageBox.Show(this, "Submission Complete", "SECURE Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.LoadingBarHide();

                MessageBox.Show(this, "Submission Failed", "SECURE Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.ConfigureOPbuttons(null);
        }
        /// <summary>
        /// Button click event for the CheckOutBatch button.
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private async void btnOP_CheckOutBatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.trvBatches.SelectedNode.Tag.GetType() == typeof(BatchDetail))
                {
                    BatchDetail batchDetail = (BatchDetail)this._lastSelectedNode.Tag;

                    this.LoadingBarShow("Checking Out Batch");
                    string responseText = await this.PerformHTTP_Async(
                        HTTP.PUT,
                        new string[] { "Batch", "CheckOut" },
                        new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID)
                            },
                        null,
                        true);
                    if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                        this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                    {
                        BatchDetail resultObject = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                        this._workSet_BatchDetail.Remove(batchDetail);
                        this._workSet_BatchDetail.Add(resultObject);
                        this.LoadTree(this._workSet_BatchDetail);
                        this.batchDetailControl.Visible = false;
                        this.documentInfoControl.Visible = false;
                        this.fileContentInfoControl_RECORDABLE.Visible = false;
                        this.fileContentInfoControl_PCOR.Visible = false;
                        this.fileContentInfoControl_OTHER.Visible = false;
                    }
                    else
                    {
                        this.LoadingBarHide();
                        this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Button click event for the View Batch button.
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private async void btnOP_ViewBatch_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Retrieving Batches");

                this._workSet_BatchDetail.Clear();

                switch (this._searchBy)
                {
                    case SearchBy.BatchID:
                        {
                            if (this.chkBatchWithDocumentsDownload.Checked)
                            {
                                // send to SECURE
                                string response = await this.PerformHTTP_Async(
                                        HTTP.GET,
                                        new string[] { "BatchWithDocuments", "ReadByID" },
                                        new DictionaryEntry[] 
                                {
                                    new DictionaryEntry("BatchID", this.txtViewBatchSearch_input.Text.Trim())
                                },
                                        null,
                                        true);
                                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                       this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                                {
                                    this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                                }
                                else
                                {
                                    BatchWithDocumentsDetail batchwithDocumentsDetail = TestController.DeserializeXML<BatchWithDocumentsDetail>(response, this._enableValidation);
                                    this._workSet_BatchDetail.Add(batchwithDocumentsDetail.Batch);
                                    this._workSet_DocumentDetail.AddRange(batchwithDocumentsDetail.Documents);
                                }
                            }
                            else
                            {
                                string response =
                                    await this.PerformHTTP_Async(
                                        HTTP.GET,
                                        new string[] { "Batch", "ReadByID" },
                                        new DictionaryEntry[] 
                                    { 
                                        
                                        new DictionaryEntry("BatchID", this.txtViewBatchSearch_input.Text.Trim())
                                    },
                                        null,
                                        true);
                                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                        this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                                {
                                    this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                                }
                                else
                                {
                                    BatchDetail batchDetail = TestController.DeserializeXML<BatchDetail>(response, this._enableValidation);
                                    this._workSet_BatchDetail.Add(batchDetail);
                                }
                            }
                            break;
                        }
                    case SearchBy.StatusCodeAndDateRange:
                        {
                            string response =
                              await this.PerformHTTP_Async(
                              HTTP.GET,
                              new string[] { "Batch", "ReadByStatusCodeDateRange" },
                              new DictionaryEntry[] 
                              { 
                                 
                                  new DictionaryEntry("BatchStatus", this.cboViewBatch_Status.SelectedItem.ToString()),
                                  new DictionaryEntry("StartDate", this.dtpBatchSearch_start.Value.ToShortDateString()),
                                  new DictionaryEntry("EndDate", this.dtpBatchSearch_end.Value.ToShortDateString())
                              },
                              null,
                              true);
                            if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                       this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                            }
                            else
                            {
                                BatchesDetail batchesDetail = TestController.DeserializeXML<BatchesDetail>(response, this._enableValidation);
                                if (batchesDetail.Batch != null)
                                {
                                    foreach (BatchDetail batchDetail in batchesDetail.Batch)
                                    {
                                        this._workSet_BatchDetail.Add(batchDetail);
                                    }
                                }
                            }
                            break;
                        }
                    case SearchBy.SubmissionID:
                        {
                            string response =
                                await this.PerformHTTP_Async(
                                    HTTP.GET,
                                    new string[] { "Batch", "ReadBySubmissionID" },
                                    new DictionaryEntry[] 
                                    { 
                                        
                                        new DictionaryEntry("SubmissionID", this.txtViewBatchSearch_input.Text.Trim()) 
                                    },
                                    null,
                                    true);
                            if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                       this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                            }
                            else
                            {
                                BatchDetail batchDetail = TestController.DeserializeXML<BatchDetail>(response, this._enableValidation);
                                this._workSet_BatchDetail.Add(batchDetail);
                            }
                            break;
                        }
                    case SearchBy.UserName:
                        {
                            string response =
                                await this.PerformHTTP_Async(
                                HTTP.GET,
                                new string[] { "Batch", "ReadByUserName" },
                                new DictionaryEntry[] 
                                { 
                                    
                                    new DictionaryEntry("UserName", this.txtViewBatchSearch_input.Text.Trim()) 
                                },
                                null,
                                true);
                            if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                       this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                            }
                            else
                            {
                                BatchesDetail batchesDetail = TestController.DeserializeXML<BatchesDetail>(response, this._enableValidation);
                                if (batchesDetail.Batch != null)
                                {
                                    foreach (BatchDetail batchDetail in batchesDetail.Batch)
                                    {
                                        this._workSet_BatchDetail.Add(batchDetail);
                                    }
                                }
                            }
                            break;
                        }
                    case SearchBy.UserNameStatusCodeAndDateRange:
                        {
                            string response = await this.PerformHTTP_Async(
                              HTTP.GET,
                              new string[] { "Batch", "ReadByUserStatusCodeDateRange" },
                              new DictionaryEntry[] 
                              { 
                                  
                                  new DictionaryEntry("UserName", this.txtViewBatchSearch_input.Text.Trim()),
                                  new DictionaryEntry("BatchStatus", this.cboViewBatch_Status.SelectedItem.ToString()),
                                  new DictionaryEntry("StartDate", this.dtpBatchSearch_start.Value.ToShortDateString()),
                                  new DictionaryEntry("EndDate", this.dtpBatchSearch_end.Value.ToShortDateString())
                              },
                              null,
                              true);
                            if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                 this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                            }
                            else
                            {
                                BatchesDetail batchesDetail = TestController.DeserializeXML<BatchesDetail>(response, this._enableValidation);
                                if (batchesDetail.Batch != null)
                                {
                                    foreach (BatchDetail batchDetail in batchesDetail.Batch)
                                    {
                                        this._workSet_BatchDetail.Add(batchDetail);
                                    }
                                }
                            }
                            break;
                        }
                    case SearchBy.BatchName:
                        {
                            string response = await this.PerformHTTP_Async(
                                 HTTP.GET,
                                 new string[] { "Batch", "ReadByBatchName" },
                                 new DictionaryEntry[] 
                              { 
                                  
                                  new DictionaryEntry("BatchName", this.txtViewBatchSearch_input.Text.Trim().Replace("[","[[]"))
                              },
                                 null,
                                 true);
                            if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                 this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                            }
                            else
                            {
                                BatchesDetail batchesDetail = TestController.DeserializeXML<BatchesDetail>(response, this._enableValidation);
                                if (batchesDetail.Batch != null)
                                {
                                    foreach (BatchDetail batchDetail in batchesDetail.Batch)
                                    {
                                        this._workSet_BatchDetail.Add(batchDetail);
                                    }
                                }
                            }
                            break;
                        }
                    case SearchBy.UserNameCounty:
                        {
                            string response = await this.PerformHTTP_Async(
                                    HTTP.GET,
                                    new string[] { "Batch", "ReadByUserNameCountyID" },
                                    new DictionaryEntry[] 
                              { 
                                  
                                  new DictionaryEntry("UserName", this.txtViewBatchSearch_input.Text.Trim()), 
                                  new DictionaryEntry("CountyID", this.GetCountyInfo(this.cboCounty.SelectedItem.ToString())._ID)
                              },
                                    null,
                                    true);
                            if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                 this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                            }
                            else
                            {
                                BatchesDetail batchesDetail = TestController.DeserializeXML<BatchesDetail>(response, this._enableValidation);
                                if (batchesDetail.Batch != null)
                                {
                                    foreach (BatchDetail batchDetail in batchesDetail.Batch)
                                    {
                                        this._workSet_BatchDetail.Add(batchDetail);
                                    }
                                }
                            }
                            break;
                        }
                    case SearchBy.UserNameStatusCodeCounty:
                        {
                            string response = await this.PerformHTTP_Async(
                                    HTTP.GET,
                                    new string[] { "Batch", "ReadByUserStatusCodeDateRangeCountyID" },
                                    new DictionaryEntry[] 
                              { 
                                  
                                  new DictionaryEntry("UserName", this.txtViewBatchSearch_input.Text.Trim()), 
                                  new DictionaryEntry("BatchStatus", this.cboViewBatch_Status.SelectedItem.ToString()),
                                  new DictionaryEntry("StartDate", this.dtpBatchSearch_start.Value.ToShortDateString()),
                                  new DictionaryEntry("EndDate", this.dtpBatchSearch_end.Value.ToShortDateString()),
                                  new DictionaryEntry("CountyID", this.GetCountyInfo(this.cboCounty.SelectedItem.ToString())._ID)
                              },
                                    null,
                                    true);
                            if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                 this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                            }
                            else
                            {
                                BatchesDetail batchesDetail = TestController.DeserializeXML<BatchesDetail>(response, this._enableValidation);
                                if (batchesDetail.Batch != null)
                                {
                                    foreach (BatchDetail batchDetail in batchesDetail.Batch)
                                    {
                                        this._workSet_BatchDetail.Add(batchDetail);
                                    }
                                }
                            }
                            break;
                        }
                    case SearchBy.StatusCodeDateRangeCounty:
                        {
                            string response = await this.PerformHTTP_Async(
                                      HTTP.GET,
                                      new string[] { "Batch", "ReadByStatusCodeDateRangeCountyID" },
                                      new DictionaryEntry[] 
                              { 
                                  
                                  new DictionaryEntry("BatchStatus", this.cboViewBatch_Status.SelectedItem.ToString()),
                                  new DictionaryEntry("StartDate", this.dtpBatchSearch_start.Value.ToShortDateString()),
                                  new DictionaryEntry("EndDate", this.dtpBatchSearch_end.Value.ToShortDateString()),
                                  new DictionaryEntry("CountyID", this.GetCountyInfo(this.cboCounty.SelectedItem.ToString())._ID)
                              },
                                      null,
                                      true);
                            if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                 this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                            }
                            else
                            {
                                BatchesDetail batchesDetail = TestController.DeserializeXML<BatchesDetail>(response, this._enableValidation);
                                if (batchesDetail.Batch != null)
                                {
                                    foreach (BatchDetail batchDetail in batchesDetail.Batch)
                                    {
                                        this._workSet_BatchDetail.Add(batchDetail);
                                    }
                                }
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                if (this._workSet_BatchDetail.Count == 0)
                {
                    this.LoadingBarHide();
                    MessageBox.Show(this, "No matching batches found", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (this._searchBy != SearchBy.BatchID && this.chkBatchWithDocumentsDownload.Checked)
                    {
                        foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                        {
                            this._workSet_DocumentDetail.AddRange(
                                (await this.BatchWithDocumentsReadByID(batchDetail._ID)).Documents);
                        }
                    }
                    this.LoadTree(this._workSet_BatchDetail);
                    this.batchDetailControl.Visible = false;
                    this.documentInfoControl.Visible = false;
                    this.fileContentInfoControl_RECORDABLE.Visible = false;
                    this.fileContentInfoControl_PCOR.Visible = false;
                    this.fileContentInfoControl_OTHER.Visible = false;
                }
            }
            catch (Exception ex)
            {
                this.LoadingBarHide();
                this.HandleException(ex);
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Click event for download batch button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOP_DownloadBatch_Click(object sender, EventArgs e)
        {
            this.mnuDownloadBatch.PerformClick();
        }
        /// <summary>
        /// Click event for download batches button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOP_DownloadBatches_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Downloading Batches");

                this._workSet_BatchDetail.Clear();
                this._workSet_DocumentDetail.Clear();

                #region  Download BatchDetails
                //=============================================================================================================

                string batchStatus = this.cboDownloadBatch_Status.SelectedValue.ToString();
                string responseText_Download = await this.PerformHTTP_Async(
                    HTTP.GET,
                    new string[] 
                    { 
                        "Batch", 
                        this.chkBatchDownload_IncludeCheckedOut.Checked ? "DownloadCheckedOut" : "Download" 
                    },
                    this.chkBatchDownload_IncludeCheckedOut.Checked ?
                    (
                        new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchStatus", batchStatus),
                                new DictionaryEntry("BypassCheckedOut", bool.FalseString)
                            }
                    ) :
                    (
                        new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchStatus", batchStatus)
                            }
                    ),
                    null,
                    true);
                if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    BatchesDetail resultBatchDetail = TestController.DeserializeXML<BatchesDetail>(responseText_Download, this._enableValidation);
                    if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                     this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                    {
                        this.LoadingBarHide();
                        this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                        return;
                    }
                    else
                    {
                        if (resultBatchDetail.Batch == null || resultBatchDetail.Batch.Length == 0)
                        {
                            this.LoadingBarHide();
                            MessageBox.Show(this, "No Batches Found");
                            return;
                        }
                        foreach (BatchDetail batchDetail in resultBatchDetail.Batch)
                        {
                            this._workSet_BatchDetail.Add(batchDetail);
                        }
                    }
                }
                else
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                    return;
                }
                #endregion

                #region Check Out Batches
                //=================================================================================================================
                if (this.chkBatchDownload_CheckOut.Checked)
                {
                    this.LoadingBarShow("Checking Out Batches");
                    List<BatchDetail> checkedOutBatchDetails = new List<BatchDetail>();
                    int checkOutCounter = 1;
                    int totalCount = this._workSet_BatchDetail.Count;
                    foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                    {
                        if (batchDetail.CheckedOutBy == null)
                        {
                            this.LoadingBarShow(string.Format("Checking Out Batch [{0}/{1}]", checkOutCounter, totalCount));
                            string responseText_CheckOut = await this.PerformHTTP_Async(
                                HTTP.PUT,
                                new string[] { "Batch", "CheckOut" },
                                new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID)
                            },
                                null,
                                true);
                            if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                BatchDetail resultObject = TestController.DeserializeXML<BatchDetail>(responseText_CheckOut, this._enableValidation);
                                checkedOutBatchDetails.Add(resultObject);
                                checkOutCounter++;
                            }
                            else
                            {
                                checkedOutBatchDetails.Add(batchDetail);
                            }
                        }
                        else
                        {
                            checkedOutBatchDetails.Add(batchDetail);
                            totalCount--;
                        }
                    }
                    this._workSet_BatchDetail = checkedOutBatchDetails;
                    if (this._workSet_BatchDetail.Count == 0)
                    {
                        this.LoadingBarHide();
                        MessageBox.Show(this, "All found batches are already checked out by other users.");
                        return;
                    }
                }
                #endregion

                #region  Download all documents
                //=================================================================================================================
                this.LoadingBarShow("Downloading Documents");
                int downloadCount = 0;
                int totalDownloads = 0;
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                    {
                        totalDownloads++;
                    }
                }
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                    {
                        this.LoadingBarShow(string.Format("Downloading Documents[{0}/{1}]", ++downloadCount, totalDownloads));
                        DocumentDetail documentDetail = await this.DownloadDocument(documentInfo._ID);
                        if (documentDetail != null)
                        {
                            this._workSet_DocumentDetail.Add(documentDetail);
                        }
                    }
                }
                #endregion

                #region  Batch/DownloadComplete
                //=================================================================================================================
                if (this.chkBatchDownload_SetComplete.Checked)
                {
                    this.LoadingBarShow("Setting Batch Download Complete");
                    List<BatchDetail> _workset_BatchDetail_DownloadComplete = new List<BatchDetail>();
                    int downloadCompletecounter = 0;
                    int totalDownloadComplete = this._workSet_BatchDetail.Count;
                    foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                    {
                        if (batchDetail.StatusCode == BatchMediaStatusCode.Uploaded_By_Submitter || batchDetail.StatusCode == BatchMediaStatusCode.Corrected_Uploaded_By_Submitter)
                        {
                            this.LoadingBarShow(string.Format("Setting Batch Download Complete [{0}/{1}]", ++downloadCompletecounter, totalDownloadComplete));
                            BatchMediaStatusCode completeStatus = batchDetail.StatusCode == BatchMediaStatusCode.Uploaded_By_Submitter ? BatchMediaStatusCode.Downloaded_By_County : BatchMediaStatusCode.Corrected_Downloaded_By_County;
                            string responseText = await this.PerformHTTP_Async(
                                HTTP.PUT,
                                new string[] { "Batch", "DownloadComplete" },
                                new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID),
                                new DictionaryEntry("BatchStatus", completeStatus.ToString())
                            },
                                null,
                                true);
                            BatchDetail resultBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                            _workset_BatchDetail_DownloadComplete.Add(resultBatchDetail);
                        }
                        else
                        {
                            _workset_BatchDetail_DownloadComplete.Add(batchDetail);
                            totalDownloadComplete--;
                        }
                    }
                    this._workSet_BatchDetail = _workset_BatchDetail_DownloadComplete;
                }
                #endregion
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Click event for Correction button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOP_Correction_Click(object sender, EventArgs e)
        {
            await this.ChangeDocumentStatus(DocumentMediaStatusCode.Correction);
        }
        private async void btnOP_Expire_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Expiring Batch");
                // Input box to get reason for expiring
                InputForm inputForm = new InputForm(new DictionaryEntry[] { new DictionaryEntry("ExpirationMessage", "Expiration message goes here.") });
                if (inputForm.ShowDialog(this) == DialogResult.OK)
                {
                    string expireMessage = inputForm.Parameters[0].Value.ToString();
                    //peform expire
                    await this.ExpireBatch((BatchDetail)this._lastSelectedNode.Tag, expireMessage);
                    this.ConfigureOPbuttons(null);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        private async void btnOP_CheckInBatch_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.trvBatches.SelectedNode.Tag.GetType() == typeof(BatchDetail))
                {
                    this.LoadingBarShow("Checking In Batch");
                    BatchDetail batchDetail = (BatchDetail)this._lastSelectedNode.Tag;

                    string responseText = await this.PerformHTTP_Async(
                        HTTP.PUT,
                        new string[] { "Batch", "CheckIn" },
                        new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID)
                            },
                        null,
                        false);
                    if (TestController.DeserializeXML<ResultDetail>(responseText, this._enableValidation).Value)
                    {
                        BatchDetail resultObject = TestController.DeserializeXML<BatchDetail>(
                            await this.PerformHTTP_Async(
                                HTTP.GET,
                                new string[] { "Batch", "ReadByID" },
                                new DictionaryEntry[] 
                                {
                                   
                                    new DictionaryEntry("BatchID", batchDetail._ID)
                                },
                                null,
                                true),
                            this._enableValidation);
                        this._workSet_BatchDetail.Remove(batchDetail);
                        this._workSet_BatchDetail.Add(resultObject);
                        this.LoadTree(this._workSet_BatchDetail);
                        this.batchDetailControl.Visible = false;
                        this.documentInfoControl.Visible = false;
                        this.fileContentInfoControl_RECORDABLE.Visible = false;
                        this.fileContentInfoControl_PCOR.Visible = false;
                        this.fileContentInfoControl_OTHER.Visible = false;
                    }
                    else
                    {
                        if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                            this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                        {
                            this.LoadingBarHide();
                            MessageBox.Show(this, string.Format("CheckIn: [{0}]", false), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            this.LoadingBarHide();
                            this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        private void btnOP_Clear_Click(object sender, EventArgs e)
        {
            // Clear saved BatchDetail and DocumentDetail. Reset Treeview
            this._workSet_DocumentDetail.Clear();
            this._workSet_BatchDetail.Clear();
            this.trvBatches.Nodes.Clear();
            // Reset control values
            this.batchDetailControl.BatchDetail = null;
            this.documentInfoControl.DocumentInfo = null;
            this.fileContentInfoControl_RECORDABLE.FileContentDetail = null;
            this.fileContentInfoControl_PCOR.FileContentDetail = null;
            this.fileContentInfoControl_OTHER.FileContentDetail = null;
            // Set controls invisible
            this.batchDetailControl.Visible = false;
            this.documentInfoControl.Visible = false;
            this.fileContentInfoControl_RECORDABLE.Visible = false;
            this.fileContentInfoControl_PCOR.Visible = false;
            this.fileContentInfoControl_OTHER.Visible = false;
            this.ConfigureOPbuttons(null);
        }
        private async void btnOP_MyBatches_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Downloading Batches");

                this._workSet_BatchDetail.Clear();
                this._workSet_DocumentDetail.Clear();
                this.btnOP_Clear.PerformClick();

                #region  Download BatchDetails
                //=============================================================================================================

                string batchStatus = this.cboDownloadBatch_Status.SelectedValue.ToString();
                string responseText_Download = null;
                try
                {
                    responseText_Download = await this.PerformHTTP_Async(
                          HTTP.GET,
                          new string[] { "Batch", "ReadByUserName" },
                          new DictionaryEntry[] 
                    { 
                        
                        new DictionaryEntry("UserName", TestController.Default_UserName_County) 
                    },
                          null,
                          true);
                    if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                        this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                    {
                        BatchesDetail resultBatchDetail = TestController.DeserializeXML<BatchesDetail>(responseText_Download, this._enableValidation);
                        if (resultBatchDetail.Batch == null || resultBatchDetail.Batch.Length == 0)
                        {
                            this.LoadingBarHide();
                            MessageBox.Show(this, "No Batches Found");
                            return;
                        }
                        foreach (BatchDetail batchDetail in resultBatchDetail.Batch)
                        {
                            this._workSet_BatchDetail.Add(batchDetail);
                        }
                    }
                    else
                    {
                        this.LoadingBarHide();
                        this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                        return;
                    }
                }
                finally
                {
                    responseText_Download = null;
                    GC.Collect();
                }
                #endregion

                #region  Download all Document Images
                //=================================================================================================================
                this.LoadingBarShow("Downloading Documents");
                int downloadCount = 0;
                int totalDownloads = 0;
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                    {
                        totalDownloads++;
                    }
                }
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                    {
                        this.LoadingBarShow(string.Format("Downloading Documents[{0}/{1}]", ++downloadCount, totalDownloads));
                        DocumentDetail documentDetail = null;
                        try
                        {
                            documentDetail = await this.DownloadDocument(documentInfo._ID);
                            if (documentDetail != null)
                            {
                                this._workSet_DocumentDetail.Add(documentDetail);
                            }
                        }
                        finally
                        {
                            documentDetail = null;
                            GC.Collect();
                        }
                    }
                }
                #endregion

                #region  Batch/DownloadComplete
                //=================================================================================================================
                if (this.chkBatchDownload_SetComplete.Checked)
                {
                    this.LoadingBarShow("Setting Batch Download Complete");
                    List<BatchDetail> _workset_BatchDetail_DownloadComplete = new List<BatchDetail>();
                    int downloadCompletecounter = 0;
                    int totalDownloadComplete = this._workSet_BatchDetail.Count;
                    foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                    {
                        if (batchDetail.StatusCode == BatchMediaStatusCode.Uploaded_By_Submitter || batchDetail.StatusCode == BatchMediaStatusCode.Corrected_Uploaded_By_Submitter)
                        {
                            this.LoadingBarShow(string.Format("Setting Batch Download Complete [{0}/{1}]", ++downloadCompletecounter, totalDownloadComplete));
                            BatchMediaStatusCode completeStatus = batchDetail.StatusCode == BatchMediaStatusCode.Uploaded_By_Submitter ? BatchMediaStatusCode.Downloaded_By_County : BatchMediaStatusCode.Corrected_Downloaded_By_County;
                            string responseText = await this.PerformHTTP_Async(
                                HTTP.PUT,
                                new string[] { "Batch", "DownloadComplete" },
                                new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID),
                                new DictionaryEntry("BatchStatus", completeStatus.ToString())
                            },
                                null,
                                true);
                            BatchDetail resultBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                            _workset_BatchDetail_DownloadComplete.Add(resultBatchDetail);
                        }
                        else
                        {
                            _workset_BatchDetail_DownloadComplete.Add(batchDetail);
                            totalDownloadComplete--;
                        }
                    }
                    this._workSet_BatchDetail = _workset_BatchDetail_DownloadComplete;
                }
                #endregion
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
                this.LoadingBarHide();
            }
        }

        #endregion

        #region SECURE API group buttons
    
        /// <summary>
        /// Button click event for API: Batch/DownloadComplete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAPI_BatchDownloadComplete_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("/api/Batch/DownloadComplete");
                List<BatchDetail> _workset_BatchDetail_DownloadComplete = new List<BatchDetail>();
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    if (batchDetail.StatusCode == BatchMediaStatusCode.Uploaded_By_Submitter || batchDetail.StatusCode == BatchMediaStatusCode.Corrected_Uploaded_By_Submitter)
                    {
                        BatchMediaStatusCode completeStatus = batchDetail.StatusCode == BatchMediaStatusCode.Uploaded_By_Submitter ? BatchMediaStatusCode.Downloaded_By_County : BatchMediaStatusCode.Corrected_Downloaded_By_County;
                        string responseText = await this.PerformHTTP_Async(
                            HTTP.PUT,
                            new string[] { "Batch", "DownloadComplete" },
                            new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID),
                                new DictionaryEntry("BatchStatus", completeStatus.ToString())
                            },
                            null,
                            true);
                        BatchDetail resultBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                        _workset_BatchDetail_DownloadComplete.Add(resultBatchDetail);
                    }
                    else
                    {
                        _workset_BatchDetail_DownloadComplete.Add(batchDetail);
                    }
                }
                this._workSet_BatchDetail = _workset_BatchDetail_DownloadComplete;
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Click event for API: Batch/Upload button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAPI_BatchUpload_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("SECURE API/Batch/Upload");
                Hashtable tempNewBatchNumbers = new Hashtable(new Dictionary<string, string>());
                Hashtable tempNewDocumentNumbers = new Hashtable(new Dictionary<string, string>());
                List<BatchDetail> temp_workset_BatchDetail = new List<BatchDetail>();
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    if (int.Parse(batchDetail._ID) > 0)
                    {
                        // Get new status code for Batch
                        this.LoadingBarHide();
                        BatchMediaStatusCode newStatus = batchDetail.StatusCode;
                        InputForm inputForm = new InputForm(new DictionaryEntry[] { new DictionaryEntry("BatchStatus", newStatus) });
                        if (inputForm.ShowDialog(this) == DialogResult.OK)
                        {
                            batchDetail.StatusCode = (BatchMediaStatusCode)Enum.Parse(typeof(BatchMediaStatusCode), inputForm.Parameters[0].Value.ToString());
                        }
                        this.LoadingBarShow("SECURE API/Batch/Upload");
                        // Determine action code for DocumentInfo
                        foreach (DocumentInfo documentInfo in batchDetail.Documents)
                        {
                            if (documentInfo.ActionCode == ActionCode.None)
                            {
                                if (documentInfo.RecordableFileContentInfo != null && documentInfo.RecordableFileContentInfo.ActionCode != ActionCode.None)
                                {
                                    documentInfo.ActionCode = ActionCode.Edit;
                                }
                                if (documentInfo.PCORFileContentInfo != null && documentInfo.PCORFileContentInfo.ActionCode != ActionCode.None)
                                {
                                    documentInfo.ActionCode = ActionCode.Edit;
                                }
                                if (documentInfo.OtherFileContentInfo != null && documentInfo.OtherFileContentInfo.ActionCode != ActionCode.None)
                                {
                                    documentInfo.ActionCode = ActionCode.Edit;
                                }
                            }
                            if (documentInfo.StatusCode == DocumentMediaStatusCode.Corrected)
                            {
                                // Change StatusCode to Uploaded, Corrected is invalid for correction batch
                                documentInfo.StatusCode = DocumentMediaStatusCode.Uploaded;
                            }
                        }
                        // Batch
                        string xml = TestController.SerializeXML(batchDetail, this._enableValidation);
                        // send to SECURE
                        string responseText = await this.PerformHTTP_Async(
                                HTTP.PUT,
                                new string[] { "Batch", "Upload" },
                                null,
                                new StringContent(
                                    TestController.SerializeXML(batchDetail, this._enableValidation),
                                    Encoding.UTF8,
                                    SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                                true);
                        if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                            this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                        {
                            this.LoadingBarHide();
                            this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                        }
                        else
                        {
                            // Saved batch response
                            BatchDetail createdBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                            temp_workset_BatchDetail.Add(createdBatchDetail);
                            tempNewBatchNumbers.Add(batchDetail._ID, createdBatchDetail._ID);
                            foreach (DocumentInfo documentInfo in batchDetail.Documents)
                            {
                                foreach (DocumentInfo createdDocumentInfo in createdBatchDetail.Documents)
                                {
                                    if (documentInfo.Sequence == createdDocumentInfo.Sequence)
                                    {
                                        tempNewDocumentNumbers.Add(documentInfo._ID, createdDocumentInfo._ID);
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        temp_workset_BatchDetail.Add(batchDetail);
                    }
                }
                this._workSet_BatchDetail = temp_workset_BatchDetail;
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
                this.LoadTree(this._workSet_BatchDetail);
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                  this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Click event for API: DocumentUpload button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAPI_DocumentUpload_Click(object sender, EventArgs e)
        {
            this.DocumentsUpload();
        }
        /// <summary>
        /// Click event for API: Batch/Uploadcomplete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAPI_BatchUploadComplete_Click(object sender, EventArgs e)
        {
            try
            {
                int count_Batches = 0;
                this.LoadingBarShow("SECURE API/Batch/UploadComplete");
                List<BatchDetail> temp_workset_BatchDetail = new List<BatchDetail>();
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    if (batchDetail.StatusCode == BatchMediaStatusCode.County_Uploading_Correction || batchDetail.StatusCode == BatchMediaStatusCode.County_Uploading_Completed)
                    {
                        count_Batches++;
                        // Get new status code for Batch
                        switch (batchDetail.StatusCode)
                        {
                            case BatchMediaStatusCode.County_Uploading_Completed:
                                {
                                    batchDetail.StatusCode = BatchMediaStatusCode.Completed_Uploaded_By_County;
                                    break;
                                }
                            case BatchMediaStatusCode.County_Uploading_Correction:
                                {
                                    batchDetail.StatusCode = BatchMediaStatusCode.Correction_Uploaded_By_County;
                                    break;
                                }
                        }
                        string responseText = await this.PerformHTTP_Async(
                            HTTP.PUT,
                            new string[] { "Batch", "UploadComplete" },
                            new DictionaryEntry[] 
                            {
                                new DictionaryEntry("BatchID", batchDetail._ID),
                                new DictionaryEntry("BatchStatus", batchDetail.StatusCode)
                            },
                            null,
                            true);
                        BatchDetail resultBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                        temp_workset_BatchDetail.Add(resultBatchDetail);
                    }
                    else
                    {
                        temp_workset_BatchDetail.Add(batchDetail);
                    }
                }
                this._workSet_BatchDetail = temp_workset_BatchDetail;
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                  this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Click event for API: Batch/Download button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAPI_BatchDownload_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("/api/Batch/Download");
                this._workSet_BatchDetail.Clear();
                this._workSet_DocumentDetail.Clear();
                // Download BatchDetails
                //=============================================================================================================
                string batchStatus = string.Empty;
                // Get new status code for Batch
                this.LoadingBarHide();
                InputForm inputForm = new InputForm(new DictionaryEntry[] { new DictionaryEntry("BatchStatus", BatchMediaStatusCode.Uploaded_By_Submitter) });
                if (inputForm.ShowDialog(this) == DialogResult.OK)
                {
                    batchStatus = inputForm.Parameters[0].Value.ToString();
                }
                else
                {
                    return;
                }
                string responseText_Download = await this.PerformHTTP_Async(
                    HTTP.GET,
                    new string[] 
                    { 
                        "Batch", 
                        "Download" 
                    },
                    new DictionaryEntry[] 
                        {
                            new DictionaryEntry("BatchStatus", batchStatus)
                        },
                    null,
                    true);
                if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    BatchesDetail resultBatchDetail = TestController.DeserializeXML<BatchesDetail>(responseText_Download, this._enableValidation);
                    if (resultBatchDetail.Batch == null || resultBatchDetail.Batch.Length == 0)
                    {
                        this.LoadingBarHide();
                        MessageBox.Show(this, "No Batches Found");
                        return;
                    }
                    foreach (BatchDetail batchDetail in resultBatchDetail.Batch)
                    {
                        this._workSet_BatchDetail.Add(batchDetail);
                    }
                }
                else
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                    return;
                }
                this.LoadTree(this._workSet_BatchDetail);
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Click event for API: Document/Download button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAPI_DocumentDownload_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("/api/Document/Download");
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                    {
                        string responseText = await this.PerformHTTP_Async(
                            HTTP.GET,
                            new string[] { "Document", "Download" },
                            new DictionaryEntry[] 
                            {
                                new DictionaryEntry("DocID", documentInfo._ID)
                            },
                            null,
                            true);
                        if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                            this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                        {
                            DocumentDetail resultDocumentDetail = TestController.DeserializeXML<DocumentDetail>(responseText, this._enableValidation);
                            DocumentDetail replaceThis = this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID);
                            if (replaceThis != null)
                            {
                                this._workSet_DocumentDetail.Remove(replaceThis);
                            }
                            this._workSet_DocumentDetail.Add(resultDocumentDetail);
                        }
                        else
                        {
                            this.LoadingBarHide();
                            this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        private async void btnAPI_BatchWithDocumentsReadByID_Click(object sender, EventArgs e)
        {
            try
            {
                InputForm inputForm = new InputForm(new string[] { "BatchID" });
                DialogResult DR = inputForm.ShowDialog(this);
                if (DR == DialogResult.OK)
                {
                    DictionaryEntry[] apiParameters = inputForm.Parameters;
                    this.LoadingBarShow(string.Format("/api/BatchWithDocuments/ReadByID?{0}", this._testController.CreateParameterString(apiParameters)));

                    BatchWithDocumentsDetail responseBatchDocuments = null;
                    // send to SECURE
                    string responseText = await this.PerformHTTP_Async(
                            HTTP.GET,
                            new string[] { "BatchWithDocuments", "ReadByID" },
                            apiParameters,
                            null,
                            true);
                    if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                     this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                    {
                        // Saved batch response
                        responseBatchDocuments = TestController.DeserializeXML<BatchWithDocumentsDetail>(responseText, this._enableValidation);

                        this._workSet_BatchDetail.Clear();
                        this._workSet_DocumentDetail.Clear();

                        this._workSet_BatchDetail.Add(responseBatchDocuments.Batch);
                        this._workSet_DocumentDetail.AddRange(responseBatchDocuments.Documents);
                    }
                    else
                    {
                        this.DisplayHTTPError(responseText);
                    }
                    this.LoadTree(this._workSet_BatchDetail);
                    this.batchDetailControl.Visible = false;
                    this.documentInfoControl.Visible = false;
                    this.fileContentInfoControl_RECORDABLE.Visible = false;
                    this.fileContentInfoControl_PCOR.Visible = false;
                    this.fileContentInfoControl_OTHER.Visible = false;
                }
            }
            catch (Exception ex)
            {
                this.LoadingBarHide();
                this.HandleException(ex);
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
    
        #endregion

        #region Authorization group buttons

        /// <summary>
        /// Button click event for the Logout button.
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAuth_Logout_Click(object sender, EventArgs e)
        {
            this.PerformLogout();
            this.enableFormControls(this._testController.SessionTokenID != null);
        }
        /// <summary>
        /// Button click event for the Login button
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private async void btnAuth_Login_Click(object sender, EventArgs e)
        {
            await this.PerformLoginAsync(TestController.TwoFactor_Enabled_County);
            this.enableFormControls(this._testController.SessionTokenID != null && this._testController.SessionTokenID != string.Empty);
            this.btnOP_Clear.PerformClick();
        }

        #endregion
        
        #region Context Menu

        /// <summary>
        /// Click event for Download Batch context menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void downloadBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Downloading Batch");
                // Get local BatchDetail
                BatchDetail batchDetail = (BatchDetail)this._lastSelectedNode.Tag;
                foreach (DocumentInfo documentInfo in batchDetail.Documents)
                {
                    // Remove existing DocumentDetails
                    DocumentDetail documentDetail = this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID);
                    if (documentDetail != null)
                    {
                        this._workSet_DocumentDetail.Remove(documentDetail);
                    }
                }
                // Check out batch
                this.LoadingBarShow("Checking Out Batch");
                string responseText_checkOut = await this.PerformHTTP_Async(
                        HTTP.PUT,
                        new string[] { "Batch", "CheckOut" },
                        new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID)
                            },
                        null,
                        true);
                if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    BatchDetail resultObject = TestController.DeserializeXML<BatchDetail>(responseText_checkOut, this._enableValidation);
                    this._workSet_BatchDetail.Remove(batchDetail);
                    this._workSet_BatchDetail.Add(resultObject);
                    batchDetail = resultObject;
                }
                //=================================================================================================================
                this.LoadingBarShow("Downloading Documents");
                int downloadCount = 0;
                foreach (DocumentInfo documentInfo in batchDetail.Documents)
                {
                    this.LoadingBarShow(string.Format("Downloading Documents[{0}/{1}]", downloadCount++, batchDetail.Documents.Length));
                    DocumentDetail documentDetail = await this.DownloadDocument(documentInfo._ID);
                    this._workSet_DocumentDetail.Add(documentDetail);
                }
                // Batch/DownloadComplete
                //=================================================================================================================
                this.LoadingBarShow("Setting Batch Download Complete");
                List<BatchDetail> _workset_BatchDetail_DownloadComplete = new List<BatchDetail>();
                if (batchDetail.StatusCode == BatchMediaStatusCode.Uploaded_By_Submitter || batchDetail.StatusCode == BatchMediaStatusCode.Corrected_Uploaded_By_Submitter)
                {
                    this.LoadingBarShow("Setting Batch Download Complete");
                    BatchMediaStatusCode completeStatus = batchDetail.StatusCode == BatchMediaStatusCode.Uploaded_By_Submitter ? BatchMediaStatusCode.Downloaded_By_County : BatchMediaStatusCode.Corrected_Downloaded_By_County;
                    string responseText = await this.PerformHTTP_Async(
                        HTTP.PUT,
                        new string[] { "Batch", "DownloadComplete" },
                        new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID),
                                new DictionaryEntry("BatchStatus", completeStatus.ToString())
                            },
                        null,
                        true);
                    BatchDetail resultBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                    _workset_BatchDetail_DownloadComplete.Add(resultBatchDetail);
                }
                else
                {
                    _workset_BatchDetail_DownloadComplete.Add(batchDetail);
                }
                this._workSet_BatchDetail = _workset_BatchDetail_DownloadComplete;
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Click event for Checkout batch context menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCheckOutBatch_Click(object sender, EventArgs e)
        {
            this.btnOP_CheckOutBatch.PerformClick();
        }
        /// <summary>
        /// Click event for Record Batch context menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuRecordBatch_Click(object sender, EventArgs e)
        {
            this.btnOP_AcceptDocument.PerformClick();
        }
        /// <summary>
        /// Click event for Check in batch context menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCheckInBatch_Click(object sender, EventArgs e)
        {
            this.btnOP_CheckInBatch.PerformClick();
        }
        /// <summary>
        /// Click event for correct batch context menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCorrectBatch_Click(object sender, EventArgs e)
        {
            this.btnOP_CorrectDocument.PerformClick();
        }
        /// <summary>
        /// Click event for Reject context menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void rejectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await this.ChangeDocumentStatus(DocumentMediaStatusCode.Rejected);
        }
        private async void mnuDownloadDocumentImages_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.trvBatches.SelectedNode;
            if (selectedNode != null && selectedNode.Tag.GetType() == typeof(BatchDetail))
            {
                try
                {
                    BatchDetail batchDetail = (BatchDetail)selectedNode.Tag;//this.GetLocalBatchDetail(selectedNode.Text);
                    int counter = 0;
                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                    {
                        if (documentInfo != null && documentInfo.ActionCode != ActionCode.New)
                        {
                            this.LoadingBarShow(string.Format("[{1}/{2}]Downloading Images for Document[{0}]", documentInfo._ID, ++counter, batchDetail.Documents.Length));
                            DocumentDetail downloadedDocumentDetail = await this.DownloadDocument(documentInfo._ID);
                            if (downloadedDocumentDetail != null)
                            {
                                DocumentDetail oldDocumentDetail = this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID);
                                if (oldDocumentDetail != null)
                                {
                                    this._workSet_DocumentDetail.Remove(oldDocumentDetail);
                                }
                                this._workSet_DocumentDetail.Add(downloadedDocumentDetail);
                            }
                            else
                            {
                                MessageBox.Show(
                                    this,
                                    string.Format("Failed to download Document with ID[{0}]", documentInfo._ID),
                                    "Download Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                    }

                    this.LoadTree(this._workSet_BatchDetail);
                    foreach (TreeNode treeNode in this.trvBatches.Nodes)
                    {
                        if (treeNode.Tag == batchDetail)
                        {
                            this.trvBatches_NodeMouseClick(null, new TreeNodeMouseClickEventArgs(treeNode, MouseButtons.Left, 1, 0, 0));
                            continue;
                        }
                    }
                }
                finally
                {
                    this.LoadingBarHide();
                }
            }
        }
        private void expireBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnOP_ExpireBatch.PerformClick();
        }

        #endregion

        /// <summary>
        /// KeyDown event for the Download Batch combo box.
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private void cboDownloadBatch_Status_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        /// <summary>
        /// Load event for the County Panel.
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private void CountyPanel_Load(object sender, EventArgs e)
        {
            this.cboViewBatch_Status.DataSource = Enum.GetNames(typeof(BatchMediaStatusCode));
            this.cboDownloadBatch_Status.DataSource = new object[]
            {
                BatchMediaStatusCode.Uploaded_By_Submitter,
                BatchMediaStatusCode.Corrected_Uploaded_By_Submitter
            };
            this._searchBy = SearchBy.None;
            this.ConfigureSearchBox();
            this.txtURL.Text = TestController.SECURE_ADDRESS_COUNTY;
            this.enableFormControls(this._testController.SessionTokenID != string.Empty && this._testController.SessionTokenID != null);
            this.chkToken.Checked = TestController.TwoFactor_Enabled_County;
        }
        /// <summary>
        /// Node Mouse Click event for the batch tree.
        /// </summary>
        /// <param name="sender">The object raising the event</param>
        /// <param name="e">The event arguments</param>
        private void trvBatches_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == this._lastSelectedNode)
            {
            }
            else
            {
                this.Enabled = false;
                this._lastSelectedNode = e.Node;
                this.ConfigureOPbuttons(e.Node);
                Type selectedType = null;
                if (e.Node != null)
                {
                    selectedType = e.Node.Tag.GetType();
                }
                if (selectedType == typeof(BatchDetail))
                {
                    this.LoadFlowPanel((BatchDetail)e.Node.Tag, null, null);
                }
                else if (selectedType == typeof(DocumentInfo))
                {
                    BatchDetail batchDetail = (BatchDetail)e.Node.Parent.Tag;
                    DocumentInfo documentInfo = (DocumentInfo)e.Node.Tag;
                    this.LoadFlowPanel(
                        batchDetail,
                        documentInfo,
                        this.GetLocalDocumentDetail(
                            documentInfo._ID,
                            batchDetail._ID));
                }
                this.ConfigureContextMenu_Visible(selectedType);
                this.Enabled = true;
            }
        }
        private void ConfigureOPbuttons(TreeNode selectedNode)
        {
            if (selectedNode == null)
            {
                this.btnOP_AcceptDocument.Enabled =
                    this.btnOP_CheckInBatch.Enabled =
                    this.btnOP_CheckOutBatch.Enabled =
                    this.btnOP_CorrectDocument.Enabled =
                    this.btnOP_DownloadBatch.Enabled =
                    this.btnOP_ExpireBatch.Enabled =
                    this.btnOP_RejectDocument.Enabled =
                    this.btnOP_SubmitBatch.Enabled =
                    this.mnuDownloadBatch.Enabled =
                    this.mnuExpireBatch.Enabled =
                    this.mnuSubmitBatch.Enabled =
                    this.mnuCheckInBatch.Enabled =
                    this.mnuCheckOutBatch.Enabled =
                    this.mnuCorrectDocument.Enabled =
                    this.mnuRecordDocument.Enabled =
                    this.mnuRejectDocument.Enabled =
                    false;
            }
            else
            {
                this.mnuRecordDocument.Enabled = this.btnOP_AcceptDocument.Enabled = (selectedNode.Tag.GetType() == typeof(DocumentInfo)) && (((BatchDetail)selectedNode.Parent.Tag).CheckedOutBy != null) &&
                    (((BatchDetail)selectedNode.Parent.Tag).CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper());

                this.mnuCheckInBatch.Enabled = this.btnOP_CheckInBatch.Enabled = (selectedNode.Tag.GetType() == typeof(BatchDetail)) &&
                    (((BatchDetail)selectedNode.Tag).CheckedOutBy != null) &&
                    (((BatchDetail)selectedNode.Tag).CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper());

                this.mnuCheckOutBatch.Enabled = this.btnOP_CheckOutBatch.Enabled = (selectedNode.Tag.GetType() == typeof(BatchDetail)) &&
                    (((BatchDetail)selectedNode.Tag).CheckedOutBy == null);

                this.mnuCorrectDocument.Enabled = this.btnOP_CorrectDocument.Enabled = (selectedNode.Tag.GetType() == typeof(DocumentInfo)) && (((BatchDetail)selectedNode.Parent.Tag).CheckedOutBy != null) &&
                    (((BatchDetail)selectedNode.Parent.Tag).CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper());

                this.mnuDownloadBatch.Enabled = this.btnOP_DownloadBatch.Enabled = (selectedNode.Tag.GetType() == typeof(BatchDetail));
                this.mnuExpireBatch.Enabled = this.btnOP_ExpireBatch.Enabled = (selectedNode.Tag.GetType() == typeof(BatchDetail)) &&
                    (((BatchDetail)selectedNode.Tag).CheckedOutBy != null) &&
                    (((BatchDetail)selectedNode.Tag).CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper());

                this.mnuRejectDocument.Enabled = this.btnOP_RejectDocument.Enabled = (selectedNode.Tag.GetType() == typeof(DocumentInfo)) && (((BatchDetail)selectedNode.Parent.Tag).CheckedOutBy != null) &&
                      (((BatchDetail)selectedNode.Parent.Tag).CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper());

                this.mnuSubmitBatch.Enabled = this.btnOP_SubmitBatch.Enabled = (selectedNode.Tag.GetType() == typeof(BatchDetail)) && (((BatchDetail)selectedNode.Tag).CheckedOutBy != null) && (((BatchDetail)selectedNode.Tag).CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper()) ||
                     (selectedNode.Tag.GetType() == typeof(DocumentInfo) && ((BatchDetail)selectedNode.Parent.Tag).CheckedOutBy != null && ((BatchDetail)selectedNode.Parent.Tag).CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper());
            }
        }
        /// <summary>
        /// Event for Checkbox for View API buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAPI_CheckedChanged(object sender, EventArgs e)
        {
            this.grpAPIButtons.Visible = this.chkAPI.Checked;
        }
        /// <summary>
        /// Check event for BatchID checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSearch_BatchID_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._ConfiguringSearch)
            {
                this._ConfiguringSearch = true;
                if (((CheckBox)sender).Checked)
                {
                    this.ConfigureSearchOptions(true, false, false, false, false, false);
                }
                else
                {
                    this.ConfigureSearchOptions(true, true, true, true, true, true);
                }
                this.DetermineSearchBy();
                this.ConfigureSearchBox();
                this.txtViewBatchSearch_input.Text = string.Empty;
                this._ConfiguringSearch = false;
            }
        }
        /// <summary>
        /// Check event for BatchName checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSearch_BatchName_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._ConfiguringSearch)
            {
                this._ConfiguringSearch = true;
                if (((CheckBox)sender).Checked)
                {
                    this.ConfigureSearchOptions(false, true, false, false, false, false);
                }
                else
                {
                    this.ConfigureSearchOptions(true, true, true, true, true, true);
                }
                this.DetermineSearchBy();
                this.ConfigureSearchBox();
                this.txtViewBatchSearch_input.Text = string.Empty;
                this._ConfiguringSearch = false;
            }
        }
        /// <summary>
        /// Check event for SubmissionID checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSearch_SubmissionID_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._ConfiguringSearch)
            {
                this._ConfiguringSearch = true;
                if (((CheckBox)sender).Checked)
                {
                    this.ConfigureSearchOptions(false, false, true, false, false, false);
                }
                else
                {
                    this.ConfigureSearchOptions(true, true, true, true, true, true);
                }
                this.DetermineSearchBy();
                this.ConfigureSearchBox();
                this.txtViewBatchSearch_input.Text = string.Empty;
                this._ConfiguringSearch = false;
            }
        }
        /// <summary>
        /// Check event for Status checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSearch_Status_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._ConfiguringSearch)
            {
                this._ConfiguringSearch = true;
                if (((CheckBox)sender).Checked)
                {
                    this.ConfigureSearchOptions(false, false, false, true, true, true);
                }
                else
                {
                    this.ConfigureSearchOptions(!this.chkSearch_UserName.Checked && !this.chkSearch_County.Checked, !this.chkSearch_UserName.Checked && !this.chkSearch_County.Checked, !this.chkSearch_UserName.Checked && !this.chkSearch_County.Checked, true, true, true);
                }
                this.DetermineSearchBy();
                this.ConfigureSearchBox();
                this._ConfiguringSearch = false;
            }
        }
        /// <summary>
        /// Check event for County checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSearch_County_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._ConfiguringSearch)
            {
                this._ConfiguringSearch = true;
                if (((CheckBox)sender).Checked)
                {
                    this.ConfigureSearchOptions(false, false, false, true, true, true);
                }
                else
                {
                    this.ConfigureSearchOptions(!this.chkSearch_UserName.Checked && !this.chkSearch_Status.Checked, !this.chkSearch_UserName.Checked && !this.chkSearch_Status.Checked, !this.chkSearch_UserName.Checked && !this.chkSearch_Status.Checked, true, true, true);
                }
                this.DetermineSearchBy();
                this.ConfigureSearchBox();
                this._ConfiguringSearch = false;
            }
        }
        /// <summary>
        /// check event for UserName checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSearch_UserName_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._ConfiguringSearch)
            {
                this._ConfiguringSearch = true;
                if (((CheckBox)sender).Checked)
                {
                    this.ConfigureSearchOptions(false, false, false, true, true, true);
                }
                else
                {
                    this.ConfigureSearchOptions(!this.chkSearch_County.Checked && !this.chkSearch_Status.Checked, !this.chkSearch_County.Checked && !this.chkSearch_Status.Checked, !this.chkSearch_County.Checked && !this.chkSearch_Status.Checked, true, true, true);
                }
                this.DetermineSearchBy();
                this.ConfigureSearchBox();
                this.txtViewBatchSearch_input.Text = TestController.Default_UserName_County;
                this._ConfiguringSearch = false;
            }
        }
        private void chkBatchDownload_CheckOut_CheckedChanged(object sender, EventArgs e)
        {
            this.chkBatchDownload_SetComplete.Enabled = this.chkBatchDownload_SetComplete.Checked = this.chkBatchDownload_CheckOut.Checked;
        }
        private async void btnCache_LoadCache_Click(object sender, EventArgs e)
        {
            await this.LoadCache();
        }
        private void btnCache_ViewCache_Click(object sender, EventArgs e)
        {
            CacheViewerForm cvForm = new CacheViewerForm(
                this._cache_citiesDetail,
                this._cache_countiesInfo,
                //this._cache_IndexOptionsDetail,
                this._cache_processQueuesDetail,
                //this._cache_requestingPartiesInfo,
                this._cache_submittingPartiesInfo,
                this._cache_titlesDetail);
            cvForm.ShowDialog(this);
        }
        /// <summary>
        /// Enable textbox to edit SECURE API URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnURLEdit_Click(object sender, EventArgs e)
        {
            if (this.txtURL.Text.Trim() != string.Empty)
            {
                this.txtURL.Enabled = !this.txtURL.Enabled;
                if (this.txtURL.Text.Trim() != string.Empty)
                {
                    string URL = this.txtURL.Text.Trim();
                    if (!URL.EndsWith("/"))
                    {
                        URL += "/";
                    }
                    if (!(URL.ToLower().StartsWith("http://") || URL.ToLower().StartsWith("https://")))
                    {
                        URL = string.Format("http://{0}", URL);
                    }
                    this.txtURL.Text = URL;
                    TestController.SECURE_ADDRESS_COUNTY = URL;
                }
                this.btnURLEdit.Text = this.txtURL.Enabled ? "OK" : "Edit";

                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(GroupBox) && !(control.Name == "grpURL" || control.Name == "grpCurrentUser" || control.Name == "grpAuthorization"))
                    {
                        ((GroupBox)control).Enabled = !this.txtURL.Enabled && this._testController.Session != null;
                    }
                    else if (control.GetType() == typeof(GroupBox) && (control.Name == "grpAuthorization"))
                    {
                        this.btnAuth_Login.Enabled = !this.txtURL.Enabled &&  this._testController.Session == null;
                        this.btnAuth_Logout.Enabled =
                        this.btnAuth_Ping.Enabled =
                        this.btnAuth_ChangePassword.Enabled = !this.btnAuth_Login.Enabled && !this.txtURL.Enabled;
                    }
                }

                if (this.txtURL.Enabled) 
                { 
                    this.txtURL.Focus();
                }

            }
            else
            {
                MessageBox.Show(this, "URL is required.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void submitBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnOP_SubmitBatch.PerformClick();
        }
        private void chk_XML_Validation_CheckedChanged(object sender, EventArgs e)
        {
            this._enableValidation = this.chk_XML_Validation.Checked;
        }
        private void picCurrentUser_Logo_MouseHover(object sender, EventArgs e)
        {

        }
        private void chkToken_CheckedChanged(object sender, EventArgs e)
        {
            TestController.TwoFactor_Enabled_County = this.chkToken.Checked;
        }

        #endregion

        #region API Calls

        /// <summary>
        /// Log out current user from SECURE
        /// </summary>
        private async void PerformLogout()
        {
            try
            {
                this.LoadingBarShow("Logging Out of SECURE");
                string responseText = await this.PerformHTTP_Async(
                    HTTP.PUT,
                    new string[] { "Authentication", "Logout" },
                    null,
                    null,
                    true);
                ResultDetail responseDetail = TestController.DeserializeXML<ResultDetail>(responseText, this._enableValidation);
                MessageBox.Show(
                    this,
                    string.Format("User [{0}] logged out {1}successfully.", TestController.Default_UserName_County, responseDetail.Value ? string.Empty : "un"),
                    string.Format(Properties.Resources.CaptionFormat_MessageBox, "Log Out"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                  this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this._testController.Session = null;
                this.lblSessionTokenID.Text = string.Empty;
                this.ssStatus.Update();
                this.enableFormControls(false);
                this.UpdateCurrentUserUI();
                this.LoadingBarHide();
            }
        }

        /// <summary>
        /// Event method for the Ping button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAuth_Ping_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Pinging SECURE");
                string response = await this.PerformHTTP_Async(
                    HTTP.GET,
                    new string[] { "Authentication", "Ping" },
                    null,
                    null,
                    true);
                bool sessionOK = false;
                try
                {
                    sessionOK = TestController.DeserializeXML<ResultDetail>(response, this._enableValidation).Value;
                    this.LoadingBarHide();
                    MessageBox.Show(this, string.Format("SECURE Session Valid: {0}", sessionOK), "Ping Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                        this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                    {
                        this.LoadingBarHide();
                        this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                this.LoadingBarHide();
                this.HandleException(ex);
            }
            finally
            {
                this.LoadingBarHide();
            }
        }

        /// <summary>
        /// Log in to SECURE
        /// </summary>
        /// <returns>Task</returns>
        private async Task PerformLoginAsync(bool enableTwoFactor)
        {
            try
            {
                // Show input box
                InputForm inputForm = new InputForm(
                   enableTwoFactor ?
                   new DictionaryEntry[] 
                    { 
                        new DictionaryEntry("UserName", TestController.Default_UserName_County),
                        new DictionaryEntry("Password", TestController.Default_Submitter_Password),
                        new DictionaryEntry("TwoFactorPassword", TestController.Default_County_Password_2Factor)
                    }
                   :
                   new DictionaryEntry[] 
                    { 
                        new DictionaryEntry("UserName", TestController.Default_UserName_County),
                        new DictionaryEntry("Password", TestController.Default_Submitter_Password)
                    });
                DialogResult DR = inputForm.ShowDialog(this);
                if (DR != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                this.LoadingBarShow("Logging In");

                DictionaryEntry[] userInputParameters = inputForm.Parameters;
                string userName = string.Empty;
                string password = string.Empty;
                string twoFactorPassword = null;
                foreach (DictionaryEntry paramEntry in userInputParameters)
                {
                    if (paramEntry.Key.ToString() == "UserName")
                    {
                        userName = paramEntry.Value.ToString();
                    }
                    if (paramEntry.Key.ToString() == "Password")
                    {
                        password = paramEntry.Value.ToString();
                    }
                    if (paramEntry.Key.ToString() == "TwoFactorPassword")
                    {
                        twoFactorPassword = paramEntry.Value.ToString();
                    }
                }
                TestController.Default_UserName_County = userName;
                TestController.Default_County_Password = password;
                TestController.Default_County_Password_2Factor = twoFactorPassword == null ? string.Empty : TestController.Default_County_Password_2Factor;
                string responseText = await this._testController.LoginAsync(
                    userName,
                    password,
                    twoFactorPassword,
                    this._enableValidation);
                this.UpdateResponseProperties(
                    this._testController.Last_HTTPResponse,
                    TestController.FormatXML(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync()),
                    this._testController.Last_HTTPResponse.RequestMessage,
                    TestController.FormatXML(this._testController.Last_HTTPRequestContent));
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                 this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.DisplayHTTPError(responseText);
                }
                else if (this._testController.NeedPasswordChange)
                {
                    MessageBox.Show(this,
                    string.Format("Password for user [{0}] has expired. Please change user's password.", userName),
                    string.Format(Properties.Resources.CaptionFormat_MessageBox, "Password Expired"), 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                }
                else
                {
                    await this.LoadCache();
                }
                this.ssStatus.Refresh();
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
            finally
            {
                this.LoadingBarHide();
                this.lblSessionTokenID.Text = (this._testController.SessionTokenID != string.Empty) ?
                    ("SessionTokenID: " + this._testController.SessionTokenID) :
                    "Login Failed";
                this.UpdateCurrentUserUI();
            }
        }
        private void UpdateCurrentUserUI()
        {
            try
            {
                if (this._testController.Session == null)
                {
                    this.txtCurrentUser_UserName.Text
                        = this.txtCurrentUser_Name.Text
                        = this.txtCurrentUser_UserType.Text
                        = this.txtCurrentUser_County.Text
                        = string.Empty;
                    this.picCurrentUser_Logo.Image = null;
                }
                else
                {
                    this.txtCurrentUser_UserName.Text = TestController.Default_UserName_County;
                    this.txtCurrentUser_Name.Text = string.Format("{0} {1}", this._testController.Session.FirstName, this._testController.Session.LastName);
                    this.txtCurrentUser_UserType.Text = this._testController.Session.UserType;
                    this.txtCurrentUser_County.Text = this._cache_countiesInfo.County[0].Name;
                    if (this._cache_countiesInfo.County[0].Logo != null && this._cache_countiesInfo.County[0].Logo != string.Empty)
                    {
                        string logoBase64DataOnly = this._cache_countiesInfo.County[0].Logo.Remove(
                            this._cache_countiesInfo.County[0].Logo.IndexOf("data:image/"),
                            ("data:image:" + "xxx" + ";base64,").Length);
                        this.picCurrentUser_Logo.Image = Image.FromStream(
                            new MemoryStream(
                                Convert.FromBase64String(
                                logoBase64DataOnly)));
                    }
                    else
                    {
                        this.picCurrentUser_Logo.Image = null;
                    }
                }
            }
            catch (Exception)
            {
                this.txtCurrentUser_UserName.Text
                        = this.txtCurrentUser_Name.Text
                        = this.txtCurrentUser_UserType.Text
                        = this.txtCurrentUser_County.Text
                        = string.Empty;
                this.picCurrentUser_Logo.Image = null;
            }
        }
        /// <summary>
        /// Event method for the Change Password button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAuth_ChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                InputForm inputForm = new InputForm(new string[] { "OldPassword", "NewPassword" });
                if (inputForm.ShowDialog(this) == DialogResult.OK)
                {
                    this.LoadingBarShow("Changing Password");

                    DictionaryEntry[] userInputParameters = inputForm.Parameters;
                    string response = await this.PerformHTTP_Async(
                        HTTP.PUT,
                        new string[] { "Authentication", "ChangePassword" },
                        null,
                       new StringContent(
                            TestController.SerializeXML(
                                new AuthenticationDetail()
                                {
                                    UserName = TestController.Default_UserName_Submitter,
                                    Password = userInputParameters[0].Value.ToString(),
                                    NewPassword = userInputParameters[1].Value.ToString()
                                })),
                        true);
                    if (this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                        this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted)
                    {
                        ResultDetail responsedetail = TestController.DeserializeXML<ResultDetail>(response, this._enableValidation);
                        this.LoadingBarHide();
                        MessageBox.Show(this,
                            string.Format("Password change {1} for user [{0}]. User needs to log out then log in again.", TestController.Default_UserName_County, responsedetail.Value ? "successful" : "failed"),
                            string.Format(Properties.Resources.CaptionFormat_MessageBox, "Password Change"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        if (responsedetail.Value)
                        {
                            this._testController.NeedPasswordChange = false;
                            this.btnAuth_ChangePassword.Enabled = false;
                            // Current SessionTokenID is invalid for SECURE API functions. Do Nothing.
                        }
                    }
                    else
                    {
                        // unsuccessful change
                        throw new Exception("Failed to change password");
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }

        #endregion

        #region HTTP-related
        /// <summary>
        /// Update the response/request properties of the user control for logging
        /// </summary>
        /// <param name="responseMessage">HttpResponseMessage, the last HTTPResponseMessage received from the SECURE API</param>
        /// <param name="responseMessageContent">string, containing the contents of the last HTTPResponseMessage</param>
        /// <param name="requestMessage">HttpRequestMessage, the last HTTPRequestMessage send to the SECURE API</param>
        /// <param name="requestMessageContent">string, containing the contents of the last HTTPRequestMessage</param>
        /// <param name="append">bool, true to append to existing response/request properties</param>
        public void UpdateResponseProperties(HttpResponseMessage responseMessage, string responseMessageContent, HttpRequestMessage requestMessage, string requestMessageContent, bool append)
        {
            this.UpdateResponseProperties(responseMessage, responseMessageContent, requestMessage, requestMessageContent, append, false);
        }
        /// <summary>
        /// Update the response/request properties of the user control for logging
        /// </summary>
        /// <param name="responseMessage">HttpResponseMessage, the last HTTPResponseMessage received from the SECURE API</param>
        /// <param name="responseMessageContent">string, containing the contents of the last HTTPResponseMessage</param>
        /// <param name="requestMessage">HttpRequestMessage, the last HTTPRequestMessage send to the SECURE API</param>
        /// <param name="requestMessageContent">string, containing the contents of the last HTTPRequestMessage</param>
        /// <param name="append">bool, true to append to existing response/request properties</param>
        public void UpdateResponseProperties(HttpResponseMessage responseMessage, string responseMessageContent, HttpRequestMessage requestMessage, string requestMessageContent, bool append, bool isRetry)
        {
            try
            {
                GC.Collect();
                // Set RequestMessage object
                this._RequestMessage = requestMessage;
                // Set RequestMessage string
                this._RequestMessageString = append ?
                    string.Format("{0}\r\n----------------------------------------------------------------------------\r\n{1}\r\nContent:\r\n{2}",
                        this._RequestMessageString,
                        requestMessage.ToString(),
                        requestMessageContent) :
                    string.Format("{0}\r\nContent:\r\n{1}",
                        requestMessage.ToString(),
                        requestMessageContent.ToString());
                // Set RequestMessage content
                this._RequestMessageContent = append ?
                    string.Format("{0}\r\n----------------------------------------------------------------------------\r\n{1}", this._RequestMessageContent, requestMessageContent) : requestMessageContent;
                // Set ResponseMessage object
                this._ResponseMessage = responseMessage;
                // Set ResponseMessage string
                this._ResponseMessageString = append ?
                    string.Format("{0}\r\n----------------------------------------------------------------------------\r\n{1}\r\nContent:\r\n{2}",
                        this._ResponseMessageString,
                        responseMessage.ToString(),
                        responseMessageContent) :
                    string.Format("{0}\r\nContent:\r\n{1}",
                        responseMessage.ToString(),
                        responseMessageContent);
                // Set ResponseMessage Content
                this._ResponseMessageContent = append ?
                    string.Format("{0}\r\n----------------------------------------------------------------------------\r\n{1}", this._ResponseMessageContent, responseMessageContent) : responseMessageContent;
            }
            catch (OutOfMemoryException ex)
            {
                try
                {
                    // Out of memory. Purge existing, start over. 
                    if (!isRetry)
                    {
                        this._ResponseMessage = null;
                        this._ResponseMessageContent = string.Empty;
                        this._ResponseMessageString = string.Empty;
                        this._RequestMessage = null;
                        this._RequestMessageContent = string.Empty;
                        this._RequestMessageString = string.Empty;
                        GC.Collect();
                        this.UpdateResponseProperties(responseMessage, responseMessageContent, requestMessage, requestMessageContent, false, true);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                catch (OutOfMemoryException)
                {
                    this._ResponseMessage = null;
                    this._ResponseMessageContent = string.Empty;
                    this._ResponseMessageString = string.Empty;
                    this._RequestMessage = null;
                    this._RequestMessageContent = string.Empty;
                    this._RequestMessageString = string.Empty;
                    GC.Collect();
                }
            }
        }
     
        /// <summary>
        /// Update the response/request properties of the user control for logging
        /// </summary>
        /// <param name="responseMessage">HttpResponseMessage, the last HTTPResponseMessage received from the SECURE API</param>
        /// <param name="responseMessageContent">string, containing the contents of the last HTTPResponseMessage</param>
        /// <param name="requestMessage">HttpRequestMessage, the last HTTPRequestMessage send to the SECURE API</param>
        /// <param name="requestMessageContent">string, containing the contents of the last HTTPRequestMessage</param>
        public void UpdateResponseProperties(HttpResponseMessage responseMessage, string responseMessageContent, HttpRequestMessage requestMessage, string requestMessageContent)
        {
            this.UpdateResponseProperties(responseMessage, responseMessageContent, requestMessage, requestMessageContent, false);
        }
   
        /// <summary>
        /// Call SECURE API and deserialize response into an object
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized</typeparam>
        /// <param name="httpMethod">HTTP, describing the method used</param>
        /// <param name="apiNames">string[], containing the API to be called</param>
        /// <param name="parameters">DictionaryEntry[], containing pairs of API parameters and values</param>
        /// <param name="content">HTTPContent that will be sent in the HTTPRequestMessage</param>
        /// <returns>Object of Type T</returns>
        private async Task<T> GetDeserialized<T>(HTTP httpMethod, string[] apiNames, DictionaryEntry[] parameters)
        {
            return await this.GetDeserialized<T>(
                httpMethod,
                apiNames,
                parameters,
                null);
        }
   
        /// <summary>
        /// Call SECURE API and deserialize response into an object
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized</typeparam>
        /// <param name="httpMethod">HTTP, describing the method used</param>
        /// <param name="apiNames">string[], containing the API to be called</param>
        /// <param name="parameters">DictionaryEntry[], containing pairs of API parameters and values</param>
        /// <param name="content">HTTPContent that will be sent in the HTTPRequestMessage</param>
        /// <returns>Object of Type T</returns>
        private async Task<T> GetDeserialized<T>(HTTP httpMethod, string[] apiNames, DictionaryEntry[] parameters, HttpContent content)
        {
            return await this.GetDeserialized<T>(httpMethod,
               apiNames,
               parameters,
               content,
               false);
        }   
    
        /// <summary>
        /// Call SECURE API and deserialize response into an object
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized</typeparam>
        /// <param name="httpMethod">HTTP, describing the method used</param>
        /// <param name="apiNames">string[], containing the API to be called</param>
        /// <param name="parameters">DictionaryEntry[], containing pairs of API parameters and values</param>
        /// <param name="content">HTTPContent that will be sent in the HTTPRequestMessage</param> 
        /// <param name="append">bool, true to append to existing response/request properties</param>
        /// <returns>Object of Type T</returns>
        private async Task<T> GetDeserialized<T>(HTTP httpMethod, string[] apiNames, DictionaryEntry[] parameters, HttpContent content, bool append)
        {
            string serialized = await this.PerformHTTP_Async(
               httpMethod,
               apiNames,
               parameters,
               content,
               append);
            return (T)TestController.DeserializeXML<T>(serialized, this._enableValidation);
        }
    
        /// <summary>
        /// Call SECURE API
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized</typeparam>
        /// <param name="httpMethod">HTTP, describing the method used</param>
        /// <param name="apiNames">string[], containing the API to be called</param>
        /// <param name="parameters">DictionaryEntry[], containing pairs of API parameters and values</param>
        /// <param name="content">HTTPContent that will be sent in the HTTPRequestMessage</param> 
        /// <param name="append">bool, true to append to existing response/request properties</param>
        /// <returns>string, containing XML-serialized response from SECURE API</returns>
        private async Task<string> PerformHTTP_Async(HTTP httpMethod, string[] apiName, DictionaryEntry[] apiParameters, HttpContent content, bool append)
        {
            string response = response = await this._testController.SECURE_API_Async(
                Properties.Resources.MediaType_XML,
                httpMethod,
                apiName,
                apiParameters,
                content);
            this.UpdateResponseProperties(
                this._testController.Last_HTTPResponse,
                TestController.FormatXML(this._testController.Last_HTTPResponseString),
                this._testController.Last_HTTPResponse.RequestMessage,
                TestController.FormatXML(this._testController.Last_HTTPRequestContent),
                append);
            return response;
        }
  
        /// <summary>
        /// Display error message from SECURE API in messagebox
        /// </summary>
        /// <param name="htmlEncodedResponse"></param>
        private void DisplayHTTPError(string htmlEncodedResponse)
        {
            this.LoadingBarHide();
            string messageBody = "Error Response";
            if (htmlEncodedResponse.StartsWith("<Error"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Error));
                object deSerialized = xs.Deserialize(new StringReader(htmlEncodedResponse));
                Error httpError = (Error)(xs.Deserialize(new StringReader(htmlEncodedResponse)));
                messageBody = string.Format("{0}{1}{2}{3}{4}",
                        httpError.ExceptionType == string.Empty ? string.Empty : (httpError.ExceptionType + ": "),
                        httpError.Message,
                        httpError.MessageDetail == string.Empty ? string.Empty : ("\r\n" + httpError.MessageDetail),
                        httpError.ExceptionMessage == string.Empty ? string.Empty : ("\r\n" + httpError.ExceptionMessage),
                        httpError.StackTrace == string.Empty ? string.Empty : ("\r\r\n" + httpError.StackTrace));
            }
            if (this._testController.Last_HTTPstatus != 0 &&
                this._testController.Last_HTTPstatus != (int)HttpStatusCode.Accepted &&
                this._testController.Last_HTTPstatus != (int)HttpStatusCode.OK)
            {
                messageBody = string.Format("{0}\r\n[HTTP:{1}]", messageBody, this._testController.Last_HTTPstatus);
            }
            MessageBox.Show(
                this,
                messageBody,
                string.Format(Properties.Resources.CaptionFormat_MessageBox, "Error"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region Helper Functions

        #region Loading Bar
      
        /// <summary>
        /// LoadingForm, display when performning lengthy operations
        /// </summary>
        private LoadingForm loadingform = null;
      
        /// <summary>
        /// Display the loading bar form
        /// </summary>
        /// <param name="displayText">string, containing text to display on the loading bar form</param>
        private void LoadingBarShow(string displayText)
        {
            if (this.loadingform == null || this.loadingform.IsDisposed)
            {
                this.loadingform = new LoadingForm(displayText)
                {
                };
            }
            this.loadingform.DisplayText = displayText;
            this.Enabled = false;
            if (this.loadingform.Visible == false)
            {
                this.loadingform.Show(this);
            }
        }
      
        /// <summary>
        /// Display the loading bar form
        /// </summary>
        private void LoadingBarShow()
        {
            this.LoadingBarShow(string.Empty);
        }
     
        /// <summary>
        /// Hide the loading bar form
        /// </summary>
        private void LoadingBarHide()
        {
            if (this.loadingform != null)
            {
                this.loadingform.Close();
            }
            this.Enabled = true;
            this.Focus();
        }

        #endregion

        private async Task<BatchWithDocumentsDetail> BatchWithDocumentsReadByID(string batchID)
        {
            try
            {
                this.LoadingBarShow(string.Format("Getting Batch [{0}] with Document Image(s)", batchID));
                if (int.Parse(batchID) > 0)
                {
                    // send to SECURE
                    string responseText = await this.PerformHTTP_Async(
                            HTTP.GET,
                            new string[] { "BatchWithDocuments", "ReadByID" },
                            new DictionaryEntry[] 
                                {
                                    new DictionaryEntry("BatchID", batchID)
                                },
                            null,
                            true);
                    if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                     this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                    {
                        // Saved batch response
                        return TestController.DeserializeXML<BatchWithDocumentsDetail>(responseText, this._enableValidation);
                    }
                    else
                    {
                        this.DisplayHTTPError(responseText);
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                //this._FailMessages.Add("Failed to upload batch");
                return null;
            }
        }

        /// <summary>
        /// Load all cache data from index API
        /// </summary>
        /// <returns></returns>
        private async Task LoadCache()
        {
            try
            {
                this.LoadingBarShow("Loading Data Cache");
                // Load cache tables
                //=================================================================================================================
                // county
                this._cache_countiesInfo = await this.GetDeserialized<CountiesInfo>(
                    HTTP.GET,
                    new string[] { "Index", "County" },
                    null,
                    null,
                    false);
                this.cboCounty.Items.Clear();
                foreach (CountyInfo countyInfo in this._cache_countiesInfo.County)
                {
                    this.cboCounty.Items.Add(countyInfo.Name);
                }
                this.cboCounty.SelectedIndex = 0;
                //=================================================================================================================
                // city
                this._cache_citiesDetail = await this.GetDeserialized<CitiesDetail>(
                    HTTP.GET,
                    new string[] { "Index", "City" },
                    null,
                    null,
                    true);
                //=================================================================================================================
                // title
                this._cache_titlesDetail = await this.GetDeserialized<TitlesDetail>(
                    HTTP.GET,
                    new string[] { "Index", "TItle" },
                    null,
                    null,
                    true);
                //=================================================================================================================
                // process queue
                this._cache_processQueuesDetail = await this.GetDeserialized<ProcessQueuesDetail>(
                    HTTP.GET,
                    new string[] { "Index", "ProcessQueue" },
                    null,
                    null,
                    true);
                //=================================================================================================================
                // submitting parties
                this._cache_submittingPartiesInfo = await this.GetDeserialized<SubmittingPartiesInfo>(
                    HTTP.GET,
                    new string[] { "User", "SubmittingParty" },
                    null,
                    null,
                    true);
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK || this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.LoadingBarHide();
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
     
        /// <summary>
        /// Enable controls based on loggin in
        /// </summary>
        /// <param name="loggedIn">bool, true if user is logged in</param>
        private void enableFormControls(bool loggedIn)
        {
            this.btnAuth_Login.Enabled = !loggedIn;
            this.btnAuth_Logout.Enabled = loggedIn;
            this.btnAuth_Ping.Enabled = loggedIn;
            this.btnAuth_ChangePassword.Enabled = loggedIn;
            
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(GroupBox) && !(control.Name == "grpAuthorization" || control.Name == "grpURL"))
                {
                    ((GroupBox)control).Enabled = loggedIn && (!this._testController.NeedPasswordChange);
                }
            }
        }
    
        /// <summary>
        /// Manually configure search box
        /// </summary>
        /// <param name="enableInput">bool, enable search textbox</param>
        /// <param name="inputLabel">string, text for label next to search textbox</param>
        /// <param name="enableStatusCode">bool, enable status code drop down</param>
        /// <param name="enableDateRange">bool, enable date range drop downs</param>
        private void ConfigureSearchBox(bool enableInput, string inputLabel, bool enableStatusCode, bool enableDateRange, bool enableCounty)
        {
            this.lblBatchSearchInpu.Text = inputLabel;
            this.txtViewBatchSearch_input.Enabled = enableInput;
            this.cboViewBatch_Status.Enabled = enableStatusCode;
            this.dtpBatchSearch_start.Enabled = enableDateRange;
            this.dtpBatchSearch_end.Enabled = enableDateRange;
            this.cboCounty.Enabled = enableCounty;
        }
    
        /// <summary>
        /// Configure the search box based on current SearchBy
        /// </summary>
        private void ConfigureSearchBox()
        {
            switch (this._searchBy)
            {
                case SearchBy.BatchID:
                    {
                        this.ConfigureSearchBox(true, "Batch ID:", false, false, false);
                        break;
                    }
                case SearchBy.BatchName:
                    {
                        this.ConfigureSearchBox(true, "Batch Name:", false, false, false);
                        break;
                    }
                case SearchBy.StatusCodeAndDateRange:
                    {
                        this.ConfigureSearchBox(false, this.lblBatchSearchInpu.Text, true, true, false);
                        break;
                    }
                case SearchBy.StatusCodeDateRangeCounty:
                    {

                        this.ConfigureSearchBox(false, this.lblBatchSearchInpu.Text, true, true, true);
                        break;
                    }
                case SearchBy.SubmissionID:
                    {
                        this.ConfigureSearchBox(true, "Submission ID:", false, false, false);
                        break;
                    }
                case SearchBy.UserName:
                    {
                        this.ConfigureSearchBox(true, "User Name:", false, false, false);
                        break;
                    }
                case SearchBy.UserNameCounty:
                    {
                        this.ConfigureSearchBox(true, "User Name:", false, false, true);
                        break;
                    }
                case SearchBy.UserNameStatusCodeAndDateRange:
                    {
                        this.ConfigureSearchBox(true, "User Name:", true, true, false);
                        break;
                    }
                case SearchBy.UserNameStatusCodeCounty:
                    {
                        this.ConfigureSearchBox(true, "User Name:", true, false, true);
                        break;
                    }
                case SearchBy.None:
                default:
                    {
                        this.ConfigureSearchBox(false, this.lblBatchSearchInpu.Text, false, false, false);
                        break;
                    }
            }
            this.btnOP_ViewBatch.Enabled = this._searchBy != SearchBy.None;
        }
    
        /// <summary>
        /// Configure search options
        /// </summary>
        /// <param name="enableBatchID">bool, true to enable option to search by Batch ID</param>
        /// <param name="enableBatchName">bool, true to enable option to search by Batch Name</param>
        /// <param name="enableSubmissionID">bool, true to enable option to search by Submission ID</param>
        /// <param name="enableBatchStatus">bool, true to enable option to search by Batch Status</param>
        /// <param name="enableCounty">bool, true to enable option to search by County</param>
        /// <param name="enableUserName">bool, true to enable option to search by User Name</param>
        private void ConfigureSearchOptions(bool enableBatchID, bool enableBatchName, bool enableSubmissionID, bool enableBatchStatus, bool enableCounty, bool enableUserName)
        {
            this.chkSearch_BatchID.Enabled = enableBatchID;
            this.chkSearch_BatchName.Enabled = enableBatchName;
            this.chkSearch_SubmissionID.Enabled = enableSubmissionID;
            this.chkSearch_Status.Enabled = enableBatchStatus;
            this.chkSearch_County.Enabled = enableCounty;
            this.chkSearch_UserName.Enabled = enableUserName;

            this.chkSearch_BatchID.Checked = enableBatchID ? this.chkSearch_BatchID.Checked : false;
            this.chkSearch_BatchName.Checked = enableBatchName ? this.chkSearch_BatchName.Checked : false;
            this.chkSearch_SubmissionID.Checked = enableSubmissionID ? this.chkSearch_SubmissionID.Checked : false;
            this.chkSearch_Status.Checked = enableBatchStatus ? this.chkSearch_Status.Checked : false;
            this.chkSearch_County.Checked = enableCounty ? this.chkSearch_County.Checked : false;
            this.chkSearch_UserName.Checked = enableUserName ? this.chkSearch_UserName.Checked : false;

            if (enableBatchStatus && enableBatchName && enableBatchID && enableSubmissionID && enableCounty && enableUserName)
            {
            }
        }
     
        /// <summary>
        /// Determine the appropriate search method based on checked search options
        /// </summary>
        private void DetermineSearchBy()
        {
            if (this.chkSearch_BatchID.Checked &&
                !this.chkSearch_BatchName.Checked &&
                !this.chkSearch_County.Checked &&
                !this.chkSearch_Status.Checked &&
                !this.chkSearch_SubmissionID.Checked &&
                !this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.BatchID;
            }
            else if (!this.chkSearch_BatchID.Checked &&
                this.chkSearch_BatchName.Checked &&
                !this.chkSearch_County.Checked &&
                !this.chkSearch_Status.Checked &&
                !this.chkSearch_SubmissionID.Checked &&
                !this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.BatchName;
            }
            else if (!this.chkSearch_BatchID.Checked &&
                !this.chkSearch_BatchName.Checked &&
                !this.chkSearch_County.Checked &&
                this.chkSearch_Status.Checked &&
                !this.chkSearch_SubmissionID.Checked &&
                !this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.StatusCodeAndDateRange;
            }
            else if (!this.chkSearch_BatchID.Checked &&
                !this.chkSearch_BatchName.Checked &&
                this.chkSearch_County.Checked &&
                this.chkSearch_Status.Checked &&
                !this.chkSearch_SubmissionID.Checked &&
                !this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.StatusCodeDateRangeCounty;
            }
            else if (!this.chkSearch_BatchID.Checked &&
                !this.chkSearch_BatchName.Checked &&
                !this.chkSearch_County.Checked &&
                !this.chkSearch_Status.Checked &&
                this.chkSearch_SubmissionID.Checked &&
                !this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.SubmissionID;
            }
            else if (!this.chkSearch_BatchID.Checked &&
                !this.chkSearch_BatchName.Checked &&
                !this.chkSearch_County.Checked &&
                !this.chkSearch_Status.Checked &&
                !this.chkSearch_SubmissionID.Checked &&
                this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.UserName;
            }
            else if (!this.chkSearch_BatchID.Checked &&
                !this.chkSearch_BatchName.Checked &&
                this.chkSearch_County.Checked &&
                !this.chkSearch_Status.Checked &&
                !this.chkSearch_SubmissionID.Checked &&
                this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.UserNameCounty;
            }
            else if (!this.chkSearch_BatchID.Checked &&
                !this.chkSearch_BatchName.Checked &&
                !this.chkSearch_County.Checked &&
                this.chkSearch_Status.Checked &&
                !this.chkSearch_SubmissionID.Checked &&
                this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.UserNameStatusCodeAndDateRange;
            }
            else if (!this.chkSearch_BatchID.Checked &&
                !this.chkSearch_BatchName.Checked &&
                this.chkSearch_County.Checked &&
                this.chkSearch_Status.Checked &&
                !this.chkSearch_SubmissionID.Checked &&
                this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.UserNameStatusCodeCounty;
            }
            else if (!this.chkSearch_BatchID.Checked &&
                !this.chkSearch_BatchName.Checked &&
                !this.chkSearch_County.Checked &&
                !this.chkSearch_Status.Checked &&
                !this.chkSearch_SubmissionID.Checked &&
                !this.chkSearch_UserName.Checked)
            {
                this._searchBy = SearchBy.None;
            }
            else
            {
                this._searchBy = SearchBy.None;
            }
        }
     
        /// <summary>
        /// Configure the search Options
        /// </summary>
        private void ConfigureSearchOptions()
        {
            this.ConfigureSearchOptions(
                this.chkSearch_BatchID.Enabled,
                this.chkSearch_BatchName.Enabled,
                this.chkSearch_SubmissionID.Enabled,
                this.chkSearch_Status.Enabled,
                this.chkSearch_County.Enabled,
                this.chkSearch_UserName.Enabled);
        }
    
        /// <summary>
        /// Create documentdetail for each document in local batchdetail
        /// </summary>
        private void CreateEmptyDocumentDetail()
        {
            foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
            {
                foreach (DocumentInfo documentInfo in batchDetail.Documents)
                {
                    DocumentDetail documentDetail = this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID);
                    if (documentDetail == null)
                    {
                        documentDetail = TestController.CreateDocumentDetail(documentInfo, batchDetail._ID);
                        this._workSet_DocumentDetail.Add(documentDetail);
                    }
                    else
                    {
                        // Update documentdetail to match the one in document info
                        documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo = documentInfo.RecordableFileContentInfo;
                        if (documentDetail.FileContentDetails.PCORFileContentDetail == null)
                        {
                            documentDetail.FileContentDetails.PCORFileContentDetail = new FileContentDetail();
                            documentDetail.FileContentDetails.PCORFileContentDetail.FileContent = new FileContent[0];
                        }
                        documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo = documentInfo.PCORFileContentInfo;
                        if (documentDetail.FileContentDetails.OtherFileContentDetail == null)
                        {
                            documentDetail.FileContentDetails.OtherFileContentDetail = new FileContentDetail();
                            documentDetail.FileContentDetails.OtherFileContentDetail.FileContent = new FileContent[0];
                        }
                        documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo = documentInfo.OtherFileContentInfo;
                    }
                }
            }
        }
   
        /// <summary>
        /// Load the TreeView with BatchDetails
        /// </summary>
        /// <param name="batchDetailList<>">List<>, containing BatchDetails</param>
        private void LoadTree(List<BatchDetail> batchDetailList)
        {
            this.LoadTree(batchDetailList.ToArray());
        }
     
        /// <summary>
        /// Load the TreeView with BatchDetails
        /// </summary>
        /// <param name="batchDetails">BatchDetail[], containing BatchDetails</param>
        private void LoadTree(BatchDetail[] batchDetails)
        {
            this._lastSelectedNode = null;
            this.trvBatches.Nodes.Clear();
            this.CreateEmptyDocumentDetail();
            foreach (BatchDetail batchDetail in batchDetails)
            {
                TreeNode newBatchNode = new TreeNode(
                    string.Format("Batch [{0}][{1}]",
                            batchDetail._ID,
                            batchDetail._SubmissionID.ToUpper()));
                newBatchNode.Tag = batchDetail;
                newBatchNode.ImageIndex = 0;
                newBatchNode.SelectedImageIndex = 0;
                foreach (DocumentInfo documentInfo in batchDetail.Documents)
                {
                    if (documentInfo.ActionCode != ActionCode.Delete)
                    {
                        TreeNode newDocNode = new TreeNode(string.Format("Document [{0}]", documentInfo._ID));
                        newDocNode.Tag = documentInfo;
                        newDocNode.ImageIndex = 1;
                        newDocNode.SelectedImageIndex = 1;
                        newBatchNode.Nodes.Add(newDocNode);
                    }
                }
                trvBatches.Nodes.Add(newBatchNode);
            }
            this.trvBatches.ExpandAll();
        }
     
        /// <summary>
        /// Change Document Status code
        /// </summary>
        private async Task ChangeDocumentStatus(DocumentMediaStatusCode newStatusCode)
        {
            try
            {
                if (!(new DocumentMediaStatusCode[]{DocumentMediaStatusCode.Correction, DocumentMediaStatusCode.Recorded, DocumentMediaStatusCode.Rejected}).Contains<DocumentMediaStatusCode>(newStatusCode))
                {
                    throw new Exception("Invalid Document Status Code");
                }
                BatchDetail batchDetail = (BatchDetail)this._lastSelectedNode.Parent.Tag;
                DocumentInfo documentInfo = (DocumentInfo)this._lastSelectedNode.Tag;

                this.LoadingBarShow(string.Format("Setting Document [{0}] to [{1}]", documentInfo._ID, DocumentMediaStatusCode.Rejected));

                if (!(batchDetail.CheckedOutBy == null) &&
                    batchDetail.CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper())
                {
                    documentInfo.StatusCode = newStatusCode;
                    documentInfo.ActionCode = ActionCode.Edit;
                    // Cannot write until finished
                    this.LoadTree(this._workSet_BatchDetail);
                    string prevBatchID = batchDetail._ID;
                    string prevDocID = documentInfo._ID;
                    this.LoadFlowPanel(batchDetail, documentInfo, this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID));
                    foreach (TreeNode batchNode in this.trvBatches.Nodes)
                    {
                        foreach (TreeNode docNode in batchNode.Nodes)
                        {
                            if (((DocumentInfo)docNode.Tag)._ID == prevDocID)
                            {
                                this._lastSelectedNode = docNode;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception(string.Format("Error: Batch must be checked out to [{0}] to change Document Status", TestController.Default_UserName_County));
                }
            }
            catch (Exception ex)
            {
                this.LoadingBarHide();
                this.HandleException(ex);               
            }
            finally
            {
                this.LoadingBarHide();
            }
        }

        /// <summary>
        /// Update the BatchStatus based on document status
        /// </summary>
        /// <param name="batchDetail">BatchDetail, containing the batch</param>
        private bool UpdateBatchStatus(BatchDetail batchDetail)
        {
            bool success = true;
            if (batchDetail.StatusCode == BatchMediaStatusCode.Downloaded_By_County || batchDetail.StatusCode == BatchMediaStatusCode.Corrected_Downloaded_By_County)
            {
                DocumentMediaStatusCode[] docStatuses = new DocumentMediaStatusCode[batchDetail.Documents.Length];
                for (int i = 0; i < batchDetail.Documents.Length; i++)
                {
                    docStatuses[i] = batchDetail.Documents[i].StatusCode;
                }
                bool foundCorrection = docStatuses.Contains<DocumentMediaStatusCode>(DocumentMediaStatusCode.Correction);
                bool foundRecorded = docStatuses.Contains<DocumentMediaStatusCode>(DocumentMediaStatusCode.Recorded);
                bool foundUploaded = docStatuses.Contains<DocumentMediaStatusCode>(DocumentMediaStatusCode.Uploaded);
                bool foundRejected = docStatuses.Contains<DocumentMediaStatusCode>(DocumentMediaStatusCode.Rejected);
                bool foundCorrected = docStatuses.Contains<DocumentMediaStatusCode>(DocumentMediaStatusCode.Corrected);

                if (foundCorrection && !foundRecorded)
                {
                    batchDetail.StatusCode = BatchMediaStatusCode.County_Uploading_Correction;
                    if (foundCorrected)
                    {
                        foreach (DocumentInfo documentInfo in batchDetail.Documents)
                        {
                            if (documentInfo.StatusCode == DocumentMediaStatusCode.Corrected)
                            {
                                // Change StatusCode to Uploaded, Corrected is invalid for correction batch
                                documentInfo.StatusCode = DocumentMediaStatusCode.Uploaded;
                            }
                        }
                    }
                }
                else if (!foundCorrection && !foundUploaded && !foundCorrected && (foundRecorded || foundRejected))
                {
                    batchDetail.StatusCode = BatchMediaStatusCode.County_Uploading_Completed;
                }
                else
                {
                    //Invalid Document Status Combination
                    string codeCombo = string.Empty;
                    foreach (DocumentMediaStatusCode docStatusCode in docStatuses)
                    {
                        codeCombo = string.Format((codeCombo==string.Empty? "{0}{1}" : "{0},{1}"), codeCombo, docStatusCode);
                    }
                    string exceptionMessage = string.Format("Submission canceled because Batch [{0}] contains an invalid combination of Document status codes:\r\n\r\n[{1}]\r\n\r\nBatch must contain at least one [Correction] document for submission as [Correction].\r\nBatch must contain only [Rejected] or [Recorded] documents for submission as [Completed].", batchDetail._ID, codeCombo);
                    (new ExceptionBox() 
                    { 
                        ExceptionMessage =  exceptionMessage
                    }).ShowDialog(this);
                    success = false;
                }
            }
            return success;
        }
    
        /// <summary>
        /// Determined if there are changes in the local DocumentDetail
        /// </summary>
        /// <param name="documentDetails">DocumentDetail[], array containing local document detail</param>
        /// <returns>bool, true if there are changes</returns>
        private bool DocumentDetailsChanged(DocumentDetail[] documentDetails)
        {
            bool hasChanges = true;
            foreach (DocumentDetail documentDetail in documentDetails)
            {
                if (documentDetail.FileContentDetails != null)
                {
                    foreach (FileContentDetail fileContentDetail in new FileContentDetail[] 
                                { 
                                    documentDetail.FileContentDetails.RecordableFileContentDetail, 
                                    documentDetail.FileContentDetails.PCORFileContentDetail, 
                                    documentDetail.FileContentDetails.OtherFileContentDetail 
                                })
                    {
                        if (fileContentDetail != null)
                        {
                            if (fileContentDetail.FileContentInfo != null)
                            {
                                if (fileContentDetail.FileContentInfo.ActionCode != ActionCode.None)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return hasChanges;
        }
    
        /// <summary>
        /// Get County info for a county
        /// </summary>
        /// <param name="countyName">string, containing the name of the county</param>
        /// <returns>CountyInfo for the specified county</returns>
        private CountyInfo GetCountyInfo(string countyName)
        {
            foreach (CountyInfo countyInfo in this._cache_countiesInfo.County)
            {
                if (countyInfo.Name == countyName)
                {
                    return countyInfo;
                }
            }
            return null;
        }
     
        /// <summary>
        /// Load objects into UserControls that are in a Flow Layout Panel
        /// </summary>
        /// <param name="batchDetail">BatchDetail, the currently selected object representing a Batch</param>
        /// <param name="documentInfo">Documentinfo, the currently selected object representing a Document</param>
        /// <param name="documentDetail">DocumentInfo, the currently selected object representing a Document's images</param>
        private void LoadFlowPanel(BatchDetail batchDetail, DocumentInfo documentInfo, DocumentDetail documentDetail)
        {
            // BatchDetail control
            //=============================================================
            this.batchDetailControl.Visible = true;
            if (this.batchDetailControl.BatchDetail != batchDetail)
            {
                this.batchDetailControl.BatchDetail = null;
                // Load cache data into control
                this.batchDetailControl.CountyInfo = this._cache_countiesInfo.County;
                this.batchDetailControl.ProcessQueueDetail = this._cache_processQueuesDetail.ProcessQueue;
                // Set enabled index options
                this.batchDetailControl.EnableConcurrentIndex = true;
                this.batchDetailControl.EnableRequestingParty = true;
                this.batchDetailControl.BatchDetail = batchDetail;
            }
            else
            {
                this.batchDetailControl.Refresh();
            }
            if (documentDetail != null && int.Parse(documentDetail._BatchID) < 0)
            {
                // Not yet saved to DB
                this.batchDetailControl.EnableEdit = true;
            }
            else
            {
                // Editing
                this.batchDetailControl.EnableEdit =
                    (new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode);
            }
            // DocumentInfo control
            //=============================================================
            if (documentInfo == null)
            {
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;

                this.batchDetailControl.EnableEdit =
                    (new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode);
            }
            else
            {
                this.batchDetailControl.EnableEdit = false;
                this.documentInfoControl.Visible = true;
                this.documentInfoControl.AllTitles = this.GetLocalTitleDetail(batchDetail.County._ID).ToList<TitleDetail>();
                this.documentInfoControl.AllCities = this.GetLocalCityDetail(batchDetail.County._ID).ToList<CityDetail>();
                this.documentInfoControl.EnableExternalID = false;
                if (this.documentInfoControl.DocumentInfo != documentInfo)
                {
                    if (batchDetail.StatusCode == BatchMediaStatusCode.Downloaded_By_County || batchDetail.StatusCode == BatchMediaStatusCode.Corrected_Downloaded_By_County)
                    {
                        if (batchDetail.CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper())
                        {
                            if (!documentInfo.RecordedTimeStampSpecified)
                            {
                                documentInfo.RecordedTimeStampSpecified = true;
                                documentInfo.RecordedTimeStamp = DateTime.Now;
                                documentInfo.ActionCode = ActionCode.Edit;
                            }
                        }
                    }
                    this.documentInfoControl.DocumentInfo = documentInfo;
                    // enable all edit options for county user
                    this.documentInfoControl.EnableAssessorParcelNumber = true;
                    this.documentInfoControl.EnableCities = true;
                    this.documentInfoControl.EnableNames = true;
                    this.documentInfoControl.EnableSaleAmount = true;
                    this.documentInfoControl.EnableTransferTaxAmount = true;
                    this.documentInfoControl.EnableDocumentNumber = true;
                    this.documentInfoControl.EnableFees = true;
                    this.documentInfoControl.EnableTitles = true;
                    this.documentInfoControl.EnableRecordedTS = true;
                    this.documentInfoControl.EnableSequence = ((new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                    this.documentInfoControl.EnableMemo = ((new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                }
                else
                {
                    this.documentInfoControl.Refresh();
                }
                // Editing
                this.documentInfoControl.EnableEdit = ((new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                //=============================================================
                this.flowFileContent.Visible = true;
                // Recordable FileContentDetail
                //=============================================================
                this.fileContentInfoControl_RECORDABLE.Visible = true;
                if (this.fileContentInfoControl_RECORDABLE.FileContentDetail != documentDetail.FileContentDetails.RecordableFileContentDetail)
                {
                    this.fileContentInfoControl_RECORDABLE.FileContentDetail = documentDetail.FileContentDetails.RecordableFileContentDetail;
                }
                else
                {
                    this.fileContentInfoControl_RECORDABLE.Refresh();
                }
                // Editing
                this.fileContentInfoControl_RECORDABLE.EnableEdit(
                    (new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                this.fileContentInfoControl_RECORDABLE.EnableFileReplace(
                    (new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                // PCOR FileContentDetail
                //=============================================================
                if (documentDetail.FileContentDetails.PCORFileContentDetail != null && documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo != null)
                {
                    this.fileContentInfoControl_PCOR.Visible = true;
                    if (this.fileContentInfoControl_PCOR.FileContentDetail != documentDetail.FileContentDetails.PCORFileContentDetail)
                    {
                        this.fileContentInfoControl_PCOR.FileContentDetail = documentDetail.FileContentDetails.PCORFileContentDetail;
                    }
                    else
                    {
                        this.fileContentInfoControl_PCOR.Refresh();
                    }
                    // Editing
                    this.fileContentInfoControl_PCOR.EnableEdit(
                        (new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                    this.fileContentInfoControl_PCOR.EnableFileReplace(
                        (new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                }
                else
                {
                    this.fileContentInfoControl_PCOR.Visible = false;
                }
                // Other FileContentDetail
                //=============================================================
                if (documentDetail.FileContentDetails.OtherFileContentDetail != null && documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo != null)
                {
                    this.fileContentInfoControl_OTHER.Visible = true;
                    if (this.fileContentInfoControl_OTHER.FileContentDetail != documentDetail.FileContentDetails.OtherFileContentDetail)
                    {
                        this.fileContentInfoControl_OTHER.FileContentDetail = documentDetail.FileContentDetails.OtherFileContentDetail;
                    }
                    else
                    {
                        this.fileContentInfoControl_OTHER.Refresh();
                    } // Editing
                    this.fileContentInfoControl_OTHER.EnableEdit(
                        (new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                    this.fileContentInfoControl_OTHER.EnableFileReplace(
                        (new BatchMediaStatusCode[] { BatchMediaStatusCode.Downloaded_By_County, BatchMediaStatusCode.Corrected_Downloaded_By_County }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                }
                else
                {
                    this.fileContentInfoControl_OTHER.Visible = false;
                }
            }
        }
        
        #region Document
     
        /// <summary>
        /// Get Titles for a county
        /// </summary>
        /// <param name="countyID">string, containing the Count's ID</param>
        /// <returns>TitleDetail[], containing titles for the specified county</returns>
        private TitleDetail[] GetLocalTitleDetail(string countyID)
        {
            List<TitleDetail> countyTitles = new List<TitleDetail>();
            if (this._cache_titlesDetail != null && this._cache_titlesDetail.Title != null)
            {
                foreach (TitleDetail titleDetail in this._cache_titlesDetail.Title)
                {
                    if (titleDetail._CountyID == countyID)
                    {
                        countyTitles.Add(titleDetail);
                    }
                }
            }
            return countyTitles.ToArray();
        }
     
        /// <summary>
        /// Get cities for a county
        /// </summary>
        /// <param name="countyID">string, containing the Count's ID</param>
        /// <returns>CityDetail[], containing cities for the specified county</returns>
        private CityDetail[] GetLocalCityDetail(string countyID)
        {
            List<CityDetail> countyCities = new List<CityDetail>();
            if (this._cache_citiesDetail != null && this._cache_citiesDetail.City != null)
            {
                foreach (CityDetail cityDetail in this._cache_citiesDetail.City)
                {
                    if (cityDetail._CountyID == countyID)
                    {
                        countyCities.Add(cityDetail);
                    }
                }
            }
            return countyCities.ToArray();
        }

        /// <summary>
        /// Get DocumentInfo
        /// </summary>
        /// <param name="documentDetail">DocumentDetail, containing status and image data for a document</param>
        /// <returns>DocumentInfo of specified document</returns>
        private DocumentInfo GetLocalDocumentInfo(DocumentDetail documentDetail)
        {
            foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
            {
                if (batchDetail._ID == documentDetail._BatchID)
                {
                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                    {
                        if (documentInfo._ID == documentDetail._ID)
                        {
                            return documentInfo;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Get DocumentInfo
        /// </summary>
        /// <param name="documentDetail">DocumentDetail, containing status and image data for a document</param>
        /// <returns>DocumentInfo of specified document</returns>
        private DocumentInfo GetLocalDocumentInfo(int sequence, BatchDetail batchDetail)
        {
            foreach (DocumentInfo documentInfo in batchDetail.Documents)
            {
                if (documentInfo.Sequence == sequence)
                {
                    return documentInfo;
                }
            }
            return null;
        }
    
        /// <summary>
        /// Get DocumentDetail
        /// </summary>
        /// <param name="documentID">string, containing the document ID</param>
        /// <param name="batchID">string, containing the batch ID</param>
        /// <returns>DocumentDetail of specified document</returns>
        private DocumentDetail GetLocalDocumentDetail(string documentID, string batchID)
        {
            foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
            {
                if (documentDetail._ID == documentID && documentDetail._BatchID == batchID)
                {
                    return documentDetail;
                }
            }
            return null;
        }
     
        #endregion

        #region Upload

        /// <summary>
        /// Perform API call: Batch/UploadComplete
        /// </summary>
        /// <param name="batchDetail">BatchDetail, the batch on which to perform this operation</param>
        /// <returns>Task</returns>
        private async Task<BatchDetail> UploadBatchComplete(string batchSubmissionID)
        {
            try
            {
                this.LoadingBarShow("Setting Batch/UploadComplete");
                BatchDetail batchDetail = null;
                foreach (BatchDetail btDetail in this._workSet_BatchDetail)
                {
                    if (btDetail._SubmissionID == batchSubmissionID)
                    {
                        batchDetail = btDetail;
                    }
                }
                if (batchDetail.StatusCode == BatchMediaStatusCode.County_Uploading_Correction)
                {
                    batchDetail.StatusCode = BatchMediaStatusCode.Correction_Uploaded_By_County;
                }
                else if (batchDetail.StatusCode == BatchMediaStatusCode.County_Uploading_Completed)
                {
                    batchDetail.StatusCode = BatchMediaStatusCode.Completed_Uploaded_By_County;
                }
                string responseText = await this.PerformHTTP_Async(
                    HTTP.PUT,
                    new string[] { "Batch", "UploadComplete" },
                    new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID),
                                new DictionaryEntry("BatchStatus", batchDetail.StatusCode)
                            },
                    null,
                    true);
                BatchDetail resultBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);

                this._workSet_BatchDetail.Remove(batchDetail);
                this._workSet_BatchDetail.Add(resultBatchDetail);
                foreach (BatchDetail btDetail in this._workSet_BatchDetail)
                {
                    if (btDetail._ID == batchDetail._ID)
                    {
                        batchDetail = btDetail;
                    }
                }
                return resultBatchDetail;
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                  this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex); 
                }
            }
            return null;
        }

        /// <summary>
        /// Perform API call: Document/Upload
        /// </summary>
        private async void DocumentsUpload()
        {
            try
            {
                this.LoadingBarShow("/api/Document/Upload");
                List<DocumentDetail> resultDocumentDetails = new List<DocumentDetail>();
                foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
                {
                    // Prep FileContentDetail objects
                    if (documentDetail.FileContentDetails.RecordableFileContentDetail != null)
                    {
                        if (documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo == null)
                        {
                            documentDetail.FileContentDetails.RecordableFileContentDetail = null;
                        }
                        else
                        {
                            documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                        }
                    }
                    if (documentDetail.FileContentDetails.PCORFileContentDetail != null)
                    {
                        if (documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo == null)
                        {
                            documentDetail.FileContentDetails.PCORFileContentDetail = null;
                        }
                        else
                        {
                            documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                        }
                    }
                    if (documentDetail.FileContentDetails.OtherFileContentDetail != null)
                    {
                        if (documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo == null)
                        {
                            documentDetail.FileContentDetails.OtherFileContentDetail = null;
                        }
                        else
                        {
                            documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                        }
                    }
                    DocumentInfo relatedDocumentInfo = this.GetLocalDocumentInfo(documentDetail);
                    documentDetail.StatusCode = relatedDocumentInfo.StatusCode;
                    //==============================================
                    string xml = TestController.SerializeXML(documentDetail, this._enableValidation);
                    string responseText = await this.PerformHTTP_Async(
                        HTTP.PUT,
                        new string[] { "Document", "Upload" },
                        null,
                        new StringContent(
                                xml,
                                Encoding.UTF8,
                                SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                        true);
                    if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                      this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                    {
                        resultDocumentDetails.Add(TestController.DeserializeXML<DocumentDetail>(responseText, false));
                    }
                    else
                    {
                        this.LoadingBarHide();
                        this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                        return;
                    }
                }
                this._workSet_DocumentDetail = resultDocumentDetails;
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
                this.LoadTree(this._workSet_BatchDetail);
            }
            catch (Exception ex)
            {
                if (ex.Message == "A task was cancelled")
                {
                    this.LoadingBarHide();
                    MessageBox.Show(this, "Operation took too long", "");
                }
                else
                {
                    if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                      this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                    {
                        this.LoadingBarHide();
                        this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        this.HandleException(ex);
                    }
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
    
        /// <summary>
        /// Perform API call: Batch/Upload
        /// </summary>
        private async void BatchUpload()
        {
            try
            {
                this.LoadingBarShow("SECURE API/Batch/Upload");

                Hashtable tempNewBatchNumbers = new Hashtable(new Dictionary<string, string>());
                Hashtable tempNewDocumentNumbers = new Hashtable(new Dictionary<string, string>());

                List<BatchDetail> temp_workset_BatchDetail = new List<BatchDetail>();
                int outX = -1;
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    if (!int.TryParse(batchDetail._ID, out outX))
                    {
                        // New Batch
                        string xml = TestController.SerializeXML(batchDetail, this._enableValidation);

                        // send to SECURE
                        string responseText = await this.PerformHTTP_Async(
                                HTTP.POST,
                                new string[] { "Batch", "Upload" },
                                null,
                                new StringContent(
                                    TestController.SerializeXML(batchDetail, this._enableValidation),
                                    Encoding.UTF8,
                                    SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                                    true);
                        // Saved batch response
                        BatchDetail createdBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                        temp_workset_BatchDetail.Add(createdBatchDetail);
                        tempNewBatchNumbers.Add(batchDetail._ID, createdBatchDetail._ID);
                        foreach (DocumentInfo documentInfo in batchDetail.Documents)
                        {
                            foreach (DocumentInfo createdDocumentInfo in createdBatchDetail.Documents)
                            {
                                if (documentInfo.Sequence == createdDocumentInfo.Sequence)
                                {
                                    tempNewDocumentNumbers.Add(documentInfo._ID, createdDocumentInfo._ID);
                                }
                            }
                        }
                    }
                    else
                    {
                        temp_workset_BatchDetail.Add(batchDetail);
                    }
                }
                this._workSet_BatchDetail = temp_workset_BatchDetail;
                this.LoadTree(this._workSet_BatchDetail);
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                  this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }

        #endregion

        #region Download
     
        /// <summary>
        /// Download document image
        /// </summary>
        /// <param name="documentID">string, containing the Document's ID</param>
        /// <returns>DocumentDetail, containing the image data</returns>
        private async Task<DocumentDetail> DownloadDocument(string documentID)
        {
            string responseText = null;
            try
            {
                GC.Collect();
                responseText = await this.PerformHTTP_Async(
                    HTTP.GET,
                    new string[] { "Document", "Download" },
                    new DictionaryEntry[] 
                        {
                           
                            new DictionaryEntry("DocID", documentID)
                        },
                    null,
                    true);
                if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    try
                    {
                        return await Task<string>.Run(() =>
                        {
                            return TestController.DeserializeXML<DocumentDetail>(responseText, this._enableValidation); 
                        }).ConfigureAwait(true);
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
                else
                {
                    this.LoadingBarHide();
                    return null;
                }
            }
            finally
            {
                responseText = null;
                GC.Collect(3, GCCollectionMode.Forced, true);
            }
        }

        /// <summary>
        /// Event method for Batch/DownloadComplete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BatchDownloadComplete(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Setting Batch Download Complete");
                List<BatchDetail> _workset_BatchDetail_DownloadComplete = new List<BatchDetail>();
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    if (batchDetail.StatusCode == BatchMediaStatusCode.Uploaded_By_Submitter || batchDetail.StatusCode == BatchMediaStatusCode.Submitter_Uploading_Corrected)
                    {
                        BatchMediaStatusCode completeStatus = batchDetail.StatusCode == BatchMediaStatusCode.Completed_Uploaded_By_County ? BatchMediaStatusCode.Completed_Downloaded_By_Submitter : BatchMediaStatusCode.Corrected_Uploaded_By_Submitter;
                        string responseText = await this.PerformHTTP_Async(
                            HTTP.PUT,
                            new string[] { "Batch", "DownloadComplete" },
                            new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID),
                                new DictionaryEntry("BatchStatus", completeStatus.ToString())
                            },
                            null,
                            true);
                        BatchDetail resultBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                        _workset_BatchDetail_DownloadComplete.Add(resultBatchDetail);
                    }
                    else
                    {
                        _workset_BatchDetail_DownloadComplete.Add(batchDetail);
                    }
                }
                this._workSet_BatchDetail = _workset_BatchDetail_DownloadComplete;
                this.LoadTree(this._workSet_BatchDetail);
            }
            catch (Exception ex)
            {
                if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                    this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    this.HandleException(ex);
                }
            }
            finally
            {
                this.LoadingBarHide();
            }
        }

        #endregion

        private async Task<bool> ExpireBatch(BatchDetail selectedBatch, string expirationMessage)
        {
            try
            {
                string responseText = await this.PerformHTTP_Async(
                                      HTTP.PUT,
                                      new string[] { "Batch", "Expire" },
                                      new DictionaryEntry[]
                                       {
                                       
                                        new DictionaryEntry("BatchID",selectedBatch._ID),
                                        new DictionaryEntry("ExpirationMessage", expirationMessage)
                                       },
                                      null,
                                      true);

                if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                              this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                {
                    ResultDetail response = TestController.DeserializeXML<ResultDetail>(responseText, this._enableValidation);
                    if (response.Value)
                    {
                        //successful
                        MessageBox.Show(this, string.Format("Batch[{0}] Expired Successfully.", selectedBatch._ID), "Batch Expired", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //refresh batchdetail
                        this._workSet_BatchDetail.Remove(selectedBatch);
                        string newBatchResponse =
                                await this.PerformHTTP_Async(
                                    HTTP.GET,
                                    new string[] { "Batch", "ReadByID" },
                                    new DictionaryEntry[] 
                                    { 
                                        
                                        new DictionaryEntry("BatchID", selectedBatch._ID)
                                    },
                                    null,
                                    true);
                        if (!(this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                        {
                            this.DisplayHTTPError(await this._testController.Last_HTTPResponse.Content.ReadAsStringAsync());
                        }
                        else
                        {
                            selectedBatch = TestController.DeserializeXML<BatchDetail>(newBatchResponse, this._enableValidation);
                            this._workSet_BatchDetail.Add(selectedBatch);
                        }
                    }
                    else
                    {
                        //fail
                        MessageBox.Show(this, "Failed to expire batch");
                    }
                }
                else
                {
                    this.LoadingBarHide();
                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                return false;
            }
            finally
            {
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
                this.LoadingBarHide();
            }
            return true;
        }
        /// <summary>
        /// Configure visibility of Content Menu items
        /// </summary>
        /// <param name="selectedType">Type of selected object</param>
        private void ConfigureContextMenu_Visible(Type selectedType)
        {
            this.mnuDownloadBatch.Visible =
                this.mnuExpireBatch.Visible =
                this.mnuSubmitBatch.Visible =
                this.mnuCheckInBatch.Visible =
                this.mnuCheckOutBatch.Visible =
                this.mnuSeparator1.Visible =
                this.mnuSeparator2.Visible =
                this.mnuDownloadDocumentImages.Visible = 
                (selectedType == typeof(BatchDetail));

            this.mnuCorrectDocument.Visible =
                this.mnuRecordDocument.Visible =
                this.mnuRejectDocument.Visible =
                (selectedType == typeof(DocumentInfo));
        }
        #region Save
   
        /// <summary>
        /// Write local data to SECURE API
        /// </summary>
        /// <param name="batchSubmissionID">string, containing the submission ID of the batch to be uploaded</param>
        /// <param name="documentUpload">bool, true to upload document images</param>
        /// <returns>Task(bool), true if successful</returns>
        private async Task<bool> SaveBatchToSECURE(string batchSubmissionID, bool documentUpload)
        {
            try
            {
                #region Update BatchDetail

                BatchDetail batchDetail = null;
                foreach (BatchDetail btDetail in this._workSet_BatchDetail)
                {
                    if (btDetail._SubmissionID == batchSubmissionID)
                    {
                        batchDetail = btDetail;
                    }
                }
                if (batchDetail.CheckedOutBy != null && batchDetail.CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_County.ToUpper())
                {
                    // Update action code of DocumentInfo if FileContentInfo has new ActionCode
                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                    {
                        documentInfo.ActionCode = (
                            (documentInfo.RecordableFileContentInfo.ActionCode != ActionCode.None) ||
                            (documentInfo.PCORFileContentInfo != null && documentInfo.PCORFileContentInfo.ActionCode != ActionCode.None) ||
                            (documentInfo.OtherFileContentInfo != null && documentInfo.OtherFileContentInfo.ActionCode != ActionCode.None)
                            ) ? ActionCode.Edit : documentInfo.ActionCode;
                    }
                    if (batchDetail.StatusCode == BatchMediaStatusCode.Downloaded_By_County || batchDetail.StatusCode == BatchMediaStatusCode.Corrected_Downloaded_By_County)
                    {
                        if (this.UpdateBatchStatus(batchDetail))
                        {
                            // Update existing batch
                            string responseText = await this.PerformHTTP_Async(
                                           HTTP.PUT,
                                           new string[] { "Batch", "Upload" },
                                           null,
                                           new StringContent(
                                               TestController.SerializeXML(batchDetail, this._enableValidation),
                                               Encoding.UTF8,
                                               SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                                           true);
                            if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                BatchDetail resultBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                                //replace local batch with updated
                                this._workSet_BatchDetail.Remove(batchDetail);
                                this._workSet_BatchDetail.Add(resultBatchDetail);
                                foreach (BatchDetail btDetail in this._workSet_BatchDetail)
                                {
                                    if (btDetail._ID == batchDetail._ID)
                                    {
                                        batchDetail = btDetail;
                                    }
                                }
                            }
                            else
                            {
                                this.LoadingBarHide();
                                this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(this, "Cannot edit a batch that is not checked out to you.", "EDIT FAIL", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                #endregion

                #region Upload DocumentDetail

                foreach (DocumentInfo documentInfo in batchDetail.Documents)
                {
                    DocumentDetail documentDetail = this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID);
                    if (documentUpload && documentDetail != null)
                    {
                        bool uploadDocument = false;
                        if (documentDetail.FileContentDetails.RecordableFileContentDetail != null)
                        {
                            if (documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo != null && 
                                documentDetail.FileContentDetails.RecordableFileContentDetail.FileContent != null &&
                                documentDetail.FileContentDetails.RecordableFileContentDetail.FileContent.Length > 0)
                            {
                                uploadDocument = true;
                            }
                        }
                        if (documentDetail.FileContentDetails.PCORFileContentDetail != null)
                        {
                            if (documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo != null &&
                                documentDetail.FileContentDetails.PCORFileContentDetail.FileContent != null &&
                                documentDetail.FileContentDetails.PCORFileContentDetail.FileContent.Length > 0)
                            {
                                uploadDocument = true;
                            }
                        }
                        if (documentDetail.FileContentDetails.OtherFileContentDetail != null && 
                            documentDetail.FileContentDetails.OtherFileContentDetail.FileContent != null &&
                            documentDetail.FileContentDetails.OtherFileContentDetail.FileContent.Length > 0)
                        {
                            if (documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo != null)
                            {
                                uploadDocument = true;
                            }
                        }
                        if (uploadDocument && documentUpload)
                        {
                            if (documentDetail.FileContentDetails.RecordableFileContentDetail != null)
                            {
                                if (documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo == null)
                                {
                                    documentDetail.FileContentDetails.RecordableFileContentDetail = null;
                                }
                                else
                                {
                                    documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                                }
                            }
                            if (documentDetail.FileContentDetails.PCORFileContentDetail != null)
                            {
                                if (documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo == null)
                                {
                                    documentDetail.FileContentDetails.PCORFileContentDetail = null;
                                }
                                else
                                {
                                    documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                                }
                            }
                            if (documentDetail.FileContentDetails.OtherFileContentDetail != null)
                            {
                                if (documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo == null)
                                {
                                    documentDetail.FileContentDetails.OtherFileContentDetail = null;
                                }
                                else
                                {
                                    documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                                }
                            }
                            documentDetail.StatusCode = documentInfo.StatusCode;
                            string xml = TestController.SerializeXML(documentDetail, this._enableValidation);
                            if (documentDetail.StatusCode == DocumentMediaStatusCode.Correction || documentDetail.StatusCode == DocumentMediaStatusCode.Recorded)
                            {
                                string responseText = await this.PerformHTTP_Async(
                                    HTTP.PUT,
                                    new string[] { "Document", "Upload" },
                                    null,
                                    new StringContent(
                                            xml,
                                            Encoding.UTF8,
                                            SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                                     true);
                                if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                                  this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                                {
                                    this._workSet_DocumentDetail.Remove(documentDetail);
                                    this._workSet_DocumentDetail.Add(TestController.DeserializeXML<DocumentDetail>(responseText, false));
                                }
                                else
                                {
                                    this.LoadingBarHide();
                                    this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                                    return false;
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                return false;
            }
            return true;
        }
      
        #endregion

        private void HandleException(Exception exception)
        {
            try
            {
                string exceptionMessage = string.Empty;
                if (this._testController.Last_HTTPResponse != null)
                {
                    exceptionMessage += "URI: " + this._testController.Last_HTTPResponse.RequestMessage.RequestUri.ToString() + Environment.NewLine;
                    exceptionMessage += "HTTP Status: " + this._testController.Last_HTTPResponse.StatusCode + Environment.NewLine;
                    exceptionMessage += "Reason Phrase: " + this._testController.Last_HTTPResponse.ReasonPhrase + Environment.NewLine;
                    exceptionMessage += "Response: " + this._testController.Last_HTTPResponse.Content.ReadAsStringAsync() + Environment.NewLine + Environment.NewLine;
                }
                else if (!string.IsNullOrEmpty(this._testController.Last_URL))
                {
                    exceptionMessage += "URI: " + this._testController.Last_URL + Environment.NewLine + Environment.NewLine;
                }
                exceptionMessage += "Message: " + exception.Message + Environment.NewLine;
                if (exception.InnerException != null)
                {
                    Exception ie = exception;
                    while (ie.InnerException != null)
                    {
                        exceptionMessage += "Inner Message: " + ie.InnerException.Message + Environment.NewLine;
                        ie = ie.InnerException;
                    }
                }
                (new ExceptionBox() { ExceptionMessage = exceptionMessage }).ShowDialog(this);
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }
        
        #endregion
    }
}
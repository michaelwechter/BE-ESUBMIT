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
//using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using WinFormTest.ClientControls;
using SECURE.Common.Controller.Media.Schema.CodeGen;

namespace WinFormTest
{
    public partial class SubmitterPanel : UserControl
    {
        #region Declarations
        /// <summary>
        /// Enumeration describing the type of File in a document
        /// </summary>
        private enum DocumentFileType
        {
            Recordable,
            PCOR,
            Other
        }

        private TestController _testController = new TestController(UserType.Submitter);
        private HttpResponseMessage _ResponseMessage;
        private HttpRequestMessage _RequestMessage;

        private string _ResponseMessageContent = string.Empty;
        private string _RequestMessageContent = string.Empty;
        private string _ResponseMessageString = string.Empty;
        private string _RequestMessageString = string.Empty;

        private SearchBy _searchBy;
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

        private CountiesInfo _cache_countiesInfo;
        private CitiesDetail _cache_citiesDetail;
        private TitlesDetail _cache_titlesDetail;
        private ProcessQueuesDetail _cache_processQueuesDetail;
        private RequestingPartiesInfo _cache_requestingPartiesInfo;
        private SubmittingPartiesInfo _cache_SubmittingParties;
        private IndexOptionDetail[] _cache_IndexOptionsDetail;

        //private ArrayList _workSet_BatchDetail = new ArrayList();
        //private ArrayList _workSet_DocumentDetail = new ArrayList();

        private List<BatchDetail> _workSet_BatchDetail = new List<BatchDetail>();
        private List<DocumentDetail> _workSet_DocumentDetail = new List<DocumentDetail>();

        private TreeNode _lastSelectedNode = null;

        private bool _ConfiguringSearch = false;

        private ArrayList _FailMessages = new ArrayList();

        private bool _enableValidation = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// The last request message sent to the SECURE API
        /// </summary>
        public string RequestMessageString
        {
            get
            {
                return this._RequestMessageString;
            }
        }
        /// <summary>
        /// The last response message received from the SECURE API
        /// </summary>
        public string ResponseMessageString
        {
            get
            {
                return this._ResponseMessageString;
            }
        }
        /// <summary>
        /// The last request message contents sent to the SECURE API
        /// </summary>
        public string RequestMessageContent
        {
            get
            {
                return this._RequestMessageContent;
            }
        }
        /// <summary>
        /// The last response message contents received from the SECURE API
        /// </summary>
        public string ResponseMessageContent
        {
            get
            {
                return this._ResponseMessageContent;
            }
        }
        /// <summary>
        /// The last HTTPRequestMessage send to the SECURE API
        /// </summary>
        public HttpRequestMessage RequestMessage
        {
            get
            {
                return this._RequestMessage;
            }
        }
        /// <summary>
        /// The last HTTPResponseMessage received from the SECURE API
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
        /// Default constructor
        /// </summary>
        public SubmitterPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region HTTP
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
                this._cache_countiesInfo = await this.SECURE_ReadCacheData<CountiesInfo>(
                    new string[] { "Index", "County" },
                    null);
                this.cboCounty.Items.Clear();
                foreach (CountyInfo countyInfo in this._cache_countiesInfo.County)
                {
                    this.cboCounty.Items.Add(countyInfo.Name);
                }
                //=================================================================================================================
                // city
                this._cache_citiesDetail = await this.SECURE_ReadCacheData<CitiesDetail>(
                        new string[] { "Index", "City" },
                        null
                    );
                //=================================================================================================================
                // index option
                ArrayList temp_IndexOptionDetail = new ArrayList();
                foreach (CountyInfo countyInfo in this._cache_countiesInfo.County)
                {
                    IndexOptionDetail countyIndexOption = await this.SECURE_ReadCacheData<IndexOptionDetail>(
                        new string[] { "Index", "IndexOption" },
                        new DictionaryEntry[] 
                        { 
                           
                            new DictionaryEntry("CountyID", countyInfo._ID)
                        }
                        );
                    if (countyIndexOption != null)
                    {
                        temp_IndexOptionDetail.Add(countyIndexOption);
                    }
                }
                this._cache_IndexOptionsDetail = (IndexOptionDetail[])temp_IndexOptionDetail.ToArray(typeof(IndexOptionDetail));
                //=================================================================================================================
                // title
                this._cache_titlesDetail = await this.SECURE_ReadCacheData<TitlesDetail>(
                      new string[] { "Index", "TItle" },
                    null);
                //=================================================================================================================
                // process queueesDetail = await this.SECURE_ReadCacheData<ProcessQueuesDetail>(
                this._cache_processQueuesDetail = await this.SECURE_ReadCacheData<ProcessQueuesDetail>(
                    new string[] { "Index", "ProcessQueue" },
                    null);
                //=================================================================================================================
                // Requesting Party
                this._cache_requestingPartiesInfo = await this.SECURE_ReadCacheData<RequestingPartiesInfo>(
                     new string[] { "User", "RequestingParty" },
                    null);
                //=================================================================================================================
                // Submitting Party
                this._cache_SubmittingParties = await this.SECURE_ReadCacheData<SubmittingPartiesInfo>(
                    new string[] { "User", "SubmittingParty" },
                    null);
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
        private async Task<T> SECURE_ReadCacheData<T>(string[] API_path, DictionaryEntry[] parameters)
        {
            try
            {
                T receivedObject = await this.GetDeserialized<T>(
                    HTTP.GET,
                    API_path,
                    parameters,
                    null,
                    true);
                return receivedObject;
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
            if (typeof(T) == typeof(RequestingPartiesInfo))
            {
                return (T)((object)new RequestingPartiesInfo()
                {
                    RequestingParty = new RequestingPartyInfo[0]
                });
            }
            else if (typeof(T) == typeof(CountiesInfo))
            {
                return (T)((object)new CountiesInfo()
                {
                    County = new CountyInfo[0]
                });
            }
            else if (typeof(T) == typeof(CitiesDetail))
            {
                return (T)((object)new CitiesDetail()
                {
                    City = new CityDetail[0]
                });
            }
            else if (typeof(T) == typeof(IndexOptionDetail))
            {

            }
            else if (typeof(T) == typeof(TitlesDetail))
            {
                return (T)((object)new TitlesDetail()
                {
                    Title = new TitleDetail[0]
                });
            }
            else if (typeof(T) == typeof(ProcessQueuesDetail))
            {
                return (T)((object)new ProcessQueuesDetail()
                {
                    ProcessQueue = new ProcessQueueDetail[0]
                });
            }
            return default(T);
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
        /// <summary>
        /// Perform API call: Batch/UploadComplete
        /// </summary>
        /// <returns>Task</returns>
        private async Task UploadBatchComplete()
        {
            try
            {
                this.LoadingBarShow("Setting Batches Upload Complete");
                List<BatchDetail> temp_workset_BatchDetail = new List<BatchDetail>();
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    if (batchDetail.StatusCode == BatchMediaStatusCode.Submitter_Uploading || batchDetail.StatusCode == BatchMediaStatusCode.Submitter_Uploading_Corrected)
                    {
                        batchDetail.StatusCode = batchDetail.StatusCode == BatchMediaStatusCode.Submitter_Uploading ? BatchMediaStatusCode.Uploaded_By_Submitter : BatchMediaStatusCode.Corrected_Uploaded_By_Submitter;
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
        }
        /// <summary>
        /// Invalidate session of currently logged in SECURE user
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
                    string.Format("User [{0}] logged out {1}successfully.", TestController.Default_UserName_Submitter, responseDetail.Value ? string.Empty : "un"),
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

        #endregion

        #region Button Event Methods

        #region Authentication Group

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
        /// Event method for the Logout button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAuth_Logout_Click(object sender, EventArgs e)
        {
            this.PerformLogout();
            this.enableFormControls(this._testController.SessionTokenID != null);
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
                            string.Format("Password change {1} for user [{0}]. User needs to log out then log in again.", TestController.Default_UserName_Submitter, responsedetail.Value ? "successful" : "failed"),
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
        /// <summary>
        /// Event method for the Login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAuth_Login_Click(object sender, EventArgs e)
        {
            await this.PerformLoginAsync(TestController.TwoFactor_Enabled_Submitter);
            this.enableFormControls(this._testController.SessionTokenID != null && this._testController.SessionTokenID != string.Empty);
        }

        #endregion

        #region SECURE API Group

        /// <summary>
        /// Event method for Batch/Create button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAPI_BatchCreate_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("/api/Batch/Create");

                Hashtable tempNewBatchNumbers = new Hashtable(new Dictionary<string, string>());
                Hashtable tempNewDocumentNumbers = new Hashtable(new Dictionary<string, string>());

                ArrayList createdBatches = new ArrayList();
                List<BatchDetail> temp_workset_BatchDetail = new List<BatchDetail>();
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    if (int.Parse(batchDetail._ID) < 0)
                    {
                        // New Batch
                        string xml = TestController.SerializeXML(batchDetail, this._enableValidation);
                        // send to SECURE
                        string responseText = await this.PerformHTTP_Async(
                                HTTP.POST,
                                new string[] { "Batch", "Create" },
                                null,
                                new StringContent(
                                    TestController.SerializeXML(batchDetail, this._enableValidation),
                                    Encoding.UTF8,
                                    SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                                true);
                        // Saved batch response
                        BatchDetail createdBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                        createdBatches.Add(createdBatchDetail);
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
                // Saved new batches to SECURE, need to update ID's in document detail arrays
                if (tempNewBatchNumbers.Count > 0)
                {
                    foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
                    {
                        if (int.Parse(documentDetail._BatchID) < 0)
                        {
                            documentDetail._BatchID = (string)tempNewBatchNumbers[documentDetail._BatchID];
                            documentDetail._ID = (string)tempNewDocumentNumbers[documentDetail._ID];

                            foreach (BatchDetail batchDetail in createdBatches)
                            {
                                if (batchDetail._ID == documentDetail._BatchID)
                                {
                                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                                    {
                                        if (documentInfo._ID == documentDetail._ID)
                                        {
                                            documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo = documentInfo.RecordableFileContentInfo;

                                            documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo = documentInfo.PCORFileContentInfo;

                                            documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo = documentInfo.OtherFileContentInfo;

                                        }
                                    }
                                }
                            }
                            documentDetail.StatusCode = DocumentMediaStatusCode.New;
                        }
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
        /// <summary>
        /// Event method for Batch/Upload button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAPI_BatchUpload_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("SECURE API/Batch/Upload");
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
                            if (batchDetail.StatusCode == BatchMediaStatusCode.Submitter_Uploading)
                            {
                                documentInfo.StatusCode = DocumentMediaStatusCode.Uploaded;
                            }
                            else if (batchDetail.StatusCode == BatchMediaStatusCode.Submitter_Uploading_Corrected)
                            {
                                documentInfo.StatusCode = DocumentMediaStatusCode.Corrected;
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
                            foreach (DocumentInfo documentInfo in batchDetail.Documents)
                            {
                                if (int.Parse(documentInfo._ID) < 0)
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

                    }
                    else
                    {
                        temp_workset_BatchDetail.Add(batchDetail);
                    }
                }

                #region Update DocumentDetail ID numbers
                // Saved new batches to SECURE, need to update ID's in document detail array
                if (tempNewDocumentNumbers.Count > 0)
                {
                    foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
                    {
                        if (int.Parse(documentDetail._ID) < 0)
                        {
                            documentDetail._ID = (string)tempNewDocumentNumbers[documentDetail._ID];
                            foreach (BatchDetail batchDetail in temp_workset_BatchDetail)
                            {
                                if (batchDetail._ID == documentDetail._BatchID)
                                {
                                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                                    {
                                        if (documentInfo._ID == documentDetail._ID)
                                        {
                                            // Update to newer FileContentInfo
                                            documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo = documentInfo.RecordableFileContentInfo;
                                            documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo = documentInfo.PCORFileContentInfo;
                                            documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo = documentInfo.OtherFileContentInfo;
                                            if (documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo != null)
                                            {
                                                documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                                            }
                                            if (documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo != null)
                                            {
                                                documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                                            }
                                            if (documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo != null)
                                            {
                                                documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                                            }
                                            documentDetail.StatusCode = documentInfo.StatusCode;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

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
        /// Event method for Batch/UploadComplete button
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
                    if (batchDetail.StatusCode == BatchMediaStatusCode.Submitter_Uploading || batchDetail.StatusCode == BatchMediaStatusCode.Submitter_Uploading_Corrected)
                    {
                        count_Batches++;
                        // Get new status code for Batch
                        switch (batchDetail.StatusCode)
                        {
                            case BatchMediaStatusCode.Submitter_Uploading:
                                {
                                    batchDetail.StatusCode = BatchMediaStatusCode.Uploaded_By_Submitter;
                                    break;
                                }
                            case BatchMediaStatusCode.Submitter_Uploading_Corrected:
                                {
                                    batchDetail.StatusCode = BatchMediaStatusCode.Corrected_Uploaded_By_Submitter;
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
        /// Event method for Document/Upload button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAPI_DocumentUpload_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("SECURE API/Document/Upload");
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
                    BatchDetail relatedBatchDetail = this.GetLocalBatchDetail(relatedDocumentInfo);
                    switch (relatedBatchDetail.StatusCode)
                    {
                        case BatchMediaStatusCode.New:
                            {
                                documentDetail.StatusCode = DocumentMediaStatusCode.New;
                                break;
                            }
                        case BatchMediaStatusCode.Submitter_Uploading:
                            {
                                documentDetail.StatusCode = DocumentMediaStatusCode.Uploaded;
                                break;
                            }
                        case BatchMediaStatusCode.Submitter_Uploading_Corrected:
                            {
                                documentDetail.StatusCode = DocumentMediaStatusCode.Corrected;
                                break;
                            }
                    }
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
        /// Event method for Document/Download button
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
        /// <summary>
        /// Event method for Batch/DownloadComplete button
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
                    if (batchDetail.StatusCode == BatchMediaStatusCode.Completed_Uploaded_By_County || batchDetail.StatusCode == BatchMediaStatusCode.Correction_Uploaded_By_County)
                    {
                        BatchMediaStatusCode completeStatus = batchDetail.StatusCode == BatchMediaStatusCode.Completed_Uploaded_By_County ? BatchMediaStatusCode.Completed_Downloaded_By_Submitter : BatchMediaStatusCode.Correction_Downloaded_By_Submitter;
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
        /// <summary>
        /// Event method for Batch/Download button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAPI_BatchDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // Download BatchDetails
                //=============================================================================================================
                string batchStatus = string.Empty;
                // Get new status code for Batch
                this.LoadingBarHide();
                InputForm inputForm = new InputForm(new DictionaryEntry[] { new DictionaryEntry("BatchStatus", BatchMediaStatusCode.Correction_Uploaded_By_County) });
                if (inputForm.ShowDialog(this) == DialogResult.OK)
                {
                    batchStatus = inputForm.Parameters[0].Value.ToString();
                }
                else
                {
                    return;
                }
                this._workSet_BatchDetail.Clear();
                this._workSet_DocumentDetail.Clear();
                DictionaryEntry[] apiParameters = inputForm.Parameters;
                this.LoadingBarShow(string.Format("/api/Batch/Download?{0}", this._testController.CreateParameterString(apiParameters)));

                string responseText_Download = await this.PerformHTTP_Async(
                    HTTP.GET,
                    new string[] 
                    { 
                        "Batch", 
                        "Download" 
                    },
                    apiParameters,
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

        private async void btnAPI_BatchWithDocuments_ReadyByID_Click(object sender, EventArgs e)
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

        private async void btnAPI_BatchWithDocuments_Upload_Click(object sender, EventArgs e)
        {
            try
            {
                List<BatchDetail> tempBatchDetail_Workset = new List<BatchDetail>();
                List<DocumentDetail> tempDocumentDetail_Workset = new List<DocumentDetail>();
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    if (int.Parse(batchDetail._ID) < 0)
                    {
                        this.LoadingBarShow("/api/BatchWithDocuments/Upload");
                        BatchWithDocumentsDetail batchWithDocs = new BatchWithDocumentsDetail();
                        batchWithDocs.Batch = batchDetail;
                        batchWithDocs.Documents = this.GetLocalDocumentDetails(batchDetail._ID);
                        // send to SECURE
                        string responseText = await this.PerformHTTP_Async(
                                HTTP.POST,
                                new string[] { "BatchWithDocuments", "Upload" },
                                null,
                                new StringContent(
                                    TestController.SerializeXML(batchWithDocs, this._enableValidation),
                                    Encoding.UTF8,
                                    SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                                true);
                        if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                         this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                        {
                            // Saved batch response
                            BatchWithDocumentsDetail createdBatchWithDocuments = TestController.DeserializeXML<BatchWithDocumentsDetail>(responseText, this._enableValidation);

                            tempBatchDetail_Workset.Add(createdBatchWithDocuments.Batch);
                            tempDocumentDetail_Workset.AddRange(createdBatchWithDocuments.Documents);
                        }
                        else
                        {
                            this.DisplayHTTPError(responseText);
                        }
                    }
                    else
                    {
                        tempBatchDetail_Workset.Add(batchDetail);
                        tempDocumentDetail_Workset.AddRange(this.GetLocalDocumentDetails(batchDetail._ID));
                    }
                }
                this._workSet_BatchDetail = tempBatchDetail_Workset;
                this._workSet_DocumentDetail = tempDocumentDetail_Workset;
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                this._FailMessages.Add("Failed to upload batch");
            }
            finally
            {
                this.LoadingBarHide();
            }
        }

        #endregion

        #region SECURE User Operations Group

        /// <summary>
        /// Save Batch And Upload button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOP_SaveBatchUpload_Click(object sender, EventArgs e)
        {
            this.LoadingBarShow("Saving Data to SECURE");
            try
            {
                if (!(await this.SaveWorkSetsToSECURE(true)))
                {
                    throw new Exception();
                }
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
                this.LoadingBarHide();
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Create Batch button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOP_CreateBatch_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Creating New Batch");
                await Task.Run(() =>
                {
                    // Create local batch
                    BatchDetail newBatchDetail = this.CreateNewBatchLocal();
                    this._workSet_BatchDetail.Add(newBatchDetail);
                    this._workSet_DocumentDetail.Add(
                        TestController.CreateDocumentDetail(
                            newBatchDetail.Documents[0],
                            newBatchDetail._ID));
                });
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
                this.LoadingBarHide();
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Clear button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOP_Clear_Click(object sender, EventArgs e)
        {
            // Clear saved BatchDetail and DocumentDetail. Reset Treeview
            this._workSet_DocumentDetail.Clear();
            this._workSet_BatchDetail.Clear();
            this.trvBatches.Nodes.Clear();
            this._lastSelectedNode = null;
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

        }
        /// <summary>
        /// Save Batch button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOP_SaveBatch_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Updating Batches");
                if (!(await this.SaveWorkSetsToSECURE(false)))
                {
                    throw new Exception();
                }
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
        /// Submit button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOP_SubmitBatch_Click(object sender, EventArgs e)
        {
            // Validation
            this.LoadingBarShow("Validating Batches");
            if (!(await this.ValidateBatches(this._workSet_BatchDetail, false)))
            {
                return;
            }
            // Begin Submission
            this.LoadingBarShow("Submitting Batches");
            this._FailMessages.Clear();
            // Simultaneous Create/Submit operation
            List<BatchWithDocumentsDetail> newCreateSubmit = await this.OP_SubmitCreate_BatchWithDocuments();
            // Update or Create batches, upload documents to secure
            if (await this.SaveWorkSetsToSECURE(true))
            {
                await Task.Run(() =>
                {
                    // Clear DocumentDetails
                    this._workSet_DocumentDetail.Clear();
                    // Set all documents to Uploaded
                    foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                    {
                        if (batchDetail.StatusCode == BatchMediaStatusCode.New)
                        {
                            foreach (DocumentInfo documentInfo in batchDetail.Documents)
                            {
                                if (documentInfo.StatusCode == DocumentMediaStatusCode.New)
                                {
                                    documentInfo.ActionCode = ActionCode.Edit;
                                    documentInfo.StatusCode = DocumentMediaStatusCode.Uploaded;
                                }
                            }
                        }
                    }
                });
                // Update in SECURE
                if (await this.SaveWorkSetsToSECURE(false))
                {
                    // Update in SECURE with status Uploading
                    if (await this.UpdateBatchDetail_Uploading())
                    {
                        // Send Batch XML on Batch/UploadComplete
                        await this.UploadBatchComplete();
                    }
                }
            }
            foreach (BatchWithDocumentsDetail batchWithDocumentDetail in newCreateSubmit)
            {
                this._workSet_BatchDetail.Add(batchWithDocumentDetail.Batch);
                this._workSet_DocumentDetail.AddRange(batchWithDocumentDetail.Documents);
            }
            // Done, set up form
            this.LoadTree(this._workSet_BatchDetail);
            this.batchDetailControl.Visible = false;
            this.documentInfoControl.Visible = false;
            this.fileContentInfoControl_RECORDABLE.Visible = false;
            this.fileContentInfoControl_PCOR.Visible = false;
            this.fileContentInfoControl_OTHER.Visible = false;
            this.LoadingBarHide();
            if (this._FailMessages.Count == 0)
            {
                MessageBox.Show(this, "Submission Complete", "SECURE Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string failMessage = string.Empty;
                foreach (string fmsg in this._FailMessages)
                {
                    if (failMessage == string.Empty)
                    {
                        failMessage = fmsg;
                    }
                    else
                    {
                        failMessage += ("\n\r" + fmsg);
                    }
                }
                MessageBox.Show(this, "Submission Failed:" + "\n\n" + failMessage, "SECURE Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnOP_MyBatches_Click(object sender, EventArgs e)
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
                    new string[] { "Batch", "ReadByUserName" },
                    new DictionaryEntry[] 
                    { 
                        
                        new DictionaryEntry("UserName", TestController.Default_UserName_Submitter) 
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

        private async void btnOP_ValidateBatches_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                this.LoadingBarShow("Validating Batches");
                await this.ValidateBatches(this._workSet_BatchDetail, true);
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

        #region Cache Group

        /// <summary>
        /// Reload cache button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnCache_LoadCache_Click(object sender, EventArgs e)
        {
            await this.LoadCache();

        }
        /// <summary>
        /// View cache button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCache_ViewCache_Click(object sender, EventArgs e)
        {
            CacheViewerForm cvForm = new CacheViewerForm(
                this._cache_citiesDetail,
                this._cache_countiesInfo,
                this._cache_IndexOptionsDetail,
                this._cache_processQueuesDetail,
                this._cache_requestingPartiesInfo,
                this._cache_titlesDetail,
                this._cache_SubmittingParties);
            cvForm.ShowDialog(this);
        }

        #endregion

        #region Search Group

        /// <summary>
        /// Search button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOP_ViewBatch_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadingBarShow("Retrieving Batches");

                this._workSet_BatchDetail.Clear();
                this._workSet_DocumentDetail.Clear();
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

        #endregion

        #region Batch Download group

        /// <summary>
        /// Download Batched button clicked
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

                // Download BatchDetails
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
                                //new DictionaryEntry("BypassChechedOut", bool.TrueString)
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
                if (this.chkBatchDownload_CheckOut.Checked)
                {
                    // Check Out batches
                    //=================================================================================================================
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
                // Download all documents
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

                if (this.chkBatchDownload_SetComplete.Checked)
                {
                    // Batch/DownloadComplete
                    //=================================================================================================================
                    this.LoadingBarShow("Setting Batch Download Complete");
                    List<BatchDetail> _workset_BatchDetail_DownloadComplete = new List<BatchDetail>();
                    int downloadCompletecounter = 0;
                    int totalDownloadComplete = this._workSet_BatchDetail.Count;
                    foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                    {
                        if ((batchDetail.StatusCode == BatchMediaStatusCode.Completed_Uploaded_By_County || batchDetail.StatusCode == BatchMediaStatusCode.Correction_Uploaded_By_County) &&
                            batchDetail.CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_Submitter.ToUpper())
                        {
                            this.LoadingBarShow(string.Format("Setting Batch Download Complete [{0}/{1}]", ++downloadCompletecounter, totalDownloadComplete));
                            BatchMediaStatusCode completeStatus = batchDetail.StatusCode == BatchMediaStatusCode.Completed_Uploaded_By_County ? BatchMediaStatusCode.Completed_Downloaded_By_Submitter : BatchMediaStatusCode.Correction_Downloaded_By_Submitter;
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
                    TestController.SECURE_ADDRESS_SUBMITTER = URL;
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
                        this.btnAuth_Login.Enabled = !this.txtURL.Enabled && this._testController.Session == null;
                        this.btnAuth_Logout.Enabled =
                        this.btnAuth_Ping.Enabled =
                        this.btnAuth_ChangePassword.Enabled = !this.btnAuth_Login.Enabled && !this.txtURL.Enabled;
                    }
                }
                if (this.txtURL.Enabled) { this.txtURL.Focus(); }
            }
            else
            {
                MessageBox.Show(this, "URL is required.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region Read from local data cache

        /// <summary>
        /// Get an IndexOption for a county
        /// </summary>
        /// <param name="countyID">string, containing the Count's ID</param>
        /// <returns>IndexOptionDetail for the specified county</returns>
        private IndexOptionDetail GetLocalIndexOption(string countyID)
        {
            if (this._cache_IndexOptionsDetail != null)
            {
                foreach (IndexOptionDetail indexOptionDetail in this._cache_IndexOptionsDetail)
                {
                    if (indexOptionDetail != null)
                    {
                        if (indexOptionDetail._CountyID == countyID)
                        {
                            return indexOptionDetail;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Get Titles for a county
        /// </summary>
        /// <param name="countyID">string, containing the Count's ID</param>
        /// <returns>TitleDetail[], containing titles for the specified county</returns>
        private TitleDetail[] GetLocalTitleDetail(string countyID)
        {
            ArrayList countyTitles = new ArrayList();
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
            return (TitleDetail[])countyTitles.ToArray(typeof(TitleDetail));
        }
        /// <summary>
        /// Get cities for a county
        /// </summary>
        /// <param name="countyID">string, containing the Count's ID</param>
        /// <returns>CityDetail[], containing cities for the specified county</returns>
        private CityDetail[] GetLocalCityDetail(string countyID)
        {
            ArrayList countyCities = new ArrayList();
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
            return (CityDetail[])countyCities.ToArray(typeof(CityDetail));
        }
        /// <summary>
        /// Get County info for a county
        /// </summary>
        /// <param name="countyName">string, containing the name of the county</param>
        /// <returns>CountyInfo for the specified county</returns>
        private CountyInfo GetCountyInfo(string countyName)
        {
            if (this._cache_countiesInfo != null && this._cache_countiesInfo.County != null)
            {
                foreach (CountyInfo countyInfo in this._cache_countiesInfo.County)
                {
                    if (countyInfo.Name == countyName)
                    {
                        return countyInfo;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Get DocumentInfo
        /// </summary>
        /// <param name="sequence">int, containing the sequence of the specified document</param>
        /// <param name="batchDetail">BatchDetail, representing the batch containing the document</param>
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
        private DocumentDetail[] GetLocalDocumentDetails(string batchID)
        {
            ArrayList foundDocumentDetails = new ArrayList();
            foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
            {
                if (documentDetail._BatchID == batchID)
                {
                    if (documentDetail.FileContentDetails.PCORFileContentDetail != null &&
                        documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo == null)
                    {
                        documentDetail.FileContentDetails.PCORFileContentDetail = null;
                    }
                    if (documentDetail.FileContentDetails.OtherFileContentDetail != null &&
                        documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo == null)
                    {
                        documentDetail.FileContentDetails.OtherFileContentDetail = null;
                    }
                    foundDocumentDetails.Add(documentDetail);
                }
            }
            return foundDocumentDetails.ToArray(typeof(DocumentDetail)) as DocumentDetail[];
        }
        private BatchDetail GetLocalBatchDetail(DocumentInfo documentInfo)
        {
            foreach (BatchDetail batchdetail in this._workSet_BatchDetail)
            {
                foreach (DocumentInfo batchDocument in batchdetail.Documents)
                {
                    if (batchDocument == documentInfo)
                    {
                        return batchdetail;
                    }
                }
            }
            return null;
        }

        #endregion

        #region Loading Bar

        /// <summary>
        /// LoadingForm, shown while performing lengthy operations
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

        #region Log In

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
                        new DictionaryEntry("UserName", TestController.Default_UserName_Submitter),
                        new DictionaryEntry("Password", TestController.Default_Submitter_Password),
                        new DictionaryEntry("TwoFactorPassword", string.Empty)
                    }
                    :
                    new DictionaryEntry[] 
                    { 
                        new DictionaryEntry("UserName", TestController.Default_UserName_Submitter),
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
                TestController.Default_UserName_Submitter = userName;
                TestController.Default_Submitter_Password = password;
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
                        = this.txtCurrentUser_SubmittingParty.Text
                        = this.txtCurrentUser_PartyType.Text
                        = string.Empty;
                }
                else
                {
                    this.txtCurrentUser_UserName.Text = TestController.Default_UserName_Submitter;
                    this.txtCurrentUser_Name.Text = string.Format("{0} {1}", this._testController.Session.FirstName, this._testController.Session.LastName);
                    this.txtCurrentUser_UserType.Text = this._testController.Session.UserType;
                    this.txtCurrentUser_SubmittingParty.Text = this._cache_SubmittingParties.SubmittingParty[0].Name;
                    this.txtCurrentUser_PartyType.Text = this._cache_SubmittingParties.SubmittingParty[0].PartyTypeSpecified ?
                        this._cache_SubmittingParties.SubmittingParty[0].PartyType.ToString() :
                        string.Empty;
                }
            }
            catch (Exception)
            {
                this.txtCurrentUser_UserName.Text
                        = this.txtCurrentUser_Name.Text
                        = this.txtCurrentUser_UserType.Text
                        = this.txtCurrentUser_SubmittingParty.Text
                        = this.txtCurrentUser_PartyType.Text
                        = string.Empty;
            }
        }
        #endregion

        #region TreeView

        ///// <summary>
        ///// Load the TreeView with BatchDetails
        ///// </summary>
        ///// <param name="batchDetailArrayList">ArrayList, containing BatchDetails</param>
        //private void LoadTree(ArrayList batchDetailArrayList)
        //{
        //    this.LoadTree((BatchDetail[])batchDetailArrayList.ToArray(typeof(BatchDetail)));
        //}
        private void LoadTree(List<BatchDetail> batchDetails)
        {
            this.LoadTree(batchDetails.ToArray());
        }
        /// <summary>
        /// Load the TreeView with BatchDetails
        /// </summary>
        /// <param name="batchDetails">BatchDetail[], containing BatchDetails</param>
        private void LoadTree(BatchDetail[] batchDetails)
        {
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
                    TreeNode newDocNode = new TreeNode(string.Format("Document [{0}]", documentInfo._ID));
                    newDocNode.Tag = documentInfo;
                    switch (documentInfo.ActionCode)
                    {
                        case ActionCode.New:
                            {
                                newDocNode.SelectedImageIndex =
                                newDocNode.ImageIndex = 2;
                                break;
                            }
                        case ActionCode.Edit:
                            {
                                newDocNode.SelectedImageIndex =
                                newDocNode.ImageIndex = 4;
                                break;
                            }
                        case ActionCode.Delete:
                            {
                                newDocNode.SelectedImageIndex =
                                newDocNode.ImageIndex = 3;
                                break;
                            }
                        case ActionCode.None:
                            {
                                newDocNode.SelectedImageIndex =
                                newDocNode.ImageIndex = 1;
                                break;
                            }
                    }
                    newBatchNode.Nodes.Add(newDocNode);
                }
                trvBatches.Nodes.Add(newBatchNode);
            }
            this.trvBatches.ExpandAll();
        }
        /// <summary>
        /// Event method for when a mouseclick is performed on the TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvBatches_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != this._lastSelectedNode)
            {
                this.Enabled = false;
                this._lastSelectedNode = e.Node;

                Type selectedType = null;
                if (e.Node != null)
                {
                    selectedType = e.Node.Tag.GetType();
                }

                if (selectedType == typeof(BatchDetail))
                {
                    BatchDetail batchDetail = (BatchDetail)e.Node.Tag;
                    this.LoadFlowPanel(batchDetail, null, null);
                    this.ConfigureContextMenu_Enable(batchDetail);
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
                    this.ConfigureContextMenu_Enable(documentInfo);
                }
                this.ConfigureContextMenu_Visible(selectedType);
                this.Enabled = true;
            }
        }

        #endregion

        #region FlowLayoutPanel

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
            IndexOptionDetail indexOptionDetail = this.GetLocalIndexOption(batchDetail.County._ID);
            if (this.batchDetailControl.BatchDetail != batchDetail)
            {
                this.batchDetailControl.BatchDetail = null;
                // Load cache data into control
                this.batchDetailControl.CountyInfo = this._cache_countiesInfo.County;
                this.batchDetailControl.ProcessQueueDetail = this._cache_processQueuesDetail.ProcessQueue;
                this.batchDetailControl.RequestingPartyInfo = this._cache_requestingPartiesInfo.RequestingParty;
                // Set enabled index options
                this.batchDetailControl.EnableConcurrentIndex = indexOptionDetail.EnableConcurrentIndex;
                this.batchDetailControl.EnableRequestingParty = indexOptionDetail.EnableRequestingParty;
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
                    (new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode);
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
                    (new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode);
            }
            else
            {
                this.batchDetailControl.EnableEdit = false;
                this.documentInfoControl.Visible = true;
                this.documentInfoControl.AllTitles = this.GetLocalTitleDetail(batchDetail.County._ID).ToList<TitleDetail>();
                this.documentInfoControl.AllCities = this.GetLocalCityDetail(batchDetail.County._ID).ToList<CityDetail>();
                this.documentInfoControl.EnableExternalID = true;
                if (this.documentInfoControl.DocumentInfo != documentInfo)
                {
                    this.documentInfoControl.DocumentInfo = documentInfo;
                    // Set enabled index options
                    this.documentInfoControl.EnableAssessorParcelNumber = indexOptionDetail.EnableAPNIndex;
                    this.documentInfoControl.EnableCities = indexOptionDetail.EnableCityIndex;
                    this.documentInfoControl.EnableNames = indexOptionDetail.EnableNamesIndex;
                    this.documentInfoControl.EnableSaleAmount = indexOptionDetail.EnableAmountSaleIndex;
                    this.documentInfoControl.EnableTitles = indexOptionDetail.EnableTitleIndex;
                    this.documentInfoControl.EnableTransferTaxAmount = indexOptionDetail.EnableTransferTaxIndex;
                    // Only enabled in county client
                    this.documentInfoControl.EnableDocumentNumber = false;
                    //
                    this.documentInfoControl.EnableSequence = ((new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                    this.documentInfoControl.EnableMemo = ((new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                }
                else
                {
                    this.documentInfoControl.Refresh();
                }
                // Editing
                this.documentInfoControl.EnableEdit =
                    ((new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode)) &&
                    !((new DocumentMediaStatusCode[] { DocumentMediaStatusCode.Rejected, DocumentMediaStatusCode.Purged, DocumentMediaStatusCode.Uploaded }).Contains<DocumentMediaStatusCode>(this.documentInfoControl.DocumentInfo.StatusCode));
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
                    (new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter, BatchMediaStatusCode.Submitter_Uploading, BatchMediaStatusCode.Submitter_Uploading_Corrected }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                this.fileContentInfoControl_RECORDABLE.EnableFileReplace(
                    (new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter, BatchMediaStatusCode.Submitter_Uploading, BatchMediaStatusCode.Submitter_Uploading_Corrected }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
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
                        (new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter, BatchMediaStatusCode.Submitter_Uploading, BatchMediaStatusCode.Submitter_Uploading_Corrected }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                    this.fileContentInfoControl_PCOR.EnableFileReplace(
                             (new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter, BatchMediaStatusCode.Submitter_Uploading, BatchMediaStatusCode.Submitter_Uploading_Corrected }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
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
                        (new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter, BatchMediaStatusCode.Submitter_Uploading, BatchMediaStatusCode.Submitter_Uploading_Corrected }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                    this.fileContentInfoControl_OTHER.EnableFileReplace(
                             (new BatchMediaStatusCode[] { BatchMediaStatusCode.New, BatchMediaStatusCode.Correction_Downloaded_By_Submitter, BatchMediaStatusCode.Submitter_Uploading, BatchMediaStatusCode.Submitter_Uploading_Corrected }).Contains<BatchMediaStatusCode>(this.batchDetailControl.BatchDetail.StatusCode));
                }
                else
                {
                    this.fileContentInfoControl_OTHER.Visible = false;
                }
            }
        }

        #endregion

        #region Context Menu Configuration

        /// <summary>
        /// Set context menu items enabled
        /// </summary>
        /// <param name="batchDetail">BatchDetail, currently selected BatchDetail</param>
        private void ConfigureContextMenu_Enable(BatchDetail batchDetail)
        {
            if (batchDetail == null)
            {
                this.addDocumentToolStripMenuItem.Enabled =
                    this.downloadDocumentsToolStripMenuItem.Enabled =
                    this.checkOutBatchToolStripMenuItem.Enabled =
                    this.checkInBatchToolStripMenuItem.Enabled =
                    this.overrideCheckOutToolStripMenuItem.Enabled =
                    true;
                this.deleteBatchToolStripMenuItem.Enabled = batchDetail.StatusCode == BatchMediaStatusCode.New;
            }
            else
            {
                this.addDocumentToolStripMenuItem.Enabled = batchDetail.StatusCode == BatchMediaStatusCode.New;
                this.downloadDocumentsToolStripMenuItem.Enabled = batchDetail.Documents != null && batchDetail.Documents.Length > 0; //!this.BatchDocumentsExistsLocal(batchDetail);
                this.checkOutBatchToolStripMenuItem.Enabled = batchDetail.CheckedOutBy == null;
                this.checkInBatchToolStripMenuItem.Enabled = batchDetail.CheckedOutBy != null;
                this.overrideCheckOutToolStripMenuItem.Enabled = batchDetail.CheckedOutBy != null;
            }
        }
        /// <summary>
        /// Set content menu items enabled
        /// </summary>
        /// <param name="documentInfo">DocumentInfo, currently selected documentInfo</param>
        private void ConfigureContextMenu_Enable(DocumentInfo documentInfo)
        {
            if (documentInfo == null)
            {
                this.addRecordableFileToolStripMenuItem.Enabled =
                    this.addPCORToolStripMenuItem.Enabled =
                    this.addOtherFileToolStripMenuItem.Enabled =
                    this.downloadDocumentToolStripMenuItem.Enabled =
                    this.removeDocumentToolStripMenuItem.Enabled =
                    this.deleteBatchToolStripMenuItem.Enabled =
                    false;
            }
            else
            {
                this.addRecordableFileToolStripMenuItem.Enabled = documentInfo.RecordableFileContentInfo == null;
                this.addPCORToolStripMenuItem.Enabled = true;
                this.addPCORToolStripMenuItem.Text = string.Format("{0} PCOR File", documentInfo.PCORFileContentInfo == null ? "Add" : "Remove");
                this.addOtherFileToolStripMenuItem.Enabled = true;
                this.addOtherFileToolStripMenuItem.Text = string.Format("{0} Other File", documentInfo.OtherFileContentInfo == null ? "Add" : "Remove");
                this.downloadDocumentToolStripMenuItem.Enabled = true;//!this.DocumentExistsLocal(documentInfo._ID);
                this.removeDocumentToolStripMenuItem.Enabled = this.GetLocalBatchDetail(documentInfo).StatusCode == BatchMediaStatusCode.New;// || documentInfo.StatusCode == DocumentMediaStatusCode.Correction;
            }
        }
        /// <summary>
        /// Configure visibility of Content Menu items
        /// </summary>
        /// <param name="selectedType">Type of selected object</param>
        private void ConfigureContextMenu_Visible(Type selectedType)
        {
            if (selectedType == null)
            {
                foreach (ToolStripMenuItem mnuItem in this.cmsTree.Items)
                {
                    mnuItem.Visible = false;
                }
            }
            else
            {
                this.addDocumentToolStripMenuItem.Visible =
                    this.downloadDocumentsToolStripMenuItem.Visible =
                    this.checkOutBatchToolStripMenuItem.Visible =
                    this.checkInBatchToolStripMenuItem.Visible =
                    this.overrideCheckOutToolStripMenuItem.Visible =
                    this.deleteBatchToolStripMenuItem.Visible =
                    (selectedType == typeof(BatchDetail));
                this.addRecordableFileToolStripMenuItem.Visible =
                    this.addPCORToolStripMenuItem.Visible =
                    this.addOtherFileToolStripMenuItem.Visible =
                    this.downloadDocumentToolStripMenuItem.Visible =
                    this.removeDocumentToolStripMenuItem.Visible =
                    (selectedType == typeof(DocumentInfo));
            }
        }
        #endregion

        #region Context Menu event methods

        /// <summary>
        /// Add Document to selected batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedBatchNode = this.trvBatches.SelectedNode;
            if (selectedBatchNode != null && selectedBatchNode.Tag.GetType() == typeof(BatchDetail))
            {
                this.AddNewDocumentToBatch((BatchDetail)selectedBatchNode.Tag);
            }
        }
        /// <summary>
        /// Add Recordable file to selected Document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void addRecordableFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.trvBatches.SelectedNode;
            if (selectedNode != null && selectedNode.Tag.GetType() == typeof(DocumentInfo))
            {
                BatchDetail batchDetail = (BatchDetail)selectedNode.Parent.Tag;
                DocumentInfo documentInfo = (DocumentInfo)selectedNode.Tag;
                if (documentInfo.RecordableFileContentInfo == null)
                {
                    await this.AddFile(documentInfo, DocumentFileType.Recordable, batchDetail._ID);
                }
                else
                {
                    await this.RemoveFile(documentInfo, DocumentFileType.Recordable, batchDetail._ID);
                }
                this.LoadTree(this._workSet_BatchDetail);
                this.LoadFlowPanel(batchDetail, documentInfo, this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID));
            }
        }
        /// <summary>
        /// Add PCOR file to selected Document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void addPCORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.trvBatches.SelectedNode;
            if (selectedNode != null && selectedNode.Tag.GetType() == typeof(DocumentInfo))
            {
                BatchDetail batchDetail = (BatchDetail)selectedNode.Parent.Tag;//this.GetLocalBatchDetail(selectedNode.Parent.Text);
                DocumentInfo documentInfo = (DocumentInfo)selectedNode.Tag; //this.GetLocalDocumentInfo(int.Parse(selectedNode.Text), batchDetail);
                if (documentInfo.PCORFileContentInfo == null)
                {
                    await this.AddFile(documentInfo, DocumentFileType.PCOR, batchDetail._ID);
                }
                else
                {
                    await this.RemoveFile(documentInfo, DocumentFileType.PCOR, batchDetail._ID);
                }
                this.LoadTree(this._workSet_BatchDetail);
                this.LoadFlowPanel(batchDetail, documentInfo, this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID));
            }
        }
        /// <summary>
        /// Add Other file to selected document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void addOtherFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.trvBatches.SelectedNode;
            if (selectedNode != null && selectedNode.Tag.GetType() == typeof(DocumentInfo))
            {
                BatchDetail batchDetail = (BatchDetail)selectedNode.Parent.Tag;//this.GetLocalBatchDetail(selectedNode.Parent.Text);
                DocumentInfo documentInfo = (DocumentInfo)selectedNode.Tag; //this.GetLocalDocumentInfo(int.Parse(selectedNode.Text), batchDetail);
                if (documentInfo.OtherFileContentInfo == null)
                {
                    await this.AddFile(documentInfo, DocumentFileType.Other, batchDetail._ID);
                }
                else
                {
                    await this.RemoveFile(documentInfo, DocumentFileType.Other, batchDetail._ID);
                }
                this.LoadTree(this._workSet_BatchDetail);
                this.LoadFlowPanel(batchDetail, documentInfo, this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID));
            }
        }
        /// <summary>
        /// Download all document images for selected batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void downloadDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
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
        /// <summary>
        /// Download document image for selected document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void downloadDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode selectedNode = this.trvBatches.SelectedNode;
                if (selectedNode != null && selectedNode.Tag.GetType() == typeof(DocumentInfo))
                {
                    BatchDetail batchDetail = (BatchDetail)selectedNode.Parent.Tag;//this.GetLocalBatchDetail(selectedNode.Parent.Text);
                    DocumentInfo documentInfo = (DocumentInfo)selectedNode.Tag;//this.GetLocalDocumentInfo(int.Parse(selectedNode.Text), batchDetail);
                    if (documentInfo != null && documentInfo.ActionCode != ActionCode.New)
                    {
                        this.LoadingBarShow(string.Format("Downloading Images for Document[{0}]", documentInfo._ID));
                        DocumentDetail downloadedDocumentDetail = await this.DownloadDocument(documentInfo._ID);
                        this.LoadingBarHide();
                        if (downloadedDocumentDetail != null)
                        {
                            DocumentDetail oldDocumentDetail = this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID);
                            if (oldDocumentDetail != null)
                            {
                                this._workSet_DocumentDetail.Remove(oldDocumentDetail);
                            }
                            this._workSet_DocumentDetail.Add(downloadedDocumentDetail);
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
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Check out selected batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void checkOutBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.trvBatches.SelectedNode.Tag.GetType() == typeof(BatchDetail))
                {
                    BatchDetail batchDetail = (BatchDetail)this.trvBatches.SelectedNode.Tag;

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
        /// Check in selected batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void checkInBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.trvBatches.SelectedNode.Tag.GetType() == typeof(BatchDetail))
                {
                    this.LoadingBarShow("Checking In Batch");
                    BatchDetail batchDetail = (BatchDetail)this.trvBatches.SelectedNode.Tag;//this.GetLocalBatchDetail(this.trvBatches.SelectedNode.Text);

                    string responseText = await this.PerformHTTP_Async(
                        HTTP.PUT,
                        new string[] { "Batch", "CheckIn" },
                        new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID)
                            },
                        null,
                        true);
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
        /// <summary>
        /// Override checkout for selected batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void overrideCheckOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.trvBatches.SelectedNode.Tag.GetType() == typeof(BatchDetail))
                {
                    this.LoadingBarShow("Overriding Checked Out Batch");
                    BatchDetail batchDetail = (BatchDetail)this.trvBatches.SelectedNode.Tag;
                    string responseText = await this.PerformHTTP_Async(
                        HTTP.PUT,
                        new string[] { "Batch", "OverrideCheckOut" },
                        new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID)
                            },
                        null,
                        true);
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
        /// <summary>
        /// Remove selected document from batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.trvBatches.SelectedNode != null && this.trvBatches.SelectedNode.Tag.GetType() == typeof(DocumentInfo))
            {
                bool success = this.RemoveDocumentFromBatch(
                   (BatchDetail)this.trvBatches.SelectedNode.Parent.Tag,
                   (DocumentInfo)this.trvBatches.SelectedNode.Tag);
                if (!success)
                {
                    MessageBox.Show(this, "Cannot remove document from batch", "Error");
                }
                this.LoadTree(this._workSet_BatchDetail);
                this.batchDetailControl.Visible = false;
                this.documentInfoControl.Visible = false;
                this.fileContentInfoControl_RECORDABLE.Visible = false;
                this.fileContentInfoControl_PCOR.Visible = false;
                this.fileContentInfoControl_OTHER.Visible = false;
            }
        }
        /// <summary>
        /// Delete selected batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void deleteBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.trvBatches.SelectedNode.Tag.GetType() == typeof(BatchDetail))
                {
                    this.LoadingBarShow("Deleting Batch");
                    BatchDetail batchDetail = (BatchDetail)this.trvBatches.SelectedNode.Tag;

                    string responseText = await this.PerformHTTP_Async(
                        HTTP.DELETE,
                        new string[] { "Batch", "Delete" },
                        new DictionaryEntry[] 
                            {
                               
                                new DictionaryEntry("BatchID", batchDetail._ID)
                            },
                        null,
                        true);
                    if (TestController.DeserializeXML<ResultDetail>(responseText, this._enableValidation).Value)
                    {
                        this._workSet_BatchDetail.Remove(batchDetail);
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

        private void chkBatchDownload_IncludeCheckedOut_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkBatchDownload_CheckOut_CheckedChanged(object sender, EventArgs e)
        {
            this.chkBatchDownload_SetComplete.Enabled = this.chkBatchDownload_SetComplete.Checked = this.chkBatchDownload_CheckOut.Checked;
        }

        #endregion

        #region Helper Functions

        private bool DocumentExistsLocal(string documentID)
        {
            foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
            {
                if (documentDetail._ID == documentID)
                {
                    return true;
                }
            }
            return false;
        }

        private bool BatchDocumentsExistsLocal(BatchDetail batchDetail)
        {
            int foundCount = 0;
            foreach (DocumentInfo documentInfo in batchDetail.Documents)
            {
                bool found = this.DocumentExistsLocal(documentInfo._ID);
                if (!found)
                {
                    return false;
                }
                else
                {
                    foundCount++;
                }
            }
            return foundCount == batchDetail.Documents.Length;
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
        /// Locally Create new BatchDetail object and 1 document with default values
        /// </summary>
        /// <returns>BatchDetail object</returns>
        private BatchDetail CreateNewBatchLocal()
        {
            BatchDetail newBatchDetail = new BatchDetail();
            newBatchDetail._ID = this.GenerateTempBatchID();//Set BatchID to negative int to signify as not saved to secure
            newBatchDetail._SubmissionID = Guid.NewGuid().ToString();
            newBatchDetail.County = this._cache_countiesInfo.County[0];
            newBatchDetail.ProcessQueue = this._cache_processQueuesDetail.ProcessQueue[0];
            newBatchDetail.CreateTimeStamp = DateTime.Now;
            newBatchDetail.CreateTimeStampSpecified = true;
            newBatchDetail.EditTimeStamp = newBatchDetail.CreateTimeStamp;
            newBatchDetail.Name = string.Format("B:{0}.{1:00}.{2:00}:[{3:00}:{4:00}:{5:00}]",
                    newBatchDetail.CreateTimeStamp.Year,
                    newBatchDetail.CreateTimeStamp.Month,
                    newBatchDetail.CreateTimeStamp.Day,
                    newBatchDetail.CreateTimeStamp.Hour,
                    newBatchDetail.CreateTimeStamp.Minute,
                    newBatchDetail.CreateTimeStamp.Second);
            newBatchDetail.StatusCode = BatchMediaStatusCode.New;
            newBatchDetail.IsConcurrent = false;
            newBatchDetail.CheckedOutBy = null;
            newBatchDetail.CheckedOutTimeStampSpecified = false;
            newBatchDetail.CheckedOutTimeStamp = DateTime.MinValue;
            newBatchDetail.Memos = new MemoDetail[0];

            newBatchDetail.Documents = new DocumentInfo[1];
            newBatchDetail.Documents[0] = new DocumentInfo();
            newBatchDetail.Documents[0]._ID = this.GenerateTempDocID();//set DocumentID to negative int to signify as not saved to secure
            newBatchDetail.Documents[0].CreateTimeStamp = newBatchDetail.CreateTimeStamp;
            newBatchDetail.Documents[0].CreateTimeStampSpecified = true;
            newBatchDetail.Documents[0].EditTimeStamp = newBatchDetail.CreateTimeStamp;
            newBatchDetail.Documents[0].Version = 0;
            newBatchDetail.Documents[0].Sequence = 0;
            newBatchDetail.Documents[0].AssessorParcelNumber = string.Empty;
            newBatchDetail.Documents[0].ActionCode = ActionCode.New;
            newBatchDetail.Documents[0].RecordableFileContentInfo = new FileContentInfo()
            {
                _ID = this.GenerateTempImageID(),
                ActionCode = ActionCode.New,
                PageCount = "0"
            };
            return newBatchDetail;
        }
        /// <summary>
        /// Generate temporary ID number
        /// </summary>
        /// <returns>string, containing a randomly generated negative integer</returns>
        private string GenerateTempDocID()
        {
            ArrayList allIDs = new ArrayList();
            //get all doc IDs
            foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
            {
                foreach (DocumentInfo documentInfo in batchDetail.Documents)
                {
                    if (documentInfo != null)
                    {
                        allIDs.Add(documentInfo._ID);
                    }
                }
            }

            //generate doc ID
            string newID = (new Random(DateTime.Now.Millisecond + 1030)).Next(-9999, 0).ToString();

            //validate unique
            while (allIDs.Contains(newID))
            {
                newID = (new Random(DateTime.Now.Millisecond + 1030)).Next(-9999, 0).ToString();
            }
            return newID;
        }
        /// <summary>
        /// Generate temporary ID number
        /// </summary>
        /// <returns>string, containing a randomly generated negative integer</returns>
        private string GenerateTempBatchID()
        {
            ArrayList allIDs = new ArrayList();
            //get all doc IDs
            foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
            {
                allIDs.Add(batchDetail._ID);
            }

            //generate doc ID
            string newID = (new Random(DateTime.Now.Millisecond + 1030)).Next(-9999, 0).ToString();

            //validate unique
            while (allIDs.Contains(newID))
            {
                newID = (new Random(DateTime.Now.Millisecond + 1030)).Next(-9999, 0).ToString();
            }
            return newID;
        }
        /// <summary>
        /// Generate temporary ID number
        /// </summary>
        /// <returns>string, containing a randomly generated negative integer</returns>
        private string GenerateTempImageID()
        {
            ArrayList allIDs = new ArrayList();
            //get all doc IDs
            foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
            {
                foreach (DocumentInfo documentInfo in batchDetail.Documents)
                {
                    if (documentInfo != null)
                    {
                        if (documentInfo.RecordableFileContentInfo != null)
                        {
                            allIDs.Add(documentInfo.RecordableFileContentInfo._ID);
                        }
                        if (documentInfo.PCORFileContentInfo != null)
                        {
                            allIDs.Add(documentInfo.PCORFileContentInfo._ID);
                        }
                        if (documentInfo.OtherFileContentInfo != null)
                        {
                            allIDs.Add(documentInfo.OtherFileContentInfo._ID);
                        }
                    }
                }
            }

            //generate doc ID
            string newID = (new Random(DateTime.Now.Millisecond + 1030)).Next(-9999, 0).ToString();

            //validate unique
            while (allIDs.Contains(newID))
            {
                newID = (new Random(DateTime.Now.Millisecond + 1030)).Next(-9999, 0).ToString();
            }
            return newID;
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
        /// Save document detail to image file
        /// </summary>
        /// <param name="documentDetails">DocumentDetail array, containing Document Details with image data</param>
        private void SaveAsFile(DocumentDetail[] documentDetails)
        {
            foreach (DocumentDetail documentDetail in documentDetails)
            {
                if (documentDetail.FileContentDetails != null)
                {
                    if (documentDetail.FileContentDetails.RecordableFileContentDetail != null &&
                        documentDetail.FileContentDetails.RecordableFileContentDetail.FileContent != null &&
                        documentDetail.FileContentDetails.RecordableFileContentDetail.FileContent.Length != 0)
                    {
                        foreach (FileContent fileContent in documentDetail.FileContentDetails.RecordableFileContentDetail.FileContent)
                        {
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
                                this.SaveAsFile(fileContent, filePath);
                            }
                        }
                    }
                    if (documentDetail.FileContentDetails.PCORFileContentDetail != null &&
                       documentDetail.FileContentDetails.PCORFileContentDetail.FileContent != null &&
                       documentDetail.FileContentDetails.PCORFileContentDetail.FileContent.Length != 0)
                    {
                        foreach (FileContent fileContent in documentDetail.FileContentDetails.PCORFileContentDetail.FileContent)
                        {
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
                                this.SaveAsFile(fileContent, filePath);
                            }
                        }
                    }
                    if (documentDetail.FileContentDetails.OtherFileContentDetail != null &&
                       documentDetail.FileContentDetails.OtherFileContentDetail.FileContent != null &&
                       documentDetail.FileContentDetails.OtherFileContentDetail.FileContent.Length != 0)
                    {
                        foreach (FileContent fileContent in documentDetail.FileContentDetails.OtherFileContentDetail.FileContent)
                        {
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
                                this.SaveAsFile(fileContent, filePath);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Save the content as a file
        /// </summary>
        /// <param name="fileContent">FileContent, containing Base-64 encoded image data</param>
        /// <param name="filePath">string, containing the destination path to put the image file</param>
        private async void SaveAsFile(FileContent fileContent, string filePath)
        {
            try
            {
                this.LoadingBarShow("Decoding file");
                await TestController.DecodeBase64File(fileContent.FileBuffer, filePath);
                this.LoadingBarHide();
                MessageBox.Show(this, string.Format("Successfully saved to file [{0}]", filePath), "File Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Add a DocumentDetail and related objects for adding a new file
        /// </summary>
        /// <param name="documentInfo">DocumentInfo for document that will have a file added</param>
        /// <param name="docFileType">DocumentFileType, describing the type of image file being added</param>
        /// <param name="batchID">string, containing the BatchID of the document's batch</param>
        /// <returns>Task</returns>
        private async Task AddFile(DocumentInfo documentInfo, DocumentFileType docFileType, string batchID)
        {
            try
            {
                // select file
                //====================================
                FileChooserForm ofd = new FileChooserForm();
                if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string[] filePaths = ofd.SelectedFile;
                    FileContentDetail newFileContentDetail = new FileContentDetail();
                    FileContentInfo newFileContentInfo =
                        new FileContentInfo()
                        {
                            EmbeddedFileType = ofd.EmbeddedFileType,
                            PageCount = ofd.PageCount
                        };
                    newFileContentDetail.FileContentInfo = newFileContentInfo;
                    newFileContentDetail.FileContent = new FileContent[filePaths.Length];
                    this.LoadingBarShow(string.Format("Encoding File{0} into Base 64", filePaths.Length == 1 ? string.Empty : "s"));
                    int counter = 0;
                    foreach (string filePath in filePaths)
                    {
                        // encode file
                        //====================================
                        string encodedValue = await TestController.EncodeBase64Async(filePath);
                        // shove encoded value into FileContent
                        //====================================
                        newFileContentDetail.FileContent[counter] =
                            new FileContent()
                            {
                                Encoding = EncodingTypes.Base64,
                                _Sequence = counter++.ToString(),
                                FileBuffer = encodedValue
                            };
                    }
                    DocumentDetail documentDetail = null;
                    foreach (DocumentDetail _documentDetail in this._workSet_DocumentDetail)
                    {
                        if (_documentDetail._ID == documentInfo._ID)
                        {
                            documentDetail = _documentDetail;
                            continue;
                        }
                    }
                    if (documentDetail == null)
                    {
                        documentDetail = new DocumentDetail();
                        documentDetail._ID = documentInfo._ID;
                        documentDetail._BatchID = batchID;
                        documentDetail.StatusCode = documentInfo.StatusCode;
                        documentDetail.FileContentDetails = new FileContentDetails();
                        this._workSet_DocumentDetail.Add(documentDetail);
                    }
                    switch (docFileType)
                    {
                        case DocumentFileType.Recordable:
                            {
                                newFileContentDetail.FileContentInfo.ActionCode = (documentInfo.RecordableFileContentInfo == null) ? ActionCode.New : ActionCode.Edit;
                                newFileContentDetail.FileContentInfo._ID = (documentInfo.RecordableFileContentInfo == null) ? this.GenerateTempImageID() : documentInfo.RecordableFileContentInfo._ID;
                                documentInfo.RecordableFileContentInfo = newFileContentDetail.FileContentInfo;
                                documentDetail.FileContentDetails.RecordableFileContentDetail = newFileContentDetail;
                                documentInfo.ActionCode = documentInfo.ActionCode != ActionCode.New ? ActionCode.Edit : ActionCode.New;
                                break;
                            }
                        case DocumentFileType.PCOR:
                            {
                                newFileContentDetail.FileContentInfo.ActionCode = (documentInfo.PCORFileContentInfo == null) ? ActionCode.New : ActionCode.Edit;
                                newFileContentDetail.FileContentInfo._ID = (documentInfo.PCORFileContentInfo == null) ? this.GenerateTempImageID() : documentInfo.PCORFileContentInfo._ID;
                                documentInfo.PCORFileContentInfo = newFileContentDetail.FileContentInfo;
                                documentDetail.FileContentDetails.PCORFileContentDetail = newFileContentDetail;
                                documentInfo.ActionCode = documentInfo.ActionCode != ActionCode.New ? ActionCode.Edit : ActionCode.New;
                                break;
                            }
                        case DocumentFileType.Other:
                            {
                                newFileContentDetail.FileContentInfo.ActionCode = (documentInfo.OtherFileContentInfo == null) ? ActionCode.New : ActionCode.Edit;
                                newFileContentDetail.FileContentInfo._ID = (documentInfo.OtherFileContentInfo == null) ? this.GenerateTempImageID() : documentInfo.OtherFileContentInfo._ID;
                                documentInfo.OtherFileContentInfo = newFileContentDetail.FileContentInfo;
                                documentDetail.FileContentDetails.OtherFileContentDetail = newFileContentDetail;
                                documentInfo.ActionCode = documentInfo.ActionCode != ActionCode.New ? ActionCode.Edit : ActionCode.New;
                                break;
                            }
                    }
                    BatchDetail batchDetail = null;
                    foreach (BatchDetail batchDetail_ in this._workSet_BatchDetail)
                    {
                        if (batchDetail_._ID == batchID)
                        {
                            batchDetail = batchDetail_;
                            continue;
                        }
                    }
                    this.LoadFlowPanel(batchDetail, this.GetLocalDocumentInfo(documentInfo.Sequence, batchDetail), this.GetLocalDocumentDetail(documentInfo._ID, batchDetail._ID));
                }
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Remove file from document
        /// </summary>
        /// <param name="documentInfo">DocumentInfo for document that will have a file removed</param>
        /// <param name="docFileType">DocumentFileType, describing the type of image file being removed</param>
        /// <param name="batchID">string, containing the BatchID of the document's batch</param>
        /// <returns>Task</returns>
        private async Task RemoveFile(DocumentInfo documentInfo, DocumentFileType docFileType, string batchID)
        {
            try
            {
                this.LoadingBarShow("Removing File");
                await Task.Run(() =>
                    {
                        DocumentDetail documentDetail = this.GetLocalDocumentDetail(documentInfo._ID, batchID);
                        switch (docFileType)
                        {
                            case DocumentFileType.Recordable:
                                {
                                    if (documentInfo.RecordableFileContentInfo.ActionCode != ActionCode.New)
                                    {
                                        documentInfo.RecordableFileContentInfo.ActionCode = ActionCode.Delete;
                                    }
                                    else
                                    {
                                        documentInfo.RecordableFileContentInfo = null;
                                    }
                                    break;
                                }
                            case DocumentFileType.PCOR:
                                {
                                    if (documentInfo.PCORFileContentInfo.ActionCode != ActionCode.New)
                                    {
                                        documentInfo.PCORFileContentInfo.ActionCode = ActionCode.Delete;
                                    }
                                    else
                                    {
                                        documentInfo.PCORFileContentInfo = null;
                                    }
                                    break;
                                }
                            case DocumentFileType.Other:
                                {
                                    if (documentInfo.OtherFileContentInfo.ActionCode != ActionCode.New)
                                    {
                                        documentInfo.OtherFileContentInfo.ActionCode = ActionCode.Delete;
                                    }
                                    else
                                    {
                                        documentInfo.OtherFileContentInfo = null;
                                    }
                                    break;
                                }
                        }
                    });
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
        /// <summary>
        /// Get the next sequence number for a new document
        /// </summary>
        /// <param name="documents">DocumentInfo array, containing all other document info in the batch</param>
        /// <returns>int</returns>
        private int GetNextSequenceNumber(DocumentInfo[] documents)
        {
            int[] docSequences = new int[documents.Length];
            for (int x = 0; x < documents.Length; x++)
            {
                docSequences[x] = documents[x].Sequence;
            }

            for (int i = 0; i < docSequences.Length; i++)
            {
                for (int j = i + 1; j < docSequences.Length; j++)
                {
                    if (docSequences[j] < docSequences[i])
                    {
                        int temp = docSequences[j];
                        docSequences[j] = docSequences[i];
                        docSequences[i] = temp;
                    }
                }
            }
            int nextSequence = 0;
            foreach (int docSeq in docSequences)
            {
                if (docSeq == nextSequence)
                {
                    nextSequence += 1;
                }
            }
            return nextSequence;
        }
        /// <summary>
        /// Add a new empty document to a batch
        /// </summary>
        /// <param name="batchSubmissionID">string, containing the SubmissionID of the Batch</param>
        private void AddNewDocumentToBatch(BatchDetail batchDetail)
        {
            if (batchDetail == null)
            {
                return;
            }
            else
            {
                int nextSequenceNumber = this.GetNextSequenceNumber(batchDetail.Documents);
                DocumentInfo[] newDocumentInfos = new DocumentInfo[batchDetail.Documents.Length + 1];
                if (batchDetail.Documents.Length != 0)
                {
                    batchDetail.Documents.CopyTo(newDocumentInfos, 0);
                }
                batchDetail.Documents = newDocumentInfos;
                batchDetail.Documents[batchDetail.Documents.Length - 1] = new DocumentInfo()
                {
                    _ID = this.GenerateTempDocID(),
                    CreateTimeStamp = DateTime.Now,
                    EditTimeStamp = DateTime.Now,
                    StatusCode = DocumentMediaStatusCode.New,
                    Version = 0,
                    Sequence = nextSequenceNumber,
                    ActionCode = ActionCode.New,
                    RecordableFileContentInfo = new FileContentInfo()
                    {
                        _ID = this.GenerateTempImageID(),
                        PageCount = "0",
                        ActionCode = ActionCode.New
                    }
                };

                this._workSet_DocumentDetail.Add(
                    TestController.CreateDocumentDetail(
                        batchDetail.Documents[batchDetail.Documents.Length - 1],
                        batchDetail._ID));
            }
            this.LoadTree(this._workSet_BatchDetail);
            this.batchDetailControl.Visible = false;
            this.documentInfoControl.Visible = false;
            this.fileContentInfoControl_RECORDABLE.Visible = false;
            this.fileContentInfoControl_PCOR.Visible = false;
            this.fileContentInfoControl_OTHER.Visible = false;
        }

        /// <summary>
        /// Perform BatchWithDocuments/Upload, removes those from the arraylist
        /// </summary>
        /// <returns></returns>
        private async Task<List<BatchWithDocumentsDetail>> OP_SubmitCreate_BatchWithDocuments()
        {
            try
            {
                List<BatchWithDocumentsDetail> createdBatchDocumentSet = new List<BatchWithDocumentsDetail>();
                List<BatchDetail> tempBatchDetail_Workset = new List<BatchDetail>();
                List<DocumentDetail> tempDocumentDetail_Workset = new List<DocumentDetail>();

                int updateBatch_counter = 1;
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    this.LoadingBarShow(string.Format("Updating Batch [{0}/{1}]", updateBatch_counter++, this._workSet_BatchDetail.Count));
                    if (int.Parse(batchDetail._ID) < 0)
                    {
                        BatchWithDocumentsDetail batchWithDocs = new BatchWithDocumentsDetail();
                        batchWithDocs.Batch = batchDetail;
                        batchWithDocs.Documents = this.GetLocalDocumentDetails(batchDetail._ID);

                        // send to SECURE
                        string responseText = await this.PerformHTTP_Async(
                                HTTP.POST,
                                new string[] { "BatchWithDocuments", "Upload" },
                                null,
                                new StringContent(
                                    TestController.SerializeXML(batchWithDocs, this._enableValidation),
                                    Encoding.UTF8,
                                    SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                                true);

                        if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                         this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                        {
                            // Saved batch response
                            BatchWithDocumentsDetail createdBatchWithDocuments = TestController.DeserializeXML<BatchWithDocumentsDetail>(responseText, this._enableValidation);
                            createdBatchDocumentSet.Add(createdBatchWithDocuments);
                        }
                        else
                        {
                            this.DisplayHTTPError(responseText);
                            return null;
                        }
                    }
                    else
                    {
                        tempBatchDetail_Workset.Add(batchDetail);
                        tempDocumentDetail_Workset.AddRange(this.GetLocalDocumentDetails(batchDetail._ID));
                    }
                }
                this._workSet_BatchDetail = tempBatchDetail_Workset;
                this._workSet_DocumentDetail = tempDocumentDetail_Workset;
                return createdBatchDocumentSet;
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                this._FailMessages.Add("Failed to upload batch");
                return null; ;
            }
        }

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
                this._FailMessages.Add("Failed to download batch");
                return null;
            }
        }

        /// <summary>
        /// Write local data to SECURE API
        /// </summary>
        /// <param name="documentUpload">bool, true to upload document images</param>
        /// <returns>Task(bool), true if successful</returns>
        private async Task<bool> SaveWorkSetsToSECURE(bool documentUpload)
        {
            try
            {
                Hashtable tempNewBatchNumbers = new Hashtable(new Dictionary<string, string>());
                Hashtable tempNewDocumentNumbers = new Hashtable(new Dictionary<string, string>());

                #region Batch/Upload || Batch/Create
                List<BatchDetail> newBatches = new List<BatchDetail>();
                int updateBatch_counter = 1;
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    this.LoadingBarShow(string.Format("Updating Batch [{0}/{1}]", updateBatch_counter++, this._workSet_BatchDetail.Count));
                    if (int.Parse(batchDetail._ID) > 0)
                    {
                        if (batchDetail.StatusCode == BatchMediaStatusCode.New || batchDetail.StatusCode == BatchMediaStatusCode.Correction_Downloaded_By_Submitter)
                        {
                            if (batchDetail.CheckedOutBy != null && batchDetail.CheckedOutBy.UserName.ToUpper() == TestController.Default_UserName_Submitter.ToUpper())
                            {
                                // Update status
                                if (batchDetail.StatusCode == BatchMediaStatusCode.Correction_Downloaded_By_Submitter)
                                {
                                    batchDetail.StatusCode = BatchMediaStatusCode.Submitter_Uploading_Corrected;
                                }
                                foreach (DocumentInfo documentInfo in batchDetail.Documents)
                                {
                                    // Update action code of DocumentInfo if FileContentInfo has new ActionCode
                                    documentInfo.ActionCode = documentInfo.ActionCode == ActionCode.None ? ActionCode.Edit : documentInfo.ActionCode;
                                    // Update DocumentStatusCode
                                    documentInfo.StatusCode = (documentInfo.StatusCode == DocumentMediaStatusCode.Correction) ? DocumentMediaStatusCode.Corrected : documentInfo.StatusCode;
                                }

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
                                    newBatches.Add(resultBatchDetail);

                                    // Check if there were new documents uploaded
                                    foreach (DocumentInfo newDocumentInfo in batchDetail.Documents)
                                    {
                                        if (int.Parse(newDocumentInfo._ID) < 0 && newDocumentInfo.ActionCode == ActionCode.New)
                                        {
                                            // look for new document ID, save in hashtable
                                            foreach (DocumentInfo resultDocument in resultBatchDetail.Documents)
                                            {
                                                if (resultDocument.Sequence == newDocumentInfo.Sequence)
                                                {
                                                    tempNewDocumentNumbers.Add(newDocumentInfo._ID, resultDocument._ID);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.DisplayHTTPError(responseText);
                                    return false;
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, "Cannot edit a batch that is not checked out to you.", "EDIT FAIL", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                newBatches.Add(batchDetail);
                            }
                        }
                        else
                        {
                            newBatches.Add(batchDetail);
                        }
                    }
                    else
                    {
                        // New Batch
                        //string xml = TestController.SerializeXML(batchDetail, this._enableValidation);
                        // send to SECURE
                        string responseText = await this.PerformHTTP_Async(
                                HTTP.POST,
                                new string[] { "Batch", "Create" },
                                null,
                                new StringContent(
                                    TestController.SerializeXML(batchDetail, this._enableValidation),
                                    Encoding.UTF8,
                                    SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                                true);
                        if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                         this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                        {
                            // Saved batch response
                            BatchDetail createdBatchDetail = TestController.DeserializeXML<BatchDetail>(responseText, this._enableValidation);
                            newBatches.Add(createdBatchDetail);
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
                            this.DisplayHTTPError(responseText);
                            return false;
                        }
                    }
                }
                #endregion

                #region Update DocumentDetail ID numbers
                // Saved new batches to SECURE, need to update ID's in document detail array
                if (tempNewBatchNumbers.Count > 0 || tempNewDocumentNumbers.Count > 0)
                {
                    foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
                    {
                        if (int.Parse(documentDetail._BatchID) < 0)
                        {
                            documentDetail._BatchID = (string)tempNewBatchNumbers[documentDetail._BatchID];
                        }
                        if (int.Parse(documentDetail._ID) < 0)
                        {
                            documentDetail._ID = (string)tempNewDocumentNumbers[documentDetail._ID];
                            foreach (BatchDetail batchDetail in newBatches)
                            {
                                if (batchDetail._ID == documentDetail._BatchID)
                                {
                                    foreach (DocumentInfo documentInfo in batchDetail.Documents)
                                    {
                                        if (documentInfo._ID == documentDetail._ID)
                                        {
                                            // Update to newer FileContentInfo
                                            documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo = documentInfo.RecordableFileContentInfo;
                                            documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo = documentInfo.PCORFileContentInfo;
                                            documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo = documentInfo.OtherFileContentInfo;
                                            if (documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo != null)
                                            {
                                                documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                                            }
                                            if (documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo != null)
                                            {
                                                documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                                            }
                                            if (documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo != null)
                                            {
                                                documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo.ActionCode = ActionCode.Edit;
                                            }
                                        }
                                    }
                                }
                            }
                            documentDetail.StatusCode = DocumentMediaStatusCode.New;
                        }
                    }
                }
                #endregion

                this._workSet_BatchDetail = newBatches;
                newBatches = null;
                GC.Collect();

                #region Upload DocumentDetail
                List<DocumentDetail> uploadedDocumentDetails_Response = new List<DocumentDetail>();
                int uploadDocument_count = 1;
                int uploadDocument_total = 0;
                foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
                {
                    uploadDocument_total++;
                }
                foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
                {
                    this.LoadingBarShow(string.Format("Uploading Document [{0}/{1}]", uploadDocument_count++, uploadDocument_total));
                    bool uploadDocument = false;
                    if (documentDetail.FileContentDetails.RecordableFileContentDetail != null)
                    {
                        if (documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo != null)
                        {
                            if (documentDetail.FileContentDetails.RecordableFileContentDetail.FileContent != null &&
                                documentDetail.FileContentDetails.RecordableFileContentDetail.FileContent.Length > 0)
                            {
                                uploadDocument = true;
                            }
                        }
                    }
                    if (documentDetail.FileContentDetails.PCORFileContentDetail != null)
                    {
                        if (documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo != null)
                        {
                            if (documentDetail.FileContentDetails.PCORFileContentDetail.FileContent != null &&
                                documentDetail.FileContentDetails.PCORFileContentDetail.FileContent.Length > 0)
                            {
                                uploadDocument = true;
                            }
                        }
                    }
                    if (documentDetail.FileContentDetails.OtherFileContentDetail != null)
                    {
                        if (documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo != null)
                        {
                            if (documentDetail.FileContentDetails.OtherFileContentDetail.FileContent != null &&
                                documentDetail.FileContentDetails.OtherFileContentDetail.FileContent.Length > 0)
                            {
                                uploadDocument = true;
                            }
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
                        DocumentInfo relatedDocumentInfo = this.GetLocalDocumentInfo(documentDetail);
                        documentDetail.StatusCode = relatedDocumentInfo.StatusCode;
                        if (documentDetail.StatusCode != DocumentMediaStatusCode.Uploaded)
                        {
                            GC.Collect();
                            string responseText = await this.PerformHTTP_Async(
                                HTTP.PUT,
                                new string[] { "Document", "Upload" },
                                null,
                                new StringContent(
                                        TestController.SerializeXML(documentDetail, this._enableValidation),
                                        Encoding.UTF8,
                                        SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                                 true);
                            if ((this._testController.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                              this._testController.Last_HTTPstatus == (int)HttpStatusCode.Accepted))
                            {
                                uploadedDocumentDetails_Response.Add(TestController.DeserializeXML<DocumentDetail>(responseText, this._enableValidation));
                            }
                            else
                            {
                                this.LoadingBarHide();
                                this.DisplayHTTPError(this._testController.Last_HTTPResponse.Content.ReadAsStringAsync().Result);
                                return false;
                            }
                            responseText = null;
                            GC.Collect();
                        }
                        else
                        {
                            uploadedDocumentDetails_Response.Add(documentDetail);
                        }
                    }
                    else
                    {
                        uploadedDocumentDetails_Response.Add(documentDetail);
                    }
                }
                this._workSet_DocumentDetail = uploadedDocumentDetails_Response;
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                this._FailMessages.Add("Failed to update batch");
                return false;
            }
        }

        private void SynchronizeDocumentDetail()
        {
            List<DocumentDetail> removalList = new List<DocumentDetail>();
            foreach (DocumentDetail documentDetail in this._workSet_DocumentDetail)
            {
                DocumentInfo relatedDocumentInfo = this.GetLocalDocumentInfo(documentDetail);
                if (relatedDocumentInfo == null)
                {
                    removalList.Add(documentDetail);
                }
                else
                {
                    documentDetail.StatusCode = relatedDocumentInfo.StatusCode;

                    //if (relatedDocumentInfo.RecordableFileContentInfo)

                    //if (documentDetail.FileContentDetails.RecordableFileContentDetail != null)
                    //{
                    //    if (documentDetail.FileContentDetails.RecordableFileContentDetail.FileContentInfo == null)
                    //    {
                    //        documentDetail.FileContentDetails.RecordableFileContentDetail = null;
                    //    }
                    //}
                    if (relatedDocumentInfo.PCORFileContentInfo != null)
                    {
                        if (documentDetail.FileContentDetails.PCORFileContentDetail == null)
                        {
                            documentDetail.FileContentDetails.PCORFileContentDetail = new FileContentDetail();
                        }

                        documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo = relatedDocumentInfo.PCORFileContentInfo;

                        if (documentDetail.FileContentDetails.PCORFileContentDetail.FileContentInfo == null)
                        {
                            documentDetail.FileContentDetails.PCORFileContentDetail = null;
                        }
                    }
                    if (documentDetail.FileContentDetails.OtherFileContentDetail != null)
                    {
                        if (documentDetail.FileContentDetails.OtherFileContentDetail.FileContentInfo == null)
                        {
                            documentDetail.FileContentDetails.OtherFileContentDetail = null;
                        }
                    }
                }
            }
        }

        private Task<string> UploadDocumentDetail(DocumentDetail documentDetail, int retry)
        {
            try
            {
                GC.Collect();
                return this.PerformHTTP_Async(
                    HTTP.PUT,
                    new string[] { "Document", "Upload" },
                    null,
                    new StringContent(
                             TestController.SerializeXML(documentDetail, this._enableValidation),
                            Encoding.UTF8,
                            SECURE.Common.Controller.Media.Schema.SupportedMediaTypes.XML.ToString()),
                     true);
            }
            catch (OutOfMemoryException ex)
            {
                if (retry < 10)
                {
                    return this.UploadDocumentDetail(documentDetail, ++retry);
                }
                else
                {
                    throw ex;
                }
            }
        }

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
        /// Upload batch changes and set Status Code to "Uploading"
        /// </summary>
        /// <returns>bool, true if successful</returns>
        private async Task<bool> UpdateBatchDetail_Uploading()
        {
            try
            {
                this.LoadingBarShow("Setting Batches to Uploading");
                List<BatchDetail> temp_workset_BatchDetail = new List<BatchDetail>();
                foreach (BatchDetail batchDetail in this._workSet_BatchDetail)
                {
                    if (int.Parse(batchDetail._ID) > 0)
                    {
                        if (batchDetail.StatusCode == BatchMediaStatusCode.New || batchDetail.StatusCode == BatchMediaStatusCode.Correction_Downloaded_By_Submitter)
                        {
                            // Validate Document Image pagecount
                            foreach (DocumentInfo documentInfo in batchDetail.Documents)
                            {
                                if (int.Parse(documentInfo.RecordableFileContentInfo.PageCount) < 1)
                                {
                                    this._FailMessages.Add(string.Format("PageCount of Recordable image in batch [{0}] cannot be zero.", batchDetail._SubmissionID));
                                    return false;
                                }
                                if (documentInfo.PCORFileContentInfo != null && int.Parse(documentInfo.PCORFileContentInfo.PageCount) < 1)
                                {
                                    this._FailMessages.Add(string.Format("PageCount of PCOR image in batch [{0}] cannot be zero.", batchDetail._SubmissionID));
                                    return false;
                                }
                                if (documentInfo.OtherFileContentInfo != null && int.Parse(documentInfo.OtherFileContentInfo.PageCount) < 1)
                                {
                                    this._FailMessages.Add(string.Format("PageCount of Other image in batch [{0}] cannot be zero.", batchDetail._SubmissionID));
                                    return false;
                                }
                            }
                            // Change status code of detail
                            batchDetail.StatusCode = batchDetail.StatusCode == BatchMediaStatusCode.New ? BatchMediaStatusCode.Submitter_Uploading : BatchMediaStatusCode.Submitter_Uploading_Corrected;
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
                                temp_workset_BatchDetail.Add(resultBatchDetail);
                            }
                            else
                            {
                                this.DisplayHTTPError(responseText);
                                return false;
                            }
                        }
                        else
                        {
                            temp_workset_BatchDetail.Add(batchDetail);
                        }
                    }
                }
                this._workSet_BatchDetail = temp_workset_BatchDetail;
                return true;
            }
            catch
            {
                this._FailMessages.Add("Failed to update batch to uploading status.");
                return false;
            }
        }
        /// <summary>
        /// Remove a document from a batch
        /// </summary>
        /// <param name="batchDetail">BatchDetail, the Batch containing the document</param>
        /// <param name="documentInfo">DocumentInfo, the document to be removed</param>
        /// <returns>bool, true if batch and document were modified successfully</returns>
        private bool RemoveDocumentFromBatch(BatchDetail batchDetail, DocumentInfo documentInfo)
        {
            try
            {
                if (documentInfo.ActionCode == ActionCode.New)
                {
                    ArrayList newDocumentArray = new ArrayList();
                    foreach (DocumentInfo batchDocument in batchDetail.Documents)
                    {
                        if (batchDocument != documentInfo)
                        {
                            newDocumentArray.Add(batchDocument);
                        }
                    }
                    batchDetail.Documents = (DocumentInfo[])newDocumentArray.ToArray(typeof(DocumentInfo));
                }
                else
                {
                    documentInfo.ActionCode = ActionCode.Delete;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> ValidateBatches(List<BatchDetail> batchDetailList, bool showValidResults)
        {
            if (this._workSet_BatchDetail.Count == 0)
            {
                this.LoadingBarHide();
                MessageBox.Show(this, "No batches to validate.");
                return false;
            }
            else
            {
                string validationMessage = string.Empty;
                await Task.Run(() =>
                {
                    foreach (BatchDetail batchDetail in batchDetailList)
                    {
                        List<string> batchValidation = TestController.ValidateSECURE(batchDetail, this.GetLocalIndexOption(batchDetail.County._ID));

                        if (batchValidation.Count == 0 && showValidResults)
                        {
                            if (validationMessage != string.Empty)
                            {
                                validationMessage = validationMessage + Environment.NewLine + "-------------------------------";
                            }
                            validationMessage = validationMessage + Environment.NewLine + string.Format("Batch [{0}] passsed validation.", batchDetail._ID);
                        }
                        else if (batchValidation.Count > 0)
                        {
                            if (validationMessage != string.Empty)
                            {
                                validationMessage = validationMessage + Environment.NewLine + "-------------------------------";
                            }
                            validationMessage = validationMessage + Environment.NewLine + string.Format("Batch [{0}] failed validation.", batchDetail._ID);
                            foreach (string vmsg in batchValidation)
                            {
                                validationMessage = string.Format("{0}{1}{2}{3}",
                                    validationMessage,
                                    validationMessage == string.Empty ? string.Empty : Environment.NewLine,
                                    vmsg.StartsWith("Document") ? "     " : string.Empty,
                                    vmsg);
                            }
                        }
                    }
                });
                if (validationMessage != string.Empty)
                {
                    this.LoadingBarHide();
                    (new ExceptionBox() { ExceptionMessage = ("Validation Results:" + Environment.NewLine + Environment.NewLine + validationMessage) }).ShowDialog(this);
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion

        #region Search Box
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
        /// Search by BatchID checked
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
        /// Search by Batch Name checked
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
        /// Search by Submission ID checked
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
        /// Search by Batch Status checked
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
        /// Search by County checked
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
        /// Search by User Name checked
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
                this.txtViewBatchSearch_input.Text = TestController.Default_UserName_Submitter;
                this._ConfiguringSearch = false;
            }
        }

        #endregion

        #region Other Event Methods

        /// <summary>
        /// Event method for when the panel loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitterPanel_Load(object sender, EventArgs e)
        {
            this.cboViewBatch_Status.DataSource = Enum.GetNames(typeof(BatchMediaStatusCode));
            this.cboDownloadBatch_Status.DataSource = new object[]
            {
                // Only these Batch Statuses are allowed for an Non-County User
                BatchMediaStatusCode.Correction_Uploaded_By_County,
                BatchMediaStatusCode.Completed_Uploaded_By_County
            };
            this.txtURL.Text = TestController.SECURE_ADDRESS_SUBMITTER;
            this._searchBy = SearchBy.None;
            this.ConfigureSearchBox();
            this.enableFormControls(this._testController.SessionTokenID != string.Empty && this._testController.SessionTokenID != null);
            this.chkToken.Checked = TestController.TwoFactor_Enabled_Submitter;
        }
        /// <summary>
        /// Event method for API checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAPI_CheckedChanged(object sender, EventArgs e)
        {
            this.grpAPIButtons.Visible = this.chkAPI.Checked;
        }

        private void chk_XML_Validation_CheckedChanged(object sender, EventArgs e)
        {
            this._enableValidation = this.chk_XML_Validation.Checked;
        }

        private void chkToken_CheckedChanged(object sender, EventArgs e)
        {
            TestController.TwoFactor_Enabled_Submitter = this.chkToken.Checked;
        }

        private void trvBatches_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.trvBatches.Nodes == null || this.trvBatches.Nodes.Count == 0)
            {
                this.ConfigureContextMenu_Visible(null);
            }
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
    }
}
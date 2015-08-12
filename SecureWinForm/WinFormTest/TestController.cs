using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace WinFormTest
    {

    public enum UserType
    {
        County,
        Submitter
    }
    
    /// <summary>
    /// Enumeration that describes possible HTTP methods
    /// </summary>
    public enum HTTP
    {
        GET,
        PUT,
        POST,
        DELETE
    }

    /// <summary>
    /// Controller containing HTTP Communication and encode/decode functions
    /// </summary>
    public class TestController
    {
        #region Declarations
        
        private static Properties.Settings _clientSettings;
        private static Properties.Settings ClientSettings
        {
            get
            {
                if (TestController._clientSettings == null)
                {
                    TestController._clientSettings = Properties.Settings.Default;
                }
                return TestController._clientSettings;
            }
        }
        public static void SaveSettings()
        {
            TestController._clientSettings.Save();
        }
        /// <summary>
        ///  Current Session Token ID
        /// </summary>
        public string SessionTokenID
        {
            get
            {
                return this.Session == null ? null : this.Session.SessionTokenID;
            }
        }
        /// <summary>
        /// The current SessionDetail object
        /// </summary>
        public SessionDetail Session
        {
            get
            {
                return this._Session;
            }
            set
            {
                this._Session = value;
                this.NeedPasswordChange = this._Session == null ? false : this._Session.PasswordExpired; 
            }
        }
        private SessionDetail _Session = null;
        /// <summary>
        /// URL of SECURE WebService for County
        /// </summary>
        public static string SECURE_ADDRESS_COUNTY 
        {
            get
            {
                return (TestController.ClientSettings.SECURE_API_URI_COUNTY == string.Empty) ? "http://127.0.0.1/WebAPI/" : TestController.ClientSettings.SECURE_API_URI_COUNTY; // Default API URL, localhost
            }
            set
            {
                TestController.ClientSettings.SECURE_API_URI_COUNTY = value;
                TestController.SaveSettings();
            }
        }
        /// <summary>
        /// URL of SECURE WebService for Submitter
        /// </summary>
        public static string SECURE_ADDRESS_SUBMITTER 
        {
            get
            {
                return (TestController.ClientSettings.SECURE_API_URI_SUBMITTER == string.Empty) ? "http://127.0.0.1/WebAPI/" : TestController.ClientSettings.SECURE_API_URI_SUBMITTER; // Default API URL, localhost
            }
            set
            {
                TestController.ClientSettings.SECURE_API_URI_SUBMITTER = value;
                TestController.SaveSettings();
            }
        }
        public static string Default_UserName_Submitter
        {
            get
            {

                return TestController.ClientSettings.Default_Submitter_UserName;
            }
            set
            {
                TestController.ClientSettings.Default_Submitter_UserName = value;
                TestController.SaveSettings();
            }
        }
        public static string Default_UserName_County
        {
            get
            {

                return TestController.ClientSettings.Default_County_UserName;
            }
            set
            {
                TestController.ClientSettings.Default_County_UserName = value;
                TestController.SaveSettings();
            }
        }
        public static string Default_Submitter_Password
        {
            get
            {
                return TestController.ClientSettings.Default_Submitter_Password;
            }
            set
            {
                TestController.ClientSettings.Default_Submitter_Password = value;
                TestController.SaveSettings();
            }
        }
        public static string Default_County_Password_2Factor
        {
            get
            {
                return TestController.ClientSettings.Default_County_Password_2Factor; 
            }
            set
            {
                TestController.ClientSettings.Default_County_Password_2Factor = value;
                TestController.SaveSettings();
            }
        }
        public static string Default_County_Password
        {
            get
            {
                return TestController.ClientSettings.Default_County_Password;
            }
            set
            {
                TestController.ClientSettings.Default_County_Password = value;
                TestController.SaveSettings();
            }
        }
        public static bool TwoFactor_Enabled_County
        {
            get
            {
                return TestController.ClientSettings.TwoFactor_Enabled_County;
            }
            set
            {
                TestController.ClientSettings.TwoFactor_Enabled_County = value;
                TestController.SaveSettings();
            }
        }
        public static bool TwoFactor_Enabled_Submitter
        {
            get
            {
                return TestController.ClientSettings.TwoFactor_Enabled_Submitter;
            }
            set
            {
                TestController.ClientSettings.TwoFactor_Enabled_Submitter = value;
                TestController.SaveSettings();
            }
        }
        private string _lastURL = string.Empty;
        /// <summary>
        /// URL that was last used
        /// </summary>
        public string Last_URL
        {
            get
            {
                return this._lastURL.Replace("&", "&&");
            }
        }
       
        /// <summary>
        /// HTTP Status code that was last received by the client
        /// </summary>
        public int Last_HTTPstatus = 0;
        /// <summary>
        /// Bool, true if the last login attempt indicated that a password change was needed
        /// </summary>
        public bool NeedPasswordChange = false;
        /// <summary>
        /// ReasonPhrase of the last HTTP Operation
        /// </summary>
        public string Last_ReasonPhrase = string.Empty;
        /// <summary>
        /// The last HTTPResponseMessage
        /// </summary>
        public HttpResponseMessage Last_HTTPResponse = null;
        /// <summary>
        /// The last HTTP Response String
        /// </summary>
        public string Last_HTTPResponseString = string.Empty;
        /// <summary>
        /// The content of the last HTTP Request
        /// </summary>
        public string Last_HTTPRequestContent = string.Empty;
        /// <summary>
        /// List of parameters that need user input
        /// </summary>
        public static string[] TextInputParameters = new string[]
        {
            "UserName",
            "Password",
            "OldPassword",
            "NewPassword",
            "SubmissionID",
            "BatchID",
            "DocID",
            "UserID",
            "StatusCode",
            "EditTS",
            "EndDate",
            "StartDate",
            "Sequence",
            "ExpirationMessage",
            "TwoFactorPassword"
        };
        /// <summary>
        /// List of Parameters that are XML-encoded objects
        /// </summary>
        public static string[] ContentParameters = new string[]
        {
            "DocumentXML",
            "BatchXML"
        };

        public static string[] EnumParameters = new string[]
        {
            "ActionCode",
            "BatchStatus",
            "DocumentStatus"
        };
        private UserType _currentUserType;
        public UserType CurrentUserType
        {
            get
            {
                return this._currentUserType;
            }
        }

        private HttpClientHandler httpClientHandler = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for TestController
        /// </summary>
        public TestController(UserType userType)
        {
            this._currentUserType = userType;
            this.httpClientHandler = new HttpClientHandler();
        }

        #endregion
        
        #region Log In

        /// <summary>
        /// Perform Login operation asynchronously
        /// </summary>
        /// <param name="mediaType">MediaType, enumeration defining the media type of the http operation</param>
        /// <param name="username">string, containing the username</param>
        /// <param name="password">string, containing the password</param>
        /// <returns>string, containing the complete text of the HTTP response/returns>
        public async Task<string> LoginAsync(string username, string password, string twoFactorPassword, bool enableValidation)
        {
            string responseText = await this.SECURE_API_Async(
                Properties.Resources.MediaType_XML,
                HTTP.PUT,
                new string[] { "Authentication", "Login" },
                null,
                new StringContent(
                    TestController.SerializeXML(new AuthenticationDetail()
                    {
                        UserName = username,
                        Password = password,
                        TwoFactorPassword = twoFactorPassword
                    })));
            if (this.Last_HTTPstatus == (int)HttpStatusCode.OK ||
                this.Last_HTTPstatus == (int)HttpStatusCode.Accepted)
            {
                this.Session = TestController.DeserializeXML<SessionDetail>(responseText, enableValidation);
            }
            return responseText;
        }
        ///// <summary>
        ///// Hash input with SHA-256 then encode in Base64
        ///// </summary>
        ///// <param name="input">string, containing the input string</param>
        ///// <returns>string, containing encoded hash</returns>
        //public static string HashSHA256Encode(string input)
        //{
        //    return Convert.ToBase64String(SHA256Managed.Create().ComputeHash(ASCIIEncoding.ASCII.GetBytes(input)));
        //}

        #endregion
        
        #region SECURE API
        
        public string CreateParameterString(DictionaryEntry[] apiParameters)
        {
            string target_URI_parameters = string.Empty;
            foreach (DictionaryEntry entry in apiParameters)
            {
                if (entry.Key.ToString() != string.Empty && entry.Value.ToString() != string.Empty)
                {
                    target_URI_parameters = string.Format(target_URI_parameters == string.Empty ? "{0}{1}={2}" : "{0}&{1}={2}",
                        target_URI_parameters,
                        entry.Key,
                        (entry.Value.ToString().Contains("+")) ? HttpUtility.UrlEncode(entry.Value.ToString()) : entry.Value);
                }
            }
            return target_URI_parameters;
        }

        /// <summary>
        /// Execute method on SECURE Web API asynchronously
        /// </summary>
        /// <param name="mediaType">MediaType, enum describing the media type of the http operation</param>
        /// <param name="httpMethod">HTTP enum, describing the http method</param>
        /// <param name="apiNames">string array, containing the api URL paths</param>
        /// <param name="apiValues">DictionaryEnty array, containing the API names and values in order</param>
        /// <param name="httpContent">HttpContent, containing the data to be uploaded in HTTP request</param>
        /// <returns>string, containing the http response</returns>
        public async Task<string> SECURE_API_Async(string mediaType, HTTP httpMethod, string[] apiNames, DictionaryEntry[] apiValues, HttpContent httpContent)
        {
            try
            {
                using (httpContent)
                {
                    this.Last_HTTPRequestContent = httpContent == null ? "null" : await httpContent.ReadAsStringAsync();
                    HttpClient httpClient = new HttpClient(httpClientHandler);
                    // Base URL
                    httpClient.BaseAddress = new Uri(this._currentUserType == UserType.County ? TestController.SECURE_ADDRESS_COUNTY : TestController.SECURE_ADDRESS_SUBMITTER);
                    // Request Headers
                    if (this.SessionTokenID != null)
                    {
                        httpClient.DefaultRequestHeaders.Add(
                            "SessionTokenID",
                            this.SessionTokenID);
                    }
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType));
                    // URL Path
                    string target_URI = "api";
                    foreach (string apiName in apiNames)
                    {
                        target_URI = string.Format("{0}/{1}",
                            target_URI,
                            apiName);
                    }
                    string target_URI_parameters = string.Empty;
                    if (apiValues != null)
                    {
                        target_URI_parameters = this.CreateParameterString(apiValues);
                    }
                    target_URI = (target_URI_parameters == string.Empty) ? target_URI : string.Format("{0}?{1}", target_URI, target_URI_parameters);

                    /*//===============================================================================================================
                    //HACK: Ignore SSL/TLS certificate errors 
                    //      DANGEROUS!! DO NOT DO THIS OUTSIDE OF UAT!
                    //      Implemented because SECURE UAT server's certificate is invalid from the sub-sub-domain names
                    //      eg: https://sapi.secure-recording.com/ will validate, but https://sapi.uat.secure-recording.com/ will fail
                    //===============================================================================================================
                    if (httpClient.BaseAddress.AbsoluteUri.ToLower().Contains(".uat.secure-recording.com"))
                    {
                        //Trust all certificates
                        System.Net.ServicePointManager.ServerCertificateValidationCallback =
                            ((sender, certificate, chain, sslPolicyErrors) => true);
                    }
                    //===============================================================================================================*/

                    // Clear values
                    this._lastURL = httpClient.BaseAddress.ToString() + target_URI;
                    this.Last_HTTPstatus = 0;
                    this.Last_ReasonPhrase = string.Empty;
                    this.Last_HTTPResponse = null;
                    this.Last_HTTPResponseString = string.Empty;

                    HttpResponseMessage response = null;
                    try
                    {
                        switch (httpMethod)
                        {
                            case HTTP.POST:
                                {
                                    response = await httpClient.PostAsync(target_URI, httpContent);
                                    break;
                                }
                            case HTTP.PUT:
                                {
                                    response = await httpClient.PutAsync(target_URI, httpContent);
                                    break;
                                }
                            case HTTP.DELETE:
                                {
                                    response = await httpClient.DeleteAsync(target_URI);
                                    break;
                                }
                            case HTTP.GET:
                            default:
                                {
                                    response = await httpClient.GetAsync(target_URI);
                                    break;
                                }
                        }
                    }
                    catch (HttpResponseException ex)
                    {
                       
                        if (ex.Response != null)
                        {
                            this.Last_HTTPResponse = ex.Response;
                        }
                        throw;
                    }

                    this.Last_HTTPstatus = (int)response.StatusCode;
                    this.Last_ReasonPhrase = response.ReasonPhrase;
                    this.Last_HTTPResponse = response;
                    this.Last_HTTPResponseString = (await response.Content.ReadAsStringAsync()).Trim();
                    return (this.Last_HTTPResponseString == string.Empty) ? this.Last_ReasonPhrase : this.Last_HTTPResponseString;
                }
            }
            finally
            {
                GC.Collect(3, GCCollectionMode.Forced, true);
            }
        }
                
        #endregion

        #region XML Serialization/Deserialization

        /// <summary>
        /// Serialize the XSD generated SECURE code to XML in string format.
        /// </summary>
        /// <param name="o">The System.Object to serialize.</param>
        /// <param name="validate">bool, true to validate data after serialization</param>
        /// <returns>a UTF-8 encoded XML string</returns>
        public static string SerializeXML(object o, bool validate)
        {
            try
            {
                using (Utf8StringWriter stringWriter = new Utf8StringWriter())
                {
                    TestController.GetCachedXMLSerializer(o.GetType(), Properties.Resources.SECURE_XML_Schema).Serialize(stringWriter, o);
                    if (validate)
                    {
                        TestController.ValidateXML(o.GetType(), stringWriter.ToString());
                    }
                    return stringWriter.ToString();
                }
            }
            finally
            {
                GC.Collect();
            }
        }
        /// <summary>
        /// Serialize the XSD generated SECURE code to XML in string format.
        /// </summary>
        /// <param name="o">The System.Object to serialize.</param>
        /// <returns>a UTF-8 encoded XML string</returns>
        public static string SerializeXML(object o)
        {
            return TestController.SerializeXML(o, true);
        }
        /// <summary>
        /// Deserialize XML string into XSD generated SECURE code
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized into</typeparam>
        /// <param name="xml">string, containing the XML data</param>
        /// <param name="validation">bool, true to enable validation during deserialization</param>
        /// <returns>object of Type T with data from the XML string</returns>
        public static T DeserializeXML<T>(string xml, bool validation)
        {
            return TestController.DeserializeXML<T>(
                xml,
                Properties.Resources.SECURE_XML_Schema,
                (System.IO.Directory.GetCurrentDirectory() + "\\XML\\SECURE.xsd"),
                validation);
        } /// <summary>
        /// Deserialize XML string into XSD generated SECURE code
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized into</typeparam>
        /// <param name="xml">string, containing the XML data</param>
        /// <returns>object of Type T with data from the XML string</returns>
        public static T DeserializeXML<T>(string xml)
        {
            return TestController.DeserializeXML<T>(xml, true);
        }
        private static readonly Hashtable serializerCache = new Hashtable();
        private static readonly Hashtable xmlReaderSettingCache = new Hashtable();

        /// <summary>
        /// Deserialize XML string into a .net Object
        /// </summary>
        /// <typeparam name="T">Type of object to be deserialized into</typeparam>
        /// <param name="xml">string, containing the XML data</param>
        /// <param name="schema">string, containing the name of the XML schema</param>
        /// <param name="schemaURI">string, containing the URI of the XML schema</param>
        /// <returns>object of Type T with data from the XML string</returns>
        private static T DeserializeXML<T>(string xml, string schema, string schemaURI, bool validation)
        {
            GC.Collect(3, GCCollectionMode.Forced, true);
            try
            {
                using (StringReader sReader = new StringReader(xml))
                {
                    // Create temporary copy of XML Reader Settings to use if it needs to be modified
                    XmlReaderSettings xmlReaderSettings = TestController.GetCachedXMLReaderSettings(schema, schemaURI).Clone();
                    if (!validation)
                    {
                        // remove ValidationEventHandler from temporary copy to skip validation
                        xmlReaderSettings.ValidationEventHandler -= xmlRSettings_ValidationEventHandler;
                        xmlReaderSettings.ValidationType = ValidationType.None;
                    }
                    using (XmlReader xmlReader = XmlReader.Create(sReader, xmlReaderSettings))
                    {
                        XmlSerializer xmlSerializer = TestController.GetCachedXMLSerializer(typeof(T), schema);
                        return (T)(xmlSerializer.Deserialize(xmlReader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to serialize XML: " + xml, ex);
            }
            finally
            {
                // Clear
                xml = null;
                GC.Collect(3, GCCollectionMode.Forced, true);
            }
        }
        private static XmlSerializer GetCachedXMLSerializer(Type type, string schema)
        {
            var cacheKey = new { Type = type, Name = schema };
            XmlSerializer xmlSerializer = (XmlSerializer)serializerCache[cacheKey];
            if (xmlSerializer == null)
            {
                lock (serializerCache)
                {
                    // double-checked
                    xmlSerializer = (XmlSerializer)serializerCache[cacheKey];
                    if (xmlSerializer == null)
                    {
                        xmlSerializer = new XmlSerializer(type, schema);
                        serializerCache.Add(cacheKey, xmlSerializer);
                    }
                }
            }
            return xmlSerializer;
        }
        private static XmlReaderSettings GetCachedXMLReaderSettings(string schema, string schemaURI)
        {
            var cacheKey = new { Schema = schema, SchemaURI = schemaURI };
            XmlReaderSettings xmlReaderSettings = (XmlReaderSettings)xmlReaderSettingCache[cacheKey];
            if (xmlReaderSettings == null)
            {
                lock (xmlReaderSettingCache)
                {
                    // double-checked
                    xmlReaderSettings = (XmlReaderSettings)xmlReaderSettingCache[cacheKey];
                    if (xmlReaderSettings == null)
                    {
                        xmlReaderSettings = new XmlReaderSettings();
                        if (schema == Properties.Resources.SECURE_XML_Schema)
                        {
                            xmlReaderSettings.ValidationType = ValidationType.Schema;
                            xmlReaderSettings.ValidationEventHandler += xmlRSettings_ValidationEventHandler;
                        }
                        xmlReaderSettings.Schemas.Add(schema, schemaURI);
                        xmlReaderSettingCache.Add(cacheKey, xmlReaderSettings);
                    }
                }
            }
            return xmlReaderSettings;
        }
           /// <summary>
        /// Event Handler for XSD Validation.
        /// </summary>
        /// <param name="sender">sender object.</param>
        /// <param name="e">Validation event Arguments.</param>
        private static void xmlRSettings_ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case System.Xml.Schema.XmlSeverityType.Error:
                    {
                        if (e.Exception is XmlSchemaValidationException)
                        {
                            throw e.Exception;
                        }
                        break;
                    }
            }
        }

        private static bool ValidateXML(Type t, string inputXML)
        {
            GC.Collect(3, GCCollectionMode.Forced, true);
            try
            {
                using (StringReader sReader = new StringReader(inputXML))
                {
                    using (XmlReader xmlReader = XmlReader.Create(sReader, TestController.GetCachedXMLReaderSettings(Properties.Resources.SECURE_XML_Schema,  (System.IO.Directory.GetCurrentDirectory() + "\\XML\\SECURE.xsd"))))
                    {
                        return (TestController.GetCachedXMLSerializer(t, Properties.Resources.SECURE_XML_Schema).Deserialize(xmlReader)) != null;
                    }
                }
            }
            finally
            {
                // Clear
                GC.Collect(3, GCCollectionMode.Forced, true);
            }
        }
        #endregion

        #region Class Construction

        #region DocumentDetail

        /// <summary>
        /// Create DocumentDetail from DocumentInfo
        /// </summary>
        /// <param name="documentInfo">DocumentInfo, containing the document info</param>
        /// <param name="batchID">string, containing the Batch ID</param>
        /// <returns>DocumentDetail</returns>
        public static DocumentDetail CreateDocumentDetail(DocumentInfo documentInfo, string batchID)
        {
            return new DocumentDetail()
           {
               _ID = documentInfo._ID,
               _BatchID = batchID,
               StatusCode = documentInfo.StatusCode,
               FileContentDetails = new FileContentDetails()
               {
                   RecordableFileContentDetail = new FileContentDetail()
                   {
                       FileContentInfo = documentInfo.RecordableFileContentInfo,
                       FileContent = new FileContent[0]
                   },
                   PCORFileContentDetail = new FileContentDetail()
                   {
                       FileContentInfo = documentInfo.PCORFileContentInfo,
                       FileContent = new FileContent[0]
                   },
                   OtherFileContentDetail = new FileContentDetail()
                   {
                       FileContentInfo = documentInfo.OtherFileContentInfo,
                       FileContent = new FileContent[0]
                   }
               }
           };
        }

        #endregion
        
        #endregion

        #region XML Formatting

        /// <summary>
        /// Format XML into readable
        /// </summary>
        /// <param name="inputXML">string, containing unformatted XML</param>
        /// <returns>string, containing formatted XML</returns>
        public static string FormatXML(string inputXML)
        {
            try
            {
                string outputXML = string.Empty;

                string readPart = string.Empty;
                int pos = 0;
                int numIndents = 0;
                string lastNode = string.Empty;
                Hashtable nodes = new Hashtable(new Dictionary<string, int>());
                while (pos < inputXML.Length)
                {
                    readPart += inputXML.Substring(pos++, 1);
                    if (readPart[0] == '<' && readPart.Contains("/>")) // self contained node and section
                    {
                        if (lastNode.Contains("</"))
                        {
                            for (int i = 0; i < int.Parse(nodes[TestController.GetXMLStart(lastNode)].ToString()); i++)
                            {
                                outputXML += "\t";
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numIndents; i++)
                            {
                                outputXML += "\t";
                            }
                        }
                        outputXML += (readPart + "\r\n");
                        readPart = string.Empty;
                    }
                    else if (readPart.Contains("<?") && readPart.Contains("?>")) // XML comment
                    {
                        if (lastNode.Contains("</"))
                        {
                            for (int i = 0; i < int.Parse(nodes[TestController.GetXMLStart(lastNode)].ToString()); i++)
                            {
                                outputXML += "\t";
                            }
                        }
                        else
                        {
                            for (int i = 0; i < numIndents; i++)
                            {
                                outputXML += "\t";
                            }
                        }
                        outputXML += (readPart + "\r\n");
                        readPart = string.Empty;
                    }
                    else if (readPart[0] == '<' && readPart.Contains(">") && !readPart.Contains("</")) // node start
                    {
                        if (nodes.ContainsKey(readPart))
                        {
                            for (int i = 0; i < int.Parse(nodes[readPart].ToString()); i++)
                            {
                                outputXML += "\t";
                            }
                            lastNode = readPart;
                        }
                        else
                        {
                            if (lastNode.Contains("</"))
                            {
                                for (int i = 0; i < int.Parse(nodes[TestController.GetXMLStart(lastNode)].ToString()); i++)
                                {
                                    outputXML += "\t";
                                }
                            }
                            else
                            {
                                for (int i = 0; i < numIndents; i++)
                                {
                                    outputXML += "\t";
                                }
                            }
                        }
                        outputXML += (readPart + "\r\n");
                        if (!nodes.ContainsKey(readPart))
                        {
                            if (readPart.Contains(" "))
                            {
                                string newNode = readPart.Substring(0, readPart.IndexOf(" ")) + ">";
                                if (!nodes.ContainsKey(newNode))
                                {
                                    nodes.Add(newNode, numIndents);
                                    if (!lastNode.Contains("</"))
                                    {
                                        numIndents++;
                                    }
                                    lastNode = newNode;
                                }
                            }
                            else
                            {
                                nodes.Add(readPart, numIndents);
                                if (!lastNode.Contains("</"))
                                {
                                    numIndents++;
                                }
                                lastNode = readPart;
                            }
                        }
                        readPart = string.Empty;
                    }
                    else if (readPart.Contains("</") && readPart.Contains(">")) // value and node close
                    {
                        if (readPart[0] == '<') // node close only
                        {
                            for (int i = 0; i < int.Parse(nodes[TestController.GetXMLStart(readPart)].ToString()); i++)
                            {
                                outputXML += "\t";
                            }
                            outputXML += (readPart + "\r\n");
                            lastNode = readPart;
                            readPart = string.Empty;
                        }
                        else
                        {
                            for (int i = 0; i < ((int)nodes[lastNode]) + 1; i++)
                            {
                                outputXML += "\t";
                            }
                            outputXML += (readPart.Substring(0, readPart.IndexOf("</")) + "\r\n");
                            numIndents = (int)nodes[lastNode];

                            for (int i = 0; i < int.Parse(nodes[TestController.GetXMLStart(readPart.Substring(readPart.IndexOf("</")))].ToString()); i++)
                            {
                                outputXML += "\t";
                            }
                            outputXML += (readPart.Substring(readPart.IndexOf("</")) + "\r\n");
                            lastNode = readPart.Substring(readPart.IndexOf("</"));
                            readPart = string.Empty;
                        }
                    }
                    if (readPart != string.Empty && pos == inputXML.Length)
                    {
                        outputXML += readPart;
                    }
                }

                return outputXML;
            }
            catch
            {
                return inputXML;
            }
        }
        private static string GetXMLClose(string inXML)
        {
            return "</" + inXML.Substring(1);
        }
        private static string GetXMLStart(string inXML)
        {
            return "<" + inXML.Substring(2);
        }

        #endregion

        #region Base 64

        /// <summary>
        /// Decode Base64 encoded string into a file
        /// </summary>
        /// <param name="base64encoded">string, containing the Base64 data</param>
        /// <param name="destFilePath">string, containing the destination file path</param>
        /// <returns>Task</returns>
        public static async Task DecodeBase64File(string base64encoded, string destFilePath)
        {
            byte[] filebytes = await Task<byte[]>.Run(() =>
                {
                    return Convert.FromBase64String(base64encoded);
                });
            FileStream fs = new FileStream(destFilePath,
                                           FileMode.Create,
                                           FileAccess.ReadWrite,
                                           FileShare.None);
            await fs.WriteAsync(filebytes, 0, filebytes.Length);
            fs.Close(); 
        }

        /// <summary>
        /// Encode file into Base64 string
        /// </summary>
        /// <param name="filePath">string, containing the full file path of the selected file</param>
        /// <returns>string, containing the encoded file data</returns>
        public static async Task<string> EncodeBase64Async(string filePath)
        {
            string encodedstring = string.Empty;
            byte[] fileBytes = await Task<string>.Run(() =>
            {
                return File.ReadAllBytes(filePath);
            }).ConfigureAwait(true);
            encodedstring = await Task<string>.Run(() =>
            {
                string encoded = Convert.ToBase64String(fileBytes);
                return encoded;
            }).ConfigureAwait(true);
            return encodedstring;
        }

        #endregion

        #region Validation 
      
        /// <summary>
        /// Perform validation on batch detail
        /// </summary>
        /// <param name="batchDetail">BatchDetail object, containing information on Batch and Documents</param>
        /// <param name="indexOptionDetail">IndexOptionDetail object, containing the validation rules for the accompanying BatchDetail</param>
        /// <returns>List containing validation error messages. Returns empty list if BatchDetail passed validation</returns>
        public static List<string> ValidateSECURE(BatchDetail batchDetail, IndexOptionDetail indexOptionDetail)
        {
            List<string> ValidationMessages = new List<string>();

            // Validate BatchDetail
            if (indexOptionDetail.RequireRequestingParty && batchDetail.RequestingParty == null)
            {
                ValidationMessages.Add(string.Format("Batch [{0}]: RequestingParty is required.", batchDetail._ID));
            }
            if (indexOptionDetail.RequireConcurrentIndex && !batchDetail.IsConcurrent)
            {
                ValidationMessages.Add(string.Format("Batch [{0}]: IsConcurrnent is required.", batchDetail._ID));
            }

            foreach (DocumentInfo documentInfo in batchDetail.Documents)
            {
                // Validate DocumentInfo
                if (indexOptionDetail.RequireTitleIndex)
                {
                    if (documentInfo.Titles == null)
                    {
                        ValidationMessages.Add(string.Format("Document [{0}]: Titles are required.", documentInfo._ID));
                    }
                    else if (documentInfo.Titles.Length == 0)
                    {
                        ValidationMessages.Add(string.Format("Document [{0}]: Titles are required.", documentInfo._ID));
                    }
                }
                if (indexOptionDetail.RequireNamesIndex)
                {
                    if (documentInfo.Names == null)
                    {
                        ValidationMessages.Add(string.Format("Document [{0}]: Names are required.", documentInfo._ID));
                    }
                    else if (documentInfo.Names.Length == 0)
                    {
                        ValidationMessages.Add(string.Format("Document [{0}]: Names are required.", documentInfo._ID));
                    }
                }
                if (indexOptionDetail.RequireAPNIndex && (documentInfo.AssessorParcelNumber == null || documentInfo.AssessorParcelNumber == string.Empty))
                {
                    ValidationMessages.Add(string.Format("Document [{0}]: Assessor Parcel Number is required.", documentInfo._ID));
                }
                if (indexOptionDetail.RequireTransferTaxIndex && !documentInfo.TransferTaxAmountSpecified)
                {
                    ValidationMessages.Add(string.Format("Document [{0}]: Transfer Tax is required.", documentInfo._ID));
                }
                if (indexOptionDetail.RequireAmountSaleIndex && !documentInfo.SaleAmountSpecified)
                {
                    ValidationMessages.Add(string.Format("Document [{0}]: Sale Amount is required.", documentInfo._ID));
                }
                if (indexOptionDetail.RequireCityIndex)
                {
                    if (documentInfo.Cities == null)
                    {
                        ValidationMessages.Add(string.Format("Document [{0}]: Cities are required.", documentInfo._ID));
                    }
                    else if (documentInfo.Cities.Length == 0)
                    {
                        ValidationMessages.Add(string.Format("Document [{0}]: Cities are required.", documentInfo._ID));
                    }
                }
            }
            return ValidationMessages;
        }
        
        #endregion
    }
    /// <summary>
    /// Class that represents errors received by SECURE API
    /// </summary>
    public class Error
    {
        public string Message = string.Empty;
        public string MessageDetail = string.Empty;
        public string ExceptionMessage = string.Empty;
        public string ExceptionType = string.Empty;
        public string StackTrace = string.Empty;
    }
}

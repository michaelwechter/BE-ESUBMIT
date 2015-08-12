using SECURE.Common.Controller.Media.Schema.CodeGen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace WinFormTest
{
    public partial class TestForm : Form
    {
        #region Declarations

        private TestController _testController;
        private LoadingForm loadingform;

        private string defaultUserName = "test_359227";
        private string defaultPassword = "Secure12345";

        #endregion

        #region Constructor

        public TestForm()
        {
            InitializeComponent();
            this._testController = null;
            this.menuStripMain.Enabled = true;
            this.urlLabel.Text = string.Empty;
            this.tabMain.SelectedTab = this.tpHTTP;            
            this.ConfigureMenuItemTags();
            // Default login values
            this.SessionTokenLabel.Text = "NOT LOGGED IN";
            this.EnableMenus(false);
            this.Text = string.Format("{0} [{1}]", this.Text, Assembly.GetExecutingAssembly().GetName().Version);
        }

        #endregion


        #region HTTP Communication

        /// <summary>
        /// Perform HTTP operation to SECURE API
        /// </summary>
        /// <param name="mediaType">MediaType, enum describing the possible mediatypes</param>
        /// <param name="httpOP">HTTP, enum describing the possible HTTP operations</param>
        /// <param name="tsItem">ToolStripMenuItem, the menu item that was clicked</param>
        private async void PerformHTTP_Async(string mediaType, HTTP httpOP, ToolStripMenuItem tsItem)
        {
            try
            {
                // clear text boxes
                this.txtSubmitHTTP.Text = string.Empty;
                this.txtReceivedHTTP.Text = string.Empty;
                HttpContent content = null;
                string requestString = string.Empty;
                // get String array of API
                string[] apiName = this.GetAPINameFromToolStripMenuItem(tsItem);
                // get Parameters for selected API
                string[] parameters = this.GetParameterListFromToolStripMenuItem(tsItem);
                ArrayList nonUserParameters = new ArrayList();
                ArrayList userParameters = new ArrayList();
                ArrayList contentParameters = new ArrayList();
                // separate parameters into parameters that require user input
                //  and parameters that can be set from known data
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (TestController.TextInputParameters.Contains(parameters[i]))
                    {
                        userParameters.Add(parameters[i]);
                    }
                    else if (TestController.ContentParameters.Contains(parameters[i]))
                    {
                        contentParameters.Add(parameters[i]);
                    }
                    else
                    {
                        throw new Exception("Unknown API parameter");
                    }
                }
                DictionaryEntry[] apiParameters = new DictionaryEntry[userParameters.Count + nonUserParameters.Count];
                if (userParameters.Count > 0 || contentParameters.Count > 0)
                {
                    // Display form for input of parameters
                    string[] allInputParameters = null;
                    if (userParameters.Count > 0 && contentParameters.Count > 0)
                    {
                        allInputParameters = new string[userParameters.Count + contentParameters.Count];
                        ((string[])userParameters.ToArray(typeof(string))).CopyTo(allInputParameters, 0);
                        ((string[])contentParameters.ToArray(typeof(string))).CopyTo(allInputParameters, userParameters.Count);
                    }
                    else if (userParameters.Count == 0 && contentParameters.Count != 0)
                    {
                        allInputParameters = ((string[])contentParameters.ToArray(typeof(string)));
                    }
                    else if (userParameters.Count != 0 && contentParameters.Count == 0)
                    {
                        allInputParameters = ((string[])userParameters.ToArray(typeof(string)));
                    }
                    InputForm inputForm = new InputForm(allInputParameters);
                    DialogResult DR = inputForm.ShowDialog(this);
                    if (DR == System.Windows.Forms.DialogResult.OK)
                    {
                        DictionaryEntry[] userInputParameters = inputForm.Parameters;
                        ArrayList contentInputParameters = new ArrayList();
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (userParameters.Contains(parameters[i]))
                            {
                                foreach (DictionaryEntry dEntry in userInputParameters)
                                {
                                    if (dEntry.Key.ToString() == parameters[i])
                                    {
                                        apiParameters[i] = dEntry;
                                    }
                                }
                            }
                            else if (contentParameters.Contains(parameters[i]))
                            {
                                foreach (DictionaryEntry dEntry in userInputParameters)
                                {
                                    if (dEntry.Key.ToString() == parameters[i])
                                    {
                                        contentInputParameters.Add(dEntry);
                                    }
                                }
                            }
                        }
                        if (contentInputParameters.Count == 1)
                        {
                            requestString = ((DictionaryEntry)contentInputParameters[0]).Value.ToString();
                            content = new StringContent(
                                requestString, 
                                Encoding.UTF8, 
                                mediaType);
                        }
                        else if (contentInputParameters.Count > 1)
                        {
                            // This should not happen! No current API's take more than one XML parameter
                            throw new Exception("Multiple XML Parameters!");
                        }
                    }
                    else
                    {
                        // User hit cancel in InputForm
                        return;
                    }
                }
                //=======================================================================================================================================
                // display loadingbar form
                this.LoadingBarShow("Connecting to SECURE");
                // perform HTTP OP
                string responseText = await this._testController.SECURE_API_Async(mediaType, httpOP, apiName, apiParameters, content);
                // display HTTP Response
                this.txtReceivedHTTP.Text = TestController.FormatXML(responseText);
                           
                // special operations
                switch (tsItem.Name)
                {
                    case "logoutToolStripMenuItem":
                        {
                            this._testController.Session = null;
                            //this._testController.SessionTokenID = string.Empty;
                            this.SessionTokenLabel.Text = "NOT LOGGED IN";
                            this.EnableMenus(false);
                            //this.tabMain.SelectedTab = tpLogin;
                            //this.menuStripMain.Enabled = false;
                            break;
                        }
                }
                // refresh StatusStrip
                this.statusStrip1.Refresh();
                this.urlLabel.Text = string.Format("HTTP {0} | URL: {1}", this._testController.Last_HTTPstatus, this._testController.Last_URL);
                // display sent HTTP request
                string formattedRequestString = TestController.FormatXML(requestString);
                this.txtSubmitHTTP.Text = string.Format(content == null ? "{0}{1}" : "{0}\r\n\nContent:\r\n{1}",
                                this._testController.Last_HTTPResponse.RequestMessage.ToString(),
                                formattedRequestString);
                this.statusStrip2.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Stop displaying loading bar form
                this.LoadingBarHide();
            }
        }

        /// <summary>
        /// Log in to SECURE
        /// </summary>
        private async void PerformLoginAsync()
        {
            try
            {
                this.LoadingBarShow("Logging In");
                // clear text boxes
                this.txtSubmitHTTP.Text = string.Empty;
                this.txtReceivedHTTP.Text = string.Empty;

                // Show input box
                InputForm inputForm = new InputForm(
                    new DictionaryEntry[] 
                    { 
                        new DictionaryEntry("UserName", this.defaultUserName),
                        new DictionaryEntry("Password", this.defaultPassword)
                    });
                DialogResult DR = inputForm.ShowDialog(this);
                if (DR != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }
                DictionaryEntry[] userInputParameters = inputForm.Parameters;
                string userName = string.Empty;
                string password = string.Empty;
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
                }
                string responseText = await this._testController.LoginAsync(
                    userName,
                    password,
                    null,
                    true);
                this.menuStripMain.Enabled = true;
                string httpText = TestController.FormatXML(responseText);                           
                this.tabMain.SelectedTab = this.tpHTTP;
                this.txtReceivedHTTP.Text = TestController.FormatXML(responseText);
                this.txtSubmitHTTP.Text = this._testController.Last_HTTPResponse.RequestMessage.ToString();

                this.SessionTokenLabel.Text = (this._testController.SessionTokenID != string.Empty) ? ("SessionTokenID: " + this._testController.SessionTokenID) : this.SessionTokenLabel.Text;
                this.EnableMenus(this._testController.SessionTokenID != string.Empty);

                this.statusStrip1.Refresh();
                this.urlLabel.Text = string.Format("HTTP {0}{1} | URL: {2}",
                    this._testController.Last_HTTPstatus,
                    ((this._testController.Last_HTTPstatus == 500 || this._testController.Last_HTTPstatus == 404) ? ": Login Failed" : (this._testController.Last_HTTPstatus == 201 ? ": Need to change password" : string.Empty)),
                    this._testController.Last_URL);
                this.statusStrip2.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.LoadingBarHide();
            }
        }
      
        #endregion

        #region API Generation

        /// <summary>
        /// Recursively look at menu item to get api path
        /// </summary>
        /// <param name="tsItem">ToolStringItem, the menu item that was selected</param>
        /// <returns>array list, containing strings that make up the API path</returns>
        /// <remarks>returned array list is in reversed order</remarks>
        private ArrayList RecursiveNameMenu(ToolStripItem tsItem)
        {
            ArrayList menuNames = new ArrayList();
            if (tsItem.OwnerItem == null)
            {
                menuNames.Add(tsItem.Text);
            }
            else
            {
                menuNames.Add(tsItem.Text);
                foreach (string menuPart in this.RecursiveNameMenu(tsItem.OwnerItem))
                {
                    menuNames.Add(menuPart);
                }
            }
            return menuNames;
        }
        /// <summary>
        /// Recursively look at menu item to get parameters
        /// </summary>
        /// <param name="tsItem">ToolStringItem, the menu item that was selected</param>
        /// <returns>array list, containing string that contain the parameters for the selected api</returns>
        /// <remarks>returned array list is in reversed order</remarks>
        private ArrayList RecursiveParameterMenu(ToolStripItem tsItem)
        {
            ArrayList menuParameters = new ArrayList();
            if (tsItem.OwnerItem == null)
            {
                if (tsItem.Tag != null)
                {
                    if (tsItem.Tag.GetType() == typeof(string))
                    {
                        menuParameters.Add(tsItem.Tag);
                    }
                    else if (tsItem.Tag.GetType() == typeof(string[]))
                    {
                        string[] paramAR = (string[])tsItem.Tag;
                        foreach (string param in paramAR)
                        {
                            menuParameters.Add(param);
                        }
                    }
                }
            }
            else
            {
                if (tsItem.Tag != null)
                {
                    if (tsItem.Tag.GetType() == typeof(string))
                    {
                        menuParameters.Add(tsItem.Tag);
                    }
                    else if (tsItem.Tag.GetType() == typeof(string[]))
                    {
                        string[] paramAR = (string[])tsItem.Tag;
                        foreach (string param in paramAR)
                        {
                            menuParameters.Add(param);
                        }
                    }
                }
                foreach (string menuPart in this.RecursiveParameterMenu(tsItem.OwnerItem))
                {
                    menuParameters.Add(menuPart);
                }
            }
            return menuParameters;
        }
        /// <summary>
        /// Generate path of selected API
        /// </summary>
        /// <param name="tsItem">ToolStringItem, the menu item that was selected</param>
        /// <returns>string array, containing the api path of the menu item</returns>
        private string[] GetAPINameFromToolStripMenuItem(ToolStripItem tsItem)
        {
            ArrayList menuPath = this.RecursiveNameMenu(tsItem);
            menuPath.Reverse();
            return (string[])menuPath.ToArray(typeof(string));
        }
        /// <summary>
        /// Generate list of parameter names for selected API
        /// </summary>
        /// <param name="tsItem">ToolStringItem, the menu item that was selected</param>
        /// <returns>string array, containing the parameters of the selected api</returns>
        private string[] GetParameterListFromToolStripMenuItem(ToolStripMenuItem tsItem)
        {
            ArrayList menuParams = this.RecursiveParameterMenu(tsItem);
            menuParams.Reverse();
            return (string[])menuParams.ToArray(typeof(string));
        }

        #endregion

        #region Menu Item event methods
        
        /// <summary>
        /// Event method for when a menu item with an HTTP GET API method is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HTTPGET_ToolStipMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                this.PerformHTTP_Async(Properties.Resources.MediaType_XML, HTTP.GET, (ToolStripMenuItem)sender);
            }
        }
        /// <summary>
        /// Event method for when a menu item with an HTTP PUT API method is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HTTPPUT_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                this.PerformHTTP_Async(Properties.Resources.MediaType_XML, HTTP.PUT, (ToolStripMenuItem)sender);
            }
        }
        /// <summary>
        /// Event method for when a menu item with an HTTP DELETE API method is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HTTPDELETE_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                this.PerformHTTP_Async(Properties.Resources.MediaType_XML, HTTP.DELETE, (ToolStripMenuItem)sender);
            }
        }
        /// <summary>
        /// Event method for when a menu item with an HTTP POST API method is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HTTPPPOST_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                this.PerformHTTP_Async(Properties.Resources.MediaType_XML, HTTP.POST, (ToolStripMenuItem)sender);
            }
        }

        #endregion

        #region Loading Bar

        /// <summary>
        /// Display the loading bar form
        /// </summary>
        /// <param name="displayText">string, containing text to display on the loading bar form</param>
        private void LoadingBarShow(string displayText)
        {
            this.loadingform = new LoadingForm(displayText);
            this.loadingform.DisplayText = displayText;
            this.Enabled = false;
            this.loadingform.Show(this);
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
                //this.loadingform.Dispose();
                //this.loadingform = null;
            }
            this.Enabled = true;
            this.Focus();
        }

        #endregion

        #region UI Control
        
        /// <summary>
        /// Event method for when the form loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestForm_Load(object sender, EventArgs e)
        {
            this.LoadingBarHide();
            this.lblStatusMediaType.Text = string.Format("MediaType:[{0}]", Properties.Resources.MediaType_XML);
            this.statusStrip1.Refresh();
        }
        /// <summary>
        /// Event method for switching tabs. Prevent switching depending on existence of SessionTokenID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == this.tpSubmitter)
            {
                
            }
            else if (e.TabPage == this.tpCounty)
            {

            }
        }
        /// <summary>
        ///  Configure enabled menus
        /// </summary>
        /// <param name="loggedIn"></param>
        private void EnableMenus(bool loggedIn)
        {
            foreach (ToolStripMenuItem tsItem in this.menuStripMain.Items)
            {
                if (tsItem.Text == "Authentication")
                {
                    tsItem.Enabled = true;
                    foreach (ToolStripMenuItem tsSubItem in tsItem.DropDownItems)
                    {
                        if (tsSubItem.Text == "Login")
                        {
                            tsSubItem.Enabled = true;
                        }
                        else
                        {
                            tsSubItem.Enabled = loggedIn;
                        }
                    }
                }
                else
                {
                    tsItem.Enabled = loggedIn;
                }
            }
        }
        /// <summary>
        /// Configure multiple tags for menu items
        /// </summary>
        private void ConfigureMenuItemTags()
        {
            this.changePasswordToolStripMenuItem.Tag = new string[] { "NewPassword", "OldPassword" };
            this.readByUserIDStatusCodeDateRangeToolStripMenuItem.Tag = new string[] { "EndDate", "StartDate", "StatusCode", "UserName" };
            this.readByStatusCodeDateRangeToolStripMenuItem.Tag = new string[] { "EndDate", "StartDate", "StatusCode" };
            this.deleteToolStripMenuItem.Tag = new string[] { "EditTS", "BatchID" };
            this.checkInToolStripMenuItem.Tag = new string[] { "BatchStatus", "EditTS", "BatchID" };
            this.checkOutToolStripMenuItem.Tag = new string[] { "BatchStatus", "EditTS", "BatchID" };
            this.overrideCheckOutToolStripMenuItem.Tag = new string[] { "BatchStatus", "EditTS", "BatchID" };
            this.uploadCompleteToolStripMenuItem.Tag = new string[] { "BatchStatus", "EditTS", "BatchID" };
            this.readByBatchIDSequenceToolStripMenuItem.Tag = new string[] { "BatchID", "Sequence" };
            this.loginToolStripMenuItem.Tag = new string[] { "Password", "UserName" };
        }
        
        #endregion

        #region Button event methods

        /// <summary>
        /// Event method for when the clear button is clicked to send XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearSend_Click(object sender, EventArgs e)
        {
            this.txtSubmitHTTP.Text = string.Empty;
        }
        /// <summary>
        /// Event method for when the Clear button is clicked for recevied XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearReceived_Click(object sender, EventArgs e)
        {
            this.txtReceivedHTTP.Text = string.Empty;
        }
           
        #endregion    
              
        #region Other event methods

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PerformLoginAsync();
        }

        private void tabMain_Deselecting(object sender, TabControlCancelEventArgs e)
        {            
            try
            {
                if (e.TabPage == this.tpSubmitter)
                {
                    this.txtSubmitHTTP.Text = this.submitterPanel1.RequestMessageString;
                    this.txtReceivedHTTP.Text = this.submitterPanel1.ResponseMessageString;
                }
                else if (e.TabPage == this.tpCounty)
                {
                    this.txtSubmitHTTP.Text = this.countyPanel1.RequestMessageString;
                    this.txtReceivedHTTP.Text = this.countyPanel1.ResponseMessageString;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Event method for when the tab is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

     
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormTest.ParamInputControls;
using WinFormTest.ClientControls;
using System.IO;

namespace WinFormTest
{
    public partial class CacheViewerForm : Form
    {
        private Hashtable _DataCache;
        private ArrayList _DataTypeList = new ArrayList();
        /// <summary>
        /// HashTable of the current data and the string representation of the data object displayed in the listbox
        /// </summary>
        private Hashtable _currentData;
        /// <summary>
        /// Constructor
        /// </summary>
        public CacheViewerForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cacheDatas">array of cache data from sECURE</param>
        public CacheViewerForm(params object[] cacheDatas):this()
        {
            this._DataCache = new Hashtable(new Dictionary<Type, object[]>());
            foreach (object cacheData in cacheDatas)
            {
                if (cacheData != null)
                {
                    Type cacheDataType = cacheData.GetType();
                    if (cacheDataType == typeof(IndexOptionDetail))
                    {
                        IndexOptionDetail indexOptionDetail = (IndexOptionDetail)cacheData;
                        this._DataTypeList.Add(typeof(IndexOptionDetail));
                        this._DataCache.Add(typeof(IndexOptionDetail), new IndexOptionDetail[] { indexOptionDetail });
                    }
                    else if (cacheDataType == typeof(IndexOptionDetail[]))
                    {
                        IndexOptionDetail[] indexOptionDetail = (IndexOptionDetail[])cacheData;
                        this._DataTypeList.Add(typeof(IndexOptionDetail));
                        this._DataCache.Add(typeof(IndexOptionDetail), indexOptionDetail);
                    }
                    else if (cacheDataType == typeof(CountiesInfo))
                    {
                        CountiesInfo countiesInfo = (CountiesInfo)cacheData;
                        this._DataTypeList.Add(typeof(CountyInfo));
                        this._DataCache.Add(typeof(CountyInfo), countiesInfo.County);
                    }
                    else if (cacheDataType == typeof(CitiesDetail))
                    {
                        CitiesDetail citiesDetail = (CitiesDetail)cacheData;
                        this._DataTypeList.Add(typeof(CityDetail));
                        this._DataCache.Add(typeof(CityDetail), citiesDetail.City);
                    }
                    else if (cacheDataType == typeof(ProcessQueuesDetail))
                    {
                        ProcessQueuesDetail processQueueDetail = (ProcessQueuesDetail)cacheData;
                        this._DataTypeList.Add(typeof(ProcessQueueDetail));
                        this._DataCache.Add(typeof(ProcessQueueDetail), processQueueDetail.ProcessQueue);
                    }
                    else if (cacheDataType == typeof(TitlesDetail))
                    {
                        TitlesDetail titleDetail = (TitlesDetail)cacheData;
                        this._DataTypeList.Add(typeof(TitleDetail));
                        this._DataCache.Add(typeof(TitleDetail), titleDetail.Title);
                    }
                    else if (cacheDataType == typeof(RequestingPartiesInfo))
                    {
                        RequestingPartiesInfo requestingParty = (RequestingPartiesInfo)cacheData;
                        this._DataTypeList.Add(typeof(RequestingPartyInfo));
                        this._DataCache.Add(typeof(RequestingPartyInfo), requestingParty.RequestingParty);
                    }
                    else if (cacheDataType == typeof(SubmittingPartiesInfo))
                    {
                        SubmittingPartiesInfo submittingPartiesInfo = (SubmittingPartiesInfo)cacheData;
                        this._DataTypeList.Add(typeof(SubmittingPartyInfo));
                        this._DataCache.Add(typeof(SubmittingPartyInfo), submittingPartiesInfo.SubmittingParty);
                    }
                }
            }
            this.PopulateDataType();
            
        }
        /// <summary>
        /// Populate the listbox with the data
        /// </summary>
        /// <param name="selectedType"></param>
        private void PopulateDataIndex(Type selectedType)
        {
            this.lstDataIndex.Items.Clear();
            this.lstDataIndex.SelectedItem = null;
            this._currentData = new Hashtable();
            object cacheArray = this._DataCache[selectedType];
            try
            {
                foreach (object dataItem in (object[])cacheArray)
                {
                    string title = string.Empty;
                    if (selectedType == typeof(IndexOptionDetail))
                    {
                        title = ((IndexOptionDetail)dataItem)._CountyID;
                    }
                    else if (selectedType == typeof(CountyInfo))
                    {
                        title = ((CountyInfo)dataItem)._ID + ": " + ((CountyInfo)dataItem).Name;
                    }
                    else if (selectedType == typeof(CityDetail))
                    {
                        title = ((CityDetail)dataItem)._ID + ": " +((CityDetail)dataItem).Description;
                    }
                    else if (selectedType == typeof(ProcessQueueDetail))
                    {
                        title = ((ProcessQueueDetail)dataItem)._ID + ": " + ((ProcessQueueDetail)dataItem).Name;
                    }
                    else if (selectedType == typeof(TitleDetail))
                    {
                        title = ((TitleDetail)dataItem)._ID + ": " + ((TitleDetail)dataItem).Description;
                    }
                    else if (selectedType == typeof(RequestingPartyInfo))
                    {
                        title = ((RequestingPartyInfo)dataItem)._ID + ": " + ((RequestingPartyInfo)dataItem).Name;
                    }
                    else if (selectedType == typeof(SubmittingPartyInfo))
                    {
                        title = ((SubmittingPartyInfo)dataItem)._ID + ": " + ((SubmittingPartyInfo)dataItem).Name;
                    }
                    this.lstDataIndex.Items.Add(title);
                    this._currentData.Add(title, dataItem);
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Populate the combo box with the datatypes to display
        /// </summary>
        private void PopulateDataType()
        {
            foreach (Type type in this._DataTypeList)
            {
                this.cmbDataType.Items.Add(type);
            }
        }
        /// <summary>
        /// event method for when the selected item in the data type combo box is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// event method for when the selected item in the data type combo box is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDataType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.cmbDataType.SelectedItem != null)
            {
                Type selectedType = (Type)this.cmbDataType.SelectedItem;
                this.PopulateDataIndex(selectedType);
                this.flpDataContents.Controls.Clear();
            }
        }
        /// <summary>
        /// Event method for when the selection within a listbox changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDataIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstDataIndex.SelectedItem != null)
            {
                string title = this.lstDataIndex.SelectedItem.ToString();
                object cachedData = this._currentData[title];
                this.flpDataContents.Visible = false;
                this.flpDataContents.Controls.Clear();
                Type selectedType = cachedData.GetType();
                if (selectedType == typeof(IndexOptionDetail))
                {
                    IndexOptionDetail indexOptionDetail = ((IndexOptionDetail)cachedData);
                    this.flpDataContents.Controls.AddRange(new Control[]
                    {
                        new IndexOptionDetailControl()
                        { 
                            IndexOptionDetail = indexOptionDetail
                        }
                    });
                }
                else if (selectedType == typeof(CountyInfo))
                {
                    CountyInfo countyInfo = ((CountyInfo)cachedData);
                    this.flpDataContents.Controls.AddRange(new Control[]
                    {
                        new CountyInfoControl()
                        {
                            CountyInfo = countyInfo
                        },
                        new PictureBox()
                        {
                            Image = this.GetImageFromCounty(countyInfo),
                            SizeMode = PictureBoxSizeMode.AutoSize
                        }
                    });
                }
                else if (selectedType == typeof(CityDetail))
                {
                    CityDetail cityDetail = ((CityDetail)cachedData);
                    this.flpDataContents.Controls.AddRange(new Control[]
                    {
                        new CityDetailControl()
                        { 
                            CityDetail = cityDetail
                        }
                    });
                }
                else if (selectedType == typeof(ProcessQueueDetail))
                {
                    ProcessQueueDetail pqDetail = ((ProcessQueueDetail)cachedData);
                    this.flpDataContents.Controls.AddRange(new Control[]
                    {
                        new ProcessQueueDetailControl()
                        {
                            ProcessQueueDetail = pqDetail
                        }
                    });
                }
                else if (selectedType == typeof(TitleDetail))
                {
                    TitleDetail titleDetail = ((TitleDetail)cachedData);
                    this.flpDataContents.Controls.AddRange(new Control[]
                    {
                        new TitleDetailControl(){ TitleDetail = titleDetail}
                    });
                }
                else if (selectedType == typeof(RequestingPartyInfo))
                {
                    RequestingPartyInfo rpInfo = ((RequestingPartyInfo)cachedData);
                    this.flpDataContents.Controls.AddRange(new Control[]
                    {
                        new RequestingPartyInfoControl()
                        { 
                            RequestingPartyInfo = rpInfo
                        }
                    });
                }
                else if (selectedType == typeof(SubmittingPartyInfo))
                {
                    SubmittingPartyInfo spInfo = ((SubmittingPartyInfo)cachedData);
                    this.flpDataContents.Controls.AddRange(new Control[]
                    {
                        new SubmittingPartyInfoControl()
                        {
                            SubmittingPartyInfo = spInfo
                        }
                    });
                }
                foreach (Control control in this.flpDataContents.Controls)
                {
                    control.Width = this.flpDataContents.Width - 10;
                    this.RecursiveReadOnlySet(control);
                }
                this.flpDataContents.Visible = true;
            }
        }
        /// <summary>
        /// recursively set readonly all controls within a control
        /// </summary>
        /// <param name="control">Control, containing other controls</param>
        private void RecursiveReadOnlySet(Control control)
        {
            if (control.Controls != null)
            {
                foreach (Control innerControl in control.Controls)
                {
                    this.RecursiveReadOnlySet(innerControl);
                }
            }
            if (control is TextBox)
            {
                ((TextBox)control).ReadOnly = true;
            }
            else if (control is CheckBox)
            {
                ((CheckBox)control).Enabled = false;
            }
        }

        private Image GetImageFromCounty(CountyInfo countyInfo)
        {
            try
            {
                if (countyInfo.Logo != null && countyInfo.Logo != string.Empty)
                {
                    string logoBase64DataOnly = countyInfo.Logo.Remove(
                        countyInfo.Logo.IndexOf("data:image/"),
                        ("data:image:" + "xxx" + ";base64,").Length);
                    return Image.FromStream(
                        new MemoryStream(
                            Convert.FromBase64String(
                            logoBase64DataOnly)));
                }
                else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}

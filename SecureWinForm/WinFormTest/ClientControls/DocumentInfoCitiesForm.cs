using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WinFormTest.ClientControls
{
    public partial class DocumentInfoCitiesForm : Form
    {

        #region Declarations
        
        public List<CityDetail> AllCities = new List<CityDetail>();
        public List<CityDetail> CurrentCities = new List<CityDetail>();

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="allCities">List of CityDetail, all cities available to be used</param>
        /// <param name="currentCities">List of CityDetail, cities associated with document</param>
        public DocumentInfoCitiesForm(List<CityDetail> allCities, List<CityDetail> currentCities)
            : this()
        {
            this.AllCities = new List<CityDetail>(allCities);
            this.CurrentCities = currentCities;
            this.btnAdd.Enabled = false;
            this.btnRemove.Enabled = false;
            this.UpdateLists();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentInfoCitiesForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Update ListBox containing document's cities
        /// </summary>
        private void UpdateSelectedList()
        {
            this.lstSelected.DataSource = null;
            this.lstSelected.DataSource = CurrentCities;
            this.lstSelected.DisplayMember = "Description";
            this.btnRemoveAll.Enabled = this.lstSelected.Items.Count > 0;
            this.lstSelected.SelectedItem = null;
        }
        /// <summary>
        /// Update ListBox containing all available cities
        /// </summary>
        private void UpdateOptionsList()
        {
            this.lstOptions.DataSource = null;
            this.lstOptions.DataSource = AllCities;
            this.lstOptions.DisplayMember = "Description";
            this.btnAddAll.Enabled = this.lstOptions.Items.Count > 0;
            this.lstOptions.SelectedItem = null;
        }
        /// <summary>
        /// Update both ListBoxes
        /// </summary>
        private void UpdateLists()
        {
            this.UpdateSelectedList();
            this.UpdateOptionsList();
        }

        #endregion
        
        #region Events

        /// <summary>
        /// Selection changed event for ListBox containing the document's current cities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnRemove.Enabled = (this.lstSelected.SelectedItem != null);
            this.ctrlCityDetailSelected.CityDetail = (CityDetail)this.lstSelected.SelectedValue;
        }
        
        /// <summary>
        /// Selection changed event for ListBox containing the document's possible cities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = (this.lstOptions.SelectedItem != null);
            this.ctrlCityDetailOptions.CityDetail = (CityDetail)this.lstOptions.SelectedValue;
        }

        /// <summary>
        /// Click event for Add All button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAll_Click(object sender, EventArgs e)
        {
            foreach (CityDetail cd in AllCities)
            {
                CityDetail findCity = this.CurrentCities.Find(r => r._ID == cd._ID);
                if (findCity == null)
                {
                    cd.ActionCode = ActionCode.New;
                    cd.CreateTimeStamp = System.DateTime.Now;
                    cd.EditTimeStamp = System.DateTime.Now;
                    this.CurrentCities.Add(cd);
                }
                else
                {
                    if (cd._ID == findCity._ID)
                    {
                        findCity.ActionCode = findCity.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.None;
                    }
                }
            }
            this.UpdateLists();
        }
     
        /// <summary>
        /// Click event for Add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CityDetail findCity = this.CurrentCities.Find(r => r._ID == ((CityDetail)this.lstOptions.SelectedValue)._ID);
            if (findCity == null)
            {
                ((CityDetail)this.lstOptions.SelectedValue).ActionCode = ActionCode.New;
                ((CityDetail)this.lstOptions.SelectedValue).CreateTimeStamp = System.DateTime.Now;
                ((CityDetail)this.lstOptions.SelectedValue).EditTimeStamp = System.DateTime.Now;
                this.CurrentCities.Add((CityDetail)lstOptions.SelectedValue);
            }
            else
            {
                if (findCity.ActionCode == ActionCode.Delete)
                {
                    findCity.ActionCode = ActionCode.None;
                }
            }
            this.UpdateLists();
        }
      
        /// <summary>
        /// Click event for Remove button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (((CityDetail)this.lstSelected.SelectedItem).ActionCode == ActionCode.New)
            {
                // remove from list
                this.CurrentCities.Remove(((CityDetail)this.lstSelected.SelectedItem));
            }
            else
            {
                ((CityDetail)this.lstSelected.SelectedItem).ActionCode = ActionCode.Delete;
            }
            this.UpdateLists();
        }
      
        /// <summary>
        /// Click event for Remove All button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            ArrayList removalList = new ArrayList();
            foreach (CityDetail cd in this.CurrentCities)
            {
                if (cd.ActionCode != ActionCode.New)
                {
                    cd.ActionCode = ActionCode.Delete;
                }
                else
                {
                    removalList.Add(cd);
                }
            }
            if (removalList.Count > 0)
            {
                foreach (CityDetail rmCityDetail in removalList)
                {
                    this.CurrentCities.Remove(rmCityDetail);
                }
            }
            this.UpdateLists();
        }
        
        #endregion
    }
}

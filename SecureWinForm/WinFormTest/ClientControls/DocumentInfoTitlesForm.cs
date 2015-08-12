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
    public partial class DocumentInfoTitlesForm : Form
    {

        #region Declarations
        /// <summary>
        /// List of all available Titles
        /// </summary>
        public List<TitleDetail> AllTitles = new List<TitleDetail>();
        /// <summary>
        /// List of document's Titles
        /// </summary>
        public List<TitleDetail> CurrentTitles = new List<TitleDetail>();

        #endregion

        #region Properties

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="allTitles">List of all available Titles</param>
        /// <param name="currentTitles">List of document's Titles</param>
        public DocumentInfoTitlesForm(List<TitleDetail> allTitles, List<TitleDetail> currentTitles) : this()
        {
            this.AllTitles = new List<TitleDetail>(allTitles);
            this.CurrentTitles = currentTitles;
            this.btnAdd.Enabled = false;
            this.btnRemove.Enabled = false;
            this.UpdateLists();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentInfoTitlesForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Update ListBox of document's Titles
        /// </summary>
        private void UpdateSelectedList()
        {
            this.lstSelected.DataSource = null;
            this.lstSelected.DataSource = CurrentTitles;
            this.lstSelected.DisplayMember = "Description";
            this.btnRemoveAll.Enabled = this.lstOptions.Items.Count > 0;
            this.lstSelected.SelectedItem = null;
        }

        /// <summary>
        /// Update ListBox of available Titles
        /// </summary>
        private void UpdateOptionsList()
        {
            this.lstOptions.DataSource = null;
            this.lstOptions.DataSource = AllTitles;
            this.lstOptions.DisplayMember = "Description";
            this.btnAddAll.Enabled = this.lstOptions.Items.Count > 0;
            this.lstOptions.SelectedItem = null;
        }

        /// <summary>
        /// Update ListBoxes
        /// </summary>
        private void UpdateLists()
        {
            this.UpdateSelectedList();
            this.UpdateOptionsList();
        }

        /// <summary>
        /// Get the next number to use for Sequence
        /// </summary>
        /// <returns></returns>
        private int GetNextSequenceNumber()
        {
            int[] titleSequence = new int[this.CurrentTitles.Count];
            for (int x = 0; x < titleSequence.Length; x++)
            {
                titleSequence[x] = this.CurrentTitles[x].Sequence;
            }

            for (int i = 0; i < titleSequence.Length; i++)
            {
                for (int j = i + 1; j < titleSequence.Length; j++)
                {
                    if (titleSequence[j] < titleSequence[i])
                    {
                        int temp = titleSequence[j];
                        titleSequence[j] = titleSequence[i];
                        titleSequence[i] = temp;
                    }
                }
            }
            int nextSequence = 0;
            foreach (int docSeq in titleSequence)
            {
                if (docSeq == nextSequence)
                {
                    nextSequence += 1;
                }
            }
            return nextSequence;
        }

        #endregion

        #region Events

        /// <summary>
        /// Selection changed event for ListBox of Document's Titles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnRemove.Enabled = this.lstSelected.SelectedItem != null;
            this.ctrlTitleDetailSelected.TitleDetail = (TitleDetail)this.lstSelected.SelectedValue;
        }

        /// <summary>
        /// Selection changed event for ListBox of available Titles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnAdd.Enabled = this.lstOptions.SelectedItem != null;
            this.ctrlTitleDetailOptions.TitleDetail = (TitleDetail)this.lstOptions.SelectedValue;
        }

        /// <summary>
        /// Click event for Add All buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAll_Click(object sender, EventArgs e)
        {
            foreach (TitleDetail td in this.AllTitles)
            {
                TitleDetail findTitle = this.CurrentTitles.Find(r => r._ID == td._ID);
                if (findTitle == null)
                {
                    this.CurrentTitles.Add(new TitleDetail()
                    {
                        ActionCode = ActionCode.New,
                        CreateTimeStamp = System.DateTime.Now,
                        CreateTimeStampSpecified = true,
                        EditTimeStamp = System.DateTime.Now,
                        Sequence = this.GetNextSequenceNumber(),
                        SequenceSpecified = true,
                        _ID = td._ID,
                        Code = td.Code,
                        Description = td.Description,
                        _CountyID = td._CountyID
                    });
                }
                else
                {
                    if (td._ID == findTitle._ID)
                    {
                        findTitle.ActionCode = findTitle.ActionCode == ActionCode.New ? ActionCode.New : ActionCode.None;
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
            TitleDetail findTitle = this.CurrentTitles.Find(r => r._ID == ((TitleDetail)this.lstOptions.SelectedValue)._ID);
            if (findTitle == null)
            {
                this.CurrentTitles.Add(new TitleDetail()
                {
                    ActionCode = ActionCode.New,
                    CreateTimeStamp = System.DateTime.Now,
                    CreateTimeStampSpecified = true,
                    EditTimeStamp = System.DateTime.Now,
                    Sequence = this.GetNextSequenceNumber(),
                    SequenceSpecified = true,
                    _ID = ((TitleDetail)this.lstOptions.SelectedValue)._ID,
                    Code = ((TitleDetail)this.lstOptions.SelectedValue).Code,
                    Description = ((TitleDetail)this.lstOptions.SelectedValue).Description,
                    _CountyID = ((TitleDetail)this.lstOptions.SelectedValue)._CountyID
                });
            }
            else
            {
                if (findTitle.ActionCode == ActionCode.Delete)
                {
                    findTitle.ActionCode = ActionCode.None;
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
            if (((TitleDetail)this.lstSelected.SelectedItem).ActionCode == ActionCode.New)
            {
                this.CurrentTitles.Remove(((TitleDetail)this.lstSelected.SelectedItem));
            }
            else
            {
                ((TitleDetail)this.lstSelected.SelectedItem).ActionCode = ActionCode.Delete;
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
            foreach (TitleDetail td in this.CurrentTitles)
            {
                if (td.ActionCode != ActionCode.New)
                {
                    td.ActionCode = ActionCode.Delete;
                }
                else
                {
                    removalList.Add(td);
                }
            }
            if (removalList.Count > 0)
            {
                foreach (TitleDetail rmTitleDetail in removalList)
                {
                    this.CurrentTitles.Remove(rmTitleDetail);
                }
            }
            this.UpdateLists();
        }

        #endregion
    }
}

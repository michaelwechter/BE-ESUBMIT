using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest
{
    public partial class ProcessQueueDetailControl : UserControl
    {

        #region Declarations

        private ProcessQueueDetail _ProcessQueueDetail;
        public ProcessQueueDetail ProcessQueueDetail
        {
            get
            {
                return this._ProcessQueueDetail;
            }
            set
            {
                this._ProcessQueueDetail = value;
                if (this._ProcessQueueDetail == null)
                {
                    this._ProcessQueueDetail = new ProcessQueueDetail();
                    this.txtID.Text = string.Empty;
                    this.txtCountyID.Text = string.Empty;
                    this.txtName.Text = string.Empty;
                    this.txtDescription.Text = string.Empty;
                    this.txtOpenTime.Text = string.Empty;
                    this.txtCloseTime.Text = string.Empty;
                }
                else
                {
                    this.txtID.Text = this._ProcessQueueDetail._ID;
                    this.txtCountyID.Text = this._ProcessQueueDetail._CountyID;
                    this.txtName.Text = this._ProcessQueueDetail.Name;
                    this.txtDescription.Text = this._ProcessQueueDetail.Description;
                    this.txtOpenTime.Text = this._ProcessQueueDetail.OpenTimeSpecified ? this._ProcessQueueDetail.OpenTime.ToString() : string.Empty;
                    this.txtCloseTime.Text = this._ProcessQueueDetail.CloseTimeSpecified ? this._ProcessQueueDetail.CloseTime.ToString() : string.Empty;
                }
            }
        }

        #endregion

        #region Properties

        public void EnableID(bool state)
        {
            this.txtID.Enabled = state;
        }
        public void EnableCountyID(bool state)
        {
            this.txtCountyID.Enabled = state;
        }
        public void EnableName(bool state)
        {
            this.txtName.Enabled = state;
        }
        public void EnableDescription(bool state)
        {
            this.txtDescription.Enabled = state;
        }
        public void EnableOpenTime(bool state)
        {
            this.txtOpenTime.Enabled = state;
        }
        public void EnableCloseTime(bool state)
        {
            this.txtCloseTime.Enabled = state;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessQueueDetailControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// TextChanged event for ID TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtID_TextChanged(object sender, EventArgs e)
        {
            int anInt;
            if (int.TryParse(this.txtID.Text, out anInt))
            {
                this._ProcessQueueDetail._ID = this.txtID.Text;
            }
            else
            {
                this.txtID.Text = string.Empty;
            }
        }

        /// <summary>
        /// TextChanged event for County ID TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCountyID_TextChanged(object sender, EventArgs e)
        {
            int anInt;
            if (int.TryParse(this.txtCountyID.Text, out anInt))
            {
                this._ProcessQueueDetail._CountyID = this.txtCountyID.Text;
            }
            else
            {
                this.txtCountyID.Text = string.Empty;
            }
        }

        /// <summary>
        /// TextChanged event for Name TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this._ProcessQueueDetail.Name = this.txtName.Text;
        }

        /// <summary>
        /// TextChanged event for Description TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            this._ProcessQueueDetail.Description = this.txtDescription.Text;
        }

        #endregion
    }
}

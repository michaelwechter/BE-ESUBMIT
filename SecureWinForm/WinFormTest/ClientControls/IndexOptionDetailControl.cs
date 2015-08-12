using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest.ClientControls
{
    public partial class IndexOptionDetailControl : UserControl
    {
        #region Declarations

        private IndexOptionDetail _IndexOptionDetail;
        public IndexOptionDetail IndexOptionDetail
        {
            get
            {
                return this._IndexOptionDetail;
            }
            set
            {
                this._IndexOptionDetail = value;
                if (_IndexOptionDetail == null)
                {
                    this._IndexOptionDetail = new IndexOptionDetail();
                    this.txtCountyID.Text = string.Empty;
                    this.chkEnableTitleIndex.Checked = false;
                    this.chkEnableNamesIndex.Checked = false;
                    this.chkEnableAPNIndex.Checked = false;
                    this.chkEnableTransferTaxIndex.Checked = false;
                    this.chkEnableAmountSaleIndex.Checked = false;
                    this.chkEnableCityIndex.Checked = false;
                    this.chkEnableConcurrentIndex.Checked = false;
                    this.chkEnableRequestingParty.Checked = false;
                    this.chkRequireTitleIndex.Checked = false;
                    this.chkRequireNamesIndex.Checked = false;
                    this.chkRequireAPNIndex.Checked = false;
                    this.chkRequireTransferTaxIndex.Checked = false;
                    this.chkRequireAmountSaleIndex.Checked = false;
                    this.chkRequireCityIndex.Checked = false;
                    this.chkRequireConcurrentIndex.Checked = false;
                    this.chkRequireRequestingParty.Checked = false;
                }
                else
                {
                    this.txtCountyID.Text = _IndexOptionDetail._CountyID.ToString();
                    this.chkEnableTitleIndex.Checked        = this._IndexOptionDetail.EnableTitleIndex;
                    this.chkEnableNamesIndex.Checked        = this._IndexOptionDetail.EnableNamesIndex;
                    this.chkEnableAPNIndex.Checked          = this._IndexOptionDetail.EnableAPNIndex;
                    this.chkEnableTransferTaxIndex.Checked  = this._IndexOptionDetail.EnableTransferTaxIndex;
                    this.chkEnableAmountSaleIndex.Checked   = this._IndexOptionDetail.EnableAmountSaleIndex;
                    this.chkEnableCityIndex.Checked         = this._IndexOptionDetail.EnableCityIndex;
                    this.chkEnableConcurrentIndex.Checked   = this._IndexOptionDetail.EnableConcurrentIndex;
                    this.chkEnableRequestingParty.Checked   = this._IndexOptionDetail.EnableRequestingParty;
                    this.chkRequireTitleIndex.Checked       = this._IndexOptionDetail.RequireTitleIndex;
                    this.chkRequireNamesIndex.Checked       = this._IndexOptionDetail.RequireNamesIndex;
                    this.chkRequireAPNIndex.Checked         = this._IndexOptionDetail.RequireAPNIndex;
                    this.chkRequireTransferTaxIndex.Checked = this._IndexOptionDetail.RequireTransferTaxIndex;
                    this.chkRequireAmountSaleIndex.Checked  = this._IndexOptionDetail.RequireAmountSaleIndex;
                    this.chkRequireCityIndex.Checked        = this._IndexOptionDetail.RequireCityIndex;
                    this.chkRequireConcurrentIndex.Checked  = this._IndexOptionDetail.RequireConcurrentIndex;
                    this.chkRequireRequestingParty.Checked  = this._IndexOptionDetail.RequireRequestingParty;
                }
            }
        }

        #endregion

        #region Properties

        public void EnableCountyID(bool state)
        {
            this.txtCountyID.Enabled = state;
        }
        public void EnableEnableTitleIndex(bool state)
        {
            this.chkEnableTitleIndex.Enabled = state;
        }
        public void EnableEnableNamesIndex(bool state)
        {
            this.chkEnableNamesIndex.Enabled = state;
        }
        public void EnableEnableAPNIndex(bool state)
        {
            this.chkEnableAPNIndex.Enabled = state;
        }
        public void EnableEnableTransferTaxIndex(bool state)
        {
            this.chkEnableTransferTaxIndex.Enabled = state;
        }
        public void EnableEnableAmountSaleIndex(bool state)
        {
            this.chkEnableAmountSaleIndex.Enabled = state;
        }
        public void EnableEnableCityIndex(bool state)
        {
            this.chkEnableCityIndex.Enabled = state;
        }
        public void EnableEnableConcurrentIndex(bool state)
        {
            this.chkEnableConcurrentIndex.Enabled = state;
        }
        public void EnableEnableRequestingParty(bool state)
        {
            this.chkEnableRequestingParty.Enabled = state;
        }


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public IndexOptionDetailControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// TextChanged event for County ID TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCountyID_TextChanged(object sender, EventArgs e)
        {
            int anInt;
            if (int.TryParse(txtCountyID.Text, out anInt))
            {
                this._IndexOptionDetail._CountyID = this.txtCountyID.Text;
            }
            else
            {
                this.txtCountyID.Text = string.Empty;
            }
        }
        
        /// <summary>
        /// CheckChanged event for Title Index CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableTitleIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.EnableTitleIndex = this.chkEnableTitleIndex.Checked;
        }

        /// <summary>
        /// CheckChanged event for Names Index CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableNamesIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.EnableNamesIndex = this.chkEnableNamesIndex.Checked;
        }

        /// <summary>
        /// CheckChanged event for APN Index CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableAPNIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.EnableAPNIndex = this.chkEnableAPNIndex.Checked;
        }

        /// <summary>
        /// CheckChanged event for TransferTax Index CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableTransferTaxIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.EnableTransferTaxIndex = this.chkEnableTransferTaxIndex.Checked;
        }

        /// <summary>
        /// CheckChanged event for Amount Sale Index CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableAmountSaleIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.EnableAmountSaleIndex = this.chkEnableAmountSaleIndex.Checked;
        }

        /// <summary>
        /// CheckChanged event for City Index CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableCityIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.EnableCityIndex = this.chkEnableCityIndex.Checked;
        }

        /// <summary>
        /// CheckChanged event for Concurrent Index CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableConcurrentIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.EnableConcurrentIndex = this.chkEnableConcurrentIndex.Checked;
        }

        /// <summary>
        /// CheckChanged event for RequestingParty CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableRequestingParty_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.EnableRequestingParty = this.chkEnableRequestingParty.Checked;
        }

        private void chkRequireTitleIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.RequireTitleIndex = this.chkRequireTitleIndex.Checked;
        }

        private void chkRequireNamesIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.RequireNamesIndex = this.chkRequireNamesIndex.Checked;
        }

        private void chkRequireAPNIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.RequireAPNIndex = this.chkRequireAPNIndex.Checked;
        }

        private void chkRequireTransferTaxIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.RequireTitleIndex = this.chkRequireTitleIndex.Checked;
        }

        private void chkRequireAmountSaleIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.RequireAmountSaleIndex = this.chkRequireAmountSaleIndex.Checked;
        }

        private void chkRequireCityIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.RequireCityIndex = this.chkRequireCityIndex.Checked;
        }

        private void chkRequireConcurrentIndex_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.RequireConcurrentIndex = this.chkRequireConcurrentIndex.Checked;
        }

        private void chkRequireRequestingParty_CheckedChanged(object sender, EventArgs e)
        {
            this._IndexOptionDetail.RequireRequestingParty = this.chkRequireRequestingParty.Checked;
        }
        #endregion

    }
}

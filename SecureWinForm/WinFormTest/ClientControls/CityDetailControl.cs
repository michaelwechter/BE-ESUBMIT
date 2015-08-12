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
    public partial class CityDetailControl : UserControl
    {
        private CityDetail _CityDetail;
        /// <summary>
        /// CityDetail object to be displayed in control
        /// </summary>
        public CityDetail CityDetail
        {
            get
            {
                return this._CityDetail;
            }
            set
            {
                this._CityDetail = value;
                if (this._CityDetail == null)
                {
                    this._CityDetail = new CityDetail();
                    this.txtID.Text = string.Empty;
                    this.txtCountyID.Text = string.Empty;
                    this.txtCode.Text = string.Empty;
                    this.txtDescription.Text = string.Empty;
                    this.txtActionCode.Text = string.Empty;
                    this.txtCreateTimeStamp.Text = string.Empty;
                    this.txtEditTimeStamp.Text = string.Empty;
                }
                else
                {
                    this.txtID.Text = this._CityDetail._ID;
                    this.txtCountyID.Text = this._CityDetail._CountyID;
                    this.txtCode.Text = this._CityDetail.Code;
                    this.txtDescription.Text = this._CityDetail.Description;
                    this.txtActionCode.Text = this._CityDetail.ActionCode.ToString();
                    this.txtCreateTimeStamp.Text = this._CityDetail.CreateTimeStampSpecified ? this._CityDetail.CreateTimeStamp.ToString() : string.Empty;
                    this.txtEditTimeStamp.Text = this._CityDetail.EditTimeStamp.ToString();
                }
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public CityDetailControl()
        {
            InitializeComponent();
        }
    }
}

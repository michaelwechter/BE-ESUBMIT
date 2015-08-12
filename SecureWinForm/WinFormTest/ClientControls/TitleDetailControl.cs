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
    public partial class TitleDetailControl : UserControl
    {
        private TitleDetail _TitleDetail;
        public TitleDetail TitleDetail
        {
            get
            {
                return this._TitleDetail;
            }
            set
            {
                this._TitleDetail = value;
                if (this._TitleDetail == null)
                {
                    this._TitleDetail = new TitleDetail();
                    this.txtID.Text = string.Empty;
                    this.txtCountyID.Text = string.Empty;
                    this.txtCode.Text = string.Empty;
                    this.txtDescription.Text = string.Empty;
                    this.txtActionCode.Text = string.Empty;
                    this.txtcreateTimeStamp.Text = string.Empty;
                    this.txtEditTimeStamp.Text = string.Empty;
                    this.txtSequence.Text = string.Empty;
                }
                else
                {
                    this.txtID.Text = _TitleDetail._ID;
                    this.txtCountyID.Text = this._TitleDetail._CountyID;
                    this.txtCode.Text = this._TitleDetail.Code;
                    this.txtDescription.Text = this._TitleDetail.Description;
                    this.txtActionCode.Text = this._TitleDetail.ActionCode.ToString();
                    this.txtSequence.Text = this._TitleDetail.SequenceSpecified ? this._TitleDetail.Sequence.ToString() : string.Empty;
                    this.txtcreateTimeStamp.Text = this._TitleDetail.CreateTimeStampSpecified ? this._TitleDetail.CreateTimeStamp.ToString() : string.Empty;
                    this.txtEditTimeStamp.Text = this._TitleDetail.EditTimeStamp.ToString();
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TitleDetailControl()
        {
            InitializeComponent();
        }
    }
}

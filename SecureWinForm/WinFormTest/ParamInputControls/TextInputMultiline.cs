using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormTest.ParamInputControls
{
    public partial class TextInputMultiline : TextInput
    {
        public TextInputMultiline()
        {
            InitializeComponent();
        }
        public TextInputMultiline(string parameterName)
            : base(parameterName)
        {
            InitializeComponent();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormTest.ParamInputControls;

namespace WinFormTest
{
    public partial class InputForm : Form
    {
        /// <summary>
        /// DictionaryEntry pairs of Parameter Names and Parameter Values
        /// </summary>
        public DictionaryEntry[] Parameters
        {
            get
            {
                ArrayList parameters = new ArrayList();
                foreach (Control formControl in this.pnlInput.Controls)
                {
                    if (formControl is InputControlBase)
                    {
                        parameters.Add(
                            new DictionaryEntry(
                                ((InputControlBase)formControl).ParameterName, 
                                ((InputControlBase)formControl).ParameterValue));
                    }
                }
                return (DictionaryEntry[])parameters.ToArray(typeof(DictionaryEntry));
            }
        }
        public InputForm()
        {
            InitializeComponent();
        }
        public InputForm(string[] parameterNames):this()
        {
            this.SetUpInput(parameterNames);
        }
        public InputForm(DictionaryEntry[] parameters):this()
        {
            this.SetUpInput(parameters);
        }
        private void SetUpInput(DictionaryEntry[] parameters)
        {
            int yPos = 0;
            foreach (DictionaryEntry param in parameters)
            {
                if (TestController.TextInputParameters.Contains(param.Key.ToString()))
                {
                    TextInput inputX = new TextInput(param.Key.ToString());
                    inputX.ParameterValue = param.Value.ToString();
                    inputX.Location = new Point(2, yPos);
                    inputX.Width = pnlInput.Width - 4;
                    yPos = yPos + inputX.Height + 2;
                    this.pnlInput.Controls.Add(inputX);
                }
                else if (TestController.EnumParameters.Contains(param.Key.ToString()))
                {
                    EnumSelector inputY = new EnumSelector(param.Key.ToString());
                    inputY.ParameterValue = param.Value.ToString();
                    inputY.Location = new Point(2, yPos);
                    inputY.Width = pnlInput.Width - 4;
                    yPos = yPos + inputY.Height + 2;
                    this.pnlInput.Controls.Add(inputY);
                }
                else if (TestController.ContentParameters.Contains(param.Key.ToString()))
                {
                    switch (param.Key.ToString())
                    {
                        case "BatchXML":
                            {
                                // Generate BatchXML with a GUI
                                //BatchXML_GUI batchXMLControl = new BatchXML_GUI();

                                // Directly input BatchXML
                                TextInputMultiline batchXMLControl = new TextInputMultiline(param.Key.ToString());

                                batchXMLControl.Location = new Point(2, yPos);
                                batchXMLControl.Width = pnlInput.Width - 4;
                                yPos = yPos + batchXMLControl.Height + 2;
                                this.pnlInput.Controls.Add(batchXMLControl);
                                break;
                            }
                        case "DocumentXML":
                            {
                                // Generate BatchXML with a GUI
                                //DocumentXML_GUI docXMLControl = new DocumentXML_GUI();

                                // Directly input BatchXML
                                TextInputMultiline docXMLControl = new TextInputMultiline(param.Key.ToString());

                                docXMLControl.Location = new Point(2, yPos);
                                docXMLControl.Width = pnlInput.Width - 4;
                                yPos = yPos + docXMLControl.Height + 2;
                                this.pnlInput.Controls.Add(docXMLControl);
                                break;
                            }
                    }
                }
            }
            this.pnlInput.Height = yPos;
            int cont = this.pnlInput.Controls.Count;
            this.Height = this.pnlInput.Height + this.pnlDialogButtons.Height + 30;
        }
        private void SetUpInput(string[] parameterNames)
        {
            int yPos = 0;
            foreach (string parameter in parameterNames)
            {
                if (TestController.TextInputParameters.Contains(parameter))
                {
                    TextInput inputX = new TextInput(parameter);
                    inputX.Location = new Point(2, yPos);
                    inputX.Width = pnlInput.Width - 4;
                    yPos = yPos + inputX.Height + 2;
                    this.pnlInput.Controls.Add(inputX);
                }
                else if (TestController.ContentParameters.Contains(parameter))
                {
                    switch (parameter)
                    {
                        case "BatchXML":
                            {
                                // Generate BatchXML with a GUI
                                //BatchXML_GUI batchXMLControl = new BatchXML_GUI();
                                
                                // Directly input BatchXML
                                TextInputMultiline batchXMLControl = new TextInputMultiline(parameter);

                                batchXMLControl.Location = new Point(2, yPos);
                                batchXMLControl.Width = pnlInput.Width - 4;
                                yPos = yPos + batchXMLControl.Height + 2;
                                this.pnlInput.Controls.Add(batchXMLControl);
                                break;
                            }
                        case "DocumentXML":
                            {
                                // Generate BatchXML with a GUI
                                //DocumentXML_GUI docXMLControl = new DocumentXML_GUI();

                                // Directly input BatchXML
                                TextInputMultiline docXMLControl = new TextInputMultiline(parameter);

                                docXMLControl.Location = new Point(2, yPos);
                                docXMLControl.Width = pnlInput.Width - 4;
                                yPos = yPos + docXMLControl.Height + 2;
                                this.pnlInput.Controls.Add(docXMLControl);
                                break;
                            }
                    }
                }
            }
            this.pnlInput.Height = yPos;
            int cont = this.pnlInput.Controls.Count;
            this.Height = this.pnlInput.Height + this.pnlDialogButtons.Height + 30;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}

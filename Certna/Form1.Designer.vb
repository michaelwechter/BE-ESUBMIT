<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.cmdLoadform = New System.Windows.Forms.Button()
        Me.cmdResponseRD = New System.Windows.Forms.Button()
        Me.btnConvertTiffPdf = New System.Windows.Forms.Button()
        Me.btnRunAll = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BtnStopApp = New System.Windows.Forms.Button()
        Me.lblTimer = New System.Windows.Forms.Label()
        Me.txtTimerInt = New System.Windows.Forms.TextBox()
        Me.lblSleepInt = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.lblConnectString = New System.Windows.Forms.Label()
        Me.cmdESubmitWeb = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.cmdUpdStatus = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(12, 8)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(500, 365)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'cmdLoadform
        '
        Me.cmdLoadform.Location = New System.Drawing.Point(559, 410)
        Me.cmdLoadform.Name = "cmdLoadform"
        Me.cmdLoadform.Size = New System.Drawing.Size(112, 23)
        Me.cmdLoadform.TabIndex = 36
        Me.cmdLoadform.Text = "Create Req Files"
        Me.cmdLoadform.UseVisualStyleBackColor = True
        '
        'cmdResponseRD
        '
        Me.cmdResponseRD.Location = New System.Drawing.Point(708, 410)
        Me.cmdResponseRD.Name = "cmdResponseRD"
        Me.cmdResponseRD.Size = New System.Drawing.Size(112, 23)
        Me.cmdResponseRD.TabIndex = 38
        Me.cmdResponseRD.Text = "Read Responses"
        Me.cmdResponseRD.UseVisualStyleBackColor = True
        '
        'btnConvertTiffPdf
        '
        Me.btnConvertTiffPdf.Location = New System.Drawing.Point(857, 410)
        Me.btnConvertTiffPdf.Name = "btnConvertTiffPdf"
        Me.btnConvertTiffPdf.Size = New System.Drawing.Size(112, 23)
        Me.btnConvertTiffPdf.TabIndex = 39
        Me.btnConvertTiffPdf.Text = "Tiff2PDF"
        Me.btnConvertTiffPdf.UseVisualStyleBackColor = True
        '
        'btnRunAll
        '
        Me.btnRunAll.Location = New System.Drawing.Point(589, 186)
        Me.btnRunAll.Name = "btnRunAll"
        Me.btnRunAll.Size = New System.Drawing.Size(112, 23)
        Me.btnRunAll.TabIndex = 40
        Me.btnRunAll.Text = "&Run App"
        Me.btnRunAll.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'BtnStopApp
        '
        Me.BtnStopApp.Location = New System.Drawing.Point(776, 186)
        Me.BtnStopApp.Name = "BtnStopApp"
        Me.BtnStopApp.Size = New System.Drawing.Size(112, 23)
        Me.BtnStopApp.TabIndex = 42
        Me.BtnStopApp.Text = "&Stop App"
        Me.BtnStopApp.UseVisualStyleBackColor = True
        '
        'lblTimer
        '
        Me.lblTimer.AutoSize = True
        Me.lblTimer.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTimer.Location = New System.Drawing.Point(715, 93)
        Me.lblTimer.Name = "lblTimer"
        Me.lblTimer.Size = New System.Drawing.Size(26, 29)
        Me.lblTimer.TabIndex = 43
        Me.lblTimer.Text = "0"
        Me.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTimerInt
        '
        Me.txtTimerInt.Location = New System.Drawing.Point(694, 23)
        Me.txtTimerInt.Name = "txtTimerInt"
        Me.txtTimerInt.Size = New System.Drawing.Size(47, 20)
        Me.txtTimerInt.TabIndex = 44
        Me.txtTimerInt.Text = "120"
        Me.txtTimerInt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSleepInt
        '
        Me.lblSleepInt.AutoSize = True
        Me.lblSleepInt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSleepInt.Location = New System.Drawing.Point(544, 27)
        Me.lblSleepInt.Name = "lblSleepInt"
        Me.lblSleepInt.Size = New System.Drawing.Size(150, 13)
        Me.lblSleepInt.TabIndex = 45
        Me.lblSleepInt.Text = "Set Sleep Interval in Seconds:"
        Me.lblSleepInt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(589, 246)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(112, 23)
        Me.Button2.TabIndex = 47
        Me.Button2.Text = "Read eSubmit FTP"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(575, 306)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(137, 23)
        Me.Button1.TabIndex = 48
        Me.Button1.Text = "Read eSubmit Recorded"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(762, 306)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(134, 23)
        Me.Button3.TabIndex = 49
        Me.Button3.Text = "Read eSubmit Rejected"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'lblConnectString
        '
        Me.lblConnectString.AutoSize = True
        Me.lblConnectString.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConnectString.Location = New System.Drawing.Point(837, 23)
        Me.lblConnectString.Name = "lblConnectString"
        Me.lblConnectString.Size = New System.Drawing.Size(133, 29)
        Me.lblConnectString.TabIndex = 50
        Me.lblConnectString.Text = "Dev or Live"
        Me.lblConnectString.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdESubmitWeb
        '
        Me.cmdESubmitWeb.Location = New System.Drawing.Point(776, 246)
        Me.cmdESubmitWeb.Name = "cmdESubmitWeb"
        Me.cmdESubmitWeb.Size = New System.Drawing.Size(112, 23)
        Me.cmdESubmitWeb.TabIndex = 51
        Me.cmdESubmitWeb.Text = "Read eSubmit Web"
        Me.cmdESubmitWeb.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(559, 477)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(112, 23)
        Me.Button4.TabIndex = 52
        Me.Button4.Text = "Update DPS Database"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'cmdUpdStatus
        '
        Me.cmdUpdStatus.Location = New System.Drawing.Point(858, 477)
        Me.cmdUpdStatus.Name = "cmdUpdStatus"
        Me.cmdUpdStatus.Size = New System.Drawing.Size(112, 23)
        Me.cmdUpdStatus.TabIndex = 53
        Me.cmdUpdStatus.Text = "Update Req Status"
        Me.cmdUpdStatus.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(706, 477)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(112, 23)
        Me.Button5.TabIndex = 54
        Me.Button5.Text = "check Certna"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1005, 536)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.cmdUpdStatus)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.cmdESubmitWeb)
        Me.Controls.Add(Me.lblConnectString)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.lblSleepInt)
        Me.Controls.Add(Me.txtTimerInt)
        Me.Controls.Add(Me.lblTimer)
        Me.Controls.Add(Me.BtnStopApp)
        Me.Controls.Add(Me.btnRunAll)
        Me.Controls.Add(Me.btnConvertTiffPdf)
        Me.Controls.Add(Me.cmdResponseRD)
        Me.Controls.Add(Me.cmdLoadform)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Name = "Form1"
        Me.Text = "XML Processor - v04-15-2015"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents cmdLoadform As System.Windows.Forms.Button
    Friend WithEvents cmdResponseRD As System.Windows.Forms.Button
    Friend WithEvents btnConvertTiffPdf As System.Windows.Forms.Button
    Friend WithEvents btnRunAll As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BtnStopApp As System.Windows.Forms.Button
    Friend WithEvents lblTimer As System.Windows.Forms.Label
    Friend WithEvents txtTimerInt As System.Windows.Forms.TextBox
    Friend WithEvents lblSleepInt As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents lblConnectString As System.Windows.Forms.Label
    Friend WithEvents cmdESubmitWeb As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents cmdUpdStatus As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button

End Class

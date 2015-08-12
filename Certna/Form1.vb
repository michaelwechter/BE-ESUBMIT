Option Compare Text
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Net.WebRequestMethods
Imports System.Xml
Imports System.Drawing.Image
Imports System.Net.Mail


Public Class Form1
    ' Public pub_connectionstring = "User ID=dpsxml;Password=Crt_data590;Initial Catalog=eSubmit_live;Server=M1CFAE"
    Public pub_connectionstring = "User ID=dpsxml;Password=Crt_data590;Initial Catalog=eSubmit_dev;Server=M1CFAE"
    'Public mySQL_connectionstring As String = "Database=qc_acme_1_dev;Data Source=50.28.48.246;User Id=crt;Password=H$b*uCw&#hsfpxT*7n6Wvvf3dQu@wY;"
    Public mySQL_connectionstring As String = "Database=qc_acme_1_dev;Data Source=10.38.18.205;User Id=crt;Password=H$b*uCw&#hsfpxT*7n6Wvvf3dQu@wY;"

    Public objDR1 As SqlDataReader
    Public objDR2 As SqlDataReader
    Public objDR3 As SqlDataReader


    Public xmlstr As String
    Public xmllist As New ArrayList

    'only set this for direct XML submit - Web checked by DPS
    Public chkMOU As String = ""
    Public secKnter As Int32 = 0

    Public strID As String = ""
    Public strReqPartyName As String = ""
    Public strReqPartyAddress As String = ""
    Public strReqPartyCity As String = ""
    Public strReqPartyState As String = ""
    Public strReqPartyPostalCode As String = ""
    Public strReqPartyIdentifier As String = ""
    ' Public strLoginAccntIdentifier As String = ""

    Public strSubmitPartyname As String = ""
    Public strSubmitLoginAccntID As String = ""

    Public strTimestamp As String = ""
    Public strRecDateTime As String = ""
    Public strInstrNumber As String = ""
    Public strOrderNumber As String = ""
    Public strOrderNumber2 As String = ""
    Public strDocType As String = ""
    ' Public strDocTag As String = ""

    Public strSecurityType As String = ""
    Public strSeq As String = ""
    Public strType As String = ""
    Public strCountyFips As String = ""
    Public strStateFips As String = ""
    Public strCountyName As String = ""
    Public strStateName As String = ""
    Public strCustomerID As String = ""
    Public strCustID As String = ""
    Public strPayLoadID As String = ""
    Public strERE As String = "Certna"
    Public strEREId As String = ""
    Public strFTPFolder As String = ""
    Public strOrigSys As String = ""
    Public strSecondaryValue As String = ""

    Public MyPath As String = "c:\dps_xml\"
    Public MyFile As String
    Public MyfullPath As String
    Public MyTiffFile As String = ""
    Public MyTiffFile2 As String = ""
    Public MyTiffPath As String = "C:\DPS_XML\CreateRequests\processing\"
    Public MyTifffullPath As String

    Public strLog As String = ""

    Public strCurTiff As String = ""

    Public img As Image
    Public numpages As Integer

    '  Public RelDocIndicator As String = ""
    Public strStatus As String = ""
    Public strErrcode As String = ""
    '  Public strErrDesc As String = ""
    ' Public strErrorTag As String = ""
    Public strDoc As String = ""

    Public strFee As String = ""
    Public strTax As String = ""
    Public strRecDocTag As String = ""

    Public arrTiff As New ArrayList
    Public arrDocType As New ArrayList
    Public arrEREDocType As New ArrayList
    Public arrEREtiffName As New ArrayList
    Public strEreDocType As String = ""
    Public strERETiffName As String = ""


    Public arrErrcode As New ArrayList
    Public arrErrDesc As New ArrayList
    Public arrSeqNum As New ArrayList
    Public arrActivity As New ArrayList
    Public arrSecurityType As New ArrayList

    Public arrCustID As New ArrayList
    Public arrFTPFolder As New ArrayList

    Public strdate As String = DateTime.Now.ToString("MMddyyyy")

    Public strEmailbody As String = ""


    Private Sub Form1_Load() Handles MyBase.Load
        On Error Resume Next

        If pub_connectionstring.ToString.Contains("Submit_dev") Then

            lblConnectString.Text = "Dev"
        Else

            lblConnectString.Text = "Live"
        End If
    End Sub

    Private Sub MoveDpsFilesToProcessing()
        Dim fileEntries = Directory.GetFiles(MyPath & "CreateRequests\queue\", "*.*")

        ' Dim strStartProcessing As String = "Moving DPS Request files from Queue to Processing..." & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff")
        '  Dim strFinishProcessing As String = "Finished moving DPS Request files to Processing..." & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff")

        ' strLog = strStartProcessing & vbCrLf
        For Each sFileName In fileEntries

            MyFile = System.IO.Path.GetFileName(sFileName)

            If IO.File.Exists(MyPath & "CreateRequests\Processing\" & MyFile) Then
                IO.File.Delete(MyPath & "CreateRequests\Processing\" & MyFile)
            End If

            IO.File.Move(sFileName, MyPath & "CreateRequests\Processing\" & MyFile)

            ' strLog += MyFile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf
        Next

        ' strLog += strFinishProcessing & vbCrLf & vbCrLf & "Start Processing DPS XML's..." & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf & vbCrLf


        Call CreateReqFiles()

    End Sub

    Private Sub MoveCertnaRespToProcessing()
        Dim fileEntries = Directory.GetFiles(MyPath & "ProcessResponses\Queue\", "*.*")

        'Dim strStartProcessing As String = "Moving Certna Repsonse files from \Responses to \Processing..." & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff")
        'Dim strFinishProcessing As String = "Finished moving Certna Repsonse files to Processing..." & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff")

        'strLog += strStartProcessing & vbCrLf

        For Each sFileName In fileEntries

            MyFile = System.IO.Path.GetFileName(sFileName)

            If IO.File.Exists(MyPath & "ProcessResponses\Processing\" & MyFile) Then
                IO.File.Delete(MyPath & "ProcessResponses\Processing\" & MyFile)
            End If

            IO.File.Move(sFileName, MyPath & "ProcessResponses\Processing\" & MyFile)

            strLog += MyFile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf
        Next

        ' strLog += strFinishProcessing & vbCrLf & vbCrLf & "Start Processing Certna Response XML's..." & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf & vbCrLf


        Call ReadResponseFiles()

    End Sub
    Private Sub eSubmitGetCertnaResponses()
        Dim fileEntries = Directory.GetFiles(MyPath & "ProcessResponses\queue\hold2\", "*.xml")

        Dim conn As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand

        Cmd1.CommandText = "select OrderNumber + '.' + cast(UniqueOrderID as char(10)) as 'CertnaOrderNum' from orders where status = 'Submitted to FTP'"
        Cmd1.Connection = conn
        conn.Open()

        objDR1 = Cmd1.ExecuteReader
        While objDR1.Read()
            With objDR1
                For Each sFileName In fileEntries
                    MyFile = System.IO.Path.GetFileName(sFileName)


                    If MyFile.Contains(RTrim(.Item("CertnaOrderNum"))) Then
                        IO.File.Move(sFileName, MyPath & "ProcessResponses\queue\" & MyFile)
                    End If
                Next

            End With
        End While
        objDR1.Close()
        conn.Close()

    End Sub
    Private Sub eSubmitGetCustomers()

        Dim conn As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand


        arrCustID.Clear()


        Cmd1.CommandText = "select CustomerID,FTPFolder from customer"
        Cmd1.Connection = conn
        conn.Open()
        'objDR1 = Cmd1.ExecuteReader

        ''get Customer info' this will wait until I have more info on CustomerID

        objDR1 = Cmd1.ExecuteReader
        While objDR1.Read()
            With objDR1
                arrCustID.Add(.Item("CustomerID"))
                arrFTPFolder.Add(.Item("FtpFolder"))
            End With
        End While
        objDR1.Close()
        conn.Close()

        For x = 0 To arrCustID.Count - 1
            chkMOU = "Y"

            strCustomerID = arrCustID.Item(x)
            strFTPFolder = arrFTPFolder.Item(x)

            Dim fileEntries = Directory.GetFiles(MyPath & "ftp\" & strFTPFolder & "\", "*.*")
            'customer ID can be determined from FTP folder
            'strCustomerID = "FAT01"

            For Each sFileName In fileEntries

                MyFile = System.IO.Path.GetFileName(sFileName)

                If IO.File.Exists(MyPath & "eSubmit\" & MyFile) Then
                    IO.File.Delete(MyPath & "eSubmit\" & MyFile)
                End If

                IO.File.Move(sFileName, MyPath & "eSubmit\" & MyFile)

            Next sFileName

            Call eSubmitGetFiles()

        Next


    End Sub

    Private Sub eSubmitGetFiles()


        Dim fileEntries = Directory.GetFiles(MyPath & "eSubmit\", "*.xml")
        'customer ID can be determined from FTP folder
        'strCustomerID = "FAT01"

        For Each sFileName In fileEntries

            ' RichTextBox1.Text = RichTextBox1.Text & sFileName & Now & vbCrLf

            Call ClearFields()

            MyFile = System.IO.Path.GetFileName(sFileName)

            MyfullPath = sFileName

            'strLog += MyFile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf

            If Not IO.File.Exists(MyPath & "eSubmit\processed\" & MyFile) Then
                Call eSubmit_ConvertXML(MyfullPath)
            End If


        Next sFileName

        '  RichTextBox1.Text += "Done Processing eSubmit Request files- " & DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss") & vbCrLf
    End Sub
    Private Sub eSubmitGetFilesWeb()


        Dim fileEntries = Directory.GetFiles(MyPath & "eSubmit\", "*.xml")
        'customer ID can be determined from FTP folder
        'strCustomerID = "FAT01"

        For Each sFileName In fileEntries

            ' RichTextBox1.Text = RichTextBox1.Text & sFileName & Now & vbCrLf

            Call ClearFields()

            MyFile = System.IO.Path.GetFileName(sFileName)

            ' strCustomerID = MyFile.Substring(0, MyFile.IndexOf("_"))
            MyfullPath = sFileName

            'strLog += MyFile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf

            If Not IO.File.Exists(MyPath & "eSubmit\processed\" & MyFile) Then
                Call eSubmit_ConvertXML(MyfullPath)
            End If

        Next sFileName

        ' RichTextBox1.Text += "Done Processing eSubmit Request files- " & DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss") & vbCrLf
    End Sub
    Private Sub eSubmitGetRecorded()

        Dim fileEntries = Directory.GetFiles(MyPath & "\ProcessResponses\recorded\", "*.xml")

        For Each sFileName In fileEntries

            ' RichTextBox1.Text = RichTextBox1.Text & sFileName & Now & vbCrLf

            Call ClearFields()

            MyFile = System.IO.Path.GetFileName(sFileName)

            MyfullPath = sFileName

            'strLog += MyFile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf

            Call eSubmit_Recorded2(MyfullPath)

            If IO.File.Exists(MyPath & "ProcessResponses\recorded\done\" & MyFile) Then
                IO.File.Delete(MyPath & "ProcessResponses\recorded\done\" & MyFile)
            End If

            IO.File.Move(MyfullPath, MyPath & "ProcessResponses\recorded\done\" & MyFile)

        Next sFileName

        'eSubmitWriteRecorded()

        '  RichTextBox1.Text += "Done Processing eSubmit Recorded files - " & DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss") & vbCrLf

    End Sub

    Private Sub eSubmitGetRejected()

        Dim fileEntries = Directory.GetFiles(MyPath & "\ProcessResponses\rejected\", "*.xml")

        For Each sFileName In fileEntries

            ' RichTextBox1.Text = RichTextBox1.Text & sFileName & Now & vbCrLf

            Call ClearFields()

            MyFile = System.IO.Path.GetFileName(sFileName)

            MyfullPath = sFileName

            'strLog += MyFile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf

            Call eSubmit_Rejected2(MyfullPath)

            If IO.File.Exists(MyPath & "ProcessResponses\rejected\done\" & MyFile) Then
                IO.File.Delete(MyPath & "ProcessResponses\rejected\done\" & MyFile)
            End If

            IO.File.Move(MyfullPath, MyPath & "ProcessResponses\rejected\done\" & MyFile)

        Next sFileName

        'Call eSubmitWriteRejected()

        ' RichTextBox1.Text += "Done Processing eSubmit Rejected files - " & Now & vbCrLf
    End Sub
    Private Sub CreateReqFiles()

        ' Find DSP XML files...aka Dave's files - original and revised

        'Dim outfile As StreamWriter

        'RichTextBox1.Text = ""
        Dim fileEntries = Directory.GetFiles(MyPath & "CreateRequests\processing\", "*.xml")

        For Each sFileName In fileEntries

            ' RichTextBox1.Text = RichTextBox1.Text & sFileName & Now & vbCrLf

            Call ClearFields()

            MyFile = System.IO.Path.GetFileName(sFileName)

            MyfullPath = sFileName

            strLog += MyFile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf

            Call Read_DavesXML(MyfullPath)

        Next sFileName

        ''Write to log
        'If IO.File.Exists(MyPath & "log\log_" & strdate & ".log") Then
        '    outfile = IO.File.AppendText(MyPath & "log\log_" & strdate & ".log")
        '    outfile.WriteLine(strLog)
        '    outfile.Flush()
        '    outfile.Close()
        'Else
        '    outfile = IO.File.CreateText(MyPath & "log\log_" & strdate & ".log")
        '    outfile.WriteLine(strLog)
        '    outfile.Flush()
        '    outfile.Close()
        'End If

        '  RichTextBox1.Text += "No more DPS Request files to process - " & Now & vbCrLf

        ' MsgBox("No more DPS Request files to process")

    End Sub

    Private Sub ReadResponseFiles()

        ' Find Certna Response files - Recorded and Rejected
        'Dim outfile As StreamWriter
        Dim fileEntries = Directory.GetFiles(MyPath & "ProcessResponses\processing", "*.xml")

        For Each sFileName In fileEntries

            Call ClearFields()

            MyFile = System.IO.Path.GetFileName(sFileName)

            MyfullPath = sFileName

            strLog += "Processing " & MyFile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf

            Call Read_ResponseXML(MyfullPath)

        Next sFileName

        ''Write to log
        'If IO.File.Exists(MyPath & "log\log_" & strdate & ".log") Then
        '    outfile = IO.File.AppendText(MyPath & "log\log_" & strdate & ".log")
        '    outfile.WriteLine(strLog)
        '    outfile.Flush()
        '    outfile.Close()
        'Else
        '    outfile = IO.File.CreateText(MyPath & "log\log_" & strdate & ".log")
        '    outfile.WriteLine(strLog)
        '    outfile.Flush()
        '    outfile.Close()
        'End If

        '  RichTextBox1.Text += "No more Response files to process - " & Now & vbCrLf


        ' MsgBox("No more Response files to process")
    End Sub
    Private Sub ClearFields()
        strStatus = ""
        strID = ""
        strReqPartyName = ""
        strReqPartyAddress = ""
        strReqPartyCity = ""
        strReqPartyState = ""
        strReqPartyPostalCode = ""
        strReqPartyIdentifier = ""
        ' strLoginAccntIdentifier = ""

        '  strCustomerID = ""
        strSubmitPartyname = ""
        strSubmitLoginAccntID = ""
        strTimestamp = ""
        strRecDateTime = ""
        strOrderNumber = ""
        strDocType = ""
        strSeq = ""
        strType = ""
        strCountyFips = ""
        strStateFips = ""
        strCountyName = ""
        strStateName = ""
        strRecDateTime = ""
        strSecondaryValue = ""
        strSecurityType = ""
        strRecDocTag = ""
        'strErrorTag = ""
        arrTiff.Clear()
        arrErrcode.Clear()
        arrErrDesc.Clear()
        arrDocType.Clear()
        arrSeqNum.Clear()

    End Sub

    Private Sub Read_DavesXML(ByVal XMLFileName As String)

        Dim curelement As String

        curelement = ""

        If IO.File.Exists(MyfullPath) Then

            Using reader As XmlReader = XmlReader.Create(XMLFileName)
                ' Parse the file and display each of the nodes.
                While reader.Read()
                    Select Case reader.NodeType
                        Case XmlNodeType.Element
                            curelement = reader.Name

                        Case XmlNodeType.Text
                            If curelement = "Id" Then
                                strID = reader.Value
                            ElseIf curelement = "timestamp" Then
                                strTimestamp = reader.Value
                            ElseIf curelement = "orderNumber" Then
                                strOrderNumber = reader.Value
                            ElseIf curelement = "SecondaryValue" Then
                                strSecondaryValue = reader.Value
                            ElseIf curelement = "docType" Then  'create arraylist for DocType
                                arrDocType.Add(reader.Value)
                            ElseIf curelement = "SecurityType" Then
                                strSecurityType = reader.Value
                            ElseIf curelement = "seq" Then
                                strSeq = reader.Value
                            ElseIf curelement = "type" Then
                                strType = reader.Value
                            ElseIf curelement = "countyFips" Then
                                strCountyFips = reader.Value
                            ElseIf curelement = "statefips" Then
                                strStateFips = reader.Value
                            ElseIf curelement = "reqPartyName" Then
                                strReqPartyName = reader.Value
                            ElseIf curelement = "streetaddress" Then
                                strReqPartyAddress = reader.Value
                            ElseIf curelement = "city" Then
                                strReqPartyCity = reader.Value
                            ElseIf curelement = "state" Then
                                strReqPartyState = reader.Value
                            ElseIf curelement = "postalcode" Then
                                strReqPartyPostalCode = reader.Value
                            ElseIf curelement = "identifier" Then
                                strReqPartyIdentifier = reader.Value
                            ElseIf curelement = "submitPartyName" Then
                                strSubmitPartyname = reader.Value

                            ElseIf curelement = "LoginAccountIdentifier" Then
                                strSubmitLoginAccntID = reader.Value
                            End If




                            If curelement = "tiff" Then
                                strCurTiff = reader.Value
                                ' If IO.File.Exists(MyTiffPath & strCurTiff) Then
                                'strLog += strCurTiff & " - file found" & vbCrLf

                                arrTiff.Add(strCurTiff)   'create arraylist for Tiff filenames

                                'Else

                                '    'if a tiff file is missing - write a log entry>>then abort this file>>then move files to Error folder >> and move in to next file
                                '    strLog += strCurTiff & " - file NOT found - file " & MyFile & " aborted and moved to Error folder" & vbCrLf
                                '    RichTextBox1.Text = strCurTiff & " - file NOT found - file " & MyFile & " aborted and moved to Error folder" & vbCrLf

                                '    reader.Close()

                                '    If IO.File.Exists(MyTiffPath & "errors\" & MyFile) Then
                                '        IO.File.Delete(MyTiffPath & "errors\" & MyFile)
                                '    End If

                                '    IO.File.Move(MyfullPath, MyTiffPath & "errors\" & MyFile)

                                '    Call MissingTiff() ' this will move the Tiff files that are present to error folder with XML

                                '    Exit Sub

                                'End If
                            End If


                    End Select
                End While
            End Using

        End If

        'check for missing tif files....
        For i = 0 To arrTiff.Count - 1

            If Not IO.File.Exists(MyTiffPath & arrTiff.Item(i)) Then

                Call MissingTiff()
                Exit Sub
            End If
        Next

        Call btnWriteXML_Click()

    End Sub
    Private Sub MissingTiff()
        'Dim fileEntries = Directory.GetFiles(MyTiffFile, MyFile.Substring(0, 5) & "*.tiff")

        'Dim sOnlyFileName As String

        'For Each sFileName In fileEntries

        '    sOnlyFileName = IO.Path.GetFileName(sFileName)
        For i = 0 To arrTiff.Count - 1

            If IO.File.Exists(MyTiffPath & arrTiff.Item(i)) Then

                If IO.File.Exists(MyTiffPath & "errors\" & arrTiff.Item(i)) Then
                    IO.File.Delete(MyTiffPath & "errors\" & arrTiff.Item(i))
                End If

                IO.File.Move(MyTiffPath & arrTiff.Item(i), MyTiffFile & "errors\" & arrTiff.Item(i))
            End If

        Next
        If IO.File.Exists(MyTiffPath & "\errors\" & MyFile) Then
            IO.File.Delete(MyTiffPath & "\errors\" & MyFile)
        End If

        IO.File.Move(MyfullPath, MyTiffPath & "\errors\" & MyFile)

    End Sub
    Private Sub eSubmitMissingTiff()
        Dim conn As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand
        ' Dim outfile As StreamWriter
        Dim strErr As String = ""
        Dim strCnt As Integer = 0

        Cmd1.Connection = conn
        conn.Open()

        Cmd1.CommandText = "select Count(*) as Cnt from orders where filename = '" & MyFile & "' and  status = 'Error-Missing Tif'"
       
        objDR1 = Cmd1.ExecuteReader
        While objDR1.Read()
            With objDR1

                strCnt = .Item("Cnt")

            End With
        End While
        objDR1.Close()



        If strCnt > 3 Then

            For i = 0 To arrTiff.Count - 1

                If IO.File.Exists(MyPath & "eSubmit\" & arrTiff.Item(i)) Then

                    If IO.File.Exists(MyPath & "eSubmit\errors\" & arrTiff.Item(i)) Then
                        IO.File.Delete(MyPath & "eSubmit\errors\" & arrTiff.Item(i))
                    End If

                    IO.File.Move(MyPath & "eSubmit\" & arrTiff.Item(i), MyPath & "eSubmit\errors\" & arrTiff.Item(i))

                Else
                    arrActivity.Add("'Error:missing tiff-" & arrTiff.Item(i) & "','" & MyFile & "','" & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & "'")

                    strErr += MyFile & " - Error:missing tiff-" & arrTiff.Item(i) & vbCrLf

                End If

            Next

            'move xml file to FTP error
            If IO.File.Exists(MyPath & "eSubmit\errors\" & MyFile) Then
                IO.File.Delete(MyPath & "eSubmit\errors\" & MyFile)
            End If

            IO.File.Move(MyfullPath, MyPath & "eSubmit\errors\" & MyFile)

            conn.Close()
            arrActivity.Clear()

            strEmailbody = "There is an order with one or more missing Tiff files. It has been moved to the eSubmit\errors folder. The filename is " & MyFile & "."

            Call SendEMail()

            Exit Sub
        End If

        

        Cmd1.CommandText = "Insert into orders(ordernumber,CustomerID,CustomerCode,filename,status,RequestProcessedDate) Values('" & strOrderNumber & "','" & strCustID & "','" & strCustomerID & "','" & MyFile & "','Error-Missing Tif', getdate())"
        Cmd1.ExecuteNonQuery()

        Cmd1.CommandText = "select uniqueOrderID from orders where filename = '" & MyFile & "'"
        objDR1 = Cmd1.ExecuteReader
        While objDR1.Read()
            With objDR1

                strID = .Item("uniqueOrderID")

            End With
        End While
        objDR1.Close()



        arrActivity.Add("'Error:missing tiff-Moved rest of package to error folder','" & MyFile & "','" & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & "'")

        For i = 0 To arrActivity.Count - 1
            Cmd1.CommandText = "Insert into Activity(UniqueOrderID,Activity,XMLFilename,ActivityDateTime) Values('" & strID & "'," & arrActivity.Item(i) & ")"
            Cmd1.ExecuteNonQuery()

        Next

        conn.Close()
        arrActivity.Clear()

    End Sub

    Private Sub Read_ResponseXML(ByVal XMLFileName As String)
        Dim curelement As String
        Dim tmpRecFee As String = ""
        Dim strRecEndDesc As String = ""
        Dim strTmpRecFeeAmt As String = ""
        Dim strTmpOrdNum As String = ""
        Dim strMid1 As Integer = 0
        Dim strMid2 As Integer = 0
        Dim strTmpDoctype As String = ""
        Dim strSecTypeCnt As String = ""
        Dim intSeq As Integer

        Dim tmpseqno As String = ""


        Dim strTiffout As String = ""
        Dim MyFileReplace As String = ""
        Dim strtiff2pdf As String = ""

        Dim strNoErrorDesc As String = "Y"

        Dim conn As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand
        Dim strOutboundFileNo As String = ""

        Dim feecounter As Integer = 0

        Cmd1.Connection = conn
        conn.Open()

        curelement = ""
        Using reader As XmlReader = XmlReader.Create(XMLFileName)
            ' Parse the file and display each of the nodes.
            While reader.Read()
                Select Case reader.NodeType
                    Case XmlNodeType.Element

                        curelement = reader.Name

                        '*****'If attributes exist
                        If reader.HasAttributes Then
                            While reader.MoveToNextAttribute()
                                'Display attribute name and value.

                                '***************
                                If curelement = "Response" And reader.Name = "ResponseDatetime" Then
                                    strTimestamp = reader.Value
                                End If

                                If curelement = "Package" Then
                                    If reader.Name = "StateFipsCode" Then
                                        strStateFips = reader.Value
                                    End If
                                    If reader.Name = "CountyFipsCode" Then
                                        strCountyFips = reader.Value
                                    End If

                                    'If reader.Name = "SecurityType" Then
                                    '    strsecur = reader.Value
                                    'End If

                                End If


                                If curelement = "Recording_Endorsement" And strDocType <> "PCOR" And strDocType <> "TransferDeclaration" And strDocType <> "OtherNonRecorded" Then
                                    If reader.Name = "_RecordedDatetime" Then
                                        strRecDateTime = reader.Value
                                    ElseIf reader.Name = "_InstrumentNumberIdentifier" Then
                                        strInstrNumber = reader.Value
                                    End If
                                End If

                                If curelement = "SUBMITTING_PARTY" And reader.Name = "LoginAccountIdentifier" Then
                                    strSubmitLoginAccntID = reader.Value
                                End If

                                If curelement = "RECORDING_TRANSACTION_IDENTIFIER" Then
                                    If reader.Name = "SecondaryValue" Then
                                        strSecondaryValue = reader.Value

                                    ElseIf reader.Name = "_Value" Then
                                        strOrderNumber = reader.Value

                                        'this is for Rejected Docs with no Error Description.
                                        If strDocType <> "" And strNoErrorDesc = "" And strStatus = "Rejected" Then
                                            If strNoErrorDesc = "" Then
                                                arrErrDesc.Add("No Error Desc")
                                            End If

                                        End If
                                    End If

                                End If

                                If curelement = "PRIA_Document" Then
                                    If reader.Name = "_UniqueIdentifier" Then
                                        If strID = "" Then
                                            strID = reader.Value

                                            Cmd1.CommandText = "select securitytypecount,orderNumber from Orders where UniqueOrderID = '" & strID & "'"

                                            objDR1 = Cmd1.ExecuteReader
                                            While objDR1.Read()
                                                With objDR1

                                                    strSecTypeCnt = .Item("securitytypecount")

                                                End With
                                            End While
                                            objDR1.Close()
                                        ElseIf strID <> "" And strStatus = "Rejected" Then
                                            strSeq = ""
                                        End If


                                    ElseIf reader.Name = "DocumentSequenceIdentifier" Then
                                        strSeq = reader.Value

                                    ElseIf reader.Name = "_code" Then

                                        'this is for Rejected Docs with no Error Description..same code is above in RecordingTransactionIdentifier to handle final doc
                                        If strDocType <> "" And strNoErrorDesc = "" And strStatus = "Rejected" Then
                                            If strNoErrorDesc = "" Then
                                                arrErrDesc.Add("No Error Desc")
                                            End If

                                        End If

                                        strDocType = reader.Value
                                        strTmpDoctype = strDocType
                                        arrDocType.Add(reader.Value)
                                        strNoErrorDesc = ""
                                    End If

                                    If strSecTypeCnt = "1" And strSeq <> "" Then

                                        'convert Leading Zeroes in Seq No
                                        intSeq = Convert.ToInt32(strSeq)
                                        strSeq = intSeq.ToString
                                        tmpseqno = strSeq

                                        arrSeqNum.Add(strSeq)


                                    ElseIf strSecTypeCnt = "2" And strSeq <> "" Then
                                        MyFileReplace = Replace(MyFile, ".xml", "")
                                        strOutboundFileNo = Replace(MyFileReplace.Substring(MyFileReplace.LastIndexOf(".") - 2, 2), ".", "")
                                        intSeq = Convert.ToInt32(strSeq)
                                        strSeq = intSeq.ToString
                                        tmpseqno = strSeq

                                        arrSeqNum.Add(strSeq)
                                    End If


                                End If

                                If strSeq <> "" And strTmpDoctype <> "" And strSecTypeCnt <> "" Then
                                    'strRecDocTag += "<DocType>" & strSeq & " - " & strDocType & " - " & strRecDateTime & "</DocType>" & vbCrLf
                                    strRecDocTag += "<Document>" & vbCrLf & "<DocType>" & strDocType & "</DocType>" & vbCrLf
                                    strRecDocTag += "<ProcessingSequenceNumber>" & strSeq & "</ProcessingSequenceNumber>" & vbCrLf
                                    strTmpDoctype = ""

                                    strSeq = ""

                                End If

                                If strRecDateTime <> "" Then
                                    strRecDocTag += "<RecordDateTime>" & strRecDateTime & "</RecordDateTime>" & vbCrLf
                                    ' strRecDocTag += "<DocumentFees>" & vbCrLf
                                    strRecDateTime = ""
                                End If
                                If strInstrNumber <> "" Then
                                    strRecDocTag += "<InstrumentNumber>" & strInstrNumber & "</InstrumentNumber>" & vbCrLf
                                    strInstrNumber = ""
                                End If


                                If curelement = "Status" And reader.Name = "_Code" Then
                                    strStatus = reader.Value



                                    If strDoc <> "" And strStatus = "recorded" Then

                                        'this little bute keeps /documentfee close tag out of PCOR and other non fee docs..
                                        If strStatus = "Recorded" Then
                                            If strRecDocTag.Substring(strRecDocTag.Length - 8, 6) = "</Fee>" Then
                                                strRecDocTag += "</DocumentFees>" & vbCrLf
                                            End If
                                            strRecDocTag += "<Status>" & strStatus & "</Status>" & vbCrLf & "</Document>" & vbCrLf
                                            'strDoc = ""
                                            ' strStatus = ""
                                            'strDocType = ""
                                            feecounter = 0
                                        End If

                                        If strDocType <> "PCOR" And strDocType <> "TransferDeclaration" And strDocType <> "OtherNonRecorded" Then

                                            If strSecTypeCnt = "1" Then

                                                ' tmpseqno = "1"

                                                Cmd1.CommandText = "select tiffFilename from xmlrequestdocs  where UniqueOrderID = '" & strID & "' and EreDocType = '" & strDocType & "'  and SeqNo = '" & tmpseqno & "'"

                                                objDR1 = Cmd1.ExecuteReader
                                                While objDR1.Read()
                                                    With objDR1

                                                        strTmpOrdNum = .Item("tiffFilename")

                                                    End With
                                                End While
                                                objDR1.Close()

                                            Else

                                                '    ' strOrderNumber2 = MyFile.Substring(MyFile.LastIndexOf("_") - 3, 1)

                                                Cmd1.CommandText = "select tiffFilename from xmlrequestdocs  where UniqueOrderID = '" & strID & "' and OutboundFileNo  = '" & strOutboundFileNo & "' and OutboundSeqNo = '" & tmpseqno & "'"
                                                'End If

                                                objDR1 = Cmd1.ExecuteReader
                                                While objDR1.Read()
                                                    With objDR1

                                                        strTmpOrdNum = .Item("tiffFilename")

                                                    End With
                                                End While
                                                objDR1.Close()


                                            End If




                                            Dim bt64 As Byte() = System.Convert.FromBase64String(strDoc)

                                            Dim sw As New IO.FileStream(MyPath & "ProcessResponses\temp\" & strTmpOrdNum, IO.FileMode.Create)

                                            sw.Write(bt64, 0, bt64.Length)

                                            sw.Close()

                                            ' strLog += "Response " & strStatus & "Tiff file - " & MyPath & "ProcessResponses\temp\" & strTmpOrdNum & "_" & strSeq & ".tiff" & " created..." & vbCrLf

                                            strSeq = ""




                                        End If

                                    End If

                                End If

                                If curelement = "RECORDING_ERROR" Then
                                    If reader.Name = "_Code" Then
                                        arrErrcode.Add(reader.Value)
                                    ElseIf reader.Name = "_Description" Then
                                        arrErrDesc.Add(Replace(Replace(reader.Value, "&", "and"), "'", ""))
                                        strNoErrorDesc = "X"
                                    End If
                                End If


                                If curelement = "_RECORDING_FEE" And reader.Name = "RecordingEndorsementFeeamount" Then
                                    strTmpRecFeeAmt = reader.Value
                                End If

                                If curelement = "_RECORDING_FEE" And reader.Name = "RecordingEndorsementFeeDescription" Then
                                    strRecEndDesc = reader.Value
                                End If

                                If strTmpRecFeeAmt <> "" And strRecEndDesc <> "" Then
                                    If curelement = "_RECORDING_FEE" Then

                                        If feecounter = 0 Then
                                            strRecDocTag += "<DocumentFees>" & vbCrLf

                                            feecounter += 1
                                        End If

                                        strRecDocTag += "<Fee>" & vbCrLf
                                        strRecDocTag += "<Description>" & strRecEndDesc & "</Description>" & vbCrLf
                                        strRecDocTag += "<Amount>" & strTmpRecFeeAmt & "</Amount>" & vbCrLf
                                        strRecDocTag += "</Fee>" & vbCrLf
                                        strTmpRecFeeAmt = ""
                                        strRecEndDesc = ""

                                    End If
                                End If

                                'If strTmpRecFeeAmt <> "" And strRecEndDesc <> "" Then
                                '    If curelement = "_RECORDING_FEE" Then
                                '        ' strRecDocTag += "<RecDocFee>" & strSeq & " - " & strRecEndDesc & " - " & strTmpRecFeeAmt & "</RecDocFee>" & vbCrLf
                                '        strRecDocTag += "<" & strRecEndDesc & ">" & strTmpRecFeeAmt & "</" & strRecEndDesc & ">" & vbCrLf
                                '        strTmpRecFeeAmt = ""
                                '        strRecEndDesc = ""

                                '    End If
                                'End If

                            End While
                        End If

                        '*****
                    Case XmlNodeType.Text
                        If curelement = "DOCUMENT" Then
                            Try
                                strDoc = reader.Value
                            Catch ex As Exception
                                ' Finally

                                strLog += "Error in " & MyFile & "trying to read doc base64 - moved to Error folder " & ex.ToString & vbCrLf
                                RichTextBox1.Text = "Error in " & MyFile & "trying to read doc base64 - moved to Error folder " & ex.ToString & vbCrLf
                                reader.Close()

                                IO.File.Move(MyfullPath, MyPath & "ProcessResponses\processing\errors\" & MyFile)
                                ' MsgBox(ex.ToString & vbCrLf & MyFile & "moved to error folder")
                                Exit Sub

                            End Try



                        End If

                End Select
            End While
        End Using
        conn.Close()


        If strStatus = "Rejected" Then
            Call WriteDPSRejected()
        ElseIf strStatus = "Recorded" Then
            Call WriteDPSRecorded()
        Else
            If IO.File.Exists(MyPath & "ProcessResponses\processing\errors\" & MyFile) Then
                IO.File.Delete(MyPath & "ProcessResponses\processing\errors\" & MyFile)
            End If
            IO.File.Move(MyfullPath, MyPath & "ProcessResponses\processing\errors\" & MyFile)

            IO.File.Delete("C:\Certna\Submitter\Ready\RETRIEVED\" & MyFile)

            strEmailbody = "There is a problem with a Response XML. UniqueOrderID is " & strID & ". The filename is " & MyFile & ". It's does not have a Status of Recorded or Rejected. The file has been moved to ""ProcessResponses\processing\errors\"" folder"
            Call SendEMail()
        End If

    End Sub

    Private Sub eSubmit_Recorded2(ByVal XMLFileName As String)

        Dim curelement As String
        Dim arrFeeDesc As New ArrayList
        Dim arrFeeAmount As New ArrayList

        Dim strOutboundFileNo As String = ""
        Dim MyFileReplace As String = ""
        Dim strSecTypeCnt As String = ""

        Dim conn As New SqlConnection(pub_connectionstring)
        '  Dim conn2 As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand
        ' Dim Cmd2 As New SqlCommand
        Cmd1.Connection = conn
        ' Cmd2.Connection = conn2

        'Dim objDR1 As New SqlDataReader
        conn.Open()
        'conn2.Open()

        curelement = ""

        ' If IO.File.Exists(MyfullPath) Then

        Using reader As XmlReader = XmlReader.Create(XMLFileName)
            ' Parse the file and display each of the nodes.
            While reader.Read()
                Select Case reader.NodeType
                    Case XmlNodeType.Element
                        curelement = reader.Name

                    Case XmlNodeType.Text
                        If curelement = "Id" Then
                            strID = reader.Value
                        ElseIf curelement = "timestamp" Then
                            strTimestamp = reader.Value
                        ElseIf curelement = "orderNumber" Then
                            strOrderNumber = reader.Value
                        ElseIf curelement = "ProcessingSequenceNumber" Then
                            strSeq = reader.Value
                        ElseIf curelement = "Status" Then
                            strStatus = reader.Value
                        ElseIf curelement = "DocType" Then
                            strDocType = reader.Value

                        ElseIf curelement = "InstrumentNumber" Then
                            strInstrNumber = reader.Value
                        ElseIf curelement = "RecordDateTime" Then
                            strRecDateTime = reader.Value
                        ElseIf curelement = "Description" Then
                            arrFeeDesc.Add(reader.Value)
                        ElseIf curelement = "Amount" Then
                            arrFeeAmount.Add(reader.Value)

                        End If


                        If curelement = "timestamp" And strID <> "" Then
                            Cmd1.CommandText = "update orders set ResponseFileCount = ResponseFileCount + 1,  ResponseTimeStamp = '" & strTimestamp & "' where UniqueOrderID = '" & strID & "'"
                            Cmd1.ExecuteNonQuery()

                          
                        End If


                        Cmd1.CommandText = "select securitytypecount,orderNumber from Orders where UniqueOrderID = '" & strID & "'"

                        objDR1 = Cmd1.ExecuteReader
                        While objDR1.Read()
                            With objDR1

                                strSecTypeCnt = .Item("securitytypecount")

                            End With
                        End While
                        objDR1.Close()


                        If curelement = "Status" And strDocType <> "PCOR" And strDocType <> "TransferDeclaration" And strDocType <> "OtherNonRecorded" Then

                            If strSecTypeCnt = "1" Then

                                Cmd1.CommandText = "update XMLRequestDocs  set InstrumentNo = '" & strInstrNumber & "', Recorddatetime = '" & strRecDateTime & "' where UniqueOrderID = '" & strID & "' and EreDocType = '" & strDocType & "' and SeqNo = '" & strSeq & "'"
                                Cmd1.ExecuteNonQuery()
                                strInstrNumber = ""
                                strRecDateTime = ""

                                Cmd1.CommandText = "exec setstatusrecorded '" & strID & "'"
                                Cmd1.ExecuteNonQuery()

                                For i = 0 To arrFeeDesc.Count - 1

                                    Cmd1.CommandText = "insert into RecordingFees (DocRecID,UniqueOrderID,FeeDescription,FeeAmount) select RecID,'" & strID & "','" & arrFeeDesc.Item(i) & "','" & arrFeeAmount.Item(i) & "' from XMLRequestDocs where UniqueOrderID = '" & strID & "' and EreDocType = '" & strDocType & "'  and SeqNo = '" & strSeq & "'"

                                    Cmd1.ExecuteNonQuery()
                                Next
                                arrFeeAmount.Clear()
                                arrFeeDesc.Clear()


                            Else

                                MyFileReplace = Replace(MyFile, ".xml", "")
                                strOutboundFileNo = Replace(MyFileReplace.Substring(MyFileReplace.LastIndexOf(".") - 2, 2), ".", "")
                                ' strSeq = Convert.ToInt32(strSeq)
                                'trSeq = intSeq.ToString

                                Cmd1.CommandText = "update XMLRequestDocs  set InstrumentNo = '" & strInstrNumber & "', Recorddatetime = '" & strRecDateTime & "' where UniqueOrderID = '" & strID & "' and EreDocType = '" & strDocType & "' and outboundfileno = '" & strOutboundFileNo & "' and outboundSeqNo = '" & strSeq & "'"
                                Cmd1.ExecuteNonQuery()
                                strInstrNumber = ""
                                strRecDateTime = ""

                                Cmd1.CommandText = "exec setstatusrecorded '" & strID & "'"
                                Cmd1.ExecuteNonQuery()

                                For i = 0 To arrFeeDesc.Count - 1

                                    Cmd1.CommandText = "insert into RecordingFees (DocRecID,UniqueOrderID,FeeDescription,FeeAmount) select RecID,'" & strID & "','" & arrFeeDesc.Item(i) & "','" & arrFeeAmount.Item(i) & "' from XMLRequestDocs where UniqueOrderID = '" & strID & "' and EreDocType = '" & strDocType & "'  and outboundfileno = '" & strOutboundFileNo & "' and outboundSeqNo = '" & strSeq & "'"

                                    Cmd1.ExecuteNonQuery()
                                Next
                                arrFeeAmount.Clear()
                                arrFeeDesc.Clear()

                            End If
                        End If
                End Select
            End While
        End Using

        conn.Close()
    End Sub

    Private Sub eSubmit_Rejected2(ByVal XMLFileName As String)

        Dim curelement As String
        Dim strProblems As String = ""

        Dim strOutboundFileNo As String = ""
        Dim MyFileReplace As String = ""
        Dim strSecTypeCnt As String = ""

        Dim conn As New SqlConnection(pub_connectionstring)
        '  Dim conn2 As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand
        ' Dim Cmd2 As New SqlCommand
        Cmd1.Connection = conn
        ' Cmd2.Connection = conn2

        'Dim objDR1 As New SqlDataReader
        conn.Open()
        'conn2.Open()

        curelement = ""

        ' If IO.File.Exists(MyfullPath) Then

        Using reader As XmlReader = XmlReader.Create(XMLFileName)
            ' Parse the file and display each of the nodes.
            While reader.Read()
                Select Case reader.NodeType
                    Case XmlNodeType.Element
                        curelement = reader.Name

                    Case XmlNodeType.Text
                        If curelement = "id" Then
                            strID = reader.Value
                        ElseIf curelement = "timestamp" Then
                            strTimestamp = reader.Value
                        ElseIf curelement = "orderNumber" Then
                            strOrderNumber = reader.Value
                        ElseIf curelement = "ProcessingSequenceNumber" Then
                            strSeq = reader.Value
                        ElseIf curelement = "Status" Then
                            strStatus = reader.Value
                        ElseIf curelement = "DocType" Then
                            strDocType = reader.Value

                        ElseIf curelement = "Problems" Then
                            arrErrDesc.Add(reader.Value)
                            'ElseIf curelement = "RecordDateTime" Then
                            '    strRecDateTime = reader.Value
                            'ElseIf curelement = "Description" Then
                            '    arrFeeDesc.Add(reader.Value)
                            'ElseIf curelement = "Amount" Then
                            '    arrFeeAmount.Add(reader.Value)

                        End If

                        'Turned off 9/18/2014 - this didn't work..moved Status update down to where error Desc is added to rejected table
                        'fixed 10/21/2014 - the change above stopped timestamp going to Order table on Rejects
                        If curelement = "timestamp" And strID <> "" Then
                            ' Cmd1.CommandText = "update orders set status = 'Rejected',ResponseFileCount = ResponseFileCount + 1, ResponseTimeStamp = '" & strTimestamp & "'  where UniqueOrderID = '" & strID & "'"
                            Cmd1.CommandText = "update orders set ResponseTimeStamp = '" & strTimestamp & "'  where UniqueOrderID = '" & strID & "'"

                            Cmd1.ExecuteNonQuery()
                        End If


                        Cmd1.CommandText = "select securitytypecount,orderNumber from Orders where UniqueOrderID = '" & strID & "'"

                        objDR1 = Cmd1.ExecuteReader
                        While objDR1.Read()
                            With objDR1

                                strSecTypeCnt = .Item("securitytypecount")

                            End With
                        End While
                        objDR1.Close()

                        If curelement = "Status" And strDocType <> "" Then

                            If strSecTypeCnt = "1" Then

                                For i = 0 To arrErrDesc.Count - 1

                                    Cmd1.CommandText = "insert into RejectedErrors (DocRecID,UniqueOrderID,ErrorDescription) select RecID,'" & strID & "','" & arrErrDesc.Item(i) & "' from XMLRequestDocs where UniqueOrderID = '" & strID & "' and EreDocType = '" & strDocType & "'  and SeqNo = '" & strSeq & "'"
                                    Cmd1.ExecuteNonQuery()

                                Next
                                ' strDocType = ""
                                arrErrDesc.Clear()

                                Cmd1.CommandText = "exec SetStatusRejected '" & strID & "'"
                                Cmd1.ExecuteNonQuery()

                            Else

                                MyFileReplace = Replace(MyFile, ".xml", "")
                                strOutboundFileNo = Replace(MyFileReplace.Substring(MyFileReplace.LastIndexOf(".") - 2, 2), ".", "")

                                For i = 0 To arrErrDesc.Count - 1

                                    Cmd1.CommandText = "insert into RejectedErrors (DocRecID,UniqueOrderID,ErrorDescription) select RecID,'" & strID & "','" & arrErrDesc.Item(i) & "' from XMLRequestDocs where UniqueOrderID = '" & strID & "' and EreDocType = '" & strDocType & "' and outboundfileno = '" & strOutboundFileNo & "' and outboundSeqNo = '" & strSeq & "'"
                                    Cmd1.ExecuteNonQuery()


                                Next
                                ' strDocType = ""
                                arrErrDesc.Clear()

                                Cmd1.CommandText = "exec SetStatusRejected '" & strID & "'"
                                Cmd1.ExecuteNonQuery()
                            End If
                        End If

                End Select
            End While
        End Using

        conn.Close()

    End Sub
    Private Sub eSubmit_ConvertXML(ByVal XMLFileName As String)

        Dim curelement As String

        curelement = ""

        If IO.File.Exists(MyfullPath) Then

            Using reader As XmlReader = XmlReader.Create(XMLFileName)
                ' Parse the file and display each of the nodes.
                While reader.Read()
                    Select Case reader.NodeType
                        Case XmlNodeType.Element
                            curelement = reader.Name

                        Case XmlNodeType.Text

                            If curelement = "orderNumber" Then
                                strOrderNumber = reader.Value
                            ElseIf curelement = "originatorSystem" Then
                                strOrigSys = reader.Value
                            ElseIf curelement = "customerCode" Then
                                strCustomerID = reader.Value
                            ElseIf curelement = "customerID" Then
                                strCustID = reader.Value
                            ElseIf curelement = "payloadID" Then
                                strPayLoadID = reader.Value
                            ElseIf curelement = "ERE" Then  'ERE ie Certna / Secure
                                strERE = reader.Value
                            ElseIf curelement = "EREId" Or curelement = "CertnaId" Then  'ie 257 for FAT Santa Ana
                                strEREId = reader.Value
                            ElseIf curelement = "DocType" Then  'create arraylist for DocType
                                arrDocType.Add(reader.Value)
                            ElseIf curelement = "ProcessingSequenceNumber" Then
                                arrSeqNum.Add(reader.Value)
                            ElseIf curelement = "county" Then
                                strCountyName = reader.Value
                            ElseIf curelement = "state" Then
                                strStateName = reader.Value
                            End If

                            '4/10/2014 changed this routine to only build Tif array. I will validate Tiffs on next sub. I also need to validate MOU and if I move Tiffs too soon it's a problem
                            If curelement = "tiff" Then
                                strCurTiff = reader.Value
                                'If IO.File.Exists(MyPath & "eSubmit\" & strCurTiff & ".tiff") Then
                                'If IO.File.Exists(MyPath & "eSubmit\" & strCurTiff) Then
                                'strLog += strCurTiff & ".tiff - file found" & vbCrLf

                                arrTiff.Add(strCurTiff)   'create arraylist for Tiff filenames



                                '    If IO.File.Exists(MyPath & "CreateRequests\queue\" & strCurTiff) Then
                                '        IO.File.Delete(MyPath & "CreateRequests\queue\" & strCurTiff)
                                '    End If

                                '    IO.File.Move(MyPath & "eSubmit\" & strCurTiff, MyPath & "CreateRequests\queue\" & strCurTiff)


                                'Else

                                '    '    'if a tiff file is missing - write a log entry>>then abort this file>>then move files to Error folder >> and move in to next file
                                '    '    strLog += strCurTiff & ".tiff - file NOT found - file " & MyFile & " aborted and moved to Error folder" & vbCrLf
                                '    '    RichTextBox1.Text = strCurTiff & ".tiff - file NOT found - file " & MyFile & " aborted and moved to Error folder" & vbCrLf

                                '    reader.Close()

                                '    If IO.File.Exists(MyPath & "eSubmit\errors\" & MyFile) Then
                                '        IO.File.Delete(MyPath & "eSubmit\errors\" & MyFile)
                                '    End If

                                '    IO.File.Move(MyfullPath, MyPath & "eSubmit\errors\" & MyFile)

                                '    arrActivity.Add("'Error:missing tiff-" & strCurTiff & "','" & MyFile & "','" & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & "'")
                                '    Call eSubmitMissingTiff() ' this will move the Tiff files that are present to error folder with XML

                                '    ' add activity log entry here for missing tiff file errors. 

                                '    Exit Sub

                                'End If
                            End If


                    End Select
                End While
            End Using

            Dim strDocArr As String = ""

            For i = 0 To arrDocType.Count - 1 'put single quotes around each doc in array of Doctypes so I can query the security types in SQL below i.e. ('doc1','doc2','doc3') -- count(distince(ere1_security_level))

                If strDocArr = "" Then
                    strDocArr = "'" & arrDocType.Item(i) & "'"
                Else
                    strDocArr += ",'" & arrDocType.Item(i) & "'"
                End If

            Next
            Call eSubmitDocTypes(strDocArr)
        End If

    End Sub
    Private Sub eSubmitDocTypes(ByVal DocArrStr As String)

        Dim strsql As String = "SELECT count(distinct(securitytype)) as Cnt from Documents where dpsdoctype in (" & DocArrStr & ")"
        Dim strsql2 As String = ""
        Dim strSecuritycount As String = ""
        Dim strDPSdatabase As String = ""

        Dim k As Int32 = 32
        Dim arrSdocknt As New ArrayList
        Dim y As Int32 = 0
        Dim z As Int32 = 0
        Dim ddoc As New ArrayList

        Dim strOrderExists As String = ""

        Dim conn As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand(strsql, conn)

        Dim seqCnt As Int32 = 0
        Dim Cmd2 As New SqlCommand
        Dim conn2 As New SqlConnection(pub_connectionstring)
        Cmd2.Connection = conn2
        conn2.Open()

        If strCustomerID = "acme_dev" Then
            strDPSdatabase = "qc_acme_1_dev"
        Else
            strDPSdatabase = "qc_" & strCustomerID & "_1"
        End If


        'Validate all Tiffs are present
        For i = 0 To arrTiff.Count - 1

            If Not IO.File.Exists(MyPath & "eSubmit\" & arrTiff.Item(i)) Then

                arrActivity.Add("'Error:missing tiff-" & arrTiff.Item(i) & "','" & MyFile & "','" & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & "'")

                Call eSubmitMissingTiff()
                Exit Sub
            End If
        Next


        Cmd1.CommandText = strsql
        conn.Open()
        objDR1 = Cmd1.ExecuteReader

        'count Security types
        While objDR1.Read()
            With objDR1
                strSecuritycount = .Item("Cnt")
            End With
        End While
        objDR1.Close()
        ' conn.Close()

        'get county and state Fips codes'
        strsql2 = "select state_fips,county_fips from fips_codes where st_abbr = '" & strStateName & "' and county_name = '" & strCountyName & "'"
        Cmd1.CommandText = strsql2
        objDR1 = Cmd1.ExecuteReader
        While objDR1.Read()
            With objDR1
                strStateFips = .Item("state_fips")
                strCountyFips = .Item("county_fips")
            End With
        End While
        objDR1.Close()
        ' conn.Close()

        'check eMOU_YN = Y only check on Direct Submit / FTP
        If chkMOU = "Y" Then
            strsql2 = "select count(*) as 'Cnt' from ChargeCodes where eMOU_YN = 'Y' and CustomerID = '" & strCustomerID & "' and StateFIPS = '" & strStateFips & "' and CountyFIPS = '" & strCountyFips & "'"
            Cmd1.CommandText = strsql2
            objDR1 = Cmd1.ExecuteReader
            While objDR1.Read()
                With objDR1
                    If .Item("Cnt") = 0 Then

                        Call eSubmitNoMou()

                        Exit Sub

                    End If
                End With
            End While
            objDR1.Close()

            chkMOU = ""
        End If

        ' ''get Customer info' this will wait until I have more info on CustomerID
        If strERE = "Certna" Then
            strsql2 = "select CustomerID,CustName,CustAddress,CustCity,CustState,CustZip,CertnaID,SecondaryValue from customer where CertnaId = '" & strEREId & "'"
        ElseIf strERE = "Secure" Then
            strsql2 = "select CustomerID,CustName,CustAddress,CustCity,CustState,CustZip,CertnaID,SecondaryValue from customer where SecureId = '" & strEREId & "'"


        End If

        Cmd1.CommandText = strsql2
        objDR1 = Cmd1.ExecuteReader
        While objDR1.Read()
            With objDR1
                strReqPartyName = .Item("CustName")
                strReqPartyAddress = .Item("CustAddress")
                strReqPartyCity = .Item("CustCity")
                strReqPartyState = .Item("CustState")
                strReqPartyPostalCode = .Item("CustZip")
                strReqPartyIdentifier = .Item("CertnaID")
                strSecondaryValue = .Item("SecondaryValue")
            End With
        End While
        objDR1.Close()
        ' conn.Close()



        Cmd1.CommandText = "Insert into orders(ordernumber,payloadid,customerID,Customercode,dpsdatabase,ERE,ERE_ID,State,County,filename,DocCount,SecurityTypeCount,RequestProcessedDate,status,originatorSystem) Values('" & strOrderNumber & "','" & strPayLoadID & "','" & strCustID & "','" & strCustomerID & "','" & strDPSdatabase & "','" & strERE & "','" & strEREId & "','" & strStateName & "','" & strCountyName & "','" & MyFile & "','" & arrDocType.Count & "','" & strSecuritycount & "', getdate(),'Created','" & strOrigSys & "')"
        Cmd1.ExecuteNonQuery()

        Cmd1.CommandText = "select uniqueOrderID from orders where filename = '" & MyFile & "'"
        objDR1 = Cmd1.ExecuteReader
        While objDR1.Read()
            With objDR1

                strID = .Item("uniqueOrderID")

            End With
        End While
        objDR1.Close()


        For i = 0 To arrDocType.Count - 1
            Cmd1.CommandText = "insert into XMLRequestDocs ( UniqueOrderID,dpsdoctype,SecurityType,EreDocType,SeqNo,TiffFilename) SELECT '" & strID & "',dpsdoctype,securitytype,EreDocType,'" & arrSeqNum.Item(i) & "','" & arrTiff.Item(i) & "' from Documents where dpsdoctype = '" & arrDocType.Item(i) & "' "
            Cmd1.ExecuteNonQuery()
        Next


        'make one or multiple files depending on Security Level count
        'makes one file

        If strSecuritycount = "1" Then
            ' strSecurityType = "1"
            arrEREDocType.Clear()
            arrEREtiffName.Clear()
            strOrderNumber2 = strOrderNumber & "_" & strID

            strsql2 = "SELECT EreDocType,SecurityType,dpsdoctype,TiffFilename from XMLRequestDocs where UniqueOrderID = '" & strID & "' order by SeqNo"
            Cmd1.CommandText = strsql2
            ' conn.Open()
            objDR1 = Cmd1.ExecuteReader
            While objDR1.Read()
                With objDR1

                    arrEREDocType.Add(.Item("EreDocType"))
                    arrEREtiffName.Add(.Item("TiffFilename"))
                    strSecurityType = .Item("SecurityType")

                End With
            End While
            objDR1.Close()
            'conn.Close()

            Call eSubmitCreateReq()

        Else   'two security types make multiple files
            'For x = 1 To arrTiff.Count
            'strSecurityType = x.ToString
            arrSdocknt.Clear()
            arrSecurityType.Clear()

            strsql2 = "SELECT EreDocType,SecurityType,seqno,dpsdoctype,TiffFilename from XMLRequestDocs where UniqueOrderID = '" & strID & "'  order by SeqNo"
            Cmd1.CommandText = strsql2
            'conn.Open()
            objDR1 = Cmd1.ExecuteReader

            'count Security types
            While objDR1.Read()
                With objDR1

                    'strSeq = .Item("seqno")
                    'strEreDocType = .Item("EreDocType")
                    'strERETiffName = .Item("TiffFilename")
                    'strSecurityType = .Item("SecurityType")

                    'arrEREDocType.Add(.Item("EreDocType"))
                    ' arrSeqNum.Add(.Item("seqno"))
                    arrSecurityType.Add(.Item("SecurityType"))
                End With

            End While
            objDR1.Close()

            For x = 0 To arrSecurityType.Count - 1

                If x = 0 Then

                    z = arrSecurityType.Item(0)
                    k = 1

                Else
                    If z = arrSecurityType.Item(x) Then
                        k += 1

                    Else

                        arrSdocknt.Add(k)
                        '  arrSeqNum.Add(x)
                        k = 1
                        z = arrSecurityType.Item(x)

                    End If

                End If

            Next
            arrSdocknt.Add(k)
            '  strOrderNumber2 = strOrderNumber & "_" & strID & "." & x & "." & arrTiff.Count & ""
            For x = 0 To arrSdocknt.Count - 1
                If x = 0 Then
                    ' secKnter =
                    z = "1"
                    y = arrSdocknt.Item(x)
                Else
                    z = y + 1
                    y = z + arrSdocknt.Item(x) - 1

                End If

                arrEREDocType.Clear()
                arrEREtiffName.Clear()
                strsql2 = "SELECT EreDocType,SecurityType,seqno,dpsdoctype,TiffFilename from XMLRequestDocs where UniqueOrderID = '" & strID & "' and seqno between '" & z & "' and '" & y & "'  order by SeqNo"
                Cmd1.CommandText = strsql2
                'conn.Open()
                objDR1 = Cmd1.ExecuteReader

                'count Security types
                seqCnt = 0

                While objDR1.Read()
                    With objDR1

                        seqCnt += 1
                        arrEREDocType.Add(.Item("EreDocType"))
                        arrEREtiffName.Add(.Item("TiffFilename"))
                        strSecurityType = .Item("SecurityType")


                        Cmd2.CommandText = "update XMLRequestDocs set outboundfileno = '" & x + 1 & "', OutboundSeqNo = '" & seqCnt & "' where UniqueOrderID = '" & strID & "' and TiffFilename = '" & .Item("TiffFilename") & "'"
                        Cmd2.ExecuteNonQuery()


                    End With

                End While
                objDR1.Close()

                strOrderNumber2 = strOrderNumber & "_" & strID & "." & x + 1 & "." & arrSdocknt.Count & ""

                

                Call eSubmitCreateReq()
            Next

        End If
        conn.Close()
        conn2.Close()
    End Sub
    Private Sub eSubmitNoMou()
        Dim conn As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand
        Dim outfile As StreamWriter
        Dim strErr As String = ""

        Cmd1.Connection = conn
        conn.Open()


        If IO.File.Exists(MyPath & "FTP\" & strFTPFolder & "\errors\" & MyFile) Then
            IO.File.Delete(MyPath & "FTP\" & strFTPFolder & "\errors\" & MyFile)
        End If

        IO.File.Move(MyfullPath, MyPath & "FTP\" & strFTPFolder & "\errors\" & MyFile)

        For i = 0 To arrTiff.Count - 1

            If IO.File.Exists(MyPath & "FTP\" & strFTPFolder & "\errors\" & arrTiff.Item(i)) Then
                IO.File.Delete(MyPath & "FTP\" & strFTPFolder & "\errors\" & arrTiff.Item(i))
            End If


            IO.File.Move(MyPath & "eSubmit\" & arrTiff.Item(i), MyPath & "FTP\" & strFTPFolder & "\errors\" & arrTiff.Item(i))

        Next
        arrActivity.Add("'Error:MOU not found','" & MyFile & "','" & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & "'")

        strErr = MyFile & " - Error:MOU not found" & vbCrLf

        Cmd1.CommandText = "Insert into orders(ordernumber,state,county,CustomerID,filename,status,RequestProcessedDate) Values('" & strOrderNumber & "','" & strStateName & "','" & strCountyName & "','" & strCustomerID & "','" & MyFile & "','Error-No MOU', getdate())"
        Cmd1.ExecuteNonQuery()

        Cmd1.CommandText = "select uniqueOrderID from orders where filename = '" & MyFile & "'"
        objDR1 = Cmd1.ExecuteReader
        While objDR1.Read()
            With objDR1

                strID = .Item("uniqueOrderID")

            End With
        End While
        objDR1.Close()


        For i = 0 To arrActivity.Count - 1
            Cmd1.CommandText = "Insert into Activity(UniqueOrderID,Activity,XMLFilename,ActivityDateTime) Values('" & strID & "'," & arrActivity.Item(i) & ")"
            Cmd1.ExecuteNonQuery()
        Next

        ''Write to Error log
        If IO.File.Exists(MyPath & "FTP\" & strFTPFolder & "\errors\" & "errors_" & strdate & ".txt") Then
            outfile = IO.File.AppendText(MyPath & "FTP\" & strFTPFolder & "\errors\" & "errors_" & strdate & ".txt")
            outfile.WriteLine(strErr)
            outfile.Flush()
            outfile.Close()
        Else
            outfile = IO.File.CreateText(MyPath & "FTP\" & strFTPFolder & "\errors\" & "errors_" & strdate & ".txt")
            outfile.WriteLine(strErr)
            outfile.Flush()
            outfile.Close()
        End If

        conn.Close()
        arrActivity.Clear()
    End Sub

    Private Sub eSubmitCreateReq()

        Dim conn As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand


        Dim timestamp As String = ""
        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        timestamp = Replace(timestamp, " ", "T")
        xmlstr = "<QC-v1.2>" & vbCrLf
        xmlstr += "<id>" & strID & "</id>" & vbCrLf
        xmlstr += "<orderNumber>" & strOrderNumber2 & "</orderNumber>" & vbCrLf
        If strSecondaryValue <> "" Then
            xmlstr += "<SecondaryValue>" & strSecondaryValue & "</SecondaryValue>" & vbCrLf
        End If

        xmlstr += "<SecurityType>" & strSecurityType & "</SecurityType>" & vbCrLf

        For i = 0 To arrEREtiffName.Count - 1

            xmlstr += "<file>"
            xmlstr += "<tiff>" & arrEREtiffName.Item(i) & "</tiff>" & vbCrLf
            xmlstr += "<DocType>" & arrEREDocType.Item(i) & "</DocType>" & vbCrLf
            xmlstr += "<ProcessingSeqNumber>" & i + 1 & "</ProcessingSeqNumber>" & vbCrLf
            xmlstr += "</file>" & vbCrLf

            'move tif files to the queue folder
            If IO.File.Exists(MyPath & "CreateRequests\queue\" & arrEREtiffName.Item(i)) Then
                IO.File.Delete(MyPath & "CreateRequests\queue\" & arrEREtiffName.Item(i))
            End If

            IO.File.Move(MyPath & "eSubmit\" & arrEREtiffName.Item(i), MyPath & "CreateRequests\queue\" & arrEREtiffName.Item(i))

        Next

        xmlstr += "<requestingParty>" & vbCrLf
        xmlstr += "<reqPartyName>" & strReqPartyName & "</reqPartyName>" & vbCrLf
        xmlstr += "<StreetAddress>" & strReqPartyAddress & "</StreetAddress>" & vbCrLf
        xmlstr += "<City>" & strReqPartyCity & "</City>" & vbCrLf
        xmlstr += "<State>" & strReqPartyState & "</State>" & vbCrLf
        xmlstr += "<PostalCode>" & strReqPartyPostalCode & "</PostalCode>" & vbCrLf
        xmlstr += "<Identifier>" & strReqPartyIdentifier & "</Identifier>" & vbCrLf
        xmlstr += "</requestingParty>" & vbCrLf
        xmlstr += "<submittingParty>" & vbCrLf
        xmlstr += "<LoginAccountIdentifier>rsherman</LoginAccountIdentifier>" & vbCrLf
        If strStateFips = "06" And strCountyFips = "077" Then
            xmlstr += "<submitPartyName>AGENT - DOCUMENT PROCESSING SOLUTIONS INC</submitPartyName>" & vbCrLf
        Else
            xmlstr += "<submitPartyName>Document Processing Solutions,Inc</submitPartyName>" & vbCrLf
        End If
        xmlstr += "</submittingParty>" & vbCrLf
        xmlstr += "<timestamp>" & timestamp & "</timestamp>" & vbCrLf
        xmlstr += "<type>Request</type>" & vbCrLf
        xmlstr += "<countyFips>" & strCountyFips & "</countyFips>" & vbCrLf
        xmlstr += "<stateFips>" & strStateFips & "</stateFips>" & vbCrLf
        xmlstr += "</QC-v1.2>" & vbCrLf

        Using outfile As New StreamWriter(MyPath & "CreateRequests\queue\" & strOrderNumber2 & "_request.xml")
            outfile.Write(xmlstr.ToString)
        End Using

        If System.IO.File.Exists(MyfullPath) Then

            If System.IO.File.Exists(MyPath & "eSubmit\processed\" & MyFile) Then
                IO.File.Delete(MyPath & "eSubmit\processed\" & MyFile)
            End If

            IO.File.Move(MyfullPath, MyPath & "eSubmit\processed\" & MyFile)
        End If

    End Sub
    Private Sub eSubmitCreateReq_2()

        Dim timestamp As String = ""
        timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        timestamp = Replace(timestamp, " ", "T")
        xmlstr = "<QC-v1.2>" & vbCrLf
        xmlstr += "<id>" & strID & "</id>" & vbCrLf
        xmlstr += "<orderNumber>" & strOrderNumber2 & "</orderNumber>" & vbCrLf
        If strSecondaryValue <> "" Then
            xmlstr += "<SecondaryValue>" & strSecondaryValue & "</SecondaryValue>" & vbCrLf
        End If

        xmlstr += "<SecurityType>" & strSecurityType & "</SecurityType>" & vbCrLf

        ' For i = 0 To arrEREtiffName.Count - 1

        xmlstr += "<file>"
        xmlstr += "<tiff>" & strERETiffName & "</tiff>" & vbCrLf
        xmlstr += "<DocType>" & strEreDocType & "</DocType>" & vbCrLf
        xmlstr += "<ProcessingSeqNumber>" & strSeq & "</ProcessingSeqNumber>" & vbCrLf
        xmlstr += "</file>" & vbCrLf

        'move tif files to the queue folder
        If IO.File.Exists(MyPath & "CreateRequests\queue\" & strERETiffName) Then
            IO.File.Delete(MyPath & "CreateRequests\queue\" & strERETiffName)
        End If

        IO.File.Move(MyPath & "eSubmit\" & strERETiffName, MyPath & "CreateRequests\queue\" & strERETiffName)

        ' Next

        xmlstr += "<requestingParty>" & vbCrLf
        xmlstr += "<reqPartyName>" & strReqPartyName & "</reqPartyName>" & vbCrLf
        xmlstr += "<StreetAddress>" & strReqPartyAddress & "</StreetAddress>" & vbCrLf
        xmlstr += "<City>" & strReqPartyCity & "</City>" & vbCrLf
        xmlstr += "<State>" & strReqPartyState & "</State>" & vbCrLf
        xmlstr += "<PostalCode>" & strReqPartyPostalCode & "</PostalCode>" & vbCrLf
        xmlstr += "<Identifier>" & strReqPartyIdentifier & "</Identifier>" & vbCrLf
        xmlstr += "</requestingParty>" & vbCrLf
        xmlstr += "<submittingParty>" & vbCrLf
        xmlstr += "<LoginAccountIdentifier>rsherman</LoginAccountIdentifier>" & vbCrLf
        xmlstr += "<submitPartyName>Document Processing Solutions,Inc</submitPartyName>" & vbCrLf
        xmlstr += "</submittingParty>" & vbCrLf
        xmlstr += "<timestamp>" & timestamp & "</timestamp>" & vbCrLf
        xmlstr += "<type>Request</type>" & vbCrLf
        xmlstr += "<countyFips>" & strCountyFips & "</countyFips>" & vbCrLf
        xmlstr += "<stateFips>" & strStateFips & "</stateFips>" & vbCrLf
        xmlstr += "</QC-v1.2>" & vbCrLf

        Using outfile As New StreamWriter(MyPath & "CreateRequests\queue\" & strOrderNumber2 & "_request.xml")
            outfile.Write(xmlstr.ToString)
        End Using

        If System.IO.File.Exists(MyfullPath) Then

            If System.IO.File.Exists(MyPath & "eSubmit\processed\" & MyFile) Then
                IO.File.Delete(MyPath & "eSubmit\processed\" & MyFile)
            End If

            IO.File.Move(MyfullPath, MyPath & "eSubmit\processed\" & MyFile)
        End If

    End Sub

    Private Sub btnWriteXML_Click()
        Dim connsql As New SqlConnection(pub_connectionstring)
        Dim cmdsql As New SqlCommand

        cmdsql.Connection = connsql


        Dim strKeyValues As String = ""
        Dim sb As New System.Text.StringBuilder
        Dim srcBT As Byte()
        Dim dest As String
        xmlstr = ""
        xmlstr = xmlstr & "<REQUEST_GROUP PRIAVersionIdentifier=""2.4.2"">" & vbCrLf
        xmlstr = xmlstr & "  <REQUESTING_PARTY _Name=""rp_reqname"" _StreetAddress=""rp_streetaddress"" _StreetAddress2="""" _City=""rp_city"" _State=""rp_state"" _PostalCode=""rp_postalcode"" _Identifier=""rp_identifier""/>" & vbCrLf
        xmlstr = xmlstr & "  <SUBMITTING_PARTY LoginAccountIdentifier=""rp_loginAccntID"" _Name=""rp_submitname""/>" & vbCrLf
        xmlstr = xmlstr & "  <REQUEST RequestDateTime=""rp_requestdatetime"">" & vbCrLf
        xmlstr = xmlstr & "    <KEY _Name=""Comment"" _Value=""rp_Keyvalues""/>" & vbCrLf
        xmlstr = xmlstr & "    <PRIA_REQUEST _RelatedDocumentsIndicator=""true"">" & vbCrLf
        xmlstr = xmlstr & "      <PACKAGE CountyFIPSCode=""rp_countyFIPSCode"" StateFIPSCode=""rp_StFips"" SecurityType=""rp_SecurityType"" Priority=""Standard"">" & vbCrLf

        ' TIFF Documents - get page count - convert to base64 string - 
        '*************
        Dim TiffFileCounter As Integer = 0


        For i = 0 To arrTiff.Count - 1

            TiffFileCounter = TiffFileCounter + 1

            MyTiffFile = arrTiff.Item(i)
            strDocType = arrDocType.Item(i)

            If strKeyValues = "" Then
                strKeyValues = arrDocType.Item(i)
            Else
                strKeyValues += "-" & arrDocType.Item(i)
            End If

            MyTifffullPath = MyTiffPath & MyTiffFile

            img = Image.FromFile(MyTifffullPath)
            numpages = img.GetFrameCount(Imaging.FrameDimension.Page)

            img.Dispose()

            '********
            dest = ""
            xmlstr = xmlstr & "         <PRIA_DOCUMENT _Code=" & Chr(34) & strDocType & Chr(34) & " DocumentSequenceIdentifier=" & Chr(34) & TiffFileCounter & Chr(34) & " _UniqueIdentifier=" & Chr(34) & strID & Chr(34) & ">" & vbCrLf
            xmlstr = xmlstr & "            <EMBEDDED_FILE _PagesCount=" & Chr(34) & numpages & Chr(34) & ">" & vbCrLf
            If System.IO.File.Exists(MyTifffullPath) Then
                Dim sr As New System.IO.FileStream(MyTifffullPath, IO.FileMode.Open)
                ReDim srcBT(sr.Length)
                sr.Read(srcBT, 0, sr.Length)
                sr.Close()
                dest = System.Convert.ToBase64String(srcBT)
            End If
            xmlstr = xmlstr & "               <DOCUMENT>" & dest & "</DOCUMENT>" & vbCrLf
            xmlstr = xmlstr & "            </EMBEDDED_FILE>" & vbCrLf
            xmlstr = xmlstr & "         </PRIA_DOCUMENT>" & vbCrLf

            '********
            If IO.File.Exists(MyPath & "CreateRequests\processed\" & MyTiffFile) Then
                IO.File.Delete(MyPath & "CreateRequests\processed\" & MyTiffFile)
            End If


            IO.File.Move(MyTifffullPath, MyPath & "CreateRequests\processed\" & MyTiffFile)

            strLog += MyTiffFile & " converted to base64 and added to Document" & vbCrLf



        Next


        xmlstr = xmlstr & "     </PACKAGE>" & vbCrLf
        xmlstr = xmlstr & "     <RECORDING_TRANSACTION_IDENTIFIER _Value=""rp_OrderNumber"" SecondaryValue=""rp_SecondaryValue""/>" & vbCrLf
        xmlstr = xmlstr & "    </PRIA_REQUEST>" & vbCrLf
        xmlstr = xmlstr & "  </REQUEST>" & vbCrLf
        xmlstr = xmlstr & "</REQUEST_GROUP>" & vbCrLf

        xmlstr = Replace(xmlstr, "rp_reqname", strReqPartyName)
        xmlstr = Replace(xmlstr, "rp_streetaddress", strReqPartyAddress)
        'xmlstr = Replace(xmlstr, "rp_strtadd2", "")
        xmlstr = Replace(xmlstr, "rp_city", strReqPartyCity)
        xmlstr = Replace(xmlstr, "rp_state", strReqPartyState)
        xmlstr = Replace(xmlstr, "rp_postalcode", strReqPartyPostalCode)
        xmlstr = Replace(xmlstr, "rp_identifier", strReqPartyIdentifier)
        xmlstr = Replace(xmlstr, "rp_OrderNumber", strOrderNumber)

        xmlstr = Replace(xmlstr, "rp_submitname", strSubmitPartyname)
        xmlstr = Replace(xmlstr, "rp_loginAccntID", strSubmitLoginAccntID)
        xmlstr = Replace(xmlstr, "rp_StFips", strStateFips)
        xmlstr = Replace(xmlstr, "rp_SecurityType", strSecurityType)

        xmlstr = Replace(xmlstr, "rp_countyFIPSCode", strCountyFips)


        xmlstr = Replace(xmlstr, "rp_SecurityType", strSecurityType)
        xmlstr = Replace(xmlstr, "rp_SecondaryValue", strSecondaryValue)


        ' xmlstr = Replace(xmlstr, "rp_keyvalues", strKeyValues)
        xmlstr = Replace(xmlstr, "rp_keyvalues", "")


        xmlstr = Replace(xmlstr, "rp_requestdatetime", Replace(strTimestamp, " ", "T"))

        If System.IO.File.Exists(MyPath & "CreateRequests\requests_RTS\" & strOrderNumber & ".xml") Then
            IO.File.Delete(MyPath & "CreateRequests\requests_RTS\" & strOrderNumber & ".xml")
        End If

        Using outfile As New StreamWriter(MyPath & "CreateRequests\requests_RTS\" & strOrderNumber & ".xml")
            outfile.Write(xmlstr.ToString)

            strLog += "Request file - " & MyFile & " created..." & vbCrLf
        End Using

        'delete file if another exists in the processed folder
        If IO.File.Exists(MyPath & "CreateRequests\processed\" & MyFile) Then
            IO.File.Delete(MyPath & "CreateRequests\processed\" & MyFile)
        End If

        IO.File.Move(MyfullPath, MyPath & "CreateRequests\processed\" & MyFile)


        connsql.Open()
        cmdsql.CommandText = "insert into OutboundXMLFiles (UniqueOrderID,ReqFilename) Values('" & strID & "','" & strOrderNumber & ".xml')"
        cmdsql.ExecuteNonQuery()
        connsql.Close()




    End Sub
    Private Sub WriteDPSRejected()

        Dim dest As String = ""
        xmlstr = ""
        xmlstr = xmlstr & "<?xml version=""1.0""?>" & vbCrLf
        xmlstr = xmlstr & "<QC-Rejected-v1>" & vbCrLf
        xmlstr = xmlstr & "<id>" & strID & "</id>" & vbCrLf
        xmlstr = xmlstr & "<OrderNumber>" & strOrderNumber & "</OrderNumber>" & vbCrLf
        If strSecondaryValue <> "" Then
            xmlstr = xmlstr & "<SecondaryValue>" & strSecondaryValue & "</SecondaryValue>" & vbCrLf
        End If
        xmlstr = xmlstr & "<LoginAccountIdentifier>" & strSubmitLoginAccntID & "</LoginAccountIdentifier>" & vbCrLf
        xmlstr = xmlstr & "<timestamp>" & strTimestamp & "</timestamp>" & vbCrLf
        xmlstr = xmlstr & "<countyFips>" & strCountyFips & "</countyFips>" & vbCrLf
        xmlstr = xmlstr & "<stateFips>" & strStateFips & "</stateFips>" & vbCrLf
        xmlstr = xmlstr & "<errors>" & vbCrLf

        For i = 0 To arrDocType.Count - 1
            ' xmlstr = xmlstr & "<Document>" & arrSeqNum.Item(i) & " - " & arrDocType.Item(i) & " - " & arrErrcode.Item(i) & " - " & arrErrDesc.Item(i) & "</Document>" & vbCrLf
            xmlstr = xmlstr & "<Document>" & vbCrLf

            xmlstr = xmlstr & "<ProcessingSequenceNumber>" & arrSeqNum.Item(i) & "</ProcessingSequenceNumber>" & vbCrLf
            ' xmlstr = xmlstr & "<ProcessingSequenceNumber>" & i + 1 & "</ProcessingSequenceNumber>" & vbCrLf
            xmlstr = xmlstr & "<DocType>" & arrDocType.Item(i) & "</DocType>" & vbCrLf
            xmlstr = xmlstr & "<Problems>" & arrErrDesc.Item(i) & "</Problems>" & vbCrLf
            xmlstr = xmlstr & "<status>Rejected</status>" & vbCrLf
            xmlstr = xmlstr & "</Document>" & vbCrLf
        Next

        xmlstr = xmlstr & "</errors>" & vbCrLf
        xmlstr = xmlstr & "</QC-Rejected-v1>" & vbCrLf


        If IO.File.Exists(MyPath & "ProcessResponses\rejected\" & strOrderNumber & "_" & strStatus & "_response.xml") Then
            IO.File.Delete(MyPath & "ProcessResponses\rejected\" & strOrderNumber & "_" & strStatus & "_response.xml")
        End If

        Using outfile As New StreamWriter(MyPath & "ProcessResponses\rejected\" & strOrderNumber & "_" & strStatus & "_response.xml")
            outfile.Write(xmlstr.ToString)

            strLog += "Response ""Rejected"" file - " & strOrderNumber & "_" & strStatus & "_response.xml" & " created..." & vbCrLf
        End Using

        IO.File.Move(MyfullPath, MyPath & "ProcessResponses\processed\" & MyFile)

        If IO.File.Exists(MyPath & "ProcessResponses\processed\" & MyFile) Then
            strLog += "Response file - " & MyFile & " moved to ProcessResponses\processed folder.." & vbCrLf
        Else
            MsgBox("Response File missing")

        End If

    End Sub
    Private Sub WriteDPSRecorded()

        Dim dest As String = ""
        xmlstr = ""
        xmlstr = xmlstr & "<?xml version=""1.0""?>" & vbCrLf
        xmlstr = xmlstr & "<QC-Recorded-v1>" & vbCrLf
        xmlstr = xmlstr & "<id>" & strID & "</id>" & vbCrLf
        xmlstr = xmlstr & "<OrderNumber>" & strOrderNumber & "</OrderNumber>" & vbCrLf
        If strSecondaryValue <> "" Then
            xmlstr = xmlstr & "<SecondaryValue>" & strSecondaryValue & "</SecondaryValue>" & vbCrLf
        End If

        xmlstr = xmlstr & "<timestamp>" & strTimestamp & "</timestamp>" & vbCrLf
        xmlstr = xmlstr & "<countyFips>" & strCountyFips & "</countyFips>" & vbCrLf
        xmlstr = xmlstr & "<stateFips>" & strStateFips & "</stateFips>" & vbCrLf
        xmlstr = xmlstr & strRecDocTag & vbCrLf
        'xmlstr = xmlstr & "<status>" & strStatus & "</status>" & vbCrLf
        xmlstr = xmlstr & "</QC-Recorded-v1>" & vbCrLf


        If IO.File.Exists(MyPath & "ProcessResponses\recorded\" & strID & "_" & strStatus & "_response.xml") Then
            IO.File.Delete(MyPath & "ProcessResponses\recorded\" & strID & "_" & strStatus & "_response.xml")
        End If

        Using outfile As New StreamWriter(MyPath & "ProcessResponses\recorded\" & strOrderNumber & "_" & strStatus & "_response.xml")
            outfile.Write(xmlstr.ToString)

            strLog += "Response ""Recorded"" file - " & strOrderNumber & "_" & strStatus & "_response.xml" & " created..." & vbCrLf
        End Using

        'move Certna_Response file to done
        If IO.File.Exists(MyPath & "ProcessResponses\processed\" & MyFile) Then
            IO.File.Delete(MyPath & "ProcessResponses\processed\" & MyFile)
        End If
        IO.File.Move(MyfullPath, MyPath & "ProcessResponses\processed\" & MyFile)

        'If IO.File.Exists(MyPath & "ProcessResponses\processed\" & MyFile) Then
        '    strLog += "Response file - " & MyFile & " moved to ProcessResponses\processed folder.." & vbCrLf
        'Else
        '    MsgBox("Response File missing")
        'End If
    End Sub
    Private Sub eSubmitWriteRecorded()

        Dim dest As String = ""
        Dim conn As New SqlConnection(pub_connectionstring)
        Dim conn2 As New SqlConnection(pub_connectionstring)
        Dim conn3 As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand
        Dim Cmd2 As New SqlCommand
        Dim Cmd3 As New SqlCommand
        Cmd1.Connection = conn
        Cmd2.Connection = conn2
        Cmd3.Connection = conn3

        'Dim objDR1 As New SqlDataReader
        conn.Open()
        conn2.Open()
        conn3.Open()

        'build <document> tags
        Cmd3.CommandText = "select UniqueOrderID, ordernumber,county,state,responsetimestamp from orders where Status = 'Recorded' and (SecurityTypeCount = '1' or (SecurityTypeCount = '2' and ResponseFileCount = DocCount)) and DPSUpdated is null"
        objDR3 = Cmd3.ExecuteReader
        While objDR3.Read()
            With objDR3
                strID = .Item("UniqueOrderID")
                strOrderNumber = .Item("ordernumber")
                strCountyName = .Item("county")
                strStateName = .Item("state")
                strTimestamp = .Item("responsetimestamp")
            End With

            strRecDocTag = ""
            xmlstr = ""
            xmlstr = xmlstr & "<?xml version=""1.0""?>" & vbCrLf
            xmlstr = xmlstr & "<QC-Recorded-v1>" & vbCrLf
            xmlstr = xmlstr & "<OrderNumber>" & strOrderNumber & "</OrderNumber>" & vbCrLf



            xmlstr = xmlstr & "<timestamp>" & strTimestamp & "</timestamp>" & vbCrLf
            xmlstr = xmlstr & "<County>" & strCountyName & "</County>" & vbCrLf
            xmlstr = xmlstr & "<State>" & strStateName & "</State>" & vbCrLf



            Cmd1.CommandText = "select recid,dpsdoctype,seqno,instrumentno,recorddatetime from XMLRequestDocs where UniqueOrderID = '" & strID & "' order by SeqNo"
            objDR1 = Cmd1.ExecuteReader


            While objDR1.Read()
                With objDR1

                    strRecDocTag += "<Document>" & vbCrLf
                    strRecDocTag += "<Doctype>" & .Item("dpsdoctype") & "</Doctype>" & vbCrLf
                    strRecDocTag += "<ProcessingSequenceNumber>" & .Item("seqno") & "</ProcessingSequenceNumber>" & vbCrLf
                    If IsDBNull(.Item("instrumentno")) = False Then
                        strRecDocTag += "<InstrumenNumber>" & .Item("instrumentno") & "</InstrumenNumber>" & vbCrLf
                        strRecDocTag += "<RecordDatetime>" & .Item("recorddatetime") & "</RecordDatetime>" & vbCrLf
                    End If
                    strRecDocTag += "<Status>Recorded</Status>" & vbCrLf

                    If .Item("dpsdoctype") <> "P C O R" Then
                        Cmd2.CommandText = "select FeeDescription,FeeAmount from RecordingFees where DocRecID = '" & .Item("recid") & "'"
                        strRecDocTag += "<DocumentFees>" & vbCrLf
                        'conn2.Open()
                        objDR2 = Cmd2.ExecuteReader
                        While objDR2.Read
                            With objDR2
                                strRecDocTag += "<Fee>" & vbCrLf
                                strRecDocTag += "<Description>" & .Item("FeeDescription") & "</Description>" & vbCrLf
                                strRecDocTag += "<Amount>" & .Item("FeeAmount") & "</Amount>" & vbCrLf
                                strRecDocTag += "</Fee>" & vbCrLf
                            End With
                        End While
                        objDR2.Close()
                        strRecDocTag += "</DocumentFees>" & vbCrLf
                    End If

                End With
                strRecDocTag += "</Document>" & vbCrLf


            End While

            objDR1.Close()

            Cmd2.CommandText = "update orders set ReturnProcessedDate = getdate() where UniqueOrderID = '" & strID & "'"
            Cmd2.ExecuteNonQuery()




            xmlstr = xmlstr & strRecDocTag & vbCrLf

            xmlstr = xmlstr & "</QC-Recorded-v1>" & vbCrLf


            If IO.File.Exists(MyPath & "ProcessResponses\recorded\" & strID & "_recorded_response.xml") Then
                IO.File.Delete(MyPath & "ProcessResponses\recorded\" & strID & "_recorded_response.xml")
            End If

            Using outfile As New StreamWriter(MyPath & "FTP\FirstAmTtl\Responses\" & strCustomerID & "_" & strOrderNumber & "_" & DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss") & "_recorded.xml")
                outfile.Write(xmlstr.ToString)
            End Using

            'move Certna_Response file to done
            'If IO.File.Exists(MyPath & "ProcessResponses\processed\" & MyFile) Then
            '    IO.File.Delete(MyPath & "ProcessResponses\processed\" & MyFile)
            'End If
            ' IO.File.Move(MyfullPath, MyPath & "ProcessResponses\processed\" & MyFile)

            'If IO.File.Exists(MyPath & "ProcessResponses\processed\" & MyFile) Then
            '    strLog += "Response file - " & MyFile & " moved to ProcessResponses\processed folder.." & vbCrLf
            'Else
            '    MsgBox("Response File missing")
            'End If
        End While
        objDR3.Close()
        conn.Close()
        conn2.Close()
        conn3.Close()
    End Sub
    Private Sub eSubmitWriteRejected()

        Dim dest As String = ""
        Dim conn As New SqlConnection(pub_connectionstring)
        Dim conn2 As New SqlConnection(pub_connectionstring)
        Dim conn3 As New SqlConnection(pub_connectionstring)
        Dim Cmd1 As New SqlCommand
        Dim Cmd2 As New SqlCommand
        Dim Cmd3 As New SqlCommand
        Cmd1.Connection = conn
        Cmd2.Connection = conn2
        Cmd3.Connection = conn3

        conn.Open()
        conn2.Open()
        conn3.Open()

        'build <document> tags
        Cmd3.CommandText = "select UniqueOrderID, ordernumber,county,state,responsetimestamp from orders where Status = 'Rejected' and (SecurityTypeCount = '1' or (SecurityTypeCount = '2' and ResponseFileCount = DocCount)) and DPSUpdated is null"
        objDR3 = Cmd3.ExecuteReader
        While objDR3.Read()
            With objDR3
                strID = .Item("UniqueOrderID")
                strOrderNumber = .Item("ordernumber")
                strCountyName = .Item("county")
                strStateName = .Item("state")
                strTimestamp = .Item("responsetimestamp")
            End With

            strRecDocTag = ""
            xmlstr = ""
            xmlstr = xmlstr & "<?xml version=""1.0""?>" & vbCrLf
            xmlstr = xmlstr & "<QC-Rejected-v1>" & vbCrLf
            xmlstr = xmlstr & "<OrderNumber>" & strOrderNumber & "</OrderNumber>" & vbCrLf

            xmlstr = xmlstr & "<timestamp>" & strTimestamp & "</timestamp>" & vbCrLf
            xmlstr = xmlstr & "<County>" & strCountyName & "</County>" & vbCrLf
            xmlstr = xmlstr & "<State>" & strStateName & "</State>" & vbCrLf
            xmlstr = xmlstr & "<errors>" & vbCrLf


            Cmd1.CommandText = "select recid,dpsdoctype,seqno from XMLRequestDocs where UniqueOrderID = '" & strID & "' order by SeqNo"
            objDR1 = Cmd1.ExecuteReader

            While objDR1.Read()
                With objDR1

                    strRecDocTag += "<Document>" & vbCrLf
                    strRecDocTag += "<Doctype>" & .Item("dpsdoctype") & "</Doctype>" & vbCrLf
                    strRecDocTag += "<ProcessingSequenceNumber>" & .Item("seqno") & "</ProcessingSequenceNumber>" & vbCrLf

                    Cmd2.CommandText = "select ErrorDescription from RejectedErrors where DocRecID = '" & .Item("recid") & "'"

                    'conn2.Open()
                    objDR2 = Cmd2.ExecuteReader
                    While objDR2.Read
                        With objDR2

                            strRecDocTag += "<Problems>" & .Item("errorDescription") & "</Problems>" & vbCrLf

                        End With
                    End While
                    objDR2.Close()

                End With
                strRecDocTag += "<Status>Rejected</Status>" & vbCrLf
                strRecDocTag += "</Document>" & vbCrLf


            End While
            strRecDocTag += "</errors>" & vbCrLf

            objDR1.Close()

            Cmd2.CommandText = "update orders set ReturnProcessedDate = getdate() where UniqueOrderID = '" & strID & "'"
            Cmd2.ExecuteNonQuery()

            xmlstr = xmlstr & strRecDocTag & vbCrLf

            xmlstr = xmlstr & "</QC-Rejected-v1>" & vbCrLf


            If IO.File.Exists(MyPath & "ProcessResponses\rejected\returnFTP\" & strOrderNumber & "_rejected_response.xml") Then
                IO.File.Delete(MyPath & "ProcessResponses\rejected\returnFTP\" & strOrderNumber & "_rejected_response.xml")
            End If

            Using outfile As New StreamWriter(MyPath & "FTP\FirstAmTtl\Responses\" & strCustomerID & "_" & strOrderNumber & "_" & DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss") & "_rejected.xml")
                outfile.Write(xmlstr.ToString)

                ' strLog += "Response ""Recorded"" file - " & strOrderNumber & "_" & strStatus & "_response.xml" & " created..." & vbCrLf
            End Using

            'move Certna_Response file to done
            'If IO.File.Exists(MyPath & "ProcessResponses\processed\" & MyFile) Then
            '    IO.File.Delete(MyPath & "ProcessResponses\processed\" & MyFile)
            'End If
            ' IO.File.Move(MyfullPath, MyPath & "ProcessResponses\processed\" & MyFile)

            'If IO.File.Exists(MyPath & "ProcessResponses\processed\" & MyFile) Then
            '    strLog += "Response file - " & MyFile & " moved to ProcessResponses\processed folder.." & vbCrLf
            'Else
            '    MsgBox("Response File missing")
            'End If
        End While
        objDR3.Close()
        conn.Close()
        conn2.Close()
        conn3.Close()

    End Sub
    Private Sub esubmitTifPageOne()
        ' Dim outfile As StreamWriter
        Dim startInfo As New ProcessStartInfo
        Dim strtiffpg1 As String = ""
        startInfo.FileName = MyPath & "convert2pdf.cmd"
        startInfo.UseShellExecute = False




        Using outfile As New StreamWriter(MyPath & "TifPageOne.cmd")
            outfile.Write(strtiffpg1.ToString)
        End Using

    End Sub

    Private Sub ConvertTiffPdf()
        'Dim outfile2 As StreamWriter
        Dim myfile As String = ""
        Dim tifName As String = ""
        Dim tmpSeqnum As String = ""
        Dim pdfName As String = ""

        'Dim conn As New SqlConnection(pub_connectionstring)
        'Dim Cmd1 As New SqlCommand

        '  Dim outfile As StreamWriter
        ' Dim strtiff2pdf2 As String = ""
        Dim strtiff2pdf As String = ""
        Dim startInfo As New ProcessStartInfo

        startInfo.FileName = MyPath & "convert2pdf.cmd"
        ' startInfo.UseShellExecute = False
        startInfo.UseShellExecute = True
        ' startInfo.CreateNoWindow = True
        'startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.WindowStyle = ProcessWindowStyle.Minimized

        ' Dim strStartProcessing As String = "Moving files from Queue to Processing..."
        ' Dim strFinishProcessing As String = "Finished moving files to Processing..."

        Dim fileEntries = Directory.GetFiles(MyPath & "ProcessResponses\temp\", "*.tif*")

        'strtiff2pdf2 = "cd " & MyPath & vbCrLf

        'Cmd1.Connection = conn
        'conn.Open()
        For Each sFileName In fileEntries

            myfile = System.IO.Path.GetFileName(sFileName)
            'tifName = myfile.Substring(0, myfile.IndexOf("_"))
            'tmpSeqnum = myfile.Substring(myfile.IndexOf("_") + 1, 1)


            'Cmd1.CommandText = "select TiffFilename  from XMLRequestDocs a inner join orders b on a.UniqueOrderID = b.UniqueOrderID where OrderNumber  + '.' + rtrim(cast(b.UniqueOrderID as char(15))) = '" & tifName & "' and seqno = '" & tmpSeqnum & "'"

            'objDR1 = Cmd1.ExecuteReader


            'While objDR1.Read()
            '    With objDR1
            '        pdfName = .Item("TiffFilename")
            '    End With
            'End While
            'objDR1.Close()


            If IO.File.Exists(MyPath & "ProcessResponses\pdfs\" & Replace(Replace(pdfName, ".tiff", ".pdf"), "tif", "pdf")) Then
                IO.File.Delete(MyPath & "ProcessResponses\pdfs\" & Replace(Replace(pdfName, ".tiff", ".pdf"), "tif", "pdf"))
            End If

            strtiff2pdf += "tiff2pdf -o " & MyPath & "ProcessResponses\pdfs\" & Replace(Replace(myfile, ".tiff", ".pdf "), "tif", "pdf ") & MyPath & "ProcessResponses\recorded\tiffs\" & myfile & vbCrLf

            If IO.File.Exists(MyPath & "ProcessResponses\recorded\tiffs\" & myfile) Then
                IO.File.Delete(MyPath & "ProcessResponses\recorded\tiffs\" & myfile)

            End If

            IO.File.Move(sFileName, MyPath & "ProcessResponses\recorded\tiffs\" & myfile)

            ' strLog += "Converting Tiff to PDF - " & MyPath & "ProcessResponses\recorded\tiffs\" & myfile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf

        Next
        ' strtiff2pdf += "C:\DPS_XML\DPSFTP.exe" & vbCrLf
        'conn.Close()
        If strtiff2pdf = "" Then
            '  RichTextBox1.Text += "No Tiff files found to Convert to PDF" & vbCrLf
            ' strLog += "No Tiff files found to Convert to PDF" & vbCrLf & vbCrLf & "******************" & vbCrLf

            'Write to log
            'If IO.File.Exists(MyPath & "log\log_" & strdate & ".log") Then
            '    outfile2 = IO.File.AppendText(MyPath & "log\log_" & strdate & ".log")
            '    outfile2.WriteLine(strLog)
            '    outfile2.Flush()
            '    outfile2.Close()
            'Else
            '    outfile2 = IO.File.CreateText(MyPath & "log\log_" & strdate & ".log")
            '    outfile2.WriteLine(strLog)
            '    outfile2.Flush()
            '    outfile2.Close()

            'End If
            Exit Sub
        Else
            strtiff2pdf = "cd " & MyPath & vbCrLf & strtiff2pdf
        End If

        Using outfile As New StreamWriter(MyPath & "convert2pdf.cmd")
            outfile.Write(strtiff2pdf.ToString)
        End Using

        ' strLog += "******************" & vbCrLf
        'Write to log
        'If IO.File.Exists(MyPath & "log\log_" & strdate & ".log") Then
        '    outfile2 = IO.File.AppendText(MyPath & "log\log_" & strdate & ".log")
        '    outfile2.WriteLine(strLog)
        '    outfile2.Flush()
        '    outfile2.Close()
        'Else
        '    outfile2 = IO.File.CreateText(MyPath & "log\log_" & strdate & ".log")
        '    outfile2.WriteLine(strLog)
        '    outfile2.Flush()
        '    outfile2.Close()
        'End If
        ' strLog = ""
        Process.Start(startInfo)
        ' strLog += MyFile & " " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff") & vbCrLf

        ' strLog = strStartProcessing & vbCrLf & strLog & strFinishProcessing & vbCrLf & vbCrLf & "Start Processing DPS XML's..." & vbCrLf

    End Sub
    Private Sub updateDPSMySQL()
        ' Dim objRdrMysql As MySqlDataReader
        Dim objDRsql As SqlDataReader
        Dim connMysql As New MySqlConnection(mySQL_connectionstring)
        Dim connsql As New SqlConnection(pub_connectionstring)
        Dim connsql2 As New SqlConnection(pub_connectionstring)
        Dim cmdMysql As New MySqlCommand
        Dim cmdsql As New SqlCommand
        Dim cmdsql2 As New SqlCommand
        Dim recCnt As Integer = 0


        Try
            'update DocumentActivity table
            ' first recorded
            cmdsql.CommandText = "select ordernumber,dpsdatabase,filename,originatorSystem,payloadID,tifffilename,left(recorddatetime,10) as RecordDate,substring(RecordDateTime,CHARINDEX('T',recorddatetime) +1 , len(RecordDateTime ) - CHARINDEX('T',recorddatetime)) as RecordTime,instrumentno,sum(case when FeeDescription = 'RecordingFee' then FeeAmount else '0' end) 'RecordingFee',sum(case when FeeDescription = 'TransferTax' then FeeAmount else '0' end) 'TransferTax' from orders a inner join XMLRequestDocs b on a.UniqueOrderID = b.UniqueOrderID left join RecordingFees c  on b.recid = c.docrecid where status = 'recorded' and EREDocType not in('PCOR','TransferDeclaration','OtherNonRecorded') and dpsupdated is null  group by ordernumber,dpsdatabase,filename,originatorSystem,payloadID,tifffilename,left(recorddatetime,10),substring(RecordDateTime,CHARINDEX('T',recorddatetime) +1 , len(RecordDateTime ) - CHARINDEX('T',recorddatetime)),instrumentno"

            cmdsql.Connection = connsql
            cmdsql2.Connection = connsql2
            cmdMysql.Connection = connMysql
            connMysql.Open()
            connsql.Open()
            connsql2.Open()

            objDRsql = cmdsql.ExecuteReader

            While objDRsql.Read
                With objDRsql

                    If .Item("originatorSystem") = "ei" Then

                        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        ' Updating MYSQL
                        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                        cmdMysql.CommandText = "insert into " & .Item("dpsdatabase") & ".DocumentActivity(documentId,instrumentNumber,recordingFee,countyTransferTax,recordedDate,recordedTime,orderStatusID,internalOrderStatusID,employeeID) select documentId,'" & .Item("instrumentno") & "','" & .Item("RecordingFee") & "','" & .Item("TransferTax") & "','" & .Item("RecordDate") & "','" & .Item("RecordTime") & "','R','R','104' from " & .Item("dpsdatabase") & ".Document where payloadID = '" & .Item("payloadID") & "' and esubmitFileName = '" & .Item("tifffilename") & "' and internalCurrentStatusID not in ('D','C')"

                    Else
                        cmdMysql.CommandText = "insert into " & .Item("dpsdatabase") & ".DocumentActivity(documentId,instrumentNumber,recordingFee,countyTransferTax,recordedDate,recordedTime,orderStatusID,employeeID) select documentId,'" & .Item("instrumentno") & "','" & .Item("RecordingFee") & "','" & .Item("TransferTax") & "','" & .Item("RecordDate") & "','" & .Item("RecordTime") & "','R','104' from " & .Item("dpsdatabase") & ".Document where payloadID = '" & .Item("payloadID") & "' and esubmitFileName = '" & .Item("tifffilename") & "' and currentStatusID not in ('D','C')"
                        'cmdMysql.CommandText = "insert into " & .Item("dpsdatabase") & ".DocumentActivity(documentId,instrumentNumber,recordingFee,countyTransferTax,recordedDate,recordedTime,orderStatusID,employeeID) select documentId,'" & .Item("instrumentno") & "','" & .Item("RecordingFee") & "','" & .Item("TransferTax") & "','" & .Item("RecordDate") & "','" & .Item("RecordTime") & "','R','104' from " & .Item("dpsdatabase") & ".Document where payloadID = '" & .Item("payloadID") & "' and currentStatusId = 'O'"
                    End If
                    cmdMysql.ExecuteNonQuery()

                    recCnt += 1

                End With
            End While
            objDRsql.Close()

            'then rejected
            cmdsql.CommandText = "select payloadID,tifffilename,dpsdatabase,originatorSystem,payloadID,ErrorDescription from orders a inner join XMLRequestDocs b on a.UniqueOrderID = b.UniqueOrderID inner join RejectedErrors c on b.recid = c.docrecid where status = 'rejected' and dpsupdated is null "


            objDRsql = cmdsql.ExecuteReader

            While objDRsql.Read
                With objDRsql

                    If .Item("originatorSystem") = "ei" Then

                        cmdMysql.CommandText = "insert into " & .Item("dpsdatabase") & ".DocumentActivity(documentId,comments,orderStatusID,internalOrderStatusID,employeeID) select documentId,'" & .Item("ErrorDescription") & "','PH','PH','104' from " & .Item("dpsdatabase") & ".Document where payloadID = '" & .Item("payloadID") & "' and esubmitFileName = '" & .Item("tifffilename") & "' and internalCurrentStatusID not in ('D','C')"

                    Else

                       ' cmdMysql.CommandText = "insert into " & .Item("dpsdatabase") & ".DocumentActivity(documentId,comments,orderStatusID,employeeID) select documentId,'" & .Item("ErrorDescription") & "','PH','104' from " & .Item("dpsdatabase") & ".Document where payloadID = '" & .Item("payloadID") & "' and esubmitFileName = '" & .Item("tifffilename") & "' and currentStatusID not in ('D','C')"

                    End If

                    cmdMysql.ExecuteNonQuery()


                    recCnt += 1
                End With
            End While
            objDRsql.Close()

            'update Document table currentStatusID

            '    cmdsql.CommandText = "select uniqueorderid,payloadID,dpsdatabase, case when status = 'rejected' then 'PH' else 'R' end 'Status', isnull((select min(errordescription) from RejectedErrors where UniqueOrderID = orders.UniqueOrderID),'') 'comments', isnull((select left(min(recorddatetime),10) from XMLRequestDocs  where UniqueOrderID = orders.UniqueOrderID and eredoctype not in('PCOR','TransferDeclaration','OtherNonRecorded')),'0000-00-00') 'recordedDate', isnull((select substring(min(RecordDateTime),CHARINDEX('T',min(recorddatetime)) +1 , len(Min(RecordDateTime)) - CHARINDEX('T',min(recorddatetime))) from XMLRequestDocs  where UniqueOrderID = orders.UniqueOrderID and eredoctype not in('PCOR','TransferDeclaration','OtherNonRecorded')),'') 'recordedTime' from orders where status in ('recorded','rejected') and dpsupdated is null"
            cmdsql.CommandText = "select uniqueorderid,originatorSystem,payloadID,dpsdatabase, case when status = 'rejected' then 'PH' else 'R' end 'Status', isnull((select min(errordescription) from RejectedErrors where UniqueOrderID = orders.UniqueOrderID),'') 'comments',case when status = 'recorded' then (select left(min(recorddatetime),10) from XMLRequestDocs  where UniqueOrderID = orders.UniqueOrderID and eredoctype not in('PCOR','TransferDeclaration','OtherNonRecorded'))  when status ='rejected' then  left(responsetimestamp,10) end 'recordedDate', isnull((select substring(min(RecordDateTime),CHARINDEX('T',min(recorddatetime)) +1 ,8) from XMLRequestDocs  where UniqueOrderID = orders.UniqueOrderID and eredoctype not in('PCOR','TransferDeclaration','OtherNonRecorded')),'') 'recordedTime'   from orders where status in ('recorded','rejected') and dpsupdated is null"

            objDRsql = cmdsql.ExecuteReader

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            '   Updating MySQL
            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            While objDRsql.Read
                With objDRsql

                    If .Item("originatorSystem") = "ei" Then
                        cmdMysql.CommandText = "update " & .Item("dpsdatabase") & ".Document set currentStatusID = '" & .Item("status") & "',internalCurrentStatusID = '" & .Item("status") & "' where payloadID = '" & .Item("payloadID") & "' and internalCurrentStatusID not in ('D','C')"
                        cmdMysql.ExecuteNonQuery()

                    Else
                        cmdMysql.CommandText = "update " & .Item("dpsdatabase") & ".Document set currentStatusID = '" & .Item("status") & "' where payloadID = '" & .Item("payloadID") & "' and currentStatusID not in ('D','C')"
                        cmdMysql.ExecuteNonQuery()
                    End If


                    cmdMysql.CommandText = "update " & .Item("dpsdatabase") & ".Payload set currentStatusID = '" & .Item("status") & "', recordingDate = '" & .Item("recordedDate") & "' where payloadID = '" & .Item("payloadID") & "'"
                    cmdMysql.ExecuteNonQuery()

                    cmdMysql.CommandText = "insert into " & .Item("dpsdatabase") & ".PayloadActivity(payloadID,orderStatusID,comments,recordedDate,recordedTime) Values ('" & .Item("payloadID") & "','" & .Item("status") & "','" & .Item("comments") & "','" & .Item("recordedDate") & "','" & .Item("recordedTime") & "')"
                    cmdMysql.ExecuteNonQuery()

                    cmdsql2.CommandText = "update orders set dpsupdated = getdate() where uniqueorderid = '" & .Item("uniqueorderid") & "'"
                    cmdsql2.ExecuteNonQuery()


                End With
            End While
            objDRsql.Close()


            'If recCnt > 0 Then
            '    cmdsql.CommandText = "update orders set dpsupdated = getdate() where dpsupdated is null"
            '    cmdsql.ExecuteNonQuery()
            'End If

            connMysql.Close()
            connsql.Close()

        Catch ex As Exception
            RichTextBox1.Text += "updateDPSMySQL " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")
            RichTextBox1.Text += ex.ToString & vbCrLf

        End Try

    End Sub
    Private Sub updStatusSC()
        Dim objDRsql As SqlDataReader
        Dim connMysql As New MySqlConnection(mySQL_connectionstring)
        Dim connsql As New SqlConnection(pub_connectionstring)

        Dim connsql2 As New SqlConnection(pub_connectionstring)
        Dim cmdMysql As New MySqlCommand
        Dim cmdsql As New SqlCommand
        Dim cmdsql2 As New SqlCommand
        'Dim MyPath As String = "c:\dps_xml\"
        Dim MyFile As String = ""
        Dim strUpdOrSt As String = ""


        Dim fileEntries = Directory.GetFiles(MyPath & "CreateRequests\requests_RTS\", "*.xml")
        Dim recCnt As Integer = 0

        cmdsql.Connection = connsql
        cmdMysql.Connection = connMysql
        cmdsql2.Connection = connsql2

        Try

        connMysql.Open()
        connsql.Open()
            connsql2.Open()

        Catch ex As Exception
            RichTextBox1.Text += "updateStatusSC " & DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")
            RichTextBox1.Text += ex.ToString & vbCrLf

            Exit Sub
        End Try

        For Each sFileName In fileEntries

            MyFile = System.IO.Path.GetFileName(sFileName)

            '  strUpdOrSt = MyFile.Substring(MyFile.IndexOf("_") + 1, MyFile.IndexOf(".") - (MyFile.IndexOf("_") + 1))

            '\\10.100.1.4\Certna\Submitter\Ready --change when live
            If IO.File.Exists("C:\Certna\Submitter\Ready\" & MyFile) Then
                'If IO.File.Exists("\\10.100.1.4\test\Submitter\Ready\" & MyFile) Then
                IO.File.Move(sFileName, MyPath & "CreateRequests\requests_RTS\error\" & MyFile)

                cmdsql.CommandText = "update OutboundXMLFiles set SentStatus = 'Error File Exists Not Sent' where ReqFilename  = '" & MyFile & "'"
                cmdsql.ExecuteNonQuery()
            Else
                ' IO.File.Copy(sFileName, " \\10.100.1.4\test\Submitter\Ready\" & MyFile)
                IO.File.Copy(sFileName, " C:\Certna\Submitter\Ready\" & MyFile)

                If IO.File.Exists(MyPath & "CreateRequests\requests_RTS\SC\" & MyFile) Then
                    IO.File.Delete(MyPath & "CreateRequests\requests_RTS\SC\" & MyFile)

                End If

                IO.File.Move(sFileName, MyPath & "CreateRequests\requests_RTS\SC\" & MyFile)
                cmdsql.CommandText = "update OutboundXMLFiles set SentStatus = 'CopiedToStella' where ReqFilename  = '" & MyFile & "'"
                cmdsql.ExecuteNonQuery()
            End If

        Next

        cmdsql.CommandText = "select UniqueOrderId,dpsdatabase,payloadid,  case when SecurityTypeCount = '1' then '1' else (select count(*) from OutboundXMLfiles where UniqueOrderID = Orders.UniqueOrderID ) end 'ordersFC', (select count(*) from OutboundXMLfiles where SentStatus = 'CopiedToStella' and UniqueOrderID = Orders.UniqueOrderID) 'OutFileCount' from orders where Status = 'Created'"
        objDRsql = cmdsql.ExecuteReader

        While objDRsql.Read
            With objDRsql

                If .Item("ordersFC") = .Item("OutFileCount") Then

                    cmdsql2.CommandText = "update orders set status = 'CopiedToStella' where UniqueOrderId  = '" & .Item("UniqueOrderId") & "'"
                    cmdsql2.ExecuteNonQuery()
                End If

            End With
        End While
        objDRsql.Close()


        cmdsql.CommandText = "select uniqueorderid,reqFilename from OutboundXMLFiles where SentStatus = 'CopiedToStella'"
        objDRsql = cmdsql.ExecuteReader

        While objDRsql.Read
            With objDRsql

                'If Not IO.File.Exists("\\10.100.1.4\test\Submitter\Ready\" & .Item("reqfilename")) Then
                If Not IO.File.Exists("C:\Certna\Submitter\Ready\" & .Item("reqfilename")) Then

                    cmdsql2.CommandText = "update OutboundXMLFiles set SentStatus = 'SentToCounty' where UniqueOrderId  = '" & .Item("UniqueOrderId") & "' and reqFilename = '" & .Item("reqfilename") & "'"
                    cmdsql2.ExecuteNonQuery()

                End If


            End With
        End While
        objDRsql.Close()


        cmdsql.CommandText = "select UniqueOrderId,dpsdatabase,originatorSystem,payloadid,  case when SecurityTypeCount = '1' then '1' else  (select count(*) from OutboundXMLfiles where UniqueOrderID = Orders.UniqueOrderID ) end 'ordersFC', (select count(*) from OutboundXMLfiles where SentStatus = 'SentToCounty' and UniqueOrderID = Orders.UniqueOrderID) 'OutFileCount' from orders where Status = 'CopiedToStella'"
        objDRsql = cmdsql.ExecuteReader

        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        'Updating MySQL here with Status
        '==================================================================

        While objDRsql.Read
            With objDRsql

                If .Item("ordersFC") = .Item("OutFileCount") Then
                    If .Item("originatorSystem") = "ei" Then

                        cmdMysql.CommandText = "update " & .Item("dpsdatabase") & ".Document set currentStatusID = 'SC', internalCurrentStatusID = 'SC' where payloadID = '" & .Item("payloadID") & "' and internalCurrentStatusID not in ('D','C')"
                        cmdMysql.ExecuteNonQuery()

                        cmdMysql.CommandText = "insert into " & .Item("dpsdatabase") & ".DocumentActivity(documentId,orderStatusID,internalOrderStatusID,employeeID) select documentId,'SC','SC','104' from " & .Item("dpsdatabase") & ".Document where payloadID = '" & .Item("payloadID") & "' and internalCurrentStatusID not in ('D','C')"
                        cmdMysql.ExecuteNonQuery()

                    Else
                        cmdMysql.CommandText = "update " & .Item("dpsdatabase") & ".Document set currentStatusID = 'SC' where payloadID = '" & .Item("payloadID") & "' and currentStatusID not in ('D','C')"
                        cmdMysql.ExecuteNonQuery()

                        cmdMysql.CommandText = "insert into " & .Item("dpsdatabase") & ".DocumentActivity(documentId,orderStatusID,employeeID) select documentId,'SC','104' from " & .Item("dpsdatabase") & ".Document where payloadID = '" & .Item("payloadID") & "' and currentStatusID not in ('D','C')"
                        cmdMysql.ExecuteNonQuery()
                    End If

                    cmdMysql.CommandText = "update " & .Item("dpsdatabase") & ".Payload set currentStatusID = 'SC', timestamp = CURRENT_TIMESTAMP where payloadID = '" & .Item("payloadID") & "' and currentStatusID = 'O'"
                    cmdMysql.ExecuteNonQuery()

                    cmdMysql.CommandText = "insert into " & .Item("dpsdatabase") & ".PayloadActivity(payloadID,orderStatusID) Values ('" & .Item("payloadID") & "','SC')"
                    cmdMysql.ExecuteNonQuery()

                    cmdsql2.CommandText = "update orders set status = 'SentToCounty' where UniqueOrderId  = '" & .Item("UniqueOrderId") & "'"
                    cmdsql2.ExecuteNonQuery()


                End If

            End With
        End While
        objDRsql.Close()

        connsql.Close()
        connMysql.Close()
        connsql2.Close()


    End Sub
    Private Sub CheckCertnaReturns()
        Dim connsql As New SqlConnection(pub_connectionstring)
        Dim cmdsql As New SqlCommand

        'Dim MyPath As String = "c:\dps_xml\"
        Dim MyFile As String = ""

        Try


            '   Dim fileEntries = Directory.GetFiles("\\10.100.1.4\test\Submitter\Ready\RETRIEVED\", "*.xml")
            Dim fileEntries = Directory.GetFiles("C:\Certna\Submitter\Ready\RETRIEVED\", "*.xml")
            Dim recCnt As Integer = 0

            cmdsql.Connection = connsql
            connsql.Open()

            'cmdsql.CommandText = "select ordernumber + '_' + rtrim(cast(uniqueorderid as char(10))) as OrderNum,* from orders where uniqueorderid = 138 and datediff(mi,requestprocesseddate,getdate()) > 60"

            cmdsql.CommandText = "select ordernumber + '_' + rtrim(cast(uniqueorderid as char(10))) as OrderNum,* from orders where status = 'SentToCounty' and datediff(mi,requestprocesseddate,getdate()) > 5"


            objDR1 = cmdsql.ExecuteReader

            While objDR1.Read
                With objDR1
                    For Each sFileName In fileEntries
                        MyFile = System.IO.Path.GetFileName(sFileName)

                        ' If Not IO.File.Exists(MyPath & "CreateRequests\requests_RTS\DPS_Ready\" & .Item("reqfilename")) Then
                        If MyFile.Contains(.Item("OrderNum")) Then

                            If Not IO.File.Exists(MyPath & "ProcessResponses\processed\" & MyFile) Then
                                ' IO.File.Move(sFileName, MyPath & "ProcessResponses\queue\" & MyFile)
                                IO.File.Copy(sFileName, MyPath & "ProcessResponses\queue\" & MyFile)
                            End If

                        End If
                    Next

                End With
            End While

        
        objDR1.Close()
        connsql.Close()

        Catch ex As Exception

            RichTextBox1.Text += "Error in checkCertnaReturns" & vbCrLf & ex.ToString & vbCrLf

        End Try

    End Sub
    Private Sub SendEMail()
        'MessageBox.Show(email)
        'Return
        Try

            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New Net.NetworkCredential("timw@crtdata.com", "password")
            SmtpServer.Port = 25
            SmtpServer.Host = "smtp.crtdata.com"
            mail = New MailMessage()
            mail.From = New MailAddress("johnm@crtdata.com")
            ' mail.From = New MailAddress("betheny@f56handson.com", "Betheny Zolt")

            'mail.ReplyTo = New MailAddress(TextBox_ReplyTo.Text)


            mail.To.Add("jlmcg01@hotmail.com")

            mail.CC.Add("rodolfog@dpsx.com")
            mail.Bcc.Add("jerryb@crtdata.com")
            mail.Subject = "Certna Error"

            mail.Body = strEmailbody
            mail.IsBodyHtml = True
            SmtpServer.Send(mail)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdLoadform_Click(sender As System.Object, e As System.EventArgs) Handles cmdLoadform.Click
        Call MoveDpsFilesToProcessing()
    End Sub

    Private Sub cmdResponseRD_Click(sender As System.Object, e As System.EventArgs) Handles cmdResponseRD.Click
        Call MoveCertnaRespToProcessing()
    End Sub

    Private Sub btnWriteXML_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub btnConvertTiffPdf_Click(sender As System.Object, e As System.EventArgs) Handles btnConvertTiffPdf.Click
        Call ConvertTiffPdf()
        ' Call eSubmitGetCertnaResponses()
    End Sub
    Private Sub Runeverything()

        lblTimer.Text = 0
        '  RichTextBox1.Text = ""


        Call eSubmitGetFilesWeb()
        Call MoveDpsFilesToProcessing()
        Call updStatusSC()

        Call MoveCertnaRespToProcessing()
        Call eSubmitGetRecorded()
        Call eSubmitGetRejected()

        Call CheckCertnaReturns()
        Call updateDPSMySQL()
        Call ConvertTiffPdf()
        Timer1.Start()

    End Sub

    Private Sub btnRunAll_Click(sender As System.Object, e As System.EventArgs) Handles btnRunAll.Click
        BtnStopApp.Enabled = True
        btnRunAll.Enabled = False
        RichTextBox1.Text += "Processor started- " & DateTime.Now.ToString("MM/dd/yyyy_HH:mm:ss") & vbCrLf

        Call Runeverything()

    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick

        lblTimer.Text += 1

        If lblTimer.Text = txtTimerInt.Text + 1 Then

            Timer1.Stop()

            Call Runeverything()
        End If

    End Sub

    Private Sub BtnStopApp_Click(sender As System.Object, e As System.EventArgs) Handles BtnStopApp.Click
        Timer1.Stop()
        BtnStopApp.Enabled = False
        btnRunAll.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'Call eSubmitGetFiles()
        Call eSubmitGetCustomers()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Call eSubmitGetRecorded()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Call eSubmitGetRejected()
    End Sub

    Private Sub cmdESubmitWeb_Click(sender As System.Object, e As System.EventArgs) Handles cmdESubmitWeb.Click
        Call eSubmitGetFilesWeb()
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        updateDPSMySQL()
    End Sub

    Private Sub cmdUpdStatus_Click(sender As System.Object, e As System.EventArgs) Handles cmdUpdStatus.Click
        Call updStatusSC()

    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Call CheckCertnaReturns()

    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class

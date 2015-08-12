Imports MySql.Data
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Math
Imports System.Xml
Imports System.Reflection
Imports System
Imports System.Net.Mail

Imports System.Collections
Imports System.Collections.Generic
Imports System.Configuration

Public Class SQLLib

    Public sErr As String = ""

    Private adoConnectA As MySql.Data.MySqlClient.MySqlConnection
    Private adoConnectB As System.Data.SqlClient.SqlConnection
    Private sSqlA As String = ""
    Private sSqlB As String = ""
    '============================================
    'Generalized TSQL Call Ins
    '============================================
    '============================================
    Public Sub WriteEvent(smsg As String, Optional EFlag As Boolean = False)
        Try
            Dim elog As New EventLog
            elog.Source = "ESubmitEvents"
            elog.WriteEntry(smsg)
            If EFlag Then
                SendEmail("ESubmit Error", smsg)
            End If
        Catch ex As Exception
            '
        End Try
    End Sub
    Public Function dbOpenA(sCnStr As String) As Boolean
        Dim bRet As Boolean
        sErr = ""
        Try
            If Not IsNothing(adoConnectA) Then
                Try
                    If adoConnectA.State = ConnectionState.Open Then
                        If sCnStr = "" Then Return True
                        If adoConnectA.ConnectionString = sCnStr Then Return True
                        adoConnectA.Close()
                    End If
                Catch ex As Exception
                    WriteEvent("DbOpenA 1 [" & sCnStr & "] Err:" & ex.Message)
                    Return False
                End Try
            End If

            Try
                If sCnStr = "" Then sCnStr = "dbApp"
                If sCnStr.Substring(0, 2) <> "db" Then
                    adoConnectA = New MySqlConnection(sCnStr)
                Else
                    adoConnectA = New MySqlConnection(My.Settings.Item(sCnStr))
                End If

                adoConnectA.Open()
                bRet = True
            Catch ex As Exception
                WriteEvent("DbOpenA 2 [" & sCnStr & "] Err:" & ex.Message)
                Return False
            End Try
        Catch ex As Exception
            sErr = "dbOpenA 3 : [" & sCnStr & "] " & ex.Message & " | " & ex.Message
            WriteEvent(sErr)
        End Try

        Return bRet

    End Function
    Public Function dbOpenB(sCnStr As String) As Boolean
        Dim bRet As Boolean
        sErr = ""
        Try
            If Not IsNothing(adoConnectB) Then
                If adoConnectB.State = ConnectionState.Open Then
                    If sCnStr = "" Then Return True
                    If adoConnectB.ConnectionString = sCnStr Then Return True
                    adoConnectB.Close()
                End If
            End If
            If sCnStr.Substring(0, 2) <> "db" Then
                adoConnectB = New SqlConnection(sCnStr)
            Else
                adoConnectB = New SqlConnection(My.Settings.Item(sCnStr))
            End If

            adoConnectB.Open()
            bRet = True
        Catch ex As Exception
            sErr = "dbOpenB: " & ex.Message & " | " & sErr
            WriteEvent(sErr)
        End Try
        Return bRet
    End Function
    Public Function dbCloseA() As Boolean
        sErr = ""
        Try
            If Not IsNothing(adoConnectA) Then
                If adoConnectA.State = ConnectionState.Open Then
                    adoConnectA.Close()
                    adoConnectA.Dispose()
                End If
            End If
        Catch ex As Exception
            sErr = "dbCloseA:" & ex.Message
            WriteEvent(sErr)
            Return False
        End Try
        Return True
    End Function
    Public Function dbCloseB() As Boolean
        sErr = ""
        Try
            If Not IsNothing(adoConnectB) Then
                If adoConnectB.State = ConnectionState.Open Then
                    adoConnectB.Close()
                    adoConnectB.Dispose()
                End If
            End If
        Catch ex As Exception
            sErr = "dbCloseB: " & ex.Message
            WriteEvent(sErr)
            Return False
        End Try
        Return True
    End Function
    Public Function ExecA(ByVal strCmd As String, ByVal iType As System.Data.CommandType, Optional ByVal Als As String = "", Optional bClose As Boolean = True) As Boolean
        Dim bRet As Boolean = False
        sErr = ""
        Try
            If dbOpenA(Als) = False Then Return False
            Dim cmd As MySqlCommand
            cmd = New MySqlCommand(strCmd, adoConnectA)
            cmd.CommandType = iType
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            bRet = True
        Catch ex As Exception
            sErr = "ExecA: " & ex.Message & " | " & sErr
            WriteEvent(sErr)
        Finally
            If bClose Then dbCloseA()
        End Try
        Return bRet
    End Function
    Public Function ExecB(ByVal strCmd As String, ByVal iType As System.Data.CommandType, Optional ByVal Als As String = "", Optional bClose As Boolean = True) As Boolean
        Dim bRet As Boolean = False
        sErr = ""
        Try
            If dbOpenB(Als) = False Then Return False
            Dim cmd As SqlCommand
            cmd = New SqlCommand(strCmd, adoConnectB)
            cmd.CommandType = iType
            cmd.ExecuteNonQuery()
            bRet = True
            cmd.Dispose()
        Catch ex As Exception
            sErr = "ExecB: " & ex.Message & " | " & sErr
            WriteEvent(sErr)
        Finally
            If bClose Then dbCloseB()
        End Try
        Return bRet
    End Function
    Public Function ExecSPA(ByVal strCmd As String, Optional ByVal Param() As Object = Nothing, Optional ByVal Als As String = "", Optional bClose As Boolean = True) As Boolean
        Dim bRet As Boolean = False
        Dim x As Integer = 0
        sErr = ""

        Try
            If dbOpenA(Als) = False Then Return False
            Dim cmd As MySqlCommand
            cmd = New MySqlCommand(strCmd, adoConnectA)
            cmd.CommandType = CommandType.StoredProcedure
            If IsNothing(Param) = False Then
                Dim TCode As TypeCode
                Try

                    For x = 0 To Param.Length - 1
                        TCode = Type.GetTypeCode(Param(x).GetType)
                        Select Case TCode
                            Case TypeCode.String
                                cmd.Parameters.Add("P" & x.ToString, SqlDbType.Char).Value = Param(x)
                            Case TypeCode.Int16, TypeCode.Single
                                cmd.Parameters.Add("P" & x.ToString, SqlDbType.Int).Value = Param(x)
                            Case TypeCode.DateTime
                                cmd.Parameters.Add("P" & x.ToString, SqlDbType.DateTime).Value = Param(x).ToString
                            Case TypeCode.Double
                                cmd.Parameters.Add("P" & x.ToString, SqlDbType.Float).Value = Param(x)
                            Case TypeCode.Decimal, TypeCode.Int32, TypeCode.Int64
                                cmd.Parameters.Add("P" & x.ToString, SqlDbType.BigInt).Value = Param(x)

                            Case Else
                                cmd.Parameters.Add("P" & x.ToString, SqlDbType.Char).Value = Param(x)
                        End Select
                    Next
                Catch ex As Exception
                    sErr = "ExecSPA Parameter [" & x.ToString & " Typ:" & Param(x).GetType.ToString.ToUpper & " Val:" & Param(x).ToString & "  Err: " & ex.Message & " | " & sErr
                    WriteEvent(sErr)
                    Return False
                End Try
            End If
            cmd.ExecuteNonQuery()
            bRet = True
            cmd.Dispose()
        Catch ex As Exception
            sErr = "ExecSpa: " & ex.Message & " | " & sErr
            WriteEvent(sErr)
        Finally
            If bClose Then dbCloseA()
        End Try
        Return bRet
    End Function
    Public Function ExecSPA(ByVal strCmd As String, Optional ByVal ParamNames() As String = Nothing, Optional ParamVal() As Object = Nothing, Optional ByVal Als As String = "", Optional bClose As Boolean = True) As Boolean
        Dim bRet As Boolean = False
        Dim x As Integer = 0
        sErr = ""

        Try
            If dbOpenA(Als) = False Then Return False
            Dim cmd As MySqlCommand
            cmd = New MySqlCommand(strCmd, adoConnectA)
            cmd.CommandType = CommandType.StoredProcedure
            If IsNothing(ParamVal) = False Then
                Dim TCode As TypeCode
                Try

                    For x = 0 To ParamVal.Length - 1
                        TCode = Type.GetTypeCode(ParamVal(x).GetType)
                        Select Case TCode
                            Case TypeCode.String
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.Char).Value = ParamVal(x)
                            Case TypeCode.Int16, TypeCode.Single
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.Int).Value = ParamVal(x)
                            Case TypeCode.DateTime
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.DateTime).Value = ParamVal(x)
                            Case TypeCode.Double
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.Float).Value = ParamVal(x)
                            Case TypeCode.Decimal, TypeCode.Int32, TypeCode.Int64
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.BigInt).Value = ParamVal(x)
                            Case TypeCode.Object
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.UniqueIdentifier).Value = ParamVal(x)

                            Case Else
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.Char).Value = ParamVal(x)
                        End Select
                    Next
                Catch ex As Exception
                    sErr = "ExecSPA Parameter [" & ParamNames(x) & " Typ:" & ParamVal(x).GetType.ToString.ToUpper & " Val:" & ParamVal(x).ToString & "  Err: " & ex.Message & " | " & sErr
                    WriteEvent(sErr)
                    Return False
                End Try
            End If
            cmd.ExecuteNonQuery()
            bRet = True
            cmd.Dispose()
        Catch ex As Exception
            If ex.Message.ToLower.IndexOf("duplicate") > -1 Then Return True
            sErr = "ExecSpa: " & ex.Message & " | " & sErr & " | " & strCmd & " | " & ParamVal(0)
            WriteEvent(sErr)
        Finally
            If bClose Then dbCloseA()
        End Try
        Return bRet
    End Function
    Public Function ExecSPB(ByVal strCmd As String, Optional ByVal ParamNames() As String = Nothing, Optional ParamVal() As Object = Nothing, Optional ByVal Als As String = "", Optional bClose As Boolean = True) As Boolean
        Dim bRet As Boolean = False
        Dim x As Integer = 0
        sErr = ""

        Try
            If dbOpenB(Als) = False Then Return False
            Dim cmd As SqlCommand
            cmd = New SqlCommand(strCmd, adoConnectB)
            cmd.CommandType = CommandType.StoredProcedure
            If IsNothing(ParamVal) = False Then
                Dim TCode As TypeCode
                Try

                    For x = 0 To ParamVal.Length - 1
                        TCode = Type.GetTypeCode(ParamVal(x).GetType)
                        Select Case TCode
                            Case TypeCode.String
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.Char).Value = ParamVal(x)
                            Case TypeCode.Int16, TypeCode.Single
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.Int).Value = ParamVal(x)
                            Case TypeCode.DateTime
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.DateTime).Value = ParamVal(x)
                            Case TypeCode.Double
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.Float).Value = ParamVal(x)
                            Case TypeCode.Decimal, TypeCode.Int32, TypeCode.Int64
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.BigInt).Value = ParamVal(x)
                            Case TypeCode.Object
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.UniqueIdentifier).Value = ParamVal(x)

                            Case Else
                                cmd.Parameters.Add("@" & ParamNames(x), SqlDbType.Char).Value = ParamVal(x)
                        End Select
                    Next
                Catch ex As Exception
                    sErr = "ExecSPB Parameter [" & ParamNames(x) & " Typ:" & ParamVal(x).GetType.ToString.ToUpper & " Val:" & ParamVal(x).ToString & "  Err: " & ex.Message & " | " & sErr
                    WriteEvent(sErr)
                    Return False
                End Try
            End If
            cmd.ExecuteNonQuery()
            bRet = True
            cmd.Dispose()
        Catch ex As Exception
            If ex.Message.ToLower.IndexOf("duplicate") > -1 Then Return True
            sErr = "ExecSpb: " & ex.Message & " | " & sErr
            WriteEvent(sErr)
        Finally
            If bClose Then dbCloseB()
        End Try
        Return bRet
    End Function

    Public Function ExecRsA(ByVal strCmd As String, ByVal iType As System.Data.CommandType, ByRef Dataset As DataSet, Optional ByVal Param() As Object = Nothing, Optional ByVal Als As String = "", Optional bClose As Boolean = True) As Boolean
        Dim bRet As Boolean = False
        Dim x As Integer = 0
        sErr = ""

        Try
            If dbOpenA(Als) = False Then Return False
            Dim adoCmd As New MySqlCommand
            Dim adoAdapter As MySqlDataAdapter
            With adoCmd
                .CommandTimeout = 6000
                .Connection = adoConnectA
                .CommandText = strCmd
                .CommandType = iType
                If IsNothing(Param) = False Then
                    Try
                        Dim TCode As TypeCode
                        For x = 0 To Param.Length - 1
                            TCode = Type.GetTypeCode(Param(x).GetType)
                            Select Case TCode
                                Case TypeCode.String
                                    .Parameters.Add("P" & x.ToString, SqlDbType.Char).Value = Param(x)
                                Case TypeCode.Int16, TypeCode.Single
                                    .Parameters.Add("P" & x.ToString, SqlDbType.Int).Value = Param(x)
                                Case TypeCode.DateTime
                                    .Parameters.Add("P" & x.ToString, SqlDbType.DateTime).Value = Param(x).ToString
                                Case TypeCode.Double
                                    .Parameters.Add("P" & x.ToString, SqlDbType.Float).Value = Param(x)
                                Case TypeCode.Decimal, TypeCode.Int32, TypeCode.Int64
                                    .Parameters.Add("P" & x.ToString, SqlDbType.BigInt).Value = Param(x)

                                Case Else
                                    .Parameters.Add("P" & x.ToString, SqlDbType.Char).Value = Param(x)
                            End Select
                        Next
                    Catch ex As Exception
                        sErr = "ExecRsNA Parameter [" & x.ToString & " Typ:" & Param(x).GetType.ToString.ToUpper & " Val:" & Param(x).ToString & "  Err: " & ex.Message & "|" & sErr
                        WriteEvent(sErr)
                        Return False
                    End Try
                End If
            End With
            adoAdapter = New MySqlDataAdapter(adoCmd)
            adoAdapter.Fill(Dataset)
            bRet = True
            adoAdapter.Dispose()
            adoCmd.Dispose()
            bRet = True
        Catch ex As Exception
            sErr = "ExecRsA: " & ex.Message & " | " & sErr
            WriteEvent(sErr)
        Finally
            If bClose Then dbCloseA()
        End Try
        Return bRet
    End Function
    Public Function ExecRsNA(ByVal strCmd As String, ByVal iType As System.Data.CommandType, ByRef dataset As DataSet, Optional ByVal Param() As Object = Nothing, Optional ByVal Als As String = "", Optional bClose As Boolean = True) As Boolean
        Dim bRet As Boolean = False
        Dim x As Integer
        sErr = ""

        Try
            If dbOpenA(Als) = False Then Return False
            Dim adoCmd As New MySqlCommand
            Dim adoAdapter As MySqlDataAdapter
            With adoCmd
                .CommandTimeout = 6000
                .Connection = adoConnectA
                .CommandText = strCmd
                .CommandType = iType
                If IsNothing(Param) = False Then
                    Try
                        Dim TCode As TypeCode
                        For x = 0 To Param.Length - 1
                            If Not IsNothing(Param(x)) Then
                                TCode = Type.GetTypeCode(Param(x).GetType)
                                Select Case TCode
                                    Case TypeCode.String
                                        .Parameters.Add("P" & x.ToString, SqlDbType.Char).Value = Param(x)
                                    Case TypeCode.Int16, TypeCode.Single
                                        .Parameters.Add("P" & x.ToString, SqlDbType.Int).Value = Param(x)
                                    Case TypeCode.DateTime
                                        .Parameters.Add("P" & x.ToString, SqlDbType.DateTime).Value = Param(x).ToString
                                    Case TypeCode.Double
                                        .Parameters.Add("P" & x.ToString, SqlDbType.Float).Value = Param(x)
                                    Case TypeCode.Decimal, TypeCode.Int32, TypeCode.Int64
                                        .Parameters.Add("P" & x.ToString, SqlDbType.BigInt).Value = Param(x)
                                    Case TypeCode.Byte
                                        .Parameters.Add("P" & x.ToString, SqlDbType.TinyInt).Value = Param(x)

                                    Case Else
                                        .Parameters.Add("P" & x.ToString, SqlDbType.Char).Value = Param(x)
                                End Select
                            End If
                        Next
                    Catch ex As Exception
                        sErr = "ExecRsNA Parameter [" & x.ToString & " Typ:" & Param(x).GetType.ToString.ToUpper & " Val:" & Param(x).ToString & "  Err: " & ex.Message & "|" & sErr
                        WriteEvent(sErr)
                        Return False
                    End Try
                End If
            End With
            adoAdapter = New MySqlDataAdapter(adoCmd)
            adoAdapter.Fill(dataset)
            adoAdapter.Dispose()
            adoCmd.Dispose()
            bRet = True
        Catch ex As Exception
            sErr = "ExecRsNA: " & ex.Message & " | " & sErr
            WriteEvent(sErr)
        Finally
            If bClose Then dbCloseA()
        End Try
        Return bRet
    End Function
    Public Function ExecRsNB(ByVal strCmd As String, ByVal iType As System.Data.CommandType, ByRef dataset As DataSet, Optional ByVal Param() As Object = Nothing, Optional ByVal Als As String = "", Optional bClose As Boolean = True) As Boolean
        Dim bRet As Boolean = False
        Dim x As Integer
        sErr = ""

        Try
            If dbOpenB(Als) = False Then Return False
            Dim adoCmd As New SqlCommand
            Dim adoAdapter As SqlDataAdapter
            With adoCmd
                .CommandTimeout = 6000
                .Connection = adoConnectB
                .CommandText = strCmd
                .CommandType = iType
                If IsNothing(Param) = False Then
                    Try
                        Dim TCode As TypeCode
                        For x = 0 To Param.Length - 1
                            TCode = Type.GetTypeCode(Param(x).GetType)
                            Select Case TCode
                                Case TypeCode.String
                                    .Parameters.Add("P" & x.ToString, SqlDbType.Char).Value = Param(x)
                                Case TypeCode.Int16, TypeCode.Single
                                    .Parameters.Add("P" & x.ToString, SqlDbType.Int).Value = Param(x)
                                Case TypeCode.DateTime
                                    .Parameters.Add("P" & x.ToString, SqlDbType.DateTime).Value = Param(x).ToString
                                Case TypeCode.Double
                                    .Parameters.Add("P" & x.ToString, SqlDbType.Float).Value = Param(x)
                                Case TypeCode.Decimal, TypeCode.Int32, TypeCode.Int64
                                    .Parameters.Add("P" & x.ToString, SqlDbType.BigInt).Value = Param(x)
                                Case Else
                                    .Parameters.Add("P" & x.ToString, SqlDbType.Char).Value = Param(x)
                            End Select
                        Next
                    Catch ex As Exception
                        sErr = "ExecRsNB Parameter [" & x.ToString & " Typ:" & Param(x).GetType.ToString.ToUpper & " Val:" & Param(x).ToString & "  Err: " & ex.Message & "|" & sErr
                        WriteEvent(sErr)
                        Return False
                    End Try
                End If
            End With
            adoAdapter = New SqlDataAdapter(adoCmd)
            adoAdapter.Fill(dataset)
            adoAdapter.Dispose()
            adoCmd.Dispose()
            bRet = True
        Catch ex As Exception
            sErr = "ExecRsNB: " & ex.Message & " | " & sErr
            WriteEvent(sErr)
        Finally
            If bClose Then dbCloseB()
        End Try
        Return bRet
    End Function

    Public Sub SendEmail(ByVal sSubject As String, ByVal sBody As String)
        Try
            SendEmail(sSubject, sBody, False, "mwechter1@gmail.com", "ESubmitService@DPS.com", "")
        Catch ex As Exception
            WriteEvent("SendEmail Err: " & ex.Message)
        End Try
    End Sub

    Public Sub SendEmail(ByVal sSubject As String, ByVal sBody As String, bhtml As Boolean, sendto As String, sendfrom As String, attach As String)
        Try
            SMTPSend(sendfrom, sendto, sSubject, bhtml, sBody, attach)
        Catch ex As Exception
            WriteEvent("SendEmail Err: " & ex.Message)
        End Try
    End Sub
    Private Function SMTPSend(sFrom As String, sTo As String, sSubject As String, bIsHtml As Boolean, sbody As String, sattach As String) As Boolean
        Try

            Dim mail As New MailMessage
            Dim sm() As String = sTo.Split(";")
            For x As Integer = 0 To sm.Length - 1
                If sm(x).Trim.Length > 5 Then mail.To.Add(sm(x))
            Next
            mail.From = New MailAddress(sFrom)

            mail.Subject = sSubject
            mail.IsBodyHtml = bIsHtml
            mail.Body = sbody
            If sattach.Trim.Length > 5 Then
                Dim attachment As System.Net.Mail.Attachment
                attachment = New System.Net.Mail.Attachment(sattach)
                mail.Attachments.Add(attachment)
            End If

            Dim SmtpServer As New SmtpClient()
            SmtpServer.Credentials = New Net.NetworkCredential("problem@Adaptivevoice.com", "adaptivevoice")
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.1and1.com"
            '   SmtpServer.Send(mail)

        Catch ex As Exception
            MsgBox(ex)
        End Try
        Return True
    End Function
#Region "uselater"
    '    '============================================

    Public Function CopyTable(sTableSource As String, sTableDestination As String, sCnSource As String, sCnDestination As String, Optional bDropTable As Boolean = False) As Boolean
        Dim sSelA As String = ""
        Dim sSelB As String = ""
        Dim dsDataSource As New DataSet
        Dim dsDataDestin As New DataSet
        Dim stB As StringBuilder

        Try
            'check connection strings
            If dbOpenA(sCnSource) = False Then Return False
            If dbOpenB(sCnDestination) = False Then Return False

            'Check if dropping table
            If bDropTable Then
                sSelB = "drop table " & sTableDestination
                If ExecB(sSelB, CommandType.Text, , False) = False Then
                    If sErr.ToLower.IndexOf("does not exist") > -1 Then GoTo Dropped
                    Return False
                End If
            Else
                sSelB = "Select top 1 * from " & sTableDestination
                If ExecRsNB(sSelB, CommandType.Text, dsDataDestin, , , False) Then
                    sErr = "CopyTable Table: " & sTableDestination & " already exists in " & sCnDestination
                    Return False
                End If
            End If

Dropped:
            sErr = ""
            'read in data table from source
            sSelA = "Select * from " & sTableSource
            If ExecRsA(sSelA, CommandType.Text, dsDataSource, , , False) = False Then Return False

            'Dynamic create table
            stB = New StringBuilder("Create table dbo." & sTableDestination & " ( ")
            For c = 0 To dsDataSource.Tables(0).Columns.Count - 1
                stB.Append("[" & dsDataSource.Tables(0).Columns(c).Caption & "] ")
                Select Case dsDataSource.Tables(0).Columns(c).DataType.FullName.ToLower
                    Case "system.boolean"
                        stB.Append("[bool] NULL, ")
                    Case "system.char"
                        If dsDataSource.Tables(0).Columns(c).MaxLength = -1 Then
                            stB.Append("[nvarchar](1000) NULL, ")
                        Else
                            stB.Append("[nvarchar](" & dsDataSource.Tables(0).Columns(c).MaxLength.ToString & ") NULL, ")
                        End If
                    Case "system.datetime"
                        stB.Append("[datetime] NULL, ")
                    Case "system.decimal"
                        stB.Append("[decimal] NULL, ")
                    Case "system.double"
                        stB.Append("[numeric] NULL, ")
                    Case "system.guid"
                        stB.Append("[uniqueidentifier] NULL, ")
                    Case "system.int16", "system.uint16"
                        stB.Append("[int] NULL, ")
                    Case "system.int32", "system.uint32"
                        stB.Append("[int] NULL, ")
                    Case "system.int64", "system.uint64"
                        stB.Append("[bigint] NULL, ")
                    Case "system.sbyte"
                        stB.Append("[bit] NULL, ")
                    Case "system.single"
                        stB.Append("[int] NULL, ")
                    Case "system.string"
                        If dsDataSource.Tables(0).Columns(c).MaxLength = -1 Then
                            stB.Append("[nvarchar](1000) NULL, ")
                        Else
                            stB.Append("[nvarchar](" & dsDataSource.Tables(0).Columns(c).MaxLength.ToString & ") NULL, ")
                        End If
                        'Case "system.timespan"

                End Select
            Next
            stB.Append(" )")
            If ExecB(stB.ToString, CommandType.Text, , False) = False Then Return False

            'now copy the data

            'start building the insert cmd
            stB = New StringBuilder
            stB.Append("insert into " & sTableDestination & " ( ")
            For c As Integer = 0 To dsDataSource.Tables(0).Columns.Count - 1
                stB.Append("[" & dsDataSource.Tables(0).Columns(c).Caption & "],")
            Next
            stB.Remove(stB.Length - 1, 1)
            stB.Append(" ) Values ( ")

            Dim sbInsert As StringBuilder
            For sRow As Integer = 0 To dsDataSource.Tables(0).Rows.Count - 1
                sbInsert = New StringBuilder(stB.ToString)
                Dim dr As DataRow = dsDataSource.Tables(0).Rows(sRow)
                For sCol As Integer = 0 To dsDataSource.Tables(0).Columns.Count - 1
                    If Not IsDBNull(dr(sCol)) Then
                        Select Case dsDataSource.Tables(0).Columns(sCol).DataType.FullName.ToLower
                            Case "system.boolean"
                                sbInsert.Append("'" & dr.Item(sCol).ToString & "',")
                            Case "system.char"
                                sbInsert.Append("'" & dr.Item(sCol).ToString.Replace("'", "") & "',")
                            Case "system.datetime"
                                sbInsert.Append("'" & dr.Item(sCol).ToString & "',")
                            Case "system.decimal"
                                sbInsert.Append(dr.Item(sCol).ToString & ",")
                            Case "system.double"
                                sbInsert.Append(dr.Item(sCol).ToString & ",")
                            Case "system.guid"
                                sbInsert.Append("'" & dr.Item(sCol).ToString & "' ,")
                            Case "system.int16", "system.uint16"
                                sbInsert.Append(dr.Item(sCol).ToString & ",")
                            Case "system.int32", "system.uint32"
                                sbInsert.Append(dr.Item(sCol).ToString & ",")
                            Case "system.int64", "system.uint64"
                                sbInsert.Append(dr.Item(sCol).ToString & ",")
                            Case "system.sbyte"
                                sbInsert.Append(dr.Item(sCol).ToString & ",")
                            Case "system.single"
                                sbInsert.Append(dr.Item(sCol).ToString & ",")
                            Case "system.string"
                                sbInsert.Append("'" & dr.Item(sCol).ToString.Replace("'", "") & "',")
                                'Case "system.timespan"
                        End Select
                    Else
                        sbInsert.Append("Null ,")
                    End If
                Next
                sSelB = sbInsert.ToString
                sSelB = sSelB.Substring(0, sSelB.Length - 1) & ")"
                If ExecB(sSelB, CommandType.Text, , False) = False Then
                    sErr += " Row:" & sRow.ToString & vbCrLf & sSelB
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            sErr = "CopyTable: " & ex.Message
        End Try
        Return False
    End Function
    '    '=========================
    '    'read JobControl Row
    '    '========================
    '    Public Function GetJobControlRecid(sRecid As String) As DataRow
    '        Try
    '            Dim dsJob As New DataSet
    '            Dim param(0) As Object
    '            param(0) = CLng(sRecid)
    '            Dim ssel As String = "sas_list_jobcontrolbyrecid"
    '            If ExecRsNA(ssel, CommandType.StoredProcedure, dsJob, param, , False) = False Then
    '                Return Nothing
    '            End If
    '            Return dsJob.Tables(0).Rows(0)
    '        Catch ex As Exception
    '            sErr = ex.Message
    '        End Try
    '        Return Nothing
    '    End Function
    '    '=========================
    '    ' update jobRow
    '    '=========================
    '    Public Function UpdateJobControlRecid(dtRow As DataRow) As Boolean
    '        Try
    '            Dim param(dtRow.Table.Columns.Count - 1) As Object
    '            For x As Integer = 0 To dtRow.Table.Columns.Count - 1
    '                param(x) = dtRow.Item(x)
    '            Next x
    '            Dim ssel As String = "sas_update_jobcontrolbyrecid"
    '            If ExecSPA(ssel, param, , False) = False Then
    '                Return False
    '            End If
    '            Return True
    '        Catch ex As Exception
    '            sErr = ex.Message
    '        End Try
    '        Return False
    '    End Function
    '    Public Function UpdateJobControlHistoryRecid(dtRow As DataRow) As Boolean
    '        Try
    '            Dim param(dtRow.Table.Columns.Count - 1) As Object
    '            Dim ind As Integer = 0
    '            For x As Integer = 0 To dtRow.Table.Columns.Count - 1
    '                param(ind) = dtRow.Item(x)
    '                ind += 1
    '            Next x

    '            Dim ssel As String = "sas_insert_jobcontrolhistory"
    '            If ExecSPA(ssel, param, , False) = False Then
    '                Return False
    '            End If
    '            Return True
    '        Catch ex As Exception
    '            sErr = ex.Message
    '        End Try
    '        Return False
    '    End Function
    '    Public Sub PostEvent(sMsg As String)
    '        Try
    '            If Not EventLog.SourceExists("AppSASJobs", ".") Then
    '                System.Diagnostics.EventLog.CreateEventSource("AppSASJobs", "Application")
    '            End If
    '            Dim ELog As New EventLog("AppSASJobs", ".", "Application")
    '            ELog.WriteEntry(sMsg)
    '        Catch ex As Exception
    '            '
    '        End Try
    '    End Sub
    '    '=======================
    '    '   reschedule status
    '    '======================
    '    Public Function NewStatus(ByRef dtRow As DataRow) As Boolean

    '        Try
    '            Dim istat As Integer = CInt(dtRow.Item("status"))
    '            If istat > 100 Then istat -= 100
    '            If istat > 100 Then istat -= 100
    '            Select Case istat
    '                Case 1 'run once
    '                    'dtRow.Item("status") = 250
    '                    'dtRow.Item("run_dt") = DateAdd(DateInterval.Year, 99, Now)
    '                    dtRow.Item("Status") = "0"

    '                Case 2 ' run manually
    '                    dtRow.Item("status") = "0"
    '                'dtRow.Item("run_dt") = DateAdd(DateInterval.Year, 99, Now)

    '                Case 3 ' run every 5 minutes
    '                    dtRow.Item("status") = istat
    '                    dtRow.Item("run_dt") = DateAdd(DateInterval.Minute, 5, Now)

    '                Case 4 ' run every hour
    '                    dtRow.Item("status") = istat
    '                    dtRow.Item("run_dt") = DateAdd(DateInterval.Hour, 1, dtRow.Item("run_dt"))

    '                Case 5 ' run every 4 hours
    '                    dtRow.Item("status") = istat
    '                    dtRow.Item("run_dt") = DateAdd(DateInterval.Hour, 4, dtRow.Item("run_dt"))

    '                Case 6 ' run every 8 hours
    '                    dtRow.Item("status") = istat
    '                    dtRow.Item("run_dt") = DateAdd(DateInterval.Hour, 4, dtRow.Item("run_dt"))

    '                Case 7 ' run every 12 hours
    '                    dtRow.Item("status") = istat
    '                    dtRow.Item("run_dt") = DateAdd(DateInterval.Hour, 12, dtRow.Item("run_dt"))

    '                Case 8 ' run every day
    '                    dtRow.Item("status") = istat
    '                    dtRow.Item("run_dt") = DateAdd(DateInterval.Day, 1, dtRow.Item("run_dt"))

    '                Case 9 ' run every week
    '                    dtRow.Item("status") = istat
    '                    dtRow.Item("run_dt") = DateAdd(DateInterval.Day, 7, dtRow.Item("run_dt"))

    '                Case 9
    '                Case 10
    '                Case 11
    '                Case 12

    '            End Select
    '            Return True
    '        Catch ex As Exception
    '            sErr = "NewStatus Err: " & ex.Message
    '        End Try
    '        Return False
    '    End Function
#End Region

End Class

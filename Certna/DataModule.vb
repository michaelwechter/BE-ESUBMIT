Module DataModule
    Class ManifestDocuments
        Public Sub New(ByVal md_file As String, ByVal md_Code As String, ByVal md_DocumentSequenceIdentifier As String, ByVal md_UniqueIdentifier As String, ByVal md_PagesCount As Integer, ByVal md_diskfile As String)
            ' Set Fields
            Me.md_file = md_file
            Me.md_Code = md_Code
            Me.md_DocumentSequenceIdentifier = md_DocumentSequenceIdentifier
            Me.md_UniqueIdentifier = md_UniqueIdentifier
            Me.md_PagesCount = md_PagesCount
            Me.md_Diskfile = md_diskfile
        End Sub

        ' Storage
        Public md_file As String
        Public md_Code As String
        Public md_DocumentSequenceIdentifier As String
        Public md_UniqueIdentifier As String
        Public md_PagesCount As Integer
        Public md_Diskfile As String
    End Class
    Public Structure xmlstruct
        Public element As String
        Public attribute As String
        Public value As String
    End Structure
    Public Structure manifest
        Public file As String
        Public Code As String
        Public DocumentSequenceIdentifier As String
        Public UniqueIdentifier As String
        Public PagesCount As String
        Public diskfile As String
    End Structure
    Public Function getlistval(ByVal element As String, ByVal attribute As String)
        getlistval = -1
        For i = 0 To Form1.xmllist.Count - 1
            If Form1.xmllist(i).element = element And Form1.xmllist(i).attribute = attribute Then
                getlistval = Form1.xmllist(i).value
                Exit For
            End If
        Next
        Return getlistval
    End Function
    Public Function getlistpages()
        getlistpages = 0
        For i = 0 To Form1.xmllist.Count - 1
            If Form1.xmllist(i).element = "DOCUMENT" Then
                getlistpages = getlistpages + 1
            End If
        Next
        Return getlistpages
    End Function
End Module

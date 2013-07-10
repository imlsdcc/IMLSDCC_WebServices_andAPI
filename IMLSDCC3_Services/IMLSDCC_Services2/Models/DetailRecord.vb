Imports System.Xml

Public Class DetailRecord
    Public Property title As String

    Public Property identifier As String

    Public Property thumbnail As String

    Public Property creator As String

    Public Property type As List(Of String)

    'Public Property type As String

    Public Property isPartOf As String

    Public Property dateof As String

    Public Property language As String

    Public Property format As String

    Public Property description As String

    Public Property subject As List(Of String)

    'Public Property coverage As String

    Public Property relation As String

    Public Property source As String
    Public Property cid As String

    Public Property publisher As String
    Public Property rights As String
    Public Property errors As List(Of String)
    Public Property queries As List(Of String)

    Public Sub New(ByVal id As Integer)
        Dim db = New IMLSDCCDataContext
        Dim temperrors As New List(Of String)
        Dim tempqueries As New List(Of String)

        Dim rs = From r In db.Records Where r.recordID = id Select r.longXML, r.shortXML, r.identifier, r.cid
        If rs.Count() > 0 Then
            Dim z = rs.Single

            Dim x = z.shortXML
            Dim y = z.longXML

            Dim dom As New XmlDocument
            Dim domy As New XmlDocument
            dom.LoadXml(x.ToString)
            domy.LoadXml(y.ToString)

            cid = z.cid

            If Not (dom.SelectSingleNode("//property[@name='title']/value") Is Nothing) Then
                title = dom.SelectSingleNode("//property[@name='title']/value").InnerText
            End If
            If Not (dom.SelectSingleNode("//property[@name='identifier']/value") Is Nothing) Then
                identifier = z.identifier
            End If
            If Not (dom.SelectSingleNode("//property[@name='creator']/value") Is Nothing) Then
                creator = dom.SelectSingleNode("//property[@name='creator']/value").InnerText
            End If


            Dim rs2 = From t In db.Types Join r In db.RecordsToTypes On t.typeID Equals r.typeID Where r.recordID = id Select t.typeText
            Dim list As New List(Of String)
            For Each j In rs2
                list.Add(j)
            Next
            type = list

            If Not (dom.SelectSingleNode("//property[@name='isPartOf']/value") Is Nothing) Then
                isPartOf = dom.SelectSingleNode("//property[@name='isPartOf']/value").InnerText
            End If
            If Not (domy.SelectSingleNode("//property[@name='format']/value") Is Nothing) Then
                format = domy.SelectSingleNode("//property[@name='format']/value").InnerText
            End If
            If Not (domy.SelectSingleNode("//property[@name='date']/value") Is Nothing) Then
                dateof = domy.SelectSingleNode("//property[@name='date']/value").InnerText
            End If
            If Not (domy.SelectSingleNode("//property[@name='language']/value") Is Nothing) Then
                language = domy.SelectSingleNode("//property[@name='language']/value").InnerText
            End If
            If Not (domy.SelectSingleNode("//property[@name='description']/value") Is Nothing) Then
                description = domy.SelectSingleNode("//property[@name='description']/value").InnerText
            End If
            If Not (domy.SelectSingleNode("//property[@name='source']/value") Is Nothing) Then
                source = domy.SelectSingleNode("//property[@name='source']/value").InnerText
            End If

            If Not (domy.SelectSingleNode("//property[@name='publisher']/value") Is Nothing) Then
                publisher = domy.SelectSingleNode("//property[@name='publisher']/value").InnerText
            End If

            If Not (domy.SelectSingleNode("//property[@name='rights']/value") Is Nothing) Then
                rights = domy.SelectSingleNode("//property[@name='rights']/value").InnerText
            End If

            If Not (domy.SelectSingleNode("//property[@name='relation']/value") Is Nothing) Then
                relation = domy.SelectSingleNode("//property[@name='relation']/value").InnerText
            End If
            If Not thumbnail Is Nothing Then
                thumbnail = identifier.Replace("/u?/", ":")
                thumbnail = thumbnail.Replace("http://", "")
                thumbnail = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & thumbnail.Replace(",", "/")
            End If
            

            Dim rsubjects = From s In db.Subjects Join r In db.RecordsToSubjects On s.subjectID Equals r.subjectID Where r.recordID = id Select s.subjectText
            Dim listsubjects As New List(Of String)
            For Each j In rsubjects
                listsubjects.Add(j)
            Next
            subject = listsubjects

            tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
            queries = tempqueries
            'coverage = domy.SelectSingleNode("//property[@name='coverage']/value").InnerText
            'relation = domy.SelectSingleNode("//property[@name='relation']/value").InnerText
        Else
            temperrors.Add("Record does not exist.")

            tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
            queries = tempqueries
        End If
        errors = temperrors
    End Sub
End Class

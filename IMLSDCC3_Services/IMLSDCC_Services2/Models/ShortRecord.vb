Imports System.Xml

Public Class ShortRecord
    Public Property title As String

    Public Property identifier As String

    Public Property detailurl As String

    Public Property uniqueid As String

    Public Property creator As String
    Public Property creatorurl As String
    Public Property collectionurl As String
    Public Property collectionbrowse As String

    Public Property type As String

    Public Property isPartOf As String

    Public Property thumbnail As String

    Public Property errors As List(Of String)
    Public Property queries As List(Of String)

    Public Sub New(ByVal id As Integer)

        Dim db = New IMLSDCCDataContext
        Dim temperrors As New List(Of String)
        Dim tempqueries As New List(Of String)
        detailurl = "/item/detail/" & id
        uniqueid = "/item/brief/" & id


        Dim rs = From r In db.Records Where r.recordID = id Select r.longXML, r.shortXML, r.identifier
        If rs.Count() > 0 Then
            Dim z = rs.Single
            Dim x = z.shortXML
            Dim dom As New XmlDocument
            dom.LoadXml(x.ToString)
            identifier = z.identifier

            Dim rs2 = From r In db.RecordsToCollections Where r.recordID = id Select r.collID
            Dim zz = rs2.Single

            'collectionurl = "/Collection/Detail/" & zz
            collectionurl = "/Detail/Collection/" & zz
            'collectionbrowse = "/Browse/Item/Collection?startswith=" & zz & "&startRecord=1&maximumRecords=10"
            collectionbrowse = "/ResultList/Items/1?collections=" & zz & "&iStartRecord=1&iMaximumRecords=10"


            If Not (dom.SelectSingleNode("//property[@name='title']/value") Is Nothing) Then
                title = dom.SelectSingleNode("//property[@name='title']/value").InnerText
            End If
            If Not (dom.SelectSingleNode("//property[@name='identifier']/value[starts-with(.,'http')]") Is Nothing) Then

                'identifier = dom.SelectSingleNode("//property[@name='identifier']/value[starts-with(.,'http')]").InnerText
            End If
            'If Not (z.identifier Is Nothing) Then
            '    identifier = z.identifier
            'End If
            If Not (dom.SelectSingleNode("//property[@name='creator']/value") Is Nothing) Then
                creator = dom.SelectSingleNode("//property[@name='creator']/value").InnerText
                'creatorurl = "/Search/Items?agent=" & creator & "&dates=&types=&places=&collections=&startRecord=1&maximumRecords=10&scope=&sort="
                creatorurl = "/ResultList/Items/1?agent=" & creator & "&iStartRecord=1&iMaximumRecords=10&scope=&sort="
            End If
            If Not (dom.SelectSingleNode("//property[@name='type']/value") Is Nothing) Then
                type = dom.SelectSingleNode("//property[@name='type']/value").InnerText
            End If
            If Not (dom.SelectSingleNode("//property[@name='isPartOf']/value") Is Nothing) Then
                isPartOf = dom.SelectSingleNode("//property[@name='isPartOf']/value").InnerText
            End If
            thumbnail = z.identifier.Replace("/u?/", ":")
            thumbnail = thumbnail.Replace("http://", "")
            thumbnail = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & thumbnail.Replace(",", "/")

            tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
            queries = tempqueries
        Else
            temperrors.Add("Record does not exist.")
            tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
            queries = tempqueries
        End If
        errors = temperrors

    End Sub
End Class

Imports System.Xml

Public Class DetailCollection
    Public Property title As String

    Public Property description As String

    Public Property isavailableat As String

    Public Property subject As List(Of String)

    Public Property GEMsubjectlist As List(Of GEMsubject)

    Public Property geographic As List(Of String)

    Public Property timeperiod As List(Of String)

    Public Property objectsrepresented As List(Of String)

    Public Property format As List(Of String)

    Public Property audience As List(Of String)

    Public Property interactivity As List(Of String)

    Public Property copyright As List(Of String)

    Public Property size As List(Of String)

    Public Property frequency As List(Of String)

    Public Property supplementary As List(Of String)

    Public Property hosting As List(Of String)

    Public Property contributing As List(Of String)
    Public Property itemsInCollection As String
    Public Property thumbnail1 As String
    Public Property thumbnail2 As String
    Public Property thumbnail3 As String
    Public Property thumbnail4 As String
    Public Property errors As List(Of String)
    Public Property queries As List(Of String)

    Public Class GEMsubject
        Public Property gemtop As String
        Public Property gemlist As List(Of String)
        Public Sub New(ByVal xgemtop As String, ByVal xgemlist As List(Of String))
            gemtop = xgemtop
            gemlist = xgemlist
        End Sub
    End Class

    Public Sub New(ByVal id As Integer)

        Dim db = New IMLSDCCIHDataContext
        Dim db2 = New IMLSDCCDataContext
        Dim temperrors As New List(Of String)
        Dim tempqueries As New List(Of String)
        Dim rs = From r In db.ShortDisplays Where r.collectionID = id Select r.title_collection, r.description, r.isAvailableAt_URL
        If rs.Count() > 0 Then
            Dim z = rs.Single

            Dim rsthumb = From s In db2.CollectionsToThumbnails Where s.collID = id Select s.oaiidentifier1, s.oaiidentifier2, s.oaiidentifier3, s.oaiidentifier4
            If rsthumb.Count() > 0 Then
                Dim y = rsthumb.Single
                thumbnail1 = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & y.oaiidentifier1
                thumbnail2 = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & y.oaiidentifier2
                thumbnail3 = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & y.oaiidentifier3
                thumbnail4 = "http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=" & y.oaiidentifier4

            End If

            itemsInCollection = (From r In db2.Records Where r.cid = id Select r.recordID).Count()

            title = z.title_collection
            description = z.description
            isavailableat = z.isAvailableAt_URL

            Dim rs2 = From r In db.CollectionProperties Where r.collectionID = id And r.property = "subject" And r.propertyType = "LC Subject Headings" Select r.text

            Dim list As New List(Of String)
            For Each j In rs2
                list.Add(j)
            Next
            subject = list

            rs2 = From r In db.CollectionProperties Where r.collectionID = id And r.property = "subject" And r.propertyType = "GEMTOP" Select r.text

            Dim tempgemtop As String
            Dim tempGEMsubjectlist As New List(Of GEMsubject)

            For Each j In rs2
                tempgemtop = j
                Dim temp As String
                temp = "GEM_" & Replace(j, " ", "_")
                Dim tempgemlist As New List(Of String)
                Dim rs3 = From r In db.CollectionProperties Where r.collectionID = id And r.property = "subject" And r.propertyType = temp Select r.text
                For Each k In rs3
                    tempgemlist.Add(k)
                Next
                Dim tempgemsubject As New GEMsubject(tempgemtop, tempgemlist)
                tempGEMsubjectlist.Add(tempgemsubject)
            Next
            GEMsubjectlist = tempGEMsubjectlist


            rs2 = From r In db.CollectionProperties Where r.collectionID = id And r.property = "coverage_spatial" Select r.text
            Dim geolist As New List(Of String)
            For Each j In rs2
                geolist.Add(j)
            Next
            geographic = geolist


            rs2 = From r In db.CollectionProperties Where r.collectionID = id And r.property = "coverage_temporal" Select r.text
            Dim timelist As New List(Of String)
            For Each j In rs2
                timelist.Add(j)
            Next
            timeperiod = timelist

            'Dim rslong = From r In db.LongDisplays Where r.collectionID = id Select r.type_collection, r.format, r.publisher, r.relation_supplement, r.size, r.audience, r.interactivity, r.accessrights, r.rights, r.accrualPeriodicity, r.contributor
            Dim rslong = From r In db.collectionDetails(id)
            Dim tempobjectsrepresented As New List(Of String)
            Dim tempformat As New List(Of String)
            Dim temphosting As New List(Of String)
            Dim tempsupplementary As New List(Of String)
            Dim tempsize As New List(Of String)
            Dim tempaudience As New List(Of String)
            Dim tempinteractivity As New List(Of String)
            Dim tempcopyright As New List(Of String)
            Dim tempfrequency As New List(Of String)
            Dim tempcontributing As New List(Of String)
            For Each j In rslong
                If (j.property = "type_collection") Then
                    tempobjectsrepresented.Add(j.value)
                End If
                If (j.property = "format") Then
                    tempformat.Add(j.value)
                End If
                If (j.property = "publisher") Then
                    temphosting.Add(j.value)
                End If
                If (j.property = "relation_supplement") Then
                    tempsupplementary.Add(j.value)
                End If
                If (j.property = "size") Then
                    tempsize.Add(j.value)
                End If
                If (j.property = "audience") Then
                    tempaudience.Add(j.value)
                End If
                If (j.property = "interactivity") Then
                    tempinteractivity.Add(j.value)
                End If
                If (j.property = "rights") Then
                    tempcopyright.Insert(0, j.value)
                End If
                If (j.property = "accessRights") Then
                    tempcopyright.Add(j.value)
                End If

                If (j.property = "accrualPeriodicity") Then
                    tempfrequency.Add(j.value)
                End If
                If (j.property = "contributor") Then
                    tempcontributing.Add(j.value)
                End If
                'tempobjectsrepresented.Add(j.type_collection)
                'tempformat.Add(j.format)
                'temphosting.Add(j.publisher)
                'tempsupplementary.Add(j.relation_supplement)
                'tempsize.Add(j.size)
                'tempaudience.Add(j.audience)
                'tempinteractivity.Add(j.interactivity)
                'tempcopyright.Add(j.accessrights)
                'tempcopyright.Add(j.rights)
                'tempfrequency.Add(j.accrualPeriodicity)
                'tempcontributing.Add(j.contributor)
            Next
            objectsrepresented = tempobjectsrepresented.Distinct().ToList()
            format = tempformat.Distinct().ToList()
            hosting = temphosting.Distinct().ToList()
            hosting.Remove(Nothing)
            supplementary = tempsupplementary.Distinct().ToList()
            size = tempsize.Distinct().ToList()
            audience = tempaudience.Distinct().ToList()
            interactivity = tempinteractivity.Distinct().ToList()
            copyright = tempcopyright.Distinct().ToList()
            frequency = tempfrequency.Distinct().ToList()
            contributing = tempcontributing.Distinct().ToList()
            contributing.Remove(Nothing)

            
            tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
            tempqueries = New GetQueries(tempqueries, db2.GetLoggedInformation()).rtrnList
            queries = tempqueries

        Else
            temperrors.Add("Record does not exist.")
            tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
            queries = tempqueries
        End If
        errors = temperrors

    End Sub
End Class

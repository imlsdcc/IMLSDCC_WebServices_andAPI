Public Class SearchCollectionStates
    Public Class items
        Public Property uri As String
        Public Property briefuri As String
        Public Property text As String
        Public Sub New(ByVal xuri As String, ByVal buri As String, ByVal xtext As String)
            uri = xuri
            briefuri = buri
            text = xtext
        End Sub
    End Class

    Public Property cStartRecord As Integer

    Public Property cMaximumRecords As Integer

    Public Property totalRecords As Integer

    Public Property uriThisPage As String

    Public Property uriPreviousPage As String

    Public Property uriNextPage As String

    Public Property itemList As List(Of items)
    Public Property errors As List(Of String)
    Public Property queries As List(Of String)

    Public Sub New(ByVal searchargs1 As String, ByVal wheretosearch As String, ByVal startRecord1 As Integer, ByVal maximumRecords1 As Integer, ByVal scope1 As String, ByVal sort1 As String)
        Dim db = New IMLSDCCDataContext
        Dim db2 = New IMLSDCCIHDataContext
        Dim typelist As String()
        typelist = searchargs1.Split(",")
        Dim temperrors As New List(Of String)
        Dim tempqueries As New List(Of String)
        If startRecord1 Then
            cStartRecord = startRecord1
        Else
            cStartRecord = 1
            startRecord1 = 1
        End If
        'startRecord = startRecord1
        If maximumRecords1 Then
            cMaximumRecords = maximumRecords1
        Else
            cMaximumRecords = 10
            maximumRecords1 = 10
        End If

        uriThisPage = "/Search/Collection?state=" & searchargs1 & "&cStartRecord=" & startRecord1 & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriNextPage = "/Search/Collection?state=" & searchargs1 & "&cStartRecord=" & startRecord1 + maximumRecords1 & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1
        uriPreviousPage = "/Search/Collection?state=" & searchargs1 & "&cStartRecord=" & startRecord1 - maximumRecords1 & "&cMaximumRecords=" & maximumRecords1 & "&scope=" & scope1 & "&sort=" & sort1

        Dim name = (From f In db.Facets Where typelist.Contains(f.facetID) Select f.facetValue).FirstOrDefault()
        Dim ret As List(Of Integer) = Nothing
        If Not name Is Nothing Then
            ret = (From c In db2.Collections Join ci In db2.CollectionInstitutions On ci.collectionID Equals c.collectionID
                   Join i In db2.Institutions On i.institutionID Equals ci.institutionID
                   Join ip In db2.InstitutionProperties On ip.institutionID Equals i.institutionID
                   Join cp In db2.CollectionProperties On cp.collectionID Equals c.collectionID
                   Where ip.text = name And ci.relationship = "hosting" And ip.property = "state" And cp.property = "title_collection" And (Not c.dlf Is Nothing Or Not c.dpla Is Nothing Or Not c.hist Is Nothing Or Not c.ih Is Nothing Or Not c.imls Is Nothing)
                   Select c.collectionID).Distinct.ToList
        End If
        'Dim ret = (From cf In db.CollectionsToFacets Where typelist.Contains(cf.facetID) Select cf.collid).Distinct

        

        Dim tempitemList As New List(Of items)
        Dim list As New List(Of String)
        'Dim fd = ret.AsQueryable().FirstOrDefault()
        If Not ret Is Nothing Then
            totalRecords = ret.Count

            For Each j In ret.Skip(startRecord1 - 1).Take(maximumRecords1)
                Dim rs = (From r In db2.CollectionProperties Where r.collectionID = j And r.property = "title_collection" Select r.text)
                Dim z = rs.Single
                Dim tempitem As New items("/Collection/Detail/" & j, "/Collection/Brief/" & j, Left(z, 127))
                tempitemList.Add(tempitem)
            Next
            itemList = tempitemList
        Else
            temperrors.Add("No results found.")
            totalRecords = 0
        End If

        tempqueries = New GetQueries(tempqueries, db.GetLoggedInformation()).rtrnList
        queries = tempqueries
        errors = temperrors
    End Sub
End Class

Namespace IMLSDCC_Services2
    Public Class FacetsController
        Inherits System.Web.Mvc.Controller




        Public Function Dates() As ActionResult

            Dim dates1 = Request.QueryString("dates")
            Dim types = Request.QueryString("types")
            Dim places = Request.QueryString("places")
            Dim collections = Request.QueryString("collections")
            Dim startRecord = Request.QueryString("startRecord")
            Dim maximumRecords = Request.QueryString("maximumRecords")
            Dim scope = Request.QueryString("scope")
            Dim sort = Request.QueryString("sort")

            Dim anywhere = Request.QueryString("anywhere")
            Dim anywheres As String()
            If Not anywhere Is Nothing Then
                anywheres = anywhere.Split(",")
                Dim sh As New FacetsDate(anywheres, "anywhere", dates1, types, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim keyword = Request.QueryString("keyword")
            Dim keywords As String()
            If Not keyword Is Nothing Then
                keywords = keyword.Split(",")
                Dim sh As New FacetsDate(keywords, "keyword", dates1, types, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim title = Request.QueryString("title")
            Dim titles As String()
            If Not title Is Nothing Then
                titles = title.Split(",")
                Dim sh As New FacetsDate(titles, "title", dates1, types, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim agent = Request.QueryString("agent")
            Dim agents As String()
            If Not agent Is Nothing Then
                agents = agent.Split(",")
                Dim sh As New FacetsDate(agents, "creator", dates1, types, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

        End Function

        Public Function Types() As ActionResult

            Dim dates1 = Request.QueryString("dates")
            Dim types1 = Request.QueryString("types")
            Dim places = Request.QueryString("places")
            Dim collections = Request.QueryString("collections")
            Dim startRecord = Request.QueryString("startRecord")
            Dim maximumRecords = Request.QueryString("maximumRecords")
            Dim scope = Request.QueryString("scope")
            Dim sort = Request.QueryString("sort")

            Dim anywhere = Request.QueryString("anywhere")
            Dim anywheres As String()
            If Not anywhere Is Nothing Then
                anywheres = anywhere.Split(",")
                Dim sh As New FacetsType(anywheres, "anywhere", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim keyword = Request.QueryString("keyword")
            Dim keywords As String()
            If Not keyword Is Nothing Then
                keywords = keyword.Split(",")
                Dim sh As New FacetsType(keywords, "keyword", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim title = Request.QueryString("title")
            Dim titles As String()
            If Not title Is Nothing Then
                titles = title.Split(",")
                Dim sh As New FacetsType(titles, "title", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim agent = Request.QueryString("agent")
            Dim agents As String()
            If Not agent Is Nothing Then
                agents = agent.Split(",")
                Dim sh As New FacetsType(agents, "creator", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

        End Function

        Public Function Place() As ActionResult

            Dim dates1 = Request.QueryString("dates")
            Dim types1 = Request.QueryString("types")
            Dim places = Request.QueryString("places")
            Dim collections = Request.QueryString("collections")
            Dim startRecord = Request.QueryString("startRecord")
            Dim maximumRecords = Request.QueryString("maximumRecords")
            Dim scope = Request.QueryString("scope")
            Dim sort = Request.QueryString("sort")

            Dim anywhere = Request.QueryString("anywhere")
            Dim anywheres As String()
            If Not anywhere Is Nothing Then
                anywheres = anywhere.Split(",")
                Dim sh As New FacetsPlace(anywheres, "anywhere", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim keyword = Request.QueryString("keyword")
            Dim keywords As String()
            If Not keyword Is Nothing Then
                keywords = keyword.Split(",")
                Dim sh As New FacetsPlace(keywords, "keyword", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim title = Request.QueryString("title")
            Dim titles As String()
            If Not title Is Nothing Then
                titles = title.Split(",")
                Dim sh As New FacetsPlace(titles, "title", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim agent = Request.QueryString("agent")
            Dim agents As String()
            If Not agent Is Nothing Then
                agents = agent.Split(",")
                Dim sh As New FacetsPlace(agents, "creator", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

        End Function

        Public Function All() As ActionResult

            Dim dates1 = Request.QueryString("dates")
            Dim types1 = Request.QueryString("types")
            Dim places = Request.QueryString("places")
            Dim collections = Request.QueryString("collections")
            Dim startRecord = Request.QueryString("startRecord")
            Dim maximumRecords = Request.QueryString("maximumRecords")
            Dim scope = Request.QueryString("scope")
            Dim sort = Request.QueryString("sort")

            Dim anywhere = Request.QueryString("anywhere")
            Dim anywheres As String()
            If Not anywhere Is Nothing Then
                anywheres = anywhere.Split(",")
                Dim sh As New FacetsAll(anywheres, "anywhere", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim keyword = Request.QueryString("keyword")
            Dim keywords As String()
            If Not keyword Is Nothing Then
                keywords = keyword.Split(",")
                Dim sh As New FacetsAll(keywords, "keyword", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim title = Request.QueryString("title")
            Dim titles As String()
            If Not title Is Nothing Then
                titles = title.Split(",")
                Dim sh As New FacetsAll(titles, "title", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim agent = Request.QueryString("agent")
            Dim agents As String()
            If Not agent Is Nothing Then
                agents = agent.Split(",")
                Dim sh As New FacetsAll(agents, "creator", dates1, types1, places, collections, startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

        End Function

        Public Function GetFacetValue() As ActionResult

            Dim db = New IMLSDCCDataContext
            Dim facetIds = Request.QueryString("ids").Split(",")
            Dim facetList As New List(Of facets)
            For Each f In facetIds
                If f <> "" Then
                    Dim str = (From r In db.Facets Where r.facetID = f Select r.facetValue).Take(1).ToList()

                    If Not str Is Nothing And str.Count > 0 Then
                        Dim item As New facets(str(0), f)
                        facetList.Add(item)
                    End If
                End If
            Next

            Return Json(facetList, "application/json", JsonRequestBehavior.AllowGet)
        End Function

        Public Class facets
            'Public Property uri As String
            Public Property text As String
            Public Property facetid As Integer
            Public Sub New(ByVal xtext As String, ByVal xfacetid As Integer)
                'uri = xuri
                text = xtext
                facetid = xfacetid
            End Sub
        End Class
    End Class
End Namespace
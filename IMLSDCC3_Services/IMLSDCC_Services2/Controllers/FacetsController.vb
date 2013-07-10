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

        Public Function GetFacetName() As ActionResult
            Dim facetIds = Request.QueryString("ids").Split(",")

            For Each f In facetIds

            Next
        End Function
    End Class
End Namespace
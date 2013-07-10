Imports System.Net
Imports System.IO
Imports System.Web
Namespace IMLSDCC_Services2
    Public Class SearchController
        Inherits System.Web.Mvc.Controller


        Public Function Items() As ActionResult
            'Comment
            Dim dates = Request.QueryString("idates")
            Dim types = Request.QueryString("itypes")
            Dim places = Request.QueryString("iplaces")
            Dim collections = Request.QueryString("collections")
            Dim startRecord = Request.QueryString("iStartRecord")
            Dim maximumRecords = Request.QueryString("iMaximumRecords")
            Dim scope = Request.QueryString("scope")
            Dim sort = Request.QueryString("sort")
            Dim withFacets = Request.QueryString("withFacets")
            If withFacets Is Nothing Then
                withFacets = False
            End If
            Dim anywhere = Request.QueryString("anywhere")
            Dim anywheres As String()
            If Not anywhere Is Nothing Then
                anywheres = anywhere.Split(",")
                Dim sh As New SearchItems(anywheres, "anywhere", dates, types, places, collections, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim keyword = Request.QueryString("keyword")
            Dim keywords As String()
            If Not keyword Is Nothing Then
                keywords = keyword.Split(",")
                Dim sh As New SearchItems(keywords, "keyword", dates, types, places, collections, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim title = Request.QueryString("title")
            Dim titles As String()
            If Not title Is Nothing Then
                titles = title.Split(",")
                Dim sh As New SearchItems(titles, "title", dates, types, places, collections, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim agent = Request.QueryString("agent")
            Dim agents As String()
            If Not agent Is Nothing Then
                agents = agent.Split(",")
                Dim sh As New SearchItems(agents, "creator", dates, types, places, collections, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim subject = Request.QueryString("subject")
            If Not subject Is Nothing Then
                Dim sh As New SearchSubjects(subject, "subject", startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim type = Request.QueryString("type")
            If Not type Is Nothing Then
                Dim sh As New SearchTypes(type, "type", startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim dateBr = Request.QueryString("date")
            If Not dateBr Is Nothing Then
                Dim sh As New SearchDates(dateBr, "date", startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim state = Request.QueryString("state")
            If Not state Is Nothing Then
                Dim sh As New SearchStates(state, "state", startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            If anywhere Is Nothing And Not collections Is Nothing Then
                Dim sh As New SearchItemsInCollection(collections, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

        End Function

        Public Function Collections() As ActionResult

            Dim dates = Request.QueryString("cdates")
            Dim types = Request.QueryString("ctypes")
            Dim places = Request.QueryString("cplaces")
            Dim collections1 = Request.QueryString("collections")
            Dim startRecord = Request.QueryString("cStartRecord")
            Dim maximumRecords = Request.QueryString("cMaximumRecords")
            Dim scope = Request.QueryString("scope")
            Dim sort = Request.QueryString("sort")
            Dim withFacets = Request.QueryString("withFacets")
            If withFacets Is Nothing Then
                withFacets = False
            End If
            Dim anywhere = Request.QueryString("anywhere")

            Dim anywheres As String()
            If Not anywhere Is Nothing Then
                anywheres = anywhere.Split(",")
                Dim sh As New SearchCollections(anywheres, "anywhere", dates, types, places, collections1, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim keyword = Request.QueryString("keyword")
            Dim keywords As String()
            If Not keyword Is Nothing Then
                keywords = keyword.Split(",")
                Dim sh As New SearchCollections(keywords, "keyword", dates, types, places, collections1, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim title = Request.QueryString("title")
            Dim titles As String()
            If Not title Is Nothing Then
                titles = title.Split(",")
                Dim sh As New SearchCollections(titles, "title", dates, types, places, collections1, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim agent = Request.QueryString("agent")
            Dim agents As String()
            If Not agent Is Nothing Then
                agents = agent.Split(",")
                Dim sh As New SearchCollections(agents, "creator", dates, types, places, collections1, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim institution = Request.QueryString("institution")
            If Not institution Is Nothing Then
                Dim sh As New SearchCollectionInstitutions(institution, "institution", startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim subjectItem = Request.QueryString("subject")
            Dim subjectItems As String()
            If Not subjectItem Is Nothing Then
                subjectItems = subjectItem.Split(",")
                Dim sh As New SearchCollections(subjectItems, "subject", dates, types, places, collections1, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim typeItem = Request.QueryString("type")
            Dim typeItems As String()
            If Not typeItem Is Nothing Then
                typeItems = typeItem.Split(",")
                Dim sh As New SearchCollections(typeItems, "type", dates, types, places, collections1, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim dateItem = Request.QueryString("date")
            Dim dateItems As String()
            If Not dateItem Is Nothing Then
                dateItems = dateItem.Split(",")
                Dim sh As New SearchCollections(dateItems, "date", dates, types, places, collections1, startRecord, maximumRecords, scope, sort, withFacets)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            Dim subject = Request.QueryString("subjectCollection")
            If Not subject Is Nothing Then
                Dim sh As New SearchCollectionSubjects(subject, "subject", startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            'Dim type = Request.QueryString("type")
            'If Not type Is Nothing Then
            '    Dim sh As New SearchCollectionTypes(type, "type", startRecord, maximumRecords, scope, sort)
            '    Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            'End If

            Dim state = Request.QueryString("state")
            If Not state Is Nothing Then
                Dim sh As New SearchCollectionStates(state, "state", startRecord, maximumRecords, scope, sort)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

            'Dim dateBr = Request.QueryString("date")
            'If Not dateBr Is Nothing Then
            '    Dim sh As New SearchCollectionDates(dateBr, "date", startRecord, maximumRecords, scope, sort)
            '    Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            'End If
        End Function

        Public Function SecondarySearch(ByVal searchType As String, ByVal url As String) As String
            Dim validTargets() As String = {"hathi", "flickr"}, str As String = "", searchUrl As String = ""
            If Not validTargets.Contains(searchType) Then
                Response.Write("Error: Unsupported Search Type: " & searchType)
            End If

            'Try
            searchUrl = "http://search.grainger.uiuc.edu/searchaid3/resultslist.asp?searchtype=" & searchType & "&url=" & Server.UrlEncode(url)
            Dim req As HttpWebRequest = WebRequest.Create(searchUrl)
            Dim rsp As HttpWebResponse = req.GetResponse

            Dim rdr As New StreamReader(rsp.GetResponseStream)
            str = rdr.ReadToEnd
            rdr.Close()

            Response.Write(str)
            'Catch ex As Exception
            'Response.Write("Error: " & Server.HtmlEncode(ex.Message))
            'End Try
            Return str
        End Function


    End Class
End Namespace

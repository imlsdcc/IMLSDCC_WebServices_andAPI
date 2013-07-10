Namespace IMLSDCC_Services2
    Public Class CollectionController
        Inherits System.Web.Mvc.Controller

        Function Brief(ByVal id As Integer) As ActionResult

            Dim sh As New BriefCollection(id)
            Dim view = Request.QueryString("view")
            If view = "html" Then
                ViewData("title") = sh.title
                ViewData("description") = sh.description
                ViewData("isavailableat") = sh.isavailableat
                ViewData("id") = id
                Return PartialView()
            Else
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If
        End Function

        Function Detail(ByVal id As Integer) As ActionResult

            Dim sh As New DetailCollection(id)
            Dim view = Request.QueryString("view")
            If view = "html" Then
                ViewData("title") = sh.title
                ViewData("description") = sh.description
                ViewData("isavailableat") = sh.isavailableat
                ViewData("subject") = sh.subject
                ViewData("GEMsubjectlist") = sh.GEMsubjectlist
                ViewData("geographic") = sh.geographic
                ViewData("timeperiod") = sh.timeperiod
                ViewData("objectsrepresented") = sh.objectsrepresented
                ViewData("format") = sh.format
                ViewData("interactivity") = sh.interactivity
                ViewData("copyright") = sh.copyright
                ViewData("size") = sh.size
                ViewData("audience") = sh.audience
                ViewData("frequency") = sh.frequency
                ViewData("supplementary") = sh.supplementary
                ViewData("hosting") = sh.hosting
                ViewData("contributing") = sh.contributing
                ViewData("size") = sh.size
                ViewData("errorscount") = sh.errors.Count
                ViewData("errors") = sh.errors
                ViewData("id") = id
                Return PartialView()
            Else
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If
        End Function

    End Class
End Namespace

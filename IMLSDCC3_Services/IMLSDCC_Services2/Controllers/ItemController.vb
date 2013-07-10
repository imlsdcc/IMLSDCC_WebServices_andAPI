Namespace IMLSDCC_Services2
    Public Class ItemController
        Inherits System.Web.Mvc.Controller

    Function Brief(id As Integer) As ActionResult
            Dim sh As New ShortRecord(id)
            Dim view = Request.QueryString("view")
            If view = "html" Then
                ViewData("title") = sh.title
                ViewData("identifier") = sh.identifier
                ViewData("creator") = sh.creator
                ViewData("type") = sh.type
                ViewData("isPartOf") = sh.isPartOf
                ViewData("id") = id
                Return PartialView()
            Else
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If

        End Function

    Function Detail(id As Integer) As ActionResult

            Dim sh As New DetailRecord(id)
            Dim view = Request.QueryString("view")
            If view = "html" Then
                ViewData("title") = sh.title
                ViewData("identifier") = sh.identifier
                ViewData("creator") = sh.creator
                ViewData("type") = sh.type
                ViewData("dateof") = sh.dateof
                ViewData("language") = sh.language
                ViewData("format") = sh.format
                ViewData("description") = sh.description
                ViewData("subject") = sh.subject
                ViewData("relation") = sh.relation
                ViewData("source") = sh.source
                ViewData("publisher") = sh.publisher
                ViewData("isPartOf") = sh.isPartOf
                ViewData("rights") = sh.rights
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

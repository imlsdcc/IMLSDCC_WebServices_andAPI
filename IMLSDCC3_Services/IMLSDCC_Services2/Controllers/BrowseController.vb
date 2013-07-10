Imports System.Xml
Imports System.Net
Imports System.IO

Namespace IMLSDCC_Services2
    Public Class errorsclass
        Public Property errors As List(Of String)
        Public Sub New(ByVal err As String)
            Dim temperrors As New List(Of String)
            temperrors.Add(err)
            errors = temperrors
        End Sub
    End Class

    Public Class BrowseController
        Inherits System.Web.Mvc.Controller
        Dim rtrnQuery As Boolean = True

        Public Function Item(ByVal id As String, ByVal startsWith As String, ByVal startRecord As Integer, ByVal maximumRecords As Integer) As ActionResult
            If id = "Titles" Then
                Dim sh As New BrowseItemTitles(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            ElseIf id = "Subjects" Then
                Dim sh As New BrowseItemSubjects(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            ElseIf id = "Types" Then
                Dim sh As New BrowseItemTypes(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            ElseIf id = "Dates" Then
                Dim sh As New BrowseItemDates(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            ElseIf id = "Collection" Then
                Dim sh As New BrowseItemCollection(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            Else
                Dim sh As New errorsclass("Invalid Browse Type.")
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If
        End Function


        Public Function Collection(ByVal id As String, ByVal startsWith As String, ByVal startRecord As Integer, ByVal maximumRecords As Integer) As ActionResult
            If id = "Titles" Then
                Dim sh As New BrowseCollectionTitles(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            ElseIf id = "Subjects" Then
                Dim sh As New BrowseCollectionSubjects(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            ElseIf id = "Types" Then
                Dim sh As New BrowseCollectionTypes(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            ElseIf id = "Dates" Then
                Dim sh As New BrowseCollectionDates(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            ElseIf id = "Institutions" Or id = "InstitutionNames" Then
                Dim sh As New BrowseCollectionInstitutions(id, startsWith, startRecord, maximumRecords)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            Else
                Dim sh As New errorsclass("Invalid Browse Type.")
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If
        End Function


        Public Function Institution(ByVal id As String, ByVal startsWith As String, ByVal startRecord As Integer, ByVal maximumRecords As Integer) As ActionResult

            If id = "States" Then
                Dim state = Request.QueryString("state")
                Dim sh As New BrowseInstitutionStates(id, startsWith, startRecord, maximumRecords, state)
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            ElseIf id = "Types" Then
                If startsWith = "" Then
                    Dim type = Request.QueryString("type")
                    Dim sh As New BrowseInstitutionTypes(id, startsWith, startRecord, maximumRecords, type)
                    Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
                Else
                    Dim sh As New errorsclass("Invalid Browse Type.")
                    Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
                End If
            Else
                Dim sh As New errorsclass("Invalid Browse Type.")
                Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
            End If
        End Function

        Public Function Stats() As ActionResult
            Dim sh As New BrowseStats()
            Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
        End Function

        Public Function mapCollections() As ActionResult
            Dim sh As New BrowseMapCollections()
            Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
        End Function
        Public Function subjectBrowse() As ActionResult
            Dim sh As New SubjectBrowse()
            Return Json(sh, "application/json", JsonRequestBehavior.AllowGet)
        End Function

        Function featuredContent() As ActionResult
            Dim myRand As New Random
            Dim feedSize As Integer = 10
            ViewData("FCItem") = Math.Floor(myRand.NextDouble() * feedSize) + 1
            Return PartialView("getFeaturedContent")
        End Function

        Public Function secondarySources() As ActionResult
            Dim validTargets() As String = {"hathi", "googbk", "amer", "jstor", "acse", "scopus"}
            If Not validTargets.Contains(Request("db")) Then
                Response.Write("Error: Unsupported Database: " & Server.HtmlEncode(Request("db")))
            End If

            Try
                Dim url As String = GetSearchServiceURL()
                Dim req As HttpWebRequest = WebRequest.Create(url)
                Dim rsp As HttpWebResponse = req.GetResponse

                Dim rdr As New StreamReader(rsp.GetResponseStream)
                Dim str As String = rdr.ReadToEnd
                rdr.Close()

                Dim xml As New XmlDocument
                xml.LoadXml(str)

                Dim html As String = ConvertXmlToHTML(xml)

                Response.Write(html)
            Catch ex As Exception
                Response.Write("Error: " & Server.HtmlEncode(ex.Message))
            End Try
            Return View("ServiceSearchAid")
        End Function
        Private Function GetSearchServiceURL() As String
            Dim urlbase As String = "http://search.grainger.uiuc.edu/searchaid2/saresultsug.asp?version=1.1"
            Dim query As String = "&query=" & Server.UrlEncode(Request("q"))
            Dim db As String = "&db=" & Server.UrlEncode(Request("db"))
            Return urlbase & query & db
        End Function
        Private Function ConvertXmlToHTML(ByVal xml As XmlDocument) As String
            Dim xmlns As New XmlNamespaceManager(xml.NameTable)
            xmlns.AddNamespace("zs", "http://www.loc.gov/zing/srw/")

            Dim lbl As String = xml.SelectSingleNode("//zs:resultSetId/@label", xmlns).InnerText
            lbl = lbl.Replace("Google Book", "Google Books")
            Dim url As String = xml.SelectSingleNode("//zs:resultSetId", xmlns).InnerText
            Dim cnt As String = xml.SelectSingleNode("//zs:numberOfRecords", xmlns).InnerText

            Return String.Format("<span><br /><a href=""{0}"" target=""_blank"">{1}</a><img alt=""External Link"" src=""../../Images/externallinks.gif"" /><br />({2} results)</span>", url, lbl, cnt)
        End Function
    End Class
End Namespace

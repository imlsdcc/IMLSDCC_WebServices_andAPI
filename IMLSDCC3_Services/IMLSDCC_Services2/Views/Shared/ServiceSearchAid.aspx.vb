Imports System.Net
Imports System.IO
Imports System.Xml

Partial Public Class ServiceSearchAid
    Inherits System.Web.UI.Page

    Private validTargets() As String = {"hathi", "googbk", "amer", "jstor", "acse", "scopus"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not validTargets.Contains(Request("db")) Then
            Response.Write("Error: Unsupported Database: " & Server.HtmlEncode(Request("db")))
            Exit Sub
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
    End Sub

    ''' <summary>
    ''' Return the Metasearch URL from the given q and db parameters
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSearchServiceURL() As String
        Dim urlbase As String = "http://search.grainger.uiuc.edu/searchaid2/saresultsug.asp?version=1.1"
        Dim query As String = "&query=" & Server.UrlEncode(Request("q"))
        Dim db As String = "&db=" & Server.UrlEncode(Request("db"))
        Return urlbase & query & db
    End Function

    ''' <summary>
    ''' Convert the XML returned by the Metasearch service into a small HTML fragment
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertXmlToHTML(ByVal xml As XmlDocument) As String
        Dim xmlns As New XmlNamespaceManager(xml.NameTable)
        xmlns.AddNamespace("zs", "http://www.loc.gov/zing/srw/")

        Dim lbl As String = xml.SelectSingleNode("//zs:resultSetId/@label", xmlns).InnerText
        lbl = lbl.Replace("Google Book", "Google Books")
        Dim url As String = xml.SelectSingleNode("//zs:resultSetId", xmlns).InnerText
        Dim cnt As String = xml.SelectSingleNode("//zs:numberOfRecords", xmlns).InnerText

        Return String.Format("<span><br /><a href=""{0}"" target=""_blank"">{1}</a><img alt=""External Link"" src=""Images/externallinks.gif"" /><br />({2} results)</span>", url, lbl, cnt)
    End Function


End Class

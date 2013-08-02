Imports System.Xml
Imports System.IO
Namespace IMLSDCC_Services2
    Public Class UNAPIController
        Inherits System.Web.Mvc.Controller



        '
        ' GET: /UNAPI/Create

        Function Index() As ActionResult
            Dim id = HttpUtility.UrlDecode(Request.QueryString("id"))
            Dim format = Request.QueryString("format")
            Dim validFormats() As String = {"mods"}
            Dim rdf
            If id Is Nothing Then
                rdf = "<?xml version='1.0' encoding='UTF-8'?>"
                rdf += "<formats><format name='mods' type='application/xml'/></formats>"
                Return Content(rdf, "text/xml")
            Else
                Dim db As New IMLSDCCDataContext
                Dim fp = (From r In db.Records Where r.identifier = id Select r.filePath).Take(1)
                If fp.Count > 0 Then
                    If Not format Is Nothing Then
                        If validFormats.Contains(format) Then
                            Dim f = fp.Single.ToString.Replace("D:", "\\libgramaterasu")
                            Dim xDoc As New XmlDocument, tw As New StringBuilder, writ As XmlWriter = XmlWriter.Create(tw), xsl As New Xsl.XslCompiledTransform()
                            xsl.Load(System.AppDomain.CurrentDomain.BaseDirectory & "Styles/XSLTs/dcToMods.xsl")
                            'xsl.Load("\dc_to_mods.xsl")
                            xDoc.Load(f)

                            'Dim node As XmlNode = xDoc.DocumentElement.ChildNodes(1).FirstChild() '.SelectSingleNode("/record/metadata/dc")
                            Dim nm As New XmlNamespaceManager(xDoc.NameTable)
                            nm.AddNamespace("oai", "http://www.openarchives.org/OAI/2.0/")
                            nm.AddNamespace("dc", "http://www.openarchives.org/OAI/2.0/oai_dc/")
                            Dim node As XmlNode = xDoc.SelectSingleNode("//oai:record/oai:metadata/dc:dc", nm)

                            If Not node Is Nothing Then
                                xsl.Transform(New XmlNodeReader(node), writ)
                                rdf = tw.ToString()
                                'Response.StatusCode = 302
                                Return Content(rdf, "text/xml")
                            Else
                                Response.Status = "404 Not Found"
                                Response.StatusCode = 404
                            End If
                        Else
                            'Return 406 becase the format for the id is not valid
                            Response.Status = "406 Not Acceptable"
                            Response.StatusCode = 406
                        End If
                    Else
                        rdf = "<?xml version='1.0' encoding='UTF-8'?>"
                        rdf += "<formats id='" & id & "'><format name='mods' type='application/xml'/></formats>"
                        Return Content(rdf, "text/xml")
                    End If
                Else
                    'Return 404 because the id was not found
                    Response.Status = "404 Not Found"
                    Response.StatusCode = 404
                End If
                
            End If



        End Function

        '
        ' POST: /UNAPI/Create

        <HttpPost> _
        Function Create(ByVal collection As FormCollection) As ActionResult
            Try
                ' TODO: Add insert logic here
                Return RedirectToAction("Index")
            Catch
                Return View()
            End Try
        End Function

    End Class
End Namespace

<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Xml.Xsl" %>

<% 
    Dim myXML As New XmlDocument
    Dim myXslt As New XslCompiledTransform
    Dim xsltArgList As New System.Xml.Xsl.XsltArgumentList()
    Dim FCItem As New Integer
    
    FCItem = ViewData("FCItem")
        
    myXML.Load("http://sowingculture.wordpress.com/feed/")
    myXslt.Load(Server.MapPath("/Styles/XSLTs/FeaturedContent.xsl"))
    xsltArgList.AddParam("whichOne", "", FCItem)
    
    myXslt.Transform(myXML, xsltArgList, Response.Output)
    
    %>

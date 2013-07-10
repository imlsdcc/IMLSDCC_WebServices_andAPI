<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of IMLSDCC_Services2.ShortRecord)" %>

<div id="details-container">
<%If ViewData("errorscount") > 0 Then%>
    <% For Each j In ViewData("errors")%>
        <li><%=j%></li>
  <% Next%>
<%Else%>
                        <h1 id="new-item-title"><%=ViewData("title")%></h1>
                        <p id="original-url"><a href="<%=ViewData("identifier")%>">
        See Original
        </a></p>
                        <table cellspacing="0" border="0" id="ItemFormView" style="border-collapse:collapse;">
	<tbody><tr>
		<td colspan="2">
                                <div class="right">
                                    <a id="ItemFormView_HyperLink4" href="http://contentdm.lib.byu.edu/u?/RelEd,6579">
                                        <img alt="Thumbnail" src="http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=oai:contentdm.lib.byu.edu:RelEd/6579&amp;format=thumbnail" class="details-image">
                                    </a>                                
                                </div>
                                <span id="item-title" style="display: none; ">A "Do-It"</span>
<p id="old-original-url" style="display: none; "><a href="http://contentdm.lib.byu.edu/u?/RelEd,6579">
        See Original
        </a><img id="ExternalLink" name="ExternalLink" alt="External Link" src="Images/externallinks.gif"></p>
        <%If ViewData("creator") <> "" Then%>
        <h3>Contributor</h3>
        <p><%=ViewData("creator")%></p>
        <% End If%>

        <%If ViewData("description") <> "" Then%>
        <h3>Description</h3>
        <p><%=ViewData("description")%><p>
        <% End If%>

        <%If ViewData("publisher") <> "" Then%>
        <h3>Publisher</h3>
        <p><%=ViewData("publisher")%></p>
        <% End If%>

        <%If ViewData("subject").count > 0 Then%>
        <h3>Subject</h3>
        <ul>
        <% For Each j In ViewData("subject") %>
        <li><%=j%></li>
        <% Next%>
        </ul>
        <% End If%>

        <%If ViewData("relation") <> "" Then%>
        <h3>Relation</h3>
        <p><%=ViewData("relation")%></p>
        <% End If%>

        <%If ViewData("source") <> "" Then%>
        <h3>Source</h3>
        <p><%=ViewData("source")%></p>
        <%End If%>

        <%If ViewData("rights") <> "" Then%>
        <h3>Rights</h3>
        <ul><li><%=ViewData("rights")%></li>
        </ul>
        <%End If%>

        <%If ViewData("type").count > 0 Then%>
        <h3>Type</h3>
        <% For Each k In ViewData("type")%>
        <li><%=k%></li>
        <% Next%>
        <%End If%>

        <%If ViewData("format") <> "" Then%>
        <h3>Format</h3>
        <p><%=ViewData("format")%></p>
        <%End If%>

       </td>
	</tr>
</tbody></table>
<%End If%>
</div>


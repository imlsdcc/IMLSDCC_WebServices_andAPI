<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of IMLSDCC_Services2.ShortRecord)" %>

<div id="details-container">
<%If ViewData("errorscount") > 0 Then%>
    <% For Each j In ViewData("errors")%>
        <li><%=j%></li>
  <% Next%>
<%Else%>
<table cellspacing="0" border="0" id="CollectionFormView" style="border-collapse:collapse;">
	<tbody><tr>
		<td colspan="2">
          <h1><%=ViewData("title")%></h1>
<p id="original-url"><a href="<%=ViewData("isavailableat")%>">
      See Original Collection
      </a></p>
<h3>Collection Description</h3>
<p><%=ViewData("description")%></p>
<% If ViewData("subject").count > 0 Then%>
<h3>Subjects</h3>
<ul>
  <% For Each j In ViewData("subject") %>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("GEMsubjectlist").count > 0 Then%> 
<h3>GEM Subjects</h3>
   
  <% For Each k In ViewData("GEMsubjectlist")%>
        <h3><%=k.gemtop%></h3>
        <ul>
        <% For Each l In k.gemlist%>
            <li><%=l%></li>
        <% Next%>
        </ul>
  <% Next%>
<% End If%>

<% If ViewData("geographic").count > 0 Then%> 
<h3>Geograpic</h3>
<ul>
  <% For Each j In ViewData("geographic") %>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("objectsrepresented").count > 0 Then%> 
<h3>Objects Represented</h3>
<ul>
  <% For Each j In ViewData("objectsrepresented")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("format").count > 0 Then%> 
<h3>Format</h3>
<ul>
  <% For Each j In ViewData("format")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("audience").count > 0 Then%> 
<h3>Audience</h3>
<ul>
  <% For Each j In ViewData("audience")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("interactivity").count > 0 Then%> 
<h3>Interaction with Collection</h3>
<ul>
  <% For Each j In ViewData("interactivity")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("copyright").count > 0 Then%> 
<h3>Copyright &amp; IP Rights</h3>
<ul>
  <% For Each j In ViewData("copyright")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("size").count > 0 Then%> 
<h3>Size</h3>
<ul>
  <% For Each j In ViewData("size")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("frequency").count > 0 Then%> 
<h3>Frenquency of Additions</h3>
<ul>
  <% For Each j In ViewData("frequency")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("supplementary").count > 0 Then%> 
<h3>Supplementary Materials</h3>
<ul>
  <% For Each j In ViewData("supplementary")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("hosting").count > 0 Then%>
<h3>Hosting Institution</h3>
<ul>
  <% For Each j In ViewData("hosting")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>

<% If ViewData("contributing").count > 0 Then%>
<h3>Contributing Institution</h3>
<ul>
  <% For Each j In ViewData("contributing")%>
        <li><%=j%></li>
  <% Next%>
</ul>
<% End If%>
                            </td>
	</tr>
</tbody></table>                 
   <% End If%>                 </div>


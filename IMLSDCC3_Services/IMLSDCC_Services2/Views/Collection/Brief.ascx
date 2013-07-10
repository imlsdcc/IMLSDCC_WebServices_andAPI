<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of IMLSDCC_Services2.ShortRecord)" %>

<li class="collection-match"><h3><a href="/Collection/detail/<%=ViewData("id")%>?view=html"> <%=ViewData("title")%></a></h3>
<p><%=ViewData("description")%></p>
<!--<div class="thumbnails">
<span class="thumbnail"><a href="Item.aspx?i=oai:lib.uchicago.edu:apf2-07755"><img src="http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=oai:lib.uchicago.edu:apf2-07755" alt="thumbnail" class="results thumbnail"></a></span><span class="thumbnail"><a href="Item.aspx?i=oai:lib.uchicago.edu:apf2-06852"><img src="http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=oai:lib.uchicago.edu:apf2-06852" alt="thumbnail" class="results thumbnail"></a></span>
<span class="thumbnail"><a href="Item.aspx?i=oai:lib.uchicago.edu:apf2-00745"><img src="http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=oai:lib.uchicago.edu:apf2-00745" alt="thumbnail" class="results thumbnail"></a></span>
<span class="thumbnail"><a href="Item.aspx?i=oai:lib.uchicago.edu:apf2-05438"><img src="http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=oai:lib.uchicago.edu:apf2-05438" alt="thumbnail" class="results thumbnail"></a></span>
</div>-->
<span><a href="<%=ViewData("isavailableat")%>"><%=ViewData("isavailableat")%></a></span></li>


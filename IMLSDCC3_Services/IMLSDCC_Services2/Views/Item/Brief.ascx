<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of IMLSDCC_Services2.ShortRecord)" %>


    <li class="item-match">
    <span class="collection-id-2424">
    <!--<div class="item-thumbnail">
    <a href="Item.aspx?i=oai%3Aoai.dlib.indiana.edu%3Aarchives%2Fcushman%2FP10265&amp;"><img alt="thumbnail" src="http://gita.grainger.uiuc.edu/thumbnails/thumbnail.aspx?identifier=oai:oai.dlib.indiana.edu:archives/cushman/P10265" class="results thumbnail"></a>
    </div>-->
    <div class="item-description">
    <span class="item-type"><%=ViewData("type")%></span>
    <h3><a title="View the full item record" href="/item/detail/<%=ViewData("id")%>?view=html"> <%=ViewData("title")%></a></h3>
    <p>Created by <a href="/Search/Items?agent=<%=ViewData("creator")%>&dates=&types=&places=&collections=&startRecord=1&maximumRecords=10&scope=&sort="> <%=ViewData("creator")%></a></p>
    <p>View original at <a target="_blank" href="<%=ViewData("identifier")%>"><%=ViewData("identifier")%></a></p>
    
    </div></span></li>
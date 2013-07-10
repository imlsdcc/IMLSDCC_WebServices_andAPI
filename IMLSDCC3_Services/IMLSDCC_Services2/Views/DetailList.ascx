<%@ Control Language="VB" Inherits="System.Web.Mvc.ViewUserControl(Of IMLSDCC_Services2.ShortRecord)" %>

<fieldset>
    <legend>ShortRecord</legend>

    <div class="display-label">title</div>
    <div class="display-field">
        <%: Html.DisplayFor(Function(model) model.title) %>
    </div>

    <div class="display-label">identifier</div>
    <div class="display-field">
        <%: Html.DisplayFor(Function(model) model.identifier) %>
    </div>

    <div class="display-label">creator</div>
    <div class="display-field">
        <%: Html.DisplayFor(Function(model) model.creator) %>
    </div>

    <div class="display-label">type</div>
    <div class="display-field">
        <%: Html.DisplayFor(Function(model) model.type) %>
    </div>

    <div class="display-label">isPartOf</div>
    <div class="display-field">
        <%: Html.DisplayFor(Function(model) model.isPartOf) %>
    </div>
</fieldset>
<p>
    <%--<%: Html.ActionLink("Edit", "Edit", New With {.id = Model.PrimaryKey}) %> |--%>
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

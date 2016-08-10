<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="Default.aspx.vb" Inherits="ITG_FORMS._Default1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="server">
    <%--    <div onclick="redirect('Reports.aspx?id=2')" class="portalLink"><i class="fa fa-users fa-3x" aria-hidden="true"></i><br><br>SAP ACCOUNT PAYABLE</div>
        <div onclick="redirect('')" class="portalLink"><i class="fa fa-users fa-3x" aria-hidden="true"></i><br><br>FINANCIAL</div>
        <div onclick="redirect('')" class="portalLink"><i class="fa fa-users fa-3x" aria-hidden="true"></i><br><br>END USER</div>
        <div onclick="redirect('')" class="portalLink"><i class="fa fa-users fa-3x" aria-hidden="true"></i><br><br>DATA CONTROL</div>
        <div onclick="redirect('')" class="portalLink"><i class="fa fa-users fa-3x" aria-hidden="true"></i><br><br>ADMINISTRATION</div>--%>
        <asp:Literal ID="litPortalListing" runat="server"></asp:Literal>
</asp:Content>

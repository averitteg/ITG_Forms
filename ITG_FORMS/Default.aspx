<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="Default.aspx.vb" Inherits="ITG_FORMS._Default1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="Scripts/font-awesome.min.css" />
    <style>
        .portalLink {
            float: left;
            padding: 10px;
            width: 250px;
            height: 120px;
            border: solid thin #909090;
            margin-left: 20px;
            margin-bottom: 50px;
            text-align: center;
            display: block;
            cursor: pointer;
            background-color: #F5F5F5;
            padding-top: 20px;
            font-weight: bold;
        }
        .portalLink:hover {
            
            background-color: white;
        }
        .container {
            margin-left: auto;
            margin-right: auto;
            margin-top: 15px;
            padding-top: 30px;
            color: #952526;
            background-color: #F5F5F5;
            border: solid thin #909090;
            min-height: 500px;
            width: 65%;
            min-width:330px;
        }
        .containerFoot {
            margin-left: auto;
            margin-right: auto;
            background-color: #F5F5F5;
            border: solid thin #909090;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="server">
    <p style="color:#808080; font-size:x-large; margin-top: 70px; padding-left: 10px; margin-left: auto; margin-right:auto; width: 65%; ">DASHBOARD</p>
    <div class="container">
        <asp:Literal ID="litPortalListing" runat="server"></asp:Literal>
    </div>
    

</asp:Content>

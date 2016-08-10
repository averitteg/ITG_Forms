<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="frmBasic.aspx.vb" Inherits="ITG_FORMS.frmBasic" validateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            color: black;
            background-color: #F5F5F5;
            border: solid thin #909090;
            min-height: 500px;
            width: 90%;
            min-width:330px;
        }
        .containerFoot {
            margin-left: auto;
            margin-right: auto;
            background-color: #F5F5F5;
            border: solid thin #909090;
        }
        .btnSubmit {
            display: block;
            height: 40px;
            width: 80px;
            margin-left: 20px;
            margin-right: 20px;
            border: solid thin #909090;
            background-color: #F5F5F5;
            margin-bottom: 20px;
        }
        .btnSubmit:hover {
            background-color: #ffe0e0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
        <asp:Literal ID="litHeader" runat="server"></asp:Literal>
        <hr />

        <asp:Panel ID="pnlFormHeader" runat="server">
            <table style="width: 70%;">
                <tr><td style="width: 80px;"><span style="font-weight: bold;">To:</span></td><td><asp:TextBox ID="txtTo" runat="server" Width="700px" TextMode="MultiLine" /> </td></tr>
                <tr><td><span style="font-weight: bold;"> From:</span></td><td><asp:Literal ID="litFrom" runat="server"></asp:Literal> </td></tr>
                <tr><td><span style="font-weight: bold;"> CC:</span></td><td><asp:TextBox ID="txtCC" runat="server" Width="700px" TextMode="MultiLine" /> </td></tr>
                <tr><td><span style="font-weight: bold;"> BCC:</span></td><td> <asp:TextBox ID="txtBCC" runat="server" Width="700px" TextMode="MultiLine" /><asp:ImageButton ID="imgAddBCC" Height="16" Width="16" ImageUrl="~/images/round_add_red.PNG" runat="server" /> </td></tr>
                <tr><td><span style="font-weight: bold;"> Subject:</span></td><td><asp:Literal ID="litSubject" runat="server"></asp:Literal> </td></tr>
            </table>
            <br />
            <asp:Literal ID="litMessageBody" runat="server"></asp:Literal>
            <br />
            <br />
            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="50px" Width="400px"></asp:TextBox>
            <br />
            <br />
            <table>
                <tr><td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnSubmit" /></td></tr>
            </table>
        </asp:Panel>
        
        <asp:Panel ID="pnlAddBCC" runat="server">
            Current BCC List: <asp:Literal ID="litBCCAdd" runat="server"></asp:Literal>
            <br /><br />
            <table>
                <tr><td>Add User</td><td></td></tr>
            </table>
            <asp:DropDownList ID="ddlUserListing" runat="server"></asp:DropDownList>
            <br /><br />
            <asp:ListBox ID="lstUserListing" runat="server" Height="200px" SelectionMode="Multiple"></asp:ListBox>
            <br /><br />
            <asp:Button ID="btnAddBCC" runat="server" Text="Add BCC" />
        </asp:Panel>
</asp:Content>

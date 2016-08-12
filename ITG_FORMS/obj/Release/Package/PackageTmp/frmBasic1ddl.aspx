<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="frmBasic1ddl.aspx.vb" Inherits="ITG_FORMS.frmBasic1ddl" %>

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
            <table style="width: 100%;">
                <tr><td style="width: 80px;"><span style="font-weight: bold;">To:</span></td><td><asp:Literal ID="litTO" runat="server"></asp:Literal></td></tr>
                <tr><td><span style="font-weight: bold;"> From:</span></td><td><asp:Literal ID="litFrom" runat="server"></asp:Literal> </td></tr>
                <tr><td style="width: 80px;"><span style="font-weight: bold;">CC:</span></td><td><asp:Literal ID="litCC" runat="server"></asp:Literal></td></tr>
                <tr><td><span style="font-weight: bold;"> Add CC:</span></td><td><asp:TextBox ID="txtCC" runat="server" Width="90%" TextMode="MultiLine" /><asp:ImageButton ID="imgAddCC" Height="24" Width="24" ImageUrl="~/images/round_add_red.PNG" runat="server" /> </td></tr>
                <tr><td><span style="font-weight: bold;"> Add BCC:</span></td><td> <asp:TextBox ID="txtBCC" runat="server" Width="90%" TextMode="MultiLine" /><asp:ImageButton ID="imgAddBCC" Height="24" Width="24" ImageUrl="~/images/round_add_red.PNG" runat="server" /> </td></tr>
                <tr><td><span style="font-weight: bold;"> Subject:</span></td><td><asp:Literal ID="litSubject" runat="server"></asp:Literal> </td></tr>
            </table>
            <br />
            <asp:Literal ID="litMessageBody" runat="server"></asp:Literal>
            <br />
            <br />
            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height="50px" Width="80%"></asp:TextBox>
            <br />
            <br />
            <asp:Literal ID="litDDLLabel" runat="server"></asp:Literal><asp:DropDownList ID="ddlOptions" runat="server"></asp:DropDownList>
            <br />
            <br />
            <table>
                <tr><td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnSubmit" />
                    <div style="display: block;height: 1px; width: 1px; border:none; overflow: hidden;">
                        <div>
                            <asp:TextBox ID="txtCCValue" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtBCCvalue" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    </td></tr>
            </table>
        </asp:Panel>
        
        <asp:Panel ID="pnlAddBCC" runat="server">
            Add Distribution List<br />
            <span style="font-style: italic; color: #530000; font-size: .8em;">*hold CTRL to select multiple entries...</span><br />
            <asp:ListBox ID="lstGroupListing" runat="server" Height="200px" SelectionMode="Multiple"></asp:ListBox>
            <br /><br />
            Add Specific Users<br />
            <span style="font-style: italic; color: #530000; font-size: .8em;">*hold CTRL to select multiple entries...</span><br />
            <asp:ListBox ID="lstUserListing" runat="server" Height="200px" SelectionMode="Multiple" ValidateRequestMode="Disabled"></asp:ListBox>
            <br /><br />
            <asp:Button ID="btnAddBCC" runat="server" Text="Add BCC" />
        </asp:Panel>
</asp:Content>

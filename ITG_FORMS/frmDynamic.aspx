<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="frmDynamic.aspx.vb" Inherits="ITG_FORMS.frmDynamic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style>
        .pnl {
            padding: 20px;
            width: 90%;
            margin-bottom: 20px;
        }
        .BOLD {
            font-weight: bold;
        }
        .UNDERLINE {
            text-decoration: underline;
        }
        .SECTIONHEAD {
            font-size: 1.2em;
        }
        .btnSubmit {
            display: inline-block;
            height: 40px;
            margin-left: 20px;
            margin-right: 20px;
            border: solid thin #909090;
            background-color: #F5F5F5;
            margin-bottom: 20px;
            cursor: pointer;
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
            <asp:Panel ID="pnlFormDetails" runat="server" CssClass="pnl"></asp:Panel>
            <br />
            <br />
            <table>
                <tr><td>
                    <asp:Button ID="btnSubmit" CssClass="btnSubmit" runat="server" Text="Submit Request" />
                    <asp:Button ID="btnCancel" CssClass="btnSubmit" runat="server" Text="Cancel" />
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


        
        

        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <script>
        $( document ).ready(function() {
            $( ".DATE" ).datepicker();
        });
    </script>
</asp:Content>

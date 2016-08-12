<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Default.Master" CodeBehind="NewBasicForm.aspx.vb" Inherits="ITG_FORMS.NewBasicForm" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %> 


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
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

    <asp:Panel ID="pnlForm" runat="server">
        <table style="width: 70%;">
            <tr><td style="width: 80px;"><span style="font-weight: bold;">Category:</span></td><td><asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList> </td></tr>
            <tr><td style="width: 80px;"><span style="font-weight: bold;">FORM NAME:</span></td><td><asp:TextBox ID="txtFormName" runat="server" Width="90%" TextMode="SingleLine" /> </td></tr>        
            <tr><td style="width: 80px;"><span style="font-weight: bold;">To:</span></td><td><asp:TextBox ID="txtTo" runat="server" Width="90%" TextMode="MultiLine" /><asp:ImageButton ID="imgAddTo" Height="24" Width="24" ImageUrl="~/images/round_add_red.PNG" runat="server" /> </td></tr>
            <tr><td><span style="font-weight: bold;"> CC:</span></td><td><asp:TextBox ID="txtCC" runat="server" Width="90%" TextMode="MultiLine" /><asp:ImageButton ID="imgAddCC" Height="24" Width="24" ImageUrl="~/images/round_add_red.PNG" runat="server" /> </td></tr>
            <tr><td><span style="font-weight: bold;"> Subject:</span></td><td><asp:TextBox ID="txtSubject" runat="server" Width="90%" TextMode="SingleLine" /></td></tr>
            <tr><td colspan="2"><span style="font-weight: bold;"> Message Body:</span></td></tr>
            <tr><td colspan="2"><CKEditor:CKEditorControl ID="ckeMessageContent" BasePath="/ckeditor/" runat="server" ToolbarFull="Cut|Copy|Paste|PasteText|-|SpellChecker Undo|Redo|-|Find|Replace|-|SelectAll|RemoveFormat /
                Bold|Italic|Underline|Strike|-|Subscript|Superscript NumberedList|BulletedList|-|Outdent|Indent|Blockquote
                JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock Link|Unlink HorizontalRule|Smiley|SpecialChar /
                Styles|Format|Font|FontSize|ShowBlocks|TextColor|BGColor|-|Table|-|RemoveFormat|Image" Columns="75" EnterMode="BR" Rows="25">
                </CKEditor:CKEditorControl></td></tr>
            <tr><td><span style="font-weight: bold;">Option Type:</span></td><td>
                <asp:DropDownList runat="server" ID="ddlOptionType">
                    <asp:ListItem Text="Drop Down List (Single select)" Value="DDL" />
                    <asp:ListItem Text="Check Box List (Multi Select)" Value="CBL" />
                </asp:DropDownList></td></tr>
            <tr><td><span style="font-weight: bold;">Options Label:</span></td><td><asp:TextBox Width="90%" ID="txtDropDownLabel" runat="server"></asp:TextBox></td></tr>
            <tr><td><span style="font-weight: bold;">Selectable Options (comma separated):</span></td><td><asp:TextBox Width="90%" ID="txtDropDownOptions" runat="server"></asp:TextBox></td></tr>
            <tr><td><asp:Button ID="btnSave" CssClass="btnSubmit" runat="server" Text="Save" /></td><td><asp:Button ID="btnClear" CssClass="btnSubmit" runat="server" Text="Clear" /></td></tr>
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

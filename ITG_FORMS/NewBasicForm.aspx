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
    <table style="width: 70%;">
                <tr><td style="width: 80px;"><span style="font-weight: bold;">FORM NAME:</span></td><td><asp:TextBox ID="txtFormName" runat="server" Width="700px" TextMode="SingleLine" /> </td></tr>        
                <tr><td style="width: 80px;"><span style="font-weight: bold;">To:</span></td><td><asp:TextBox ID="txtTo" runat="server" Width="700px" TextMode="MultiLine" /> </td></tr>
                <tr><td><span style="font-weight: bold;"> From:</span></td><td><asp:Literal ID="litFrom" runat="server"></asp:Literal> </td></tr>
                <tr><td><span style="font-weight: bold;"> CC:</span></td><td><asp:TextBox ID="txtCC" runat="server" Width="700px" TextMode="MultiLine" /> </td></tr>
                <tr><td><span style="font-weight: bold;"> Subject:</span></td><td><asp:TextBox ID="txtSubject" runat="server" Width="700px" TextMode="SingleLine" /></td></tr>
                <tr><td colspan="2"><span style="font-weight: bold;"> Message Body:</span></td></tr>
                <tr><td colspan="2"><CKEditor:CKEditorControl ID="ckeMessageContent" BasePath="/ckeditor/" runat="server" ToolbarFull="Cut|Copy|Paste|PasteText|-|SpellChecker Undo|Redo|-|Find|Replace|-|SelectAll|RemoveFormat /
                    Bold|Italic|Underline|Strike|-|Subscript|Superscript NumberedList|BulletedList|-|Outdent|Indent|Blockquote
                    JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock Link|Unlink HorizontalRule|Smiley|SpecialChar /
                    Styles|Format|Font|FontSize|ShowBlocks|TextColor|BGColor|-|Table|-|RemoveFormat|Image" Columns="75" EnterMode="BR" Rows="25">
                   </CKEditor:CKEditorControl></td></tr>
                <tr><td><asp:Button ID="btnSave" CssClass="btnSubmit" runat="server" Text="Save" /></td><td><asp:Button ID="btnClear" CssClass="btnSubmit" runat="server" Text="Clear" /></td></tr>
            </table>
</asp:Content>

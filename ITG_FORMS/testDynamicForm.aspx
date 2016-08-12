<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testDynamicForm.aspx.vb" Inherits="ITG_FORMS.testDynamicForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
            display: block;
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="pnlFormDetails" runat="server" CssClass="pnl"></asp:Panel>
        <asp:Button ID="btnSubmit" CssClass="btnSubmit" runat="server" Text="Submit Request" />

        <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
    </div>
    </form>
</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testDatePicker.aspx.vb" Inherits="ITG_FORMS.testDatePicker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/jQuery-2.2.4.js"></script>
    <script src="Scripts/jquery-ui-1.8.24.min.js"></script>
    <link href="Scripts/jquery-ui.theme.min.css" rel="stylesheet" />
    <link href="Scripts/jquery-ui.structure.min.css" rel="stylesheet" />
    <title></title>
    <script>jQuery.browser = {};
        (function () {
            jQuery.browser.msie = false;
            jQuery.browser.version = 0;
            if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
                jQuery.browser.msie = true;
                jQuery.browser.version = RegExp.$1;
            }
        })();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" CssClass="DATE" runat="server"></asp:TextBox>
    </div>
    </form>

    <script>
            $(document).ready(function () {
                $(".DATE").datepicker({
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    dateFormat: 'dd/mm/yy'
                });
            });
      </script>
</body>
</html>

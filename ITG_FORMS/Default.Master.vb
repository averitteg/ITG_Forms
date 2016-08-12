Public Class _Default
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            litMenu.Text = "<a href=""Default.aspx"">Main</a>"
            If validateAdmin() Then
                litMenu.Text &= "&nbsp;&nbsp;&nbsp;&nbsp;<a href=""NewBasicForm.aspx"">New Basic Form</a>"
            End If
        End If

    End Sub

End Class
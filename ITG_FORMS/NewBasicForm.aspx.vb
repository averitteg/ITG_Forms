Public Class NewBasicForm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub clearForm()
        txtFormName.Text = ""
        txtSubject.Text = ""
        txtCC.Text = ""
        txtTo.Text = ""
        ckeMessageContent.Text = ""
    End Sub

    Private Sub saveForm()

    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        clearForm()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim formName As String = txtFormName.Text.Replace("'", "''")
        Dim strArticle As String = ckeMessageContent.Text.Replace("'", "''")
        Dim to_Value As String = txtTo.Text.Replace("'", "''")
        Dim CC_Value As String = txtCC.Text.Replace("'", "''")
        Dim Subject As String = txtSubject.Text.Replace("'", "''")

        Dim strSQL As String = "Insert into mc_forms (form_name, mc_category, url) VALUES (" &
                        "'" & formName & "',2,'')"
        Dim intFormID As Integer = g_IO_Execute_SQL(strSQL, False).Rows(0)(0)

        strSQL = "update dbo.mc_forms set url = 'frmBasic.aspx?form=bsc&id=" & intFormID & "' where mc_forms_id = " & intFormID
        g_IO_Execute_SQL(strSQL, False)

        strSQL = "Insert into dbo.form_details (mc_forms_id, to_value, cc_value, subject, message_body) values (" &
                intFormID & ",'" & to_Value & "','" & CC_Value & "','" & Subject & "','" & strArticle & "')"
        g_IO_Execute_SQL(strSQL, False)
        clearForm()

    End Sub
End Class
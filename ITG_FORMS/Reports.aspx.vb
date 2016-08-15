Public Class Reports
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If IsNothing(Request.QueryString("ID")) Then
                Response.Redirect("Default.aspx")
            End If
            Dim strSQL As String = "Select * from dbo.VW_FORMS_LIST where MC_CATEGORY_ID = " & Request.QueryString("ID") & " order by form_name"
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
            If tblResults.Rows.Count > 0 Then
                litReportListing.Text = ""
                For Each row In tblResults.Rows
                    litReportListing.Text &= "<div onclick=""redirect('" & row("URL") & "')"" class=""reportLink""><table><tr><td><i class=""fa fa-file-text-o fa-3x"" aria-hidden=""true""></i></td><td>" & row("FORM_NAME") & "</td></tr></table></div>"
                Next
            Else
                litReportListing.Text = "<h3> There are no reports for this category at this time.</h3>"
            End If
        End If
    End Sub

End Class
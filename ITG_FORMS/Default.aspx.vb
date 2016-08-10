Public Class _Default1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            validateUser()
            Dim strSQL As String = "Select * from dbo.mc_category order by category_name"
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
            If tblResults.Rows.Count > 0 Then
                litPortalListing.Text = ""
                For Each row In tblResults.Rows
                    litPortalListing.Text &= "<div onclick=""redirect('Reports.aspx?id=" & row("MC_CATEGORY_ID") & "')"" class=""portalLink""><i class=""fa fa-users fa-3x"" aria-hidden=""true""></i><br><br>" & row("CATEGORY_NAME") & "</div>"
                Next
            Else
                litPortalListing.Text = "<h3> There are no categories in the database at this time.</h3>"
            End If
        End If

    End Sub

End Class
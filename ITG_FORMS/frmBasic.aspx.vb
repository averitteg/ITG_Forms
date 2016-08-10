Public Class frmBasic
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If IsNothing(Request.QueryString("form")) Then
                Response.Redirect("Default.aspx")
            Else
                hidePanels()
                ''Load the form
                Dim strForm As String = Request.QueryString("FORM").ToString.ToUpper.Trim
                If strForm = "BSC" Then
                    load_basic_form()
                    pnlFormHeader.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub hidePanels()
        pnlAddBCC.Visible = False
        pnlFormHeader.Visible = False
    End Sub

    Private Function getBasicFormDataFromDatabase()
        Dim tblResults As DataTable = Nothing
        Dim strSQL As String = "Select * from dbo.vw_frm_details where mc_forms_id = " & Request.QueryString("ID")
        tblResults = g_IO_Execute_SQL(strSQL, False)

        Return tblResults
    End Function

    Private Sub loadBCCDropDown()
        Dim strSQL As String = "Select name, email from syn_users where email is not null and enabled = 1 and ltrim(rtrim(email)) not in('','n/a','n\a') order by name"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)



        If tblResults.Rows.Count > 0 Then
            litFrom.Text = Session("USER_NAME")
            'ddlUserListing.DataTextField = "name"
            'ddlUserListing.DataValueField = "email"
            'ddlUserListing.DataSource = tblResults
            'ddlUserListing.DataBind()
            'Dim tblFormDataBCC As DataTable = getFormDataFromDatabase().rows(0)("BCC_VALUE")

            For Each row In tblResults.Rows
                Dim strItem As String = row("Name") & " <" & row("EMAIL") & ">"
                lstUserListing.Items.Add(strItem)
            Next



        Else
            ''Do nothing
        End If

    End Sub

    Private Sub logUserSubmission()

    End Sub

    Private Sub load_basic_form()
        Dim tblResults As DataTable = getBasicFormDataFromDatabase()
        If tblResults.Rows.Count > 0 Then
            ''" & tblResults.Rows(0)("HEADER_DISPLAY") & "
            litHeader.Text = "<h3 style=""text-decoration: underline;"">" & IIf(IsDBNull(tblResults.Rows(0)("HEADER")), "", tblResults.Rows(0)("HEADER")) & "</h3>"
            txtTo.Text = IIf(IsDBNull(tblResults.Rows(0)("TO_VALUE")), "", tblResults.Rows(0)("TO_VALUE"))
            litFrom.Text = Session("USER_NAME") & "<" & Session("USER_EMAIL") & ">"
            txtCC.Text = IIf(IsDBNull(tblResults.Rows(0)("CC_VALUE")), "", tblResults.Rows(0)("CC_VALUE"))
            litSubject.Text = IIf(IsDBNull(tblResults.Rows(0)("SUBJECT")), "", tblResults.Rows(0)("SUBJECT"))
            litMessageBody.Text = Server.HtmlDecode(tblResults.Rows(0)("MESSAGE_BODY"))
        End If

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Dim tblResults As DataTable = getBasicFormDataFromDatabase()
        Dim toEmail As String = tblResults.Rows(0)("TO_VALUE")
        Dim FromEmail As String = Session("USER_EMAIL")
        Dim CCEmail As String = Session("USER_EMAIL")
        Dim BCCEmail As String = txtBCC.Text
        Dim Subject As String = tblResults.Rows(0)("SUBJECT")
        Dim Message As String = tblResults.Rows(0)("MESSAGE_BODY") & "<br /><br />Comments: " & txtComments.Text.ToString.Trim

        g_SendEmail(toEmail, Subject, Message, CCEmail, BCCEmail)

        Dim strSQL As String = "Exec udp_incrementReportCounter @id = " & Request.QueryString("ID")
        g_IO_Execute_SQL(strSQL, False)

        logUserSubmission()

    End Sub

    Protected Sub imgAddBCC_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddBCC.Click
        hidePanels()
        loadBCCDropDown()

        pnlAddBCC.Visible = True
    End Sub

    Protected Sub btnAddBCC_Click(sender As Object, e As EventArgs) Handles btnAddBCC.Click
        Dim delimiter As String = ""
        For Each item In lstUserListing.Items
            If item.selected Then
                Dim i As Integer = item.ToString.IndexOf("<")
                Dim emailAddress As String = item.ToString.Substring(i + 1, item.ToString.IndexOf(">", i + 1) - i - 1)
                txtBCC.Text &= delimiter & emailAddress
                delimiter = ";"
            End If
        Next
        hidePanels()
        pnlFormHeader.Visible = True
    End Sub

End Class
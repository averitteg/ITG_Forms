Public Class SAPFICorpTaxQuaterly
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If IsNothing(Request.QueryString("form")) Then
                Response.Redirect("Default.aspx")
            Else
                hidePanels()
                ''Load the form
                load_basic_form()
                pnlFormHeader.Visible = True
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
        Dim strSQL As String = ""
        If lstGroupListing.Items.Count < 1 Then
            ''SELECT * FROM DBO.DISTRIBUTION_LIST
            strSQL = "SELECT DISTRIBUTION_LIST_NAME, EMAIL_ADDRESS FROM DBO.DISTRIBUTION_LIST ORDER BY DISTRIBUTION_LIST_NAME"
            Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

            If tblResults.Rows.Count > 0 Then
                For Each row In tblResults.Rows
                    Dim strItem As String = row("DISTRIBUTION_LIST_NAME") & " <" & row("EMAIL_ADDRESS") & ">"
                    lstGroupListing.Items.Add(strItem)
                Next

                Try
                    lstGroupListing.SelectedIndex = -1
                Catch ex As Exception

                End Try
            Else
                ''Do nothing
            End If
            lstGroupListing.SelectedIndex = -1
        Else
            lstGroupListing.SelectedIndex = -1
        End If


        If lstUserListing.Items.Count < 1 Then
            strSQL = "Select name, email from syn_users where email is not null and enabled = 1 and ltrim(rtrim(email)) not in('','n/a','n\a') order by name"
            Dim tblResults_users As DataTable = g_IO_Execute_SQL(strSQL, False)

            If tblResults_users.Rows.Count > 0 Then
                For Each row In tblResults_users.Rows
                    Dim strItem As String = row("Name") & " <" & row("EMAIL") & ">"
                    lstUserListing.Items.Add(strItem)
                Next

                Try
                    lstUserListing.SelectedIndex = -1
                Catch ex As Exception

                End Try
            Else
                ''Do nothing
            End If
            lstUserListing.SelectedIndex = -1
        Else
            lstUserListing.SelectedIndex = -1
        End If



    End Sub

    Private Sub logUserSubmission()

    End Sub

    Private Sub load_basic_form()
        Dim tblResults As DataTable = getBasicFormDataFromDatabase()
        If tblResults.Rows.Count > 0 Then
            ''" & tblResults.Rows(0)("HEADER_DISPLAY") & "
            litHeader.Text = "<h3 style=""text-decoration: underline;"">" & IIf(IsDBNull(tblResults.Rows(0)("HEADER")), "", tblResults.Rows(0)("HEADER")) & "</h3>"
            litTO.Text = "<pre>" & tblResults.Rows(0)("TO_VALUE").ToString.Replace("<", "&lt;").Replace(">", "&gt;") & "</pre>"
            litFrom.Text = "<pre>" & Session("USER_NAME") & "&nbsp;&lt;" & Session("USER_EMAIL") & "&gt;" & "</pre>"
            litCC.Text = "<pre>" & IIf(IsDBNull(tblResults.Rows(0)("CC_VALUE")), "", tblResults.Rows(0)("CC_VALUE").ToString.Replace("<", "&lt;").Replace(">", "&gt;")) & "</pre>"
            litSubject.Text = IIf(IsDBNull(tblResults.Rows(0)("SUBJECT")), "", tblResults.Rows(0)("SUBJECT").ToString.Replace("<", "&lt;").Replace(">", "&gt;"))
            litMessageBody.Text = Server.HtmlDecode(tblResults.Rows(0)("MESSAGE_BODY"))
        End If

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Dim tblResults As DataTable = getBasicFormDataFromDatabase()
        Dim toEmail As String = tblResults.Rows(0)("TO_VALUE")
        Dim FromEmail As String = Session("USER_NAME") & " <" & Session("USER_EMAIL") & ">"
        Dim CCEmail As String = litCC.Text.ToString.Replace("&lt;", "<").Replace("&gt;", ">") & ";" & txtCC.Text.ToString.Replace("&lt;", "<").Replace("&gt;", ">")
        Dim BCCEmail As String = txtBCC.Text
        Dim Subject As String = tblResults.Rows(0)("SUBJECT")
        Dim Message As String = tblResults.Rows(0)("MESSAGE_BODY") & "<br /><br />Comments: " & txtComments.Text.ToString.Trim

        If g_portalEnvironment = "TEST" Then
            g_SendEmail(ToAddress:="Brian Averitt <Brian.Averitt@Itgbrands.com>", FromAddress:=FromEmail, CC_Address:="", BCC_Address:="Dustin Hall <Dustin.Hall@Itgbrands.com>", Subject:=Subject, Message:=Message)
        Else
            g_SendEmail(toEmail, FromEmail, CCEmail, BCCEmail, Subject, Message)
        End If

        Dim strSQL As String = "Exec udp_incrementReportCounter @id = " & Request.QueryString("ID")
        g_IO_Execute_SQL(strSQL, False)

        logUserSubmission()

    End Sub

    Protected Sub imgAddBCC_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddBCC.Click
        hidePanels()
        loadBCCDropDown()
        btnAddBCC.Text = "Add BCC"
        pnlAddBCC.Visible = True
    End Sub

    Protected Sub imgAddCC_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddCC.Click
        hidePanels()
        loadBCCDropDown()
        btnAddBCC.Text = "Add CC"
        pnlAddBCC.Visible = True
    End Sub

    Protected Sub btnAddBCC_Click(sender As Object, e As EventArgs) Handles btnAddBCC.Click

        Dim delimiter As String = ""

        If btnAddBCC.Text.ToString.IndexOf("BCC") <> -1 Then
            ''Handle the BCC's
            If txtBCC.Text <> "" Then
                delimiter = "; "
            End If
            For Each item In lstGroupListing.Items
                If item.selected Then
                    'Dim i As Integer = item.ToString.IndexOf("<")
                    'Dim emailAddress As String = item.ToString.Substring(i + 1, item.ToString.IndexOf(">", i + 1) - i - 1)
                    'Dim userName As String = item.ToString.Substring(0, item.ToString.IndexOf("<")).Trim()
                    'txtBCCvalue.Text &= delimiter & item.ToString
                    txtBCC.Text &= delimiter & item.ToString
                    delimiter = "; "
                End If
            Next

            For Each item In lstUserListing.Items
                If item.selected Then
                    'Dim i As Integer = item.ToString.IndexOf("<")
                    'Dim emailAddress As String = item.ToString.Substring(i + 1, item.ToString.IndexOf(">", i + 1) - i - 1)
                    'Dim userName As String = item.ToString.Substring(0, item.ToString.IndexOf("<")).Trim()
                    'txtBCCvalue.Text &= delimiter & item.ToString
                    txtBCC.Text &= delimiter & item.ToString
                    delimiter = "; "
                End If
            Next

        Else
            ''Handle the CC's
            If txtCC.Text <> "" Then
                delimiter = "; "
            End If
            For Each item In lstGroupListing.Items
                If item.selected Then
                    'Dim i As Integer = item.ToString.IndexOf("<")
                    'Dim emailAddress As String = item.ToString.Substring(i + 1, item.ToString.IndexOf(">", i + 1) - i - 1)
                    'Dim userName As String = item.ToString.Substring(0, item.ToString.IndexOf("<")).Trim()
                    'txtBCCvalue.Text &= delimiter & item.ToString
                    txtCC.Text &= delimiter & item.ToString
                    delimiter = "; "
                End If
            Next
            For Each item In lstUserListing.Items
                If item.selected Then
                    'Dim i As Integer = item.ToString.IndexOf("<")
                    'Dim emailAddress As String = item.ToString.Substring(i + 1, item.ToString.IndexOf(">", i + 1) - i - 1)
                    'Dim userName As String = item.ToString.Substring(0, item.ToString.IndexOf("<")).Trim()
                    'txtCCValue.Text &= delimiter & item.ToString
                    txtCC.Text &= delimiter & item.ToString
                    delimiter = "; "
                End If
            Next
        End If


        hidePanels()
        pnlFormHeader.Visible = True
    End Sub

End Class
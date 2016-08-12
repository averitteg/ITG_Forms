Public Class NewBasicForm
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            loadCategoryDropdown()
            hidePanels()
            pnlForm.Visible = True
        End If

    End Sub

    Private Sub loadCategoryDropdown()
        Dim strSQL As String = "SELECT MC_CATEGORY_ID, CATEGORY_NAME FROM DBO.MC_CATEGORY ORDER BY CATEGORY_NAME"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        ddlCategory.DataValueField = "MC_CATEGORY_ID"
        ddlCategory.DataTextField = "CATEGORY_NAME"
        ddlCategory.DataSource = tblResults
        ddlCategory.DataBind()
    End Sub

    Private Sub clearForm()
        txtFormName.Text = ""
        txtSubject.Text = ""
        txtCC.Text = ""
        txtTo.Text = ""
        ckeMessageContent.Text = ""
        txtDropDownLabel.Text = ""
        txtDropDownOptions.Text = ""
    End Sub

    Private Sub hidePanels()
        pnlAddBCC.Visible = False
        pnlForm.Visible = False
    End Sub

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

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        clearForm()
        litMessage.Text = ""
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim urlName As String = "frmBasic.aspx"
        Dim formName As String = txtFormName.Text.Replace("'", "''")
        Dim strArticle As String = ckeMessageContent.Text.Replace("'", "''")
        Dim to_Value As String = txtTo.Text.Replace("'", "''")
        Dim CC_Value As String = txtCC.Text.Replace("'", "''")
        Dim Subject As String = txtSubject.Text.Replace("'", "''")

        Dim strSQL As String = "Insert into mc_forms (form_name, mc_category, url) VALUES (" &
                        "'" & formName & "'," & ddlCategory.SelectedValue & ",'')"
        Dim intFormID As Integer = g_IO_Execute_SQL(strSQL, False).Rows(0)(0)



        If Trim(txtDropDownLabel.Text) <> "" And Trim(txtDropDownOptions.Text) <> "" Then
            Dim intFormLabelID As Integer
            strSQL = "Insert into FORM_DDL_LABEL (MC_FORMS_ID,FORM_DDL_LABEL) VALUES (" & intFormID & ",'" & txtDropDownLabel.Text.Replace("'", "''") & "')"
            intFormLabelID = g_IO_Execute_SQL(strSQL, False).Rows(0)(0)
            For Each item In Trim(txtDropDownOptions.Text).ToString.Split(",")
                strSQL = "Insert into FORM_DDL_OPTIONS (FORM_DDL_LABEL_ID,[OPTION]) VALUES (" & intFormLabelID & ",'" & item.Replace("'", "''") & "')"
                g_IO_Execute_SQL(strSQL, False)
            Next
            If ddlOptionType.SelectedValue = "DDL" Then
                urlName = "frmBasic1ddl.aspx"
            Else
                urlName = "frmBasic1cbl.aspx"
            End If


        End If

        Dim url As String = urlName & "?form=bsc&id=" & intFormID
        strSQL = "update dbo.mc_forms set url = '" & url & "' where mc_forms_id = " & intFormID
        g_IO_Execute_SQL(strSQL, False)

        strSQL = "Insert into dbo.form_details (mc_forms_id, to_value, cc_value, subject, message_body) values (" &
                intFormID & ",'" & to_Value & "','" & CC_Value & "','" & Subject & "','" & strArticle & "')"
        g_IO_Execute_SQL(strSQL, False)


        litMessage.Text = "<span style=""color: green;"">Form <a href=""" & g_portalURL & url & """>" & txtFormName.Text & " </a> has been successfully created.</span>"
        clearForm()
        hidePanels()
        pnlForm.Visible = True
    End Sub

    Protected Sub imgAddCC_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddCC.Click
        hidePanels()
        loadBCCDropDown()
        btnAddBCC.Text = "Add 'CC' Emails"
        pnlAddBCC.Visible = True
    End Sub

    Protected Sub imgAddTo_Click(sender As Object, e As ImageClickEventArgs) Handles imgAddTo.Click
        hidePanels()
        loadBCCDropDown()
        btnAddBCC.Text = "Add 'To' Emails"
        pnlAddBCC.Visible = True
    End Sub

    Protected Sub btnAddBCC_Click(sender As Object, e As EventArgs) Handles btnAddBCC.Click

        Dim delimiter As String = ""

        If btnAddBCC.Text.ToString.IndexOf("CC") <> -1 Then
            ''Handle the CC's
            If txtCC.Text <> "" Then
                delimiter = "; "
            End If
            For Each item In lstGroupListing.Items
                If item.selected Then
                    txtCC.Text &= delimiter & item.ToString
                    delimiter = "; "
                End If
            Next

            For Each item In lstUserListing.Items
                If item.selected Then
                    txtCC.Text &= delimiter & item.ToString
                    delimiter = "; "
                End If
            Next

        Else
            ''Handle the To's
            If txtTo.Text <> "" Then
                delimiter = "; "
            End If
            For Each item In lstGroupListing.Items
                If item.selected Then
                    txtTo.Text &= delimiter & item.ToString
                    delimiter = "; "
                End If
            Next
            For Each item In lstUserListing.Items
                If item.selected Then
                    txtTo.Text &= delimiter & item.ToString
                    delimiter = "; "
                End If
            Next
        End If

        hidePanels()
        pnlForm.Visible = True

    End Sub

End Class
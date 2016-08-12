Public Class frmDynamic
    Inherits System.Web.UI.Page

    Dim pnlTextBox As Panel
    Dim pnlDropDownList As Panel
    Dim litCounter As Integer = 1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        Else
            If IsNothing(Request.QueryString("ID")) Then
                Response.Redirect("Default.aspx")
            Else
                hidePanels()
                ''Load the form
                load_basic_form()
                pnlFormHeader.Visible = True
            End If
        End If

        generateDynamicObjects()
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
            litTO.Text = tblResults.Rows(0)("TO_VALUE").ToString.Replace("<", "&lt;").Replace(">", "&gt;")
            litFrom.Text = Session("USER_NAME") & "&nbsp;&lt;" & Session("USER_EMAIL") & "&gt;"
            litCC.Text = IIf(IsDBNull(tblResults.Rows(0)("CC_VALUE")), "", tblResults.Rows(0)("CC_VALUE").ToString.Replace("<", "&lt;").Replace(">", "&gt;"))
            litSubject.Text = IIf(IsDBNull(tblResults.Rows(0)("SUBJECT")), "", tblResults.Rows(0)("SUBJECT").ToString.Replace("<", "&lt;").Replace(">", "&gt;"))
            ''litMessageBody.Text = Server.HtmlDecode(tblResults.Rows(0)("MESSAGE_BODY"))
        End If

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


#Region "DYNAMIC FORM CODE"

    Private Function genLitBreak()
        Dim lit As New Literal()
        lit.ID = "Lit_" & litCounter
        litCounter += 1
        lit.Text = "<br />"
        Return lit
    End Function

    Private Function genTextBox(ByVal ordinal As Integer, ByVal type As String, ByVal options As String)
        Dim TXT As New TextBox()
        TXT.ID = type & "_" & ordinal

        options = options.ToString.ToUpper
        If options <> "" Then
            Dim delimiter As String = ""
            For Each x In options.Split(",")
                TXT.CssClass &= delimiter & x.ToString
                delimiter = " "
            Next
        End If

        If options.IndexOf("MULTILINE") <> -1 Then
            TXT.TextMode = TextBoxMode.MultiLine
        End If

        Return TXT
    End Function

    Private Function genDDL(ByVal formID As Integer, ByVal ordinal As Integer, ByVal type As String)
        Dim DDL As New DropDownList
        DDL.ID = type & "_" & ordinal

        Dim strSQL As String = "Select * from dynamic_form_ddl_options where mc_form_id = " & formID & " and ordinal_position = " & ordinal & " order by value"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)

        Try
            If tblResults.Rows.Count > 0 Then
                For Each row In tblResults.Rows
                    Dim listItem = New ListItem(row("value"), row("value"))
                    DDL.Items.Add(listItem)
                Next
            Else

            End If
        Catch ex As Exception
            ''Do nothing, leave blank

        End Try
        Return DDL
    End Function

    Private Function genLBL(ByVal ordinal As Integer, ByVal value As String, ByVal options As String)
        Dim LBL As New Label()
        LBL.ID = "LABEL_" & ordinal
        LBL.Text = value
        options = options.ToString.ToUpper
        If options <> "" Then
            Dim delimiter As String = ""
            For Each x In options.Split(",")
                LBL.CssClass &= delimiter & x.ToString
                delimiter = " "
            Next
        End If
        Return LBL
    End Function

    Private Sub generateDynamicObjects()
        Dim lbl As Label

        ''Get the form data
        Dim strSQL As String = "Select * from dbo.dynamic_form_control where mc_form_id = " & Request.QueryString("ID") & "  order by ordinal_position"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        For Each row In tblResults.Rows
            If IsDBNull(row("control_type")) Then
                ''Display label with options
                Dim labelOptions As String = row("label_options").ToString
                pnlFormDetails.Controls.Add(genLBL(row("Ordinal_position"), row("label"), labelOptions))

            Else
                lbl = New Label()
                lbl.ID = "label_" & row("ordinal_position")
                lbl.Text = row("label") & " : "
                pnlFormDetails.Controls.Add(lbl)

                Dim dataType As String = row("control_type").ToString.ToUpper
                If dataType = "TEXTBOX" Or dataType = "DATE" Then
                    pnlFormDetails.Controls.Add(genTextBox(row("ordinal_position"), row("control_type"), row("label_options").ToString()))
                ElseIf dataType = "DROPDOWN" Then
                    pnlFormDetails.Controls.Add(genDDL(25, row("ordinal_position"), row("control_type")))
                ElseIf dataType = "LABEL" Then
                    pnlFormDetails.Controls.Add(genLBL(row("Ordinal_position"), row("label"), row("label_options")))
                End If
            End If

            pnlFormDetails.Controls.Add(genLitBreak())
            pnlFormDetails.Controls.Add(genLitBreak())
        Next

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        If btn.ID = "btnAddTxt" Then
            Dim cnt As Integer = FindOccurence("txtDynamic")
            Dim txt As New TextBox()
            txt.ID = "txtDynamic-" & Convert.ToString(cnt + 1)
            pnlTextBox.Controls.Add(txt)
        End If
    End Sub

    Private Function FindOccurence(ByVal substr As String) As Integer
        Dim reqstr As String = Request.Form.ToString()
        Return ((reqstr.Length - reqstr.Replace(substr, "").Length) / substr.Length)
    End Function

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim tblResults As DataTable = getBasicFormDataFromDatabase()
        Dim toEmail As String = tblResults.Rows(0)("TO_VALUE")
        Dim FromEmail As String = Session("USER_NAME") & " <" & Session("USER_EMAIL") & ">"
        Dim CCEmail As String = litCC.Text.ToString.Replace("&lt;", "<").Replace("&gt;", ">") & ";" & txtCC.Text.ToString.Replace("&lt;", "<").Replace("&gt;", ">")
        Dim BCCEmail As String = txtBCC.Text
        Dim Subject As String = tblResults.Rows(0)("SUBJECT")
        Dim Message As String = ""

        Dim strSQL As String = "Select * from dbo.dynamic_form_control where mc_form_id = " & Request.QueryString("ID") & " order by ordinal_position"
        Dim tblResultsDyn As DataTable = g_IO_Execute_SQL(strSQL, False)
        lblMessage.Text = ""
        For Each row In tblResultsDyn.Rows
            Dim strControlName = ""
            If IsDBNull(row("control_type")) Then
                ''Encapsulate the control for formatting
                Dim strEncap As String = "<span style="""
                Dim strCloseSpan As String = "</span>"
                ''Only Find the Label
                strControlName = "LABEL_" & row("ordinal_position")
                Dim curControl1 As Control = pnlFormDetails.FindControl(strControlName)
                Dim lblControl As Label = curControl1
                If row("label_options").ToString <> "" Then
                    strEncap = "<span style="""
                    For Each item In row("label_options").ToString.Split(",")
                        If item.ToUpper = "BOLD" Then
                            strEncap &= "font-weight: bold;"
                        ElseIf item.ToUpper = "UNDERLINE" Then
                            strEncap &= "text-decoration: underline;"
                        ElseIf item.ToUpper = "SECTIONHEAD" Then
                            strEncap &= "font-size: 1.2em;"
                        ElseIf item.ToUpper = "CENTER" Then
                            strEncap = "<center>" & strEncap
                            strCloseSpan = "</span></center>"
                        End If
                    Next
                End If
                strEncap &= """>"
                Message &= strEncap & lblControl.Text & strCloseSpan
            Else
                ''Find the label and the control
                ''Find the Label
                strControlName = "LABEL_" & row("ordinal_position")
                Dim curControl1 As Control = pnlFormDetails.FindControl(strControlName)
                Dim lblControl As Label = curControl1
                Message &= lblControl.Text

                ''Find Control and handle types
                strControlName = row("control_type") & "_" & row("ordinal_position")
                Dim curControl2 As Control = pnlFormDetails.FindControl(strControlName)
                If TypeOf curControl2 Is TextBox Then
                    Dim txtControl As TextBox = curControl2
                    Message &= txtControl.Text
                ElseIf TypeOf curControl2 Is DropDownList Then
                    Dim ddlControl As DropDownList = curControl2
                    Message &= ddlControl.SelectedValue.ToString
                ElseIf TypeOf curControl2 Is CheckBoxList Then
                    Dim cblControl As CheckBoxList = curControl2
                    Message &= cblControl.SelectedValue.ToString
                ElseIf TypeOf curControl2 Is RadioButtonList Then
                    Dim rdoControl As CheckBoxList = curControl2
                    Message &= rdoControl.SelectedValue.ToString
                End If

            End If
            Message &= "<br /><br />"
        Next

        lblMessage.Text = Message

        If g_portalEnvironment = "TEST" Then
            g_SendEmail(ToAddress:="Brian Averitt <Brian.Averitt@Itgbrands.com>", FromAddress:=FromEmail, CC_Address:="", BCC_Address:="Dustin Hall <Dustin.Hall@Itgbrands.com>", Subject:=Subject, Message:=Message)
        Else
            g_SendEmail(toEmail, FromEmail, CCEmail, BCCEmail, Subject, Message)
        End If

        Dim strSQL1 As String = "Exec udp_incrementReportCounter @id = " & Request.QueryString("ID")
        g_IO_Execute_SQL(strSQL1, False)

        logUserSubmission()

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("Default.aspx")
    End Sub

#End Region

End Class
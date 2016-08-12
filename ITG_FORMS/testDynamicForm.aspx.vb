Public Class testDynamicForm
    Inherits System.Web.UI.Page

    Dim pnlTextBox As Panel
    Dim pnlDropDownList As Panel
    Dim litCounter As Integer = 1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        generateDynamicObjects()
    End Sub

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
        Dim strSQL As String = "Select * from dbo.dynamic_form_control where mc_form_id = 25 order by ordinal_position"
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
        Dim strSQL As String = "Select * from dbo.dynamic_form_control where mc_form_id = 25 order by ordinal_position"
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSQL, False)
        lblMessage.Text = ""
        For Each row In tblResults.Rows
            Dim strControlName = ""
            If IsDBNull(row("control_type")) Then
                ''Only Find the Label
                strControlName = "LABEL_" & row("ordinal_position")
                Dim curControl1 As Control = pnlFormDetails.FindControl(strControlName)
                Dim lblControl As Label = curControl1
                lblMessage.Text &= lblControl.Text
            Else
                ''Find the label and the control
                ''Find the Label
                strControlName = "LABEL_" & row("ordinal_position")
                Dim curControl1 As Control = pnlFormDetails.FindControl(strControlName)
                Dim lblControl As Label = curControl1
                lblMessage.Text &= lblControl.Text

                ''Find Control and handle types
                strControlName = row("control_type") & "_" & row("ordinal_position")
                Dim curControl2 As Control = pnlFormDetails.FindControl(strControlName)
                If TypeOf curControl2 Is TextBox Then
                    Dim txtControl As TextBox = curControl2
                    lblMessage.Text &= txtControl.Text
                ElseIf TypeOf curControl2 Is DropDownList Then
                    Dim ddlControl As DropDownList = curControl2
                    lblMessage.Text &= ddlControl.SelectedValue.ToString
                ElseIf TypeOf curControl2 Is CheckBoxList Then
                    Dim cblControl As CheckBoxList = curControl2
                    lblMessage.Text &= cblControl.SelectedValue.ToString
                ElseIf TypeOf curControl2 Is RadioButtonList Then
                    Dim rdoControl As CheckBoxList = curControl2
                    lblMessage.Text &= rdoControl.SelectedValue.ToString
                End If

            End If
            lblMessage.Text &= "<br /><br />"
        Next
    End Sub

End Class
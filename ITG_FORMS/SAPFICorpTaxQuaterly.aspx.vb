Public Class SAPFICorpTaxQuaterly
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsPostBack Then
        'Else
        '    If IsNothing(Request.QueryString("form")) Then
        '        Response.Redirect("Default.aspx")
        '    Else
        hidePanels()
        '        ''Load the form
        '        load_basic_form()
        pnlFormHeader.Visible = True
        pnlFormHeader.Controls.Add(genTextBox(1, "textbox", "DATE"))
        '    End If
        'End If
    End Sub

    Private Sub hidePanels()
        pnlAddBCC.Visible = False
        pnlFormHeader.Visible = False
    End Sub

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
            ''pnlFormDetails.Controls.Add(genLitBreak())
            TXT.TextMode = TextBoxMode.MultiLine
        End If

        Return TXT
    End Function

End Class
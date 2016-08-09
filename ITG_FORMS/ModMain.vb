Imports System.Net.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Security.Principal
Imports System.DirectoryServices.AccountManagement

Module ModMain

    Dim g_EmailHostIPAddress As String = ConfigurationManager.AppSettings("EmailIPAddress")
    Dim g_EmailPort As String = ConfigurationManager.AppSettings("EmailPort")
    Dim g_EmailUserName As String = ConfigurationManager.AppSettings("EmailUserName")
    Dim g_EmailPassword As String = ConfigurationManager.AppSettings("EmailPassword")
    Dim g_EmailFromAddress As String = ConfigurationManager.AppSettings("EmailFromAddress")
    Dim g_EmailEnabled As Boolean = IIf(UCase(ConfigurationManager.AppSettings("EmailEnabled")) = "TRUE", True, False)
    Dim g_SiteLocation As String = ConfigurationManager.AppSettings("siteLocation")
    Public g_EfilesReferralsBaseDirectory As String = ConfigurationManager.AppSettings("EfilesReferralsBaseDirectory")
    Public g_PDFTempDirectory As String = ConfigurationManager.AppSettings("TempPDFDir")
    Public g_StringArrayOuterSplitParameter = "||"
    Public g_StringArrayValueSplitParameter = "~~"
    Public g_intUserRecid = -1
    Public g_strFormObjectTypes = "input+select+textarea"
    Public g_Debug As Boolean = False
    Public g_portalURL As String = ConfigurationManager.AppSettings("portalURL")
    Public g_portalEnvironment As String = ConfigurationManager.AppSettings("portalEnvironment")



    Public Sub g_RetrieveSessions(ByRef txtSessions As TextBox)
        g_RetrieveSessions(txtSessions.Text)
    End Sub

    Public Sub g_RetrieveSessions(ByRef txtSessions As HiddenField)
        g_RetrieveSessions(txtSessions.Value)
    End Sub

    Public Sub g_RetrieveSessions(ByRef txtSessions As String)

        If Trim(txtSessions = "") Then
            ' no Sessions string area sent to client form
        Else
            Dim arrStrSessionVariables() As String = Split(txtSessions, "^^")


            ' restore session variables
            For Each strSessionVariable As String In arrStrSessionVariables
                Dim strSessionVariablePair() As String = Split(strSessionVariable, "||")
                System.Web.HttpContext.Current.Session(strSessionVariablePair(0)) = strSessionVariablePair(1)
            Next
        End If

    End Sub

    Public Sub g_SendSessions(ByRef txtSessions As TextBox)
        txtSessions.Text = g_SendSessions(txtSessions.Text)
    End Sub

    Public Sub g_SendSessions(ByRef txtSessions As HiddenField)
        txtSessions.Value = g_SendSessions(txtSessions.Value)
    End Sub

    Public Function g_SendSessions(ByRef txtSessions As String) As String

        '  Only do this if user is not signed on
        Dim strSessionsName As String = ""
        Dim strDelimiter As String = ""

        For Each txtFieldName As String In System.Web.HttpContext.Current.Session.Keys
            If txtFieldName.ToUpper = "RELAYMESSAGE" Then
            Else
                strSessionsName = strSessionsName & strDelimiter & txtFieldName & "||" & System.Web.HttpContext.Current.Session(txtFieldName)
                strDelimiter = "^^"
            End If
        Next

        Return strSessionsName

    End Function

    Public Sub g_SendEmail(ByVal ToAddress As String, ByVal Subject As String, ByVal Message As String)
        Debug.WriteLine("Email Module -- To: " & ToAddress & "  Subject: " & Subject & "   Message: " & Message)
        If ConfigurationManager.AppSettings("EmailEnabled") = True Then
            Dim Mail As New System.Net.Mail.MailMessage
            Mail.Subject = Subject
            If ToAddress = "" Then
                Debug.Print("ModMain (g_SendEmail): No email address provided. Can't send it.")
            Else
                For Each strEmailAddress As String In Split(ToAddress, ";")
                    Mail.To.Add(strEmailAddress)
                Next

                Mail.From = New System.Net.Mail.MailAddress(g_EmailFromAddress)
                Mail.Body = Message

                Dim strHTMLCheck As String = UCase(Message)
                Mail.IsBodyHtml = strHTMLCheck.ToUpper.Contains("<BODY") Or strHTMLCheck.ToUpper.Contains("<TABLE") Or strHTMLCheck.ToUpper.Contains("<DIV") Or strHTMLCheck.ToUpper.Contains("<BR") Or strHTMLCheck.ToUpper.Contains("<P")
                Dim SMTPServer As New System.Net.Mail.SmtpClient()

                SMTPServer.Timeout = 100000
                SMTPServer.Host = g_EmailHostIPAddress
                SMTPServer.Port = g_EmailPort
                SMTPServer.EnableSsl = False
                ''SMTPServer.Credentials = New System.Net.NetworkCredential(g_EmailUserName, g_EmailPassword)
                Debug.Print(ToAddress & " - " & Subject)
                If g_EmailEnabled Then
                    SMTPServer.Send(Mail)
                End If
            End If
        End If
    End Sub

    Function GetNextDate(ByVal d As DayOfWeek, Optional ByVal StartDate As Date = Nothing) As Date
        If StartDate = DateTime.MinValue Then
            StartDate = Now
            For p As Integer = 1 To 7
                If StartDate.AddDays(p).DayOfWeek = d Then Return StartDate.AddDays(p)
            Next
        Else
            Return Date.Now
        End If
    End Function

    Public Function getUserGroupsAsString()
        Dim strGroups As String = ""
        ''PrincipalContext yourDomain = new PrincipalContext(ContextType.Domain);
        Dim curDomain As PrincipalContext = New PrincipalContext(ContextType.Domain)
        Dim curUser As String = HttpContext.Current.Request.LogonUserIdentity.Name.ToString.Replace("D700\", "").Replace("ITGBRANDS\", "")

        ''Find your User
        ''UserPrincipal user = UserPrincipal.FindByIdentity(yourDomain, userName);
        Dim curUserPrincipal As UserPrincipal = UserPrincipal.FindByIdentity(curDomain, curUser)

        If Not IsNothing(curUserPrincipal) Then
            ''USER FOUND
            'PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();
            Dim delimiter As String = ""
            Dim userGroups As PrincipalSearchResult(Of Principal) = curUserPrincipal.GetAuthorizationGroups
            For Each p In userGroups
                strGroups &= delimiter & p.Name.ToString
                delimiter = "<br />"
            Next

        Else

        End If







        For Each row In HttpContext.Current.Request.LogonUserIdentity.Groups
            strGroups &= row.ToString
        Next
        Return strGroups
    End Function

    Public Function validateUser()
        Dim User As String = ""
        Dim strSql As String = ""
        Dim tblResults As DataTable = Nothing

        ''''''''''VERIFY USER''''''''''''
        If IsNothing(System.Web.HttpContext.Current.Session("User_Name")) Then
            User = HttpContext.Current.Request.LogonUserIdentity.Name.ToString.Replace("ITGBRANDS\", "").Replace("D700\", "")
            strSql = "Select user_id, NAME, CAN_EDIT, IS_ADMIN from vw_sys_users where user_id = '" & User & "'"
            tblResults = g_IO_Execute_SQL(strSql, False)

            If tblResults.Rows.Count > 0 Then
                System.Web.HttpContext.Current.Session("USER_NAME") = tblResults.Rows(0)("NAME")
                System.Web.HttpContext.Current.Session("USER_ID") = tblResults.Rows(0)("USER_ID")
                System.Web.HttpContext.Current.Session("CAN_EDIT") = tblResults.Rows(0)("CAN_EDIT")
                System.Web.HttpContext.Current.Session("IS_ADMIN") = tblResults.Rows(0)("IS_ADMIN")


                Return True
            Else
                Return False
            End If
        Else
            Return True
            Exit Function
        End If
    End Function

    Public Function validateAdmin()
        If IsNothing(System.Web.HttpContext.Current.Session("IS_ADMIN")) Then
            validateUser()
        End If

        If System.Web.HttpContext.Current.Session("IS_ADMIN") = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function validateCanEdit()
        If IsNothing(System.Web.HttpContext.Current.Session("CAN_EDIT")) Then
            validateUser()
        End If

        If System.Web.HttpContext.Current.Session("CAN_EDIT") = 1 Or System.Web.HttpContext.Current.Session("IS_ADMIN") = 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub createDefaultRow(ByRef tbl As DataTable, ByVal strDefault As String, ByVal strTextField As String, ByVal strValueField As String)
        ''Create the default
        Dim defaultRow As DataRow = tbl.NewRow
        defaultRow(strTextField) = strDefault
        defaultRow(strValueField) = "-1"
        tbl.Rows.InsertAt(defaultRow, 0)
    End Sub

    Public Function createCSVLink(ByVal URL As String)
        Dim strResponse As String = "<div><img src=""images/download-csv.png"" style=""cursor: pointer"" onclick=""redirect('" & URL & "')"" /></div>"
        Return strResponse
    End Function

    Public Sub create_csv_from_datatable(ByVal table As DataTable, ByVal strFileName As String)
        Dim myCsv As New StringBuilder
        Dim strBuilder As String = ""
        Dim delimiter As String = ""
        ''Build the header
        For Each col In table.Columns
            strBuilder &= delimiter & col.ToString.ToUpper
            delimiter = ","
        Next
        ''Append the column header to the csv
        myCsv.AppendLine(strBuilder)

        For Each row In table.Rows
            delimiter = ""
            strBuilder = ""
            For Each col In table.Columns
                strBuilder &= delimiter & """" & IIf(IsDBNull(row(col)), "", row(col).ToString.ToUpper) & """"
                delimiter = ","
            Next
            myCsv.AppendLine(strBuilder)
        Next

        'foreach(myRow r in myRows) {
        '  myCsv.AppendFormat("\"{0}\",{1}", r.MyCol1, r.MyCol2);
        '}

        System.Web.HttpContext.Current.Response.ContentType = "application/csv"
        System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" & strFileName)
        System.Web.HttpContext.Current.Response.Write(myCsv.ToString())
        System.Web.HttpContext.Current.Response.End()

    End Sub

    Public Function createHeaderWithSorts(ByVal headerList As String, ByVal redirectPage As String, ByVal QueryString As NameValueCollection)
        Dim strResponse As String = ""
        For Each col In headerList.Split(";")
            ''rpt_GetReportDetails.aspx?Report=LOC_W_USR
            If Not IsNothing(QueryString("sort")) Then
                ''It's been sorted already  Check the sort order
                If QueryString("sort").ToString.ToUpper = col.Split("~")(1).ToUpper AndAlso QueryString("order").ToString.ToUpper = "ASC" Then
                    strResponse &= "<th style=""cursor: pointer;"" onclick=""redirect('" & redirectPage & "&sort=" & col.Split("~")(1) & "&order=desc')"">" & col.Split("~")(0) & "</th>"
                Else
                    strResponse &= "<th style=""cursor: pointer;"" onclick=""redirect('" & redirectPage & "&sort=" & col.Split("~")(1) & "&order=asc')"">" & col.Split("~")(0) & "</th>"
                End If
            Else
                ''It hasn't been sorted, so set the defaults...
                strResponse &= "<th style=""cursor: pointer;"" onclick=""redirect('" & redirectPage & "&sort=" & col.Split("~")(1) & "&order=asc')"">" & col.Split("~")(0) & "</th>"
            End If
        Next
        Return strResponse
    End Function

    Public Function createSortedTblView(ByVal tblToSort As DataTable, ByVal colToSort As String, ByVal sortOrder As String)
        Dim dataView As New DataView(tblToSort)

        If Not IsNothing(colToSort) Then
            ''Sort in the order given
            dataView.Sort = colToSort & " " & sortOrder ''" AutoID DESC, Name DESC"
            tblToSort = dataView.ToTable()
        End If

        Return tblToSort

    End Function

    Public Function returnQueryString(ByVal queryString As NameValueCollection)
        Dim strVariables As String = ""
        Dim delimiter As String = ""

        For Each item In queryString
            strVariables &= delimiter & item.ToString & "=" & queryString(item)
            delimiter = "&"
        Next

        Return strVariables
    End Function

    Public Sub logActivity(ByVal queryString As NameValueCollection, ByVal PAGE As String, ByVal Action As String)
        If queryString.Count > 0 Then
            PAGE &= "?" & returnQueryString(queryString)
        End If

        Dim userID As String = System.Web.HttpContext.Current.Session("USER_ID")
        Dim strSQL As String = "udp_LogActivity @USERID = '" & userID.Replace("'", "''") & "', @PAGE = '" & PAGE.Replace("'", "''") & "', @ACTION = '" & Action.Replace("'", "''") & "'"
        g_IO_Execute_SQL(strSQL, False)
    End Sub


End Module

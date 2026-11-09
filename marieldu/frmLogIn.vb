Public Class frmLogIn

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        Dim usernamInput As String = txtusername.Text.Trim()
        Dim passwordInput As String = txtpass.Text.Trim()

        ' 1. Validate non-empty inputs
        If String.IsNullOrEmpty(usernamInput) OrElse String.IsNullOrEmpty(passwordInput) Then
            MsgBox("Please enter both username and password!", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        ' 2. Hash the entered password
        Dim hashedPassword As String = HashPassword(passwordInput)

        Try
            Call connection()

            ' 3. Query role where BOTH username and hashed password match
            Dim query As String = "SELECT role FROM accs WHERE usernamee = ? AND password = ? LIMIT 1"

            Using cmd As New Odbc.OdbcCommand(query, con)
                cmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = usernamInput
                cmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = hashedPassword

                If con.State = ConnectionState.Closed Then con.Open()
                Dim userRoleObj As Object = cmd.ExecuteScalar()

                If userRoleObj IsNot Nothing AndAlso Not IsDBNull(userRoleObj) Then
                    currentUserRole = userRoleObj.ToString().Trim()

                    MsgBox("Log In Successfully!", MsgBoxStyle.Information, "Success")

                    ' --- ROLE-BASED ROUTING ---
                    If String.Equals(currentUserRole, "Admin", StringComparison.OrdinalIgnoreCase) Then
                        Form1.Show()
                    Else
                        frmReserve.Show()
                    End If

                    Me.Hide() ' Hide login window
                Else
                    MsgBox("Invalid username or password!", MsgBoxStyle.Critical, "Log In Failed")
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Database Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        frnSignUp.Show()
        Me.Hide()
        txtusername.Clear()
        txtpass.Clear()
    End Sub

    Private Sub txtpass_TextChanged(sender As Object, e As EventArgs) Handles txtpass.TextChanged
        If txtpass.Text <> "" Then
            eyeee.Visible = True
        Else
            eyeee.Visible = False
        End If
    End Sub

    Private Sub eyeee_MouseDown(sender As Object, e As MouseEventArgs) Handles eyeee.MouseDown
        If txtpass.UseSystemPasswordChar = True Then
            txtpass.UseSystemPasswordChar = False
        Else
            txtpass.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        MsgBox("Please contact the faculty for help!", MsgBoxStyle.Information)
    End Sub

   
End Class
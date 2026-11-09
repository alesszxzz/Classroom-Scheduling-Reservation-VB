Public Class frnSignUp

    Public Sub RegisterUser(ByVal username As String, ByVal plainPassword As String)
        If username = "" OrElse plainPassword = "" Then
            MsgBox("Please fill in both the username and password fields.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            If con.State = ConnectionState.Closed Then con.Open()

            Dim checkQuery As String = "SELECT COUNT(*) FROM accs WHERE usernamee = ?"
            Using checkCmd As New Odbc.OdbcCommand(checkQuery, con)
                checkCmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = username

                Dim userCount As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                If userCount > 0 Then
                    MsgBox("Username is already taken! Please choose another one.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim hashedPassword As String = HashPassword(plainPassword)
            Dim insertQuery As String = "INSERT INTO accs (usernamee, password) VALUES (?, ?)"

            Using cmd As New Odbc.OdbcCommand(insertQuery, con)
                cmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = username
                cmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = hashedPassword

                cmd.ExecuteNonQuery()
                MsgBox("User registered successfully!", MsgBoxStyle.Information)

                Call ClearFields()
                frmLogIn.Show()
                Me.Hide()
            End Using

        Catch ex As Exception
            MsgBox("Error registering user: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        If txtpass.Text.Trim <> TextBox1.Text.Trim Then
            MsgBox("Passwords do not match! Please confirm your password.", MsgBoxStyle.Exclamation)
        Else
            Call RegisterUser(txtusername.Text.Trim, txtpass.Text.Trim)
        End If
    End Sub

    Private Sub ClearFields()
        txtpass.Clear()
        txtusername.Clear()
        TextBox1.Clear()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        Call ClearFields()
        frmLogIn.Show()
        Me.Hide()
    End Sub

    Private Sub txtpass_TextChanged(sender As Object, e As EventArgs) Handles txtpass.TextChanged
        eyeee.Visible = (txtpass.Text.Trim <> "")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        PictureBox4.Visible = (TextBox1.Text.Trim <> "")
    End Sub

    Private Sub eyeee_MouseClick(sender As Object, e As MouseEventArgs) Handles eyeee.MouseClick
        txtpass.UseSystemPasswordChar = Not txtpass.UseSystemPasswordChar
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        TextBox1.UseSystemPasswordChar = Not TextBox1.UseSystemPasswordChar
    End Sub

End Class
Public Class frmforgotpass

    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        If txtpass.Text.Trim <> TextBox1.Text.Trim Then
            MsgBox("Passwords do not match! Please confirm your password.", MsgBoxStyle.Exclamation)
        Else
            Call RegisterUser(txtusername.Text.Trim, txtpass.Text.Trim)
        End If
    End Sub

    Public Sub RegisterUser(ByVal username As String, ByVal plainPassword As String)
        If username = "" OrElse plainPassword = "" Then
            MsgBox("Please fill in both the username and password fields.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Try
            If con.State = ConnectionState.Closed Then con.Open()

            Dim hashedPassword As String = HashPassword(plainPassword)

            Using cmd As New Odbc.OdbcCommand("update accs set password=? where usernamee=?", con)
                cmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = hashedPassword
                cmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = username

                cmd.ExecuteNonQuery()
                MsgBox("Password updated sucessfully!", MsgBoxStyle.Information)

                frmLogIn.Show()
                Me.Hide()
            End Using

        Catch ex As Exception
            MsgBox("Error registering user: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub frmforgotpass_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
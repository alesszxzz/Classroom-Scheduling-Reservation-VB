Public Class frnSignUp

    Public Sub RegisterUser(ByVal username As String, ByVal plainPassword As String)
        Dim hashedPassword As String = HashPassword(plainPassword)
        Try
            connection()
            Dim query As String = "insert into accs (usernamee, password) values (?,?)"
            Using cmd As New Odbc.OdbcCommand(query, con)
                cmd.Parameters.Add("?", Odbc.OdbcType.Char).Value = username
                cmd.Parameters.Add("?", Odbc.OdbcType.Char).Value = hashedPassword

                cmd.ExecuteNonQuery()
                MsgBox("User registered successfully!", MsgBoxStyle.Information)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub btnlogin_Click(sender As Object, e As EventArgs) Handles btnlogin.Click
        If txtpass.Text.Trim <> TextBox1.Text.Trim Then
            MsgBox("Please match the password!")
        Else
            Call RegisterUser(txtusername.Text.Trim, txtpass.Text.Trim)
            frmLogIn.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        frmLogIn.Show()
        Me.Hide()
        txtpass.Clear()
        txtusername.Clear()
        TextBox1.Clear()
    End Sub

    Private Sub txtpass_TextChanged(sender As Object, e As EventArgs) Handles txtpass.TextChanged
        If txtpass.Text.Trim = "" Then
            eyeee.Visible = False
        Else
            eyeee.Visible = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.Trim = "" Then
            PictureBox4.Visible = False
        Else
            PictureBox4.Visible = True
        End If
    End Sub

    Private Sub eyeee_MouseClick(sender As Object, e As MouseEventArgs) Handles eyeee.MouseClick
        If txtpass.UseSystemPasswordChar = True Then
            txtpass.UseSystemPasswordChar = False
        Else
            txtpass.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If TextBox1.UseSystemPasswordChar = True Then
            TextBox1.UseSystemPasswordChar = False
        Else
            TextBox1.UseSystemPasswordChar = True
        End If
    End Sub
End Class
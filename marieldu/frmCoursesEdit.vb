Public Class frmCoursesEdit

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmd As Odbc.OdbcCommand
        Try
            cmd = New Odbc.OdbcCommand("update courses set course_code=?, course_name=? where course_id=" & frmCourses.dgvrooms.Tag, con)
            With cmd.Parameters
                .AddWithValue("?", Trim(txtcode.Text))
                .AddWithValue("?", Trim(txtname.Text))
            End With
            cmd.ExecuteNonQuery()
            Call loaddata("select * from courses", frmCourses.dgvrooms)
            txtcode.Clear()
            txtname.Clear()
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmCoursesEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()
    End Sub
End Class
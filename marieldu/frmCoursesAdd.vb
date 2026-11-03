Public Class frmCoursesAdd

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Dim cmd As Odbc.OdbcCommand
        Try
            cmd = New Odbc.OdbcCommand("insert into courses (course_code, course_name) values (?,?)", con)
            With cmd.Parameters
                .AddWithValue("?", Trim(txtcode.Text))
                .AddWithValue("?", Trim(txtname.Text))
            End With
            cmd.ExecuteNonQuery()
            txtcode.Clear()
            txtname.Clear()
            Call loaddata("select * from courses", frmCourses.dgvrooms)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
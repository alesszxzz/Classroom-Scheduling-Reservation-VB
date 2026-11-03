Public Class frmInstructorAdd

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim cmd As Odbc.OdbcCommand
        Try
            cmd = New Odbc.OdbcCommand("insert into teachers (titles, last_name, employment_type, course_id) values(?,?,?,?)", con)

            With cmd.Parameters
                .AddWithValue("?", Trim(txttitle.Text))
                .AddWithValue("?", Trim(txtlname.Text))
                .AddWithValue("?", cboemploymenttype.Text)
                .AddWithValue("?", cbodept.SelectedValue)
            End With
            cmd.ExecuteNonQuery()
            Call loaddata("select teachers.teacher_id, teachers.titles, teachers.last_name, teachers.employment_type, courses.course_code from teachers join courses on teachers.course_id = courses.course_id", frmInstructors.dgvrooms)
            Me.Hide()
            Call clearing()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub frmInstructorAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call connection()
        Call loadcombo("select course_id, course_code from courses", cbodept, "course_code", "course_id")
    End Sub

    Private Sub clearing()
        txttitle.Clear()
        txtlname.Clear()
        cboemploymenttype.SelectedIndex = -1
    End Sub

End Class
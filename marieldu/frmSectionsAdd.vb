Public Class frmSectionsAdd

    Private Sub frmSectionsAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call loadcombo("select course_id, course_code from courses", cmbcousre, "course_code", "course_id")
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Dim cmd As Odbc.OdbcCommand
        Try
            cmd = New Odbc.OdbcCommand("insert into sections (section_name, year_level, course_id) values (?,?,?)", con)
            With cmd.Parameters
                .AddWithValue("?", Trim(txtname.Text))
                .AddWithValue("?", cmbyearlevel.SelectedItem)
                .AddWithValue("?", cmbcousre.SelectedValue)
            End With
            cmd.ExecuteNonQuery()
            txtname.Clear()
            cmbcousre.SelectedIndex = -1
            cmbyearlevel.SelectedIndex = -1
            Call loaddata("select * from sections", frmSection.dgvrooms)
            Me.Hide()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
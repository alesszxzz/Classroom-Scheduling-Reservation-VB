Public Class frmSubjects

    Private Sub frmSubjects_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call connection()
        Call loadcombo("select course_id, course_code from courses", cbocourse, "course_code", "course_id")
        Call loaddata("select subjects.subject_id, subjects.subject_code, subjects.subject_title, subjects.units, subjects.year_level, subjects.semester, subjects.course_id, courses.course_code from subjects join courses on subjects.course_id = courses.course_id", dgvrooms)
    End Sub

    'Form Design
    Private Sub sidebar_MouseEnter(sender As Object, e As EventArgs) Handles sidebar.MouseEnter
        sidebar.Width = 176
    End Sub

    Private Sub sidebar_MouseLeave(sender As Object, e As EventArgs) Handles sidebar.MouseLeave
        sidebar.Width = 60
    End Sub

    Private Sub Panel2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles phome.MouseEnter
        sidebar.Width = 176
        phome.BackColor = Color.PeachPuff
    End Sub

    Private Sub Panel2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles phome.MouseLeave
        phome.BackColor = Color.Transparent
    End Sub

    Private Sub preservation_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles preservation.MouseEnter
        sidebar.Width = 176
        preservation.BackColor = Color.PeachPuff
    End Sub

    Private Sub preservation_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles preservation.MouseLeave
        preservation.BackColor = Color.Transparent
    End Sub

    Private Sub proom_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles proom.MouseEnter
        sidebar.Width = 176
        proom.BackColor = Color.PeachPuff
    End Sub

    Private Sub proom_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles proom.MouseLeave
        proom.BackColor = Color.Transparent
    End Sub

    Private Sub psubs_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles psubs.MouseEnter
        sidebar.Width = 176
    End Sub

    Private Sub pcourses_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles pcourses.MouseEnter
        sidebar.Width = 176
        pcourses.BackColor = Color.PeachPuff
    End Sub

    Private Sub pcourses_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pcourses.MouseLeave
        pcourses.BackColor = Color.Transparent
    End Sub

    Private Sub plogout_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles plogout.MouseEnter
        sidebar.Width = 176
        plogout.BackColor = Color.PeachPuff
    End Sub

    Private Sub plogout_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles plogout.MouseLeave
        plogout.BackColor = Color.Transparent
    End Sub

    Private Sub Panel1_MouseEnter1(ByVal sender As Object, ByVal e As System.EventArgs) Handles pinstructors.MouseEnter
        sidebar.Width = 176
        pinstructors.BackColor = Color.PeachPuff
    End Sub

    Private Sub Panel1_MouseLeave1(ByVal sender As Object, ByVal e As System.EventArgs) Handles pinstructors.MouseLeave
        pinstructors.BackColor = Color.Transparent
    End Sub
    Private Sub pnlsections_MouseEnter(sender As Object, e As EventArgs) Handles pnlsections.MouseEnter
        sidebar.Width = 176
        pnlsections.BackColor = Color.PeachPuff
    End Sub

    Private Sub pnlsections_MouseLeave(sender As Object, e As EventArgs) Handles pnlsections.MouseLeave
        pnlsections.BackColor = Color.Transparent
    End Sub

    Private Sub phome_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles phome.MouseClick
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub preservation_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles preservation.MouseClick
        Scheduling.Show()
        Me.Hide()
    End Sub

    Private Sub proom_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles proom.MouseClick
        frmRooms.Show()
        Me.Hide()
    End Sub

    Private Sub pinstructors_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pinstructors.MouseClick
        frmInstructors.Show()
        Me.Hide()
    End Sub

    Private Sub pcourses_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pcourses.MouseClick
        frmCourses.Show()
        Me.Hide()
    End Sub

    Private Sub plogout_MouseClick(sender As Object, e As MouseEventArgs) Handles plogout.MouseClick
        frmLogIn.Show()
        Me.Hide()
        frmLogIn.txtpass.Clear()
        frmLogIn.txtusername.Clear()
    End Sub


    'Functions
    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        dgvrooms.Tag = 0
        frmSubjectsAdd.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If dgvrooms.Tag <> 0 Then
            frmSubjectsEdit.ShowDialog()
        Else
            MsgBox("Please select a record!")
        End If
    End Sub

    Private Sub dgvrooms_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvrooms.CellClick
        If e.RowIndex >= 0 Then
            dgvrooms.Tag = dgvrooms.Item(0, e.RowIndex).Value
            frmSubjectsEdit.txtsucode.Text = dgvrooms.Item(1, e.RowIndex).Value
            frmSubjectsEdit.txttitle.Text = dgvrooms.Item(2, e.RowIndex).Value
            frmSubjectsEdit.txtunits.Text = dgvrooms.Item(3, e.RowIndex).Value
            frmSubjectsEdit.cmbyearlevel.SelectedIndex = dgvrooms.Item(4, e.RowIndex).Value - 1
            frmSubjectsEdit.cmbsemester.SelectedIndex = dgvrooms.Item(5, e.RowIndex).Value - 1
            frmSubjectsEdit.cmbcourse.SelectedIndex = dgvrooms.Item(6, e.RowIndex).Value - 1
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cmd As Odbc.OdbcCommand
        Dim results As DialogResult
        Try
            If dgvrooms.Tag <> 0 Then
                results = MessageBox.Show("Are you sure you want to delete this?", "Confirm", MessageBoxButtons.YesNo)
                If results = DialogResult.Yes Then
                    cmd = New Odbc.OdbcCommand("delete from subjects where subject_id=" & dgvrooms.Tag, con)
                    cmd.ExecuteNonQuery()
                    Call loaddata("select * from subjects", dgvrooms)
                    MsgBox("Deleted successfully!")
                    dgvrooms.Tag = 0
                Else
                    MsgBox("Cancelled")
                End If
            Else
                MsgBox("Please select a record you want to delete!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    
    Private Sub pnlsections_MouseClick(sender As Object, e As MouseEventArgs) Handles pnlsections.MouseClick
        frmSection.Show()
        Me.Close()
    End Sub

    Private Sub cbocourse_TextChanged(sender As Object, e As EventArgs) Handles cbocourse.TextChanged
        Call loaddata("select subjects.subject_id, subjects.subject_code, subjects.subject_title, subjects.units, subjects.year_level, subjects.semester, subjects.course_id, courses.course_code from subjects join courses on subjects.course_id = courses.course_id where course_code like '%" & cbocourse.Text.ToString & "%'", dgvrooms)
    End Sub

    Private Sub cbosem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbosem.SelectedIndexChanged
        Call loaddata("select subjects.subject_id, subjects.subject_code, subjects.subject_title, subjects.units, subjects.year_level, subjects.semester, subjects.course_id, courses.course_code from subjects join courses on subjects.course_id = courses.course_id where semester like '%" & cbosem.Text.ToString & "%'", dgvrooms)
    End Sub

    Private Sub cboyearl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboyearl.SelectedIndexChanged
        Call loaddata("select subjects.subject_id, subjects.subject_code, subjects.subject_title, subjects.units, subjects.year_level, subjects.semester, subjects.course_id, courses.course_code from subjects join courses on subjects.course_id = courses.course_id where year_level like '%" & cboyearl.Text.ToString & "%'", dgvrooms)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call loaddata("select subjects.subject_id, subjects.subject_code, subjects.subject_title, subjects.units, subjects.year_level, subjects.semester, subjects.course_id, courses.course_code from subjects join courses on subjects.course_id = courses.course_id where course_code like '%" & cbocourse.Text.ToString & "%' AND semester like '%" & cbosem.Text.ToString & "%' AND year_level like '%" & cboyearl.Text.ToString & "%'", dgvrooms)
    End Sub
End Class
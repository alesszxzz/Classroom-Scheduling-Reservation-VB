Public Class frmInstructors

    Private Sub frmInstructors_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call connection()
        Call loadcombo("select course_id, course_code from courses", cbosearch, "course_code", "course_id")
        Call loadcombo("select teacher_id, last_name from teachers", cbosearchh, "last_name", "teacher_id")
        Call loaddata("select teachers.teacher_id, teachers.titles, teachers.last_name, teachers.employment_type, courses.course_code, courses.course_id from teachers join courses on teachers.course_id = courses.course_id", dgvrooms)
        Call loaddata("SELECT teacher_availability.availability_id, teachers.last_name, teacher_availability.day_of_week, teacher_availability.start_time, teacher_availability.end_time FROM teacher_availability JOIN teachers ON teacher_availability.teacher_id = teachers.teacher_id", DataGridView1)
    End Sub

    'Form Design
    Private Sub Panel1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles sidebar.MouseEnter
        sidebar.Width = 176
    End Sub

    Private Sub Panel1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles sidebar.MouseLeave
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
        psubs.BackColor = Color.PeachPuff
    End Sub

    Private Sub psubs_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles psubs.MouseLeave
        psubs.BackColor = Color.Transparent
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
    End Sub
    Private Sub pnlsections_MouseEnter(sender As Object, e As EventArgs) Handles pnlsections.MouseEnter
        sidebar.Width = 176
        pnlsections.BackColor = Color.PeachPuff
    End Sub

    Private Sub pnlsections_MouseLeave(sender As Object, e As EventArgs) Handles pnlsections.MouseLeave
        pnlsections.BackColor = Color.Transparent
    End Sub


    'Navigation -=-=-==-=-==-=-=

    Private Sub plogout_MouseClick(sender As Object, e As MouseEventArgs) Handles plogout.MouseClick
        frmLogIn.Show()
        Me.Hide()
        frmLogIn.txtpass.Clear()
        frmLogIn.txtusername.Clear()
    End Sub
    Private Sub psubs_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles psubs.MouseClick
        frmSubjects.Show()
        Me.Hide()
    End Sub

    Private Sub phome_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles phome.MouseClick
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub proom_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles proom.MouseClick
        frmRooms.Show()
        Me.Hide()
    End Sub

    Private Sub preservation_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles preservation.MouseClick
        Scheduling.Show()
        Me.Hide()
    End Sub

    Private Sub pcourses_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pcourses.MouseClick
        frmCourses.Show()
        Me.Hide()
    End Sub
    Private Sub pnlsections_MouseClick(sender As Object, e As MouseEventArgs) Handles pnlsections.MouseClick
        frmSection.Show()
        Me.Hide()
    End Sub

    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        If dgvrooms.Tag <> 0 Then
            dgvrooms.Tag = 0
            frmInsAddChoice.ShowDialog()
        Else
            frmInsAddChoice.ShowDialog()
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If dgvrooms.Tag <> 0 Then
            frminstructorEdit.ShowDialog()
        Else
            MsgBox("Please select a record!")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim cmd As Odbc.OdbcCommand
        Dim results As DialogResult
        Try
            If Val(DataGridView1.Tag) <> 0 Then
                ' Delete Teacher Availability Record
                results = MessageBox.Show("Are you sure you want to delete this availability record?", "Confirm", MessageBoxButtons.YesNo)
                If results = DialogResult.Yes Then
                    If con.State = ConnectionState.Closed Then con.Open()

                    cmd = New Odbc.OdbcCommand("DELETE FROM teacher_availability WHERE availability_id=" & Val(DataGridView1.Tag), con)
                    cmd.ExecuteNonQuery()

                    Call loaddata("SELECT a.availability_id, t.last_name, a.day_of_week, a.start_time, a.end_time FROM teacher_availability a JOIN teachers t ON a.teacher_id = t.teacher_id", DataGridView1)
                    DataGridView1.Tag = 0
                    MsgBox("Availability deleted successfully!")
                Else
                    MsgBox("Cancelled")
                End If

            ElseIf Val(dgvrooms.Tag) <> 0 Then
                ' Delete Teacher Record
                results = MessageBox.Show("Are you sure you want to delete this teacher?", "Confirm", MessageBoxButtons.YesNo)
                If results = DialogResult.Yes Then
                    If con.State = ConnectionState.Closed Then con.Open()

                    cmd = New Odbc.OdbcCommand("DELETE FROM teachers WHERE teacher_id=" & Val(dgvrooms.Tag), con)
                    cmd.ExecuteNonQuery()

                    Call loaddata("SELECT * FROM teachers", dgvrooms)
                    dgvrooms.Tag = 0
                    MsgBox("Teacher deleted successfully!")
                Else
                    MsgBox("Cancelled")
                End If

            Else
                MsgBox("Please select a record you want to delete!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            DataGridView1.Tag = DataGridView1.Item(0, e.RowIndex).Value
            MsgBox(DataGridView1.Tag)
        End If
    End Sub

    Private Sub dgvrooms_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvrooms.CellClick
        Try
            If e.RowIndex >= 0 Then
                dgvrooms.Tag = dgvrooms.Item(0, e.RowIndex).Value
                frminstructorEdit.txttitles.Text = dgvrooms.Item(1, e.RowIndex).Value
                frminstructorEdit.txtlname.Text = dgvrooms.Item(2, e.RowIndex).Value
                frminstructorEdit.cboemploymenttype.SelectedItem = dgvrooms.Item(3, e.RowIndex).Value
                frminstructorEdit.cbodept.SelectedIndex = dgvrooms.Item(5, e.RowIndex).Value - 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub

    Private Sub cbosearch_TextChanged(sender As Object, e As EventArgs) Handles cbosearch.TextChanged
        If cbosearch.Text <> "" Then
            Call loaddata("select teachers.teacher_id, teachers.titles, teachers.last_name, teachers.employment_type, courses.course_code, courses.course_id from teachers join courses on teachers.course_id = courses.course_id where course_code like '%" & cbosearch.Text.ToString & "%'", dgvrooms)
        Else
            Call loaddata("select teachers.teacher_id, teachers.titles, teachers.last_name, teachers.employment_type, courses.course_code, courses.course_id from teachers join courses on teachers.course_id = courses.course_id", dgvrooms)
        End If
    End Sub

    Private Sub cbosearchh_TextChanged(sender As Object, e As EventArgs) Handles cbosearchh.TextChanged
        If cbosearchh.Text <> "" Then
            Call loaddata("SELECT teacher_availability.availability_id, teachers.last_name, teacher_availability.day_of_week, teacher_availability.start_time, teacher_availability.end_time FROM teacher_availability JOIN teachers ON teacher_availability.teacher_id = teachers.teacher_id where teachers.last_name like '%" & cbosearchh.Text.ToString & "%'", DataGridView1)
        Else
            Call loaddata("SELECT teacher_availability.availability_id, teachers.last_name, teacher_availability.day_of_week, teacher_availability.start_time, teacher_availability.end_time FROM teacher_availability JOIN teachers ON teacher_availability.teacher_id = teachers.teacher_id", DataGridView1)
        End If
    End Sub
End Class
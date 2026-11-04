Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call connection()
        Dim query As String = "SELECT rooms.room_number FROM reservations JOIN rooms ON reservations.room_id = rooms.room_id group by rooms.room_number order by count(reservations.room_id) desc limit 1"

        Using cmd As New Odbc.OdbcCommand(query, con)
            Try
                Dim result = cmd.ExecuteScalar()

                If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                    lblmostbook.Text = result.ToString()
                Else
                    lblmostbook.Text = "No Bookings"
                End If
            Catch ex As Odbc.OdbcException
                MessageBox.Show("ODBC Database Error: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using




        Scheduling.PopulateFilterComboBox(cboSection, "SELECT DISTINCT section_name FROM sections ORDER BY section_name", "section_name")
        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Accepted%'", dgvAccepted)
        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Pending%'", dgvrejected)
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
        phome.BackColor = Color.Beige
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

    'Navigation
    Private Sub preservation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles preservation.Click
        Scheduling.Show()
        Me.Hide()
    End Sub

    Private Sub proom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles proom.Click
        frmRooms.Show()
        Me.Hide()
    End Sub

    Private Sub pinstructors_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pinstructors.MouseClick
        frmInstructors.Show()
        Me.Hide()
    End Sub

    Private Sub psubs_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles psubs.MouseClick
        frmSubjects.Show()
        Me.Hide()
    End Sub

    Private Sub pcourses_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pcourses.MouseClick
        frmCourses.Show()
        Me.Hide()
    End Sub

    Private Sub Panel3_MouseClick(sender As Object, e As MouseEventArgs) Handles pnlsections.MouseClick
        frmSection.Show()
        Me.Hide()
    End Sub

    Private Sub plogout_MouseClick(sender As Object, e As MouseEventArgs) Handles plogout.MouseClick
        frmLogIn.Show()
        Me.Hide()

        frmLogIn.txtpass.Clear()
        frmLogIn.txtusername.Clear()
    End Sub

    Private Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        Dim query As String = "SELECT s.schedule_id AS 'ID', sec.section_name AS 'Section', " &
                              "sub.subject_title AS 'Subject', CONCAT(t.titles, ' ', t.last_name) AS 'Teacher', " &
                              "r.room_number AS 'Room', s.day_of_week AS 'Day of Week', " &
                              "TIME_FORMAT(s.start_time, '%h:%i %p') AS 'Start Time', " &
                              "TIME_FORMAT(s.end_time, '%h:%i %p') AS 'End Time', " &
                              "s.academic_year AS 'Academic Year' " &
                              "FROM schedules s " &
                              "INNER JOIN sections sec ON s.section_id = sec.section_id " &
                              "INNER JOIN subjects sub ON s.subject_id = sub.subject_id " &
                              "INNER JOIN teachers t ON s.teacher_id = t.teacher_id " &
                              "INNER JOIN rooms r ON s.room_id = r.room_id WHERE 1=1"
        If cboSection.SelectedIndex > 0 Then
            query &= " AND sec.section_name = '" & cboSection.Text.Replace("'", "''") & "'"
        End If
        Call loaddata(query, dgvrooms)
    End Sub

End Class

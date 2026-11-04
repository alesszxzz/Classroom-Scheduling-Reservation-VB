Public Class frmSchedulesUsers


    Private Sub frmSchedulesUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()
        Scheduling.PopulateFilterComboBox(cboRoom, "SELECT DISTINCT room_number FROM rooms ORDER BY room_number", "room_number")
        Call loaddata("SELECT s.schedule_id AS 'ID', sec.section_name AS 'Section', sub.subject_title AS 'Subject', CONCAT(t.titles, ' ', t.last_name) AS 'Teacher', r.room_number AS 'Room', s.day_of_week AS 'Day of Week', TIME_FORMAT(s.start_time, '%h:%i %p') AS 'Start Time', TIME_FORMAT(s.end_time, '%h:%i %p') AS 'End Time', s.academic_year AS 'Academic Year' FROM schedules s INNER JOIN sections sec ON s.section_id = sec.section_id INNER JOIN subjects sub ON s.subject_id = sub.subject_id INNER JOIN teachers t ON s.teacher_id = t.teacher_id INNER JOIN rooms r ON s.room_id = r.room_id ORDER BY s.schedule_id DESC", dgvrooms)
    End Sub

    Private Sub cboRoom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRoom.SelectedIndexChanged
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

        If cboRoom.SelectedIndex > 0 Then
            query &= " AND r.room_number = '" & cboRoom.Text.Replace("'", "''") & "'"
        End If
        Call loaddata(query, dgvrooms)
    End Sub
End Class
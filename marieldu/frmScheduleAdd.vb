Public Class frmScheduleAdd

    Private Sub frmScheduleAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()

        cboAcademicYear.Items.Clear()
        cboAcademicYear.Items.Add("2026-2027")
        cboAcademicYear.Items.Add("2025-2026")
        cboAcademicYear.SelectedIndex = 0

        cboDay.Items.Clear()
        cboDay.Items.AddRange(New Object() {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        cboDay.SelectedIndex = 0

        cboStartTime.Items.Clear()
        cboEndTime.Items.Clear()
        Dim t As DateTime = DateTime.Today.AddHours(7).AddMinutes(30)
        While t <= DateTime.Today.AddHours(21)
            cboStartTime.Items.Add(t.ToString("hh:mm tt"))
            cboEndTime.Items.Add(t.ToString("hh:mm tt"))
            t = t.AddMinutes(30)
        End While

        PopulateComboWithID(cboSection, "SELECT section_id, section_name FROM sections ORDER BY section_name", "section_name", "section_id")

        PopulateComboWithID(cboSubject, "SELECT subject_id, CONCAT(subject_code, ' - ', subject_title) AS display_name FROM subjects ORDER BY subject_code", "display_name", "subject_id")

        PopulateComboWithID(cboTeacher, "SELECT teacher_id, CONCAT(titles, ' ', last_name) AS teacher_name FROM teachers ORDER BY last_name", "teacher_name", "teacher_id")

        PopulateComboWithID(cboRoom, "SELECT room_id, room_number FROM rooms ORDER BY room_number", "room_number", "room_id")

        cboAcademicYear.SelectedIndex = -1
        cboSection.SelectedIndex = -1
        cboSubject.SelectedIndex = -1
        cboTeacher.SelectedIndex = -1
        cboRoom.SelectedIndex = -1
        cboDay.SelectedIndex = -1
        cboStartTime.SelectedIndex = -1
        cboEndTime.SelectedIndex = -1
    End Sub

    Private Sub PopulateComboWithID(cbo As ComboBox, query As String, displayCol As String, idCol As String)
        cbo.Items.Clear()
        If con.State = ConnectionState.Closed Then con.Open()
        Dim cmd As New Odbc.OdbcCommand(query, con)
        Dim dr As Odbc.OdbcDataReader = cmd.ExecuteReader()

        While dr.Read()
            cbo.Items.Add(New frminstructorEdit.ComboboxItem(dr(displayCol).ToString(), Val(dr(idCol))))
        End While

        dr.Close()
        con.Close()
        If cbo.Items.Count > 0 Then cbo.SelectedIndex = 0
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Dim startTime24 As String = DateTime.Parse(cboStartTime.SelectedItem.ToString()).ToString("HH:mm:ss")
        Dim endTime24 As String = DateTime.Parse(cboEndTime.SelectedItem.ToString()).ToString("HH:mm:ss")

        If TimeSpan.Parse(startTime24) >= TimeSpan.Parse(endTime24) Then
            MsgBox("End time must be later than start time!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim sectionId As Integer = Val(CType(cboSection.SelectedItem, frminstructorEdit.ComboboxItem).Value)
        Dim subjectId As Integer = Val(CType(cboSubject.SelectedItem, frminstructorEdit.ComboboxItem).Value)
        Dim teacherId As Integer = Val(CType(cboTeacher.SelectedItem, frminstructorEdit.ComboboxItem).Value)
        Dim roomId As Integer = Val(CType(cboRoom.SelectedItem, frminstructorEdit.ComboboxItem).Value)
        Dim dayOfWeek As String = cboDay.SelectedItem.ToString()
        Dim academicYear As String = cboAcademicYear.SelectedItem.ToString()

        ' --- RUN CONFLICT CHECK ---
        Dim conflictMessage As String = CheckScheduleConflicts(roomId, teacherId, sectionId, dayOfWeek, startTime24, endTime24, academicYear)

        If conflictMessage <> "" Then
            MsgBox(conflictMessage, MsgBoxStyle.Critical, "Scheduling Conflict Error")
            Exit Sub ' Stop saving process
        End If

        ' Proceed with INSERT query if conflictMessage is empty...
        Dim query As String = "INSERT INTO schedules (section_id, subject_id, teacher_id, room_id, day_of_week, start_time, end_time, academic_year) VALUES (?, ?, ?, ?, ?, ?, ?, ?)"

        Using cmd As New Odbc.OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("?", sectionId)
            cmd.Parameters.AddWithValue("?", subjectId)
            cmd.Parameters.AddWithValue("?", teacherId)
            cmd.Parameters.AddWithValue("?", roomId)
            cmd.Parameters.AddWithValue("?", dayOfWeek)
            cmd.Parameters.AddWithValue("?", startTime24)
            cmd.Parameters.AddWithValue("?", endTime24)
            cmd.Parameters.AddWithValue("?", academicYear)

            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        MsgBox("Schedule successfully saved with no conflicts!", MsgBoxStyle.Information)
        Scheduling.LoadScheduleData()
        Me.Hide()
    End Sub

    Private Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        If cboSection.SelectedItem IsNot Nothing Then
            ' 1. Get selected section_id
            Dim selectedSection As frminstructorEdit.ComboboxItem = CType(cboSection.SelectedItem, frminstructorEdit.ComboboxItem)
            Dim sectionId As Integer = Val(selectedSection.Value)

            Dim subjectQuery As String = "SELECT s.subject_id, CONCAT(s.subject_code, ' - ', s.subject_title) AS display_name " &
                                         "FROM subjects s " &
                                         "INNER JOIN sections sec ON s.course_id = sec.course_id " &
                                         "WHERE sec.section_id = " & sectionId & " " &
                                         "ORDER BY s.subject_code"

            PopulateComboWithID(cboSubject, subjectQuery, "display_name", "subject_id")

            Dim teacherQuery As String = "SELECT DISTINCT t.teacher_id, CONCAT(t.titles, ' ', t.last_name) AS teacher_name " &
                                         "FROM teachers t " &
                                         "INNER JOIN sections sec ON (t.course_id = sec.course_id OR t.course_id IS NULL) " &
                                         "WHERE sec.section_id = " & sectionId & " " &
                                         "ORDER BY t.last_name"

            PopulateComboWithID(cboTeacher, teacherQuery, "teacher_name", "teacher_id")
        End If
    End Sub
End Class
Public Class frmScheduleEdit

    Dim isLoading As Boolean = False

    Private Sub frmScheduleEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isLoading = True

        Call connection()

        cboAcademicYear.Items.Clear()
        cboAcademicYear.Items.AddRange(New Object() {"2026-2027", "2025-2026"})

        cboDay.Items.Clear()
        cboDay.Items.AddRange(New Object() {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})

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

        Dim scheduleId As Integer = Val(Me.Tag)
        If scheduleId > 0 Then
            LoadExistingScheduleData(scheduleId)
        End If

        isLoading = False
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

    Private Sub LoadExistingScheduleData(ByVal scheduleId As Integer)
        If con.State = ConnectionState.Closed Then con.Open()

        ' TIME_FORMAT forces 08:00 AM format matching your ComboBox items exactly
        Dim query As String = "SELECT section_id, subject_id, teacher_id, room_id, day_of_week, " &
                              "TIME_FORMAT(start_time, '%h:%i %p') AS start_formatted, " &
                              "TIME_FORMAT(end_time, '%h:%i %p') AS end_formatted, " &
                              "academic_year FROM schedules WHERE schedule_id = " & scheduleId

        Dim cmd As New Odbc.OdbcCommand(query, con)
        Dim dr As Odbc.OdbcDataReader = cmd.ExecuteReader()

        If dr.Read() Then
            ' Foreign Keys
            SelectComboByValue(cboSection, Val(dr("section_id")))
            SelectComboByValue(cboSubject, Val(dr("subject_id")))
            SelectComboByValue(cboTeacher, Val(dr("teacher_id")))
            SelectComboByValue(cboRoom, Val(dr("room_id")))

            ' Strings / Dropdowns
            cboAcademicYear.Text = dr("academic_year").ToString().Trim()
            cboDay.Text = dr("day_of_week").ToString().Trim()
            cboStartTime.Text = dr("start_formatted").ToString().Trim()
            cboEndTime.Text = dr("end_formatted").ToString().Trim()
        End If

        dr.Close()
        con.Close()
    End Sub

    ' Helper Subroutine to select ComboboxItem by numeric ID value
    Private Sub SelectComboByValue(ByVal cbo As ComboBox, ByVal targetId As Integer)
        For Each item As Object In cbo.Items
            If TypeOf item Is frminstructorEdit.ComboboxItem Then
                Dim cboItem As frminstructorEdit.ComboboxItem = CType(item, frminstructorEdit.ComboboxItem)
                If Val(cboItem.Value) = targetId Then
                    cbo.SelectedItem = cboItem
                    Exit For
                End If
            End If
        Next
    End Sub
    Private Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged
        If isLoading Then Exit Sub
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
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
        Dim query As String = "update schedules set section_id=?, subject_id=?, teacher_id=?, room_id=?, day_of_week=?, start_time=?, end_time=?, academic_year=? where reservation_id =" & Val(Scheduling.dgvrooms.Tag)

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
            cboAcademicYear.SelectedIndex = -1
            cboSection.SelectedIndex = -1
            cboSubject.SelectedIndex = -1
            cboTeacher.SelectedIndex = -1
            cboRoom.SelectedIndex = -1
            cboDay.SelectedIndex = -1
            cboStartTime.SelectedIndex = -1
            cboEndTime.SelectedIndex = -1
        End Using

        MsgBox("Schedule successfully saved with no conflicts!", MsgBoxStyle.Information)
        Scheduling.LoadScheduleData()
        Me.Hide()
        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Approved%'", Form1.dgvAccepted)
        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Pending%'", Form1.dgvpendings)
    End Sub
End Class
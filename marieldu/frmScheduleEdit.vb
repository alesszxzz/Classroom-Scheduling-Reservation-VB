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
End Class
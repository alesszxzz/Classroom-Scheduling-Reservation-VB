Imports System.Data.Odbc
Imports System.Security.Cryptography
Imports System.Text

Module modcon
    Public con As OdbcConnection
    Public currentUserRole As String = ""
    Public Sub connection()
        Try
            ' Ensure connection is open
            If con Is Nothing OrElse con.State = ConnectionState.Closed Then
                con = New OdbcConnection("dsn=prac")
                con.Open()
            End If
        Catch ex As Exception
            MsgBox("Connection Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' SHA-256 Hashing Helper Function
    Public Function HashPassword(ByVal plainText As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(plainText)
            Dim hashBytes As Byte() = sha256.ComputeHash(bytes)

            Dim builder As New StringBuilder()
            For Each b As Byte In hashBytes
                builder.Append(b.ToString("x2"))
            Next
            Return builder.ToString()
        End Using
    End Function

    Public Sub loaddata(ByVal sql As String, ByVal dgv As DataGridView)
        Try
            connection()
            Dim dt As New DataTable
            Dim da As New OdbcDataAdapter(sql, con)

            da.Fill(dt)
            dgv.DataSource = dt
            da.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub loadcombo(ByVal sql As String, ByVal cmb As ComboBox, ByVal displayMember As String, ByVal valueMember As String)
        Try
            connection()
            Dim dt As New DataTable
            Dim da As New OdbcDataAdapter(sql, con)

            da.Fill(dt)

            cmb.DataSource = dt
            cmb.DisplayMember = displayMember
            cmb.ValueMember = valueMember

            cmb.SelectedIndex = -1
            da.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function CheckScheduleConflicts(ByVal roomId As Integer,
                                       ByVal teacherId As Integer,
                                       ByVal sectionId As Integer,
                                       ByVal dayOfWeek As String,
                                       ByVal startTime24 As String,
                                       ByVal endTime24 As String,
                                       ByVal academicYear As String,
                                       Optional ByVal currentScheduleId As Integer = 0) As String

        If con.State = ConnectionState.Closed Then con.Open()

        ' 1. Check Room Overlap Conflict
        Dim roomQuery As String = "SELECT r.room_number FROM schedules s " &
                                  "INNER JOIN rooms r ON s.room_id = r.room_id " &
                                  "WHERE s.room_id = ? AND s.day_of_week = ? AND s.academic_year = ? " &
                                  "AND s.schedule_id <> ? " &
                                  "AND (? < s.end_time AND ? > s.start_time)"

        Using cmd As New Odbc.OdbcCommand(roomQuery, con)
            cmd.Parameters.AddWithValue("?", roomId)
            cmd.Parameters.AddWithValue("?", dayOfWeek)
            cmd.Parameters.AddWithValue("?", academicYear)
            cmd.Parameters.AddWithValue("?", currentScheduleId)
            cmd.Parameters.AddWithValue("?", startTime24)
            cmd.Parameters.AddWithValue("?", endTime24)

            Dim roomConflict As Object = cmd.ExecuteScalar()
            If roomConflict IsNot Nothing AndAlso Not IsDBNull(roomConflict) Then
                con.Close()
                Return "Room Conflict: Room " & roomConflict.ToString() & " is already booked for " & academicYear & " during this time slot!"
            End If
        End Using

        ' 2. Check Teacher Overlap Conflict
        Dim teacherQuery As String = "SELECT CONCAT(t.titles, ' ', t.last_name) FROM schedules s " &
                                     "INNER JOIN teachers t ON s.teacher_id = t.teacher_id " &
                                     "WHERE s.teacher_id = ? AND s.day_of_week = ? AND s.academic_year = ? " &
                                     "AND s.schedule_id <> ? " &
                                     "AND (? < s.end_time AND ? > s.start_time)"

        Using cmd As New Odbc.OdbcCommand(teacherQuery, con)
            cmd.Parameters.AddWithValue("?", teacherId)
            cmd.Parameters.AddWithValue("?", dayOfWeek)
            cmd.Parameters.AddWithValue("?", academicYear)
            cmd.Parameters.AddWithValue("?", currentScheduleId)
            cmd.Parameters.AddWithValue("?", startTime24)
            cmd.Parameters.AddWithValue("?", endTime24)

            Dim teacherConflict As Object = cmd.ExecuteScalar()
            If teacherConflict IsNot Nothing AndAlso Not IsDBNull(teacherConflict) Then
                con.Close()
                Return "Teacher Conflict: " & teacherConflict.ToString() & " is already teaching another section in " & academicYear & " during this time!"
            End If
        End Using

        ' 3. Check Section Overlap Conflict
        Dim sectionQuery As String = "SELECT sec.section_name FROM schedules s " &
                                     "INNER JOIN sections sec ON s.section_id = sec.section_id " &
                                     "WHERE s.section_id = ? AND s.day_of_week = ? AND s.academic_year = ? " &
                                     "AND s.schedule_id <> ? " &
                                     "AND (? < s.end_time AND ? > s.start_time)"

        Using cmd As New Odbc.OdbcCommand(sectionQuery, con)
            cmd.Parameters.AddWithValue("?", sectionId)
            cmd.Parameters.AddWithValue("?", dayOfWeek)
            cmd.Parameters.AddWithValue("?", academicYear)
            cmd.Parameters.AddWithValue("?", currentScheduleId)
            cmd.Parameters.AddWithValue("?", startTime24)
            cmd.Parameters.AddWithValue("?", endTime24)

            Dim sectionConflict As Object = cmd.ExecuteScalar()
            If sectionConflict IsNot Nothing AndAlso Not IsDBNull(sectionConflict) Then
                con.Close()
                Return "Section Conflict: " & sectionConflict.ToString() & " already has a class scheduled in " & academicYear & " during this time!"
            End If
        End Using

        ' 4. Check Part-Time Teacher Availability Window
        Dim empQuery As String = "SELECT employment_type FROM teachers WHERE teacher_id = ?"
        Dim empType As String = ""

        Using cmd As New Odbc.OdbcCommand(empQuery, con)
            cmd.Parameters.AddWithValue("?", teacherId)
            empType = Convert.ToString(cmd.ExecuteScalar())
        End Using

        If empType = "Part-Time" Then
            Dim availQuery As String = "SELECT COUNT(*) FROM teacher_availability " &
                                       "WHERE teacher_id = ? AND day_of_week = ? " &
                                       "AND start_time <= ? AND end_time >= ?"

            Using cmd As New Odbc.OdbcCommand(availQuery, con)
                cmd.Parameters.AddWithValue("?", teacherId)
                cmd.Parameters.AddWithValue("?", dayOfWeek)
                cmd.Parameters.AddWithValue("?", startTime24)
                cmd.Parameters.AddWithValue("?", endTime24)

                Dim isAvailable As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                If isAvailable = 0 Then
                    con.Close()
                    Return "Availability Conflict: This part-time instructor is not available on " & dayOfWeek & " from " & startTime24 & " to " & endTime24 & "!"
                End If
            End Using
        End If

        con.Close()
        Return ""
    End Function
End Module
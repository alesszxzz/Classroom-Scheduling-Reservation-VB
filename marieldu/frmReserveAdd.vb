Public Class frmReserveAdd

    Public Function CheckReservationConflict(ByVal roomId As Integer, ByVal reserveDate As Date, ByVal startTime24 As String, ByVal endTime24 As String) As String
        If con.State = ConnectionState.Closed Then con.Open()

        ' Convert Date to Day of Week (e.g., 'Monday')
        Dim dayOfWeek As String = reserveDate.ToString("dddd")
        Dim formattedDate As String = reserveDate.ToString("yyyy-MM-dd")

        ' 1. Check against regular weekly class schedules
        Dim scheduleQuery As String = "SELECT r.room_number FROM schedules s " &
                                      "INNER JOIN rooms r ON s.room_id = r.room_id " &
                                      "WHERE s.room_id = " & roomId & " AND s.day_of_week = '" & dayOfWeek & "' " &
                                      "AND ('" & startTime24 & "' < s.end_time AND '" & endTime24 & "' > s.start_time)"

        Dim cmd As New Odbc.OdbcCommand(scheduleQuery, con)
        Dim classConflict As Object = cmd.ExecuteScalar()

        If classConflict IsNot Nothing AndAlso Not IsDBNull(classConflict) Then
            con.Close()
            Return "Conflict with Regular Class: Room " & classConflict.ToString() & " has an active class scheduled on " & dayOfWeek & "s during this time!"
        End If

        ' 2. Check against other approved special reservations
        Dim reservationQuery As String = "SELECT r.room_number FROM reservations res " &
                                        "INNER JOIN rooms r ON res.room_id = r.room_id " &
                                        "WHERE res.room_id = " & roomId & " AND res.reservation_date = '" & formattedDate & "' " &
                                        "AND res.status = 'Approved' " &
                                        "AND ('" & startTime24 & "' < res.end_time AND '" & endTime24 & "' > res.start_time)"

        cmd.CommandText = reservationQuery
        Dim reserveConflict As Object = cmd.ExecuteScalar()

        If reserveConflict IsNot Nothing AndAlso Not IsDBNull(reserveConflict) Then
            con.Close()
            Return "Conflict with Existing Reservation: Room " & reserveConflict.ToString() & " is already reserved on " & formattedDate & "!"
        End If

        con.Close()
        Return "" ' No conflicts found
    End Function

    Private Sub frmReserveAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()

        ' 1. Load Rooms into ComboBox
        PopulateComboWithID(cboRoom, "SELECT room_id, room_number FROM rooms ORDER BY room_number", "room_number", "room_id")

        ' 2. Populate Time Slots
        cboStartTime.Items.Clear()
        cboEndTime.Items.Clear()
        Dim t As DateTime = DateTime.Today.AddHours(7).AddMinutes(30)
        While t <= DateTime.Today.AddHours(21)
            cboStartTime.Items.Add(t.ToString("hh:mm tt"))
            cboEndTime.Items.Add(t.ToString("hh:mm tt"))
            t = t.AddMinutes(30)
        End While

        dtpDate.Value = DateTime.Today
    End Sub

    ' Save Reservation Button
    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        ' Validation
        If cboRoom.SelectedItem Is Nothing OrElse cboStartTIme.SelectedItem Is Nothing OrElse
           cboEndTime.SelectedItem Is Nothing OrElse txtReservedBy.Text.Trim() = "" Then
            MsgBox("Please fill in all required fields!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim roomId As Integer = Val(CType(cboRoom.SelectedItem, frminstructorEdit.ComboboxItem).Value)
        Dim reserveDate As String = dtpDate.Value.ToString("yyyy-MM-dd")
        Dim startTime24 As String = DateTime.Parse(cboStartTIme.SelectedItem.ToString()).ToString("HH:mm:ss")
        Dim endTime24 As String = DateTime.Parse(cboEndTime.SelectedItem.ToString()).ToString("HH:mm:ss")

        If TimeSpan.Parse(startTime24) >= TimeSpan.Parse(endTime24) Then
            MsgBox("End time must be later than start time!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' --- CHECK CONFLICTS WITH BOTH REGULAR SCHEDULES AND OTHER RESERVATIONS ---
        Dim conflictMsg As String = CheckReservationConflict(roomId, dtpDate.Value, startTime24, endTime24)
        If conflictMsg <> "" Then
            MsgBox(conflictMsg, MsgBoxStyle.Critical, "Reservation Conflict")
            Exit Sub
        End If

        Dim query As String = "INSERT INTO reservations (room_id, reserved_by, purpose, reservation_date, start_time, end_time, status) " &
                              "VALUES (?, ?, ?, ?, ?, ?, 'Pending')"

        Using cmd As New Odbc.OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("?", roomId)
            cmd.Parameters.AddWithValue("?", txtReservedBy.Text.Trim())
            cmd.Parameters.AddWithValue("?", txtPurpose.Text.Trim())
            cmd.Parameters.AddWithValue("?", reserveDate)
            cmd.Parameters.AddWithValue("?", startTime24)
            cmd.Parameters.AddWithValue("?", endTime24)

            If con.State = ConnectionState.Closed Then con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using

        MsgBox("Reservation request submitted! Waiting for admin approval.", MsgBoxStyle.Information)
        frmReserve.LoadReservations()
        Me.Hide()

        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Approved%'", Form1.dgvAccepted)
        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Pending%'", Form1.dgvpendings)

        Call connection()
        Dim query1 As String = "SELECT rooms.room_number FROM reservations JOIN rooms ON reservations.room_id = rooms.room_id group by rooms.room_number order by count(reservations.room_id) desc limit 1"

        Using cmd As New Odbc.OdbcCommand(query1, con)
            Try
                Dim result = cmd.ExecuteScalar()

                If result IsNot DBNull.Value AndAlso result IsNot Nothing Then
                    Form1.lblmostbook.Text = result.ToString()
                Else
                    Form1.lblmostbook.Text = "No Bookings"
                End If
            Catch ex As Odbc.OdbcException
                MessageBox.Show("ODBC Database Error: " & ex.Message)
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    ' Helper to populate combo box with ID
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

End Class
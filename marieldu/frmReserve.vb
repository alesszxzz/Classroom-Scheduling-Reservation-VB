Public Class frmReserve

    Private Sub frmReserve_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()
        LoadReservations()

        Dim isAdmin As Boolean = String.Equals(currentUserRole, "Admin", StringComparison.OrdinalIgnoreCase)

        btnApproved.Visible = isAdmin
        btnReject.Visible = isAdmin
        btnBack.Visible = isAdmin
        btnLogOut.Visible = Not isAdmin
        btnscheddd.Visible = Not isAdmin
    End Sub

    Public Sub LoadReservations()
        If con.State = ConnectionState.Closed Then con.Open()
        Dim autoCompleteQuery As String = "UPDATE reservations " &
                                           "SET status = 'Completed' " &
                                           "WHERE status = 'Approved' " &
                                           "AND (" &
                                           "    reservation_date < CURRENT_DATE() " &
                                           "    OR (reservation_date = CURRENT_DATE() AND end_time <= CURRENT_TIME())" &
                                           ")"

        Try
            Using cmdUpdate As New Odbc.OdbcCommand(autoCompleteQuery, con)
                cmdUpdate.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim autoRejectedQuery As String = "update reservations set status = 'Rejected' where status = 'Pending' and (reservation_date < CURRENT_DATE()) OR (reservation_date = CURRENT_DATE() AND end_time <= CURRENT_TIME())"
        Try
            Using cmdUpdatee As New Odbc.OdbcCommand(autoRejectedQuery, con)
                cmdUpdatee.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim query As String = "SELECT r.reservation_id AS 'ID', rm.room_number AS 'Room', " &
                              "DATE_FORMAT(r.reservation_date, '%Y-%m-%d') AS 'Date', " &
                              "CONCAT(TIME_FORMAT(r.start_time, '%h:%i %p'), ' - ', TIME_FORMAT(r.end_time, '%h:%i %p')) AS 'Time Block', " &
                              "r.reserved_by AS 'Reserved By', r.purpose AS 'Purpose', r.status AS 'Status' " &
                              "FROM reservations r " &
                              "INNER JOIN rooms rm ON r.room_id = rm.room_id " &
                              "ORDER BY r.reservation_date DESC, r.start_time ASC"

        Call loaddata(query, dgvReservations)
    End Sub

    ' Open Add Reservation Popup c
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        frmReserveAdd.ShowDialog()
    End Sub
    Private Sub dgvReservations_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReservations.CellClick
        If e.RowIndex >= 0 Then
            dgvReservations.Tag = dgvReservations.Rows(e.RowIndex).Cells(0).Value
        End If
    End Sub

    ' Approve Button Click
    Private Sub btnApproved_Click(sender As Object, e As EventArgs) Handles btnApproved.Click
        If Val(dgvReservations.Tag) = 0 Then
            MsgBox("Please select a reservation request from the table first!", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim resId As Integer = Val(dgvReservations.Tag)

        If con.State = ConnectionState.Closed Then con.Open()
        Dim cmd As New Odbc.OdbcCommand("UPDATE reservations SET status = 'Approved' WHERE reservation_id = " & resId, con)
        cmd.ExecuteNonQuery()
        con.Close()

        MsgBox("Reservation approved successfully!", MsgBoxStyle.Information)
        LoadReservations()
        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Approved%'", Form1.dgvAccepted)
        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Pending%'", Form1.dgvpendings)
    End Sub

    ' Reject Button Click
    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        If Val(dgvReservations.Tag) = 0 Then
            MsgBox("Please select a reservation request from the table first!", MsgBoxStyle.Information)
            Exit Sub
        End If

        Dim resId As Integer = Val(dgvReservations.Tag)

        If con.State = ConnectionState.Closed Then con.Open()
        Dim cmd As New Odbc.OdbcCommand("UPDATE reservations SET status = 'Rejected' WHERE reservation_id = " & resId, con)
        cmd.ExecuteNonQuery()
        con.Close()

        MsgBox("Reservation rejected.", MsgBoxStyle.Information)
        LoadReservations()
        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Approved%'", Form1.dgvAccepted)
        Call loaddata("select rooms.room_number, reservations.reserved_by, reservations.purpose, reservations.reservation_date, reservations.status from reservations join rooms on reservations.room_id = rooms.room_id where status like '%Pending%'", Form1.dgvpendings)
    End Sub

    Private Sub btnLogOut_Click(sender As Object, e As EventArgs) Handles btnLogOut.Click
        frmLogIn.Show()
        Me.Hide()
        frmLogIn.txtusername.Clear()
        frmLogIn.txtpass.Clear()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmSchedulesUsers.Show()
    End Sub

    Private Sub dgvReservations_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvReservations.CellFormatting
        ' Check if we are formatting the 'status' column
        If dgvReservations.Columns(e.ColumnIndex).Name.Equals("status", StringComparison.OrdinalIgnoreCase) Then
            If e.Value IsNot Nothing AndAlso Not IsDBNull(e.Value) Then
                Dim statusValue As String = e.Value.ToString().Trim()

                ' Option A: Highlight just the Status Cell
                Select Case statusValue
                    Case "Approved"
                        e.CellStyle.BackColor = Color.LightGreen
                        e.CellStyle.ForeColor = Color.DarkGreen
                    Case "Pending"
                        e.CellStyle.BackColor = Color.LightGoldenrodYellow
                        e.CellStyle.ForeColor = Color.DarkOrange
                    Case "Rejected"
                        e.CellStyle.BackColor = Color.MistyRose
                        e.CellStyle.ForeColor = Color.DarkRed
                    Case "Completed"
                        e.CellStyle.BackColor = Color.LightBlue
                        e.CellStyle.ForeColor = Color.DarkBlue
                End Select
            End If
        End If
    End Sub

    Private Sub btnscheddd_Click(sender As Object, e As EventArgs) Handles btnscheddd.Click
        frmWeeklySched.ShowDialog()
    End Sub

End Class
Public Class frmWeeklySched

    Private Sub frmWeeklySched_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()

        ' 1. Load Dropdowns
        Call loadcombo("SELECT section_id, section_name FROM sections", cbosection, "section_name", "section_id")
        Call loadcombo("SELECT room_id, room_number FROM rooms", cboroom, "room_number", "room_id")

        ' Clear initial grid selections
        dgvSectionSchedule.DataSource = Nothing
        dgvRoomSchedule.DataSource = Nothing
    End Sub

    ' ==============================================================================
    ' 1. TOP GRID: SCHEDULE BY SECTION
    ' ==============================================================================
    Private Sub cbosection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbosection.SelectedIndexChanged
        If cbosection.SelectedIndex >= 0 AndAlso cbosection.SelectedValue IsNot Nothing Then
            Dim selectedSectionId As Integer = 0
            If Integer.TryParse(cbosection.SelectedValue.ToString(), selectedSectionId) Then
                Call LoadSectionMatrix(selectedSectionId, dgvSectionSchedule)
            End If
        End If
    End Sub

    Public Sub LoadSectionMatrix(ByVal sectionId As Integer, ByVal dgv As DataGridView)
        Try
            If con.State = ConnectionState.Closed Then con.Open()

            Dim dtMatrix As New DataTable()
            dtMatrix.Columns.Add("Time Slot", GetType(String))
            dtMatrix.Columns.Add("Monday", GetType(String))
            dtMatrix.Columns.Add("Tuesday", GetType(String))
            dtMatrix.Columns.Add("Wednesday", GetType(String))
            dtMatrix.Columns.Add("Thursday", GetType(String))
            dtMatrix.Columns.Add("Friday", GetType(String))
            dtMatrix.Columns.Add("Saturday", GetType(String))

            Dim sql As String = "SELECT s.day_of_week, " & _
                               "CONCAT(TIME_FORMAT(s.start_time, '%h:%i %p'), ' - ', TIME_FORMAT(s.end_time, '%h:%i %p')) AS time_slot, " & _
                               "sub.subject_code, r.room_number " & _
                               "FROM schedules s " & _
                               "JOIN subjects sub ON s.subject_id = sub.subject_id " & _
                               "JOIN rooms r ON s.room_id = r.room_id " & _
                               "WHERE s.section_id = ? " & _
                               "ORDER BY s.start_time ASC"

            Dim dtRaw As New DataTable()
            Using cmd As New Odbc.OdbcCommand(sql, con)
                cmd.Parameters.Add("?", Odbc.OdbcType.Int).Value = sectionId
                Dim da As New Odbc.OdbcDataAdapter(cmd)
                da.Fill(dtRaw)
            End Using

            For Each row As DataRow In dtRaw.Rows
                Dim day As String = row("day_of_week").ToString().Trim()
                Dim timeSlot As String = row("time_slot").ToString()
                Dim cellContent As String = row("subject_code").ToString() & " (" & row("room_number").ToString() & ")"

                Dim targetRow As DataRow = Nothing
                For Each r As DataRow In dtMatrix.Rows
                    If r("Time Slot").ToString() = timeSlot Then
                        targetRow = r
                        Exit For
                    End If
                Next

                If targetRow Is Nothing Then
                    targetRow = dtMatrix.NewRow()
                    targetRow("Time Slot") = timeSlot
                    targetRow("Monday") = "—"
                    targetRow("Tuesday") = "—"
                    targetRow("Wednesday") = "—"
                    targetRow("Thursday") = "—"
                    targetRow("Friday") = "—"
                    targetRow("Saturday") = "—"
                    dtMatrix.Rows.Add(targetRow)
                End If

                If dtMatrix.Columns.Contains(day) Then
                    targetRow(day) = cellContent
                End If
            Next

            dgv.DataSource = dtMatrix
            Call FormatMatrixGrid(dgv)

        Catch ex As Exception
            MsgBox("Error loading section schedule: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ==============================================================================
    ' 2. BOTTOM GRID: SCHEDULE BY ROOM
    ' ==============================================================================
    Private Sub cboroom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboroom.SelectedIndexChanged
        If cboroom.SelectedIndex >= 0 AndAlso cboroom.SelectedValue IsNot Nothing Then
            Dim selectedRoomId As Integer = 0
            If Integer.TryParse(cboroom.SelectedValue.ToString(), selectedRoomId) Then
                Call LoadRoomMatrix(selectedRoomId, dgvRoomSchedule)
            End If
        End If
    End Sub

    Public Sub LoadRoomMatrix(ByVal roomId As Integer, ByVal dgv As DataGridView)
        Try
            If con.State = ConnectionState.Closed Then con.Open()

            Dim dtMatrix As New DataTable()
            dtMatrix.Columns.Add("Time Slot", GetType(String))
            dtMatrix.Columns.Add("Monday", GetType(String))
            dtMatrix.Columns.Add("Tuesday", GetType(String))
            dtMatrix.Columns.Add("Wednesday", GetType(String))
            dtMatrix.Columns.Add("Thursday", GetType(String))
            dtMatrix.Columns.Add("Friday", GetType(String))
            dtMatrix.Columns.Add("Saturday", GetType(String))

            Dim sql As String = "SELECT s.day_of_week, " & _
                               "CONCAT(TIME_FORMAT(s.start_time, '%h:%i %p'), ' - ', TIME_FORMAT(s.end_time, '%h:%i %p')) AS time_slot, " & _
                               "sub.subject_code, sec.section_name " & _
                               "FROM schedules s " & _
                               "JOIN subjects sub ON s.subject_id = sub.subject_id " & _
                               "JOIN sections sec ON s.section_id = sec.section_id " & _
                               "WHERE s.room_id = ? " & _
                               "ORDER BY s.start_time ASC"

            Dim dtRaw As New DataTable()
            Using cmd As New Odbc.OdbcCommand(sql, con)
                cmd.Parameters.Add("?", Odbc.OdbcType.Int).Value = roomId
                Dim da As New Odbc.OdbcDataAdapter(cmd)
                da.Fill(dtRaw)
            End Using

            For Each row As DataRow In dtRaw.Rows
                Dim day As String = row("day_of_week").ToString().Trim()
                Dim timeSlot As String = row("time_slot").ToString()
                Dim cellContent As String = row("subject_code").ToString() & " [" & row("section_name").ToString() & "]"

                Dim targetRow As DataRow = Nothing
                For Each r As DataRow In dtMatrix.Rows
                    If r("Time Slot").ToString() = timeSlot Then
                        targetRow = r
                        Exit For
                    End If
                Next

                If targetRow Is Nothing Then
                    targetRow = dtMatrix.NewRow()
                    targetRow("Time Slot") = timeSlot
                    targetRow("Monday") = "—"
                    targetRow("Tuesday") = "—"
                    targetRow("Wednesday") = "—"
                    targetRow("Thursday") = "—"
                    targetRow("Friday") = "—"
                    targetRow("Saturday") = "—"
                    dtMatrix.Rows.Add(targetRow)
                End If

                If dtMatrix.Columns.Contains(day) Then
                    targetRow(day) = cellContent
                End If
            Next

            dgv.DataSource = dtMatrix
            Call FormatMatrixGrid(dgv)

        Catch ex As Exception
            MsgBox("Error loading room schedule: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ==============================================================================
    ' 3. GRID FORMATTING & HIGHLIGHTING
    ' ==============================================================================
    Private Sub FormatMatrixGrid(ByVal dgv As DataGridView)
        dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.AllowUserToAddRows = False
        dgv.ReadOnly = True
        dgv.ClearSelection()

        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells("Time Slot").Value IsNot Nothing Then
                row.Cells("Time Slot").Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                row.Cells("Time Slot").Style.Font = New Font(dgv.Font, FontStyle.Bold)
            End If

            ' Columns 1 to 6 correspond to Monday through Saturday
            For colIndex As Integer = 1 To 6
                Dim cellValue As String = If(row.Cells(colIndex).Value IsNot Nothing, row.Cells(colIndex).Value.ToString(), "")
                If cellValue <> "—" AndAlso cellValue <> "" Then
                    row.Cells(colIndex).Style.BackColor = Color.PeachPuff
                    row.Cells(colIndex).Style.SelectionBackColor = Color.Orange
                    row.Cells(colIndex).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                Else
                    row.Cells(colIndex).Style.ForeColor = Color.Gray
                    row.Cells(colIndex).Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If
            Next
        Next
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Me.Hide()
    End Sub

End Class
Public Class frmInstructorsAdd_availability_
    Private Sub frmInstructorsAdd_availability__Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbodayofweek.Items.Clear()
        cbodayofweek.Items.AddRange(New Object() {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
        })

        cbostarttime.Items.Clear()
        cboendtime.Items.Clear()

        Dim startTime As DateTime = DateTime.Today.AddHours(7).AddMinutes(30)
        Dim endTime As DateTime = DateTime.Today.AddHours(21)

        While startTime <= endTime
            Dim timeFormatted As String = startTime.ToString("hh:mm tt")
            cbostarttime.Items.Add(timeFormatted)
            cboendtime.Items.Add(timeFormatted)

            startTime = startTime.AddMinutes(30)
        End While
        Call loadcombo("select teacher_id, last_name from teachers where employment_type = 'Part-Time'", cboprof, "last_name", "teacher_id")
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Dim selectedDay As String = cbodayofweek.SelectedItem.ToString()
        Dim startTime24 As String = DateTime.Parse(cbostarttime.SelectedItem.ToString()).ToString("HH:mm:ss")
        Dim endTime24 As String = DateTime.Parse(cboendtime.SelectedItem.ToString()).ToString("HH:mm:ss")

        ' Note: ODBC uses '?' positional parameters instead of named '@Param'
        Dim query As String = "INSERT INTO teacher_availability (teacher_id, day_of_week, start_time, end_time) VALUES (?, ?, ?, ?)"

        Dim results As DialogResult
        Using cmd As New Odbc.OdbcCommand(query, con)
            cmd.Parameters.AddWithValue("?", cboprof.SelectedValue)
            cmd.Parameters.AddWithValue("?", selectedDay)
            cmd.Parameters.AddWithValue("?", startTime24)
            cmd.Parameters.AddWithValue("?", endTime24)

            cmd.ExecuteNonQuery()
            cboprof.SelectedIndex = -1
            cbodayofweek.SelectedIndex = -1
            cbostarttime.SelectedIndex = -1
            cboendtime.SelectedIndex = -1
            Call loaddata("SELECT teacher_availability.availability_id, teachers.last_name, teacher_availability.day_of_week, teacher_availability.start_time, teacher_availability.end_time FROM teacher_availability JOIN teachers ON teacher_availability.teacher_id = teachers.teacher_id", frmInstructors.DataGridView1)



            results = MessageBox.Show("Do you want to add more?", "Confirm", MessageBoxButtons.YesNo)
            If results = DialogResult.Yes Then
                Me.Hide()
            Else
                frmInsAddChoice.Hide()
                Me.Hide()
            End If
        End Using
    End Sub
End Class
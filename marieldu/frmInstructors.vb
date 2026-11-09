Public Class frmInstructors

    Private Sub frmInstructors_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call connection()
        Call loadcombo("select course_id, course_code from courses", cbosearch, "course_code", "course_id")
        Call loadcombo("select teacher_id, last_name from teachers", cbosearchh, "last_name", "teacher_id")

        ' Initialize Tag values to prevent non-numeric operations
        dgvrooms.Tag = 0
        DataGridView1.Tag = 0

        Call LoadTeachersGrid()
        Call LoadAvailabilityGrid()
    End Sub

    ' Helper methods to load grids cleanly
    Public Sub LoadTeachersGrid()
        Call loaddata("SELECT teachers.teacher_id, teachers.titles, teachers.last_name, teachers.employment_type, courses.course_code, courses.course_id FROM teachers JOIN courses ON teachers.course_id = courses.course_id", dgvrooms)
        dgvrooms.ClearSelection()
        dgvrooms.Tag = 0
    End Sub

    Public Sub LoadAvailabilityGrid()
        Call loaddata("SELECT teacher_availability.availability_id, teachers.last_name, teacher_availability.day_of_week, teacher_availability.start_time, teacher_availability.end_time FROM teacher_availability JOIN teachers ON teacher_availability.teacher_id = teachers.teacher_id", DataGridView1)
        DataGridView1.ClearSelection()
        DataGridView1.Tag = 0
    End Sub

    ' Form Design & Hover Animations
    Private Sub Panel1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles sidebar.MouseEnter
        sidebar.Width = 176
    End Sub

    Private Sub Panel1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles sidebar.MouseLeave
        sidebar.Width = 60
    End Sub

    Private Sub Panel2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles phome.MouseEnter
        sidebar.Width = 176
        phome.BackColor = Color.PeachPuff
    End Sub

    Private Sub Panel2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles phome.MouseLeave
        phome.BackColor = Color.Transparent
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
    End Sub

    Private Sub pnlsections_MouseEnter(sender As Object, e As EventArgs) Handles pnlsections.MouseEnter
        sidebar.Width = 176
        pnlsections.BackColor = Color.PeachPuff
    End Sub

    Private Sub pnlsections_MouseLeave(sender As Object, e As EventArgs) Handles pnlsections.MouseLeave
        pnlsections.BackColor = Color.Transparent
    End Sub

    ' Navigation Handlers
    Private Sub plogout_MouseClick(sender As Object, e As MouseEventArgs) Handles plogout.MouseClick
        frmLogIn.Show()
        Me.Hide()
        frmLogIn.txtpass.Clear()
        frmLogIn.txtusername.Clear()
    End Sub

    Private Sub psubs_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles psubs.MouseClick
        frmSubjects.Show()
        Me.Hide()
    End Sub

    Private Sub phome_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles phome.MouseClick
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub proom_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles proom.MouseClick
        frmRooms.Show()
        Me.Hide()
    End Sub

    Private Sub preservation_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles preservation.MouseClick
        Scheduling.Show()
        Me.Hide()
    End Sub

    Private Sub pcourses_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pcourses.MouseClick
        frmCourses.Show()
        Me.Hide()
    End Sub

    Private Sub pnlsections_MouseClick(sender As Object, e As MouseEventArgs) Handles pnlsections.MouseClick
        frmSection.Show()
        Me.Hide()
    End Sub

    ' Action Buttons
    Private Sub btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        dgvrooms.Tag = 0
        frmInsAddChoice.ShowDialog()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Val(dgvrooms.Tag) <> 0 Then
            frminstructorEdit.ShowDialog()
        Else
            MsgBox("Please select an instructor record from the table first!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim results As DialogResult
        Try
            ' Priority 1: Delete Availability if selected
            If Val(DataGridView1.Tag) <> 0 Then
                results = MessageBox.Show("Are you sure you want to delete this availability record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If results = DialogResult.Yes Then
                    If con.State = ConnectionState.Closed Then con.Open()

                    Using cmd As New Odbc.OdbcCommand("DELETE FROM teacher_availability WHERE availability_id=" & Val(DataGridView1.Tag), con)
                        cmd.ExecuteNonQuery()
                    End Using

                    Call LoadAvailabilityGrid()
                    MsgBox("Availability deleted successfully!", MsgBoxStyle.Information)
                End If

                ' Priority 2: Delete Teacher if selected
            ElseIf Val(dgvrooms.Tag) <> 0 Then
                results = MessageBox.Show("Are you sure you want to delete this teacher record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If results = DialogResult.Yes Then
                    If con.State = ConnectionState.Closed Then con.Open()

                    Using cmd As New Odbc.OdbcCommand("DELETE FROM teachers WHERE teacher_id=" & Val(dgvrooms.Tag), con)
                        cmd.ExecuteNonQuery()
                    End Using

                    Call LoadTeachersGrid()
                    MsgBox("Teacher deleted successfully!", MsgBoxStyle.Information)
                End If

            Else
                MsgBox("Please select a record you want to delete!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox("Error executing delete: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' Grid CellClick Events
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 AndAlso Not DataGridView1.Rows(e.RowIndex).IsNewRow Then
            DataGridView1.Tag = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            ' Deselect teacher grid to avoid ambiguous deletes
            dgvrooms.Tag = 0
        End If
    End Sub

    Private Sub dgvrooms_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvrooms.CellClick
        Try
            ' Ignore clicks on headers or empty area
            If e.RowIndex < 0 OrElse dgvrooms.Rows(e.RowIndex).IsNewRow Then Exit Sub

            Dim selectedRow As DataGridViewRow = dgvrooms.Rows(e.RowIndex)

            ' Store teacher_id in Tag and clear availability tag
            dgvrooms.Tag = selectedRow.Cells(0).Value
            DataGridView1.Tag = 0

            ' Populate Title & Last Name
            frminstructorEdit.txttitles.Text = If(selectedRow.Cells(1).Value IsNot Nothing, selectedRow.Cells(1).Value.ToString(), "")
            frminstructorEdit.txtlname.Text = If(selectedRow.Cells(2).Value IsNot Nothing, selectedRow.Cells(2).Value.ToString(), "")

            ' Populate Employment Type ComboBox
            If selectedRow.Cells(3).Value IsNot Nothing Then
                frminstructorEdit.cboemploymenttype.Text = selectedRow.Cells(3).Value.ToString()
            End If

            ' Match Department by Course Code (Column 4: "BSIT", "BSED", etc.)
            If selectedRow.Cells(4).Value IsNot Nothing Then
                Dim targetDept As String = selectedRow.Cells(4).Value.ToString()
                Dim matchIndex As Integer = -1

                For i As Integer = 0 To frminstructorEdit.cbodept.Items.Count - 1
                    If frminstructorEdit.cbodept.Items(i).ToString() = targetDept Then
                        matchIndex = i
                        Exit For
                    End If
                Next

                frminstructorEdit.cbodept.SelectedIndex = matchIndex
            End If

        Catch ex As Exception
            MsgBox("Error binding selected row: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' Search Box TextChanged Events
    Private Sub cbosearch_TextChanged(sender As Object, e As EventArgs) Handles cbosearch.TextChanged
        If Trim(cbosearch.Text) <> "" Then
            Call loaddata("SELECT teachers.teacher_id, teachers.titles, teachers.last_name, teachers.employment_type, courses.course_code, courses.course_id FROM teachers JOIN courses ON teachers.course_id = courses.course_id WHERE courses.course_code LIKE '%" & Trim(cbosearch.Text) & "%'", dgvrooms)
        Else
            Call LoadTeachersGrid()
        End If
    End Sub

    Private Sub cbosearchh_TextChanged(sender As Object, e As EventArgs) Handles cbosearchh.TextChanged
        If Trim(cbosearchh.Text) <> "" Then
            Call loaddata("SELECT teacher_availability.availability_id, teachers.last_name, teacher_availability.day_of_week, teacher_availability.start_time, teacher_availability.end_time FROM teacher_availability JOIN teachers ON teacher_availability.teacher_id = teachers.teacher_id WHERE teachers.last_name LIKE '%" & Trim(cbosearchh.Text) & "%'", DataGridView1)
        Else
            Call LoadAvailabilityGrid()
        End If
    End Sub

End Class
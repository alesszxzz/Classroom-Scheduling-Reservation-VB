Public Class frmCourses

    Private Sub frmCourses_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call connection()
        Call loaddata("select * from courses", dgvrooms)
    End Sub

    'Form Design
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
        pinstructors.BackColor = Color.PeachPuff
    End Sub

    Private Sub Panel1_MouseLeave1(ByVal sender As Object, ByVal e As System.EventArgs) Handles pinstructors.MouseLeave
        pinstructors.BackColor = Color.Transparent
    End Sub
    Private Sub pnlsections_MouseEnter(sender As Object, e As EventArgs) Handles pnlsections.MouseEnter
        sidebar.Width = 176
        pnlsections.BackColor = Color.PeachPuff
    End Sub

    Private Sub pnlsections_MouseLeave(sender As Object, e As EventArgs) Handles pnlsections.MouseLeave
        pnlsections.BackColor = Color.Transparent
    End Sub


    'NAVIGATION
    Private Sub psubs_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles psubs.MouseClick
        frmSubjects.Show()
        Me.Hide()
    End Sub

    Private Sub pinstructors_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pinstructors.MouseClick
        frmInstructors.Show()
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

    Private Sub phome_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles phome.MouseClick
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        dgvrooms.Tag = 0
        frmCoursesAdd.ShowDialog()
    End Sub
    Private Sub pnlsections_MouseClick(sender As Object, e As MouseEventArgs) Handles pnlsections.MouseClick
        frmSection.Show()
        Me.Close()
    End Sub

    Private Sub plogout_MouseClick(sender As Object, e As MouseEventArgs) Handles plogout.MouseClick
        frmLogIn.Show()
        Me.Hide()
        frmLogIn.txtpass.Clear()
        frmLogIn.txtusername.Clear()
    End Sub

    Private Sub dgvrooms_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvrooms.CellClick
        If e.RowIndex >= 0 Then
            dgvrooms.Tag = dgvrooms.Item(0, e.RowIndex).Value
            frmCoursesEdit.txtcode.Text = dgvrooms.Item(1, e.RowIndex).Value
            frmCoursesEdit.txtname.Text = dgvrooms.Item(2, e.RowIndex).Value
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If dgvrooms.Tag <> 0 Then
            frmCoursesEdit.ShowDialog()
        Else
            MsgBox("Please select a record!")
        End If
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class
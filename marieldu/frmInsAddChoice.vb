Public Class frmInsAddChoice

    Private Sub btnadd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        frmInstructorAdd.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmInstructorsAdd_availability_.ShowDialog()
    End Sub
End Class
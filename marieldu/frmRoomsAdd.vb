Public Class frmRoomsAdd

    Private Sub clearing()
        txtroomnumber.Clear()
        txtlname.Clear()
        cboemploymenttype.SelectedIndex = -1
    End Sub

    Private Sub btnadd_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnadd.Click
        Dim cmd As Odbc.OdbcCommand
        Try
            cmd = New Odbc.OdbcCommand("insert into rooms (room_number, capacity, room_type) values(?,?,?)", con)
            With cmd.Parameters
                .AddWithValue("?", Trim(txtroomnumber.Text))
                .AddWithValue("?", CInt(txtlname.Text))
                .AddWithValue("?", Trim(cboemploymenttype.Text))
            End With
            cmd.ExecuteNonQuery()
            Call loaddata("select * from rooms", frmRooms.dgvrooms)
            Call clearing()
            Me.Hide()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
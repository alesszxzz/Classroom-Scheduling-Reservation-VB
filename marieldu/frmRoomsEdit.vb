Public Class frmRoomsEdit

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cmd As Odbc.OdbcCommand
        Try
            cmd = New Odbc.OdbcCommand("update rooms set room_number=?, capacity=?, room_type=? where room_id=" & frmRooms.dgvrooms.Tag, con)
            With cmd.Parameters
                .AddWithValue("?", Trim(txtroomnumber.Text))
                .AddWithValue("?", CInt(txtcapacity.Text))
                .AddWithValue("?", Trim(cboemploymenttype.Text))
            End With
            cmd.ExecuteNonQuery()
            Call clearing()
            Call loaddata("select * from rooms", frmRooms.dgvrooms)
            Me.Hide()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub clearing()
        txtroomnumber.Clear()
        txtcapacity.Clear()
        cboemploymenttype.SelectedIndex = -1
    End Sub
End Class
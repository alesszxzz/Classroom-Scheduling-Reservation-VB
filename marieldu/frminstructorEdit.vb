Public Class frminstructorEdit

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' 1. Validate inputs before executing
        If Trim(txttitles.Text) = "" OrElse Trim(txtlname.Text) = "" OrElse cbodept.SelectedItem Is Nothing Then
            MsgBox("Please fill in all required fields.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim selectedCourseId As Integer = 0
        If TypeOf cbodept.SelectedItem Is ComboboxItem Then
            Dim selectedItem As ComboboxItem = CType(cbodept.SelectedItem, ComboboxItem)
            selectedCourseId = Convert.ToInt32(selectedItem.Value)
        End If

        Dim teacherId As Integer = 0
        If frmInstructors.dgvrooms.Tag IsNot Nothing Then
            Integer.TryParse(frmInstructors.dgvrooms.Tag.ToString(), teacherId)
        End If

        If teacherId = 0 Then
            MsgBox("Invalid Teacher ID selected.", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Try
            Dim sql As String = "UPDATE teachers SET titles=?, last_name=?, employment_type=?, course_id=? WHERE teacher_id=?"

            Using cmd As New Odbc.OdbcCommand(sql, con)
                cmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = Trim(txttitles.Text)
                cmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = Trim(txtlname.Text)
                cmd.Parameters.Add("?", Odbc.OdbcType.VarChar).Value = cboemploymenttype.Text
                cmd.Parameters.Add("?", Odbc.OdbcType.Int).Value = selectedCourseId
                cmd.Parameters.Add("?", Odbc.OdbcType.Int).Value = teacherId

                cmd.ExecuteNonQuery()
            End Using

            Me.Hide()
            ' Reload grid with updated data
            Call loaddata("SELECT teachers.teacher_id, teachers.titles, teachers.last_name, teachers.employment_type, courses.course_code FROM teachers JOIN courses ON teachers.course_id = courses.course_id", frmInstructors.dgvrooms)
            Call clearing()

            frmInstructors.dgvrooms.Tag = 0

        Catch ex As Exception
            MsgBox("Error updating instructor: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub clearing()
        txttitles.Clear()
        txtlname.Clear()
        cboemploymenttype.SelectedIndex = -1
        cbodept.SelectedIndex = -1
    End Sub

    Private Sub frminstructorEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New()
        ' Required by the designer
        InitializeComponent()

        ' Populate dropdown items on initialization
        cbodept.Items.Clear()
        cbodept.Items.Add(New ComboboxItem("BSIT", 1))
        cbodept.Items.Add(New ComboboxItem("BSHM", 2))
        cbodept.Items.Add(New ComboboxItem("BSTM", 3))
        cbodept.Items.Add(New ComboboxItem("BSED", 4))
    End Sub

    Public Class ComboboxItem
        Public Property Text As String
        Public Property Value As Object

        Public Sub New(ByVal text As String, ByVal value As Object)
            Me.Text = text
            Me.Value = value
        End Sub

        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

End Class
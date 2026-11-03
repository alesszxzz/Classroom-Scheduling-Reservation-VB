Public Class frminstructorEdit

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cmd As Odbc.OdbcCommand
        Try
            cmd = New Odbc.OdbcCommand("update teachers set first_name=?, last_name=?, employment_type=?, course_id=? where teacher_id=" & frmInstructors.dgvrooms.Tag, con)
            With cmd.Parameters
                .AddWithValue("?", Trim(txttitles.Text))
                .AddWithValue("?", Trim(txtlname.Text))
                .AddWithValue("?", cboemploymenttype.Text)
                .AddWithValue("?", cbodept.SelectedValue)
            End With
            cmd.ExecuteNonQuery()
            Me.Hide()
            Call loaddata("select teachers.teacher_id, teachers.titles, teachers.last_name, teachers.employment_type, courses.course_code from teachers join courses on teachers.course_id = courses.course_id", frmInstructors.dgvrooms)
            Call clearing()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub clearing()
        txttitles.Clear()
        txtlname.Clear()
        cboemploymenttype.SelectedIndex = -1
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
Public Class frmSubjectsAdd

    Private Sub frmSubjectsAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call connection()
    End Sub
    Private Sub frmSectionsEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Call loadcombo("select course_id, course_code from courses", cmbcousre, "course_code", "course_id")
        cmbcourse.Items.Clear()
        cmbcourse.Items.Add(New ComboboxItem("BSIT", 1))
        cmbcourse.Items.Add(New ComboboxItem("BSHM", 2))
        cmbcourse.Items.Add(New ComboboxItem("BSTM", 3))
        cmbcourse.Items.Add(New ComboboxItem("BSED", 4))
    End Sub
    Public Class ComboboxItem
        Public Property Text As String
        Public Property Value As Object

        Public Sub New(ByVal text As String, ByVal value As Object)
            Me.Text = text
            Me.Value = value
        End Sub

        ' Controls what displays in the ComboBox dropdown
        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnadd.Click
        Dim cmd As Odbc.OdbcCommand
        Try
            Dim selectedCourse As ComboboxItem = TryCast(cmbcourse.SelectedItem, ComboboxItem)

            If selectedCourse Is Nothing Then
                MsgBox("Please select a course.")
                Exit Sub
            End If

            Dim courseId As Integer = Convert.ToInt32(selectedCourse.Value)

            cmd = New Odbc.OdbcCommand("INSERT INTO subjects (subject_code, subject_title, units, year_level, semester, course_id) VALUES (?, ?, ?, ?, ?, ?)", con)

            With cmd.Parameters
                .Add("?", Odbc.OdbcType.VarChar).Value = Trim(txtsucode.Text)
                .Add("?", Odbc.OdbcType.VarChar).Value = Trim(txttitle.Text)
                .Add("?", Odbc.OdbcType.VarChar).Value = Trim(txtunits.Text)
                .Add("?", Odbc.OdbcType.Int).Value = Convert.ToInt32(cmbyearlevel.SelectedItem)
                .Add("?", Odbc.OdbcType.Int).Value = Convert.ToInt32(cmbsemester.SelectedItem)

                .Add("?", Odbc.OdbcType.Int).Value = courseId
            End With

            cmd.ExecuteNonQuery()
            MsgBox("Subject added successfully!")

            Call clearing()
            Call loaddata("select subjects.subject_id, subjects.subject_code, subjects.subject_title, subjects.units, subjects.year_level, subjects.semester, subjects.course_id, courses.course_code from subjects join courses on subjects.course_id = courses.course_id", frmSubjects.dgvrooms)
            Me.Hide()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub clearing()
        txtsucode.Clear()
        txttitle.Clear()
        txtunits.Clear()
        cmbcourse.SelectedIndex = -1
        cmbsemester.SelectedIndex = -1
        cmbyearlevel.SelectedIndex = -1
    End Sub
End Class
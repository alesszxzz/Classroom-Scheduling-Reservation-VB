Public Class frmSectionsEdit

    Private Sub frmSectionsEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Call loadcombo("select course_id, course_code from courses", cmbcousre, "course_code", "course_id")
        cmbcousre.Items.Clear()
        cmbcousre.Items.Add(New ComboboxItem("BSIT", 1))
        cmbcousre.Items.Add(New ComboboxItem("BSHM", 2))
        cmbcousre.Items.Add(New ComboboxItem("BSTM", 3))
        cmbcousre.Items.Add(New ComboboxItem("BSED", 4))
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmd As Odbc.OdbcCommand
        Try
            ' Extract the numeric Value from the selected ComboboxItem object
            Dim selectedCourse As ComboboxItem = TryCast(cmbcousre.SelectedItem, ComboboxItem)
            Dim courseId As Integer = If(selectedCourse IsNot Nothing, Convert.ToInt32(selectedCourse.Value), 0)

            cmd = New Odbc.OdbcCommand("UPDATE sections SET section_name=?, year_level=?, course_id=? WHERE section_id=?", con)
            With cmd.Parameters
                .Add("?", Odbc.OdbcType.VarChar).Value = Trim(txtname.Text)
                .Add("?", Odbc.OdbcType.Int).Value = Convert.ToInt32(cmbyearlevel.SelectedItem)
                .Add("?", Odbc.OdbcType.Int).Value = courseId
                .Add("?", Odbc.OdbcType.Int).Value = Convert.ToInt32(frmSection.dgvrooms.Tag)
            End With

            cmd.ExecuteNonQuery()
            txtname.Clear()
            cmbcousre.SelectedIndex = -1
            cmbyearlevel.SelectedIndex = -1
            frmSection.dgvrooms.Tag = 0
            Call loaddata("select sections.section_id, sections.section_name, sections.year_level, sections.course_id, courses.course_code from sections join courses on sections.course_id = courses.course_id", frmSection.dgvrooms)
            Me.Hide()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReserve
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnLogOut = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnApproved = New System.Windows.Forms.Button()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.dgvReservations = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvReservations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Peru
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1064, 53)
        Me.Panel1.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Tan
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Font = New System.Drawing.Font("MV Boli", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1064, 53)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "CSTA Lab Scheduling && Reservation"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.BurlyWood
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.btnBack)
        Me.Panel3.Controls.Add(Me.btnLogOut)
        Me.Panel3.Controls.Add(Me.btnReject)
        Me.Panel3.Controls.Add(Me.btnApproved)
        Me.Panel3.Controls.Add(Me.btnadd)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 611)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1064, 70)
        Me.Panel3.TabIndex = 10
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.PapayaWhip
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("SketchFlow Print", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.prac.My.Resources.Resources.icons8_add_24
        Me.Button1.Location = New System.Drawing.Point(936, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(116, 38)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Schedules"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnLogOut
        '
        Me.btnLogOut.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLogOut.BackColor = System.Drawing.Color.PapayaWhip
        Me.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnLogOut.Font = New System.Drawing.Font("SketchFlow Print", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogOut.Image = Global.prac.My.Resources.Resources.icons8_add_24
        Me.btnLogOut.Location = New System.Drawing.Point(38, 20)
        Me.btnLogOut.Name = "btnLogOut"
        Me.btnLogOut.Size = New System.Drawing.Size(129, 38)
        Me.btnLogOut.TabIndex = 4
        Me.btnLogOut.Text = "Log Out"
        Me.btnLogOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLogOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLogOut.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.PapayaWhip
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBack.Font = New System.Drawing.Font("SketchFlow Print", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Image = Global.prac.My.Resources.Resources.icons8_add_24
        Me.btnBack.Location = New System.Drawing.Point(38, 20)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(92, 38)
        Me.btnBack.TabIndex = 3
        Me.btnBack.Text = "Back"
        Me.btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnReject
        '
        Me.btnReject.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReject.BackColor = System.Drawing.Color.PapayaWhip
        Me.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnReject.Font = New System.Drawing.Font("SketchFlow Print", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReject.Image = Global.prac.My.Resources.Resources.icons8_add_24
        Me.btnReject.Location = New System.Drawing.Point(701, 20)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(92, 38)
        Me.btnReject.TabIndex = 2
        Me.btnReject.Text = "Reject"
        Me.btnReject.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReject.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnReject.UseVisualStyleBackColor = False
        '
        'btnApproved
        '
        Me.btnApproved.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApproved.BackColor = System.Drawing.Color.PapayaWhip
        Me.btnApproved.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnApproved.Font = New System.Drawing.Font("SketchFlow Print", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnApproved.Image = Global.prac.My.Resources.Resources.icons8_add_24
        Me.btnApproved.Location = New System.Drawing.Point(567, 20)
        Me.btnApproved.Name = "btnApproved"
        Me.btnApproved.Size = New System.Drawing.Size(107, 38)
        Me.btnApproved.TabIndex = 1
        Me.btnApproved.Text = "Approve"
        Me.btnApproved.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnApproved.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnApproved.UseVisualStyleBackColor = False
        '
        'btnadd
        '
        Me.btnadd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnadd.BackColor = System.Drawing.Color.PapayaWhip
        Me.btnadd.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnadd.Font = New System.Drawing.Font("SketchFlow Print", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnadd.Image = Global.prac.My.Resources.Resources.icons8_add_24
        Me.btnadd.Location = New System.Drawing.Point(819, 20)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(92, 38)
        Me.btnadd.TabIndex = 0
        Me.btnadd.Text = "Add"
        Me.btnadd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnadd.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Beige
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 53)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1064, 558)
        Me.Panel4.TabIndex = 11
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.dgvReservations)
        Me.Panel2.Location = New System.Drawing.Point(51, 47)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(967, 467)
        Me.Panel2.TabIndex = 4
        '
        'dgvReservations
        '
        Me.dgvReservations.AllowUserToAddRows = False
        Me.dgvReservations.AllowUserToDeleteRows = False
        Me.dgvReservations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvReservations.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.BlanchedAlmond
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Javanese Text", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReservations.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.dgvReservations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.Moccasin
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvReservations.DefaultCellStyle = DataGridViewCellStyle17
        Me.dgvReservations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvReservations.EnableHeadersVisualStyles = False
        Me.dgvReservations.Location = New System.Drawing.Point(0, 0)
        Me.dgvReservations.Name = "dgvReservations"
        Me.dgvReservations.ReadOnly = True
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.PeachPuff
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReservations.RowHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.dgvReservations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvReservations.Size = New System.Drawing.Size(965, 465)
        Me.dgvReservations.TabIndex = 0
        '
        'frmReserve
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1064, 681)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "frmReserve"
        Me.Text = "frmReserve"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgvReservations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dgvReservations As System.Windows.Forms.DataGridView
    Friend WithEvents btnReject As System.Windows.Forms.Button
    Friend WithEvents btnApproved As System.Windows.Forms.Button
    Friend WithEvents btnLogOut As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNodes
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
        Me.cmdDel = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.GV = New System.Windows.Forms.DataGridView()
        Me.cmdSetupNode = New System.Windows.Forms.Button()
        CType(Me.GV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdDel
        '
        Me.cmdDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdDel.Location = New System.Drawing.Point(407, 228)
        Me.cmdDel.Name = "cmdDel"
        Me.cmdDel.Size = New System.Drawing.Size(105, 30)
        Me.cmdDel.TabIndex = 15
        Me.cmdDel.Text = "Удалить"
        Me.cmdDel.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Location = New System.Drawing.Point(283, 228)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(104, 30)
        Me.cmdAdd.TabIndex = 10
        Me.cmdAdd.Text = "Добавить"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'GV
        '
        Me.GV.AllowUserToAddRows = False
        Me.GV.AllowUserToDeleteRows = False
        Me.GV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GV.Location = New System.Drawing.Point(12, 12)
        Me.GV.MultiSelect = False
        Me.GV.Name = "GV"
        Me.GV.ReadOnly = True
        Me.GV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GV.Size = New System.Drawing.Size(498, 203)
        Me.GV.TabIndex = 8
        '
        'cmdSetupNode
        '
        Me.cmdSetupNode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSetupNode.Location = New System.Drawing.Point(151, 228)
        Me.cmdSetupNode.Name = "cmdSetupNode"
        Me.cmdSetupNode.Size = New System.Drawing.Size(113, 29)
        Me.cmdSetupNode.TabIndex = 32
        Me.cmdSetupNode.Text = "Открыть"
        Me.cmdSetupNode.UseVisualStyleBackColor = True
        '
        'frmNodes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 264)
        Me.Controls.Add(Me.cmdSetupNode)
        Me.Controls.Add(Me.cmdDel)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.GV)
        Me.Name = "frmNodes"
        Me.Text = "Узлы"
        CType(Me.GV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmdDel As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents GV As System.Windows.Forms.DataGridView
    Friend WithEvents cmdSetupNode As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ExpiredLicense
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExpiredLicense))
        Me.PromptLabel = New System.Windows.Forms.Label()
        Me.ContinueButton = New Sage.Common.Controls.Button()
        Me.SuspendLayout()
        '
        'PromptLabel
        '
        Me.PromptLabel.AutoSize = True
        Me.PromptLabel.Location = New System.Drawing.Point(12, 19)
        Me.PromptLabel.Name = "PromptLabel"
        Me.PromptLabel.Size = New System.Drawing.Size(0, 13)
        Me.PromptLabel.TabIndex = 0
        Me.PromptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ContinueButton
        '
        Me.ContinueButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContinueButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ContinueButton.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContinueButton.Location = New System.Drawing.Point(171, 114)
        Me.ContinueButton.Name = "ContinueButton"
        Me.ContinueButton.Size = New System.Drawing.Size(97, 28)
        Me.ContinueButton.TabIndex = 1
        Me.ContinueButton.Text = "Continue"
        Me.ContinueButton.UseVisualStyleBackColor = True
        '
        'ExpiredLicense
        '
        Me.AcceptButton = Me.ContinueButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ContinueButton
        Me.ClientSize = New System.Drawing.Size(439, 153)
        Me.Controls.Add(Me.ContinueButton)
        Me.Controls.Add(Me.PromptLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ExpiredLicense"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Warning"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PromptLabel As System.Windows.Forms.Label
    Friend WithEvents ContinueButton As Sage.Common.Controls.Button
End Class

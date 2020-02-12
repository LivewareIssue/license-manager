<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LicenseInformation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LicenseInformation))
        Me.AccountNumberLabel = New Sage.Common.Controls.Label()
        Me.AccountNumberTextBox = New System.Windows.Forms.TextBox()
        Me.ExpiryDateTextBox = New System.Windows.Forms.TextBox()
        Me.ExpiryDateLabel = New Sage.Common.Controls.Label()
        Me.LicensedAddonNameTextBox = New System.Windows.Forms.TextBox()
        Me.LicensedAddonNameLabel = New Sage.Common.Controls.Label()
        Me.SuspendLayout()
        '
        'AccountNumberLabel
        '
        Me.AccountNumberLabel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.AccountNumberLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AccountNumberLabel.IgnoreTextFormatting = False
        Me.AccountNumberLabel.Location = New System.Drawing.Point(12, 41)
        Me.AccountNumberLabel.Name = "AccountNumberLabel"
        Me.AccountNumberLabel.Size = New System.Drawing.Size(68, 14)
        Me.AccountNumberLabel.TabIndex = 0
        Me.AccountNumberLabel.Text = "Licensed To:"
        Me.AccountNumberLabel.ToolTip = ""
        '
        'AccountNumberTextBox
        '
        Me.AccountNumberTextBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.AccountNumberTextBox.Enabled = False
        Me.AccountNumberTextBox.Location = New System.Drawing.Point(112, 38)
        Me.AccountNumberTextBox.Name = "AccountNumberTextBox"
        Me.AccountNumberTextBox.Size = New System.Drawing.Size(68, 20)
        Me.AccountNumberTextBox.TabIndex = 1
        '
        'ExpiryDateTextBox
        '
        Me.ExpiryDateTextBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExpiryDateTextBox.Enabled = False
        Me.ExpiryDateTextBox.Location = New System.Drawing.Point(112, 64)
        Me.ExpiryDateTextBox.Name = "ExpiryDateTextBox"
        Me.ExpiryDateTextBox.Size = New System.Drawing.Size(68, 20)
        Me.ExpiryDateTextBox.TabIndex = 3
        '
        'ExpiryDateLabel
        '
        Me.ExpiryDateLabel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ExpiryDateLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExpiryDateLabel.IgnoreTextFormatting = False
        Me.ExpiryDateLabel.Location = New System.Drawing.Point(12, 67)
        Me.ExpiryDateLabel.Name = "ExpiryDateLabel"
        Me.ExpiryDateLabel.Size = New System.Drawing.Size(46, 14)
        Me.ExpiryDateLabel.TabIndex = 2
        Me.ExpiryDateLabel.Text = "Expires:"
        Me.ExpiryDateLabel.ToolTip = ""
        '
        'LicensedAddonNameTextBox
        '
        Me.LicensedAddonNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LicensedAddonNameTextBox.Enabled = False
        Me.LicensedAddonNameTextBox.Location = New System.Drawing.Point(112, 12)
        Me.LicensedAddonNameTextBox.Name = "LicensedAddonNameTextBox"
        Me.LicensedAddonNameTextBox.Size = New System.Drawing.Size(147, 20)
        Me.LicensedAddonNameTextBox.TabIndex = 5
        '
        'LicensedAddonNameLabel
        '
        Me.LicensedAddonNameLabel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.LicensedAddonNameLabel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LicensedAddonNameLabel.IgnoreTextFormatting = False
        Me.LicensedAddonNameLabel.Location = New System.Drawing.Point(12, 15)
        Me.LicensedAddonNameLabel.Name = "LicensedAddonNameLabel"
        Me.LicensedAddonNameLabel.Size = New System.Drawing.Size(94, 14)
        Me.LicensedAddonNameLabel.TabIndex = 4
        Me.LicensedAddonNameLabel.Text = "Licensed Add-On:"
        Me.LicensedAddonNameLabel.ToolTip = ""
        '
        'LicenseInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(275, 97)
        Me.Controls.Add(Me.LicensedAddonNameTextBox)
        Me.Controls.Add(Me.LicensedAddonNameLabel)
        Me.Controls.Add(Me.ExpiryDateTextBox)
        Me.Controls.Add(Me.ExpiryDateLabel)
        Me.Controls.Add(Me.AccountNumberTextBox)
        Me.Controls.Add(Me.AccountNumberLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "LicenseInformation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "License"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents AccountNumberLabel As Sage.Common.Controls.Label
    Friend WithEvents AccountNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ExpiryDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ExpiryDateLabel As Sage.Common.Controls.Label
    Friend WithEvents LicensedAddonNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LicensedAddonNameLabel As Sage.Common.Controls.Label
End Class

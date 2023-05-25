<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Me.components = New System.ComponentModel.Container()
        Me.StartButton = New System.Windows.Forms.Button()
        Me.SnapButton = New System.Windows.Forms.Button()
        Me.txtScore = New System.Windows.Forms.Label()
        Me.txtHighScore = New System.Windows.Forms.Label()
        Me.picCanvas = New System.Windows.Forms.PictureBox()
        Me.GameTimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StartButton
        '
        Me.StartButton.Location = New System.Drawing.Point(646, 24)
        Me.StartButton.Name = "StartButton"
        Me.StartButton.Size = New System.Drawing.Size(100, 44)
        Me.StartButton.TabIndex = 0
        Me.StartButton.Text = "Start"
        Me.StartButton.UseVisualStyleBackColor = True
        '
        'SnapButton
        '
        Me.SnapButton.Location = New System.Drawing.Point(646, 74)
        Me.SnapButton.Name = "SnapButton"
        Me.SnapButton.Size = New System.Drawing.Size(100, 47)
        Me.SnapButton.TabIndex = 1
        Me.SnapButton.Text = "Snap"
        Me.SnapButton.UseVisualStyleBackColor = True
        '
        'txtScore
        '
        Me.txtScore.AutoSize = True
        Me.txtScore.Location = New System.Drawing.Point(643, 147)
        Me.txtScore.Name = "txtScore"
        Me.txtScore.Size = New System.Drawing.Size(36, 15)
        Me.txtScore.TabIndex = 2
        Me.txtScore.Text = "Score"
        '
        'txtHighScore
        '
        Me.txtHighScore.AutoSize = True
        Me.txtHighScore.Location = New System.Drawing.Point(643, 177)
        Me.txtHighScore.Name = "txtHighScore"
        Me.txtHighScore.Size = New System.Drawing.Size(65, 15)
        Me.txtHighScore.TabIndex = 3
        Me.txtHighScore.Text = "High Score"
        '
        'picCanvas
        '
        Me.picCanvas.Location = New System.Drawing.Point(39, 30)
        Me.picCanvas.Name = "picCanvas"
        Me.picCanvas.Size = New System.Drawing.Size(580, 680)
        Me.picCanvas.TabIndex = 4
        Me.picCanvas.TabStop = False
        '
        'GameTimer
        '
        Me.GameTimer.Interval = 50
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(748, 725)
        Me.Controls.Add(Me.picCanvas)
        Me.Controls.Add(Me.txtHighScore)
        Me.Controls.Add(Me.txtScore)
        Me.Controls.Add(Me.SnapButton)
        Me.Controls.Add(Me.StartButton)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.picCanvas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StartButton As Button
    Friend WithEvents SnapButton As Button
    Friend WithEvents txtScore As Label
    Friend WithEvents txtHighScore As Label
    Friend WithEvents picCanvas As PictureBox
    Friend WithEvents GameTimer As Timer
End Class

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace Proyecto_VB
    Partial Public Class Form1
        Inherits Form
        Private Snake As New List(Of Circle)()
        Private food As New Circle()

        Private maxWidth As Integer
        Private maxHeight As Integer

        Private score As Integer
        Private highScore As Integer

        Private rand As New Random()

        Private goLeft As Boolean, goRight As Boolean, goDown As Boolean, goUp As Boolean

        Public Sub New()
            InitializeComponent()

        End Sub

        Private Sub KeyIsDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
            If e.KeyCode = Keys.Left AndAlso Settings.directions <> "right" Then
                goLeft = True
            End If
            If e.KeyCode = Keys.Right AndAlso Settings.directions <> "left" Then
                goRight = True
            End If
            If e.KeyCode = Keys.Up AndAlso Settings.directions <> "down" Then
                goUp = True
            End If
            If e.KeyCode = Keys.Down AndAlso Settings.directions <> "up" Then
                goDown = True
            End If
        End Sub

        Private Sub KeyIsUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyUp
            If e.KeyCode = Keys.Left Then
                goLeft = False
            End If
            If e.KeyCode = Keys.Right Then
                goRight = False
            End If
            If e.KeyCode = Keys.Up Then
                goUp = False
            End If
            If e.KeyCode = Keys.Down Then
                goDown = False
            End If
        End Sub

        Private Sub StartGame(ByVal sender As Object, ByVal e As EventArgs)
            RestartGame()
        End Sub

        Private Sub TakeSnapShot(ByVal sender As Object, ByVal e As EventArgs) Handles SnapButton.Click
            Dim caption As New Label()
            caption.Text = "Logre: " & score & " Y mi record es: " & highScore & " En la vivorita"
            caption.Font = New Font("Arial", 12, FontStyle.Bold)
            caption.ForeColor = Color.Purple
            caption.AutoSize = False
            caption.Width = picCanvas.Width
            caption.Height = 30
            caption.TextAlign = ContentAlignment.MiddleCenter
            picCanvas.Controls.Add(caption)

            Dim dialog As New SaveFileDialog()
            dialog.FileName = "capturas de la vivorita"
            dialog.DefaultExt = "jpg"
            dialog.Filter = "JPG Image File | *.jpg"
            dialog.ValidateNames = True

            If dialog.ShowDialog() = DialogResult.OK Then
                Dim width As Integer = Convert.ToInt32(picCanvas.Width)
                Dim heigh As Integer = Convert.ToInt32(picCanvas.Height)
                Dim bmp As New Bitmap(width, heigh)
                picCanvas.DrawToBitmap(bmp, New Rectangle(0, 0, width, heigh))
                bmp.Save(dialog.FileName, ImageFormat.Jpeg)
                picCanvas.Controls.Remove(caption)
            End If
        End Sub

        Private Sub GameTimerEvent(ByVal sender As Object, ByVal e As EventArgs) Handles GameTimer.Tick
            If goLeft Then
                Settings.directions = "left"
            End If
            If goRight Then
                Settings.directions = "right"
            End If
            If goDown Then
                Settings.directions = "down"
            End If
            If goUp Then
                Settings.directions = "up"
            End If
            For i As Integer = Snake.Count - 1 To 0 Step -1
                If i = 0 Then
                    Select Case Settings.directions
                        Case "left"
                            Snake(i).X -= 1
                            Exit Select
                        Case "right"
                            Snake(i).X += 1
                            Exit Select
                        Case "down"
                            Snake(i).Y += 1
                            Exit Select
                        Case "up"
                            Snake(i).Y -= 1
                            Exit Select
                    End Select

                    If Snake(i).X < 0 Then
                        Snake(i).X = maxWidth
                    End If
                    If Snake(i).X > maxWidth Then
                        Snake(i).X = 0
                    End If
                    If Snake(i).Y < 0 Then
                        Snake(i).Y = maxHeight
                    End If
                    If Snake(i).Y > maxHeight Then
                        Snake(i).Y = 0
                    End If

                    If Snake(i).X = food.X AndAlso Snake(i).Y = food.Y Then
                        EatFood()
                    End If

                    For j As Integer = 1 To Snake.Count - 1
                        If Snake(i).X = Snake(j).X AndAlso Snake(i).Y = Snake(j).Y Then
                            GameOver()
                        End If
                    Next
                Else
                    Snake(i).X = Snake(i - 1).X
                    Snake(i).Y = Snake(i - 1).Y
                End If
            Next

            picCanvas.Invalidate()
        End Sub

        Private Sub UpdatePictureBoxGraphics(ByVal sender As Object, ByVal e As PaintEventArgs) Handles picCanvas.Paint
            Dim canvas As Graphics = e.Graphics
            Dim snakeColour As Brush

            For i As Integer = 0 To Snake.Count - 1
                If i = 0 Then
                    snakeColour = Brushes.Black
                Else
                    snakeColour = Brushes.DarkGreen
                End If

                canvas.FillEllipse(snakeColour, New Rectangle(Snake(i).X * Settings.Width, Snake(i).Y * Settings.Height, Settings.Width, Settings.Height))
            Next

            canvas.FillEllipse(Brushes.DarkRed, New Rectangle(food.X * Settings.Width, food.Y * Settings.Height, Settings.Width, Settings.Height))
        End Sub

        Private Sub RestartGame()
            maxWidth = picCanvas.Width \ Settings.Width - 1
            maxHeight = picCanvas.Height \ Settings.Height - 1

            Snake.Clear()

            StartButton.Enabled = False
            SnapButton.Enabled = False
            score = 0
            txtScore.Text = "Score: " & score

            Dim head As New Circle With {.X = 10, .Y = 5}
            Snake.Add(head)

            For i As Integer = 0 To 9
                Dim body As New Circle()
                Snake.Add(body)
            Next

            food = New Circle With {.X = rand.Next(2, maxWidth), .Y = rand.Next(2, maxHeight)}

            GameTimer.Start()
        End Sub

        Private Sub EatFood()
            score += 1
            txtScore.Text = "Score: " & score

            Dim body As New Circle With {.X = Snake(Snake.Count - 1).X, .Y = Snake(Snake.Count - 1).Y}
            Snake.Add(body)

            food = New Circle With {.X = rand.Next(2, maxWidth), .Y = rand.Next(2, maxHeight)}
        End Sub

        Public Sub GameOver()
            GameTimer.Stop()
            StartButton.Enabled = True
            SnapButton.Enabled = True

            If score > highScore Then

                highScore = score

                txtHighScore.Text = "High Score: " & Environment.NewLine & highScore
                txtHighScore.ForeColor = Color.Maroon
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter
            End If
        End Sub

        Friend WithEvents StartButton As Button
        Friend WithEvents SnapButton As Button
        Friend WithEvents txtScore As Label
        Friend WithEvents txtHighScore As Label

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

        Friend WithEvents picCanvas As PictureBox
        Friend WithEvents GameTimer As Timer
        Private components As IContainer

    End Class

    Public Class Circle
        Public Property X As Integer
        Public Property Y As Integer
    End Class

    Public Class Settings
        Public Shared Width As Integer = 16
        Public Shared Height As Integer = 16
        Public Shared directions As String = "right"
    End Class
End Namespace

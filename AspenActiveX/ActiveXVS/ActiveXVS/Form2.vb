Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = True
        Dim Form2_Color As Graphics
        Form2_Color = Me.CreateGraphics

    End Sub

    Private Sub Form2_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = Me.CreateGraphics
        Dim mBrush As New SolidBrush(Color.Blue)
        g.DrawString("Welcome to paint!", Me.Font, mBrush, 80.0F, 80.0F)
        Dim Col As Color
        Col = Color.FromArgb(128, Color.Yellow)

        Dim Bru As New System.Drawing.Drawing2D.LinearGradientBrush _
        (ClientRectangle, Color.Black, Color.Gray, System.Drawing.Drawing2D.LinearGradientMode.Vertical)

        g.FillRectangle(Bru, New RectangleF(80, 80, 400, 200))

        Dim Pen As New Pen(Col)
        Pen.Color = Color.Red
        Pen.Width = 6
        Pen.EndCap = Drawing2D.LineCap.ArrowAnchor
        g.DrawLine(Pen, 0, 100, 400, 100)
        Pen.DashStyle = Drawing2D.DashStyle.DashDot
        Pen.Color = Color.Green
        Pen.Width = 3
        g.DrawLine(Pen, 0, 100, 200, 0)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim gr As Graphics
        gr = PictureBox1.CreateGraphics



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class


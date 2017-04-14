Imports System.Windows.Forms.DataVisualization.Charting
Public Class Form3
    Dim random As New Random()
    Dim pointIndex As Integer


    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call DrawNTR()

    End Sub

    Sub DrawNTR()

        '图片属性设置
        Chart2.Width = 600    '图片宽度
        Chart2.Height = 400                        '图片高度
        Chart2.BackColor = Color.Azure             '图片背景色
        Chart2.Titles.Clear()
        Dim Title2 As New Title("回流比与理论塔板数的关系") '建立标题
        Title2.Text = "回流比与理论塔板数的关系"
        Chart2.Titles.Add(Title2)
        Chart2.Series.Clear()
        '数据集"出口"显示属性设置
        Dim Series1 As New Series("出口")             '数据集声明  
        Series1.Points.Clear()
        Series1.ChartType = SeriesChartType.Line    '数据显示方式 Line:为折线Spline: 曲线
        Series1.Color = Color.Green                 ' 线条颜色
        Series1.BorderWidth = 2                     ' 线条宽度
        Series1.ShadowOffset = 1                    ' 阴影宽度
        Series1.IsVisibleInLegend = True            ' 是否显示数据说明
        Series1.IsValueShownAsLabel = False         ' 线条上是否给吃数据的显示
        Series1.MarkerStyle = MarkerStyle.Circle    ' 线条上的数据点标志类型
        Series1.MarkerSize = 8                      ' 标志的大小
        Dim i As Integer
        i = 0

        MsgBox("" & Stage.Count)

        Do While (i < Stage.Count)                '向数据集绑定数据
            Series1.Points.AddXY(Stage(i), R(i)) '分别往X,Y轴添加数据(可以为多种类型)    （有多中添加方式）
            i = i + 1
        Loop
        Chart2.Series.Add(Series1)                  '把数据集添加到chart中


        Chart2.ChartAreas.Clear() '清除所有绘图区
        Dim ChartArea1 As New ChartArea("ChartArea1") '新增绘图区
        Chart2.ChartAreas.Add(ChartArea1)
        '作图区的显示属性设置
        Chart2.ChartAreas("ChartArea1").AxisX.IsMarginVisible = True
        Chart2.ChartAreas("ChartArea1").AxisY.IsMarginVisible = True
        Chart2.ChartAreas("ChartArea1").Area3DStyle.Enable3D = False
        '背景色设置
        Chart2.ChartAreas("ChartArea1").ShadowColor = Color.Transparent
        Chart2.ChartAreas("ChartArea1").BackColor = Color.Azure        '该处设置为了由天蓝到白色的逐渐变化
        Chart2.ChartAreas("ChartArea1").BackGradientStyle = GradientStyle.TopBottom
        Chart2.ChartAreas("ChartArea1").BackSecondaryColor = Color.White
        'X,Y坐标线颜色和大小
        Chart2.ChartAreas("ChartArea1").AxisX.LineColor = Color.Blue
        Chart2.ChartAreas("ChartArea1").AxisY.LineColor = Color.Blue
        Chart2.ChartAreas("ChartArea1").AxisX.LineWidth = 2
        Chart2.ChartAreas("ChartArea1").AxisY.LineWidth = 2
        Chart2.ChartAreas("ChartArea1").AxisY.Title = "回流比"
        Chart2.ChartAreas("ChartArea1").AxisX.Title = "塔板数"
        '中间X,Y线条的颜色设置
        Chart2.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.Blue
        Chart2.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.Blue
        'X.Y轴数据显示间隔
        Chart2.ChartAreas("ChartArea1").AxisX.Interval = 0 'AxisInterval(Stage(0), Stage(Stage.Count - 1))  'X轴数据显示间隔
        Chart2.ChartAreas("ChartArea1").AxisY.Interval = 0 ' AxisInterval(R(0), R(R.Count - 1))
        'X轴线条显示间隔
        Chart2.ChartAreas("ChartArea1").AxisX.MajorGrid.Interval = 0 'AxisInterval(Stage(0), Stage(Stage.Count - 1)) / 2
        Chart2.ChartAreas("ChartArea1").AxisY.MajorGrid.Interval = 0 'AxisInterval(R(0), R(R.Count - 1))


        ' Zoom into the X axis
        Chart2.ChartAreas("ChartArea1").CursorX.IsUserEnabled = True
        Chart2.ChartAreas("ChartArea1").CursorX.IsUserSelectionEnabled = True
        Chart2.ChartAreas("ChartArea1").CursorX.Interval = 1
        Chart2.ChartAreas("ChartArea1").CursorX.IntervalOffset = 0
        '  Chart2.ChartAreas("ChartArea1").CursorX.IntervalType = DateTimeIntervalType.Minutes
        Chart2.ChartAreas("ChartArea1").AxisX.ScaleView.Zoomable = True
        Chart2.ChartAreas("ChartArea1").AxisX.ScrollBar.IsPositionedInside = False
        ' Zoom into the y axis
        Chart2.ChartAreas("ChartArea1").CursorY.IsUserEnabled = True
        Chart2.ChartAreas("ChartArea1").CursorY.IsUserSelectionEnabled = True
        Chart2.ChartAreas("ChartArea1").CursorY.Interval = 1
        Chart2.ChartAreas("ChartArea1").CursorY.IntervalOffset = 0
        '  Chart2.ChartAreas("ChartArea1").CursorX.IntervalType = DateTimeIntervalType.Minutes
        Chart2.ChartAreas("ChartArea1").AxisY.ScaleView.Zoomable = True
        Chart2.ChartAreas("ChartArea1").AxisY.ScrollBar.IsPositionedInside = False

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IsNumeric(Me.TextBox1.Text) And IsNumeric(Me.TextBox2.Text) Then

            LowStage = Me.TextBox1.Text
            HighStage = Me.TextBox2.Text
            R.Clear()
            Stage.Clear()
            Call ChangeNTR(ihAPsim)
            engine.Run2()
            Call TransferNTR(ihAPsim)
            Call DrawNTR()
        End If
    End Sub
End Class
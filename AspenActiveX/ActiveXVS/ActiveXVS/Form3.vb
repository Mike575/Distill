Imports System.Windows.Forms.DataVisualization.Charting
Public Class Form3
    Dim random As New Random()
    Dim pointIndex As Integer


    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call DrawNTR(Chart2)
        MaxAcceleratePoint(Stage, R)
        MsgBox(ModifyStage & " " & ModifyR)
    End Sub

    Sub DrawNTR(Chart2)
        Dim NTR As Chart
        NTR = Chart2
        '图片属性设置
        NTR.Width = 600                          '图片宽度
        NTR.Height = 400                         '图片高度
        NTR.BackColor = Color.Azure              '图片背景色
        NTR.Titles.Clear()                       '########################清除Chart中所有Title###############
        Dim Title As New Title("回流比与理论塔板数的关系") '建立标题
        Title.Text = "回流比与理论塔板数的关系"
        NTR.Titles.Add(Title)
        NTR.Series.Clear()
        '数据集"回流比 R"显示属性设置
        Dim Series1 As New Series("回流比 R")        '数据集声明  
        Series1.Points.Clear()                      '########################清除Series1所有点###############
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
        Do While (i < Stage.Count)                    '向数据集绑定数据
            Series1.Points.AddXY(Stage(i), R(i))      '分别往X,Y轴添加数据(可以为多种类型)    （有多中添加方式）
            i = i + 1
        Loop
        NTR.Series.Add(Series1)                       '把数据集添加到chart中
        NTR.ChartAreas.Clear()                        '########################清除所有绘图区###############
        Dim ChartArea1 As New ChartArea("ChartArea1") '新增绘图区
        NTR.ChartAreas.Add(ChartArea1)
        '作图区的显示属性设置
        NTR.ChartAreas("ChartArea1").AxisX.IsMarginVisible = True
        NTR.ChartAreas("ChartArea1").AxisY.IsMarginVisible = True
        NTR.ChartAreas("ChartArea1").Area3DStyle.Enable3D = False
        '背景色设置
        NTR.ChartAreas("ChartArea1").ShadowColor = Color.Transparent
        NTR.ChartAreas("ChartArea1").BackColor = Color.Azure        '该处设置为了由天蓝到白色的逐渐变化
        NTR.ChartAreas("ChartArea1").BackGradientStyle = GradientStyle.TopBottom
        NTR.ChartAreas("ChartArea1").BackSecondaryColor = Color.White
        'X,Y坐标线颜色和大小
        NTR.ChartAreas("ChartArea1").AxisX.LineColor = Color.Blue
        NTR.ChartAreas("ChartArea1").AxisY.LineColor = Color.Blue
        NTR.ChartAreas("ChartArea1").AxisX.LineWidth = 2
        NTR.ChartAreas("ChartArea1").AxisY.LineWidth = 2
        NTR.ChartAreas("ChartArea1").AxisY.Title = "回流比"
        NTR.ChartAreas("ChartArea1").AxisX.Title = "塔板数"
        '中间X,Y线条的颜色设置
        NTR.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.Blue
        NTR.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.Blue
        'X.Y轴数据显示间隔
        NTR.ChartAreas("ChartArea1").AxisX.Interval = 0 'AxisInterval(Stage(0), Stage(Stage.Count - 1))  'X轴数据显示间隔
        NTR.ChartAreas("ChartArea1").AxisY.Interval = 0 ' AxisInterval(R(0), R(R.Count - 1))
        'X轴线条显示间隔
        NTR.ChartAreas("ChartArea1").AxisX.MajorGrid.Interval = 0 'AxisInterval(Stage(0), Stage(Stage.Count - 1)) / 2
        NTR.ChartAreas("ChartArea1").AxisY.MajorGrid.Interval = 0 'AxisInterval(R(0), R(R.Count - 1))
        ' Zoom into the X axis
        NTR.ChartAreas("ChartArea1").CursorX.IsUserEnabled = True
        NTR.ChartAreas("ChartArea1").CursorX.IsUserSelectionEnabled = True
        NTR.ChartAreas("ChartArea1").CursorX.Interval = 1
        NTR.ChartAreas("ChartArea1").CursorX.IntervalOffset = 0
        NTR.ChartAreas("ChartArea1").AxisX.ScaleView.Zoomable = True
        NTR.ChartAreas("ChartArea1").AxisX.ScrollBar.IsPositionedInside = False
        ' Zoom into the y axis
        NTR.ChartAreas("ChartArea1").CursorY.IsUserEnabled = True
        NTR.ChartAreas("ChartArea1").CursorY.IsUserSelectionEnabled = True
        NTR.ChartAreas("ChartArea1").CursorY.Interval = 1
        NTR.ChartAreas("ChartArea1").CursorY.IntervalOffset = 0
        NTR.ChartAreas("ChartArea1").AxisY.ScaleView.Zoomable = True
        NTR.ChartAreas("ChartArea1").AxisY.ScrollBar.IsPositionedInside = False

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
            Call DrawNTR(Chart2)
        End If
    End Sub

End Class
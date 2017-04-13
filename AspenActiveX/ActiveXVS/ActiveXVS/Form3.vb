Imports System.Windows.Forms.DataVisualization.Charting
Public Class Form3
    Dim random As New Random()
    Dim pointIndex As Integer
    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Chart1.ChartAreas.Clear() '清除所有绘图区
        Dim newChartAreas1 As New ChartArea("Default") '新增绘图区
        Chart1.ChartAreas.Add(newChartAreas1)
        Chart1.ChartAreas("Default").BackColor = Color.FromName("GradientInactiveCaption") '设置绘图区颜色
        Chart1.ChartAreas("Default").BackGradientStyle = GradientStyle.HorizontalCenter '设置绘图区颜色渐变方式
        Chart1.ChartAreas("Default").AxisX.IsMarginVisible = True
        Chart1.ChartAreas("Default").Area3DStyle.Enable3D = True '启用3D显示
        Chart1.ChartAreas("Default").AxisX.Title = "时间" 'X轴名称
        Chart1.ChartAreas("Default").AxisY.Title = "数量" 'Y轴名称
        Chart1.Titles.Clear()
        Dim newTitles1 As New Title("回流比与塔板数图") '建立标题
        newTitles1.Text = "回流比与塔板数图"
        Chart1.Titles.Add(newTitles1)
        Chart1.Series.Clear() '清除所有数据集
        Dim newSeries1 As New Series("回流比") '新增数据集
        newSeries1.ChartType = SeriesChartType.Line '直线
        newSeries1.BorderWidth = 2
        newSeries1.Color = Color.Blue
        newSeries1.XValueType = ChartValueType.Time
        newSeries1.IsValueShownAsLabel = False
        Chart1.Series.Add(newSeries1)
        Dim newSeries2 As New Series("塔板数")
        newSeries2.ChartType = SeriesChartType.Line
        newSeries2.BorderWidth = 2
        newSeries2.Color = Color.Green
        newSeries2.XValueType = ChartValueType.Time
        newSeries2.IsValueShownAsLabel = True
        newSeries2.MarkerStyle = MarkerStyle.Square
        Chart1.Series.Add(newSeries2)
        Dim newSeries3 As New Series("下限值")
        newSeries3.ChartType = SeriesChartType.Line
        newSeries3.BorderWidth = 2
        newSeries3.Color = Color.OrangeRed
        newSeries3.XValueType = ChartValueType.Time
        newSeries3.IsValueShownAsLabel = False
        Chart1.Series.Add(newSeries3)



        Dim Series As New Series("曲线") '新增数据集
        Series.ChartType = SeriesChartType.Spline
        Series.BorderWidth = 3
        Series.ShadowOffset = 2
        ' Populate New series with data
        Series.Points.AddY(167)
        Series.Points.AddY(157)
        Series.Points.AddY(183)
        Series.Points.AddY(123)
        Series.Points.AddY(170)
        Series.Points.AddY(160)
        Series.Points.AddY(190)
        Series.Points.AddY(120)
        Series("ShowMarkerLines") = "true"





        '数据源
        Dim test() As Double = {100, 70, 40, 30, 20, 60, 50, 30, 50, 90, 80, 70, 90, 10, 80, 60, 50, 40, 30, 20, 10, 30}     '内销
        Dim test1() As Double = {80, 70, 90, 10, 80, 60, 50, 40, 30, 20, 10, 0, 50, 90, 100, 70, 40, 30, 20, 60, 50, 30}       '出口

        '图片属性设置
        Chart2.Width = IIf((test.Count() * 25 + 200) >= 600, test.Count() * 25 + 200, 600)    '图片宽度
        Chart2.Height = 400                        '图片高度
        Chart2.BackColor = Color.Azure             '图片背景色
        Chart2.Titles.Clear()
        Dim Title2 As New Title("回流比与理论塔板数的关系") '建立标题
        Title2.Text = "回流比与理论塔板数的关系"
        Chart2.Titles.Add(Title2)




        '数据集"出口"显示属性设置
        Dim Series1 As New Series("出口")             '数据集声明  
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
        MsgBox("" & Stage(1) & " " & R(1))
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
        Chart2.ChartAreas("ChartArea1").AxisX.Interval = AxisInterval(Stage(0), Stage(Stage.Count - 1))  'X轴数据显示间隔
        Chart2.ChartAreas("ChartArea1").AxisY.Interval = AxisInterval(R(0), R(R.Count - 1))
        'X轴线条显示间隔
        Chart2.ChartAreas("ChartArea1").AxisX.MajorGrid.Interval = AxisInterval(Stage(0), Stage(Stage.Count - 1)) / 2
        Chart2.ChartAreas("ChartArea1").AxisY.MajorGrid.Interval = AxisInterval(R(0), R(R.Count - 1))


        ' Zoom into the X axis

        Chart2.ChartAreas("ChartArea1").CursorX.IsUserEnabled = True
        Chart2.ChartAreas("ChartArea1").CursorX.IsUserSelectionEnabled = True
        Chart2.ChartAreas("ChartArea1").CursorX.Interval = 0
        Chart2.ChartAreas("ChartArea1").CursorX.IntervalOffset = 0
        Chart2.ChartAreas("ChartArea1").CursorX.IntervalType = DateTimeIntervalType.Minutes
        Chart2.ChartAreas("ChartArea1").AxisX.ScaleView.Zoomable = True
        Chart2.ChartAreas("ChartArea1").AxisX.ScrollBar.IsPositionedInside = False

        '线条1下限横线
        Dim seriesMin As New Series("Min")
        seriesMin.ChartType = SeriesChartType.Line
        seriesMin.BorderWidth = 1
        seriesMin.ShadowOffset = 0
        seriesMin.IsVisibleInLegend = True
        seriesMin.IsValueShownAsLabel = False
        seriesMin.Color = Color.Red
        seriesMin.XValueType = ChartValueType.DateTime
        seriesMin.MarkerStyle = MarkerStyle.None
        Chart2.Series.Add(seriesMin)

        '线条3上限横线
        Dim seriesMax As New Series("Max")
        seriesMax.ChartType = SeriesChartType.Line
        seriesMax.BorderWidth = 1
        seriesMax.ShadowOffset = 0
        seriesMax.IsVisibleInLegend = True
        seriesMax.IsValueShownAsLabel = False
        seriesMax.Color = Color.Red
        seriesMax.XValueType = ChartValueType.DateTime
        seriesMax.MarkerStyle = MarkerStyle.None
        Chart2.Series.Add(seriesMax)

6    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim current_time As DateTime
        current_time = DateTime.Now
        Chart1.Series("标准值").Points.AddXY(current_time, 90)
        Chart1.Series("生产量").Points.AddXY(current_time, random.Next(20, 75))
        Chart1.Series("下限值").Points.AddXY(current_time, 15)
    End Sub

    Private Sub Chart2_Click(sender As Object, e As EventArgs) Handles Chart2.Click

    End Sub
End Class
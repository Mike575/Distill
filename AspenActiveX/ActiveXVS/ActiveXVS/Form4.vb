Public Class Form4

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Chart1.ChartAreas.Clear() '清除所有绘图区
        'Dim newChartAreas1 As New ChartArea("Default") '新增绘图区
        'Chart1.ChartAreas.Add(newChartAreas1)
        'Chart1.ChartAreas("Default").BackColor = Color.FromName("GradientInactiveCaption") '设置绘图区颜色
        'Chart1.ChartAreas("Default").BackGradientStyle = GradientStyle.HorizontalCenter '设置绘图区颜色渐变方式
        'Chart1.ChartAreas("Default").AxisX.IsMarginVisible = True
        'Chart1.ChartAreas("Default").Area3DStyle.Enable3D = True '启用3D显示
        'Chart1.ChartAreas("Default").AxisX.Title = "时间" 'X轴名称
        'Chart1.ChartAreas("Default").AxisY.Title = "数量" 'Y轴名称
        'Chart1.Titles.Clear()
        'Dim newTitles1 As New Title("回流比与塔板数图") '建立标题
        'newTitles1.Text = "回流比与塔板数图"
        'Chart1.Titles.Add(newTitles1)
        'Chart1.Series.Clear() '清除所有数据集
        'Dim newSeries1 As New Series("回流比") '新增数据集
        'newSeries1.ChartType = SeriesChartType.Line '直线
        'newSeries1.BorderWidth = 2
        'newSeries1.Color = Color.Blue
        'newSeries1.XValueType = ChartValueType.Time
        'newSeries1.IsValueShownAsLabel = False
        'Chart1.Series.Add(newSeries1)
        'Dim newSeries2 As New Series("塔板数")
        'newSeries2.ChartType = SeriesChartType.Line
        'newSeries2.BorderWidth = 2
        'newSeries2.Color = Color.Green
        'newSeries2.XValueType = ChartValueType.Time
        'newSeries2.IsValueShownAsLabel = True
        'newSeries2.MarkerStyle = MarkerStyle.Square
        'Chart1.Series.Add(newSeries2)
        'Dim newSeries3 As New Series("下限值")
        'newSeries3.ChartType = SeriesChartType.Line
        'newSeries3.BorderWidth = 2
        'newSeries3.Color = Color.OrangeRed
        'newSeries3.XValueType = ChartValueType.Time
        'newSeries3.IsValueShownAsLabel = False
        'Chart1.Series.Add(newSeries3)
    End Sub
End Class
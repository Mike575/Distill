Imports System.Windows.Forms.DataVisualization.Charting

Public Class MSchart
    Public myChart As Chart
    Public X As New ArrayList
    Public Y As New ArrayList
    Public AxisX_Title As String
    Public AxisY_Title As String
    Public myTitle As String
    Sub Draw_chart()


        '图片属性设置
        myChart.Width = 600                          '图片宽度
        myChart.Height = 400                         '图片高度
        myChart.BackColor = Color.Azure              '图片背景色
        myChart.Titles.Clear()                       '########################清除Chart中所有Title###############
        Dim Title As New Title(myTitle) '建立标题
        Title.Text = myTitle
        myChart.Titles.Add(Title)
        myChart.Series.Clear()
        '数据集"回流比 DSTWU_R"显示属性设置
        Dim Series1 As New Series(AxisY_Title)        '数据集声明  
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
        Do While (i < Y.Count)                    '向数据集绑定数据
            Series1.Points.AddXY(X(i), Y(i))      '分别往X,Y轴添加数据(可以为多种类型)    （有多中添加方式）
            i = i + 1
        Loop
        myChart.Series.Add(Series1)                       '把数据集添加到chart中
        myChart.ChartAreas.Clear()                        '########################清除所有绘图区###############
        Dim ChartArea1 As New ChartArea("ChartArea1") '新增绘图区
        myChart.ChartAreas.Add(ChartArea1)
        '作图区的显示属性设置
        myChart.ChartAreas("ChartArea1").AxisX.IsMarginVisible = True
        myChart.ChartAreas("ChartArea1").AxisY.IsMarginVisible = True
        myChart.ChartAreas("ChartArea1").Area3DStyle.Enable3D = False
        '背景色设置
        myChart.ChartAreas("ChartArea1").ShadowColor = Color.Transparent
        myChart.ChartAreas("ChartArea1").BackColor = Color.Azure        '该处设置为了由天蓝到白色的逐渐变化
        myChart.ChartAreas("ChartArea1").BackGradientStyle = GradientStyle.TopBottom
        myChart.ChartAreas("ChartArea1").BackSecondaryColor = Color.White
        'X,Y坐标线颜色和大小
        myChart.ChartAreas("ChartArea1").AxisX.LineColor = Color.Blue
        myChart.ChartAreas("ChartArea1").AxisY.LineColor = Color.Blue
        myChart.ChartAreas("ChartArea1").AxisX.LineWidth = 2
        myChart.ChartAreas("ChartArea1").AxisY.LineWidth = 2
        myChart.ChartAreas("ChartArea1").AxisY.Title = Me.AxisY_Title
        myChart.ChartAreas("ChartArea1").AxisX.Title = Me.AxisX_Title
        '中间X,Y线条的颜色设置
        myChart.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.Blue
        myChart.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.Blue
        'X.Y轴数据显示间隔
        myChart.ChartAreas("ChartArea1").AxisX.Interval = 0 'AxisInterval(DSTWU_Stage(0), DSTWU_Stage(DSTWU_Stage.Count - 1))  'X轴数据显示间隔
        myChart.ChartAreas("ChartArea1").AxisY.Interval = 0 ' AxisInterval(DSTWU_R(0), DSTWU_R(DSTWU_R.Count - 1))
        'X轴线条显示间隔
        myChart.ChartAreas("ChartArea1").AxisX.MajorGrid.Interval = 0 'AxisInterval(DSTWU_Stage(0), DSTWU_Stage(DSTWU_Stage.Count - 1)) / 2
        myChart.ChartAreas("ChartArea1").AxisY.MajorGrid.Interval = 0 'AxisInterval(DSTWU_R(0), DSTWU_R(DSTWU_R.Count - 1))
        ' Zoom into the X axis
        myChart.ChartAreas("ChartArea1").CursorX.IsUserEnabled = True
        myChart.ChartAreas("ChartArea1").CursorX.IsUserSelectionEnabled = True
        myChart.ChartAreas("ChartArea1").CursorX.Interval = 1
        myChart.ChartAreas("ChartArea1").CursorX.IntervalOffset = 0
        myChart.ChartAreas("ChartArea1").AxisX.ScaleView.Zoomable = True
        myChart.ChartAreas("ChartArea1").AxisX.ScrollBar.IsPositionedInside = False
        ' Zoom into the y axis
        myChart.ChartAreas("ChartArea1").CursorY.IsUserEnabled = True
        myChart.ChartAreas("ChartArea1").CursorY.IsUserSelectionEnabled = True
        myChart.ChartAreas("ChartArea1").CursorY.Interval = 1
        myChart.ChartAreas("ChartArea1").CursorY.IntervalOffset = 0
        myChart.ChartAreas("ChartArea1").AxisY.ScaleView.Zoomable = True
        myChart.ChartAreas("ChartArea1").AxisY.ScrollBar.IsPositionedInside = False
    End Sub
End Class

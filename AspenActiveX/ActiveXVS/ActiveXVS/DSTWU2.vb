Imports System.Windows.Forms.DataVisualization.Charting
Public Class DSTWU2
    Dim random As New Random()
    Dim pointIndex As Integer


    Sub DrawNTR(Chart2)

        '获取回流比和理论板数的关系
        Call Transfer_DSTWU_NTR(ihApsim_DSTWU)

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
        '数据集"回流比 DSTWU_R"显示属性设置
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
        Do While (i < DSTWU_Stage.Count)                    '向数据集绑定数据
            Series1.Points.AddXY(DSTWU_Stage(i), DSTWU_R(i))      '分别往X,Y轴添加数据(可以为多种类型)    （有多中添加方式）
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
        NTR.ChartAreas("ChartArea1").AxisX.Interval = 0 'AxisInterval(DSTWU_Stage(0), DSTWU_Stage(DSTWU_Stage.Count - 1))  'X轴数据显示间隔
        NTR.ChartAreas("ChartArea1").AxisY.Interval = 0 ' AxisInterval(DSTWU_R(0), DSTWU_R(DSTWU_R.Count - 1))
        'X轴线条显示间隔
        NTR.ChartAreas("ChartArea1").AxisX.MajorGrid.Interval = 0 'AxisInterval(DSTWU_Stage(0), DSTWU_Stage(DSTWU_Stage.Count - 1)) / 2
        NTR.ChartAreas("ChartArea1").AxisY.MajorGrid.Interval = 0 'AxisInterval(DSTWU_R(0), DSTWU_R(DSTWU_R.Count - 1))
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

    Sub DrawNTR_Accelerate(Chart1)

        '获取回流比二次导数与塔盘数的关系
        Call MinAcceleratePoint（DSTWU_Stage, DSTWU_R）

        Dim NTR_Accelerate As Chart
        NTR_Accelerate = Chart1
        '图片属性设置
        NTR_Accelerate.Width = 600                          '图片宽度
        NTR_Accelerate.Height = 400                         '图片高度
        NTR_Accelerate.BackColor = Color.Azure              '图片背景色
        NTR_Accelerate.Titles.Clear()                       '########################清除Chart中所有Title###############
        Dim Title As New Title("回流比与理论塔板数二阶导数关系图") '建立标题
        Title.Text = "回流比与理论塔板数二阶导数关系图"
        NTR_Accelerate.Titles.Add(Title)
        NTR_Accelerate.Series.Clear()
        '数据集"回流比 DSTWU_R"显示属性设置
        Dim Series1 As New Series("回流比 R 二阶导数值")        '数据集声明  
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

        Do While (i < Accelerate.Count)                    '向数据集绑定数据
            Series1.Points.AddXY(DSTWU_Stage(i), Accelerate(i))      '分别往X,Y轴添加数据(可以为多种类型)    （有多中添加方式）
            i = i + 1
        Loop
        NTR_Accelerate.Series.Add(Series1)                       '把数据集添加到chart中
        NTR_Accelerate.ChartAreas.Clear()                        '########################清除所有绘图区###############
        Dim ChartArea1 As New ChartArea("ChartArea1") '新增绘图区
        NTR_Accelerate.ChartAreas.Add(ChartArea1)
        '作图区的显示属性设置
        NTR_Accelerate.ChartAreas("ChartArea1").AxisX.IsMarginVisible = True
        NTR_Accelerate.ChartAreas("ChartArea1").AxisY.IsMarginVisible = True
        NTR_Accelerate.ChartAreas("ChartArea1").Area3DStyle.Enable3D = False
        '背景色设置
        NTR_Accelerate.ChartAreas("ChartArea1").ShadowColor = Color.Transparent
        NTR_Accelerate.ChartAreas("ChartArea1").BackColor = Color.Azure        '该处设置为了由天蓝到白色的逐渐变化
        NTR_Accelerate.ChartAreas("ChartArea1").BackGradientStyle = GradientStyle.TopBottom
        NTR_Accelerate.ChartAreas("ChartArea1").BackSecondaryColor = Color.White
        'X,Y坐标线颜色和大小
        NTR_Accelerate.ChartAreas("ChartArea1").AxisX.LineColor = Color.Blue
        NTR_Accelerate.ChartAreas("ChartArea1").AxisY.LineColor = Color.Blue
        NTR_Accelerate.ChartAreas("ChartArea1").AxisX.LineWidth = 2
        NTR_Accelerate.ChartAreas("ChartArea1").AxisY.LineWidth = 2
        NTR_Accelerate.ChartAreas("ChartArea1").AxisY.Title = "回流比二阶导数"
        NTR_Accelerate.ChartAreas("ChartArea1").AxisX.Title = "塔板数"
        '中间X,Y线条的颜色设置
        NTR_Accelerate.ChartAreas("ChartArea1").AxisX.MajorGrid.LineColor = Color.Blue
        NTR_Accelerate.ChartAreas("ChartArea1").AxisY.MajorGrid.LineColor = Color.Blue
        'X.Y轴数据显示间隔
        NTR_Accelerate.ChartAreas("ChartArea1").AxisX.Interval = 0 'AxisInterval(DSTWU_Stage(0), DSTWU_Stage(DSTWU_Stage.Count - 1))  'X轴数据显示间隔
        NTR_Accelerate.ChartAreas("ChartArea1").AxisY.Interval = 0 ' AxisInterval(DSTWU_R(0), DSTWU_R(DSTWU_R.Count - 1))
        'X轴线条显示间隔
        NTR_Accelerate.ChartAreas("ChartArea1").AxisX.MajorGrid.Interval = 0 'AxisInterval(DSTWU_Stage(0), DSTWU_Stage(DSTWU_Stage.Count - 1)) / 2
        NTR_Accelerate.ChartAreas("ChartArea1").AxisY.MajorGrid.Interval = 0 'AxisInterval(DSTWU_R(0), DSTWU_R(DSTWU_R.Count - 1))
        ' Zoom into the X axis
        NTR_Accelerate.ChartAreas("ChartArea1").CursorX.IsUserEnabled = True
        NTR_Accelerate.ChartAreas("ChartArea1").CursorX.IsUserSelectionEnabled = True
        NTR_Accelerate.ChartAreas("ChartArea1").CursorX.Interval = 1
        NTR_Accelerate.ChartAreas("ChartArea1").CursorX.IntervalOffset = 0
        NTR_Accelerate.ChartAreas("ChartArea1").AxisX.ScaleView.Zoomable = True
        NTR_Accelerate.ChartAreas("ChartArea1").AxisX.ScrollBar.IsPositionedInside = False
        ' Zoom into the y axis
        NTR_Accelerate.ChartAreas("ChartArea1").CursorY.IsUserEnabled = True
        NTR_Accelerate.ChartAreas("ChartArea1").CursorY.IsUserSelectionEnabled = True
        NTR_Accelerate.ChartAreas("ChartArea1").CursorY.Interval = 1
        NTR_Accelerate.ChartAreas("ChartArea1").CursorY.IntervalOffset = 0
        NTR_Accelerate.ChartAreas("ChartArea1").AxisY.ScaleView.Zoomable = True
        NTR_Accelerate.ChartAreas("ChartArea1").AxisY.ScrollBar.IsPositionedInside = False

    End Sub

    Sub DSTWU2_Variable_Transfer（）
        On Error GoTo ErrorHandler
        Pressure_Top = TextBox5.Text        '1.0
        Pressure_Bottom = TextBox4.Text     '1.1
        Reflux_Ratio = TextBox3.Text        '-1.5
        LowStage = TextBox1.Text
        HighStage = TextBox2.Text
        ihApsim_DSTWU.Tree.Data.Blocks.B1.Input.PBOT.value = Pressure_Bottom     '1.1
        ihApsim_DSTWU.Tree.Data.Blocks.B1.Input.PTOP.value = Pressure_Top     '1.0
        ihApsim_DSTWU.Tree.Data.Blocks.B1.Input.RR.value = Reflux_Ratio      '-1.5
        ihApsim_DSTWU.Tree.Data.Blocks.B1.Input.LOWER.value = LowStage    '90  
        ihApsim_DSTWU.Tree.Data.Blocks.B1.Input.UPPER.value = HighStage    '200
        Exit Sub
ErrorHandler:
        MsgBox("Form3_Variable_Transfer raised error " & Err.Description)
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IsNumeric(Me.TextBox1.Text) And IsNumeric(Me.TextBox2.Text) And IsNumeric(Me.TextBox3.Text) And IsNumeric(Me.TextBox4.Text) And IsNumeric(Me.TextBox5.Text) Then

            DSTWU2_Variable_Transfer()      '传递DSTWU2窗体中所有的变量

            If LowStage < HighStage Then

                AspenPlusDistilR.Engine.Run2()  '计算塔盘数和回流比的关系
                Call DrawNTR(Chart2)            '获取塔盘数和回流比数据，并绘图
                Call DrawNTR_Accelerate(Chart1) '计算回流比二次导数和塔盘数关系，并绘图
            Else
                MsgBox("please input right information")
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If IsNumeric(Me.TextBox1.Text) And IsNumeric(Me.TextBox2.Text) And IsNumeric(Me.TextBox3.Text) And IsNumeric(Me.TextBox4.Text) And IsNumeric(Me.TextBox5.Text) Then

            Call DSTWU2_Variable_Transfer()         '传递DSTWU2窗体中所有的变量

            If LowStage < HighStage Then

                AspenPlusDistilR.Engine.Run2()      '计算塔盘数和回流比的关系

                '获取新的Accelerate组，获得DSTWU模型在谷底的塔盘数和回流比
                Call MinAcceleratePoint（DSTWU_Stage, DSTWU_R）       'calculate ddR
                RadFrac_Stage = ModifyStage_DSTWU
                MsgBox("Modify DSTWU_Stage:" & ModifyStage_DSTWU & vbCrLf & "Modify DSTWU_R：" & ModifyR_DSTWU)
            Else
                MsgBox("error: LowStage > HighStage")
            End If
        End If
        Me.Visible = False
        DSTWU_Result.Visible = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Visible = False
        DSTWU1.Visible = True

    End Sub

    Private Sub DSTWU2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
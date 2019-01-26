Imports System.Windows.Forms.DataVisualization.Charting
Public Class DSTWU2

    Sub DrawNTR(Chart2)

        '获取回流比和理论板数的关系
        Call myDSTWU.Transfer_DSTWU_NTR(myDSTWU.ihApsim)

        Dim myMSchart2 As New MSchart
        '输入
        myMSchart2.myChart = Chart2
        myMSchart2.myTitle = "回流比与理论塔板数的关系"
        myMSchart2.AxisX_Title = "塔板数"
        myMSchart2.AxisY_Title = "回流比"
        myMSchart2.X = myDSTWU.DSTWU_Stage
        myMSchart2.Y = myDSTWU.DSTWU_R
        '输出
        Call myMSchart2.Draw_chart()

    End Sub

    Sub DrawNTR_N(Chart3)

        '获取回流比和理论板数的关系
        Call myDSTWU.Transfer_DSTWU_NTR(myDSTWU.ihApsim)

        Dim myMSchart3 As New MSchart
        '输入
        myMSchart3.myChart = Chart3
        myMSchart3.myTitle = "理论板数与回流比乘积VS理论板数"
        myMSchart3.AxisX_Title = "塔板数"
        myMSchart3.AxisY_Title = "理论板数与回流比乘积"
        myMSchart3.X = myDSTWU.DSTWU_Stage
        myMSchart3.Y = myDSTWU.DSTWU_StageMulR
        '输出
        Call myMSchart3.Draw_chart()
    End Sub

    Sub DrawNTR_Accelerate(Chart1)

        '获取回流比二次导数与塔盘数的关系
        Call myDSTWU.MinAcceleratePoint（myDSTWU.DSTWU_Stage, myDSTWU.DSTWU_R）

        Dim myMSchart1 As New MSchart
        '输入
        myMSchart1.myChart = Chart1
        myMSchart1.myTitle = "回流比与理论塔板数二阶导数关系图"
        myMSchart1.AxisX_Title = "塔板数"
        myMSchart1.AxisY_Title = "回流比二阶导数"
        myMSchart1.X = myDSTWU.DSTWU_Stage
        myMSchart1.Y = myDSTWU.Accelerate
        '输出
        Call myMSchart1.Draw_chart()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IsNumeric(Me.TextBox1.Text) And IsNumeric(Me.TextBox2.Text) And IsNumeric(Me.TextBox3.Text) And IsNumeric(Me.TextBox4.Text) And IsNumeric(Me.TextBox5.Text) Then
            '输入

            myDSTWU.Pressure_Top = TextBox5.Text        '1.0
            myDSTWU.Pressure_Bottom = TextBox4.Text     '1.1
            myDSTWU.Reflux_Ratio = TextBox3.Text        '-1.5
            myDSTWU.StartStage = TextBox1.Text
            myDSTWU.EndStage = TextBox2.Text

            If myDSTWU.StartStage < myDSTWU.EndStage Then
                Call myDSTWU.DSTWU_Variable_Transfer()
                '   myDSTWU.ihApsim.Engine.Run2()  '计算塔盘数和回流比的关系

                myDSTWU.ihApsim.Run2()  '计算塔盘数和回流比的关系
                Call DrawNTR(Chart2)            '获取塔盘数和回流比数据，并绘图
                Call DrawNTR_N(Chart3)
                Call DrawNTR_Accelerate(Chart1) '计算回流比二次导数和塔盘数关系，并绘图
            Else
                MsgBox("please input right information")
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        If IsNumeric(Me.TextBox1.Text) And IsNumeric(Me.TextBox2.Text) And IsNumeric(Me.TextBox3.Text) And IsNumeric(Me.TextBox4.Text) And IsNumeric(Me.TextBox5.Text) Then

            '输入

            myDSTWU.Pressure_Top = TextBox5.Text        '1.0
            myDSTWU.Pressure_Bottom = TextBox4.Text     '1.1
            myDSTWU.Reflux_Ratio = TextBox3.Text        '-1.5
            myDSTWU.StartStage = TextBox1.Text
            myDSTWU.EndStage = TextBox2.Text
            Call myDSTWU.DSTWU_Variable_Transfer()
            '优化
            Call myDSTWU.get_ModifyStage_DSTWU()

        End If
        Me.Visible = False
        Result.Visible = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Visible = False
        DSTWU1.Visible = True

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub


End Class
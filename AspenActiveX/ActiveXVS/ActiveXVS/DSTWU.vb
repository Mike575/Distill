Public Class DSTWU
    '输入
    Public ihApsim As IHapp
    Public Lightkey_Row As Integer
    Public Heavykey_Row As Integer
    Public Heavykey_feed_MassFrac As Double     '重组分进料质量分数
    Public Lightkey_feed_MassFrac As Double     '轻组分进料质量分数
    Public Heavykey_Up_MassFrac As Double       '重组分塔顶回收质量分数
    Public Lightkey_Up_MassFrac As Double       '轻组分塔顶回收质量分数
    Public Heavykey_Bottom_MassFrac As Double   '重组分塔釜回收质量分数
    Public Temperature_feed As Double           '进料温度
    Public Pressure_feed As Double              '进料压力
    Public velocity_feed As Double              '进料流速   kg/hr
    Public Pressure_Top As Double               '塔顶压力
    Public Pressure_Bottom As Double            '塔底压力
    Public Reflux_Ratio As Double               '回流比
    Public StartStage As Double                 '起始板
    Public EndStage As Double                   '结束板
    '输出
    Public ModifyStage_DSTWU As Double
    Public ModifyR_DSTWU As Double
    Public Accelerate As New ArrayList
    Public DSTWU_R As New ArrayList
    Public DSTWU_Stage As New ArrayList


    Sub DSTWU_Variable_Transfer（）
        On Error GoTo ErrorHandler
        ihApsim.Data.Streams.FEED.Input.FLOW.MIXED.YIBEN.value = Me.Lightkey_feed_MassFrac  '0.3
        ihApsim.Tree.Data.Streams.FEED.Input.FLOW.MIXED.BENYIXI.value = Me.Heavykey_feed_MassFrac '0.7
        ihApsim.Tree.Data.Blocks.B1.Input.RECOVL.value = Me.Lightkey_Up_MassFrac '0.998
        ihApsim.Tree.Data.Blocks.B1.Input.RECOVH.value = Me.Heavykey_Up_MassFrac  '0.001
        ihApsim.Tree.Data.Streams.FEED.Input.PRES.MIXED.value = Me.Pressure_feed    '1.1
        ihApsim.Tree.Data.Streams.FEED.Input.TEMP.MIXED.value = Me.Temperature_feed   '30
        ihApsim.Tree.Data.Streams.FEED.Input.TOTFLOW.MIXED.value = Me.velocity_feed   '1000

        ihApsim.Tree.Data.Blocks.B1.Input.PBOT.value = Me.Pressure_Bottom     '1.1
        ihApsim.Tree.Data.Blocks.B1.Input.PTOP.value = Me.Pressure_Top     '1.0
        ihApsim.Tree.Data.Blocks.B1.Input.RR.value = Me.Reflux_Ratio      '-1.5
        ihApsim.Tree.Data.Blocks.B1.Input.LOWER.value = Me.StartStage    '90  
        ihApsim.Tree.Data.Blocks.B1.Input.UPPER.value = Me.EndStage    '200
        Exit Sub
ErrorHandler:
        MsgBox("DSTWU_Variable_Transfer raised error " & Err.Description)
    End Sub

    Sub get_ModifyStage_DSTWU()
        If Me.StartStage < Me.EndStage Then

            myDSTWU.ihApsim.Engine.Run2()      '计算塔盘数和回流比的关系

            '获取新的Accelerate组，获得DSTWU模型在谷底的塔盘数和回流比
            Call Me.MinAcceleratePoint（DSTWU_Stage, DSTWU_R）       'calculate ddR
            myRadFrac.NStage_Numbers = Me.ModifyStage_DSTWU
            MsgBox("Modify DSTWU_Stage:" & Me.ModifyStage_DSTWU & vbCrLf & "Modify DSTWU_R：" & Me.ModifyR_DSTWU)
        Else
            MsgBox("error: StartStage > EndStage")
        End If

    End Sub
    Function MinAcceleratePoint(X As ArrayList, Y As ArrayList) As Integer

        Dim MaxAccelerate As Double = 0
        Dim MaxAccelerate_plusONE As Double = 0
        Dim MinAccelerate As Double = 0
        Dim MaxAcceleratePoint As Integer
        Dim i As Integer = 1

        '传递DSTWU模型中回流比与理论板数
        Call Me.Transfer_DSTWU_NTR(Me.ihApsim)

        MaxAcceleratePoint = 0
        MaxAccelerate = (DSTWU_R(0) + DSTWU_R(2)) - 2 * DSTWU_R(1)
        Accelerate.Clear()
        '(total stage number - 2) = dR.count
        Do While (i < DSTWU_Stage.Count - 2)
            MaxAccelerate = (DSTWU_R(i - 1) + DSTWU_R(i + 1)) - 2 * DSTWU_R(i)
            MaxAccelerate_plusONE = (DSTWU_R(i) + DSTWU_R(i + 2)) - 2 * DSTWU_R(i + 1)
            Accelerate.Add((DSTWU_R(i - 1) + DSTWU_R(i + 1)) - 2 * DSTWU_R(i))
            If MaxAccelerate_plusONE > MaxAccelerate Then
                MaxAccelerate = MaxAccelerate_plusONE
                MaxAcceleratePoint = i
            End If
            i = i + 1
        Loop

        i = 1
        MinAcceleratePoint = 0

        '清理之前Accelerate计算痕迹
        MinAccelerate = (DSTWU_R(0) + DSTWU_R(2)) - 2 * DSTWU_R(1)
        Do While (i < DSTWU_Stage.Count - 1 - 4)      '(total stage number - 2) = dR.count
            If i > MaxAcceleratePoint + 1 Then      '确保取值在波峰右侧 91
                If (Accelerate(i) - Accelerate(i + 1)) < 2 * ((Accelerate(i + 1) - Accelerate(i + 2))) Then
                    MinAccelerate = Accelerate(i)
                    MinAcceleratePoint = i     'modify
                    Exit Do
                End If
            End If
            i = i + 1
        Loop
        ModifyStage_DSTWU = DSTWU_Stage(MinAcceleratePoint)
        ModifyR_DSTWU = DSTWU_R(MinAcceleratePoint)
        Return MinAcceleratePoint
    End Function

    '传递DSTWU模型中回流比与理论板数
    Public Sub Transfer_DSTWU_NTR(ByVal ihApsim As IHapp)
        On Error GoTo ErrorHandler
        Dim i As Integer
        Dim ihNStage As IHNodeCol
        Dim ihStage As IHNode
        Dim strout As String
        strout = ""
        i = 0

        DSTWU_Stage.Clear()
        DSTWU_R.Clear()
        ihNStage = ihApsim.Tree.Data.Blocks.B1.Output.RR_OUT.Elements
        For Each ihStage In ihNStage
            DSTWU_Stage.Add(ihStage.Name)
            DSTWU_R.Add(ihStage.Value)
            i = i + 1
        Next ihStage
        Exit Sub
ErrorHandler:
        MsgBox("Transfer_DSTWU_NTR raised error " & Err.Description)

    End Sub

End Class

Imports Microsoft.Office.Interop

Module Module1
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'AppExcel means location of database
    Public AppExcel As String = "C:\Users\57584\Documents\GitHub\Ditill\Distill\AspenActiveX\ActiveXVS\ActiveXVS"
    Public ActiveSheet As Object
    Public CellList() As Integer
    Public Accelerate As New ArrayList
    Public XlsObj As Excel.Application 'Excel对象
    Public XlsBook As Excel.Workbook '工作簿
    Public XlsSheet As Excel.Worksheet '工作表
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '判断stage数目，确定X坐标间距
    Function AxisInterval(mix As Double, max As Double) As Integer
        Dim Scale As Integer
        If mix < max Then
            Scale = Math.Log(max - mix, 10)
        ElseIf mix > max Then
            Scale = Math.Log(mix - max, 10)
        Else
            Scale = 1
        End If
        AxisInterval = 10 ^ (Scale - 1)
        Return AxisInterval
    End Function

    '获得DSTWU模型在谷底的塔盘数和回流比
    '获取新的Accelerate组
    Function MinAcceleratePoint(X As ArrayList, Y As ArrayList) As Integer

        Dim MaxAccelerate As Double = 0
        Dim MaxAccelerate_plusONE As Double = 0
        Dim MinAccelerate As Double = 0
        Dim MaxAcceleratePoint As Integer
        Dim i As Integer = 1

        '传递DSTWU模型中回流比与理论板数
        Call Transfer_DSTWU_NTR(ihApsim_DSTWU)

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

            'If i > MaxAcceleratePoint Then      '确保取值在波峰左侧
            '    If MinAccelerate > ((DSTWU_R(i - 1) + DSTWU_R(i + 1)) - 2 * DSTWU_R(i)) Then
            '        MinAccelerate = (DSTWU_R(i - 1) + DSTWU_R(i + 1)) - 2 * DSTWU_R(i)
            '        MinAcceleratePoint = i - 1      'modify
            '    End If
            'End If
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

    Public Function Abs_double(X As Double) As Double       '取double类型变量的绝对值
        If X < 0 Then
            Abs_double = -X
        ElseIf X > 0 Then
            Abs_double = X
        Else
            Abs_double = 0
        End If
        Return Abs_double
    End Function

End Module

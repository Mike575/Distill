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

    '判断最佳塔盘数和回流比
    Function MinAcceleratePoint(X As ArrayList, Y As ArrayList) As Integer
        Dim MaxAccelerate As Double = 0
        Dim MaxAccelerate_plusONE As Double = 0
        Dim MinAccelerate As Double = 0
        Dim MaxAcceleratePoint As Integer
        Dim i As Integer = 1
        MaxAcceleratePoint = 0
        MaxAccelerate = (R(0) + R(2)) - 2 * R(1)
        Do While (i < Stage.Count - 2)      '(total stage number - 2) = dR.count
            MaxAccelerate = (R(i - 1) + R(i + 1)) - 2 * R(i)
            MaxAccelerate_plusONE = (R(i) + R(i + 2)) - 2 * R(i + 1)
            If MaxAccelerate_plusONE > MaxAccelerate Then
                MaxAccelerate = MaxAccelerate_plusONE
                MaxAcceleratePoint = i
            End If
            i = i + 1
        Loop

        i = 1
        MinAcceleratePoint = 0
        MinAccelerate = (R(0) + R(2)) - 2 * R(1)
        Do While (i < Stage.Count - 1)      '(total stage number - 2) = dR.count
            'Accelerate.Add((R(i - 1) - R(i + 1)) / 2)
            Accelerate.Add((R(i - 1) + R(i + 1)) - 2 * R(i))
            If i < MaxAcceleratePoint Then      '确保波谷在波峰左侧
                If MinAccelerate > ((R(i - 1) + R(i + 1)) - 2 * R(i)) Then
                    MinAccelerate = (R(i - 1) + R(i + 1)) - 2 * R(i)
                    MinAcceleratePoint = i - 1      'modify
                End If
            End If
            i = i + 1
        Loop
        DSTWU_ModifyStage = Stage(MinAcceleratePoint)
        DSTWU_ModifyR = R(MinAcceleratePoint)
        Return MinAcceleratePoint
    End Function

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

Imports Microsoft.Office.Interop

Module Module1
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'AppExcel means location of database
    Public AppExcel As String = "C:\Users\57584\Documents\GitHub\Ditill\Distill\AspenActiveX\ActiveXVS\ActiveXVS"
    Public ActiveSheet As Object
    Public CellList() As Integer
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
    Function MaxAcceleratePoint(X As ArrayList, Y As ArrayList) As Integer
        Dim MaxAccelerate As Double = 0
        Dim i As Integer = 1
        Do While (i <= Stage.Count - 2)
            If MaxAccelerate < ((R(i - 1) + R(i + 1)) / 2 - R(i)) Then
                MaxAccelerate = ((R(i - 1) + R(i + 1)) / 2 - R(i))
                MaxAcceleratePoint = i
            End If
            i = i + 1
        Loop
        ModifyStage = Stage(MaxAcceleratePoint)
        ModifyR = R(MaxAcceleratePoint)
        Return MaxAcceleratePoint
    End Function






End Module

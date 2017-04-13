Module Module1

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
End Module

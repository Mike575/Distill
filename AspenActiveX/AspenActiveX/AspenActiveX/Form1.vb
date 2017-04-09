Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ihAPsim = AspenPlus.Application
        'Call GetCollectionExample(ihAPsim)
        Call GetScalarValuesExample(ihAPsim)
        Call ListBlocksExample(ihAPsim)
        'Call UnitStringExample(ihAPsim)
        'Call UnitsConversionExample(ihAPsim)
        'Call UnitsChangeExample(ihAPsim)
        'Call TempProfExample(ihAPsim)
        'Call CompProfExample(ihAPsim)
        'Call ReacCoeffExample(ihAPsim)
        'Call ConnectivityExample(ihAPsim)
        'Call RunExample(ihAPsim)
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ihAPsim = OpenSimulation()

    End Sub
    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        '  Call CloseSimulation(ihAPsim)

    End Sub

End Class

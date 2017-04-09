Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ihAPsim As IHapp
        ihAPsim = OpenSimulation()

        Call GetCollectionExample(ihAPsim)
        Call GetScalarValuesExample(ihAPsim)
        Call ListBlocksExample(ihAPsim)
        Call UnitStringExample(ihAPsim)
        Call UnitsConversionExample(ihAPsim)
        Call UnitsChangeExample(ihAPsim)
        Call TempProfExample(ihAPsim)
        Call CompProfExample(ihAPsim)
        Call ReacCoeffExample(ihAPsim)
        Call ConnectivityExample(ihAPsim)
        Call RunExample(ihAPsim)
    End Sub
End Class

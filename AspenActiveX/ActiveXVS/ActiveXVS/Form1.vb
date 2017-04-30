Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Call GetCollectionExample(ihAPsim)
        Call GetScalarValuesExample(ihAPsim_RadFrac)
        Call ListBlocksExample(ihAPsim_RadFrac)
        'Call UnitStringExample(ihAPsim)
        'Call UnitsConversionExample(ihAPsim)
        'Call UnitsChangeExample(ihAPsim)
        'Call TempProfExample(ihAPsim)
        'Call CompProfExample(ihAPsim)
        'Call ReacCoeffExample(ihAPsim)
        Call ConnectivityExample(ihAPsim_RadFrac)
        'Call RunExample(ihAPsim)
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Database_init()
        ihApsim_DSTWU = OpenDistilR()
        ihAPsim_RadFrac = OpenRadFrac1()

    End Sub
    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        ''Call CloseSimulation(ihApsim_DSTWU)
        ''Call CloseSimulation(ihAPsim_RadFrac)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ihRoot As IHNode
        Dim ihcolOffspring As IHNodeCol
        Dim ihOffspring As IHNode
        Dim strOut As String
        On Error GoTo ErrorHandler
        'get the root of the tree
        'ihAPsim_RadFrac = AspenPlusDistilR.Application
        ihRoot = ihAPsim_RadFrac.Tree.Data.Blocks.B1.Input
        'now get the collection of nodes immediately below the Root
        ihcolOffspring = ihRoot.Elements
        strOut = ""
        For Each ihOffspring In ihcolOffspring
            If IsNumeric(ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUE)) Then

                If ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUE) = 0.1 Then       '<> ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUEDEFAULT) Then
                    strOut = strOut & vbCrLf & ihOffspring.Name
                End If

            End If
        Next
        MsgBox("Offspring nodes are: " & strOut, , "GetCollectionExample")
        TextBox1.Text = strOut
        Exit Sub
ErrorHandler:
        MsgBox("GetCollectionExample raised error" & Err.Description)
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Find.Show()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        DSTWU2.Visible = True

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        RadFrac1.Visible = True
    End Sub
End Class
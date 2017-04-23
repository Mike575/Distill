Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Call GetCollectionExample(ihAPsim)
        Call GetScalarValuesExample(ihAPsim)
        Call ListBlocksExample(ihAPsim)
        'Call UnitStringExample(ihAPsim)
        'Call UnitsConversionExample(ihAPsim)
        'Call UnitsChangeExample(ihAPsim)
        'Call TempProfExample(ihAPsim)
        'Call CompProfExample(ihAPsim)
        'Call ReacCoeffExample(ihAPsim)
        Call ConnectivityExample(ihAPsim)
        'Call RunExample(ihAPsim)
    End Sub



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ihAPsim = OpenDistilR()
        ihAPsim = OpenRadFrac1()
        ihAPsim = AspenPlusDistilR.Application
        Call TransferNTR(ihAPsim)

        '  ihAPsim.Tree.Data.Components.Specifications.Input.CASN.ALCOHOL.value = "7732 - 18 - 5"

    End Sub
    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        '  Call CloseSimulation(ihAPsim)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ihRoot As IHNode
        Dim ihcolOffspring As IHNodeCol
        Dim ihOffspring As IHNode
        Dim strOut As String
        On Error GoTo ErrorHandler
        'get the root of the tree
        ihAPsim = AspenPlusDistilR.Application
        ihRoot = ihAPsim.Tree.Data.Blocks.B1.Input
        'now get the collection of nodes immediately below the Root
        ihcolOffspring = ihRoot.Elements
        strOut = ""
        For Each ihOffspring In ihcolOffspring
            If Not IsNumeric(ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUE)) Then

                If ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUE) = "BENYIXI" Then       '<> ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUEDEFAULT) Then
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
        Form2.Show()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form3.Visible = True

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form4.Visible = True
    End Sub
End Class
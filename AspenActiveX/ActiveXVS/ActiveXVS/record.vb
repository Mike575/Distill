Module record
    'Application.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\2\Input\LL_MAXIT") '最低回流比
    'Application.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\2\Input\MAX_BROY")   '最大回流比
    'Application.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\1\Input\VALUE\1")  ’轻组分乙苯回流质量分数规定
    'Application.Tree.FindNode("\Data\Blocks\B1\Subobjects\Design Specs\2\Input\VALUE\2")  ’重组分苯乙烯回流质量分数规定
    'Application.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\LB\1")          '280 D:F
    'Application.Tree.FindNode("\Data\Blocks\B1\Subobjects\Vary\1\Input\UB\1")          '320 D:F

    '    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '        Dim ihRoot As IHNode
    '        Dim ihcolOffspring As IHNodeCol
    '        Dim ihOffspring As IHNode
    '        Dim strOut As String
    '        Dim ihRoot_Standard As IHNode
    '        Dim ihcolOffspring_Standard As IHNodeCol
    '        Dim ihOffspring_Standard As IHNode

    '        Dim badNode As New Node
    '        Dim goodNode As Node

    '        On Error GoTo ErrorHandler
    '        'get the root of the tree
    '        'ihAPsim_RadFrac = AspenPlusDistilR.Application

    '        ihRoot = RadFrac_Standard.Tree.FindNode("" & TextBox2.Text)
    '        ihRoot_Standard = RadFrac_Standard.Tree.FindNode("" & TextBox2.Text)
    '        'now get the collection of nodes immediately below the Root
    '        ihcolOffspring = ihRoot.Elements
    '        ihcolOffspring_Standard = ihRoot_Standard.Elements
    '        strOut = ""

    '        Dim i = 0
    '        Dim temp As New Node
    '        Dim AllNodes() As Node     'RadFrac1
    '        For Each ihOffspring In ihcolOffspring

    '            temp.Row = i
    '            temp.Name = ihOffspring.Name
    '            temp.value = ihOffspring.Value
    '            ReDim Preserve AllNodes(i)
    '            AllNodes(i) = temp
    '            i = i + 1
    '        Next

    '        i = 0
    '        Dim FindTest_Standard() As Node     'RadFrac standard good condition
    '        For Each ihOffspring_test In ihcolOffspring_Standard
    '            temp.Row = i
    '            temp.Name = ihOffspring_test.Name
    '            temp.value = ihOffspring_test.Value
    '            ReDim Preserve FindTest_Standard(i)
    '            FindTest_Standard(i) = temp
    '            i = i + 1
    '        Next

    '        'If IsNumeric(ihOffspring_test.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUE)) Then

    '        'If ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUE) <> ihOffspring_test.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUE) Then       '<> ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUEDEFAULT) Then
    '        '    strOut = strOut & vbCrLf & ihOffspring.Name
    '        'End If
    '        i = 0
    '        Do While i < AllNodes.Count
    '            'If FindTest_Standard(i).value <> AllNodes(i).value Then       '<> ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUEDEFAULT) Then
    '            '    strOut = strOut & vbCrLf & "RradFrac1:" & AllNodes(i).Name & " " & AllNodes(i).value & vbCrLf & "Standard:" & FindTest_Standard(i).Name & " " & FindTest_Standard(i).value

    '            'End If

    '            If IsNumeric(AllNodes(i).value) AndAlso AllNodes(i).value = TextBox3.Text Then       '<> ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUEDEFAULT) Then
    '                strOut = strOut & vbCrLf & "RradFrac1:" & AllNodes(i).Name & " Value:" & AllNodes(i).value

    '            End If


    '            i = i + 1
    '        Loop
    '        If strOut = "" Then
    '            strOut = "none"
    '        End If
    '        TextBox1.Text = strOut
    '        Exit Sub
    'ErrorHandler:
    '        MsgBox("GetCollectionExample raised error" & Err.Description)
    '    End Sub
End Module







'Temperature liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_TL")
'Temperature vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_TVTO")
'Mass flow liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_LMF")
'Mass flow vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_VMF")
'Volume flow liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_LVF")
'Volume flow vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_VVF")
'Molecular wt liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MWL")
'Molecular wt vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MWV")
'Density liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_RHOL")
'Density vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_RHOV")
'Viscosity liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MUL")
'Viscosity vapor to: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_MUV")
'Surface tension liquid from: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_STEN")
'Foaming Index: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_FMIDX")
'Flow parameter: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_PARM")
'Reduced vapor: Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_QR")
'Reduced F factor        Application.Tree.FindNode("\Data\Blocks\B1\Output\HYD_FFR")
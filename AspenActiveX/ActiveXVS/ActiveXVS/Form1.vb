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

        Dim strOut As String
        Dim i As Integer
        Dim address As String
        Dim deepin_Count As Integer
        i = 0
        strOut = ""
        address = ""
        ReDim deepin(0)
        ihRoot = ihAPsim_RadFrac.Tree.FindNode("" & Me.TextBox2.Text)
        On Error GoTo ErrorHandler
        Call Get_nodes()

        Do While i < FindTest1.Count

            If IsNumeric(FindTest1(i).value) And FindTest1(i).value = TextBox3.Text Then       '<> ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUEDEFAULT) Then
                For deepin_Count = 1 To FindTest1(i).deepin.Count - 1
                    address = address & FindTest1(i).deepin(deepin_Count) & "."
                Next
                FindTest1(i).address = address
                address = ""
                strOut = strOut & vbCrLf & "RradFrac1: " & vbCrLf & "Node_Name: " & FindTest1(i).Name & " Value:" & FindTest1(i).value & vbCrLf & "Node_deep: " & FindTest1(i).deep & vbCrLf & "Address: " & FindTest1(i).address & vbCrLf
            End If
            i = i + 1
        Loop
        If strOut = "" Then
            strOut = "none"
        End If
        TextBox1.Text = strOut
        Exit Sub
ErrorHandler:
        MsgBox("GetCollectionExample raised error" & ihRoot.Name & Err.Description)
    End Sub

    '定义窗体级别变量
    Dim FindTest1() As Node     'RadFrac1
    Dim ihRoot As IHNode
    Dim deepin() As String


    Sub Get_nodes()
        Static temp_Row_i As Integer = 0

        Dim ihcolOffspring As IHNodeCol
        Dim ihOffspring As IHNode
        Dim temp As New Node
        Static deep As Integer = 1
        'now get the collection of nodes immediately below the Root
        ihcolOffspring = ihRoot.Elements

        For Each ihOffspring In ihcolOffspring
            ReDim Preserve deepin(deep)
            deepin(deep) = ihOffspring.Name
            If ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_HASCHILDREN) Then

                ihRoot = ihOffspring
                deep = deep + 1 '深度在此处加一
                Get_nodes()     '迭代
                deep = deep - 1 '深度在此处减一

            Else
                temp.Row = temp_Row_i
                temp.value = ihOffspring.Value
                temp.Name = ihOffspring.Name
                temp.deep = deep
                temp.deepin = deepin
                ReDim Preserve FindTest1(temp_Row_i)
                FindTest1(temp_Row_i) = temp
                temp_Row_i = temp_Row_i + 1
            End If
        Next
        '文本框中数据为根节点，默认深度deep=0 
        '初始化deep=1
        '第一级Get_Node() For循环
        '   ReDim Preserve deepin(deep) #deep=1
        '   deepin(deep) = ihOffspring.Name
        '   第一个子节点没有child,该节点深度deep=1,节点名称为一级循环第一句deepin(1)=offSpring.Name
        '   第n个子节点有child时，deep=deep+1=1+1=2,并调用函数               
        '       第二级Get_Node() For循环
        '           ReDim Preserve deepin(deep) #deep=2
        '           deepin(deep) = ihOffspring.Name
        '           第一个孙节点没有child,该节点深度deep=2,节点名称为二级循环第一句deepin(2)=offSpring.Name
        '           至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量FindTest1（），完毕。         
        '           第n个孙节点有child时，deep=deep+1=2+1=3,并调用函数
        '               第三级Get_Node() For循环
        '                   ReDim Preserve deepin(deep) #deep=3
        '                   deepin(deep) = ihOffspring.Name
        '                   第一个重孙节点没有child,该节点深度deep = 3,redim preserve deepin(deep),节点名称为三级循环第一句deepin(3)=offSpring.Name
        '                   至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量FindTest1（），完毕。         
        '                   第二个重孙节点没有child,该节点深度deep = 2 + 1 = 3,redim preserve deepin(deep),节点名称为deepin(3)=offSpring.Name
        '                   至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量FindTest1（），完毕。         
        '               第三级Get_Node()，结束，deep=deep-1=3-1=2
        '           第n+1个孙节点没有child,该节点深度deep=2,节点名称为此时deepin(2)=offSpring.Name
        '           至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量FindTest1（），完毕。         
        '       第二级Get_Node()，结束，deep=deep-1=2-1=1
        '   第n+1个子节点没有child,该节点深度deep=1,节点名称为此时deepin(1)=offSpring.Name
        '   至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量FindTest1（），完毕。         
        '第一级Get_Node()，结束

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
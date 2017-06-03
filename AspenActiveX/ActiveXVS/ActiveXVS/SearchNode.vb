Public Class SearchNode
    '输入
    Public ihAPsim As IHapp
    Public strIn As String
    Public filename As String
    Public Search_Node_Value As String
    '窗体级私有变量
    Private AllNodes() As Node     '结构体数组
    Private ihRoot As IHNode
    Private deepin() As String

    Sub Get_ihAPsim()
        Dim AspenPlus_temp As HappLS
        AspenPlus_temp = GetObject(filename)
        Me.ihAPsim = AspenPlus_temp.Application
    End Sub


    Function Node_detail() As String
        Dim strOut As String

        Dim i As Integer
        Dim address As String
        Dim deepin_Count As Integer
        Dim strIn_Array() As String = Split(strIn, """")
        i = 0
        strOut = "" : address = ""
        ReDim deepin(0)
        Call Get_ihAPsim()
        ihRoot = ihAPsim.Tree.FindNode("" & strIn_Array(1))

        On Error GoTo ErrorHandler
        Call Get_nodes()

        Do While i < AllNodes.Count

            If IsNumeric(AllNodes(i).value) And AllNodes(i).value = Search_Node_Value Then       '<> ihOffspring.AttributeValue(Happ.HAPAttributeNumber.HAP_VALUEDEFAULT) Then
                For deepin_Count = 1 To AllNodes(i).deepin.Count - 1
                    address = address & AllNodes(i).deepin(deepin_Count) & "."
                Next
                AllNodes(i).address = address
                address = ""
                strOut = strOut & vbCrLf & filename & ": " & vbCrLf & "Node_Name: " & AllNodes(i).Name & vbCrLf & " Node_Value:" & AllNodes(i).value & vbCrLf & "Node_deep: " & AllNodes(i).deep & vbCrLf & "Address: " & AllNodes(i).address & vbCrLf
            End If
            i = i + 1
        Loop
        If strOut = "" Then
            strOut = "none"
        End If
        Node_detail = strOut
        Exit Function
ErrorHandler:
        MsgBox("Node_detail raised error" & vbCrLf & ihRoot.Name & vbCrLf & Err.Description)

    End Function
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
                ReDim Preserve AllNodes(temp_Row_i)
                AllNodes(temp_Row_i) = temp
                temp_Row_i = temp_Row_i + 1
            End If
        Next
        '文本框中数据为根节点，默认深度deep=0 
        '初始化deep=1
        '第一级Get_Node() For循环
        '   ReDim Preserve deepin(deep) #deep=1
        '   deepin(deep) = ihOffspring.Name
        '第一个子节点没有child, 该节点深度deep = 1, 节点名称为一级循环第一句deepin(1) = offSpring.Name
        '   第n个子节点有child时， deep = deep + 1 = 1 + 1 = 2, 并调用函数               
        '       第二级Get_Node() For循环
        '           ReDim Preserve deepin(deep) #deep=2
        '           deepin(deep) = ihOffspring.Name
        '第一个孙节点没有child, 该节点深度deep = 2, 节点名称为二级循环第一句deepin(2) = offSpring.Name
        '           至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量AllNodes（），完毕。         
        '           第n个孙节点有child时， deep = deep + 1 = 2 + 1 = 3, 并调用函数
        '               第三级Get_Node() For循环
        '                   ReDim Preserve deepin(deep) #deep=3
        '                   deepin(deep) = ihOffspring.Name
        '第一个重孙节点没有child, 该节点深度deep = 3,redim preserve deepin(deep),节点名称为三级循环第一句deepin(3)=offSpring.Name
        '                   至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量AllNodes（），完毕。         
        '                   第二个重孙节点没有child, 该节点深度deep = 2 + 1 = 3,redim preserve deepin(deep),节点名称为deepin(3)=offSpring.Name
        '                   至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量AllNodes（），完毕。         
        '               第三级Get_Node()，结束，deep=deep-1=3-1=2
        '           第n+1个孙节点没有child, 该节点深度deep = 2, 节点名称为此时deepin(2) = offSpring.Name
        '           至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量AllNodes（），完毕。         
        '       第二级Get_Node()，结束，deep=deep-1=2-1=1
        '   第n+1个子节点没有child, 该节点深度deep = 1, 节点名称为此时deepin(1) = offSpring.Name
        '   至今该叶子节点已确定， 将局部静态变量deepin()传递给temp.deepin(),将temp节点传递给全局变量AllNodes（），完毕。         
        '第一级Get_Node()，结束

    End Sub
End Class

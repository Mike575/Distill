Public Class addressform

    Private Sub addressform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Database_init()

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Application_path = OpenFileDialog1.FileName
        End If

        Call OpenSimulation()

    End Sub
    Private Sub addressform_Closed(sender As Object, e As EventArgs) Handles Me.Closed

        Call CloseSimulation(myDSTWU.ihApsim)
        Call CloseSimulation(myRadFrac.ihAPsim)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim filename As String
        filename = ""
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            filename = OpenFileDialog1.FileName
            ' TextBox2.Text = System.IO.Path.GetFullPath(OpenFileDialog1.FileName)
        End If

        Dim MySearchNode As New SearchNode
        '输入
        MySearchNode.filename = filename
        If TextBox2.Text <> "" And TextBox3.Text <> "" Then
            MySearchNode.Search_Node_Value = TextBox3.Text
            MySearchNode.strIn = TextBox2.Text      '给定搜索根目录
        Else
            MsgBox("please input data")
            Exit Sub
        End If

        '输出
        TextBox1.Text = MySearchNode.Node_detail()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Find.Show()
    End Sub

End Class
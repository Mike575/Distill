Public Class DSTWU_Result
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub DSTWU_Result_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Results As New ArrayList
        Dim str As String
        Results.Add(ihApsim_DSTWU.Tree.FindNode("\Data\Blocks\B1\Output\MIN_REFLUX").Value)    '最小回流比
        Results.Add(ihApsim_DSTWU.Tree.FindNode("\Data\Blocks\B1\Output\ACT_REFLUX").Value)    '实际回流比
        Results.Add(ihApsim_DSTWU.Tree.FindNode("\Data\Blocks\B1\Output\MIN_STAGES").Value)     '最小理论板数
        Results.Add(ihApsim_DSTWU.Tree.FindNode("\Data\Blocks\B1\Output\ACT_STAGES").Value)    '实际理论板数
        Results.Add(ihApsim_DSTWU.Tree.FindNode("\Data\Blocks\B1\Output\FEED_LOCATN").Value)    '进料位置

        str = ""
        str = str & "最小回流比： " & Results(0) & vbCrLf
        str = str & "实际回流比： " & Results(1) & vbCrLf
        str = str & "最小理论板数： " & Results(2) & vbCrLf
        str = str & "实际理论板数： " & Results(3) & vbCrLf
        str = str & "进料位置： " & Results(4) & vbCrLf

        TextBox1.Text = str
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RadFrac1.Visible = True
    End Sub
End Class
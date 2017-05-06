Public Class DSTWU1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If (TextBox3.Text <> "") And (TextBox4.Text <> "") And (TextBox5.Text <> "") And (TextBox6.Text <> "") And (TextBox7.Text <> "") And (TextBox8.Text <> ""） Then
            Form2_Variable_Transfer（）
            Me.Visible = False
            DSTWU2.Visible = True
        Else
            MsgBox("Please input right information")
        End If

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Combox_init()
    End Sub

    Sub Combox_init()
        ComboBox1.Items.Add("K")
        ComboBox1.Items.Add("℃")
        ComboBox1.SelectedIndex = 1

        ComboBox2.Items.Add("pa")
        ComboBox2.Items.Add("bar")
        ComboBox2.SelectedIndex = 1

        ComboBox3.Items.Add("kmol/hr")
        ComboBox3.Items.Add("kg/hr")
        ComboBox3.SelectedIndex = 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Visible = False
        Find.Visible = True
    End Sub

    Sub Form2_Variable_Transfer（）
        'Public Heavykey_feed_MassFrac As Double     '重组分进料质量分数
        'Public Lightkey_feed_MassFrac As Double     '轻组分进料质量分数
        'Public Heavykey_Up_MassFrac As Double       '重组分塔顶回收质量分数
        'Public Lightkey_Up_MassFrac As Double       '轻组分塔顶回收质量分数
        'Public Heavykey_Bottom_MassFrac As Double   '重组分塔釜回收质量分数
        'Public Temperature_feed As Double           '进料温度
        'Public Pressure_feed As Double              '进料压力
        'Public velocity_feed As Double              '进料流速   kg/hr
        Lightkey_feed_MassFrac = TextBox6.Text
        Heavykey_feed_MassFrac = TextBox7.Text
        Lightkey_Up_MassFrac = TextBox8.Text
        Heavykey_Up_MassFrac = TextBox9.Text
        Temperature_feed = TextBox3.Text
        Pressure_feed = TextBox4.Text
        velocity_feed = TextBox5.Text

        ihApsim_DSTWU.Tree.Data.Streams.FEED.Input.FLOW.MIXED.YIBEN.value = Lightkey_feed_MassFrac  '0.3
        ihApsim_DSTWU.Tree.Data.Streams.FEED.Input.FLOW.MIXED.BENYIXI.value = Heavykey_feed_MassFrac '0.7
        ihApsim_DSTWU.Tree.Data.Blocks.B1.Input.RECOVL.value = Lightkey_Up_MassFrac '0.998
        ihApsim_DSTWU.Tree.Data.Blocks.B1.Input.RECOVH.value = Heavykey_Up_MassFrac  '0.001
        ihApsim_DSTWU.Tree.Data.Streams.FEED.Input.PRES.MIXED.value = Pressure_feed    '1.2
        ihApsim_DSTWU.Tree.Data.Streams.FEED.Input.TEMP.MIXED.value = Temperature_feed   '30
        ihApsim_DSTWU.Tree.Data.Streams.FEED.Input.TOTFLOW.MIXED.value = velocity_feed   '1000
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub
End Class


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
        '输入
        myDSTWU.Lightkey_feed_MassFrac = TextBox6.Text
        myDSTWU.Heavykey_feed_MassFrac = TextBox7.Text
        myDSTWU.Lightkey_Up_MassFrac = TextBox8.Text
        myDSTWU.Heavykey_Up_MassFrac = TextBox9.Text
        myDSTWU.Temperature_feed = TextBox3.Text
        myDSTWU.Pressure_feed = TextBox4.Text
        myDSTWU.velocity_feed = TextBox5.Text


    End Sub


End Class


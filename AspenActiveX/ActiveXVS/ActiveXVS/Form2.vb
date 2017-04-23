Public Class Form2
    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form3.Visible = True

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
        Database_init()
        Find.Visible = True

    End Sub
End Class


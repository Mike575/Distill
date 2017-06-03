Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Database_init()

        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Application_path = OpenFileDialog1.FileName
        End If

        Call OpenSimulation()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Find.Show()
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Call CloseSimulation(myDSTWU.ihApsim)
        Call CloseSimulation(myRadFrac.ihAPsim)
    End Sub


End Class
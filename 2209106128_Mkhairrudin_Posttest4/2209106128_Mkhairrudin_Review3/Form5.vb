Public Class Form5
    Private id_user As Integer
    Public Sub New(id_user As Integer)
        InitializeComponent()
        Me.id_user = id_user
        MessageBox.Show("ID User in Form5: " & id_user) ' Tambahkan ini untuk debugging
    End Sub



    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub DataTiketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataTiketToolStripMenuItem.Click
        Dim form6 As New Form6(id_user)
        form6.Show()
    End Sub

    Private Sub DataGerbongToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataGerbongToolStripMenuItem.Click
        Form8.Show()
    End Sub
End Class
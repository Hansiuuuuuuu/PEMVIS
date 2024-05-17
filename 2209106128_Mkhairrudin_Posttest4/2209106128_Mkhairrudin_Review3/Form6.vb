Imports MySql.Data.MySqlClient

Public Class Form6
    Private id_user As Integer

    Public Sub New(id_user As Integer)
        InitializeComponent()
        Me.id_user = id_user
        MessageBox.Show("ID User in Form6: " & id_user) ' Tambahkan ini untuk debugging
    End Sub

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        FillComboBoxAsal()
    End Sub
    Private Sub FillComboBoxAsal()
        Try
            ' Pastikan koneksi dibuka
            If CONN.State = ConnectionState.Closed Then
                CONN.Open()
            End If

            Dim query As String = "SELECT DISTINCT asal FROM tbkereta"
            CMD = New MySqlCommand(query, CONN)
            RD = CMD.ExecuteReader()

            cbasal.Items.Clear()
            While RD.Read()
                cbasal.Items.Add(RD("asal").ToString())
            End While
            RD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Pastikan koneksi ditutup
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub
    Private Sub FillComboBoxtujuan()

        Try
            ' Pastikan koneksi dibuka
            If CONN.State = ConnectionState.Closed Then
                CONN.Open()
            End If

            Dim query As String = "SELECT DISTINCT tujuan FROM tbkereta WHERE asal = @asal"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@asal", cbasal.SelectedItem.ToString())
            RD = CMD.ExecuteReader()

            cbtujuan.Items.Clear()
            While RD.Read()
                cbtujuan.Items.Add(RD("tujuan").ToString())
            End While
            RD.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Pastikan koneksi ditutup
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

    Private Sub FillComboBoxkelas()
        koneksi()
        Try
            If cbtujuan.SelectedItem IsNot Nothing Then
                Dim selectedtujuan As String = cbtujuan.SelectedItem.ToString()
                Dim query As String = "SELECT DISTINCT kelas FROM tbkereta WHERE tujuan = @tujuan"
                CMD = New MySqlCommand(query, CONN)
                CMD.Parameters.AddWithValue("@tujuan", selectedtujuan)
                RD = CMD.ExecuteReader()

                cbkelas.Items.Clear()
                While RD.Read()
                    cbkelas.Items.Add(RD("kelas").ToString())
                End While

                ' Tutup koneksi dan tutup DataReader
                RD.Close()
                CONN.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Displayharga()
        ' Panggil fungsi koneksi dari modul
        koneksi()

        Try

            Dim selectedtujuan As String = cbtujuan.SelectedItem.ToString()
            Dim selectedkelas As String = cbkelas.SelectedItem.ToString()
            Dim query As String = "SELECT harga FROM tbkereta WHERE tujuan = @tujuan AND kelas = @kelas"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@tujuan", selectedtujuan)
            CMD.Parameters.AddWithValue("@kelas", selectedkelas)
            RD = CMD.ExecuteReader()

            cbharga.Items.Clear()
            While RD.Read()
                cbharga.Items.Add(RD("harga").ToString())
            End While

            ' Tutup koneksi dan tutup DataReader
            RD.Close()
            CONN.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Displayjam()
        ' Panggil fungsi koneksi dari modul
        koneksi()

        Try
            Dim selectedtujuan As String = cbtujuan.SelectedItem.ToString()
            Dim selectedkelas As String = cbkelas.SelectedItem.ToString()
            Dim query As String = "SELECT jam FROM tbkereta WHERE tujuan = @tujuan AND kelas = @kelas"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@tujuan", selectedtujuan)
            CMD.Parameters.AddWithValue("@kelas", selectedkelas)
            RD = CMD.ExecuteReader()

            cbjam.Items.Clear()
            While RD.Read()
                cbjam.Items.Add(RD("jam"))
            End While

            ' Tutup koneksi dan tutup DataReader
            RD.Close()
            CONN.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Displaygerbong()
        ' Panggil fungsi koneksi dari modul
        koneksi()

        Try
            Dim selectedtujuan As String = cbtujuan.SelectedItem.ToString()
            Dim selectedkelas As String = cbkelas.SelectedItem.ToString()
            Dim query As String = "SELECT gerbong FROM tbkereta WHERE tujuan = @tujuan AND kelas = @kelas"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@tujuan", selectedtujuan)
            CMD.Parameters.AddWithValue("@kelas", selectedkelas)
            RD = CMD.ExecuteReader()

            cbgerbong.Items.Clear()
            While RD.Read()
                cbgerbong.Items.Add(RD("gerbong").ToString())
            End While

            ' Tutup koneksi dan tutup DataReader
            RD.Close()
            CONN.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub cbtujuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbtujuan.SelectedIndexChanged
        cbkelas.Items.Clear()


        FillComboBoxkelas()
    End Sub

    Private Sub cbkelas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbkelas.SelectedIndexChanged
        Displayharga()
        Displayjam()
        Displaygerbong()
    End Sub

    Private Sub cbharga_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbharga.SelectedIndexChanged

    End Sub

    Private Sub btnsimpan_Click(sender As Object, e As EventArgs) Handles btnsimpan.Click
        koneksi()

        If txtnama.Text = "" Or textnohp.Text = "" Then
            MsgBox("Data Belum Lengkap")
            txtnama.Focus()
        Else
            ' Periksa apakah id_user ada di tabel user
            CMD = New MySqlCommand("SELECT * FROM user WHERE id_user = @id_user", CONN)
            CMD.Parameters.AddWithValue("@id_user", id_user)
            RD = CMD.ExecuteReader()
            If RD.HasRows Then
                RD.Close()

                CMD = New MySqlCommand("SELECT * FROM tbpesanan WHERE nama = @nama", CONN)
                CMD.Parameters.AddWithValue("@nama", txtnama.Text)
                RD = CMD.ExecuteReader()
                RD.Read()
                If Not RD.HasRows Then
                    RD.Close()
                    CMD = New MySqlCommand("INSERT INTO tbpesanan (nama, nohp, asal, tujuann, kelass, hargaa, jamm, gerbongg, id_user) VALUES (@nama, @nohp, @asal, @tujuann, @kelass, @hargaa, @jamm, @gerbongg, @id_user)", CONN)
                    CMD.Parameters.AddWithValue("@nama", txtnama.Text)
                    CMD.Parameters.AddWithValue("@nohp", textnohp.Text)
                    CMD.Parameters.AddWithValue("@asal", cbasal.Text)

                    CMD.Parameters.AddWithValue("@tujuann", cbtujuan.Text)
                    CMD.Parameters.AddWithValue("@kelass", cbkelas.Text)
                    CMD.Parameters.AddWithValue("@hargaa", cbharga.Text)
                    CMD.Parameters.AddWithValue("@jamm", DateTime.Now.ToString("yyyy-MM-dd"))
                    CMD.Parameters.AddWithValue("@gerbongg", cbgerbong.Text)
                    CMD.Parameters.AddWithValue("@id_user", id_user)
                    CMD.ExecuteNonQuery()

                    MsgBox("Simpan Data Sukses!")
                Else
                    RD.Close()
                    MsgBox("Data dengan nama tersebut sudah ada!")
                End If
            Else
                RD.Close()
                MsgBox("ID User tidak valid!")
            End If
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cbasal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbasal.SelectedIndexChanged
        cbtujuan.Items.Clear()


        FillComboBoxtujuan()
    End Sub

    Private Sub cbjam_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbjam.SelectedIndexChanged

    End Sub
End Class
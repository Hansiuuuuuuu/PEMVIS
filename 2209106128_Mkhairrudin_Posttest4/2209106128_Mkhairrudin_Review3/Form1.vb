Imports MySql.Data.MySqlClient
Public Class Form1

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
    Sub Kosong()
        txtTujuan.Clear()
        txtHarga.Clear()
        txtTujuan.Focus()
    End Sub
    Sub isi()
        txtTujuan.Clear()
        txtTujuan.Focus()
    End Sub
    Sub tampilJenis()
        DA = New MySqlDataAdapter("SELECT * FROM tbkereta", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tbkereta")

        

        DataGridView1.DataSource = DS.Tables("tbkereta")
        DataGridView1.Refresh()
    End Sub

    Sub aturGrid()
        DataGridView1.Columns(0).Width = 100
        DataGridView1.Columns(1).Width = 150
        DataGridView1.Columns(2).Width = 150
        DataGridView1.Columns(3).Width = 150
        DataGridView1.Columns(4).Width = 150
        DataGridView1.Columns(5).Width = 150
        DataGridView1.Columns(6).Width = 150
        DataGridView1.Columns(0).HeaderText = "Kode Kereta"
        DataGridView1.Columns(1).HeaderText = "Asal"
        DataGridView1.Columns(2).HeaderText = "Tujuan"
        DataGridView1.Columns(3).HeaderText = "Harga"
        DataGridView1.Columns(4).HeaderText = "Kelas"
        DataGridView1.Columns(5).HeaderText = "Tanggal Keberangkatan"
        DataGridView1.Columns(6).HeaderText = "Gerbong"
    End Sub

    Private Sub txtTujuan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTujuan.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[a-z, A-Z]" _
            OrElse keyascii = Keys.Back _
            OrElse keyascii = Keys.Space _
            OrElse keyascii = Keys.Return _
            OrElse keyascii = Keys.Delete) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub


    Private Sub txtHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHarga.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)

        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub

    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        If txtTujuan.Text = "" Or txtHarga.Text = "" Then
            MsgBox("Data Belum Lengkap")
            txtTujuan.Focus()
        Else
            Dim connStr As String = "server=localhost;user=root;database=dbtiket;port=3306;password="
            Dim conn As New MySqlConnection(connStr)
            Try
                conn.Open()

                Dim query As String = "INSERT INTO tbkereta (asal, tujuan, harga, kelas, jam, gerbong) VALUES (@asal, @tujuan, @harga, @kelas, @jam, @gerbong)"
                Dim cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@asal", txtasal.Text)
                cmd.Parameters.AddWithValue("@tujuan", txtTujuan.Text)
                cmd.Parameters.AddWithValue("@harga", txtHarga.Text)
                If rbPria.Checked Then
                    cmd.Parameters.AddWithValue("@kelas", rbPria.Text)
                Else
                    cmd.Parameters.AddWithValue("@kelas", rbPerempuan.Text)
                End If
                cmd.Parameters.AddWithValue("@jam", DateTime.Now.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@gerbong", cbgerbong.SelectedItem.ToString())
                cmd.ExecuteNonQuery()

                tampilJenis()
                Kosong()
                MsgBox("Simpan Data Sukses!")
                txtTujuan.Focus()
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End If


    End Sub

    Private Sub txtkode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtkode.KeyPress
        Dim keyascii As Short = Asc(e.KeyChar)

        If (e.KeyChar Like "[0-9]" OrElse keyascii = Keys.Back) Then
            keyascii = 0
        Else
            e.Handled = CBool(keyascii)
        End If
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        tampilJenis()
        tampilgerbong()
        Kosong()
        aturGrid()
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text <> "" Then
            Dim connStr As String = "server=localhost;user=root;database=dbtiket;port=3306;password="
            Dim conn As New MySqlConnection(connStr)
            Try
                conn.Open()
                Dim query As String = "select * from tbkereta where kode like '%" & txtsearch.Text & "%'"
                Dim cmd As New MySqlCommand(query, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(reader)
                If dt.Rows.Count > 0 Then
                    DataGridView1.DataSource = dt
                Else
                    MsgBox("Data tidak ditemukan", MsgBoxStyle.Information)
                End If
                reader.Close()
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            Finally
                conn.Close()
            End Try
        Else
            tampilJenis()
        End If
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If txtkode.Text = "" Then
            MsgBox("Kode Jenis belum diisi")
            txtkode.Focus()
        Else
            Dim connStr As String = "server=localhost;user=root;database=dbtiket;port=3306;password="
            Dim conn As New MySqlConnection(connStr)
            Try
                conn.Open()
                Dim query As String = "select * from tbkereta where kode = '" & txtkode.Text & "'"
                Dim cmd As New MySqlCommand(query, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    reader.Close()
                    Dim deleteQuery As String = "delete from tbkereta where kode = '" & txtkode.Text & "'"
                    Dim deleteCmd As New MySqlCommand(deleteQuery, conn)
                    deleteCmd.ExecuteNonQuery()
                    MsgBox("Data berhasil dihapus")
                    tampilJenis()
                    Kosong()
                Else
                    MsgBox("Kode Jenis belum ada")
                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End If
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If txtkode.Text = "" Then
            MsgBox("Kode Jenis belum diisi")
            txtkode.Focus()
        Else
            Dim connStr As String = "server=localhost;user=root;database=dbtiket;port=3306;password="
            Dim conn As New MySqlConnection(connStr)
            Try
                conn.Open()
                Dim query As String = "select * from tbkereta where kode = '" & txtkode.Text & "'"
                Dim cmd As New MySqlCommand(query, conn)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    reader.Close()
                    Dim updateQuery As String = "update tbkereta set asal=@asal, tujuan=@tujuan, harga=@harga, kelas=@kelas, jam=@jam, gerbong=@gerbong where kode=@kode"
                    Dim updateCmd As New MySqlCommand(updateQuery, conn)
                    updateCmd.Parameters.AddWithValue("@asal", txtasal.Text)
                    updateCmd.Parameters.AddWithValue("@tujuan", txtTujuan.Text)
                    updateCmd.Parameters.AddWithValue("@harga", txtHarga.Text)
                    If rbPria.Checked Then
                        updateCmd.Parameters.AddWithValue("@kelas", rbPria.Text)
                    Else
                        updateCmd.Parameters.AddWithValue("@kelas", rbPerempuan.Text)
                    End If
                    updateCmd.Parameters.AddWithValue("@jam", DateTime.Now.ToString("yyyy-MM-dd"))
                    updateCmd.Parameters.AddWithValue("@gerbong", cbgerbong.SelectedItem.ToString())
                    updateCmd.Parameters.AddWithValue("@kode", txtkode.Text)
                    updateCmd.ExecuteNonQuery()
                    MsgBox("Data berhasil diubah")
                    tampilJenis()
                    Kosong()
                Else
                    MsgBox("Kode Jenis belum ada")
                End If
            Catch ex As Exception
                MsgBox("Error: " & ex.Message)
            Finally
                conn.Close()
            End Try
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Hide()
    End Sub
    Sub tampilgerbong()
        CMD = New MySqlCommand("select gerbong from tbgerbong", CONN)
        RD = CMD.ExecuteReader
        Do While RD.Read
            cbgerbong.Items.Add(RD.Item(0))
        Loop
        RD.Close()
    End Sub
End Class

Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Public Class Form8
    Dim pageWidth As Integer = 827
    Dim pageHeight As Integer = 1169
    Dim listpesanan As New List(Of List(Of String))()
    Dim currentPage, totalPage, totalItem, marginPixels, y, x, marginRight
    Dim marginInch As Single

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        tampilpesanan()
    End Sub

    Sub tampilpesanan()
        DA = New MySqlDataAdapter("SELECT * FROM tbpesanan", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tbpesanan")



        DataGridView1.DataSource = DS.Tables("tbpesanan")
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
        DataGridView1.Columns(7).Width = 150
        DataGridView1.Columns(8).Width = 150
        DataGridView1.Columns(0).HeaderText = "Kode Pesanan"
        DataGridView1.Columns(1).HeaderText = "Nama"
        DataGridView1.Columns(2).HeaderText = "No Hp"
        DataGridView1.Columns(3).HeaderText = "Asal"
        DataGridView1.Columns(4).HeaderText = "Tujuan"
        DataGridView1.Columns(5).HeaderText = "Harga"
        DataGridView1.Columns(6).HeaderText = "Kelas"
        DataGridView1.Columns(7).HeaderText = "Tanggal Keberangkatan"
        DataGridView1.Columns(8).HeaderText = "Gerbong"
    End Sub
    Private Sub readDatapesanan()
        CMD = New MySqlCommand("select * from tbpesanan order by id_pesan",
        CONN)
        RD = CMD.ExecuteReader
        totalItem = 0
        Do While RD.Read
            Dim datapesanan As New List(Of String)()
            datapesanan.Add(RD("id_pesan").ToString)
            datapesanan.Add(RD("nama").ToString)
            datapesanan.Add(RD("nohp").ToString)
            datapesanan.Add(RD("asal").ToString)
            datapesanan.Add(RD("tujuann").ToString)
            datapesanan.Add(RD("kelass").ToString)
            datapesanan.Add(RD("hargaa").ToString)
            datapesanan.Add(RD("jamm").ToString)
            datapesanan.Add(RD("gerbongg").ToString)
            listpesanan.Add(datapesanan)
            totalItem += 1
        Loop
        totalPage = Math.Ceiling(totalItem / 3)
        RD.Close()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin menghapus pesanan ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.Yes Then
                Try
                    Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
                    Dim id_pesan As Integer = Convert.ToInt32(selectedRow.Cells(0).Value)

                    koneksi()
                    CMD = New MySqlCommand("DELETE FROM tbpesanan WHERE id_pesan = @id_pesan", CONN)
                    CMD.Parameters.AddWithValue("@id_pesan", id_pesan)
                    CMD.ExecuteNonQuery()

                    MessageBox.Show("Pesanan berhasil dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    tampilpesanan() ' Refresh data pesanan
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If CONN.State = ConnectionState.Open Then
                        CONN.Close()
                    End If
                End Try
            End If
        Else
            MessageBox.Show("Pilih pesanan yang ingin dihapus terlebih dahulu.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        readDatapesanan()
        currentPage = 1
        PrintDocument1.Print()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim Fheader As New Font("Times New Roman", 24, FontStyle.Bold)
        Dim FBodyB As New Font("Times New Roman", 14, FontStyle.Bold)
        Dim FBody As New Font("Times New Roman", 14, FontStyle.Regular)

        Dim black As SolidBrush = New SolidBrush(Color.Black)
        Dim center As New StringFormat()
        center.Alignment = StringAlignment.Center
        Dim posY, i As Integer
        Dim hitung As Integer = 0
        If currentPage <= 1 Then
            marginInch = 2.54F
            marginPixels = CInt(e.PageSettings.PrinterResolution.X * marginInch) '
            e.PageSettings.Margins = New Margins(marginPixels, marginPixels, marginPixels, marginPixels)
            x = e.MarginBounds.Left
            y = e.MarginBounds.Top
            marginRight = e.MarginBounds.Right
            'judul
            e.Graphics.DrawString("Data Pesanan", Fheader, black, pageWidth /
            2, y, center)
            posY = y + 70
        Else
            posY = y
        End If
        e.Graphics.DrawLine(Pens.Black, x, posY, marginRight, posY)
        For i = (currentPage - 1) * 3 To totalItem - 1
            e.Graphics.DrawString("Kode Pesanan", FBody, black, x + 20, posY +
            30)
            e.Graphics.DrawString(": " & listpesanan(i)(0).ToString, FBody, black, x + 200, posY + 30)
            e.Graphics.DrawString("Nama", FBody, black, x + 20, posY + 60)
            e.Graphics.DrawString(": " & listpesanan(i)(1).ToString, FBody, black, x + 200, posY + 60)
            e.Graphics.DrawString("No HP", FBody, black, x + 20, posY + 90)
            e.Graphics.DrawString(": " & listpesanan(i)(2).ToString, FBody, black, x + 200, posY + 90)
            e.Graphics.DrawString("Asal Kereta", FBody, black, x + 20, posY + 120)
            e.Graphics.DrawString(": " & listpesanan(i)(3).ToString, FBody, black, x + 200, posY + 120)
            e.Graphics.DrawString("Tujuan Kereta", FBody, black, x + 20, posY + 150)
            e.Graphics.DrawString(": " & listpesanan(i)(4).ToString, FBody, black, x + 200, posY + 150)
            e.Graphics.DrawString("Harga Tiket", FBody, black, x + 20, posY + 180)
            e.Graphics.DrawString(": " & listpesanan(i)(5).ToString, FBody, black, x + 200, posY + 180)
            e.Graphics.DrawString("Kelas", FBody, black, x + 20, posY + 210)
            e.Graphics.DrawString(": " & listpesanan(i)(6).ToString, FBody, black, x + 200, posY + 210)
            e.Graphics.DrawString("Tanggal pesanan", FBody, black, x + 20, posY + 240)
            e.Graphics.DrawString(": " & listpesanan(i)(7).ToString, FBody, black, x + 200, posY + 240)
            e.Graphics.DrawString("Gerbong Kereta", FBody, black, x + 20, posY + 270)
            e.Graphics.DrawString(": " & listpesanan(i)(8).ToString, FBody, black, x + 200, posY + 270)
            e.Graphics.DrawLine(Pens.Black, x, posY + 280, marginRight, posY + 310)
            posY += 270
            hitung += 1
            If hitung >= 3 Then
                Exit For
            End If
        Next
        If currentPage <= 1 Then
            e.Graphics.DrawLine(Pens.Black, x, y + 70, x, posY + 10)
            e.Graphics.DrawLine(Pens.Black, marginRight, y + 70,
            marginRight, posY + 10)
        Else
            e.Graphics.DrawLine(Pens.Black, x, y, x, posY + 10)
            e.Graphics.DrawLine(Pens.Black, marginRight, y, marginRight,
            posY + 10)
        End If
        currentPage += 1
        e.HasMorePages = currentPage <= totalPage
    End Sub
End Class
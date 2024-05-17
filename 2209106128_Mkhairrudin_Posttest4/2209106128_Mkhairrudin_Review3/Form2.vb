Imports MySql.Data.MySqlClient
Public Class Form2
    Dim x, y As Integer

    Public id_user As Integer
    Dim newpoint As New System.Drawing.Point

    Private Sub Panel2_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel2.MouseDown
        x = Control.MousePosition.X - Me.Location.X
        y = Control.MousePosition.Y - Me.Location.Y
    End Sub

    Private Sub Panel2_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel2.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            newpoint = Control.MousePosition
            newpoint.X -= (x)
            newpoint.Y -= (y)
            Me.Location = newpoint
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel2.Left = Me.Width
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Panel2.Left > 12 Then
            Panel2.Left = Int(Panel2.Left - 20)
        Else
            Panel2.Left = 12
        End If
    End Sub

    Private Sub login_berhasil()
        Form4.Show()
    End Sub

    Private Sub login_berhasil2()
        Dim form5 As New Form5(id_user)
        form5.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim connStr As String = "server=localhost;user id=root;password=;database=dbtiket"
        Using conn As New MySqlConnection(connStr)
            Try
                conn.Open()
                Dim query As String = "SELECT * FROM user WHERE username = @username AND password = @password"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@username", user.Text)
                    cmd.Parameters.AddWithValue("@password", password.Text)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            id_user = reader("id_user")
                            MessageBox.Show("Login berhasil! ID User: " & id_user) ' Tambahkan ini untuk debugging
                            If user.Text = "admin" Then
                                login_berhasil()
                            Else
                                login_berhasil2()
                            End If
                        Else
                            MessageBox.Show("Username atau password salah")
                        End If
                    End Using
                End Using
            Catch ex As MySqlException
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    End Sub

    Private Sub btnRegis_Click(sender As Object, e As EventArgs) Handles btnRegis.Click
        Form7.Show()
    End Sub
End Class

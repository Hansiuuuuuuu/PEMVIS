Imports MySql.Data.MySqlClient
Public Class Form7
    Dim x, y As Integer

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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub btnRegis_Click(sender As Object, e As EventArgs) Handles btnRegis.Click
        Dim connStr As String = "server=localhost;user id=root;password=;database=dbtiket"
        Using conn As New MySqlConnection(connStr)
            Try
                conn.Open()

                ' Check if the username already exists
                Dim checkQuery As String = "SELECT COUNT(*) FROM user WHERE username = @username"
                Using checkCmd As New MySqlCommand(checkQuery, conn)
                    checkCmd.Parameters.AddWithValue("@username", user.Text)
                    Dim userExists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())

                    If userExists > 0 Then
                        MessageBox.Show("Username sudah ada. Silakan pilih username lain.")
                    Else

                        Dim query As String = "INSERT INTO user (username, password) VALUES (@username, @password)"
                        Using cmd As New MySqlCommand(query, conn)
                            cmd.Parameters.AddWithValue("@Username", user.Text)
                            cmd.Parameters.AddWithValue("@Password", password.Text)
                            cmd.ExecuteNonQuery()
                        End Using
                        MessageBox.Show("Registrasi berhasil!")
                        Me.Close()
                    End If
                End Using

            Catch ex As MySqlException
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub
End Class

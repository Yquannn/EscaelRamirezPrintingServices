Public Class signup
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Password_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub UserName_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If UserType.Text = "Admin" AndAlso (UserName.Text = "Admin" OrElse UserName.Text = "admin") AndAlso (Password.Text = "Admin" OrElse Password.Text = "admin") Then
            Me.Hide()
            AdminMod.Show()
            UserType.Text = ""
            UserName.Text = ""
            Password.Text = ""
        ElseIf UserType.Text = "Cashier" AndAlso (UserName.Text = "Cashier@gmail.com" OrElse UserName.Text = "cashier@gmail.com") AndAlso (Password.Text = "Cashier" OrElse Password.Text = "cashier") Then
            Me.Hide()
            caashier.Show()
            UserType.Text = ""
            UserName.Text = ""
            Password.Text = ""

        ElseIf UserType.Text = "Inventory Clerk" AndAlso (UserName.Text = "InventoryClerk@gmail.com" OrElse UserName.Text = "inventoryClerk@gmail.com") AndAlso (Password.Text = "InventoryClerk" OrElse Password.Text = "inventoryclerk") Then
            Me.Hide()
            InvenClerk.Show()
            UserType.Text = ""
            UserName.Text = ""
            Password.Text = ""
        Else
            MsgBox("Wrong password or email!")
        End If



    End Sub

End Class
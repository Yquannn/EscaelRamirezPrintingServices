
Public Class OrderSummaryForm
    Public Property OrderSummary As String

    Public Property TotalPrice As Decimal
    Public Property paymentOption As String = ""




    Private Sub OrderSummaryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = OrderSummary

        ' Display the total price in the Label
        'TotalPriceLabel.Text = "Total Price: " & TotalPrice

    End Sub

    Private Sub fpayment_CheckedChanged(sender As Object, e As EventArgs) Handles fpayment.CheckedChanged
        paymentOption = "Full Payment"
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click

        Dim AdminInstance = AdminMod


        Dim paymentInformation As String = PaymentTextBox.Text.Trim()

        If Not String.IsNullOrEmpty(paymentOption) Then
            MessageBox.Show("Payment Option: " & paymentOption & vbCrLf & "Payment Information: " & paymentInformation, "Payment Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If RichTextBox1 IsNot Nothing Then
                RichTextBox1.Clear()
            End If


            AdminInstance.InsertDB()

            'Dim var_stocks As Integer = 0
            'Dim var_sell As Integer = 0
            'Dim result As Integer = 0

            'If Integer.TryParse(item.ProdQty.Text, var_stocks) AndAlso Integer.TryParse(item.Qty.Text, var_sell) Then
            '    result = var_stocks - var_sell


            '    Con.Open()
            '        Dim cmd As New SqlCommand("UPDATE ProductTb1 SET Quantity=@result WHERE ProductId=@prodId", Con)
            '        cmd.Parameters.AddWithValue("@result", result)
            '        cmd.Parameters.AddWithValue("@prodId", item.ProdId.Text)
            '        cmd.ExecuteNonQuery()

            '    ' Rest of your code here...
            'Else
            '    MsgBox("Invalid quantity input")
            'End If


            If paymentOption = "" Then
                MessageBox.Show("Please select a payment option.", "Payment Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If


        End If


    End Sub

    Private Sub dpayment_CheckedChanged(sender As Object, e As EventArgs) Handles dpayment.CheckedChanged
        paymentOption = "Down Payment"
    End Sub

    Private Sub PaymentTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PaymentTextBox.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub PaymentTextBox_TextChanged(sender As Object, e As EventArgs) Handles PaymentTextBox.TextChanged

    End Sub
End Class
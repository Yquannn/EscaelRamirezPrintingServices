Imports System.Data.SqlClient
Imports System.Text
Public Class AdminMod
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Dim adp As SqlDataAdapter
    Dim strPayment As String
    Public Property OrderSummary As String




    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub TabPage4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TabControl1.SelectedTab = TabPage3
        CalcTotal()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        CalcTotalReceived()
        TabControl1.SelectedTab = TabPage4
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub
    Public Sub populate()
        Con.Open()

        Dim sql As String = "SELECT referenceNumber, Customer_Name, Contact_Number, Address, Date, Total, Payment_Status, Payment, Product_Order FROM ProductTb4"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As New DataSet()
        adapter.Fill(ds)
        ProductDVG3.DataSource = ds.Tables(0)
        Con.Close()
    End Sub

    Private Sub fillCategory()

        Con.Open()
        Dim sql = "Select * from ProductTb1"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim tb1 As New DataTable()
        adapter.Fill(tb1)
        Order.DataSource = tb1
        Order.DisplayMember = "ProductName"
        Order.ValueMember = "ProductName"
        If Order.Items.Count > 1 Then
            Order.SelectedIndex = -1
            'Order.SelectedText = "Select Order"
        End If

        Con.Close()
    End Sub



    Private Sub FillCustomer()

        Con.Open()
        Dim sql = "Select * from ProductTb4"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim tb1 As New DataTable()
        adapter.Fill(tb1)
        id.DataSource = tb1
        id.DisplayMember = "referenceNumber"
        id.ValueMember = "referenceNumber"

        If id.Items.Count > 1 Then
            id.SelectedIndex = -1
            'id.SelectedText = "Enter Id/Choose Id"
        End If

        Con.Close()
    End Sub
    Private Sub FillContact()
        Con.Open()
        Dim query = "SELECT * FROM ProductTb4 WHERE referenceNumber='" & id.SelectedValue.ToString() & "'"
        Dim cmd As New SqlCommand(query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            Number.Text = reader(2).ToString
        End While
        Con.Close()
    End Sub





    Private Sub FillAddress()
        Con.Open()
        Dim query = "SELECT * FROM ProductTb4 WHERE id='" & id.SelectedValue.ToString() & "'"
        Dim cmd As New SqlCommand(query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()

        While reader.Read
            address.Text = reader(3).ToString()
        End While



        Con.Close()

    End Sub

    Private Sub FillPrice()
        If Order IsNot Nothing AndAlso Order.SelectedValue IsNot Nothing Then
            Try
                Con.Open()
                Dim query As String = "SELECT * FROM ProductTb1 WHERE ProductName = @ProductName"
                Dim cmd As New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@ProductName", Order.SelectedValue.ToString())

                Dim dt As New DataTable
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Price.Text = "₱" + reader(3).ToString() ' add the peso sign before the value
                    End If
                End Using
            Catch ex As Exception
                MsgBox("An error occurred: " & ex.Message)
            Finally
                Con.Close()
            End Try
        End If
    End Sub




    'Private Sub FillOrder()
    '    Con.Open()
    '    Dim query As String = "SELECT * FROM ProductTb4 WHERE Product_Order=@Product_Order"
    '    Dim cmd As New SqlCommand(query, Con)
    '    cmd.Parameters.AddWithValue("@Product_Order", Order.SelectedValue.ToString())

    '    Dim dt As New DataTable
    '    Dim reader As SqlDataReader = cmd.ExecuteReader()

    '    If reader.Read() Then
    '        GroupBox1.Text = reader("Product_Order").ToString() ' Replace "ColumnName" with the actual column name containing the desired value
    '    End If

    '    reader.Close()
    '    Con.Close()
    'End Sub

    Private Sub fillId()
        If Order IsNot Nothing AndAlso Order.SelectedValue IsNot Nothing Then
            Con.Close() ' Close the connection if it's already open
            Con.Open()
            Dim query = "SELECT * FROM ProductTb1 WHERE ProductName = @ProductName"
            Dim cmd As New SqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@ProductName", Order.SelectedValue.ToString())

            Dim dt As New DataTable
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                ProdId.Text = reader(0).ToString()
            End If

            reader.Close()
            Con.Close()
        End If
    End Sub






    Private Sub fetchId()
        Con.Close()
        Con.Open()
        Dim query As String = "SELECT ProductId FROM ProductTb1 WHERE ProductName=@productName"
        Dim cmd As New SqlCommand(query, Con)
        If Order.SelectedValue IsNot Nothing Then
            cmd.Parameters.AddWithValue("@productName", Order.SelectedValue.ToString())
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim IdValue As String = reader("ProductId").ToString()
                ProdId.Text = IdValue
            End If
            reader.Close()
            Con.Close()
        Else
            ' Handle the case when Order.SelectedValue is null
            ' You can assign a default value or display an error message
        End If

    End Sub




    Private Sub fetchQty()
        If Order IsNot Nothing AndAlso Order.SelectedValue IsNot Nothing Then
            Try
                Con.Open()
                Dim query As String = "SELECT ProductId, Quantity FROM ProductTb1 WHERE ProductName = @productName"
                Dim cmd As New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@productName", Order.SelectedValue.ToString())
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                If reader.Read() Then
                    Dim qtyValue As String = reader("Quantity").ToString()
                    ProdQty.Text = qtyValue
                End If

                reader.Close()
            Catch ex As Exception
                MsgBox("An error occurred: " & ex.Message)
            Finally
                Con.Close()
            End Try
        End If
    End Sub




    Private Sub ProductDVG3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ProductDVG3.CellContentClick
        fethPayment.Text = ""
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = Me.ProductDVG3.Rows(e.RowIndex)
            id.Text = row.Cells("referenceNumber").Value.ToString()
            Name.Text = row.Cells("Customer_Name").Value.ToString()
            Number.Text = row.Cells("Contact_Number").Value.ToString()
            address.Text = row.Cells("Address").Value.ToString()
            OrderItem.Text = row.Cells("Product_Order").Value.ToString()
            Price.Text = row.Cells("Total").Value.ToString()
            DateTime2.Text = row.Cells("Date").Value.ToString()
            fethPayment.Text = row.Cells("Payment").Value.ToString()

            fetchPaymenOp()
            fetchId()
            FillPrice()
            fetchQty()
            fetchProd()
            fetchPrice()
            fetchIPayment()

            If String.IsNullOrEmpty(RichTextBox1.Text) Then
                ShowBill2()
                Price.Text = ""

            Else
                RichTextBox1.Clear()
                ShowBill2()
                Price.Text = ""
            End If

        End If
    End Sub



    Private Sub fetchProd()

        If id.SelectedValue IsNot Nothing Then
            Con.Close()
            Con.Open()
            Dim query = "SELECT * FROM ProductTb4 WHERE referenceNumber='" & id.SelectedValue.ToString() & "'"

            Dim cmd As New SqlCommand(query, Con)
            Dim dt As New DataTable
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader()
            While reader.Read
                OrderItem.Text = reader(4).ToString()
            End While
            Con.Close()
        End If
    End Sub

    Private Sub fetchPrice()
        If id.SelectedValue IsNot Nothing Then
            Con.Open()
            Dim query = "SELECT * FROM ProductTb4 WHERE referenceNumber='" & id.SelectedValue.ToString() & "'"

            Dim cmd As New SqlCommand(query, Con)
            Dim dt As New DataTable
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader()
            While reader.Read
                Price.Text = reader(6).ToString()
            End While
            Con.Close()
        End If
    End Sub

    Private Sub fetchPaymenOp()
        If id.SelectedValue IsNot Nothing Then
            Con.Open()
            Dim query = "SELECT * FROM ProductTb4 WHERE referenceNumber='" & id.SelectedValue.ToString() & "'"

            Dim cmd As New SqlCommand(query, Con)
            Dim dt As New DataTable
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader()
            While reader.Read
                PaymentOp.Text = reader(7).ToString()
            End While
            Con.Close()
        End If
    End Sub
    Private Sub fetchIPayment()


        Try
                Con.Open()
            Dim query As String = "SELECT * FROM ProductTb4 WHERE Payment = @payment"
            Dim cmd As New SqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@payment", fethPayment.Text)

            Dim dt As New DataTable
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                    fethPayment.Text = reader(8).ToString() ' add the peso sign before the value
                End If
                End Using
            Catch ex As Exception
                MsgBox("An error occurred: " & ex.Message)
            Finally
                Con.Close()
            End Try

    End Sub




    Private Sub ShowBill()
        RichTextBox1.Text = "      Escabel Ramirez Printing Services              " & vbCrLf
        RichTextBox1.Text += "               Rosario Batangas                       " & vbCrLf
        RichTextBox1.Text += "               Tel + 09321313643                        " & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf
        RichTextBox1.Text += "Ref No. :  " & id.Text & "  " & DateTime.Now.ToString() & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf
        RichTextBox1.Text += "Customer Name    :    " & Name.Text & vbCrLf
        RichTextBox1.Text += "Contact Number   :    " & Number.Text & vbCrLf
        RichTextBox1.Text += "Address          :    " & address.Text & vbCrLf
        RichTextBox1.Text += "Product Order    :    " & OrderItem.Text & vbCrLf
        'RichTextBox1.Text += "Quantity         :    x" & Qty.Text & vbCrLf
        RichTextBox1.Text += "Payment Option   :    " & paymentOption & vbCrLf
        RichTextBox1.Text += "Pick Up Schedule :    " & DateTime2.Text & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf
        RichTextBox1.Text += "" & paymentOption & "     :    " & ChrW(&H20B1) & PaymentTextBox.Text & ".00" & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf
        RichTextBox1.Text += "             " & TotalPriceLabel.Text & ".00" & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf
        RichTextBox1.Text += "            Thank you for Shopping                  " & vbCrLf

    End Sub

    Private Sub ShowBill2()
        RichTextBox1.Text = "      Escabel Ramirez Printing Services              " & vbCrLf
        RichTextBox1.Text += "               Rosario Batangas                       " & vbCrLf
        RichTextBox1.Text += "               Tel + 09321313643                        " & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf
        RichTextBox1.Text += "Ref No. :  " & id.Text & "  " & DateTime.Now.ToString() & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf
        RichTextBox1.Text += "Customer Name    :    " & Name.Text & vbCrLf
        RichTextBox1.Text += "Contact Number   :    " & Number.Text & vbCrLf
        RichTextBox1.Text += "Address          :    " & address.Text & vbCrLf
        RichTextBox1.Text += "Product Order    :    " & OrderItem.Text & vbCrLf
        'RichTextBox1.Text += "Quantity         :    x" & Qty.Text & vbCrLf
        RichTextBox1.Text += "Pick Up Schedule :    " & DateTime2.Text & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf


        RichTextBox1.Text += "                  " & PaymentOp.Text & vbCrLf
        RichTextBox1.Text += "                     " & ChrW(&H20B1) & fethPayment.Text & ".00" & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf
        RichTextBox1.Text += "                  Total: " & Price.Text & ".00" & vbCrLf
        RichTextBox1.Text += "************************************************" & vbCrLf
        RichTextBox1.Text += "            Thank you for Shopping                  " & vbCrLf

    End Sub


    'Private Sub ShowBill3()
    '    RichTextBox1.Text = "      Escabel Ramirez Printing Services              " & vbCrLf
    '    RichTextBox1.Text += "               Rosario Batangas                       " & vbCrLf
    '    RichTextBox1.Text += "               Tel + 09321313643                        " & vbCrLf
    '    RichTextBox1.Text += "************************************************" & vbCrLf
    '    RichTextBox1.Text += "Ref No. :  " & id.Text & "  " & DateTime.Now.ToString() & vbCrLf
    '    RichTextBox1.Text += "************************************************" & vbCrLf
    '    RichTextBox1.Text += "Customer Name    :    " & Name.Text & vbCrLf
    '    RichTextBox1.Text += "Contact Number   :    " & Number.Text & vbCrLf
    '    RichTextBox1.Text += "Address          :    " & address.Text & vbCrLf
    '    RichTextBox1.Text += "Product Order    :    " & OrderItem.Text & vbCrLf 'Product Order
    '    'RichTextBox1.Text += "Quantity         :    x" & Qty.Text & vbCrLf
    '    RichTextBox1.Text += "Pick Up Schedule :    " & DateTime2.Text & vbCrLf
    '    RichTextBox1.Text += "************************************************" & vbCrLf
    '    RichTextBox1.Text += "                  " & PaymentOp.Text & vbCrLf
    '    RichTextBox1.Text += "                     " & ChrW(&H20B1) & fethPayment.Text & ".00" & vbCrLf
    '    RichTextBox1.Text += "************************************************" & vbCrLf
    '    RichTextBox1.Text += "                  Total: " & Price.Text & ".00" & vbCrLf
    '    RichTextBox1.Text += "************************************************" & vbCrLf
    '    RichTextBox1.Text += "            Thank you for Shopping                  " & vbCrLf

    'End Sub



    Private Sub Search_TextChanged(sender As Object, e As EventArgs) Handles Search.TextChanged
        Dim query As String = "SELECT * FROM ProductTb4 WHERE Customer_Name LIKE '%" & Search.Text & "%' OR referenceNumber LIKE '%" & Search.Text & "%'"

        Using Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30")
            Using cmd As New SqlCommand(query, Con)
                Using da As New SqlDataAdapter()
                    da.SelectCommand = cmd
                    Using dt As New DataTable()
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            ProductDVG3.DataSource = dt



                        Else
                            MsgBox("No record found!")
                            Search.Text = ""
                            RichTextBox1.Clear()
                        End If
                    End Using
                End Using
            End Using
        End Using


    End Sub


    Private Function GetDataTableFromDatabase() As DataTable
        ' Create a new SqlConnection and SqlCommand objects
        Dim connectionString As String = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30"
        Dim connection As New SqlConnection(connectionString)
        Dim command As New SqlCommand("SELECT * FROM ProductTb1", connection)

        ' Create a new DataTable to store the data from the database
        Dim dataTable As New DataTable()

        ' Use a SqlDataAdapter to fill the DataTable with data from the database
        Dim adapter As New SqlDataAdapter(command)
        adapter.Fill(dataTable)

        ' Return the filled DataTable as the new data source
        Return dataTable
    End Function




    Public Property paymentOption As String = ""

    Public Sub InsertDB()
        Dim myid As Guid = Guid.NewGuid()
        Dim myidstring = myid.ToString().Substring(0, 10)
        id.Text = myidstring
        Dim totalPrice As Decimal = TotalPrice2
        Dim paymentOp As String = paymentOption

        Try


            Dim query As String = "INSERT INTO ProductTb4 (referenceNumber, Customer_Name, Contact_Number, Address, Product_Order, Date, Payment_Status, Total, Payment) VALUES (@referenceNumber, @Customer_Name, @Contact_Number, @Address, @selectedItems, GETDATE(), @payment_Statuss, @totalValue, @payment)"
            Dim cmd As SqlCommand = New SqlCommand(query, Con)

            cmd.Parameters.Add("@referenceNumber", SqlDbType.NVarChar, 10).Value = id.Text
            cmd.Parameters.Add("@Customer_Name", SqlDbType.NVarChar, 50).Value = Name.Text.ToUpper()
            cmd.Parameters.Add("@Contact_Number", SqlDbType.NVarChar, 20).Value = Number.Text
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = address.Text.ToUpper()
            cmd.Parameters.Add("@payment_Statuss", SqlDbType.NVarChar, 50).Value = paymentOp
            cmd.Parameters.Add("@totalValue", SqlDbType.Decimal).Value = HoldPrice
            cmd.Parameters.Add("@selectedItems", SqlDbType.NVarChar, -1).Value = OrderItem.Text.ToUpper
            cmd.Parameters.Add("@payment", SqlDbType.NVarChar).Value = PaymentTextBox.Text


            Dim querys As String = "INSERT INTO OrderReceived (refNum, Customer_Name, [Date], Total, id, Contact_Number, Address, Product_Order) VALUES (@refNum, @Customer_Name, GETDATE(), @TotalValue2, @id, @Contact_Number, @Address, @Product_Order)"
            Dim cmds As SqlCommand = New SqlCommand(querys, Con)

            cmds.Parameters.AddWithValue("@refNum", id.Text)
            cmds.Parameters.AddWithValue("@Customer_Name", Name.Text.ToUpper())
            cmds.Parameters.AddWithValue("@TotalValue2", totalPrice)
            cmds.Parameters.AddWithValue("@id", id.Text)
            cmds.Parameters.AddWithValue("@Contact_Number", Number.Text)
            cmds.Parameters.AddWithValue("@Address", address.Text.ToUpper())
            cmds.Parameters.AddWithValue("@Product_Order", Order.Text.ToUpper())

            Con.Open()
            cmd.ExecuteNonQuery()
            cmds.ExecuteNonQuery()
            MsgBox("Added Successfully!")

            RichTextBox1.Clear()

            HoldPrice = 0

            TabPage5.Visible = False
            TabPage1.Visible = True

            Con.Close()




        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Con.Close()
        End Try

        populate()
    End Sub






    Private Sub Button16_Click(sender As Object, e As EventArgs)
        'If RichTextBox1 IsNot Nothing Then
        '    RichTextBox1.Clear()
        'End If

        'Dim myId As Guid = Guid.NewGuid()
        'Dim myIdString = myId.ToString().Substring(0, 10)
        'id.Text = myIdString

        'Try
        '    If Name.Text = "" Or Number.Text = "" Or address.Text = "" Or Qty.Text = "" Then
        '        MsgBox("Please Complete details before adding order!")
        '    Else

        '        Dim var_stocks As Integer = 0
        '        Dim var_sell As Integer = 0
        '        Dim result As Integer = 0

        '        var_stocks = Convert.ToInt32(ProdQty.Text)
        '        var_sell = Convert.ToInt32(Qty.Text)
        '        result = var_stocks - var_sell

        '        If var_stocks <= 0 Then
        '            MsgBox("Product Sold out")
        '            Qty.Text = ""
        '        ElseIf var_stocks < var_sell Then
        '            MsgBox("Low stocked!")
        '            Qty.Text = ""
        '        Else
        '            Con.Open()
        '            Dim cmds As New SqlCommand("SELECT * FROM ProductTb1", Con)
        '            cmds.CommandText = "UPDATE ProductTb1 SET Quantity='" + result.ToString() + "' WHERE ProductId='" + ProdId.Text + "'"
        '            cmds.ExecuteNonQuery()
        '            Con.Close()
        '            InsertDB()
        '        End If
        '    End If

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try


    End Sub


    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If id.Text = "" Then
            MsgBox("Select Customer to be Order Received!")
        Else

            adp = New SqlDataAdapter("SELECT refNum, Customer_Name, Date, Total FROM OrderReceived WHERE id LIKE '%" & id.Text & "%'", Con)


            Dim dt As New DataTable
            adp.Fill(dt)

            For Each drow As DataRow In dt.Rows
                myListView.Items.Add(drow(0).ToString())
                myListView.Items(myListView.Items.Count - 1).SubItems.Add(drow(1).ToString())
                myListView.Items(myListView.Items.Count - 1).SubItems.Add(drow(2).ToString())
                myListView.Items(myListView.Items.Count - 1).SubItems.Add(drow(3).ToString())
                myListView.Items(myListView.Items.Count - 1).SubItems.Add(drow(3).ToString())
                myListView.Items(myListView.Items.Count - 1).SubItems.Add(drow(3).ToString())


            Next
            Con.Close()
            MsgBox("Product received!")

            Dim querys As String = "DELETE FROM productTb4 WHERE referenceNumber = @referenceNumber"
            Dim cmds As SqlCommand
            cmds = New SqlCommand(querys, Con)
            cmds.Parameters.AddWithValue("@referenceNumber", id.Text)
            Con.Open()
            cmds.ExecuteNonQuery()
            Con.Close()
            clear()
            populate()
            'Application.Restart()
        End If


    End Sub
    Private Sub clear()
        Name.Text = ""
        Number.Text = ""
        address.Text = ""
        Qty.Text = ""
        Order.Text = ""
        id.Text = ""
        Price.Text = ""
        DateTime2.Text = ""
        ProdId.Text = ""
        ProdQty.Text = ""
        Search.Text = ""
        ProductName.Text = ""
        Quantity.Text = ""
        pr.Text = ""
        RichTextBox1.Clear()
    End Sub
    Public Sub populateForOR()
        Con.Open()

        Dim sql As String = "SELECT * FROM OrderReceived"
        Dim cmd As New SqlCommand(sql, Con)
        Dim reader As SqlDataReader = cmd.ExecuteReader()

        ' Clear existing items in the ListView
        myListView.Items.Clear()

        While reader.Read()
            ' Read data from the SqlDataReader and create a new ListViewItem
            Dim item As New ListViewItem(reader("refNum").ToString())
            item.SubItems.Add(reader("Customer_Name").ToString())
            item.SubItems.Add(reader("Date").ToString())
            item.SubItems.Add(reader("Total").ToString())
            'item.SubItems.Add(reader("Quantity").ToString())
            item.SubItems.Add(reader("Product_Order").ToString())

            'item.SubItems.Add(reader("id").ToString())
            'item.SubItems.Add(reader("Contact_Number").ToString())
            'item.SubItems.Add(reader("Address").ToString())

            ' Add the ListViewItem to the ListView
            myListView.Items.Add(item)
        End While

        reader.Close()
        Con.Close()
    End Sub
    Private Sub TabPage4_Click_1(sender As Object, e As EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        clear()
        PaymentTextBox.Text = ""
        TotalPriceLabel.Text = ""
        PaymentOp.Text = ""
        fethPayment.Text = ""
        RichTextBox1.SelectedText = ""
        RichTextBox1.Text = String.Empty

        OrderItem.Text = ""
        OrderItem.Text = String.Empty

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If id.Text = "" Then
            MsgBox("select customer to cancel")

        Else
            Try
                Con.Close()
                Dim query As String = "DELETE FROM productTb4 WHERE referenceNumber = @referenceNumber"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@referenceNumber", id.Text)
                Con.Open()
                cmd.ExecuteNonQuery()
                Con.Close()
                MsgBox("Order cancelled!")
                populate()
                Dim var_stocks As Integer = 0
                Dim var_sell As Integer = 0
                Dim result As Integer = 0

                If Integer.TryParse(ProdQty.Text, var_stocks) AndAlso Integer.TryParse(Qty.Text, var_sell) Then
                    result = var_stocks + var_sell
                    Dim cmds As New SqlCommand("UPDATE ProductTb1 SET Quantity='" + result.ToString() + "' WHERE ProductId='" + ProdId.Text + "'", Con)
                    Con.Open()

                    cmds.ExecuteNonQuery()
                    Con.Close()
                    populate() ' moved inside the using block
                End If

                Con.Close()
                Dim newDataSource As Object = GetDataTableFromDatabase()
                RefreshDataGridView(ProductDVGLP, newDataSource)
                'Dim itemsForm As Items = CType(Application.OpenForms("Items"), Items)
                'Dim newDataSource As Object = GetDataTableFromDatabase()
                'RefreshDataGridView(itemsForm.ProductDVG, newDataSource)

                clear()

                FillCustomer()

            Catch ex As Exception
                MsgBox("An error occurred: " & ex.Message)
            End Try
        End If

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim pageBounds As RectangleF = e.PageSettings.PrintableArea
        Dim marginBounds As RectangleF = New RectangleF(pageBounds.Left + 50, pageBounds.Top + 50, pageBounds.Width - 100, pageBounds.Height - 100)

        Dim font As Font = RichTextBox1.Font
        Dim brush As Brush = New SolidBrush(RichTextBox1.ForeColor)
        Dim text As String = RichTextBox1.Text

        Dim format As StringFormat = New StringFormat()
        format.Alignment = StringAlignment.Near
        format.LineAlignment = StringAlignment.Near

        Dim textSize As SizeF = e.Graphics.MeasureString(text, font, marginBounds.Width, format)

        e.Graphics.DrawString(text, font, brush, marginBounds, format)

        ' If there is more text to print, indicate that the next page should be printed
        If textSize.Height + marginBounds.Top < marginBounds.Bottom Then
            e.HasMorePages = False
        Else
            e.HasMorePages = True
        End If
    End Sub


    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.Font = New Font("Courier New", 13)
    End Sub

    Private Sub Number_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Number.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Qty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Qty.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles search3.TextChanged
        Dim query As String = "SELECT * FROM OrderReceived WHERE Customer_Name LIKE '%" & search3.Text & "%' OR refNum LIKE '%" & search3.Text & "%'"


        Using Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30")
            Using cmd As New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@SearchText", "%" & search3.Text & "%")

                Using da As New SqlDataAdapter()
                    da.SelectCommand = cmd

                    Using dt As New DataTable()
                        da.Fill(dt)

                        If dt.Rows.Count > 0 Then
                            myListView.Items.Clear() ' Clear existing items

                            For Each row As DataRow In dt.Rows
                                Dim item As New ListViewItem(row("refNum").ToString())
                                item.SubItems.Add(row("Customer_Name").ToString())
                                item.SubItems.Add(row("Date").ToString())
                                item.SubItems.Add(row("Total").ToString())
                                'item.SubItems.Add(row("Quantity").ToString())
                                item.SubItems.Add(row("Product_Order").ToString())

                                ' Add other sub-items as needed
                                myListView.Items.Add(item)
                            Next

                            ' ... rest of your code ...

                        Else
                            MsgBox("No record found!")
                            search3.Text = ""
                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        '    MsgBox("Password is correct!")
        Try
            If String.IsNullOrWhiteSpace(ProductName.Text) OrElse String.IsNullOrWhiteSpace(Quantity.Text) OrElse String.IsNullOrWhiteSpace(pr.Text) Then
                MsgBox("Please add a product.")
            Else
                Con.Close()
                Con.Open()
                Dim query As String = "INSERT INTO ProductTb1 (ProductId, ProductName, Quantity, Price, DateTime) VALUES (@ProductId, @ProductName, @Quantity, @Price, GETDATE())"
                Using cmd As New SqlCommand(query, Con)
                    cmd.Parameters.AddWithValue("@ProductId", Guid.NewGuid().ToString().Substring(0, 10))
                    cmd.Parameters.AddWithValue("@ProductName", ProductName.Text.ToUpper())
                    cmd.Parameters.AddWithValue("@Quantity", Integer.Parse(Quantity.Text))
                    cmd.Parameters.AddWithValue("@Price", Decimal.Parse(pr.Text))
                    cmd.ExecuteNonQuery()
                    Con.Close()

                End Using
                MsgBox("Product added successfully!")
                ProductId.Text = ""
                ProductName.Text = ""
                Quantity.Text = ""
                pr.Text = ""
                populateForPA()

            End If
        Catch ex As Exception
            MsgBox("An error occurred: " & ex.Message)
        End Try
    End Sub
    Public Sub populate2()
        Con.Open()

        Dim sql As String = "SELECT * FROM OrderReceived"
        Dim cmd As New SqlCommand(sql, Con)
        Dim reader As SqlDataReader = cmd.ExecuteReader()

        ' Clear existing items in the ListView
        myListView.Items.Clear()

        While reader.Read()
            ' Read data from the SqlDataReader and create a new ListViewItem
            Dim item As New ListViewItem(reader("refNum").ToString())
            item.SubItems.Add(reader("Customer_Name").ToString())
            item.SubItems.Add(reader("Date").ToString())
            item.SubItems.Add(reader("Total").ToString())
            'item.SubItems.Add(reader("Quantity").ToString())
            item.SubItems.Add(reader("Product_Order").ToString())

            'item.SubItems.Add(reader("id").ToString())
            'item.SubItems.Add(reader("Contact_Number").ToString())
            'item.SubItems.Add(reader("Address").ToString())

            ' Add the ListViewItem to the ListView
            myListView.Items.Add(item)
        End While

        reader.Close()
        Con.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        idOf.Text = ""
        fethPayment.Text = ""

        'OrderSum()

        RichTextBox2.Text = OrderSummary
        ' Display the total price in the Label
        'TotalPriceLabel.Text = "Total Price: " & TotalPrice2.ToString()

        populateForOtherOrder()
        populateForPA()
        fillCategory()
        FillCustomer()
        populate()
        populate2()
        populatedMat()
        populateForOR()
        ProductDVGLP.Refresh()
        fetchId2()
        fetchId3()
        fetchId4()

        populateForSubCat()
        populateForColor()
    End Sub

    Public Sub populateForPA()
        Con.Open()
        Dim sql = "Select * from ProductTb1"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, Con)
        Dim builder = New SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ProductDVGLP.DataSource = ds.Tables(0)
        Con.Close()

    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click
        populateForPA()
        ProductDVGLP.Refresh()
    End Sub

    Private Sub Quantity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Quantity.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub pr_KeyPress(sender As Object, e As KeyPressEventArgs) Handles pr.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click


        If ProductId.Text = "" Or Quantity.Text = "" Then
            MsgBox("Please Fill up the data you want to Update")
        Else
            Con.Open()
            Dim sql = "UPDATE ProductTb1 SET ProductName=@ProductName, Quantity=@Quantity, Price=@Price WHERE ProductId=@ProductId"
            Dim cmd As New SqlCommand(sql, Con)
            cmd.Parameters.AddWithValue("@ProductName", ProductName.Text)
            cmd.Parameters.AddWithValue("@Quantity", Quantity.Text)
            cmd.Parameters.AddWithValue("@Price", pr.Text)
            cmd.Parameters.AddWithValue("@ProductId", ProductId.Text)
            cmd.ExecuteNonQuery()
            MsgBox("Materials Updated!")
            Con.Close()
            populateForPA()
            ProductId.Text = ""
            ProductName.Text = ""
            Quantity.Text = ""
            pr.Text = ""

        End If
    End Sub

    Private Sub ProductDVGLP_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ProductDVGLP.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = Me.ProductDVGLP.Rows(e.RowIndex)

            ProductId.Text = row.Cells("ProductId").Value.ToString()
            ProductName.Text = row.Cells("ProductName").Value.ToString()
            Quantity.Text = row.Cells("Quantity").Value.ToString()
            pr.Text = row.Cells("Price").Value.ToString()

        End If

    End Sub
    Public Sub RefreshDataGridView(ProductDVG As DataGridView, dataSource As Object)
        ' Set the data source for the DataGridView control
        ProductDVG.DataSource = dataSource
        ' Refresh the DataGridView control
        ProductDVG.Refresh()
    End Sub
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        clear()

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click

        If ProductId.Text = "" Then
            MsgBox("Enter Product ID to be deleted!")
        Else
            Con.Open()

            Dim query As String = "DELETE FROM productTb1 WHERE ProductId= @ProductId"
            Dim cmd As SqlCommand

            cmd = New SqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@ProductId", ProductId.Text)
            cmd.ExecuteNonQuery()
            MsgBox("Product deleted successfully!")
            Con.Close()
            populateForPA()
            ProductId.Text = ""
            ProductName.Text = ""
            Quantity.Text = ""
            pr.Text = ""

        End If
    End Sub

    Dim TotalPrice2 As Decimal
    Dim PriceForOtherOffer As Decimal
    Dim updatePrices As Decimal
    Dim itemTotalPrice As Decimal

    Dim HoldPrice As Decimal



    Public Sub OrderSum()
        Dim var_stocks As Integer
        Dim quantity As Integer
        Dim var_sell As Integer
        Dim result As Integer
        Dim price As Decimal

        Dim itemTotalPrice2 As Decimal
        Dim itemWithQuantityAndPrice As String

        If Order.Text = "SOUVENIRS" Or Order.Text = "TARPAULIN" Or Order.Text = "T-SHIRT" Then
            TabControl1.SelectedTab = TabPage6
            PriceForOtherOffer += itemTotalPrice ' Update the value of priceForOtherOffer

            Dim selectedItem As DataRowView = DirectCast(Order.SelectedItem, DataRowView)
            Dim selectedValue As String = selectedItem("ProductName").ToString()

            ' Get the quantity input from the user

            If Integer.TryParse(Qty.Text, quantity) AndAlso Integer.TryParse(ProdQty.Text, var_stocks) Then
                var_sell = quantity
                result = var_stocks - var_sell

                'price = CDec(selectedItem("Price"))
                If Decimal.TryParse(Qty.Text, quantity) AndAlso Decimal.TryParse(UpdatePrice.Text, updatePrices) Then
                    itemTotalPrice = updatePrices * quantity
                    itemWithQuantityAndPrice = selectedValue & " (" & quantity.ToString() & ") - Price: " & itemTotalPrice.ToString()


                Else
                    ' Handle the case when parsing the user input fails
                End If

                If var_stocks <= 0 Then
                    MsgBox("Product Sold out")
                    Qty.Text = ""
                ElseIf var_stocks < var_sell Then
                    MsgBox("Low stocked!")
                    Qty.Text = ""
                Else
                    Con.Open()
                    Dim cmds As New SqlCommand("SELECT * FROM ProductTb1", Con)
                    cmds.CommandText = "UPDATE ProductTb1 SET Quantity='" + result.ToString() + "' WHERE ProductId='" + ProdId.Text + "'"
                    cmds.ExecuteNonQuery()
                    Con.Close()

                    ' Append the itemWithQuantityAndPrice to the RichTextBox
                    RichTextBox1.AppendText(itemWithQuantityAndPrice & Environment.NewLine)
                    RichTextBox2.AppendText(itemWithQuantityAndPrice & Environment.NewLine)
                    'TotalPriceLabel.Text = "Total Price: " & TotalPrice2.ToString()

                    TotalPriceLabel.Text += itemTotalPrice.ToString()
                    HoldPrice += itemTotalPrice


                    TotalPriceLabel.Text = "Total Price: " & HoldPrice.ToString()
                    itemTotalPrice = 0

                    Qty.Text = ""
                End If
            Else
                ' Handle the case when parsing the user input or product quantity fails

            End If

            RichTextBox2.SelectedText = ""

        ElseIf Order.SelectedIndex >= 0 Then
            Dim selectedItem As DataRowView = DirectCast(Order.SelectedItem, DataRowView)
            Dim selectedValue As String = selectedItem("ProductName").ToString()
            ' Get the quantity input from the user
            If Integer.TryParse(Qty.Text, quantity) AndAlso Integer.TryParse(ProdQty.Text, var_stocks) Then
                var_sell = quantity
                result = var_stocks - var_sell

                price = CDec(selectedItem("Price"))
                itemTotalPrice2 = price * quantity

                itemWithQuantityAndPrice = selectedValue & " (" & quantity.ToString() & ") - Price: " & itemTotalPrice2.ToString()

                If var_stocks <= 0 Then
                    MsgBox("Product Sold out")
                    Qty.Text = ""
                ElseIf var_stocks < var_sell Then
                    MsgBox("Low stocked!")
                    Qty.Text = ""
                Else
                    Con.Open()
                    Dim cmds As New SqlCommand("SELECT * FROM ProductTb1", Con)
                    cmds.CommandText = "UPDATE ProductTb1 SET Quantity='" + result.ToString() + "' WHERE ProductId='" + ProdId.Text + "'"
                    cmds.ExecuteNonQuery()
                    Con.Close()
                    ' Append the itemWithQuantityAndPrice to the RichTextBox
                    RichTextBox1.AppendText(itemWithQuantityAndPrice & Environment.NewLine)
                    RichTextBox2.AppendText(itemWithQuantityAndPrice & Environment.NewLine)
                    HoldPrice += itemTotalPrice2


                    'TotalPriceLabel.Text = "Total Price: " & TotalPrice2.ToString()
                    TotalPriceLabel.Text += HoldPrice.ToString()
                    TotalPriceLabel.Text = "Total Price: " & HoldPrice.ToString()
                    Order.Text = ""
                    Qty.Text = ""
                End If

            Else
                ' Handle the case when parsing the user input or product quantity fails
                Con.Open()
                Dim cmds As New SqlCommand("SELECT * FROM ProductTb1", Con)
                cmds.CommandText = "UPDATE ProductTb1 SET Quantity='" + result.ToString() + "' WHERE ProductId='" + ProdId.Text + "'"
                cmds.ExecuteNonQuery()
                Con.Close()
            End If
        Else

            RichTextBox2.SelectedText = ""
        End If

        Order.Text = ""


    End Sub









    Dim lines As String
    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Price.Text = ""
        If Name.Text = "" OrElse address.Text = "" OrElse RichTextBox1.Text = "" Then
            MsgBox("Please Complete details")

        Else
            TabControl1.SelectedTab = TabPage5
            OrderSummary = RichTextBox2.Text

        End If



        ' Pass the total price to the OrderSummaryForm


        ' Show the OrderSummaryForm
        'OrderSummaryForm.Show()
    End Sub
    'Private selectedItems As New StringBuilder()
    Private selectedItems As New List(Of KeyValuePair(Of String, String))()

    Private isInitialSelection As Boolean = True
    Private Sub Order_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Order.SelectedIndexChanged
        populateForOtherOrder()
        TotalPriceLabel.Text = ""


        Dim var_stocks As Integer
        Dim var_sell As Integer
        Dim multipleOrder As Boolean = True
        Dim quantity As String = Qty.Text.Trim()


        If Integer.TryParse(ProdQty.Text, var_stocks) AndAlso Integer.TryParse(quantity, var_sell) Then
            Dim result As Integer = var_stocks - var_sell

            If var_stocks <= 0 Then
                MsgBox("Product Sold out")
                Qty.Text = ""
            ElseIf var_stocks < var_sell Then
                MsgBox("Low stocked!")
            Else
                ' Rest of your code here

                ' Error parsing quantity or var_stocks
                ' Handle the error or display an error message


                'ProdQty.Text = ""
                If Order.SelectedItem IsNot Nothing AndAlso Order.SelectedIndex > 0 Then

                        Dim selectedItem As DataRowView = DirectCast(Order.SelectedItem, DataRowView)
                        Dim productName As String = selectedItem("ProductName").ToString()


                        ' Check if the product name already exists in the list
                        Dim existingItem As KeyValuePair(Of String, String) = selectedItems.FirstOrDefault(Function(item) item.Key = productName)


                        If existingItem.Key IsNot Nothing Then
                            ' Product name already exists, update the quantity
                            selectedItems.Remove(existingItem)
                            quantity = (Integer.Parse(existingItem.Value)) + Integer.Parse(quantity).ToString()
                        End If

                        ' Add the updated or new item to the list
                        selectedItems.Add(New KeyValuePair(Of String, String)(productName, quantity))

                        ' Update the OrderItem TextBox with the updated selected items
                        OrderItem.Text = String.Join(", ", selectedItems.Select(Function(item) item.Key & " (x " & item.Value & ")"))

                    ElseIf Order.SelectedIndex = 0 Then
                        ' Clear the OrderItem TextBox if "Select Item" is chosen
                        OrderItem.Text = String.Empty
                    End If
                    multipleOrder = False

            End If

        End If

        OrderSum()


        fillId()
        FillPrice()
        fetchQty()
        fetchProd()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Search1.TextChanged
        Dim query As String = "SELECT * FROM ProductTb1 WHERE ProductId Like '%" & Search1.Text & "%' OR ProductName LIKE '%" & Search1.Text & "%'"

        Using Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30")
            Using cmd As New SqlCommand(query, Con)
                Using da As New SqlDataAdapter()
                    da.SelectCommand = cmd
                    Using dt As New DataTable()
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            ProductDVGLP.DataSource = dt
                        Else
                            MsgBox("No record found!")
                            Search1.Text = ""

                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub TabPage3_Click(sender As Object, e As EventArgs) Handles TabPage3.Click
        populatedMat()
    End Sub
    Public Sub populatedMat()
        Con.Open()
        Dim sql = "Select * from ProductTb3"
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(sql, Con)
        Dim builder = New SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ProductDVG2.DataSource = ds.Tables(0)
        Con.Close()

    End Sub
    Private Sub ProductDVG2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ProductDVG2.CellContentClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = Me.ProductDVG2.Rows(e.RowIndex)

            MaterialId.Text = row.Cells("id").Value.ToString()
            Material.Text = row.Cells("Material").Value.ToString()
            Count.Text = row.Cells("Count").Value.ToString()
            pr2.Text = row.Cells("Price").Value.ToString()

        End If
    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles Search2.TextChanged
        Dim query As String = "SELECT * FROM ProductTb1 WHERE ProductId LIKE '%" & Search2.Text & "%' OR ProductName LIKE '%" & Search2.Text & "%'"

        Using Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30")
            Using cmd As New SqlCommand(query, Con)
                Using da As New SqlDataAdapter()
                    da.SelectCommand = cmd
                    Using dt As New DataTable()
                        da.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            ProductDVG2.DataSource = dt
                        Else
                            MsgBox("No record found!")
                            Search2.Text = ""

                        End If
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Private Sub Panel4_Paint(sender As Object, e As PaintEventArgs) Handles Panel4.Paint

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        If MaterialId.Text = "" Then
            Dim myId As Guid = Guid.NewGuid()
            Dim myIdString = myId.ToString().Substring(0, 10)
            MaterialId.Text = myIdString

        End If
        Try
            If Material.Text = "" Then
                MsgBox("Insert Material!")
            ElseIf Count.Text = "" Then
                MsgBox("Insert Count!")
            ElseIf pr2.Text = "" Then
                MsgBox("Insert Price!")
            Else
                If Con.State = ConnectionState.Closed Then
                    Con.Open()

                    Dim query As String
                    query = "INSERT INTO ProductTb3 (Id, Material, Count, Price, DateTime) VALUES (@Id, @Material, @Count, @Price, GETDATE())"
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand(query, Con)
                    cmd.Parameters.AddWithValue("@Id", MaterialId.Text)
                    cmd.Parameters.AddWithValue("@Material", Material.Text.ToUpper)
                    cmd.Parameters.AddWithValue("@Count", Integer.Parse(Count.Text))
                    cmd.Parameters.AddWithValue("@Price", Decimal.Parse(pr2.Text))
                    cmd.ExecuteNonQuery()
                    MsgBox("Material added Successfully!")

                    Material.Text = ""
                    Count.Text = ""
                    pr2.Text = ""
                    MaterialId.Text = ""
                    Con.Close()
                    populatedMat()

                End If
            End If
        Catch ex As Exception
            MsgBox("Error adding product: " & ex.Message)
            'Material.Text = ""
            'Count.Text = ""
            'Price.Text = ""
            'MaterialId.Text = ""
        End Try
        Con.Close()

        CalcTotal()
    End Sub
    Public Sub CalcTotal()

        Dim colSum As Decimal
        For Each R As DataGridViewRow In ProductDVG2.Rows
            colSum += R.Cells(3).Value
        Next
        TotalExpenses.Text = " " & ChrW(&H20B1) & colSum.ToString("#,##0.00")

    End Sub

    Public Sub CalcTotalReceived()
        Dim colSum As Decimal = 0

        For Each item As ListViewItem In myListView.Items
            Dim value As Decimal
            If Decimal.TryParse(item.SubItems(3).Text, value) Then
                colSum += value
            End If
        Next

        receiveMoney.Text = "Total Receive: " & ChrW(&H20B1) & colSum.ToString("#,##0.00")
    End Sub


    Private Sub Count_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Count.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles pr2.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        If Material.Text = "" Or Count.Text = "" Or pr2.Text = "" Then
            CalcTotal()
            MsgBox("Please Fill up the data you want to Update")
        Else
            Con.Open()
            Dim sql = "UPDATE ProductTb3 SET Material=@Material, Count=@Count, Price=@Price WHERE Id=@MaterialId"
            If MaterialId.Text = "" Then
                'Dim colSum As Decimal
                'For Each R As DataGridViewRow In ProductDVG2.Rows
                '    colSum += R.Cells(3).Value
                'Next
                'TotalExpenses.Text = colSum
                MsgBox("Enter Material Id")
            Else
                Dim cmd As New SqlCommand(sql, Con)
                cmd.Parameters.AddWithValue("@Material", Material.Text)
                cmd.Parameters.AddWithValue("@Count", Count.Text)
                cmd.Parameters.AddWithValue("@Price", pr2.Text)
                cmd.Parameters.AddWithValue("@MaterialId", MaterialId.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Product Updated!")
                Con.Close()
                populatedMat()
                Material.Text = ""
                Count.Text = ""
                pr2.Text = ""
                MaterialId.Text = ""



            End If
        End If
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Material.Text = ""
        Count.Text = ""
        Price.Text = ""
        MaterialId.Text = ""
        Search.Text = ""
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Try
            If MaterialId.Text = "" Then
                MsgBox("Enter Material ID to be deleted!")
            Else
                Con.Open()



                Dim query As String = "DELETE FROM productTb3 WHERE Id= @MaterialId"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@MaterialId", MaterialId.Text)
                cmd.ExecuteNonQuery()
                MsgBox("Material deleted successfully!")
                Con.Close()
                populatedMat()
                MaterialId.Text = ""
                Material.Text = ""
                Count.Text = ""
                pr2.Text = ""

                CalcTotal()




            End If

        Catch ex As Exception
            MsgBox("Error Deleting Material: " & ex.Message)
            MaterialId.Text = ""

        End Try
    End Sub

    Private Sub Number_TextChanged(sender As Object, e As EventArgs) Handles Number.TextChanged

    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Application.Restart()

    End Sub

    Private Sub PrintDocument2_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage

    End Sub

    Private Sub PrintPreviewDialog2_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog2.Load

    End Sub

    Private Sub PrintDocument4_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument4.PrintPage

    End Sub

    Private Sub PrintDocument6_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument6.PrintPage

    End Sub

    Private Sub PrintDocument7_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument7.PrintPage

    End Sub

    Private Sub myListView_SelectedIndexChanged(sender As Object, e As EventArgs) Handles myListView.SelectedIndexChanged

    End Sub

    Private Sub PrintDocument3_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument3.PrintPage

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub

    Private Sub PrintPreviewDialog1_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog1.Load

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub PrintPreviewDialog3_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog3.Load

    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles Label17.Click

    End Sub

    Private Sub address_TextChanged(sender As Object, e As EventArgs) Handles address.TextChanged

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click

    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles Label16.Click

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ProdQty_Click(sender As Object, e As EventArgs) Handles ProdQty.Click

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub ProdId_Click(sender As Object, e As EventArgs) Handles ProdId.Click

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles Label18.Click

    End Sub

    Private Sub Price_Click(sender As Object, e As EventArgs) Handles Price.Click

    End Sub

    Private Sub hihi_Click(sender As Object, e As EventArgs) Handles hihi.Click

    End Sub

    Private Sub id_SelectedIndexChanged(sender As Object, e As EventArgs) Handles id.SelectedIndexChanged

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles Label19.Click

    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click

    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles Label21.Click

    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles Label22.Click

    End Sub

    Private Sub DateTime2_ValueChanged(sender As Object, e As EventArgs) Handles DateTime2.ValueChanged

    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles Label23.Click

    End Sub

    Private Sub Qty_TextChanged(sender As Object, e As EventArgs) Handles Qty.TextChanged

    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles Label24.Click

    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click

    End Sub

    Private Sub Name_TextChanged(sender As Object, e As EventArgs) Handles Name.TextChanged

    End Sub

    Private Sub Label26_Click(sender As Object, e As EventArgs) Handles Label26.Click

    End Sub

    Private Sub Label27_Click(sender As Object, e As EventArgs) Handles Label27.Click

    End Sub

    Private Sub Label43_Click(sender As Object, e As EventArgs) Handles Label43.Click

    End Sub

    Private Sub ProductId_Click(sender As Object, e As EventArgs) Handles ProductId.Click

    End Sub

    Private Sub Label28_Click(sender As Object, e As EventArgs) Handles Label28.Click

    End Sub

    Private Sub Label29_Click(sender As Object, e As EventArgs) Handles Label29.Click

    End Sub

    Private Sub pr_TextChanged(sender As Object, e As EventArgs) Handles pr.TextChanged

    End Sub

    Private Sub Quantity_TextChanged(sender As Object, e As EventArgs) Handles Quantity.TextChanged

    End Sub

    Private Sub ProductName_TextChanged(sender As Object, e As EventArgs) Handles ProductName.TextChanged

    End Sub

    Private Sub Label30_Click(sender As Object, e As EventArgs) Handles Label30.Click

    End Sub

    Private Sub Label31_Click(sender As Object, e As EventArgs) Handles Label31.Click

    End Sub

    Private Sub Label32_Click(sender As Object, e As EventArgs) Handles Label32.Click

    End Sub

    Private Sub Label33_Click(sender As Object, e As EventArgs) Handles Label33.Click

    End Sub

    Private Sub MaterialId_Click(sender As Object, e As EventArgs) Handles MaterialId.Click

    End Sub

    Private Sub TotalExpenses_Click(sender As Object, e As EventArgs) Handles TotalExpenses.Click

    End Sub

    Private Sub Label35_Click(sender As Object, e As EventArgs) Handles Label35.Click

    End Sub

    Private Sub Label36_Click(sender As Object, e As EventArgs) Handles Label36.Click

    End Sub

    Private Sub pr2_TextChanged(sender As Object, e As EventArgs) Handles pr2.TextChanged

    End Sub

    Private Sub Label37_Click(sender As Object, e As EventArgs) Handles Label37.Click

    End Sub

    Private Sub Count_TextChanged(sender As Object, e As EventArgs) Handles Count.TextChanged

    End Sub

    Private Sub Label38_Click(sender As Object, e As EventArgs) Handles Label38.Click

    End Sub

    Private Sub Material_TextChanged(sender As Object, e As EventArgs) Handles Material.TextChanged

    End Sub

    Private Sub Label39_Click(sender As Object, e As EventArgs) Handles Label39.Click

    End Sub

    Private Sub Label40_Click(sender As Object, e As EventArgs) Handles Label40.Click

    End Sub

    Private Sub Label41_Click(sender As Object, e As EventArgs) Handles Label41.Click

    End Sub

    Private Sub Label42_Click(sender As Object, e As EventArgs) Handles Label42.Click

    End Sub

    Private Sub Label34_Click(sender As Object, e As EventArgs) Handles Label34.Click

    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub PrintPreviewDialog4_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog4.Load

    End Sub

    Private Sub PrintDocument5_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument5.PrintPage

    End Sub

    Private Sub PrintPreviewDialog5_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog5.Load

    End Sub

    Private Sub PrintPreviewDialog6_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog6.Load

    End Sub

    Private Sub PrintPreviewDialog7_Load(sender As Object, e As EventArgs) Handles PrintPreviewDialog7.Load

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click

    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click

    End Sub

    Private Sub payment_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub fpayment_CheckedChanged(sender As Object, e As EventArgs)
        strPayment = "Full Payment"
    End Sub

    Private Sub dpaymet_CheckedChanged(sender As Object, e As EventArgs)
        strPayment = "Down Payment"
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs)
        If RichTextBox1.SelectionLength > 0 Then
            RichTextBox1.SelectedText = ""
        Else
            MsgBox("Please select an item to delete.")
        End If

    End Sub

    Private Sub TabPage5_Click(sender As Object, e As EventArgs) Handles TabPage5.Click

    End Sub

    Private Sub Button29_Click_1(sender As Object, e As EventArgs) Handles Button29.Click



        Dim paymentInformation As String = PaymentTextBox.Text.Trim()
        If paymentOption = "" Or PaymentTextBox.Text = "" Or Name.Text = "" Or address.Text = "" Or Number.Text = "" Then

            MessageBox.Show("Please complete the details.", "Payment Information", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TabPage5.Visible = False
            TabPage1.Visible = True
        Else
            If Not String.IsNullOrEmpty(paymentOption) Then
                MessageBox.Show("Payment Option: " & paymentOption & vbCrLf & "Payment Information: " & paymentInformation, "Payment Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If RichTextBox1 IsNot Nothing Then
                    RichTextBox1.Clear()
                End If




                InsertDB()
                ShowBill()
                OrderItem.Text = String.Empty
                RichTextBox2.Text = ""

                fpayment.Checked = False

                dpayment.Checked = False
                fetchIPayment()
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



            End If


        End If
    End Sub

    Private Sub fpayment_CheckedChanged_1(sender As Object, e As EventArgs) Handles fpayment.CheckedChanged
        paymentOption = "Full Payment"
    End Sub

    Private Sub dpayment_CheckedChanged(sender As Object, e As EventArgs) Handles dpayment.CheckedChanged
        paymentOption = "Down Payment"

    End Sub

    Private Sub Label51_Click(sender As Object, e As EventArgs) Handles Label51.Click

    End Sub

    Private Sub Button16_Click_1(sender As Object, e As EventArgs) Handles Button16.Click
        Dim myid As Guid = Guid.NewGuid()
        Dim myidstring = myid.ToString().Substring(0, 7)
        CID.Text = myidstring

        If TextBox2.Text = "" Then
            MsgBox("Insert Data")
        Else
            Dim query As String = "INSERT INTO Color (Id,Color) VALUES (@id2,@color)"
            Using command As New SqlCommand(query, Con)
                ' Set the parameter value
                command.Parameters.AddWithValue("@id2", CID.Text)
                command.Parameters.AddWithValue("@color", TextBox2.Text)

                Con.Open()
                command.ExecuteNonQuery()
                MsgBox("Added Successfully")
                Con.Close()
                TextBox2.Text = ""
                CID.Text = ""
            End Using
        End If

        populateForColor()
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Dim myid As Guid = Guid.NewGuid()
        Dim myidstring = myid.ToString().Substring(0, 7)
        SbID.Text = myidstring

        If TextBox2.Text = "" Then
            MsgBox("Insert Data")
        Else
            Dim query As String = "INSERT INTO SubCat (Id,SubCategory) VALUES (@id2,@subcat)"
            Using command As New SqlCommand(query, Con)
                ' Set the parameter value
                command.Parameters.AddWithValue("@id2", SbID.Text)
                command.Parameters.AddWithValue("@subcat", TextBox2.Text)

                Con.Open()
                command.ExecuteNonQuery()
                MsgBox("Added Successfully")
                Con.Close()
                TextBox2.Text = ""
                SbID.Text = ""
            End Using
        End If

        populateForSubCat()
    End Sub
    Private Sub fetchId4()
        Con.Close()
        Con.Open()
        Dim query As String = "SELECT Id FROM Color WHERE color=@col"
        Dim cmd As New SqlCommand(query, Con)
        If col.SelectedValue IsNot Nothing Then
            cmd.Parameters.AddWithValue("@col", col.SelectedValue.ToString())
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim IdValue As String = reader("id").ToString()
                CID.Text = IdValue
            End If
            reader.Close()
            Con.Close()
        Else
            ' Handle the case when Order.SelectedValue is null
            ' You can assign a default value or display an error message
        End If

    End Sub


    Private Sub fetchId2()
        Con.Close()
        Con.Open()
        Dim query As String = "SELECT Id FROM OtherOrder WHERE GeneralCat=@genCat"
        Dim cmd As New SqlCommand(query, Con)
        If GenCat.SelectedValue IsNot Nothing Then
            cmd.Parameters.AddWithValue("@genCat", GenCat.SelectedValue.ToString())
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim IdValue As String = reader("id").ToString()
                idOf.Text = IdValue
            End If
            reader.Close()
            Con.Close()
        Else
            ' Handle the case when Order.SelectedValue is null
            ' You can assign a default value or display an error message
        End If

    End Sub









    Private Sub fetchId3()
        Con.Close()
        Con.Open()
        Dim query As String = "SELECT Id FROM SubCat WHERE SubCategory=@subCat"
        Dim cmd As New SqlCommand(query, Con)
        If subcat.SelectedValue IsNot Nothing Then
            cmd.Parameters.AddWithValue("@subCat", subcat.SelectedValue.ToString())
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                Dim IdValue As String = reader("id").ToString()
                SbID.Text = IdValue
            End If
            reader.Close()
            Con.Close()
        Else
            ' Handle the case when Order.SelectedValue is null
            ' You can assign a default value or display an error message
        End If
    End Sub

    Private Sub populateForColor()
        Con.Close()
        Con.Open()
        Dim sql = "Select * from Color"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim tb1 As New DataTable()
        adapter.Fill(tb1)
        col.DataSource = tb1
        col.DisplayMember = "Color"
        col.ValueMember = "Color"
        If col.Items.Count > 1 Then
            col.SelectedIndex = -1
            'Order.SelectedText = "Select Order"
        End If

        Con.Close()
    End Sub


    Private Sub populateForOtherOrder()
        Con.Close()
        Con.Open()
        Dim sql = "Select * from OtherOrder"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim tb1 As New DataTable()
        adapter.Fill(tb1)
        GenCat.DataSource = tb1
        GenCat.DisplayMember = "GeneralCat"
        GenCat.ValueMember = "GeneralCat"
        If GenCat.Items.Count > 1 Then
            GenCat.SelectedIndex = -1
            'Order.SelectedText = "Select Order"
        End If

        Con.Close()
    End Sub
    Private Sub populateForSubCat()
        Con.Close()
        Con.Open()
        Dim sql = "Select * from SubCat"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim tb1 As New DataTable()
        adapter.Fill(tb1)
        subcat.DataSource = tb1
        subcat.DisplayMember = "SubCategory"
        subcat.ValueMember = "SubCategory"
        If subcat.Items.Count > 1 Then
            subcat.SelectedIndex = -1
            'Order.SelectedText = "Select Order"
        End If

        Con.Close()
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        ' Insert the value into the database


        Dim myid As Guid = Guid.NewGuid()
            Dim myidstring = myid.ToString().Substring(0, 7)
            idOf.Text = myidstring

            If TextBox2.Text = "" Then
                MsgBox("Insert Data")
            Else
                Dim query As String = "INSERT INTO OtherOrder (id,GeneralCat) VALUES (@id2,@gencat)"
                Using command As New SqlCommand(query, Con)
                    ' Set the parameter value
                    command.Parameters.AddWithValue("@id2", idOf.Text)
                    command.Parameters.AddWithValue("@gencat", TextBox2.Text)

                    Con.Open()
                    command.ExecuteNonQuery()
                    MsgBox("Added Successfully")
                    Con.Close()
                    TextBox2.Text = ""
                    idOf.Text = ""
                End Using
            End If

        populateForOtherOrder()


    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click


        If GenCat.Text = "" Then
            MsgBox("Select Item")
        Else



            ' Delete the corresponding record from the database
            Dim query As String = "DELETE FROM OtherOrder WHERE Id = @RecordId"
            Using connection As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30")
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@RecordId", idOf.Text)
                    connection.Open()
                    command.ExecuteNonQuery()
                    connection.Close()
                    MsgBox("Deleted Successfully!")
                    populateForOtherOrder()
                End Using
            End Using
        End If
        idOf.Text = ""
        GenCat.Text = ""
        populateForOtherOrder()
    End Sub

    Private Sub GenCat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GenCat.SelectedIndexChanged
        fetchId2()
    End Sub

    Private Sub subcat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles subcat.SelectedIndexChanged
        fetchId3()
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        If subcat.Text = "" Then
            MsgBox("Select Item")
        Else



            ' Delete the corresponding record from the database
            Dim query As String = "DELETE FROM SubCat WHERE Id = @RecordId"
            Using connection As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30")
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@RecordId", SbID.Text)
                    connection.Open()
                    command.ExecuteNonQuery()
                    connection.Close()
                    MsgBox("Deleted Successfully!")
                    populateForSubCat()
                End Using
            End Using
        End If
        SbID.Text = ""
        subcat.Text = ""
        populateForSubCat()
    End Sub

    Private Sub col_SelectedIndexChanged(sender As Object, e As EventArgs) Handles col.SelectedIndexChanged
        fetchId4()
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        If col.Text = "" Then
            MsgBox("Select Item")
        Else


            ' Delete the corresponding record from the database
            Dim query As String = "DELETE FROM Color WHERE Id = @RecordId"
            Using connection As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30")
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@RecordId", CID.Text)
                    connection.Open()
                    command.ExecuteNonQuery()
                    connection.Close()
                    MsgBox("Deleted Successfully!")
                    populateForColor()
                End Using
            End Using
        End If
        CID.Text = ""
        subcat.Text = ""
        populateForColor()

    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        If Qty.Text = "" Then
            MsgBox("Insert Quantity")
            TabPage6.Visible = False
            TabPage1.Visible = True
        Else


            If UpdatePrice.Text = "" Then
                MsgBox("Insert Price")
            Else
                PriceForOtherOffer += UpdatePrice.Text
                OrderSum()

                UpdatePrice.Text = ""

                TabPage6.Visible = False
                TabPage5.Visible = True
            End If

            'Con.Open()
            'Dim sql = "UPDATE ProductTb1 SET  Price=@Price "
            'Dim cmd As New SqlCommand(sql, Con)

            'cmd.Parameters.AddWithValue("@Price", UpdatePrice.Text)
            'cmd.ExecuteNonQuery()
            'MsgBox("Saved!")
            'Con.Close()
            'UpdatePrice.Text = ""
            'populateForPA()

        End If
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) 


    End Sub
End Class

Imports System.Data.SqlClient
Public Class Form1
    Dim Con As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kathlene\source\repos\capstoneProject2ndYear\capstoneProject2ndYear\ProductVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Dim adp As SqlDataAdapter

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
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        TabControl1.SelectedTab = TabPage4
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub
    Public Sub populate()
        Con.Open()

        Dim sql As String = "SELECT * FROM ProductTb4"
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


    Private Sub FillOrder()
        Con.Open()
        Dim query = "SELECT * FROM ProductTb4 WHERE referenceNumber='" & id.SelectedValue.ToString() & "'"
        Dim cmd As New SqlCommand(query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            Order.Text = reader(4).ToString()
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




    Private Sub fechName()
        If id.SelectedValue IsNot Nothing Then
            Con.Open()
            Dim query = "SELECT * FROM ProductTb4 WHERE referenceNumber='" & id.SelectedValue.ToString() & "'"

            Dim cmd As New SqlCommand(query, Con)
            Dim dt As New DataTable
            Dim reader As SqlDataReader
            reader = cmd.ExecuteReader()
            While reader.Read
                Name.Text = reader(1).ToString()
            End While
            Con.Close()
        End If
    End Sub
    Private Sub FillDate()
        Con.Open()
        Dim query = "SELECT * FROM ProductTb4 WHERE referenceNumber='" & id.SelectedValue.ToString() & "'"
        Dim cmd As New SqlCommand(query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            DateTime2.Text = reader(6).ToString
        End While
        Con.Close()
    End Sub




    Private Sub FillQuantity()
        Con.Open()
        Dim query = "SELECT * FROM ProductTb4 WHERE referenceNumber='" & id.SelectedValue.ToString() & "'"
        Dim cmd As New SqlCommand(query, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader()
        While reader.Read
            Qty.Text = reader(5).ToString
        End While
        Con.Close()
    End Sub
    Private Sub fetchPrice()
        Con.Open()
        Dim query As String = "SELECT Price FROM ProductTb1 WHERE ProductName=@productName"
        Dim cmd As New SqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@productName", Order.SelectedValue.ToString())
        Dim reader As SqlDataReader = cmd.ExecuteReader()

        If reader.Read() Then
            Dim priceValue As String = reader("Price").ToString()
            Price.Text = priceValue
        End If
        reader.Close()
        Con.Close()
    End Sub


    Private Sub fetchId()
        Con.Open()
        Dim query As String = "SELECT ProductId FROM ProductTb1 WHERE ProductName=@productName"
        Dim cmd As New SqlCommand(query, Con)
        cmd.Parameters.AddWithValue("@productName", Order.SelectedValue.ToString())
        Dim reader As SqlDataReader = cmd.ExecuteReader()

        If reader.Read() Then
            Dim IdValue As String = reader("ProductId").ToString()
            ProdId.Text = IdValue
        End If
        reader.Close()
        Con.Close()
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

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = Me.ProductDVG3.Rows(e.RowIndex)
            id.Text = row.Cells("referenceNumber").Value.ToString()
            Name.Text = row.Cells("Customer_Name").Value.ToString()
            Number.Text = row.Cells("Contact_Number").Value.ToString()
            address.Text = row.Cells("Address").Value.ToString()
            Order.Text = row.Cells("Product_Order").Value.ToString()
            Qty.Text = row.Cells("Quantity").Value.ToString()
            DateTime2.Text = row.Cells("Date").Value.ToString()

            fetchId()
            FillPrice()
            fetchQty()
            If String.IsNullOrEmpty(RichTextBox1.Text) Then
                ShowBill()


            Else
                RichTextBox1.Clear()
                ShowBill()

            End If

        End If
    End Sub

    Private Sub ShowBill()

        'If Decimal.TryParse(Price.Text, prices) AndAlso Integer.TryParse(Qty.Text, qtys) Then
        Dim res = Price.Text * Qty.Text

        RichTextBox1.Text += "      Escabel Ramirez Printing Services              " & vbCrLf
            RichTextBox1.Text += "               Rosario Batangas                       " & vbCrLf
            RichTextBox1.Text += "               Tel + 09321313643                        " & vbCrLf
            RichTextBox1.Text += "************************************************" & vbCrLf
            RichTextBox1.Text += "Ref No. :  " & id.Text & "  " & DateTime.Now.ToString() & vbCrLf
            RichTextBox1.Text += "************************************************" & vbCrLf
            RichTextBox1.Text += "Customer Name    :    " & Name.Text & vbCrLf
            RichTextBox1.Text += "Contact Number   :    " & Number.Text & vbCrLf
            RichTextBox1.Text += "Address          :    " & address.Text & vbCrLf
            RichTextBox1.Text += "Product Order    :    " & Order.Text & vbCrLf
            RichTextBox1.Text += "Quantity         :    x" & Qty.Text & vbCrLf
            RichTextBox1.Text += "Date Release     :    " & DateTime2.Text & vbCrLf
            RichTextBox1.Text += "************************************************" & vbCrLf
            RichTextBox1.Text += "Total            :    " & ChrW(&H20B1) & res.ToString("#,##0.00") & vbCrLf
            RichTextBox1.Text += "************************************************" & vbCrLf
            RichTextBox1.Text += "            Thank you for Shopping                  " & vbCrLf

        'End If



    End Sub

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



    Private Sub InsertDB()
        Dim query As String = "INSERT INTO ProductTb4 (referenceNumber, Customer_Name, Contact_Number, Address, Product_Order, Quantity, Date, Total) VALUES (@referenceNumber, @Customer_Name, @Contact_Number, @Address, @Product_Order, @Quantity, GETDATE(), @Total)"
        Dim cmd As SqlCommand = New SqlCommand(query, Con)
        Dim querys As String = "INSERT INTO OrderReceived (refNum, Customer_Name,  [Date], Total, id, Contact_Number, Address, Product_Order, Quantity) VALUES (@refNum, @Customer_Name, GETDATE(), @Total, @id, @Contact_Number, @Address, @Product_Order, @Quantity)"
        Dim cmds As SqlCommand = New SqlCommand(querys, Con)
        Dim res As Decimal
        cmd.Parameters.Add("@referenceNumber", SqlDbType.NVarChar, 10).Value = id.Text
        cmd.Parameters.Add("@Customer_Name", SqlDbType.NVarChar, 50).Value = Name.Text.ToUpper()
        cmd.Parameters.Add("@Contact_Number", SqlDbType.NVarChar, 20).Value = Number.Text
        cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = address.Text.ToUpper()
        cmd.Parameters.Add("@Product_Order", SqlDbType.NVarChar, 50).Value = Order.Text.ToUpper()
        cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Integer.Parse(Qty.Text)



        cmds.Parameters.AddWithValue("@refNum", SqlDbType.NVarChar).Value = id.Text
        cmds.Parameters.AddWithValue("@Customer_Name", SqlDbType.NVarChar).Value = Name.Text.ToUpper()
        cmds.Parameters.AddWithValue("@Total", res)
        cmds.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = id.Text
        cmds.Parameters.AddWithValue("@Contact_Number", SqlDbType.NVarChar).Value = Number.Text
        cmds.Parameters.AddWithValue("@Address", SqlDbType.NVarChar).Value = address.Text.ToUpper()
        cmds.Parameters.AddWithValue("@Product_Order", SqlDbType.NVarChar).Value = Order.Text.ToUpper()
        cmds.Parameters.AddWithValue("@Quantity", SqlDbType.Int).Value = Integer.Parse(Qty.Text)


        Dim newDataSource As Object = GetDataTableFromDatabase()
        RefreshDataGridView(ProductDVGLP, newDataSource)




        'If Decimal.TryParse(Price.Text, prices) AndAlso Integer.TryParse(Qty.Text, qtys) Then
        res = Price.Text * Qty.Text
        'End If
        cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = res
        'ShowBill()



        Try
            Con.Open()
            cmd.ExecuteNonQuery()
            cmds.ExecuteNonQuery()
            MsgBox("Added Successfully!")
            ShowBill()



            Name.Text = ""
            Number.Text = ""
            Qty.Text = ""
            Order.Text = ""
            ProdId.Text = ""
            Price.Text = ""
            id.Text = ""
            ProdQty.Text = ""
            address.Text = ""

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Con.Close()
        End Try



        'Con.Open()
        'cmd.CommandText = "UPDATE ProductTb1 SET Quantity=@Quantity WHERE ProductId=@ProductId"
        'cmd.Parameters.AddWithValue("@Quantity", res.ToString())
        'cmd.Parameters.AddWithValue("@ProductId", ProdId.Text)
        'cmd.ExecuteNonQuery()
        Con.Close()
        populate()

        'if richtextbox1 isnot nothing then
        '    richtextbox1.clear()
        'end if
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If RichTextBox1 IsNot Nothing Then
            RichTextBox1.Clear()
        End If

        Dim myId As Guid = Guid.NewGuid()
        Dim myIdString = myId.ToString().Substring(0, 10)
        id.Text = myIdString






        Try
            If Name.Text = "" Or Number.Text = "" Or address.Text = "" Or Qty.Text = "" Then
                MsgBox("Please Complete details before adding order!")
            Else

                Dim var_stocks As Integer = 0
                Dim var_sell As Integer = 0
                Dim result As Integer = 0

                var_stocks = Convert.ToInt32(ProdQty.Text)
                var_sell = Convert.ToInt32(Qty.Text)
                result = var_stocks - var_sell

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
                    InsertDB()
                End If
            End If

        Catch ex As Exception
            MsgBox("Invalid")
        End Try




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
            Application.Restart()
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
            item.SubItems.Add(reader("Quantity").ToString())
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

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If id.Text = "" Then
            MsgBox("select customer to cancel")

        Else
            Try

                Dim query As String = "DELETE FROM productTb4 WHERE referenceNumber = @referenceNumber"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.Parameters.AddWithValue("@referenceNumber", id.Text)
                Con.Open()
                cmd.ExecuteNonQuery()
                Con.Close()
                MsgBox("Order cancelled!")

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
                                item.SubItems.Add(row("Quantity").ToString())
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
            item.SubItems.Add(reader("Quantity").ToString())
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
        populateForPA()
        fillCategory()
        FillCustomer()
        populate()
        populate2()
        populatedMat()
        populateForOR()
        ProductDVGLP.Refresh()


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


        If ProductId.Text = "" Or Quantity.Text = "" Or pr.Text = "" Then
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

    Private Sub Order_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Order.SelectedIndexChanged
        fillId()
        FillPrice()
        fetchQty()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Search1.TextChanged
        Dim query As String = "SELECT * FROM ProductTb1 WHERE ProductId LIKE '%" & Search1.Text & "%' OR ProductName LIKE '%" & Search1.Text & "%'"

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

                CalcTotal()

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
End Class

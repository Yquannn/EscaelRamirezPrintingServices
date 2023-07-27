<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OrderSummaryForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.TotalPriceLabel = New System.Windows.Forms.Label()
        Me.Button28 = New System.Windows.Forms.Button()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.dpayment = New System.Windows.Forms.RadioButton()
        Me.fpayment = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PaymentTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox1.Location = New System.Drawing.Point(34, 27)
        Me.RichTextBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(381, 342)
        Me.RichTextBox1.TabIndex = 157
        Me.RichTextBox1.Text = ""
        '
        'TotalPriceLabel
        '
        Me.TotalPriceLabel.AutoSize = True
        Me.TotalPriceLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalPriceLabel.Location = New System.Drawing.Point(471, 27)
        Me.TotalPriceLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.TotalPriceLabel.Name = "TotalPriceLabel"
        Me.TotalPriceLabel.Size = New System.Drawing.Size(0, 25)
        Me.TotalPriceLabel.TabIndex = 158
        '
        'Button28
        '
        Me.Button28.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button28.BackColor = System.Drawing.Color.Transparent
        Me.Button28.BackgroundImage = Global.EscaelRamirezPrintingServices.My.Resources.Resources.icons8_shopping_basket_add_48
        Me.Button28.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button28.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button28.FlatAppearance.BorderSize = 0
        Me.Button28.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button28.Font = New System.Drawing.Font("Microsoft Tai Le", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.Button28.Location = New System.Drawing.Point(498, 293)
        Me.Button28.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Button28.Name = "Button28"
        Me.Button28.Size = New System.Drawing.Size(55, 58)
        Me.Button28.TabIndex = 172
        Me.Button28.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button28.UseVisualStyleBackColor = False
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Microsoft Tai Le", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(483, 354)
        Me.Label45.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(83, 21)
        Me.Label45.TabIndex = 173
        Me.Label45.Text = "Add Order"
        '
        'dpayment
        '
        Me.dpayment.AutoSize = True
        Me.dpayment.Font = New System.Drawing.Font("Microsoft Tai Le", 12.0!)
        Me.dpayment.Location = New System.Drawing.Point(472, 190)
        Me.dpayment.Name = "dpayment"
        Me.dpayment.Size = New System.Drawing.Size(134, 25)
        Me.dpayment.TabIndex = 177
        Me.dpayment.TabStop = True
        Me.dpayment.Text = "Down Payment"
        Me.dpayment.UseVisualStyleBackColor = True
        '
        'fpayment
        '
        Me.fpayment.AutoSize = True
        Me.fpayment.Font = New System.Drawing.Font("Microsoft Tai Le", 12.0!)
        Me.fpayment.Location = New System.Drawing.Point(472, 159)
        Me.fpayment.Name = "fpayment"
        Me.fpayment.Size = New System.Drawing.Size(118, 25)
        Me.fpayment.TabIndex = 176
        Me.fpayment.TabStop = True
        Me.fpayment.Text = "Full Payment"
        Me.fpayment.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Tai Le", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(471, 130)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(163, 26)
        Me.Label1.TabIndex = 178
        Me.Label1.Text = "Payment Option"
        '
        'PaymentTextBox
        '
        Me.PaymentTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PaymentTextBox.Location = New System.Drawing.Point(472, 236)
        Me.PaymentTextBox.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PaymentTextBox.Name = "PaymentTextBox"
        Me.PaymentTextBox.Size = New System.Drawing.Size(219, 31)
        Me.PaymentTextBox.TabIndex = 179
        '
        'OrderSummaryForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.PaymentTextBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dpayment)
        Me.Controls.Add(Me.fpayment)
        Me.Controls.Add(Me.Label45)
        Me.Controls.Add(Me.Button28)
        Me.Controls.Add(Me.TotalPriceLabel)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Name = "OrderSummaryForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OrderSummaryForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RichTextBox1 As RichTextBox
    Public WithEvents TotalPriceLabel As Label
    Friend WithEvents Button28 As Button
    Friend WithEvents Label45 As Label
    Friend WithEvents dpayment As RadioButton
    Friend WithEvents fpayment As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents PaymentTextBox As TextBox
End Class

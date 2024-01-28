
namespace EnergyService
{
    partial class AddResourceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.AddPreviewDataGridView = new System.Windows.Forms.DataGridView();
            this.AddButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.UnitsComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.QuantityTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SupplierInvoiceDateMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SupplierInvoiceTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SupplierComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.L9_ComboBox = new System.Windows.Forms.ComboBox();
            this.L8_ComboBox = new System.Windows.Forms.ComboBox();
            this.L7_ComboBox = new System.Windows.Forms.ComboBox();
            this.L6_ComboBox = new System.Windows.Forms.ComboBox();
            this.L5_ComboBox = new System.Windows.Forms.ComboBox();
            this.L4_ComboBox = new System.Windows.Forms.ComboBox();
            this.L3_ComboBox = new System.Windows.Forms.ComboBox();
            this.L2_ComboBox = new System.Windows.Forms.ComboBox();
            this.L1_ComboBox = new System.Windows.Forms.ComboBox();
            this.L0_ComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CloseAddResourceButton = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.units = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplyDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.AddPreviewDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // AddPreviewDataGridView
            // 
            this.AddPreviewDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AddPreviewDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.AddPreviewDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AddPreviewDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.title,
            this.quantity,
            this.units,
            this.supplier,
            this.invoice,
            this.supplyDate});
            this.AddPreviewDataGridView.Location = new System.Drawing.Point(382, 80);
            this.AddPreviewDataGridView.MultiSelect = false;
            this.AddPreviewDataGridView.Name = "AddPreviewDataGridView";
            this.AddPreviewDataGridView.ReadOnly = true;
            this.AddPreviewDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AddPreviewDataGridView.Size = new System.Drawing.Size(858, 354);
            this.AddPreviewDataGridView.TabIndex = 0;
            this.AddPreviewDataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.AddPreviewDataGridView_RowsAdded);
            this.AddPreviewDataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.AddPreviewDataGridView_RowsRemoved);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(282, 1);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(64, 23);
            this.AddButton.TabIndex = 22;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Controls.Add(this.UnitsComboBox);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.QuantityTextBox);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.SupplierInvoiceDateMaskedTextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.SupplierInvoiceTextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.SupplierComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TitleTextBox);
            this.panel1.Controls.Add(this.L9_ComboBox);
            this.panel1.Controls.Add(this.AddButton);
            this.panel1.Controls.Add(this.L8_ComboBox);
            this.panel1.Controls.Add(this.L7_ComboBox);
            this.panel1.Controls.Add(this.L6_ComboBox);
            this.panel1.Controls.Add(this.L5_ComboBox);
            this.panel1.Controls.Add(this.L4_ComboBox);
            this.panel1.Controls.Add(this.L3_ComboBox);
            this.panel1.Controls.Add(this.L2_ComboBox);
            this.panel1.Controls.Add(this.L1_ComboBox);
            this.panel1.Controls.Add(this.L0_ComboBox);
            this.panel1.Location = new System.Drawing.Point(12, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 385);
            this.panel1.TabIndex = 23;
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(282, 219);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(64, 48);
            this.SaveButton.TabIndex = 48;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // UnitsComboBox
            // 
            this.UnitsComboBox.FormattingEnabled = true;
            this.UnitsComboBox.Items.AddRange(new object[] {
            "m",
            "pcs"});
            this.UnitsComboBox.Location = new System.Drawing.Point(307, 351);
            this.UnitsComboBox.Name = "UnitsComboBox";
            this.UnitsComboBox.Size = new System.Drawing.Size(39, 21);
            this.UnitsComboBox.TabIndex = 47;
            this.UnitsComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UnitsComboBox_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(254, 354);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 46;
            this.label9.Text = "Одиниці";
            // 
            // QuantityTextBox
            // 
            this.QuantityTextBox.Location = new System.Drawing.Point(180, 351);
            this.QuantityTextBox.MaxLength = 5;
            this.QuantityTextBox.Name = "QuantityTextBox";
            this.QuantityTextBox.Size = new System.Drawing.Size(37, 20);
            this.QuantityTextBox.TabIndex = 45;
            this.QuantityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.QuantityTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QuantityTextBox_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(121, 354);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Кількість";
            // 
            // SupplierInvoiceDateMaskedTextBox
            // 
            this.SupplierInvoiceDateMaskedTextBox.Location = new System.Drawing.Point(53, 351);
            this.SupplierInvoiceDateMaskedTextBox.Mask = "00/00/0000";
            this.SupplierInvoiceDateMaskedTextBox.Name = "SupplierInvoiceDateMaskedTextBox";
            this.SupplierInvoiceDateMaskedTextBox.Size = new System.Drawing.Size(62, 20);
            this.SupplierInvoiceDateMaskedTextBox.TabIndex = 39;
            this.SupplierInvoiceDateMaskedTextBox.ValidatingType = typeof(System.DateTime);
            this.SupplierInvoiceDateMaskedTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SupplierInvoiceDateMaskedTextBox_KeyPress);
            this.SupplierInvoiceDateMaskedTextBox.MouseEnter += new System.EventHandler(this.SupplierInvoiceDateMaskedTextBox_MouseEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Дата";
            // 
            // SupplierInvoiceTextBox
            // 
            this.SupplierInvoiceTextBox.Location = new System.Drawing.Point(121, 326);
            this.SupplierInvoiceTextBox.Name = "SupplierInvoiceTextBox";
            this.SupplierInvoiceTextBox.Size = new System.Drawing.Size(225, 20);
            this.SupplierInvoiceTextBox.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Видаткова накладна";
            // 
            // SupplierComboBox
            // 
            this.SupplierComboBox.FormattingEnabled = true;
            this.SupplierComboBox.Location = new System.Drawing.Point(88, 299);
            this.SupplierComboBox.Name = "SupplierComboBox";
            this.SupplierComboBox.Size = new System.Drawing.Size(258, 21);
            this.SupplierComboBox.TabIndex = 35;
            this.SupplierComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SupplierComboBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Постачальник";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Назва";
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(73, 273);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(273, 20);
            this.TitleTextBox.TabIndex = 32;
            // 
            // L9_ComboBox
            // 
            this.L9_ComboBox.FormattingEnabled = true;
            this.L9_ComboBox.Location = new System.Drawing.Point(3, 246);
            this.L9_ComboBox.Name = "L9_ComboBox";
            this.L9_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L9_ComboBox.TabIndex = 31;
            this.L9_ComboBox.Text = "--";
            this.L9_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L9_ComboBox_SelectedIndexChanged);
            this.L9_ComboBox.TextChanged += new System.EventHandler(this.L9_ComboBox_TextChanged);
            // 
            // L8_ComboBox
            // 
            this.L8_ComboBox.FormattingEnabled = true;
            this.L8_ComboBox.Location = new System.Drawing.Point(3, 219);
            this.L8_ComboBox.Name = "L8_ComboBox";
            this.L8_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L8_ComboBox.TabIndex = 30;
            this.L8_ComboBox.Text = "--";
            this.L8_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L8_ComboBox_SelectedIndexChanged);
            this.L8_ComboBox.TextChanged += new System.EventHandler(this.L8_ComboBox_TextChanged);
            // 
            // L7_ComboBox
            // 
            this.L7_ComboBox.FormattingEnabled = true;
            this.L7_ComboBox.Location = new System.Drawing.Point(3, 192);
            this.L7_ComboBox.Name = "L7_ComboBox";
            this.L7_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L7_ComboBox.TabIndex = 29;
            this.L7_ComboBox.Text = "--";
            this.L7_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L7_ComboBox_SelectedIndexChanged);
            this.L7_ComboBox.TextChanged += new System.EventHandler(this.L7_ComboBox_TextChanged);
            // 
            // L6_ComboBox
            // 
            this.L6_ComboBox.FormattingEnabled = true;
            this.L6_ComboBox.Location = new System.Drawing.Point(3, 165);
            this.L6_ComboBox.Name = "L6_ComboBox";
            this.L6_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L6_ComboBox.TabIndex = 28;
            this.L6_ComboBox.Text = "--";
            this.L6_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L6_ComboBox_SelectedIndexChanged);
            this.L6_ComboBox.TextChanged += new System.EventHandler(this.L6_ComboBox_TextChanged);
            // 
            // L5_ComboBox
            // 
            this.L5_ComboBox.FormattingEnabled = true;
            this.L5_ComboBox.Location = new System.Drawing.Point(3, 138);
            this.L5_ComboBox.Name = "L5_ComboBox";
            this.L5_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L5_ComboBox.TabIndex = 27;
            this.L5_ComboBox.Text = "--";
            this.L5_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L5_ComboBox_SelectedIndexChanged);
            this.L5_ComboBox.TextChanged += new System.EventHandler(this.L5_ComboBox_TextChanged);
            // 
            // L4_ComboBox
            // 
            this.L4_ComboBox.FormattingEnabled = true;
            this.L4_ComboBox.Location = new System.Drawing.Point(3, 111);
            this.L4_ComboBox.Name = "L4_ComboBox";
            this.L4_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L4_ComboBox.TabIndex = 26;
            this.L4_ComboBox.Text = "--";
            this.L4_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L4_ComboBox_SelectedIndexChanged);
            this.L4_ComboBox.TextChanged += new System.EventHandler(this.L4_ComboBox_TextChanged);
            // 
            // L3_ComboBox
            // 
            this.L3_ComboBox.FormattingEnabled = true;
            this.L3_ComboBox.Location = new System.Drawing.Point(3, 84);
            this.L3_ComboBox.Name = "L3_ComboBox";
            this.L3_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L3_ComboBox.TabIndex = 25;
            this.L3_ComboBox.Text = "--";
            this.L3_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L3_ComboBox_SelectedIndexChanged);
            this.L3_ComboBox.TextChanged += new System.EventHandler(this.L3_ComboBox_TextChanged);
            // 
            // L2_ComboBox
            // 
            this.L2_ComboBox.FormattingEnabled = true;
            this.L2_ComboBox.Location = new System.Drawing.Point(3, 57);
            this.L2_ComboBox.Name = "L2_ComboBox";
            this.L2_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L2_ComboBox.TabIndex = 24;
            this.L2_ComboBox.Text = "--";
            this.L2_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L2_ComboBox_SelectedIndexChanged);
            this.L2_ComboBox.TextChanged += new System.EventHandler(this.L2_ComboBox_TextChanged);
            // 
            // L1_ComboBox
            // 
            this.L1_ComboBox.FormattingEnabled = true;
            this.L1_ComboBox.Location = new System.Drawing.Point(3, 30);
            this.L1_ComboBox.Name = "L1_ComboBox";
            this.L1_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L1_ComboBox.TabIndex = 23;
            this.L1_ComboBox.Text = "--";
            this.L1_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L1_ComboBox_SelectedIndexChanged);
            this.L1_ComboBox.TextChanged += new System.EventHandler(this.L1_ComboBox_TextChanged);
            // 
            // L0_ComboBox
            // 
            this.L0_ComboBox.FormattingEnabled = true;
            this.L0_ComboBox.Location = new System.Drawing.Point(3, 3);
            this.L0_ComboBox.Name = "L0_ComboBox";
            this.L0_ComboBox.Size = new System.Drawing.Size(273, 21);
            this.L0_ComboBox.TabIndex = 22;
            this.L0_ComboBox.Text = "--";
            this.L0_ComboBox.SelectedIndexChanged += new System.EventHandler(this.L0_ComboBox_SelectedIndexChanged);
            this.L0_ComboBox.TextChanged += new System.EventHandler(this.L0_ComboBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(506, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Попередній перегляд";
            // 
            // CloseAddResourceButton
            // 
            this.CloseAddResourceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseAddResourceButton.Location = new System.Drawing.Point(1154, 46);
            this.CloseAddResourceButton.Name = "CloseAddResourceButton";
            this.CloseAddResourceButton.Size = new System.Drawing.Size(75, 23);
            this.CloseAddResourceButton.TabIndex = 25;
            this.CloseAddResourceButton.Text = "Close";
            this.CloseAddResourceButton.UseVisualStyleBackColor = true;
            this.CloseAddResourceButton.Click += new System.EventHandler(this.CloseAddResourceButton_Click);
            // 
            // ID
            // 
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ID.Width = 50;
            // 
            // title
            // 
            this.title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.title.HeaderText = "Назва";
            this.title.Name = "title";
            this.title.ReadOnly = true;
            // 
            // quantity
            // 
            this.quantity.HeaderText = "Кількість";
            this.quantity.Name = "quantity";
            this.quantity.ReadOnly = true;
            this.quantity.Width = 70;
            // 
            // units
            // 
            this.units.HeaderText = "Одиниці";
            this.units.Name = "units";
            this.units.ReadOnly = true;
            this.units.Width = 50;
            // 
            // supplier
            // 
            this.supplier.HeaderText = "Постачальник";
            this.supplier.Name = "supplier";
            this.supplier.ReadOnly = true;
            this.supplier.Width = 150;
            // 
            // invoice
            // 
            this.invoice.HeaderText = "Видаткова накладна";
            this.invoice.Name = "invoice";
            this.invoice.ReadOnly = true;
            this.invoice.Width = 200;
            // 
            // supplyDate
            // 
            this.supplyDate.HeaderText = "Дата";
            this.supplyDate.Name = "supplyDate";
            this.supplyDate.ReadOnly = true;
            // 
            // AddResourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1241, 464);
            this.ControlBox = false;
            this.Controls.Add(this.CloseAddResourceButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.AddPreviewDataGridView);
            this.MinimumSize = new System.Drawing.Size(1257, 480);
            this.Name = "AddResourceForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "0";
            this.Text = "Form2";
            this.VisibleChanged += new System.EventHandler(this.AddResourceForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.AddPreviewDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.DataGridView AddPreviewDataGridView;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox L9_ComboBox;
        private System.Windows.Forms.ComboBox L8_ComboBox;
        private System.Windows.Forms.ComboBox L7_ComboBox;
        private System.Windows.Forms.ComboBox L6_ComboBox;
        private System.Windows.Forms.ComboBox L5_ComboBox;
        private System.Windows.Forms.ComboBox L4_ComboBox;
        private System.Windows.Forms.ComboBox L3_ComboBox;
        private System.Windows.Forms.ComboBox L2_ComboBox;
        private System.Windows.Forms.ComboBox L1_ComboBox;
        private System.Windows.Forms.ComboBox L0_ComboBox;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.ComboBox SupplierComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox SupplierInvoiceDateMaskedTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SupplierInvoiceTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button CloseAddResourceButton;
        private System.Windows.Forms.TextBox QuantityTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox UnitsComboBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn units;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplyDate;
    }
}
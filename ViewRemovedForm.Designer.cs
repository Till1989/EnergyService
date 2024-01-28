
namespace EnergyService
{
    partial class ViewRemovedForm
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
            this.SearchByDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.SearchBySenderPersonCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SenderPersonComboBox = new System.Windows.Forms.ComboBox();
            this.RecipientPersonComboBox = new System.Windows.Forms.ComboBox();
            this.SearchByRecipientPersonCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.EquipmentComboBox = new System.Windows.Forms.ComboBox();
            this.SearchByEquipmentCheckBox = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ActIsCreatedSearchComboBox = new System.Windows.Forms.ComboBox();
            this.SupplierComboBox = new System.Windows.Forms.ComboBox();
            this.ClearSearchButton = new System.Windows.Forms.Button();
            this.SearchByActIsCreatedCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SearchByIdCheckBox = new System.Windows.Forms.CheckBox();
            this.SearcByInvoiceCheckBox = new System.Windows.Forms.CheckBox();
            this.SearchBySupplierCheckBox = new System.Windows.Forms.CheckBox();
            this.SearchByDateCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.InvoiceTextBox = new System.Windows.Forms.TextBox();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.CreateActButton = new System.Windows.Forms.Button();
            this.RemovedStockDataGridView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.units = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.equipment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.senderPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recipientPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplyDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RemovedStockDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchByDateTimePicker
            // 
            this.SearchByDateTimePicker.Location = new System.Drawing.Point(49, 7);
            this.SearchByDateTimePicker.Name = "SearchByDateTimePicker";
            this.SearchByDateTimePicker.Size = new System.Drawing.Size(156, 20);
            this.SearchByDateTimePicker.TabIndex = 0;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(1173, 11);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SearchPanel
            // 
            this.SearchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchPanel.Controls.Add(this.SearchBySenderPersonCheckBox);
            this.SearchPanel.Controls.Add(this.label8);
            this.SearchPanel.Controls.Add(this.SenderPersonComboBox);
            this.SearchPanel.Controls.Add(this.RecipientPersonComboBox);
            this.SearchPanel.Controls.Add(this.SearchByRecipientPersonCheckBox);
            this.SearchPanel.Controls.Add(this.label7);
            this.SearchPanel.Controls.Add(this.EquipmentComboBox);
            this.SearchPanel.Controls.Add(this.SearchByEquipmentCheckBox);
            this.SearchPanel.Controls.Add(this.label6);
            this.SearchPanel.Controls.Add(this.ActIsCreatedSearchComboBox);
            this.SearchPanel.Controls.Add(this.SupplierComboBox);
            this.SearchPanel.Controls.Add(this.ClearSearchButton);
            this.SearchPanel.Controls.Add(this.SearchByActIsCreatedCheckBox);
            this.SearchPanel.Controls.Add(this.label5);
            this.SearchPanel.Controls.Add(this.SearchByIdCheckBox);
            this.SearchPanel.Controls.Add(this.SearcByInvoiceCheckBox);
            this.SearchPanel.Controls.Add(this.SearchBySupplierCheckBox);
            this.SearchPanel.Controls.Add(this.SearchByDateCheckBox);
            this.SearchPanel.Controls.Add(this.label4);
            this.SearchPanel.Controls.Add(this.label3);
            this.SearchPanel.Controls.Add(this.label2);
            this.SearchPanel.Controls.Add(this.label1);
            this.SearchPanel.Controls.Add(this.InvoiceTextBox);
            this.SearchPanel.Controls.Add(this.IDTextBox);
            this.SearchPanel.Controls.Add(this.SearchButton);
            this.SearchPanel.Controls.Add(this.SearchByDateTimePicker);
            this.SearchPanel.Location = new System.Drawing.Point(3, 2);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(876, 81);
            this.SearchPanel.TabIndex = 2;
            // 
            // SearchBySenderPersonCheckBox
            // 
            this.SearchBySenderPersonCheckBox.AutoSize = true;
            this.SearchBySenderPersonCheckBox.Location = new System.Drawing.Point(723, 36);
            this.SearchBySenderPersonCheckBox.Name = "SearchBySenderPersonCheckBox";
            this.SearchBySenderPersonCheckBox.Size = new System.Drawing.Size(15, 14);
            this.SearchBySenderPersonCheckBox.TabIndex = 26;
            this.SearchBySenderPersonCheckBox.UseVisualStyleBackColor = true;
            this.SearchBySenderPersonCheckBox.CheckedChanged += new System.EventHandler(this.SearchBySenderPersonCheckBox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(483, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Sender person:";
            // 
            // SenderPersonComboBox
            // 
            this.SenderPersonComboBox.FormattingEnabled = true;
            this.SenderPersonComboBox.Location = new System.Drawing.Point(571, 32);
            this.SenderPersonComboBox.Name = "SenderPersonComboBox";
            this.SenderPersonComboBox.Size = new System.Drawing.Size(146, 21);
            this.SenderPersonComboBox.TabIndex = 24;
            this.SenderPersonComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SenderPersonComboBox_KeyPress);
            // 
            // RecipientPersonComboBox
            // 
            this.RecipientPersonComboBox.FormattingEnabled = true;
            this.RecipientPersonComboBox.Location = new System.Drawing.Point(571, 7);
            this.RecipientPersonComboBox.Name = "RecipientPersonComboBox";
            this.RecipientPersonComboBox.Size = new System.Drawing.Size(146, 21);
            this.RecipientPersonComboBox.TabIndex = 21;
            this.RecipientPersonComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RecipientPersonComboBox_KeyPress);
            // 
            // SearchByRecipientPersonCheckBox
            // 
            this.SearchByRecipientPersonCheckBox.AutoSize = true;
            this.SearchByRecipientPersonCheckBox.Location = new System.Drawing.Point(723, 11);
            this.SearchByRecipientPersonCheckBox.Name = "SearchByRecipientPersonCheckBox";
            this.SearchByRecipientPersonCheckBox.Size = new System.Drawing.Size(15, 14);
            this.SearchByRecipientPersonCheckBox.TabIndex = 23;
            this.SearchByRecipientPersonCheckBox.UseVisualStyleBackColor = true;
            this.SearchByRecipientPersonCheckBox.CheckedChanged += new System.EventHandler(this.SearchByRecipientPersonCheckBox_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(483, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Recipient person:";
            // 
            // EquipmentComboBox
            // 
            this.EquipmentComboBox.FormattingEnabled = true;
            this.EquipmentComboBox.Location = new System.Drawing.Point(305, 53);
            this.EquipmentComboBox.Name = "EquipmentComboBox";
            this.EquipmentComboBox.Size = new System.Drawing.Size(146, 21);
            this.EquipmentComboBox.TabIndex = 18;
            this.EquipmentComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EquipmentComboBox_KeyPress);
            // 
            // SearchByEquipmentCheckBox
            // 
            this.SearchByEquipmentCheckBox.AutoSize = true;
            this.SearchByEquipmentCheckBox.Location = new System.Drawing.Point(457, 57);
            this.SearchByEquipmentCheckBox.Name = "SearchByEquipmentCheckBox";
            this.SearchByEquipmentCheckBox.Size = new System.Drawing.Size(15, 14);
            this.SearchByEquipmentCheckBox.TabIndex = 20;
            this.SearchByEquipmentCheckBox.UseVisualStyleBackColor = true;
            this.SearchByEquipmentCheckBox.CheckedChanged += new System.EventHandler(this.SearchByEquipmentCheckBox_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(242, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Equipment:";
            // 
            // ActIsCreatedSearchComboBox
            // 
            this.ActIsCreatedSearchComboBox.FormattingEnabled = true;
            this.ActIsCreatedSearchComboBox.Items.AddRange(new object[] {
            "True",
            "False"});
            this.ActIsCreatedSearchComboBox.Location = new System.Drawing.Point(84, 57);
            this.ActIsCreatedSearchComboBox.Name = "ActIsCreatedSearchComboBox";
            this.ActIsCreatedSearchComboBox.Size = new System.Drawing.Size(121, 21);
            this.ActIsCreatedSearchComboBox.TabIndex = 17;
            this.ActIsCreatedSearchComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ActIsCreatedSearchComboBox_KeyPress);
            // 
            // SupplierComboBox
            // 
            this.SupplierComboBox.FormattingEnabled = true;
            this.SupplierComboBox.Location = new System.Drawing.Point(305, 30);
            this.SupplierComboBox.Name = "SupplierComboBox";
            this.SupplierComboBox.Size = new System.Drawing.Size(146, 21);
            this.SupplierComboBox.TabIndex = 4;
            this.SupplierComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SupplierComboBox_KeyPress);
            // 
            // ClearSearchButton
            // 
            this.ClearSearchButton.Location = new System.Drawing.Point(796, 31);
            this.ClearSearchButton.Name = "ClearSearchButton";
            this.ClearSearchButton.Size = new System.Drawing.Size(75, 23);
            this.ClearSearchButton.TabIndex = 16;
            this.ClearSearchButton.Text = "Clear";
            this.ClearSearchButton.UseVisualStyleBackColor = true;
            this.ClearSearchButton.Click += new System.EventHandler(this.ClearSearchButton_Click);
            // 
            // SearchByActIsCreatedCheckBox
            // 
            this.SearchByActIsCreatedCheckBox.AutoSize = true;
            this.SearchByActIsCreatedCheckBox.Location = new System.Drawing.Point(211, 60);
            this.SearchByActIsCreatedCheckBox.Name = "SearchByActIsCreatedCheckBox";
            this.SearchByActIsCreatedCheckBox.Size = new System.Drawing.Size(15, 14);
            this.SearchByActIsCreatedCheckBox.TabIndex = 15;
            this.SearchByActIsCreatedCheckBox.UseVisualStyleBackColor = true;
            this.SearchByActIsCreatedCheckBox.CheckedChanged += new System.EventHandler(this.SearchByActIsCreatedCheckBox_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Act is Created:";
            // 
            // SearchByIdCheckBox
            // 
            this.SearchByIdCheckBox.AutoSize = true;
            this.SearchByIdCheckBox.Location = new System.Drawing.Point(211, 36);
            this.SearchByIdCheckBox.Name = "SearchByIdCheckBox";
            this.SearchByIdCheckBox.Size = new System.Drawing.Size(15, 14);
            this.SearchByIdCheckBox.TabIndex = 13;
            this.SearchByIdCheckBox.UseVisualStyleBackColor = true;
            this.SearchByIdCheckBox.CheckedChanged += new System.EventHandler(this.SearchByIdCheckBox_CheckedChanged);
            // 
            // SearcByInvoiceCheckBox
            // 
            this.SearcByInvoiceCheckBox.AutoSize = true;
            this.SearcByInvoiceCheckBox.Location = new System.Drawing.Point(457, 10);
            this.SearcByInvoiceCheckBox.Name = "SearcByInvoiceCheckBox";
            this.SearcByInvoiceCheckBox.Size = new System.Drawing.Size(15, 14);
            this.SearcByInvoiceCheckBox.TabIndex = 12;
            this.SearcByInvoiceCheckBox.UseVisualStyleBackColor = true;
            this.SearcByInvoiceCheckBox.CheckedChanged += new System.EventHandler(this.SearcByInvoiceCheckBox_CheckedChanged);
            // 
            // SearchBySupplierCheckBox
            // 
            this.SearchBySupplierCheckBox.AutoSize = true;
            this.SearchBySupplierCheckBox.Location = new System.Drawing.Point(457, 35);
            this.SearchBySupplierCheckBox.Name = "SearchBySupplierCheckBox";
            this.SearchBySupplierCheckBox.Size = new System.Drawing.Size(15, 14);
            this.SearchBySupplierCheckBox.TabIndex = 11;
            this.SearchBySupplierCheckBox.UseVisualStyleBackColor = true;
            this.SearchBySupplierCheckBox.CheckedChanged += new System.EventHandler(this.SearchBySupplierCheckBox_CheckedChanged);
            // 
            // SearchByDateCheckBox
            // 
            this.SearchByDateCheckBox.AutoSize = true;
            this.SearchByDateCheckBox.Location = new System.Drawing.Point(211, 10);
            this.SearchByDateCheckBox.Name = "SearchByDateCheckBox";
            this.SearchByDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.SearchByDateCheckBox.TabIndex = 10;
            this.SearchByDateCheckBox.UseVisualStyleBackColor = true;
            this.SearchByDateCheckBox.CheckedChanged += new System.EventHandler(this.SearchByDateCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Supplier:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Invoice:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Date:";
            // 
            // InvoiceTextBox
            // 
            this.InvoiceTextBox.Location = new System.Drawing.Point(305, 7);
            this.InvoiceTextBox.Name = "InvoiceTextBox";
            this.InvoiceTextBox.Size = new System.Drawing.Size(146, 20);
            this.InvoiceTextBox.TabIndex = 4;
            this.InvoiceTextBox.TextChanged += new System.EventHandler(this.InvoiceTextBox_TextChanged);
            // 
            // IDTextBox
            // 
            this.IDTextBox.Location = new System.Drawing.Point(59, 33);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.Size = new System.Drawing.Size(146, 20);
            this.IDTextBox.TabIndex = 3;
            this.IDTextBox.TextChanged += new System.EventHandler(this.IDTextBox_TextChanged);
            this.IDTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IDTextBox_KeyPress);
            // 
            // SearchButton
            // 
            this.SearchButton.Enabled = false;
            this.SearchButton.Location = new System.Drawing.Point(796, 5);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // CreateActButton
            // 
            this.CreateActButton.Enabled = false;
            this.CreateActButton.Location = new System.Drawing.Point(885, 2);
            this.CreateActButton.Name = "CreateActButton";
            this.CreateActButton.Size = new System.Drawing.Size(179, 81);
            this.CreateActButton.TabIndex = 3;
            this.CreateActButton.Text = "Create Act";
            this.CreateActButton.UseVisualStyleBackColor = true;
            // 
            // RemovedStockDataGridView
            // 
            this.RemovedStockDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RemovedStockDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.RemovedStockDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RemovedStockDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.title,
            this.quantity,
            this.units,
            this.equipment,
            this.senderPerson,
            this.recipientPerson,
            this.offDate,
            this.supplyDate,
            this.actCreated,
            this.supplier,
            this.invoice});
            this.RemovedStockDataGridView.Location = new System.Drawing.Point(3, 89);
            this.RemovedStockDataGridView.Name = "RemovedStockDataGridView";
            this.RemovedStockDataGridView.ReadOnly = true;
            this.RemovedStockDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RemovedStockDataGridView.Size = new System.Drawing.Size(1247, 517);
            this.RemovedStockDataGridView.TabIndex = 4;
            this.RemovedStockDataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RemovedStockDataGridView_CellMouseClick);
            this.RemovedStockDataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.RemovedStockDataGridView_RowHeaderMouseClick);
            this.RemovedStockDataGridView.SelectionChanged += new System.EventHandler(this.RemovedStockDataGridView_SelectionChanged);
            this.RemovedStockDataGridView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RemovedStockDataGridView_KeyPress);
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
            this.title.HeaderText = "Name";
            this.title.Name = "title";
            this.title.ReadOnly = true;
            this.title.Width = 300;
            // 
            // quantity
            // 
            this.quantity.HeaderText = "Quantity";
            this.quantity.Name = "quantity";
            this.quantity.ReadOnly = true;
            this.quantity.Width = 60;
            // 
            // units
            // 
            this.units.HeaderText = "Units";
            this.units.Name = "units";
            this.units.ReadOnly = true;
            this.units.Width = 50;
            // 
            // equipment
            // 
            this.equipment.HeaderText = "Equipment";
            this.equipment.Name = "equipment";
            this.equipment.ReadOnly = true;
            // 
            // senderPerson
            // 
            this.senderPerson.HeaderText = "Sender Person";
            this.senderPerson.Name = "senderPerson";
            this.senderPerson.ReadOnly = true;
            this.senderPerson.Width = 150;
            // 
            // recipientPerson
            // 
            this.recipientPerson.HeaderText = "Received Person";
            this.recipientPerson.Name = "recipientPerson";
            this.recipientPerson.ReadOnly = true;
            // 
            // offDate
            // 
            this.offDate.HeaderText = "Off Date";
            this.offDate.Name = "offDate";
            this.offDate.ReadOnly = true;
            // 
            // supplyDate
            // 
            this.supplyDate.HeaderText = "Supply Date";
            this.supplyDate.Name = "supplyDate";
            this.supplyDate.ReadOnly = true;
            this.supplyDate.Width = 70;
            // 
            // actCreated
            // 
            this.actCreated.HeaderText = "Act Is Created";
            this.actCreated.Name = "actCreated";
            this.actCreated.ReadOnly = true;
            // 
            // supplier
            // 
            this.supplier.HeaderText = "Supplier";
            this.supplier.Name = "supplier";
            this.supplier.ReadOnly = true;
            this.supplier.Width = 300;
            // 
            // invoice
            // 
            this.invoice.HeaderText = "Invoice";
            this.invoice.Name = "invoice";
            this.invoice.ReadOnly = true;
            this.invoice.Width = 200;
            // 
            // ViewRemovedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1260, 618);
            this.ControlBox = false;
            this.Controls.Add(this.RemovedStockDataGridView);
            this.Controls.Add(this.CreateActButton);
            this.Controls.Add(this.SearchPanel);
            this.Controls.Add(this.CloseButton);
            this.MinimizeBox = false;
            this.Name = "ViewRemovedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.VisibleChanged += new System.EventHandler(this.ViewRemovedForm_VisibleChanged);
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RemovedStockDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker SearchByDateTimePicker;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Panel SearchPanel;
        private System.Windows.Forms.CheckBox SearchByIdCheckBox;
        private System.Windows.Forms.CheckBox SearcByInvoiceCheckBox;
        private System.Windows.Forms.CheckBox SearchBySupplierCheckBox;
        private System.Windows.Forms.CheckBox SearchByDateCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InvoiceTextBox;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button ClearSearchButton;
        private System.Windows.Forms.CheckBox SearchByActIsCreatedCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CreateActButton;
        private System.Windows.Forms.ComboBox SupplierComboBox;
        private System.Windows.Forms.DataGridView RemovedStockDataGridView;
        private System.Windows.Forms.ComboBox ActIsCreatedSearchComboBox;
        private System.Windows.Forms.ComboBox RecipientPersonComboBox;
        private System.Windows.Forms.CheckBox SearchByRecipientPersonCheckBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox EquipmentComboBox;
        private System.Windows.Forms.CheckBox SearchByEquipmentCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox SenderPersonComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox SearchBySenderPersonCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn units;
        private System.Windows.Forms.DataGridViewTextBoxColumn equipment;
        private System.Windows.Forms.DataGridViewTextBoxColumn senderPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn recipientPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn offDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplyDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn actCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoice;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace EnergyService
{
    public partial class ViewRemovedForm : Form
    {
        OleDbConnection StockDBConnection;
        OleDbCommand StockDBCommand;
        OleDbDataReader StockReader;

        OleDbConnection PersonsDBConnection;
        OleDbCommand PersonsDBCommand;
        OleDbDataReader PersonsReader;

        public ViewRemovedForm()
        {
            InitializeComponent();
        }


        //CLASSES/////////////////////////////////////////////////
        public class Group
        {
            public string title;
            public int L0ID;
            public int L1ID;
            public int L2ID;
            public int L3ID;
            public int L4ID;
            public int L5ID;
            public int L6ID;
            public int L7ID;
            public int L8ID;
            public int L9ID;

            public Group(string title, int L0ID, int L1ID, int L2ID, int L3ID, int L4ID, int L5ID,
                int L6ID, int L7ID, int L8ID, int L9ID)
            {
                this.title = title;
                this.L0ID = L0ID;
                this.L1ID = L1ID;
                this.L2ID = L2ID;
                this.L3ID = L3ID;
                this.L4ID = L4ID;
                this.L5ID = L5ID;
                this.L6ID = L6ID;
                this.L7ID = L7ID;
                this.L8ID = L8ID;
                this.L9ID = L9ID;
            }

            public override string ToString()
            {
                return title;
            }
        }
        public class WroteOffResource
        {
            public int ID;
            public string title;
            public int quantity;
            public string units;
            public string supplier;
            public string invoice;
            public string supplyDate;
            public string equipment;
            public string recipientPerson;
            public string senderPerson;
            public string offDate;
            public bool actIsCreated;

            public WroteOffResource(int ID, string title, int quantity, string units, string supplier,
                string invoice, string supplyDate, string equipment, string recipientPerson, string offDate, bool actIsCreated, string senderPerson)
            {
                this.ID = ID;
                this.title = title;
                this.quantity = quantity;
                this.units = units;
                this.supplier = supplier;
                this.invoice = invoice;
                this.supplyDate = supplyDate;
                this.equipment = equipment;
                this.recipientPerson = recipientPerson;
                this.senderPerson = senderPerson;
                this.offDate = offDate;
                this.actIsCreated = actIsCreated;
            }
        }
        public class Supplier
        {
            public string title;
            public int taxNumber;
            public Supplier(string title, int taxNumber)
            {
                this.title = title;
                this.taxNumber = taxNumber;
            }
            public override string ToString()
            {
                return title;
            }
        }

        private class Tmp
        {
            string[] searchOptions = new string[3];
            public Tmp(string option0, string option1, string option2)
            {
                this.searchOptions[0] = option0;
                this.searchOptions[1] = option1;
                this.searchOptions[2] = option2;
            }
        }

        public class Equipment
        {
            public string title;
            public string ID;
            public Equipment(string title, string ID)
            {
                this.title = title;
                this.ID = ID;
            }
            public override string ToString()
            {
                return title + "/" + ID;
            }
        }
        public class Person
        {
            public string name;
            public string position;
            public Person(string name, string position)
            {
                this.name = name;
                this.position = position;
            }
            public override string ToString()
            {
                return name;
            }
        }


        //FUNCTIONS/////////////////////////////////////////////////////////////////////////////////
        private string SetProvider(string DataBaseName)
        {
            String provider = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + Environment.CurrentDirectory + "\\Data\\DataBases\\" + DataBaseName;

            return provider;
        }

        private void LoadClearedData(string commandText)
        {
            RemovedStockDataGridView.Rows.Clear();

            StockDBCommand = new OleDbCommand(commandText +" ORDER BY ID ASC", StockDBConnection);
            WroteOffResource[] tmp = new WroteOffResource[1000];
            int i = 0;
            this.StockReader = StockDBCommand.ExecuteReader();
            while (StockReader.Read())
            {


                tmp[i] = new WroteOffResource(Convert.ToInt32(StockReader[0]), StockReader[1].ToString(), Convert.ToInt32(StockReader[2]), StockReader[3].ToString(), StockReader[4].ToString(),
                    StockReader[5].ToString(), StockReader[6].ToString(), StockReader[7].ToString(), StockReader[8].ToString(), StockReader[9].ToString(), Convert.ToBoolean(StockReader[10]), StockReader[11].ToString());

                i++;
            }

            tmp = tmp.Where(t => t != null).ToArray();

            for (int y = 0; y < tmp.Length; y++)
            {
                RemovedStockDataGridView.Rows.Add(tmp[y].ID, tmp[y].title, tmp[y].quantity, tmp[y].units, tmp[y].equipment, tmp[y].senderPerson.ToString(), tmp[y].recipientPerson, tmp[y].offDate, tmp[y].supplyDate, tmp[y].actIsCreated.ToString(), tmp[y].supplier, tmp[y].invoice);
            }

            RemovedStockDataGridView.ClearSelection();
        }

        private Supplier[] GetSuppliers()
        {
            Supplier[] tmp = new Supplier[1000];
            StockDBCommand = new OleDbCommand("SELECT * FROM Suppliers ORDER BY title ASC", StockDBConnection);
            int i = 0;

            StockReader = StockDBCommand.ExecuteReader();
            while (StockReader.Read())
            {
                tmp[i] = new Supplier(StockReader[0].ToString(), Convert.ToInt32(StockReader[1]));
                i++;
            }
            tmp = tmp.Where(t => t != null).ToArray();

            return tmp;
        }

        private Equipment[] GetEquipment()
        {
            Equipment[] tmp = new Equipment[1000];
            StockDBCommand = new OleDbCommand("SELECT * FROM Equipment ORDER BY title ASC", StockDBConnection);
            int i = 0;

            StockReader = StockDBCommand.ExecuteReader();
            while (StockReader.Read())
            {
                tmp[i] = new Equipment(StockReader[0].ToString(), StockReader[1].ToString());
                i++;
            }
            tmp = tmp.Where(t => t != null).ToArray();

            return tmp;
        }

        private Person[] GetPersons()
        {
            Person[] tmp = new Person[1000];
            PersonsDBCommand = new OleDbCommand("SELECT * FROM Persons", PersonsDBConnection);

            int i = 0;

            this.PersonsReader = PersonsDBCommand.ExecuteReader();
            while (PersonsReader.Read())
            {
                tmp[i] = new Person(PersonsReader[0].ToString(), PersonsReader[1].ToString());
                i++;
            }
            tmp = tmp.Where(temp => temp != null).ToArray();

            return tmp;
        }

        //EVENTS/////////////////////////////////////////////////////////////////////////////////////
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void IDTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }

        }



        private void SearchByDateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(SearchByDateCheckBox.Checked==true||SearchByIdCheckBox.Checked==true||SearcByInvoiceCheckBox.Checked==true||SearchBySupplierCheckBox.Checked==true||
                SearchByActIsCreatedCheckBox.Checked==true)
            {
                SearchButton.Enabled = true;
            }
            else
            {
                SearchButton.Enabled = false;
            }
        }

        private void SearchByIdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(IDTextBox.Text=="")
            {
                SearchByIdCheckBox.Checked = false;
            }

            if (SearchBySenderPersonCheckBox.Checked==true || SearchByRecipientPersonCheckBox.Checked==true || SearchByEquipmentCheckBox.Checked==true || SearchByDateCheckBox.Checked == true || SearchByIdCheckBox.Checked == true || SearcByInvoiceCheckBox.Checked == true || SearchBySupplierCheckBox.Checked == true ||
                SearchByActIsCreatedCheckBox.Checked == true)
            {
                SearchButton.Enabled = true;
            }
            else
            {
                SearchButton.Enabled = false;
            }
        }

        private void SearchByActIsCreatedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SearchBySenderPersonCheckBox.Checked == true || SearchByRecipientPersonCheckBox.Checked == true || SearchByEquipmentCheckBox.Checked == true || SearchByDateCheckBox.Checked == true || SearchByIdCheckBox.Checked == true || SearcByInvoiceCheckBox.Checked == true || SearchBySupplierCheckBox.Checked == true ||
                SearchByActIsCreatedCheckBox.Checked == true)
            {
                SearchButton.Enabled = true;
            }
            else
            {
                SearchButton.Enabled = false;
            }
        }

        private void SearcByInvoiceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (InvoiceTextBox.Text == "")
            {
                SearcByInvoiceCheckBox.Checked = false;
            }
            if (SearchBySenderPersonCheckBox.Checked == true || SearchByRecipientPersonCheckBox.Checked == true || SearchByEquipmentCheckBox.Checked == true || SearchByDateCheckBox.Checked == true || SearchByIdCheckBox.Checked == true || SearcByInvoiceCheckBox.Checked == true || SearchBySupplierCheckBox.Checked == true ||
                SearchByActIsCreatedCheckBox.Checked == true)
            {
                SearchButton.Enabled = true;
            }
            else
            {
                SearchButton.Enabled = false;
            }
        }

        private void SearchBySupplierCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SearchBySenderPersonCheckBox.Checked == true || SearchByRecipientPersonCheckBox.Checked == true || SearchByEquipmentCheckBox.Checked == true || SearchByDateCheckBox.Checked == true || SearchByIdCheckBox.Checked == true || SearcByInvoiceCheckBox.Checked == true || SearchBySupplierCheckBox.Checked == true ||
                SearchByActIsCreatedCheckBox.Checked == true)
            {
                SearchButton.Enabled = true;
            }
            else
            {
                SearchButton.Enabled = false;
            }
        }

        private void SearchByEquipmentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SearchBySenderPersonCheckBox.Checked == true || SearchByRecipientPersonCheckBox.Checked == true || SearchByEquipmentCheckBox.Checked == true || SearchByDateCheckBox.Checked == true || SearchByIdCheckBox.Checked == true || SearcByInvoiceCheckBox.Checked == true || SearchBySupplierCheckBox.Checked == true ||
                SearchByActIsCreatedCheckBox.Checked == true)
            {
                SearchButton.Enabled = true;
            }
            else
            {
                SearchButton.Enabled = false;
            }
        }

        private void SearchByRecipientPersonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SearchBySenderPersonCheckBox.Checked == true || SearchByRecipientPersonCheckBox.Checked == true || SearchByEquipmentCheckBox.Checked == true || SearchByDateCheckBox.Checked == true || SearchByIdCheckBox.Checked == true || SearcByInvoiceCheckBox.Checked == true || SearchBySupplierCheckBox.Checked == true ||
                SearchByActIsCreatedCheckBox.Checked == true)
            {
                SearchButton.Enabled = true;
            }
            else
            {
                SearchButton.Enabled = false;
            }
        }

        private void SearchBySenderPersonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SearchBySenderPersonCheckBox.Checked == true || SearchByRecipientPersonCheckBox.Checked == true || SearchByEquipmentCheckBox.Checked == true || SearchByDateCheckBox.Checked == true || SearchByIdCheckBox.Checked == true || SearcByInvoiceCheckBox.Checked == true || SearchBySupplierCheckBox.Checked == true ||
                SearchByActIsCreatedCheckBox.Checked == true)
            {
                SearchButton.Enabled = true;
            }
            else
            {
                SearchButton.Enabled = false;
            }
        }



        private void SearchButton_Click(object sender, EventArgs e)
        {
            string[] searchOptions = new string[5];

            string commandText = "SELECT * FROM WroteOffResources WHERE";
            int cmdLength = commandText.Length;
            if (SearchByDateCheckBox.Checked==true)
            {
                if(commandText.Length> cmdLength)
                {
                    commandText += " AND";
                }
                commandText += " " + "offDate=" +"'"+ SearchByDateTimePicker.Value.ToString("dd/MM/yyyy")+"'";
            }
            if(SearchByIdCheckBox.Checked==true)
            {
                if (commandText.Length > cmdLength)
                {
                    commandText += " AND";
                }
                commandText += " " + "ID=" + Convert.ToInt32(IDTextBox.Text);
            }
            if (SearcByInvoiceCheckBox.Checked == true)
            {
                if (commandText.Length > cmdLength)
                {
                    commandText += " AND";
                }
                commandText += " " + "invoice=" + "'" + InvoiceTextBox.Text + "'";
            }
            if (SearchBySupplierCheckBox.Checked == true)
            {
                if (commandText.Length > cmdLength)
                {
                    commandText += " AND";
                }
                commandText += " " + "supplier=" + "'" + SupplierComboBox.Text + "'";
            }
            if (SearchByActIsCreatedCheckBox.Checked == true)
            {
                if (commandText.Length > cmdLength)
                {
                    commandText += " AND";
                }
                commandText += " " + "actIsCreated=" + ActIsCreatedSearchComboBox.Text;
            }


            if (SearchByEquipmentCheckBox.Checked == true)
            {
                if (commandText.Length > cmdLength)
                {
                    commandText += " AND";
                }
                commandText += " " + "equipment=" + "'" + ((Equipment)EquipmentComboBox.SelectedItem).title + "'";
            }

            if (SearchByRecipientPersonCheckBox.Checked == true)
            {
                if (commandText.Length > cmdLength)
                {
                    commandText += " AND";
                }
                commandText += " " + "recipientPerson=" + "'" + RecipientPersonComboBox.Text + "'";
            }

            if (SearchBySenderPersonCheckBox.Checked == true)
            {
                if (commandText.Length > cmdLength)
                {
                    commandText += " AND";
                }
                commandText += " " + "senderPerson=" + "'" + SenderPersonComboBox.Text + "'";
            }


            LoadClearedData(commandText);





        }

        private void ViewRemovedForm_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible==true)
            {
                StockDBConnection = new OleDbConnection(SetProvider("Stock.accdb"));
                StockDBConnection.Open();

                PersonsDBConnection = new OleDbConnection(SetProvider("Persons.accdb"));
                PersonsDBConnection.Open();

                SupplierComboBox.Items.AddRange(GetSuppliers());
                EquipmentComboBox.Items.AddRange(GetEquipment());
                RecipientPersonComboBox.Items.AddRange(GetPersons());
                SenderPersonComboBox.Items.AddRange(GetPersons());
                LoadClearedData("SELECT * FROM WroteOffResources");

                ActIsCreatedSearchComboBox.SelectedIndex = 0;
                SupplierComboBox.SelectedIndex = 0;
                EquipmentComboBox.SelectedIndex = 0;
                RecipientPersonComboBox.SelectedIndex = 0;
                SenderPersonComboBox.SelectedIndex = 0;
            }
            else
            {
                StockDBConnection.Close();
                PersonsDBConnection.Close();
            }
        }

        private void SupplierComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void ClearSearchButton_Click(object sender, EventArgs e)
        {
            SearchByDateCheckBox.Checked = false;
            SearchByIdCheckBox.Checked = false;
            SearcByInvoiceCheckBox.Checked = false;
            SearchBySupplierCheckBox.Checked = false;
            SearchByActIsCreatedCheckBox.Checked = false;

            IDTextBox.Text = "";
            InvoiceTextBox.Text = "";

            CreateActButton.Enabled = false;

            LoadClearedData("SELECT * FROM WroteOffResources");
        }

        private void RemovedStockDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*if (e.ColumnIndex > -1)
            {
                RemovedStockDataGridView.ClearSelection();
                CreateActButton.Enabled = false;
            }
            else
            {
                RemovedStockDataGridView.Select();
            }*/
        }

        private void ActIsCreatedSearchComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void RemovedStockDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
//////////////////
        }

        private void IDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (IDTextBox.Text == "")
            {
                SearchByIdCheckBox.Checked = false;
            }
        }

        private void InvoiceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (InvoiceTextBox.Text == "")
            {
                SearcByInvoiceCheckBox.Checked = false;
            }
        }

        private void EquipmentComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void RecipientPersonComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void SenderPersonComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void RemovedStockDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(RemovedStockDataGridView.SelectedRows.Count.ToString());

            if(RemovedStockDataGridView.SelectedRows.Count>0)
            {
                bool clear = false;
                for(int i=0;i< RemovedStockDataGridView.SelectedRows.Count;i++)
                {
                    if(RemovedStockDataGridView.SelectedRows[i].Index == RemovedStockDataGridView.RowCount - 1)
                    {
                        clear = true;
                    }
                }

                if (RemovedStockDataGridView.RowCount > 1 && !clear)
                {
                    if (RemovedStockDataGridView.SelectedRows.Count > 0 && RemovedStockDataGridView.SelectedRows.Count < 11)
                    {
                        CreateActButton.Enabled = true;
                    }
                    else
                    {
                        CreateActButton.Enabled = false;
                    }
                }
                else
                {
                    CreateActButton.Enabled = false;
                    RemovedStockDataGridView.ClearSelection();
                }
            }
            else
            {
                CreateActButton.Enabled = false;
                RemovedStockDataGridView.ClearSelection();
            }

        }

        private void RemovedStockDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (number == 27)
            {
                RemovedStockDataGridView.ClearSelection();
            }
        }
    }
}

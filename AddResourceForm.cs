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
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Diagnostics;
using System.Drawing.Printing;

namespace EnergyService
{
    public partial class AddResourceForm : Form
    {
        public AddResourceForm()
        {
            InitializeComponent();
        }
        OleDbConnection StockDBConnection;
        OleDbCommand StockDBCommand;
        OleDbDataReader reader;


        Resource[] temp_resousces = new Resource[0];


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
        public class Resource
        {
            public int ID;
            public string title;
            public int quantity;
            public string units;
            public string supplier;
            public string invoice;
            public string supplyDate;

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

            public Resource(int ID, string title, int quantity, string units, string supplier,
                string invoice, string supplyDate, int L0ID, int L1ID, int L2ID, int L3ID, int L4ID, int L5ID,
                int L6ID, int L7ID, int L8ID, int L9ID)
            {
                this.ID = ID;
                this.title = title;
                this.quantity = quantity;
                this.units = units;
                this.supplier = supplier;
                this.invoice = invoice;
                this.supplyDate = supplyDate;
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
        }
        public class Supplier
        {
            public string title;
            public Int64 taxNumber;
            public Supplier(string title, Int64 taxNumber)
            {
                this.title = title;
                this.taxNumber = taxNumber;
            }
            public override string ToString()
            {
                return title;
            }
        }

        ///DEFINITIONS///////////////////////////////////////////////////////////////////


        //FUNCTIONS***************************************************************************
        private string SetProvider(string DataBaseName)
        {
            String provider = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + Environment.CurrentDirectory + "\\Data\\DataBases\\" + DataBaseName;

            return provider;
        }

        public Group[] GetGroups(int levelID)
        {
            Group[] tmp = new Group[1000];

            if (levelID == 0)
            {
                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID>0 AND L1ID=0 AND L2ID=0 AND L3ID=0" +
                    "AND L4ID=0 AND L5ID=0 AND L6ID=0 AND L7ID=0 AND L8ID=0 AND L9ID=0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }
            if (levelID == 1)
            {
                Group gr = (Group)L0_ComboBox.SelectedItem;
                int L0ID = gr.L0ID;
                int L1ID = gr.L1ID;
                int L2ID = gr.L2ID;
                int L3ID = gr.L3ID;
                int L4ID = gr.L4ID;
                int L5ID = gr.L5ID;
                int L6ID = gr.L6ID;
                int L7ID = gr.L7ID;
                int L8ID = gr.L8ID;
                int L9ID = gr.L9ID;

                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID=" + L0ID + " AND L1ID>0 AND L2ID=0 AND L3ID=0" +
                    "AND L4ID=0 AND L5ID=0 AND L6ID=0 AND L7ID=0 AND L8ID=0 AND L9ID=0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }
            if (levelID == 2)
            {
                Group gr = (Group)L1_ComboBox.SelectedItem;
                int L0ID = gr.L0ID;
                int L1ID = gr.L1ID;
                int L2ID = gr.L2ID;
                int L3ID = gr.L3ID;
                int L4ID = gr.L4ID;
                int L5ID = gr.L5ID;
                int L6ID = gr.L6ID;
                int L7ID = gr.L7ID;
                int L8ID = gr.L8ID;
                int L9ID = gr.L9ID;

                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID=" + L0ID + " AND L1ID=" + L1ID + " AND L2ID>0 AND L3ID=0" +
                    "AND L4ID=0 AND L5ID=0 AND L6ID=0 AND L7ID=0 AND L8ID=0 AND L9ID=0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }
            if (levelID == 3)
            {
                Group gr = (Group)L2_ComboBox.SelectedItem;
                int L0ID = gr.L0ID;
                int L1ID = gr.L1ID;
                int L2ID = gr.L2ID;
                int L3ID = gr.L3ID;
                int L4ID = gr.L4ID;
                int L5ID = gr.L5ID;
                int L6ID = gr.L6ID;
                int L7ID = gr.L7ID;
                int L8ID = gr.L8ID;
                int L9ID = gr.L9ID;

                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID=" + L0ID + " AND L1ID=" + L1ID + " AND L2ID=" + L2ID + " AND L3ID>0" +
                    "AND L4ID=0 AND L5ID=0 AND L6ID=0 AND L7ID=0 AND L8ID=0 AND L9ID=0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }
            if (levelID == 4)
            {
                Group gr = (Group)L3_ComboBox.SelectedItem;
                int L0ID = gr.L0ID;
                int L1ID = gr.L1ID;
                int L2ID = gr.L2ID;
                int L3ID = gr.L3ID;
                int L4ID = gr.L4ID;
                int L5ID = gr.L5ID;
                int L6ID = gr.L6ID;
                int L7ID = gr.L7ID;
                int L8ID = gr.L8ID;
                int L9ID = gr.L9ID;

                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID=" + L0ID + " AND L1ID=" + L1ID + " AND L2ID=" + L2ID + " AND L3ID=" + L3ID +
                    " AND L4ID>0 AND L5ID=0 AND L6ID=0 AND L7ID=0 AND L8ID=0 AND L9ID=0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }
            if (levelID == 5)
            {
                Group gr = (Group)L4_ComboBox.SelectedItem;
                int L0ID = gr.L0ID;
                int L1ID = gr.L1ID;
                int L2ID = gr.L2ID;
                int L3ID = gr.L3ID;
                int L4ID = gr.L4ID;
                int L5ID = gr.L5ID;
                int L6ID = gr.L6ID;
                int L7ID = gr.L7ID;
                int L8ID = gr.L8ID;
                int L9ID = gr.L9ID;

                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID=" + L0ID + " AND L1ID=" + L1ID + " AND L2ID=" + L2ID + " AND L3ID=" + L3ID +
                    " AND L4ID=" + L4ID + " AND L5ID>0 AND L6ID=0 AND L7ID=0 AND L8ID=0 AND L9ID=0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }
            if (levelID == 6)
            {
                Group gr = (Group)L5_ComboBox.SelectedItem;
                int L0ID = gr.L0ID;
                int L1ID = gr.L1ID;
                int L2ID = gr.L2ID;
                int L3ID = gr.L3ID;
                int L4ID = gr.L4ID;
                int L5ID = gr.L5ID;
                int L6ID = gr.L6ID;
                int L7ID = gr.L7ID;
                int L8ID = gr.L8ID;
                int L9ID = gr.L9ID;

                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID=" + L0ID + " AND L1ID=" + L1ID + " AND L2ID=" + L2ID + " AND L3ID=" + L3ID +
                    " AND L4ID=" + L4ID + " AND L5ID=" + L5ID + " AND L6ID>0 AND L7ID=0 AND L8ID=0 AND L9ID=0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }
            if (levelID == 7)
            {
                Group gr = (Group)L6_ComboBox.SelectedItem;
                int L0ID = gr.L0ID;
                int L1ID = gr.L1ID;
                int L2ID = gr.L2ID;
                int L3ID = gr.L3ID;
                int L4ID = gr.L4ID;
                int L5ID = gr.L5ID;
                int L6ID = gr.L6ID;
                int L7ID = gr.L7ID;
                int L8ID = gr.L8ID;
                int L9ID = gr.L9ID;

                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID=" + L0ID + " AND L1ID=" + L1ID + " AND L2ID=" + L2ID + " AND L3ID=" + L3ID +
                    " AND L4ID=" + L4ID + " AND L5ID=" + L5ID + " AND L6ID=" + L6ID + " AND L7ID>0 AND L8ID=0 AND L9ID=0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }
            if (levelID == 8)
            {
                Group gr = (Group)L7_ComboBox.SelectedItem;
                int L0ID = gr.L0ID;
                int L1ID = gr.L1ID;
                int L2ID = gr.L2ID;
                int L3ID = gr.L3ID;
                int L4ID = gr.L4ID;
                int L5ID = gr.L5ID;
                int L6ID = gr.L6ID;
                int L7ID = gr.L7ID;
                int L8ID = gr.L8ID;
                int L9ID = gr.L9ID;

                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID=" + L0ID + " AND L1ID=" + L1ID + " AND L2ID=" + L2ID + " AND L3ID=" + L3ID +
                    " AND L4ID=" + L4ID + " AND L5ID=" + L5ID + " AND L6ID=" + L6ID + " AND L7ID=" + L7ID + " AND L8ID>0 AND L9ID=0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }
            if (levelID == 9)
            {
                Group gr = (Group)L8_ComboBox.SelectedItem;
                int L0ID = gr.L0ID;
                int L1ID = gr.L1ID;
                int L2ID = gr.L2ID;
                int L3ID = gr.L3ID;
                int L4ID = gr.L4ID;
                int L5ID = gr.L5ID;
                int L6ID = gr.L6ID;
                int L7ID = gr.L7ID;
                int L8ID = gr.L8ID;
                int L9ID = gr.L9ID;

                StockDBCommand = new OleDbCommand("SELECT * FROM Groups WHERE L0ID=" + L0ID + " AND L1ID=" + L1ID + " AND L2ID=" + L2ID + " AND L3ID=" + L3ID +
                    " AND L4ID=" + L4ID + " AND L5ID=" + L5ID + " AND L6ID=" + L6ID + " AND L7ID=" + L7ID + " AND L8ID=" + L8ID + " AND L9ID>0", StockDBConnection);

                int i = 0;

                this.reader = StockDBCommand.ExecuteReader();
                while (reader.Read())
                {
                    tmp[i] = new Group(reader[0].ToString(), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]),
                        Convert.ToInt32(reader[4]), Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]),
                        Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), Convert.ToInt32(reader[10]));
                    i++;
                }
            }

            tmp = tmp.Where(temp => temp != null).ToArray();

            return tmp;
        }
        private Supplier[] GetSuppliers()
        {
            Supplier[] tmp = new Supplier[1000];
            StockDBCommand = new OleDbCommand("SELECT * FROM Suppliers", StockDBConnection);
            int i = 0;

            reader = StockDBCommand.ExecuteReader();
            while (reader.Read())
            {
                tmp[i] = new Supplier(reader[0].ToString(), Convert.ToInt64(reader[1]));
                i++;
            }
            tmp = tmp.Where(t => t != null).ToArray();

            return tmp;
        }

        private int[] GetCurrentGroup()
        {
            int[] tmp = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Group temp;

            if (L0_ComboBox.Text != "--")
            {
                temp = (Group)L0_ComboBox.SelectedItem;
                tmp[0] = temp.L0ID;
                if (L1_ComboBox.Text != "--")
                {
                    temp = (Group)L1_ComboBox.SelectedItem;
                    tmp[1] = temp.L1ID;
                    if (L2_ComboBox.Text != "--")
                    {
                        temp = (Group)L2_ComboBox.SelectedItem;
                        tmp[2] = temp.L2ID;
                        if (L3_ComboBox.Text != "--")
                        {
                            temp = (Group)L3_ComboBox.SelectedItem;
                            tmp[3] = temp.L3ID;
                            if (L4_ComboBox.Text != "--")
                            {
                                temp = (Group)L4_ComboBox.SelectedItem;
                                tmp[4] = temp.L4ID;
                                if (L5_ComboBox.Text != "--")
                                {
                                    temp = (Group)L5_ComboBox.SelectedItem;
                                    tmp[5] = temp.L5ID;
                                    if (L6_ComboBox.Text != "--")
                                    {
                                        temp = (Group)L6_ComboBox.SelectedItem;
                                        tmp[6] = temp.L6ID;
                                        if (L7_ComboBox.Text != "--")
                                        {
                                            temp = (Group)L7_ComboBox.SelectedItem;
                                            tmp[7] = temp.L7ID;
                                            if (L8_ComboBox.Text != "--")
                                            {
                                                temp = (Group)L8_ComboBox.SelectedItem;
                                                tmp[8] = temp.L8ID;
                                                if (L9_ComboBox.Text != "--")
                                                {
                                                    temp = (Group)L9_ComboBox.SelectedItem;
                                                    tmp[9] = temp.L9ID;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return tmp;
        }

        private void AddResourceRefresh()
        {
            TitleTextBox.Text = "";
            AddPreviewDataGridView.Rows.Clear();
            for (int i=0;i<temp_resousces.Length;i++)
            {
                AddPreviewDataGridView.Rows.Add(temp_resousces[i].ID, temp_resousces[i].title, temp_resousces[i].quantity, temp_resousces[i].units, temp_resousces[i].supplier, temp_resousces[i].invoice, temp_resousces[i].supplyDate);

            }
        }

        private int GetLastResourceID()
        {
            int ID = 0;

            StockDBCommand = new OleDbCommand("SELECT ID FROM Resources ORDER BY ID DESC", StockDBConnection);


            reader = StockDBCommand.ExecuteReader();
            reader.Read();
            ID = Convert.ToInt32(reader[0]);


            return ID;
        }


        //EVENTS********************************************************************

        private void CloseAddResourceButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void AddResourceForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                StockDBConnection = new OleDbConnection(SetProvider("Stock.accdb"));
                StockDBConnection.Open();
                L0_ComboBox.Items.Clear();
                L0_ComboBox.Text = "--";
                L0_ComboBox.Items.AddRange(GetGroups(0));
                SupplierComboBox.Text = "--";
                SupplierComboBox.Items.Clear();
                SupplierComboBox.Items.AddRange(GetSuppliers());
                UnitsComboBox.SelectedIndex = 0;
                TitleTextBox.Text = "";
                SupplierInvoiceTextBox.Text = "";
                SupplierInvoiceDateMaskedTextBox.Text = "";
                QuantityTextBox.Text = "";


                SupplierInvoiceDateMaskedTextBox.Enabled = true;
                SupplierInvoiceTextBox.Enabled = true;
                SupplierInvoiceDateMaskedTextBox.Enabled = true;
                SupplierComboBox.Enabled = true;
            }
            else
            {
                Array.Resize(ref temp_resousces, 0);
                AddPreviewDataGridView.Rows.Clear();
                StockDBConnection.Close();
            }
        }
        private void L0_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            L1_ComboBox.Items.Clear();
            L1_ComboBox.Items.AddRange(GetGroups(1));
        }

        private void L1_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            L2_ComboBox.Items.Clear();
            L2_ComboBox.Items.AddRange(GetGroups(2));
        }

        private void L2_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            L3_ComboBox.Items.Clear();
            L3_ComboBox.Items.AddRange(GetGroups(3));
        }

        private void L3_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            L4_ComboBox.Items.Clear();
            L4_ComboBox.Items.AddRange(GetGroups(4));
        }

        private void L4_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            L5_ComboBox.Items.Clear();
            L5_ComboBox.Items.AddRange(GetGroups(5));
        }

        private void L5_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            L6_ComboBox.Items.Clear();
            L6_ComboBox.Items.AddRange(GetGroups(6));
        }

        private void L6_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            L7_ComboBox.Items.Clear();
            L7_ComboBox.Items.AddRange(GetGroups(7));
        }

        private void L7_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            L8_ComboBox.Items.Clear();
            L8_ComboBox.Items.AddRange(GetGroups(8));
        }

        private void L8_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            L9_ComboBox.Items.Clear();
            L9_ComboBox.Items.AddRange(GetGroups(9));
        }

        private void L9_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
  
        }

        private void L0_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L0_ComboBox.Text = "--";
            L1_ComboBox.Text = "--";
            L2_ComboBox.Text = "--";
            L3_ComboBox.Text = "--";
            L4_ComboBox.Text = "--";
            L5_ComboBox.Text = "--";
            L6_ComboBox.Text = "--";
            L7_ComboBox.Text = "--";
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L1_ComboBox.Items.Clear();
            L2_ComboBox.Items.Clear();
            L3_ComboBox.Items.Clear();
            L4_ComboBox.Items.Clear();
            L5_ComboBox.Items.Clear();
            L6_ComboBox.Items.Clear();
            L7_ComboBox.Items.Clear();
            L8_ComboBox.Items.Clear();
            L9_ComboBox.Items.Clear();
        }

        private void L1_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L1_ComboBox.Text = "--";
            L2_ComboBox.Text = "--";
            L3_ComboBox.Text = "--";
            L4_ComboBox.Text = "--";
            L5_ComboBox.Text = "--";
            L6_ComboBox.Text = "--";
            L7_ComboBox.Text = "--";
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L2_ComboBox.Items.Clear();
            L3_ComboBox.Items.Clear();
            L4_ComboBox.Items.Clear();
            L5_ComboBox.Items.Clear();
            L6_ComboBox.Items.Clear();
            L7_ComboBox.Items.Clear();
            L8_ComboBox.Items.Clear();
            L9_ComboBox.Items.Clear();
        }

        private void L2_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L2_ComboBox.Text = "--";
            L3_ComboBox.Text = "--";
            L4_ComboBox.Text = "--";
            L5_ComboBox.Text = "--";
            L6_ComboBox.Text = "--";
            L7_ComboBox.Text = "--";
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L3_ComboBox.Items.Clear();
            L4_ComboBox.Items.Clear();
            L5_ComboBox.Items.Clear();
            L6_ComboBox.Items.Clear();
            L7_ComboBox.Items.Clear();
            L8_ComboBox.Items.Clear();
            L9_ComboBox.Items.Clear();
        }

        private void L3_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L3_ComboBox.Text = "--";
            L4_ComboBox.Text = "--";
            L5_ComboBox.Text = "--";
            L6_ComboBox.Text = "--";
            L7_ComboBox.Text = "--";
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L4_ComboBox.Items.Clear();
            L5_ComboBox.Items.Clear();
            L6_ComboBox.Items.Clear();
            L7_ComboBox.Items.Clear();
            L8_ComboBox.Items.Clear();
            L9_ComboBox.Items.Clear();
        }

        private void L4_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L4_ComboBox.Text = "--";
            L5_ComboBox.Text = "--";
            L6_ComboBox.Text = "--";
            L7_ComboBox.Text = "--";
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L5_ComboBox.Items.Clear();
            L6_ComboBox.Items.Clear();
            L7_ComboBox.Items.Clear();
            L8_ComboBox.Items.Clear();
            L9_ComboBox.Items.Clear();
        }

        private void L5_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L5_ComboBox.Text = "--";
            L6_ComboBox.Text = "--";
            L7_ComboBox.Text = "--";
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L6_ComboBox.Items.Clear();
            L7_ComboBox.Items.Clear();
            L8_ComboBox.Items.Clear();
            L9_ComboBox.Items.Clear();
        }

        private void L6_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L6_ComboBox.Text = "--";
            L7_ComboBox.Text = "--";
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L7_ComboBox.Items.Clear();
            L8_ComboBox.Items.Clear();
            L9_ComboBox.Items.Clear();
        }

        private void L7_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L7_ComboBox.Text = "--";
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L8_ComboBox.Items.Clear();
            L9_ComboBox.Items.Clear();
        }

        private void L8_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L9_ComboBox.Items.Clear();
        }

        private void L9_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L9_ComboBox.Text = "--";
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if(L0_ComboBox.Text!="--")
            {
                if (TitleTextBox.Text == "" || QuantityTextBox.Text == "" || UnitsComboBox.Text == "" || SupplierComboBox.Text == "" || SupplierInvoiceDateMaskedTextBox.Text == "")
                {
                    MessageBox.Show("Деякі поля пусті");
                }
                else
                {
                    int ID = 0;
                    if (AddPreviewDataGridView.Rows.Count == 1)
                    {
                        ID = GetLastResourceID() + 1;
                    }
                    else
                    {
                        ID = Convert.ToInt32(AddPreviewDataGridView.Rows[AddPreviewDataGridView.Rows.Count - 2].Cells[0].Value) + 1;
                    }

                    string title = TitleTextBox.Text;
                    int quantity = Convert.ToInt32(QuantityTextBox.Text);
                    string units = UnitsComboBox.Text;
                    string supplier = SupplierComboBox.Text;
                    string invoice = SupplierInvoiceTextBox.Text;
                    string supplyDate = SupplierInvoiceDateMaskedTextBox.Text;
                    int[] cuurentGroup = GetCurrentGroup();
                    Array.Resize(ref temp_resousces, temp_resousces.Length + 1);

                    temp_resousces[temp_resousces.Length - 1] = new Resource(ID, title, quantity, units, supplier, invoice, supplyDate, cuurentGroup[0], cuurentGroup[1], cuurentGroup[2], cuurentGroup[3],
                        cuurentGroup[4], cuurentGroup[5], cuurentGroup[6], cuurentGroup[7], cuurentGroup[8], cuurentGroup[9]);
                    AddResourceRefresh();

                    AddPreviewDataGridView.ClearSelection();

                    SupplierInvoiceDateMaskedTextBox.Enabled = false;
                    SupplierInvoiceTextBox.Enabled = false;
                    SupplierInvoiceDateMaskedTextBox.Enabled = false;
                    SupplierComboBox.Enabled = false;
                    QuantityTextBox.Text = "";
                }
            }
            else
            {
                MessageBox.Show("No group");
            }
    }


        private void SupplierInvoiceDateMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if(!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void QuantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void UnitsComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void SupplierComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if(temp_resousces.Length!=0)
            {
                Resource tmp;
                for (int i = 0; i < temp_resousces.Length; i++)
                {
                    tmp = temp_resousces[i];
                    StockDBCommand = new OleDbCommand("INSERT INTO Resources (title, quantity, units, supplier, invoice, supplyDate, " +
                        "L0ID, L1ID, L2ID, L3ID, L4ID, L5ID, L6ID, L7ID, L8ID, L9ID)" +
                        " VALUES ('" + tmp.title + "', '" + tmp.quantity + "', '" + tmp.units + "', '" + tmp.supplier + "', '" + tmp.invoice + "', '" + tmp.supplyDate + "', '" +
                        tmp.L0ID + "', '" + tmp.L1ID + "', '" + tmp.L2ID + "', '" + tmp.L3ID + "', '" + tmp.L4ID + "', '" + tmp.L5ID + "', '" + tmp.L6ID + "', '" + tmp.L7ID + "', '" + tmp.L8ID + "', '" + tmp.L9ID + "')", StockDBConnection);
                    StockDBCommand.ExecuteNonQuery();
                }
                AddPreviewDataGridView.Rows.Clear();
                TitleTextBox.Text = "";
                SupplierComboBox.SelectedIndex = -1;
                SupplierComboBox.Text = "--";
                SupplierInvoiceTextBox.Text = "";
                SupplierInvoiceDateMaskedTextBox.Text = "";
                QuantityTextBox.Text = "";
                UnitsComboBox.SelectedIndex = 0;

                L0_ComboBox.Items.Clear();
                L0_ComboBox.Text = "--";
                L1_ComboBox.Items.Clear();
                L1_ComboBox.Text = "--";
                L2_ComboBox.Items.Clear();
                L2_ComboBox.Text = "--";
                L3_ComboBox.Items.Clear();
                L3_ComboBox.Text = "--";
                L4_ComboBox.Items.Clear();
                L4_ComboBox.Text = "--";
                L5_ComboBox.Items.Clear();
                L5_ComboBox.Text = "--";
                L6_ComboBox.Items.Clear();
                L6_ComboBox.Text = "--";
                L7_ComboBox.Items.Clear();
                L7_ComboBox.Text = "--";
                L8_ComboBox.Items.Clear();
                L8_ComboBox.Text = "--";
                L9_ComboBox.Items.Clear();
                L9_ComboBox.Text = "--";

                SaveButton.Enabled = false;
                this.Visible = false;
            }
            
        }

        private void AddPreviewDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            SaveButton.Enabled = true;
        }

        private void AddPreviewDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if(AddPreviewDataGridView.RowCount < 2)
            {
                SaveButton.Enabled = false;
            }
        }

        private void SupplierInvoiceDateMaskedTextBox_MouseEnter(object sender, EventArgs e)
        {
            SupplierInvoiceDateMaskedTextBox.Text = DateTime.Now.ToShortDateString();
        }
    }
}

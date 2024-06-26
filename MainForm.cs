﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml.XPath;
using System.Globalization;
using System.IO;
using OfficeOpenXml;
using System.Runtime;
using System.Runtime.InteropServices;

namespace EnergyService
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

        }

        OleDbConnection PersonsDBConnection;
        OleDbCommand PersonsDBCommand;
        OleDbDataReader PersonsReader;


        private void MainForm_Load(object sender, EventArgs e)
        {
            currentUser = new User(-1, "", "", "UNREGISTERED");

            this.LoginDBConnection = new OleDbConnection(SetProvider("Users.accdb"));
            LoginDBConnection.Open();

            this.PersonsDBConnection = new OleDbConnection(SetProvider("Persons.accdb"));
            PersonsDBConnection.Open();

            this.StockDBConnection = new OleDbConnection(SetProvider("Stock.accdb"));
            StockDBConnection.Open();
            L0_ComboBox.Items.AddRange(GetGroups(0));
            WriteOffEquipmentComboBox.Items.AddRange(GetFacilities());
            WriteOffRecipientPersonComboBox.Items.AddRange(GetPersons("Worker"));
            WriteOffRecipientPersonComboBox.Items.AddRange(GetPersons("Engineer"));
            WriteOffRecipientPersonComboBox.Items.AddRange(GetPersons("Manager"));
            WriteOffSenderPersonComboBox.Items.AddRange(GetPersons("Engineer"));
            WriteOffSenderPersonComboBox.Items.AddRange(GetPersons("Manager"));
            GetResources(new Group("", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0), 0);
            UpdatePath();


            maintenanceYearComboBox.Text = DateTime.Now.ToString("yyyy", new CultureInfo("en-US"));
            maintenanceMonthComboBox.Text = DateTime.Now.ToString("MMMM", new CultureInfo("en-US"));
            this.MaintenanceDBConnection = new OleDbConnection(SetProvider("Maintenance.accdb"));
            MaintenanceDBConnection.Open();
            maintenanceExecutorComboBox.Items.AddRange(GetPersons("Worker"));
            maintenanceExecutorComboBox.Items.AddRange(GetPersons("Engineer"));


            plannedExpensesYearComboBox.Text = DateTime.Now.ToString("yyyy", new CultureInfo("en-US"));
            plannedExpensesMonthComboBox.Text = DateTime.Now.ToString("MMMM", new CultureInfo("en-US"));
            this.PlannedExpensesDBConnection = new OleDbConnection(SetProvider("PlannedExpenses.accdb"));
            PlannedExpensesDBConnection.Open();


            consumptionYearComboBox.Text = DateTime.Now.ToString("yyyy", new CultureInfo("en-US"));
            this.ConsumptionDBConnection = new OleDbConnection(SetProvider("Consumption.accdb"));
            ConsumptionDBConnection.Open();



            searchWorkTimeMonthComboBox.Text = DateTime.Now.ToString("MMMM", new CultureInfo("en-US"));
            searchWorkTimeYearComboBox.Text = DateTime.Now.ToString("yyyy", new CultureInfo("en-US"));
            this.WorkTimeDBConnection = new OleDbConnection(SetProvider("WorkTime.accdb"));
            WorkTimeDBConnection.Open();
            addWorkTimePersonComboBox.Items.AddRange(GetPersons("Worker"));
            addWorkTimePersonComboBox.Items.AddRange(GetPersons("Engineer"));
            searchWorkTimePersonComboBox.Items.AddRange(GetPersons("Worker"));
            searchWorkTimePersonComboBox.Items.AddRange(GetPersons("Engineer"));
            addWorkTimeMultiplierComboBox.SelectedIndex = 0;
            searchWorkTimePersonComboBox.SelectedIndex = 0;
            addWorkTimePersonComboBox.SelectedIndex = 0;
            workShiftComboBox.SelectedIndex = 0;

            this.ConstantsDBConnection= new OleDbConnection(SetProvider("Constants.accdb"));
            dayTypeComboBox.Text = GetConstant("dayType");
            if (dayTypeComboBox.Text=="Wrk")
            {
                rateTextBox.Text = GetConstant("rate");
            }
            if (dayTypeComboBox.Text == "Vac")
            {
                rateTextBox.Text = GetConstant("vacRate");
            }

            this.ContractDBConnection = new OleDbConnection(SetProvider("Contracts.accdb"));
            ContractDBConnection.Open();

            StockDBConnection.Close();
            MaintenanceDBConnection.Close();
            PlannedExpensesDBConnection.Close();
            ConsumptionDBConnection.Close();
            WorkTimeDBConnection.Close();
            ContractDBConnection.Close();

            SetEventsHandlers();

            mainTabControl.SelectedIndex = 0;

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;

            MaintenanceDBConnection.Close();
            StockDBConnection.Close();
            PersonsDBConnection.Close();
            LoginDBConnection.Close();
            PlannedExpensesDBConnection.Close();
            ConsumptionDBConnection.Close();
            WorkTimeDBConnection.Close();

            Copy(sourceDirectory, targetDirectory);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            centerLoadingImage();
        }

        private void loadDelayTimer_Tick(object sender, EventArgs e)
        {
            //loadDelayTimer.Enabled = false;
        }

        private void centerLoadingImage()
        {
            //loadingPictureBox.Location = new Point(this.Size.Width / 2 - 50, this.Height / 2 - 50);
        }

        private void hidePanels()
        {
            StockPanel.Visible = false;
            maintenancePanel.Visible = false;
            plannedExpensesPanel.Visible = false;
            consumptionPanel.Visible = false;
            workTimePanel.Visible = false;
        }

        private void SetEventsHandlers()
        {
            this.plannedExpensesYearComboBox.SelectedIndexChanged += new System.EventHandler(this.plannedExpensesYearComboBox_SelectedIndexChanged);
            this.plannedExpensesYearComboBox.TextChanged += new System.EventHandler(this.plannedExpensesYearComboBox_TextChanged);
            this.plannedExpensesMonthComboBox.SelectedIndexChanged += new System.EventHandler(this.plannedExpensesMonthComboBox_SelectedIndexChanged);
            this.plannedExpensesMonthComboBox.TextChanged += new System.EventHandler(this.plannedExpensesMonthComboBox_TextChanged);

            this.maintenanceYearComboBox.SelectedIndexChanged += new System.EventHandler(this.maintenanceYearComboBox_SelectedIndexChanged);
            this.maintenanceYearComboBox.TextChanged += new System.EventHandler(this.maintenanceYearComboBox_TextChanged);
            this.maintenanceMonthComboBox.SelectedIndexChanged += new System.EventHandler(this.maintenanceMonthComboBox_SelectedIndexChanged);
            this.maintenanceMonthComboBox.TextChanged += new System.EventHandler(this.maintenanceMonthComboBox_TextChanged);

            this.searchWorkTimePersonComboBox.SelectedIndexChanged += new System.EventHandler(this.searchWorkTimePersonComboBox_SelectedIndexChanged);
            this.searchWorkTimeMonthComboBox.SelectedIndexChanged += new System.EventHandler(this.searchWorkTimeMonthComboBox_SelectedIndexChanged);
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            hidePanels();
            if (mainTabControl.SelectedIndex == 0)
            {
                StockDBConnection.Close();
                MaintenanceDBConnection.Close();
                PlannedExpensesDBConnection.Close();
                ConsumptionDBConnection.Close();
                WorkTimeDBConnection.Close();

                Cursor.Current = Cursors.Default;
            }
            if (mainTabControl.SelectedIndex == 1)
            {
                StockDBConnection.Open();
                MaintenanceDBConnection.Close();
                PlannedExpensesDBConnection.Close();
                ConsumptionDBConnection.Close();
                WorkTimeDBConnection.Close();

                StockPanel.Visible = true;
                Cursor.Current = Cursors.Default;
            }
            if (mainTabControl.SelectedIndex == 2)
            {
                StockDBConnection.Close();
                MaintenanceDBConnection.Open();
                PlannedExpensesDBConnection.Close();
                ConsumptionDBConnection.Close();
                WorkTimeDBConnection.Close();

                GetMaintenances(PrepareSearchCommand());
                maintenancePanel.Visible = true;
                Cursor.Current = Cursors.Default;
            }
            if (mainTabControl.SelectedIndex==3)
            {
                StockDBConnection.Close();
                MaintenanceDBConnection.Close();
                PlannedExpensesDBConnection.Open();
                ConsumptionDBConnection.Close();
                WorkTimeDBConnection.Close();

                GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
                plannedExpensesPanel.Visible = true;
                Cursor.Current = Cursors.Default;
            }
            if (mainTabControl.SelectedIndex == 4)
            {
                StockDBConnection.Close();
                MaintenanceDBConnection.Close();
                PlannedExpensesDBConnection.Close();
                ConsumptionDBConnection.Open();
                WorkTimeDBConnection.Close();

                SetConsumptionsLabels();
                RedrawChart(GetConsumptionData(GetCommand()));
                consumptionPanel.Visible = true;
                Cursor.Current = Cursors.Default;
            }
            if (mainTabControl.SelectedIndex == 5)
            {
                StockDBConnection.Close();
                MaintenanceDBConnection.Close();
                PlannedExpensesDBConnection.Close();
                ConsumptionDBConnection.Close();
                WorkTimeDBConnection.Open();

                GetWorkTime();
                workTimePanel.Visible = true;
                Cursor.Current = Cursors.Default;
            }
            if (mainTabControl.SelectedIndex == 6)
            {
                StockDBConnection.Close();
                MaintenanceDBConnection.Close();
                PlannedExpensesDBConnection.Close();
                ConsumptionDBConnection.Close();
                WorkTimeDBConnection.Close();

                Cursor.Current = Cursors.Default;
            }
            if (mainTabControl.SelectedIndex == 7)
            {
                StockDBConnection.Close();
                MaintenanceDBConnection.Close();
                PlannedExpensesDBConnection.Close();
                ConsumptionDBConnection.Close();
                WorkTimeDBConnection.Close();

                Cursor.Current = Cursors.Default;
            }
        }

        private string GetConstant(string name)
        {
            ConstantsDBConnection.Open();
            string value="";
            string command = "SELECT constValue FROM Constants WHERE constName='" + name + "'";
            ConstantsDBCommand = new OleDbCommand(command, ConstantsDBConnection);
            this.ConstantsDBReader = ConstantsDBCommand.ExecuteReader();
            while (ConstantsDBReader.Read())
            {
                value = ConstantsDBReader[0].ToString();
            }
            ConstantsDBConnection.Close();
            return value;
        }

        private void SetConstant(string name, string value)
        {
            ConstantsDBConnection.Open();
            ConstantsDBCommand = new OleDbCommand("UPDATE Constants SET constValue='" + value + "' WHERE constName='" + name+"'", ConstantsDBConnection);
            ConstantsDBCommand.ExecuteNonQuery();
            ConstantsDBConnection.Close();
        }



        //LOGIN**************************************************************************************************************************************************************************/
        OleDbConnection LoginDBConnection;
        OleDbCommand LoginDBCommand;
        OleDbDataReader LoginReader;
        User currentUser;
        public bool loggedIn = false;



        //CLASSES/////////////////////////////////////////////////

        private class User
        {
            int ID;
            string login;
            string password;
            public string type { get; }

            public User(int ID, string login, string password, string type)
            {
                this.ID = ID;
                this.login = login;
                this.password = password;
                this.type = type;
            }


        }


        //FUNCTIONS////////////////////////////////////////////////

        private User GetUser(string login, string password)
        {
            User tmp = new User(-1, "", "", "UNREGISTERED");

            LoginDBCommand = new OleDbCommand("SELECT * FROM Users WHERE login=" + "'" + login + "'" + " AND password=" + "'" + password+ "'", LoginDBConnection);

            this.LoginReader = LoginDBCommand.ExecuteReader();
            if(LoginReader.Read())
            {
                tmp = new User(Convert.ToInt32(LoginReader[0]), LoginReader[1].ToString(), LoginReader[2].ToString(), LoginReader[3].ToString());
            }
            return tmp;
        }

        private void SetUserPermissions(string userType)
        {
            if(userType=="UNREGISTERED")
            {
                StockPanel.Enabled = false;
                maintenancePanel.Enabled = false;
            }
            if (userType == "admin")
            {
                StockPanel.Enabled = true;
                maintenancePanel.Enabled = true;

            }
            if (userType == "user")
            {
                StockPanel.Enabled = true;
                maintenancePanel.Enabled = true;

                AddResourcesButton.Enabled = false;
                removeResourcePanel.Enabled = false;
                maintenanceCreatePanel.Enabled = false;
                createActButton.Enabled = false;
                maintenanceAddNotePanel.Enabled = false;
            }
        }

        //EVENTS//////////////////////////////////////////////////
        private void LoginButton_Click(object sender, EventArgs e)
        {
            currentUser = GetUser(LoginTextBox.Text, PasswordMaskedTextBox.Text);
            if (currentUser.type == "UNREGISTERED")
            {
                MessageBox.Show("Not Registered user");
            }
            else
            {
                this.Text = "EnergyService/" + currentUser.type;
                LoginButton.Enabled = false;
                LogOffButton.Enabled = true;
                LoginTextBox.Enabled = false;
                LoginTextBox.Text = "";
                PasswordMaskedTextBox.Enabled = false;
                PasswordMaskedTextBox.Text = "";
                loggedIn = true;
                MessageBox.Show("Entered as " + currentUser.type);
                mainTabControl.SelectedIndex = 1;
            }

        }

        private void LogOffButton_Click(object sender, EventArgs e)
        {
            currentUser = new User(-1, "", "", "UNREGISTERED");
            LoginButton.Enabled = true;
            LogOffButton.Enabled = false;
            LoginTextBox.Enabled = true;
            PasswordMaskedTextBox.Enabled = true;
            this.Text = "EnergyService" + currentUser.type;
            loggedIn = false;

        }
        //EVENTS//////////////////////////////////////////////////



        //LOGIN**************************************************************************************************************************************************************************/






        //STOCK**************************************************************************************************************************************************************************/
        OleDbConnection StockDBConnection;
        OleDbCommand StockDBCommand;
        OleDbDataReader stockReader;
        public AddResourceForm addResourceForm = new AddResourceForm();
        public ViewRemovedForm viewRemovedForm = new ViewRemovedForm();
        public bool search = false;
       


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
        public class OffResource
        {
            public int ID;
            public string title;
            public int quantity;
            public string units;
            public string supplier;
            public string invoice;
            public string supplyDate;

            public string equipment;
            public string senderPerson;
            public string recipientPerson;
            public string offDate;

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

            public OffResource(int ID, string title, int quantity, string units, string supplier,
                string invoice, string supplyDate, string equipment, string senderPerson, string offDate, int L0ID, int L1ID, int L2ID, int L3ID, int L4ID, int L5ID,
                int L6ID, int L7ID, int L8ID, int L9ID, string recipientPerson)
            {
                this.ID = ID;
                this.title = title;
                this.quantity = quantity;
                this.units = units;
                this.supplier = supplier;
                this.invoice = invoice;
                this.supplyDate = supplyDate;

                this.equipment = equipment;
                this.senderPerson = senderPerson;
                this.recipientPerson = recipientPerson;
                this.offDate = offDate;

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
            public string division;
            public string status;
            public Person(string name, string position, string division, string status)
            {
                this.name = name;
                this.position = position;
                this.division = division;
                this.status = status;
            }
            public override string ToString()
            {
                return name;
            }
            public string GetPosition()
            {
                return position;
            }
        }


        //FUNCTIONS////////////////////////////////////////////////
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
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

                this.stockReader = StockDBCommand.ExecuteReader();
                while (stockReader.Read())
                {
                    tmp[i] = new Group(stockReader[0].ToString(), Convert.ToInt32(stockReader[1]), Convert.ToInt32(stockReader[2]), Convert.ToInt32(stockReader[3]),
                        Convert.ToInt32(stockReader[4]), Convert.ToInt32(stockReader[5]), Convert.ToInt32(stockReader[6]), Convert.ToInt32(stockReader[7]),
                        Convert.ToInt32(stockReader[8]), Convert.ToInt32(stockReader[9]), Convert.ToInt32(stockReader[10]));
                    i++;
                }
            }

            tmp = tmp.Where(temp => temp != null).ToArray();

            return tmp;
        }
        public void GetResources(Group group, int mode)
        {
            Cursor.Current = Cursors.AppStarting;
            int[] groupID = new int[10];
            groupID[0] = group.L0ID;
            groupID[1] = group.L1ID;
            groupID[2] = group.L2ID;
            groupID[3] = group.L3ID;
            groupID[4] = group.L4ID;
            groupID[5] = group.L5ID;
            groupID[6] = group.L6ID;
            groupID[7] = group.L7ID;
            groupID[8] = group.L8ID;
            groupID[9] = group.L9ID;

            string[] result = new string[1000];
            if (mode == 0)
            {
                StockDBCommand = new OleDbCommand("SELECT * FROM Resources ORDER BY title ASC", StockDBConnection);
            }
            else
            {
                StockDBCommand = new OleDbCommand("SELECT * FROM Resources WHERE L0ID=" + groupID[0] + " AND L1ID=" + groupID[1] + " AND L2ID=" + groupID[2] + " AND L3ID=" + groupID[3] +
                    " AND L4ID=" + groupID[4] + " AND L5ID=" + groupID[5] + " AND L6ID=" + groupID[6] + " AND L7ID=" + groupID[7] + " AND L8ID=" + groupID[8] + " AND L9ID=" + groupID[9] + " ORDER BY title ASC", StockDBConnection);
            }


            string[,] tmp = new string[1000, 30];
            int i = 0;

            this.stockReader = StockDBCommand.ExecuteReader();
            while (stockReader.Read())
            {
                tmp[i, 0] = stockReader[0].ToString();
                tmp[i, 1] = stockReader[1].ToString();
                tmp[i, 2] = stockReader[2].ToString();
                tmp[i, 3] = stockReader[3].ToString();
                tmp[i, 4] = stockReader[4].ToString();
                tmp[i, 5] = stockReader[5].ToString();
                tmp[i, 6] = stockReader[6].ToString();
                string[] d = tmp[i, 6].Split(' ');
                tmp[i, 6] = d[0];
                tmp[i, 7] = stockReader[7].ToString();
                tmp[i, 8] = stockReader[8].ToString();
                tmp[i, 9] = stockReader[9].ToString();
                tmp[i, 10] = stockReader[10].ToString();
                tmp[i, 11] = stockReader[11].ToString();
                tmp[i, 12] = stockReader[12].ToString();
                tmp[i, 13] = stockReader[13].ToString();
                tmp[i, 14] = stockReader[14].ToString();
                tmp[i, 15] = stockReader[15].ToString();
                tmp[i, 16] = stockReader[16].ToString();
                tmp[i, 17] = stockReader[17].ToString();
                tmp[i, 18] = stockReader[18].ToString();
                tmp[i, 19] = stockReader[19].ToString();
                tmp[i, 20] = stockReader[20].ToString();
                tmp[i, 21] = stockReader[21].ToString();
                tmp[i, 22] = stockReader[22].ToString();
                tmp[i, 23] = stockReader[23].ToString();
                tmp[i, 24] = stockReader[24].ToString();
                tmp[i, 25] = stockReader[25].ToString();
                tmp[i, 26] = stockReader[26].ToString();
                tmp[i, 27] = stockReader[27].ToString();
                tmp[i, 28] = stockReader[28].ToString();
                tmp[i, 29] = stockReader[29].ToString();

                i++;
            }

            string[] temp = new string[1000];
            for (int y = 0; y < temp.Length; y++)
            {
                if (tmp[y, 0] != null || tmp[y, 1] != null || tmp[y, 2] != null || tmp[y, 3] != null || tmp[y, 4] != null || tmp[y, 5] != null || tmp[y, 6] != null)
                {
                    string[] t = new string[7];
                    t[0] = tmp[y, 0];
                    t[1] = tmp[y, 1];
                    t[2] = tmp[y, 2];
                    t[3] = tmp[y, 3];
                    t[4] = tmp[y, 4];
                    t[5] = tmp[y, 5];
                    t[6] = tmp[y, 6];
                    temp[y] = string.Join("_", t);
                }
            }
            temp = temp.Where(t => t != null).ToArray();

            CurrentStockDataGridView.Rows.Clear();
            for (int x = 0; x < temp.Length; x++)
            {
                string[] t = new string[7];
                t = temp[x].Split('_');
                CurrentStockDataGridView.Rows.Add(t[0], t[1], t[2], t[3], t[4], t[5], t[6]);
            }
            CurrentStockDataGridView.ClearSelection();
            Cursor.Current = Cursors.Default;

        }
        private void SearchResources(int ID)
        {
            OleDbCommand StockDBCommand = new OleDbCommand("SELECT * FROM Resources WHERE ID = " + ID, StockDBConnection);
            string[] tmp = new string[30];

            this.stockReader = StockDBCommand.ExecuteReader();
            while (stockReader.Read())
            {
                tmp[0] = stockReader[0].ToString();
                tmp[1] = stockReader[1].ToString();
                tmp[2] = stockReader[2].ToString();
                tmp[3] = stockReader[3].ToString();
                tmp[4] = stockReader[4].ToString();
                tmp[5] = stockReader[5].ToString();
                tmp[6] = stockReader[6].ToString();
            }

            CurrentStockDataGridView.Rows.Add(tmp[0], tmp[1], tmp[2], tmp[3], tmp[4], tmp[5], tmp[6]);
        }
        private void UpdatePath()
        {
            PathLabel.Text = "";
            if (L0_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L0_ComboBox.Text + "/";
            }
            if (L1_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L1_ComboBox.Text + "/";
            }
            if (L2_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L2_ComboBox.Text + "/";
            }
            if (L3_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L3_ComboBox.Text + "/";
            }
            if (L4_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L4_ComboBox.Text + "/";
            }
            if (L5_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L5_ComboBox.Text + "/";
            }
            if (L6_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L6_ComboBox.Text + "/";
            }
            if (L7_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L7_ComboBox.Text + "/";
            }
            if (L8_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L8_ComboBox.Text + "/";
            }
            if (L9_ComboBox.SelectedIndex > -1)
            {
                PathLabel.Text += L9_ComboBox.Text + "/";
            }
        }

        private Equipment[] GetFacilities()
        {
            Equipment[] tmp = new Equipment[1000];
            StockDBCommand = new OleDbCommand("SELECT * FROM Equipment", StockDBConnection);

            int i = 0;

            this.stockReader = StockDBCommand.ExecuteReader();
            while (stockReader.Read())
            {
                tmp[i] = new Equipment(stockReader[0].ToString(), stockReader[1].ToString());
                i++;
            }
            tmp = tmp.Where(temp => temp != null).ToArray();

            return tmp;
        }
        private Person[] GetPersons(string status)
        {
            Person[] tmp = new Person[1000];
            string command = "SELECT * FROM Persons";
            if(status != "")
            {
                command += " WHERE status='" + status + "'";
            }
            PersonsDBCommand = new OleDbCommand(command, PersonsDBConnection);

            int i = 0;

            this.PersonsReader = PersonsDBCommand.ExecuteReader();
            while (PersonsReader.Read())
            {
                tmp[i] = new Person(PersonsReader[0].ToString(), PersonsReader[1].ToString(), PersonsReader[2].ToString(), PersonsReader[3].ToString());
                i++;
            }
            tmp = tmp.Where(temp => temp != null).ToArray();

            return tmp;
        }
        private void CheckPreWriteOffData()
        {
            if (WriteOffQuantityTextBox.Text != "" && WriteOffEquipmentComboBox.SelectedIndex != -1 && WriteOffRecipientPersonComboBox.SelectedIndex != -1
                && CurrentStockDataGridView.SelectedRows.Count < 2 && CurrentStockDataGridView.SelectedRows.Count > 0)
            {
                WriteOffResourceButton.Enabled = true;
            }
            else
            {
                WriteOffResourceButton.Enabled = false;
            }
        }

        private int[] GetCurrentGroup(int ID)
        {
            int[] tmp = new int[10];
            StockDBCommand = new OleDbCommand("SELECT * FROM Resources WHERE ID=" + ID, StockDBConnection);

            this.stockReader = StockDBCommand.ExecuteReader();
            stockReader.Read();
            for (int i = 0; i < 10; i++)
            {
                tmp[i] = Convert.ToInt32(stockReader[i + 20]);
            }
            return tmp;
        }

        //EVENTS//////////////////////////////////////////////////

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
            CurrentStockDataGridView.Rows.Clear(); 
            GetResources(new Group("",0,0,0,0,0,0,0,0,0,0),0);

            UpdatePath();
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

            CurrentStockDataGridView.Rows.Clear();
            if (L0_ComboBox.Text != "--") GetResources((Group)L0_ComboBox.SelectedItem, 1);
            UpdatePath();
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

            CurrentStockDataGridView.Rows.Clear();
            if (L1_ComboBox.Text != "--") GetResources((Group)L1_ComboBox.SelectedItem, 1);
            UpdatePath();
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

            CurrentStockDataGridView.Rows.Clear();
            if (L2_ComboBox.Text != "--") GetResources((Group)L2_ComboBox.SelectedItem, 1);
            UpdatePath();
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

            CurrentStockDataGridView.Rows.Clear();
            if (L3_ComboBox.Text != "--") GetResources((Group)L3_ComboBox.SelectedItem, 1);
            UpdatePath();
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

            CurrentStockDataGridView.Rows.Clear();
            if (L4_ComboBox.Text != "--") GetResources((Group)L4_ComboBox.SelectedItem, 1);
            UpdatePath();
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

            CurrentStockDataGridView.Rows.Clear();
            if (L5_ComboBox.Text != "--") GetResources((Group)L5_ComboBox.SelectedItem, 1);
            UpdatePath();
        }

        private void L7_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L7_ComboBox.Text = "--";
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L8_ComboBox.Items.Clear();
            L9_ComboBox.Items.Clear();

            CurrentStockDataGridView.Rows.Clear();
            if (L6_ComboBox.Text != "--") GetResources((Group)L6_ComboBox.SelectedItem, 1);
            UpdatePath();
        }

        private void L8_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L8_ComboBox.Text = "--";
            L9_ComboBox.Text = "--";

            L9_ComboBox.Items.Clear();

            CurrentStockDataGridView.Rows.Clear();
            if (L7_ComboBox.Text != "--") GetResources((Group)L7_ComboBox.SelectedItem, 1);
            UpdatePath();
        }

        private void L9_ComboBox_TextChanged(object sender, EventArgs e)
        {
            L9_ComboBox.Text = "--";
            CurrentStockDataGridView.Rows.Clear();
            if (L8_ComboBox.Text != "--") GetResources((Group)L8_ComboBox.SelectedItem, 1);
            UpdatePath();
        }

        private void L0_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            L1_ComboBox.Items.Clear();
            L1_ComboBox.Text = "--";
            if (L0_ComboBox.SelectedIndex != -1)
            {
                L1_ComboBox.Items.AddRange(GetGroups(1));
                GetResources((Group)L0_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }

        private void L1_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            L2_ComboBox.Items.Clear();
            L2_ComboBox.Text = "--";
            if (L1_ComboBox.SelectedIndex != -1)
            {
                L2_ComboBox.Items.AddRange(GetGroups(2));
                GetResources((Group)L1_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }

        private void L2_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            L3_ComboBox.Items.Clear();
            L3_ComboBox.Text = "--";
            if (L2_ComboBox.SelectedIndex != -1)
            {
                L3_ComboBox.Items.AddRange(GetGroups(3));
                GetResources((Group)L2_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }

        private void L3_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            L4_ComboBox.Items.Clear();
            L4_ComboBox.Text = "--";
            if (L3_ComboBox.SelectedIndex != -1)
            {
                L4_ComboBox.Items.AddRange(GetGroups(4));
                GetResources((Group)L3_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }

        private void L4_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            L5_ComboBox.Items.Clear();
            L5_ComboBox.Text = "--";
            if (L4_ComboBox.SelectedIndex != -1)
            {
                L5_ComboBox.Items.AddRange(GetGroups(5));
                GetResources((Group)L4_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }

        private void L5_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            L6_ComboBox.Items.Clear();
            L6_ComboBox.Text = "--";
            if (L5_ComboBox.SelectedIndex != -1)
            {
                L6_ComboBox.Items.AddRange(GetGroups(6));
                GetResources((Group)L5_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }

        private void L6_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            L7_ComboBox.Items.Clear();
            L7_ComboBox.Text = "--";
            if (L6_ComboBox.SelectedIndex != -1)
            {
                L7_ComboBox.Items.AddRange(GetGroups(7));
                GetResources((Group)L6_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }

        private void L7_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            L8_ComboBox.Items.Clear();
            L8_ComboBox.Text = "--";
            if (L7_ComboBox.SelectedIndex != -1)
            {
                L8_ComboBox.Items.AddRange(GetGroups(8));
                GetResources((Group)L7_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }

        private void L8_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            L9_ComboBox.Items.Clear();
            L9_ComboBox.Text = "--";
            if (L8_ComboBox.SelectedIndex != -1)
            {
                L9_ComboBox.Items.AddRange(GetGroups(9));
                GetResources((Group)L8_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }

        private void L9_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            search = false;
            if (L9_ComboBox.SelectedIndex != -1)
            {
                GetResources((Group)L9_ComboBox.SelectedItem, 1);
            }
            UpdatePath();
        }




        private void CurrentStockDataGridView_Resize(object sender, EventArgs e)// Add colunms resize
        {

        }

        private void CurrentStockDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            CheckPreWriteOffData();

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            search = true;
            if (SearchByIDTextBox.Text != "")
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

                PathLabel.Text = "Результати пошуку";


                CurrentStockDataGridView.Rows.Clear();
                SearchResources(Convert.ToInt32(SearchByIDTextBox.Text));
                CurrentStockDataGridView.ClearSelection();
            }
            else
            {
                MessageBox.Show("Поле пустое");
            }
        }
        private void AddResourcesButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            addResourceForm.Visible = true;
            StockDBConnection.Close();
            timer1.Enabled = true;


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(addResourceForm.Visible==false)
            {
                this.Visible = true;
                timer1.Enabled = false;
                StockDBConnection.Open();

                L0_ComboBox.SelectedIndex = -1;
                L0_ComboBox.Text = "--";
                L1_ComboBox.SelectedIndex = -1;
                L1_ComboBox.Text = "--";
                L2_ComboBox.SelectedIndex = -1;
                L2_ComboBox.Text = "--";
                L3_ComboBox.SelectedIndex = -1;
                L3_ComboBox.Text = "--";
                L4_ComboBox.SelectedIndex = -1;
                L4_ComboBox.Text = "--";
                L5_ComboBox.SelectedIndex = -1;
                L5_ComboBox.Text = "--";
                L6_ComboBox.SelectedIndex = -1;
                L6_ComboBox.Text = "--";
                L7_ComboBox.SelectedIndex = -1;
                L7_ComboBox.Text = "--";
                L8_ComboBox.SelectedIndex = -1;
                L8_ComboBox.Text = "--";
                L9_ComboBox.SelectedIndex = -1;
                L9_ComboBox.Text = "--";

            }
        }

        private void WriteOffQuantityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void WriteOffQuantityTextBox_TextChanged(object sender, EventArgs e)
        {
            CheckPreWriteOffData();
        }

        private void WriteOffRecipientPersonComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void WriteOffRecipientPersonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckPreWriteOffData();
        }

        private void WriteOffEquipmentComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void WriteOffEquipmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckPreWriteOffData();
        }
        private void WriteOffResourceButton_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(CurrentStockDataGridView.SelectedRows[0].Cells[0].Value);
            int maxQuantity = Convert.ToInt32(CurrentStockDataGridView.SelectedRows[0].Cells[2].Value);
            int writeOffQuantity = Convert.ToInt32(WriteOffQuantityTextBox.Text);
            int[] currentGroupID = GetCurrentGroup(ID);

            if (writeOffQuantity > maxQuantity)
            {
                MessageBox.Show("Out of quantity");
            }
            else
            {
                if(writeOffQuantity == maxQuantity)
                {
                    StockDBCommand = new OleDbCommand("DELETE * FROM Resources WHERE ID=" + ID, StockDBConnection);
                    StockDBCommand.ExecuteNonQuery();
                }
                if(writeOffQuantity < maxQuantity)
                {
                    StockDBCommand = new OleDbCommand("UPDATE Resources SET quantity=" + (maxQuantity - writeOffQuantity) + " WHERE ID=" + ID, StockDBConnection);
                    StockDBCommand.ExecuteNonQuery();
                }

                string title = Convert.ToString(CurrentStockDataGridView.SelectedRows[0].Cells[1].Value);
                int quantity = writeOffQuantity;
                string units = Convert.ToString(CurrentStockDataGridView.SelectedRows[0].Cells[3].Value);
                string supplier = Convert.ToString(CurrentStockDataGridView.SelectedRows[0].Cells[4].Value);
                string invoice = Convert.ToString(CurrentStockDataGridView.SelectedRows[0].Cells[5].Value);
                string supplyDate = Convert.ToString(CurrentStockDataGridView.SelectedRows[0].Cells[6].Value);

                string equipment = WriteOffEquipmentComboBox.Text;
                string recipientPerson = WriteOffRecipientPersonComboBox.Text;
                string senderPerson = WriteOffSenderPersonComboBox.Text;
                string offDate = WriteOffDateMaskedTextBox.Text;

                OffResource offRes = new OffResource(ID, title, quantity, units, supplier, invoice, supplyDate, equipment, senderPerson, offDate, currentGroupID[0], currentGroupID[1],
                    currentGroupID[2], currentGroupID[3], currentGroupID[4], currentGroupID[5], currentGroupID[6], currentGroupID[7], currentGroupID[8], currentGroupID[9], recipientPerson);


                StockDBCommand = new OleDbCommand("INSERT INTO WroteOffResources (ID, title, quantity, units, supplier, invoice, supplyDate," +
                    " equipment, recipientPerson, offDate, L0ID, L1ID, L2ID, L3ID, L4ID, L5ID, L6ID, L7ID, L8ID, L9ID, senderPerson) VALUES('" + offRes.ID + "', '" + offRes.title+ "', '" + offRes.quantity + "', '" + offRes.units + "', '" + offRes.supplier + "', '" + offRes.invoice + "'," +
                    " '" + offRes.supplyDate + "', '" + offRes.equipment + "', '" + offRes.recipientPerson + "', '" + offRes.offDate + "', '" + offRes.L0ID + "', '" + offRes.L1ID + "', '" + offRes.L2ID + "', '" + offRes.L3ID + "', '" + offRes.L4ID + "'," +
                    " '" + offRes.L5ID + "', '" + offRes.L6ID + "', '" + offRes.L7ID + "', '" + offRes.L8ID + "', '" + offRes.L9ID + "', '" + offRes.senderPerson + "')", StockDBConnection);
                StockDBCommand.ExecuteNonQuery();



                Group currentGroup = new Group("", currentGroupID[0], currentGroupID[1], currentGroupID[2], currentGroupID[3], currentGroupID[4], currentGroupID[5], currentGroupID[6],
                    currentGroupID[7], currentGroupID[8], currentGroupID[9]);

                if(search== true)
                {
                    CurrentStockDataGridView.Rows.Clear();
                    SearchResources(Convert.ToInt32(SearchByIDTextBox.Text));
                    CurrentStockDataGridView.ClearSelection();
                }
                else
                {
                    GetResources(currentGroup, 1);
                }

                WriteOffQuantityTextBox.Text = "";

            }
        }
        private void WriteOffDateMaskedTextBox_MouseEnter(object sender, EventArgs e)
        {
            WriteOffDateMaskedTextBox.Text = DateTime.Now.ToShortDateString();
        }

        private void ViewRemovedButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            viewRemovedForm.Visible = true;
            StockDBConnection.Close();
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (viewRemovedForm.Visible == false)
            {
                this.Visible = true;
                timer2.Enabled = false;
                StockDBConnection.Open();
            }
        }
        private void WriteOffSenderPersonComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        private void CurrentStockDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            bool clear = false;
            for (int i = 0; i < CurrentStockDataGridView.SelectedRows.Count; i++)
            {
                if (CurrentStockDataGridView.SelectedRows[i].Index == CurrentStockDataGridView.RowCount - 1)
                {
                    clear = true;
                }
            }

            if (CurrentStockDataGridView.SelectedRows.Count > 1 || clear)
            {
                CurrentStockDataGridView.ClearSelection();
            }
            CheckPreWriteOffData();





        }

        private void SearchByIDTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void CurrentStockDataGridView_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (number == 27)
            {
                CurrentStockDataGridView.ClearSelection();
            }
        }
        //STOCK**************************************************************************************************************************************************************************/




        //MAINTENANCE********************************************************************************************************************************************************************/
        OleDbConnection MaintenanceDBConnection;
        OleDbCommand MaintenanceDBCommand;
        OleDbDataReader maintenanceDBReader;

        //CLASSES/////////////////////////////////////////////////
        public class Maintenance
        {
            public int equipmentCommonID;
            public string equipmentName;
            public string equipmentModelType;
            public string equipmentPowerVoltage;
            public string equipmentGroup;
            public string equipmentID;
            public int equipmentCommissioning;
            public string equipmentFacility;
            public string equipmentStatus;
            public string maintenanceMonth;
            public string doneMark;
            public string maintenanceExecutor;
            public string maintenanceDate;
            public string maintenanceAct;
            public string note;
            public int maintenanceYear;

            public Maintenance(int equipmentCommonID, string equipmentName, string equipmentModelType, string equipmentPowerVoltage, string equipmentGroup, string equipmentID, int equipmentCommissioning,
                string equipmentFacility, string equipmentStatus, string maintenanceMonth, string doneMark, string maintenanceExecutor, string maintenanceDate, string maintenanceAct, string note,
                int maintenanceYear)
            {
                this.equipmentCommonID = equipmentCommonID;
                this.equipmentName = equipmentName;
                this.equipmentModelType = equipmentModelType;
                this.equipmentPowerVoltage = equipmentPowerVoltage;
                this.equipmentGroup = equipmentGroup;
                this.equipmentID = equipmentID;
                this.equipmentCommissioning = equipmentCommissioning;
                this.equipmentFacility = equipmentFacility;
                this.equipmentStatus = equipmentStatus;
                this.maintenanceMonth = maintenanceMonth;
                this.doneMark = doneMark;
                this.maintenanceExecutor = maintenanceExecutor;
                this.maintenanceDate = maintenanceDate;
                this.maintenanceAct = maintenanceAct;
                this.note = note;
                this.maintenanceYear = maintenanceYear;
            }
        }


        //FUNCTIONS////////////////////////////////////////////////
        private string PrepareSearchCommand()
        {
            string tmp = "";

            if(maintenanceMonthComboBox.Text!="--")
            {
                tmp += " maintenanceMonth='" + maintenanceMonthComboBox.Text + "' AND";
            }
            if (maintenanceGroupComboBox.Text != "--")
            {
                tmp += " equipmentGroup='" + maintenanceGroupComboBox.Text + "' AND";
            }
            if (maintenanceDoneMarkComboBox.Text != "--")
            {
                tmp += " doneMark='" + maintenanceDoneMarkComboBox.Text + "' AND";
            }


            if (tmp!="")
            {
                tmp = tmp.Substring(0, tmp.Length - 4);
            }

            return tmp;
        }
        private void GetMaintenances(string searchParameters)
        {
            Cursor.Current = Cursors.AppStarting;
            string searchParameter = "";
                string commandText = "";

                if (searchParameters != "")
                {
                    searchParameter += " WHERE " + searchParameters;
                }

                commandText = "SELECT * FROM MaintenanceSchedule" + searchParameter;

                MaintenanceDBCommand = new OleDbCommand(commandText, MaintenanceDBConnection);

                Maintenance[] tmp = new Maintenance[1000];
                int i = 0;

                this.maintenanceDBReader = MaintenanceDBCommand.ExecuteReader();
                while (maintenanceDBReader.Read())
                {
                    tmp[i] = new Maintenance(Convert.ToInt32(maintenanceDBReader[0]), maintenanceDBReader[1].ToString(), maintenanceDBReader[2].ToString(), maintenanceDBReader[3].ToString(), maintenanceDBReader[4].ToString(), maintenanceDBReader[5].ToString(),
                        Convert.ToInt32(maintenanceDBReader[6]), maintenanceDBReader[7].ToString(), maintenanceDBReader[8].ToString(), maintenanceDBReader[9].ToString(),
                        maintenanceDBReader[10].ToString(), maintenanceDBReader[11].ToString(), maintenanceDBReader[12].ToString(), maintenanceDBReader[13].ToString(),
                        maintenanceDBReader[14].ToString(), Convert.ToInt32(maintenanceDBReader[15]));
                    i++;
                }

                tmp = tmp.Where(temp => temp != null).ToArray();
                maintenanceDataGridView.Rows.Clear();
                if (tmp.Length > 0)
                {
                    for (int x = 0; x < tmp.Length; x++)
                    {
                        maintenanceDataGridView.Rows.Add(tmp[x].equipmentCommonID.ToString(), tmp[x].equipmentName, tmp[x].equipmentModelType, tmp[x].equipmentPowerVoltage, tmp[x].equipmentGroup, tmp[x].equipmentID,
                            tmp[x].equipmentCommissioning.ToString(), tmp[x].equipmentFacility, tmp[x].equipmentStatus, tmp[x].maintenanceMonth, tmp[x].doneMark, tmp[x].maintenanceExecutor,
                            tmp[x].maintenanceDate, tmp[x].maintenanceAct, tmp[x].note);
                    }


                    int currDate = Convert.ToInt32(DateTime.Now.ToString("MM"));
                    int maintenanceDate = -1;

                    for (int x = 0; x < tmp.Length; x++)
                    {
                        switch(maintenanceDataGridView.Rows[x].Cells[9].Value.ToString())
                        {
                            case "January": maintenanceDate = 1; break;
                            case "February": maintenanceDate = 2; break;
                            case "March": maintenanceDate = 3; break;
                            case "April": maintenanceDate = 4; break;
                            case "May": maintenanceDate = 5; break;
                            case "June": maintenanceDate = 6; break;
                            case "July": maintenanceDate = 7; break;
                            case "August": maintenanceDate = 8; break;
                            case "September": maintenanceDate = 9; break;
                            case "October": maintenanceDate = 10; break;
                            case "November": maintenanceDate = 11; break;
                            case "December": maintenanceDate = 12; break;
                            default: break;
                        }
                        if(maintenanceDate!=-1)
                        {
                            if(currDate < maintenanceDate)
                            {
                                if(maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "False" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "False")
                                {
                                    Color clr = Color.LightGray;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "False" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "True")
                                {
                                    Color clr = Color.Red;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "True" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "False")
                                {
                                    Color clr = Color.Red;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "True" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "True")
                                {
                                    Color clr = Color.Red;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }

                            }
                            if (currDate == maintenanceDate)
                            {
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "False" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "False")
                                {
                                    Color clr = Color.LightYellow;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "False" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "True")
                                {
                                    Color clr = Color.Red;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "True" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "False")
                                {
                                    Color clr = Color.Orange;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "True" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "True")
                                {
                                    Color clr = Color.LightGreen;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                            }
                            if (currDate > maintenanceDate)
                            {
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "False" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "False")
                                {
                                    Color clr = Color.Red;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "False" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "True")
                                {
                                    Color clr = Color.Red;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "True" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "False")
                                {
                                    Color clr = Color.Red;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                                if (maintenanceDataGridView.Rows[x].Cells[10].Value.ToString() == "True" && maintenanceDataGridView.Rows[x].Cells[13].Value.ToString() == "True")
                                {
                                    Color clr = Color.LightGreen;
                                    maintenanceDataGridView.Rows[x].Cells[10].Style.BackColor = clr;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Something is wrong with maintenance month in database");
                        }
                    }
                }
                maintenanceDataGridView.ClearSelection();
            maintenanceDataGridView.ClearSelection();

            maintenanceAddNotePanel.Enabled = false;
            maintenanceNoteTextBox.Text = "";
            //maintenanceDoneButton.Enabled = false;
            Cursor.Current = Cursors.Default;
        }

        //EVENTS//////////////////////////////////////////////////
        private void maintenanceYearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            maintenanceYearComboBox.BackColor = Color.LightBlue;
            GetMaintenances(PrepareSearchCommand());
        }
        private void maintenanceMonthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            maintenanceMonthComboBox.BackColor = Color.LightBlue;
            GetMaintenances(PrepareSearchCommand());
        }
        private void maintenanceGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            maintenanceGroupComboBox.BackColor = Color.LightBlue;
            GetMaintenances(PrepareSearchCommand());
        }
        private void maintenanceDoneMarkComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            maintenanceDoneMarkComboBox.BackColor = Color.LightBlue;
            GetMaintenances(PrepareSearchCommand());
        }

        private void maintenanceYearComboBox_TextChanged(object sender, EventArgs e)
        {
            maintenanceYearComboBox.BackColor = Color.White;
            maintenanceYearComboBox.Text = "--";
            GetMaintenances(PrepareSearchCommand());
        }
        private void maintenanceMonthComboBox_TextChanged(object sender, EventArgs e)
        {
            maintenanceMonthComboBox.BackColor = Color.White;
            maintenanceMonthComboBox.Text = "--";
            GetMaintenances(PrepareSearchCommand());
        }
        private void maintenanceGroupComboBox_TextChanged(object sender, EventArgs e)
        {
            maintenanceGroupComboBox.BackColor = Color.White;
            maintenanceGroupComboBox.Text = "--";
            GetMaintenances(PrepareSearchCommand());
        }
        private void maintenanceDoneMarkComboBox_TextChanged(object sender, EventArgs e)
        {
            maintenanceDoneMarkComboBox.BackColor = Color.White;
            maintenanceDoneMarkComboBox.Text = "--";
            GetMaintenances(PrepareSearchCommand());
        }



        private void maintenanceNoteButton_Click(object sender, EventArgs e)
        {
            string maintenanceNote = maintenanceNoteTextBox.Text;
            string equipmentID = maintenanceDataGridView.SelectedRows[0].Cells[5].Value.ToString();

            MaintenanceDBCommand = new OleDbCommand("UPDATE MaintenanceSchedule SET maintenanceNote='" +
                maintenanceNote + "' WHERE equipmentID='" + equipmentID + "'", MaintenanceDBConnection);
            MaintenanceDBCommand.ExecuteNonQuery();

            GetMaintenances(PrepareSearchCommand());


        }
        private void maintenanceSearchClearButton_Click(object sender, EventArgs e)
        {
            maintenanceYearComboBox.Text = "--";
            maintenanceMonthComboBox.Text = "--";
            maintenanceGroupComboBox.Text = "--";
            maintenanceDoneMarkComboBox.Text = "--";

            maintenanceAddNotePanel.Enabled = false;
            maintenanceNoteTextBox.Text = "";

            maintenanceDataGridView.ClearSelection();
        }
        private void createActButton_Click(object sender, EventArgs e)
        {
            string equipmentID = maintenanceDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            MaintenanceDBCommand = new OleDbCommand("UPDATE MaintenanceSchedule SET maintenanceAct='True'" +
                " WHERE equipmentID='" + equipmentID + "'", MaintenanceDBConnection);
            MaintenanceDBCommand.ExecuteNonQuery();
            GetMaintenances(PrepareSearchCommand());
        }
        private void maintenanceDoneButton_Click(object sender, EventArgs e)
        {
            int sel = Convert.ToInt32(maintenanceDataGridView.SelectedRows[0].Cells[0].Value);
            if (maintenanceDataGridView.SelectedRows[0].Cells[10].Value.ToString() == "False")
            {
                if (maintenanceExecutorComboBox.Text == "--")
                {
                    MessageBox.Show("Executor field is empty");
                }
                else
                {
                    string maintenanceExecutor = ((Person)maintenanceExecutorComboBox.SelectedItem).name + "/"
                        + ((Person)maintenanceExecutorComboBox.SelectedItem).position;
                    string maintenanceDate = maintenanceDateTimePicker.Value.ToString("dd/MM/yyyy");
                    string equipmentID = maintenanceDataGridView.SelectedRows[0].Cells[5].Value.ToString();

                    MaintenanceDBCommand = new OleDbCommand("UPDATE MaintenanceSchedule SET maintenanceExecutor='" +
                        maintenanceExecutor + "', doneMark='True', maintenanceDate='" + maintenanceDate +
                        "' WHERE equipmentID='" + equipmentID + "'", MaintenanceDBConnection);
                    MaintenanceDBCommand.ExecuteNonQuery();
                }
            }
            else
            {

            }
            GetMaintenances(PrepareSearchCommand());
        }



        private void maintenanceDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            maintenanceCreatePanel.Enabled = false;
        }
        private void maintenanceDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool clear = false;
            for (int i = 0; i < maintenanceDataGridView.SelectedRows.Count; i++)
            {
                if (maintenanceDataGridView.SelectedRows[i].Index == maintenanceDataGridView.RowCount - 1)
                {
                    clear = true;
                }
            }

            if (maintenanceDataGridView.SelectedRows.Count > 1 || clear)
            {
                maintenanceDataGridView.ClearSelection();

                maintenanceAddNotePanel.Enabled = false;
                maintenanceNoteTextBox.Text = "";
                //maintenanceDoneButton.Enabled = false;
            }
            if (maintenanceDataGridView.SelectedRows.Count > 0)
            {
                maintenanceNoteTextBox.Text = maintenanceDataGridView.SelectedRows[0].Cells[14].Value.ToString();
                maintenanceAddNotePanel.Enabled = true;
                //maintenanceDoneButton.Enabled = true;

                if (maintenanceDataGridView.SelectedRows[0].Cells[10].Value.ToString() == "False")
                {
                    maintenanceCreatePanel.Enabled = true;
                    createActButton.Enabled = false;
                }
                else
                {
                    maintenanceCreatePanel.Enabled = false;
                    if (maintenanceDataGridView.SelectedRows[0].Cells[13].Value.ToString() == "False")
                    {
                        createActButton.Enabled = true;
                    }
                    else
                    {
                        createActButton.Enabled = false;
                    }
                        
                }
            }

        }
        private void maintenanceDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            maintenanceDataGridView.ClearSelection();
        }


        private void maintenanceExecutorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            maintenanceExecutorPositionTextBox.Text = ((Person)maintenanceExecutorComboBox.SelectedItem).position;
        }
        //MAINTENANCE********************************************************************************************************************************************************************/


        //PLANNED EXPENSES********************************************************************************************************************************************************************/
        OleDbConnection PlannedExpensesDBConnection;
        OleDbCommand PlannedExpensesDBCommand;
        OleDbDataReader PlannedExpensesDBReader;
        //CLASSES/////////////////////////////////////////////////

        public class PlannedExpenses
        {
            public int ID;
            public string Description;
            public int Year;
            public string Month;
            public int Amount;
            public string DoneMark;
            public string Type;
            public PlannedExpenses(int ID, string Description, int Year, string Month, int Amount, string DoneMark, string Type)
            {
                this.ID = ID;
                this.Description = Description;
                this.Year = Year;
                this.Month = Month;
                this.Amount = Amount;
                this.DoneMark = DoneMark;
                this.Type = Type;
            }
        }



        //FUNCTIONS////////////////////////////////////////////////
        private string PreparePlannedExpensesSearchCommand()
        {
            string tmp = "";

            if (plannedExpensesYearComboBox.Text != "--")
            {
                tmp += " plannedExpensesYear=" + plannedExpensesYearComboBox.Text + " AND";
            }

            if (plannedExpensesMonthComboBox.Text != "--")
            {
                tmp += " plannedExpensesMonth='" + plannedExpensesMonthComboBox.Text + "' AND";
            }
            if (plannedExpensesDoneComboBox.Text != "--")
            {
                tmp += " plannedExpensesDoneMark='" + plannedExpensesDoneComboBox.Text + "' AND";
            }
            if (plannedExpEveTypeComboBox.Text != "--")
            {
                tmp += " plannedExpensesType='" + plannedExpEveTypeComboBox.Text + "' AND";
            }


            if (tmp != "")
            {
                tmp = tmp.Substring(0, tmp.Length - 4);
            }



            return tmp;
        }
        private void GetPlannedExpenses(string command)
        {
            Cursor.Current = Cursors.AppStarting;
            if (command != "")
            {
                command = " WHERE " + command;
            }
            command = "SELECT * FROM PlannedExpenses" + command;

            PlannedExpensesDBCommand = new OleDbCommand(command, PlannedExpensesDBConnection);

            PlannedExpenses[] tmp = new PlannedExpenses[1000];
            int i = 0;

            this.PlannedExpensesDBReader = PlannedExpensesDBCommand.ExecuteReader();
            while (PlannedExpensesDBReader.Read())
            {
                tmp[i] = new PlannedExpenses(Convert.ToInt32(PlannedExpensesDBReader[0]), PlannedExpensesDBReader[1].ToString(),
                    Convert.ToInt32(PlannedExpensesDBReader[2]), PlannedExpensesDBReader[3].ToString(), Convert.ToInt32(PlannedExpensesDBReader[4]),
                    PlannedExpensesDBReader[5].ToString(), PlannedExpensesDBReader[6].ToString());
                i++;
            }
            tmp = tmp.Where(temp => temp != null).ToArray();
            plannedExpensesDataGridView.Rows.Clear();
            if (tmp.Length > 0)
            {
                for (int x = 0; x < tmp.Length; x++)
                {
                    plannedExpensesDataGridView.Rows.Add(tmp[x].ID, tmp[x].Description, tmp[x].Year, tmp[x].Month, tmp[x].Amount, tmp[x].DoneMark, tmp[x].Type);
                }

                int currDate = Convert.ToInt32(DateTime.Now.ToString("MM"));
                int purchEveDate = -1;

                for (int x = 0; x < tmp.Length; x++)
                {
                    switch (plannedExpensesDataGridView.Rows[x].Cells[3].Value.ToString())
                    {
                        case "January": purchEveDate = 1; break;
                        case "February": purchEveDate = 2; break;
                        case "March": purchEveDate = 3; break;
                        case "April": purchEveDate = 4; break;
                        case "May": purchEveDate = 5; break;
                        case "June": purchEveDate = 6; break;
                        case "July": purchEveDate = 7; break;
                        case "August": purchEveDate = 8; break;
                        case "September": purchEveDate = 9; break;
                        case "October": purchEveDate = 10; break;
                        case "November": purchEveDate = 11; break;
                        case "December": purchEveDate = 12; break;
                        default: break;
                    }
                    if (purchEveDate != -1)
                    {
                        if (currDate < purchEveDate)
                        {
                            if(plannedExpensesDataGridView.Rows[x].Cells[5].Value.ToString()=="True")
                            {
                                plannedExpensesDataGridView.Rows[x].Cells[5].Style.BackColor = Color.Red;
                            }
                            else
                            {
                                plannedExpensesDataGridView.Rows[x].Cells[5].Style.BackColor = Color.LightGray;
                            }

                        }
                        if (currDate == purchEveDate)
                        {
                            if (plannedExpensesDataGridView.Rows[x].Cells[5].Value.ToString() == "True")
                            {
                                plannedExpensesDataGridView.Rows[x].Cells[5].Style.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                plannedExpensesDataGridView.Rows[x].Cells[5].Style.BackColor = Color.Orange;
                            }
                        }
                        if (currDate > purchEveDate)
                        {
                            if (plannedExpensesDataGridView.Rows[x].Cells[5].Value.ToString() == "True")
                            {
                                plannedExpensesDataGridView.Rows[x].Cells[5].Style.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                plannedExpensesDataGridView.Rows[x].Cells[5].Style.BackColor = Color.Red;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Something is wrong with month in database");
                    }
                }
            }

            if (plannedExpensesDataGridView.Rows.Count>1)
            {
                int total = 0;
                for(int y=0; y< plannedExpensesDataGridView.Rows.Count-1;y++)
                {
                    total += Convert.ToInt32(plannedExpensesDataGridView.Rows[y].Cells[4].Value);
                }


                plannedExpensesDataGridView.Rows.Add();
                plannedExpensesDataGridView.Rows[plannedExpensesDataGridView.Rows.Count - 2].Cells[3].Value = "TOTAL:";
                plannedExpensesDataGridView.Rows[plannedExpensesDataGridView.Rows.Count - 2].Cells[4].Value = total;
            }
            plannedExpensesDataGridView.ClearSelection();
            Cursor.Current = Cursors.Default;
        }   
        //EVENTS//////////////////////////////////////////////////
        private void addPlannedExpensesYearComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void addPlannedExpensesMonthComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void addPlannedExpensesAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void addExpEveTypeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void addPlannedExpensesButton_Click(object sender, EventArgs e)//////////////////////////
        {

            if (addPlannedExpensesButton.Text== "Add Expenses/Event")
            {
                if (addPlannedExpensesYearComboBox.Text != "--" && addPlannedExpensesMonthComboBox.Text != "--" && addPlannedExpensesAmountTextBox.Text != "" && addPlannedExpensesDescriptionTextBox.Text != "" && addExpEveTypeComboBox.Text!="--")
                {
                    int year = Convert.ToInt32(addPlannedExpensesYearComboBox.Text);
                    string month = addPlannedExpensesMonthComboBox.Text;
                    int amount = Convert.ToInt32(addPlannedExpensesAmountTextBox.Text);
                    string description = addPlannedExpensesDescriptionTextBox.Text;
                    string done = "False";
                    string type = addExpEveTypeComboBox.Text;

                    PlannedExpensesDBCommand = new OleDbCommand("INSERT INTO PlannedExpenses(plannedExpensesDescription, plannedExpensesYear, plannedExpensesMonth, plannedExpensesAmount, plannedExpensesDoneMark, plannedExpensesType) VALUES('" + description + "', " + year + ", '" + month + "', " + amount + ", '" + done + "', '" + type + "')", PlannedExpensesDBConnection);
                    PlannedExpensesDBCommand.ExecuteNonQuery();

                    GetPlannedExpenses(PreparePlannedExpensesSearchCommand());

                    addPlannedExpensesAmountTextBox.Text = "";
                    addPlannedExpensesDescriptionTextBox.Text = "";

                }
                else
                {
                    MessageBox.Show("Some Fields Are Empty");
                }
            }
            else
            {
                int ID = Convert.ToInt32(plannedExpensesDataGridView.SelectedRows[0].Cells[0].Value);
                int year = Convert.ToInt32(addPlannedExpensesYearComboBox.Text);
                string month = addPlannedExpensesMonthComboBox.Text;
                int amount = Convert.ToInt32(addPlannedExpensesAmountTextBox.Text);
                string description = addPlannedExpensesDescriptionTextBox.Text;
                string type = addExpEveTypeComboBox.Text;

                PlannedExpensesDBCommand = new OleDbCommand("UPDATE PlannedExpenses SET plannedExpensesDescription='" + description + "', plannedExpensesYear=" + year + ", plannedExpensesMonth='" + month +
                    "', plannedExpensesAmount=" + amount + ", plannedExpensesType='" + type + "' WHERE ID=" + ID, PlannedExpensesDBConnection);
                PlannedExpensesDBCommand.ExecuteNonQuery();

                addPlannedExpensesAmountTextBox.Text = "";
                addPlannedExpensesDescriptionTextBox.Text = "";
                donePlannedExpensesButton.Enabled = false;
                addPlannedExpensesButton.Text = "Add Expenses/Event";


                GetPlannedExpenses(PreparePlannedExpensesSearchCommand());





            }

        }

    

        private void plannedExpensesYearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            plannedExpensesYearComboBox.BackColor = Color.LightBlue;
            addPlannedExpensesYearComboBox.Text = plannedExpensesYearComboBox.Text;
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
        }
        private void plannedExpensesMonthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            plannedExpensesMonthComboBox.BackColor = Color.LightBlue;
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
        }
        private void plannedExpensesDoneComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            plannedExpensesDoneComboBox.BackColor = Color.LightBlue;
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
        }
        private void plannedExpEveTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            plannedExpEveTypeComboBox.BackColor = Color.LightBlue;
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
        }


        private void plannedExpensesYearComboBox_TextChanged(object sender, EventArgs e)
        {
            plannedExpensesYearComboBox.BackColor = Color.White;
            plannedExpensesYearComboBox.Text = "--";
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
        }
        private void plannedExpensesMonthComboBox_TextChanged(object sender, EventArgs e)
        {
            plannedExpensesMonthComboBox.BackColor = Color.White;
            plannedExpensesMonthComboBox.Text = "--";
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
        }
        private void plannedExpensesDoneComboBox_TextChanged(object sender, EventArgs e)
        {
            plannedExpensesDoneComboBox.BackColor = Color.White;
            plannedExpensesDoneComboBox.Text = "--";
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
        }
        private void plannedExpEveTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            plannedExpEveTypeComboBox.BackColor = Color.White;
            plannedExpEveTypeComboBox.Text = "--";
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
        }
        private void addExpEveTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            addExpEveTypeComboBox.Text = "--";
        }

        private void plannedExpensesSearchClearButton_Click(object sender, EventArgs e)
        {
            plannedExpensesYearComboBox.Text = "--";
            plannedExpensesMonthComboBox.Text = "--";
            plannedExpensesDoneComboBox.Text = "--";
            plannedExpEveTypeComboBox.Text = "--";
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
        }



        private void plannedExpensesDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool clear = false;

            if (plannedExpensesDataGridView.SelectedRows[0].Index == plannedExpensesDataGridView.RowCount - 1)
            {
                clear = true;
            }
            if (plannedExpensesDataGridView.Rows.Count > 2)
            {
                if (plannedExpensesDataGridView.SelectedRows[0].Index == plannedExpensesDataGridView.RowCount - 2)
                {
                    clear = true;
                }
            }

            if (plannedExpensesDataGridView.SelectedRows.Count > 1 || clear)
            {
                plannedExpensesDataGridView.ClearSelection();
                removePlannedExpensesPanel.Enabled = false;


                addPlannedExpensesAmountTextBox.Text= "";
                addPlannedExpensesDescriptionTextBox.Text = "";
                donePlannedExpensesButton.Enabled = false;
                addPlannedExpensesButton.Text = "Add Expenses/Event";///////////////////////////////////////



            }
            if (plannedExpensesDataGridView.SelectedRows.Count > 0)
            {
                if(plannedExpensesDataGridView.SelectedRows[0].Cells[5].Value.ToString()=="False")
                {
                    addPlannedExpensesYearComboBox.Text = plannedExpensesDataGridView.SelectedRows[0].Cells[2].Value.ToString();
                    removePlannedExpensesPanel.Enabled = true;
                    donePlannedExpensesButton.Enabled = true;
                    addPlannedExpensesButton.Text = "Update Expenses/Event";//////////////////////////////////////////
                }
                else
                {
                    removePlannedExpensesPanel.Enabled = false;
                    donePlannedExpensesButton.Enabled = false;
                    addPlannedExpensesButton.Text = "Add Expenses/Event";/////////////////////////////////////
                }


                addPlannedExpensesYearComboBox.Text = plannedExpensesYearComboBox.Text;
                addPlannedExpensesAmountTextBox.Text = plannedExpensesDataGridView.SelectedRows[0].Cells[4].Value.ToString();
                addPlannedExpensesDescriptionTextBox.Text= plannedExpensesDataGridView.SelectedRows[0].Cells[1].Value.ToString();
                addPlannedExpensesMonthComboBox.Text = plannedExpensesDataGridView.SelectedRows[0].Cells[3].Value.ToString();
                

            }
        }
        private void plannedExpensesDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //plannedExpensesDataGridView.ClearSelection();
        }
        private void plannedExpensesDataGridView_SelectionChanged(object sender, EventArgs e)////////////////////////////////////////
        {
            if(plannedExpensesDataGridView.SelectedRows.Count>0) 
            {
                if(plannedExpensesDataGridView.SelectedRows[0].Cells[5].Value !=null)
                {
                    if (plannedExpensesDataGridView.SelectedRows[0].Cells[5].Value.ToString()=="False")
                    {
                        removePlannedExpensesPanel.Enabled = true;
                    }
                        
                }
                
            }
            else
            {
                removePlannedExpensesPanel.Enabled = false;
                donePlannedExpensesButton.Enabled = false;
                addPlannedExpensesButton.Text = "Add Expenses/Event";//////////////////////////////////////////

            }
        }


        private void removePlannedExpensesButton_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(plannedExpensesDataGridView.SelectedRows[0].Cells[0].Value);
            PlannedExpensesDBCommand = new OleDbCommand("DELETE * FROM PlannedExpenses WHERE ID=" + ID, PlannedExpensesDBConnection);
            PlannedExpensesDBCommand.ExecuteNonQuery();
            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());
            removePlannedExpensesPanel.Enabled = false;

        }
        private void donePlannedExpensesButton_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(plannedExpensesDataGridView.SelectedRows[0].Cells[0].Value);
            PlannedExpensesDBCommand = new OleDbCommand("UPDATE PlannedExpenses SET plannedExpensesDoneMark='True' WHERE ID=" + ID, PlannedExpensesDBConnection);
            PlannedExpensesDBCommand.ExecuteNonQuery();

            GetPlannedExpenses(PreparePlannedExpensesSearchCommand());


            addPlannedExpensesAmountTextBox.Text = "";
            addPlannedExpensesDescriptionTextBox.Text = "";
        }
        //PLANNED EXPENSES********************************************************************************************************************************************************************/

        //CONSUMPTION********************************************************************************************************************************************************************/


        //CLASSES/////////////////////////////////////////////////
        OleDbConnection ConsumptionDBConnection;
        OleDbCommand ConsumptionDBCommand;
        OleDbDataReader ConsumptionReader;

        public class Consumption
        {
            public string month;
            public double fullValue;
            public double productionValue;
            public double heatingValue;
            public Consumption(string month, double fullValue, double productionValue, double heatingValue)
            {
                this.month = month;
                this.fullValue = fullValue;
                this.productionValue = productionValue;
                this.heatingValue = heatingValue;
            }
        }

        //FUNCTIONS////////////////////////////////////////////////

        private string GetCommand()
        {
            string command = "consumptionType='" + consumptionTypeComboBox.Text + "' AND consumptionYear=" + Convert.ToInt32(consumptionYearComboBox.Text);
            return command;
        }
        private Consumption[] GetConsumptionData(string command)
        {
            double[] fullValues = new double[12];
            double[] productionValues = new double[12];
            double[] heatingValues = new double[12];
            int y = 0;
            ConsumptionDBCommand = new OleDbCommand("SELECT consumptionFullValue FROM Consumption WHERE " + command, ConsumptionDBConnection);
            this.ConsumptionReader = ConsumptionDBCommand.ExecuteReader();
            while(ConsumptionReader.Read())
            {
                fullValues[y] = Convert.ToDouble(ConsumptionReader[0]);
                y++;
            }
            y = 0;
            ConsumptionDBCommand = new OleDbCommand("SELECT consumptionProductionValue FROM Consumption WHERE " + command, ConsumptionDBConnection);
            this.ConsumptionReader = ConsumptionDBCommand.ExecuteReader();
            while (ConsumptionReader.Read())
            {
                productionValues[y] = Convert.ToDouble(ConsumptionReader[0]);
                y++;
            }
            y = 0;
            ConsumptionDBCommand = new OleDbCommand("SELECT consumptionHeatingValue FROM Consumption WHERE " + command, ConsumptionDBConnection);
            this.ConsumptionReader = ConsumptionDBCommand.ExecuteReader();
            while (ConsumptionReader.Read())
            {
                heatingValues[y] = Convert.ToDouble(ConsumptionReader[0]);
                y++;
            }



            Consumption[] tmp = new Consumption[12];
            for(int i=0;i<12;i++)
            {
                switch (i)
                {
                    case 0: tmp[0] = new Consumption("January", fullValues[0], productionValues[0], heatingValues[0]); break;
                    case 1: tmp[1] = new Consumption("February", fullValues[1], productionValues[1], heatingValues[1]); break;
                    case 2: tmp[2] = new Consumption("March", fullValues[2], productionValues[2], heatingValues[2]); break;
                    case 3: tmp[3] = new Consumption("April", fullValues[3], productionValues[3], heatingValues[3]); break;
                    case 4: tmp[4] = new Consumption("May", fullValues[4], productionValues[4], heatingValues[4]); break;
                    case 5: tmp[5] = new Consumption("June", fullValues[5], productionValues[5], heatingValues[5]); break;
                    case 6: tmp[6] = new Consumption("July", fullValues[6], productionValues[6], heatingValues[6]); break;
                    case 7: tmp[7] = new Consumption("August", fullValues[7], productionValues[7], heatingValues[7]); break;
                    case 8: tmp[8] = new Consumption("September", fullValues[8], productionValues[8], heatingValues[8]); break;
                    case 9: tmp[9] = new Consumption("October", fullValues[9], productionValues[9], heatingValues[9]); break;
                    case 10: tmp[10] = new Consumption("November", fullValues[10], productionValues[10], heatingValues[10]); break;
                    case 11: tmp[11] = new Consumption("December", fullValues[11], productionValues[11], heatingValues[11]); break;
                }
            }
            return tmp;
        }
        private void RedrawChart(Consumption[] consumptions)
        {
            Cursor.Current = Cursors.AppStarting;
            Consumption[] tmp = new Consumption[12];
            tmp = consumptions;
            double yearTotal = 0;
            double yearProduction = 0;
            double yearHeating = 0;
            totalConsumkptionTextBox.Text = "";

            if (consumptionTypeComboBox.Text=="Gas")
            {
                consumptionChart.Series[3].Enabled = false;
                for (int i=0;i<tmp.Length;i++)
                {
                    yearTotal += tmp[i].fullValue;
                    yearProduction += tmp[i].productionValue;
                    yearHeating += tmp[i].heatingValue;
                }
                totalConsumkptionTextBox.Text = "Total consumption per year: " + Convert.ToString(yearTotal) + " cub. m ||" +
                    " Production consumption per year: " + Convert.ToString(yearProduction) + " cub. m ||" +
                    " Heating consumption per year: " + Convert.ToString(yearHeating) + " cub. m";
                consumptionChart.Series[0].Name="Total, cub. m";
                consumptionChart.Series[1].Name = "Production, cub. m";
                consumptionChart.Series[2].Name = "Heating, cub. m";
                consumptionChart.Titles[0].Text = "Gas consumption per " + consumptionYearComboBox.Text + " year";
                consumptionChart.Titles[1].Text = "Gas consumption, cub m";
            }
            if (consumptionTypeComboBox.Text == "Electricity")
            {
                consumptionChart.Series[3].Enabled = false;
                for (int i = 0; i < tmp.Length; i++)
                {
                    yearTotal += tmp[i].fullValue;
                    yearProduction += tmp[i].productionValue;
                    yearHeating += tmp[i].heatingValue;
                }
                totalConsumkptionTextBox.Text = "Total consumption per year: " + Convert.ToString(yearTotal) + " W*h ||" +
                    " Production consumption per year: " + Convert.ToString(yearProduction) + " W*h ||" +
                    " Heating consumption per year: " + Convert.ToString(yearHeating) + " W*h";
                consumptionChart.Series[0].Name = "Total, W*h";
                consumptionChart.Series[1].Name = "Production, W*h";
                consumptionChart.Series[2].Name = "Heating, W*h";
                consumptionChart.Titles[0].Text = "Electricity consumption per " + consumptionYearComboBox.Text + " year";
                consumptionChart.Titles[1].Text = "Electricity consumption, W*h";
            }

            if (consumptionTypeComboBox.Text == "Water")
            {
                consumptionChart.Series[3].Enabled = true;
                for (int i = 0; i < tmp.Length; i++)
                {
                    yearTotal += tmp[i].fullValue;
                    yearProduction += tmp[i].productionValue;
                    yearHeating += tmp[i].heatingValue;
                }
                totalConsumkptionTextBox.Text = "КВМ-U 178083-07 consumption per year: " + Convert.ToString(yearTotal) + " cub. m ||" +
                    " КВМ-U 103836 consumption per year: " + Convert.ToString(yearProduction) + " cub. m ||" +
                    " КВ-1,5 104512 consumption per year: " + Convert.ToString(yearHeating) + " cub. m || Total: " + Convert.ToString(yearTotal + yearProduction + yearHeating) + " cub. m";
                consumptionChart.Series[0].Name = "КВМ-U 178083-07, cub. m";
                consumptionChart.Series[1].Name = "КВМ-U 103836, cub. m";
                consumptionChart.Series[2].Name = "КВ-1,5 104512, cub. m";
                consumptionChart.Titles[0].Text = "Water consumption per " + consumptionYearComboBox.Text + " year";
                consumptionChart.Titles[1].Text = "Water consumption, cub m";
            }


            for (int i = 0; i < consumptionChart.Series.Count; i++)
            {
                consumptionChart.Series[i].Points.Clear();
            }

            for (int i = 0; i < tmp.Length; i++)
            {
                consumptionChart.Series[0].Points.AddXY(tmp[i].month, tmp[i].fullValue);
                consumptionChart.Series[1].Points.AddXY(tmp[i].month, tmp[i].productionValue);
                consumptionChart.Series[2].Points.AddXY(tmp[i].month, tmp[i].heatingValue);

                consumptionChart.Series[0].Points[i].Label = consumptionChart.Series[0].Points[i].YValues[0].ToString();
                consumptionChart.Series[1].Points[i].Label = consumptionChart.Series[1].Points[i].YValues[0].ToString();
                consumptionChart.Series[2].Points[i].Label = consumptionChart.Series[2].Points[i].YValues[0].ToString();
                if (consumptionTypeComboBox.Text == "Water")
                {
                    
                    consumptionChart.Series[3].Points.AddXY(tmp[i].month, tmp[i].fullValue + tmp[i].productionValue + tmp[i].heatingValue);
                    consumptionChart.Series[3].Points[i].Label = consumptionChart.Series[3].Points[i].YValues[0].ToString();
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private bool CheckMonth()
        {
            bool result = false;

            ConsumptionDBCommand = new OleDbCommand("SELECT consumptionFullValue FROM Consumption WHERE consumptionMonth='" + addConsumptionMonthComboBox.Text + "'" +
                " AND consumptionType='" + addConsumptionTypeComboBox.Text + "'" +
                " AND consumptionYear=" + Convert.ToDouble(addConsumptionYearComboBox.Text), ConsumptionDBConnection);
            this.ConsumptionReader = ConsumptionDBCommand.ExecuteReader();
            while(ConsumptionReader.Read())
            {
                if(Convert.ToInt32(ConsumptionReader[0])>0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            


            return result;
        }
        private void UpdateData()
        {
            string type = addConsumptionTypeComboBox.Text;
            int year = Convert.ToInt32(addConsumptionYearComboBox.Text);
            string month = addConsumptionMonthComboBox.Text;
            double full = Convert.ToDouble(fullValueTextBox.Text);
            double production = Convert.ToDouble(productionValueTextBox.Text);
            double heating = Convert.ToDouble(heatingValueTextBox.Text);
            ConsumptionDBCommand = new OleDbCommand("UPDATE Consumption SET consumptionFullValue=" + full + ", consumptionProductionValue=" + production + ", consumptionHeatingValue=" + heating +
                " WHERE consumptionType='" + type + "' AND consumptionYear=" + year + " AND consumptionMonth='" + month + "'", ConsumptionDBConnection);
            ConsumptionDBCommand.ExecuteNonQuery();

            consumptionTypeComboBox.Text = addConsumptionTypeComboBox.Text;
            consumptionYearComboBox.Text = addConsumptionYearComboBox.Text;
        }

        private void SetConsumptionsLabels()
        {
            if (addConsumptionTypeComboBox.Text == "Gas")
            {
                label29.Text = "Full value:";
                label33.Text = "Production value:";
                label36.Text = "Heating value:";

                addConsumptionValueUnitsLabel.Text = "cub. m";
                label32.Text = "cub. m";
                label35.Text = "cub. m";

                addConsumptionValueUnitsLabel.Visible = true;
                label32.Visible = true;
                label35.Visible = true;
            }
            if (addConsumptionTypeComboBox.Text == "Electricity")
            {
                label29.Text = "Full value:";
                label33.Text = "Production value:";
                label36.Text = "Heating value:";

                addConsumptionValueUnitsLabel.Text = "W*h";
                label32.Text = "W*h";
                label35.Text = "W*h";

                addConsumptionValueUnitsLabel.Visible = true;
                label32.Visible = true;
                label35.Visible = true;
            }
            if (addConsumptionTypeComboBox.Text == "Water")
            {
                label29.Text = "КВМ-U 178083-07:";
                label33.Text = "КВМ-U 103836:";
                label36.Text = "КВ-1,5 104512:";

                addConsumptionValueUnitsLabel.Text = "cub. m";
                label32.Text = "cub. m";
                label35.Text = "cub. m";

                addConsumptionValueUnitsLabel.Visible = true;
                label32.Visible = true;
                label35.Visible = true;
            }
        }



        //EVENTS/////////////////////////////////////////////////

        private void consumptionTypeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void consumptionYearComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void addConsumptionTypeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void addConsumptionYearComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void addConsumptionMonthComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void fullValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void productionValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void heatingValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }


        private void consumptionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawChart(GetConsumptionData(GetCommand()));
            addConsumptionTypeComboBox.Text = consumptionTypeComboBox.Text;
            addConsumptionYearComboBox.Text = consumptionYearComboBox.Text;
        }
        private void consumptionYearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawChart(GetConsumptionData(GetCommand()));
            addConsumptionTypeComboBox.Text = consumptionTypeComboBox.Text;
            addConsumptionYearComboBox.Text = consumptionYearComboBox.Text;
        }
        private void addConsumptionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetConsumptionsLabels();
        }

        private void addConsumptionValueButton_Click(object sender, EventArgs e)
        {
            if(fullValueTextBox.Text!="" && productionValueTextBox.Text!="" && heatingValueTextBox.Text!="")
            {
                if(CheckMonth())
                {
                    UpdateData();
                    RedrawChart(GetConsumptionData(GetCommand()));
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Data Has Already Been Entered."+"\r\n"+"Rewrite?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        UpdateData();
                        RedrawChart(GetConsumptionData(GetCommand()));

                        fullValueTextBox.Text = "";
                        productionValueTextBox.Text = "";
                        heatingValueTextBox.Text = "";
                    }

                }
            }
            else
            {
                MessageBox.Show("Some Fields Are Empty");
            }
        }

        //CONSUMPTION********************************************************************************************************************************************************************/




        //CURRENCY********************************************************************************************************************************************************************/
        CancellationTokenSource CancellationTokenThread { get; set; }
        Thread thread { get; set; }
        private void readCurrency()
        {
            //CancellationTokenThread = new CancellationTokenSource();
            //thread = new Thread(new ThreadStart(DownloadExCurr));
            //thread.Start();
            DownloadExCurr();
        }
        void DownloadExCurr()
        {
            bool isOK = false;

            XPathDocument xPathDoc;
            XPathNavigator xPathDocNavigator = null;
            string currentDate = Convert.ToString(DateTime.Now.ToShortDateString());
            string[] currentDateTemp = new string[3];
            currentDateTemp = currentDate.Split('.');

            try
            {
                xPathDoc = new XPathDocument("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchangenew?date=" + currentDateTemp[2] + currentDateTemp[1] + currentDateTemp[0]);
                xPathDocNavigator = xPathDoc.CreateNavigator();

                isOK = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (isOK)
            {
                XPathNodeIterator currRate = xPathDocNavigator.Select("/exchange/currency");

                while (currRate.MoveNext() == true)
                {
                    //if (CancellationTokenThread.IsCancellationRequested) break;

                    XPathNavigator current = currRate.Current;

                    string code_R030 = int.Parse(current?.SelectSingleNode("r030")?.Value ?? "0").ToString("D3");
                    string currencyName = current?.SelectSingleNode("txt")?.Value ?? "";
                    string currencyShortName = current?.SelectSingleNode("cc")?.Value ?? "";
                    decimal currencyExchange = decimal.Parse(current?.SelectSingleNode("rate")?.Value.Replace(".", ",") ?? "0");
                    DateTime currencyExchangeDate = DateTime.Parse(current?.SelectSingleNode("exchangedate")?.Value ?? DateTime.MinValue.ToString());

                    if (code_R030 == "840")
                    {
                        USDUpdate(Convert.ToString(currencyExchange));

                    }
                    if (code_R030 == "978")
                    {
                        EURUpdate(Convert.ToString(currencyExchange));
                    }

                    if (this.InvokeRequired) BeginInvoke(new MethodInvoker(delegate
                    {
                        currencyLabel.Text = "Date: " + Convert.ToString(currencyExchangeDate.Date.ToShortDateString());
                    }
                     ));
                    else
                    {
                        currencyLabel.Text = "Date: " + Convert.ToString(currencyExchangeDate.Date.ToShortDateString());
                    }
                }
            }
            else
            {
                currencyLabel.Text = "Date: " + DateTime.Now.ToShortDateString();
                USDCurrencyLabel.Text = "USD: N/A";
                EURCurrencyLabel.Text = "EUR: N/A";
            }
  
        }
        private void USDUpdate(string data)
        {
            if (USDCurrencyLabel.InvokeRequired)
            {
                USDCurrencyLabel.Invoke(new Action<string>(USDUpdate), data);
            }
            else
            {
                USDCurrencyLabel.Text = "USD: " + data;
            }

        }
        private void EURUpdate(string data)
        {
            if (EURCurrencyLabel.InvokeRequired)
            {
                EURCurrencyLabel.Invoke(new Action<string>(EURUpdate), data);
            }
            else
            {
                EURCurrencyLabel.Text = "EUR: " + data;
            }
        }

        private void currencyTimer_Tick(object sender, EventArgs e)
        {
            readCurrency();
            currencyTimer.Enabled = false;
        }

        //CURRENCY********************************************************************************************************************************************************************/

        //WORK TIME********************************************************************************************************************************************************************/
        OleDbConnection WorkTimeDBConnection;
        OleDbCommand WorkTimeDBCommand;
        OleDbDataReader WorkTimeDBReader;
        OleDbConnection ConstantsDBConnection;
        OleDbCommand ConstantsDBCommand;
        OleDbDataReader ConstantsDBReader;
        //CLASSES/////////////////////////////////////////////////
        public class WorkTime
        {
            public string personName;
            public string personStatus;
            public int workYear;
            public string workMonth;
            public int[] workDay;
            public int[] workHours;
            public int[] paymentMultiplier;
            public int[] workShift;
            public string[] rate;
            public int[] nightWorkHours;
            public int[] daysOfWeek;
            public string[] dayType;

            public WorkTime(string personName, string personStatus, int workYear, string workMonth, int[] workDay, int[] workHours, int[] nightWorkHours, int[] paymentMultiplier, int[] workShift, string[] rate, int[] daysOfWeek, string[] dayType)
            {
                this.personName = personName;
                this.personStatus = personStatus;
                this.workYear = workYear;
                this.workMonth = workMonth;
                this.workDay = workDay;
                this.workHours = workHours;
                this.nightWorkHours = nightWorkHours;
                this.paymentMultiplier = paymentMultiplier;
                this.workShift = workShift;
                this.rate = rate;
                this.daysOfWeek = daysOfWeek;
                this.dayType = dayType;
            }
        }
        //FUNCTIONS////////////////////////////////////////////////
        private void GetWorkTime()
        {         
            Cursor.Current = Cursors.AppStarting;
            int month = 0;
            switch (searchWorkTimeMonthComboBox.Text)
            {
                case "January": month = 1; break;
                case "February": month = 2; break;
                case "March": month = 3; break;
                case "April": month = 4; break;
                case "May": month = 5; break;
                case "June": month = 6; break;
                case "July": month = 7; break;
                case "August": month = 8; break;
                case "September": month = 9; break;
                case "October": month = 10; break;
                case "November": month = 11; break;
                case "December": month = 12; break;
                default: break;
            }
            int maxDays = DateTime.DaysInMonth(Convert.ToInt32(searchWorkTimeYearComboBox.Text), month);

            WorkTime[] tmp = new WorkTime[10];
            string name = "";
            string status = "";
            int year = 0;

            string comm = "workYear=" + Convert.ToInt32(searchWorkTimeYearComboBox.Text) + " AND" + " workMonth='" + searchWorkTimeMonthComboBox.Text + "' AND personName='";

            if (showAllPersonsCheckBox.Checked == false)
            {
                int[] hours = new int[40];
                int[] nightHours = new int[40];
                int[] days = new int[40];
                int[] multiplier = new int[40];
                int[] shift = new int[40];
                string[] rate = new string[40];
                int[] daysOfWeek = new int[40];
                string[] daysType = new string[40];

                name = ((Person)searchWorkTimePersonComboBox.SelectedItem).name;
                status = ((Person)searchWorkTimePersonComboBox.SelectedItem).position;
                year = Convert.ToInt32(searchWorkTimeYearComboBox.Text);


                int i = 0;

                string command = "SELECT * FROM WorkTime WHERE " + comm + name + "'";
                WorkTimeDBCommand = new OleDbCommand(command, WorkTimeDBConnection);
                this.WorkTimeDBReader = WorkTimeDBCommand.ExecuteReader();
                while (WorkTimeDBReader.Read())
                {
                    days[i] = Convert.ToInt32(WorkTimeDBReader[4]);
                    hours[i] = Convert.ToInt32(WorkTimeDBReader[5]);
                    multiplier[i] = Convert.ToInt32(WorkTimeDBReader[6]);
                    shift[i] = Convert.ToInt32(WorkTimeDBReader[7]);
                    rate[i] = WorkTimeDBReader[8].ToString();
                    nightHours[i] = Convert.ToInt32(WorkTimeDBReader[9]);
                    daysOfWeek[i] = Convert.ToInt32(WorkTimeDBReader[10]);
                    daysType[i] = WorkTimeDBReader[11].ToString();
                    i++;
                }

                tmp[0] = new WorkTime(name, status, year, searchWorkTimeMonthComboBox.Text, days, hours, nightHours, multiplier, shift, rate, daysOfWeek, daysType);

                Array.Resize(ref tmp[0].workDay, maxDays);
                Array.Resize(ref tmp[0].workHours, maxDays);
                Array.Resize(ref tmp[0].nightWorkHours, maxDays);
                Array.Resize(ref tmp[0].paymentMultiplier, maxDays);
                Array.Resize(ref tmp[0].workShift, maxDays);
                Array.Resize(ref tmp[0].rate, maxDays);
                Array.Resize(ref tmp[0].rate, maxDays);
                Array.Resize(ref tmp[0].daysOfWeek, maxDays);
                Array.Resize(ref tmp[0].dayType, maxDays);
            }
            else
            {
                for (int i = 0; i < searchWorkTimePersonComboBox.Items.Count; i++)
                {
                    name = ((Person)searchWorkTimePersonComboBox.Items[i]).name;
                    status = ((Person)searchWorkTimePersonComboBox.Items[i]).position;
                    year = Convert.ToInt32(searchWorkTimeYearComboBox.Text);

                    int[] hours = new int[40];
                    int[] nightHours = new int[40];
                    int[] days = new int[40];
                    int[] multiplier = new int[40];
                    int[] shift = new int[40];
                    string[] rate = new string[40];
                    int[] daysOfWeek = new int[40];
                    string[] daysType = new string[40];

                    int y = 0;

                    string command = "SELECT * FROM WorkTime WHERE " + comm + name + "'";
                    WorkTimeDBCommand = new OleDbCommand(command, WorkTimeDBConnection);
                    this.WorkTimeDBReader = WorkTimeDBCommand.ExecuteReader();
                    while (WorkTimeDBReader.Read())
                    {
                        days[y] = Convert.ToInt32(WorkTimeDBReader[4]);
                        hours[y] = Convert.ToInt32(WorkTimeDBReader[5]);
                        multiplier[y] = Convert.ToInt32(WorkTimeDBReader[6]);
                        shift[y] = Convert.ToInt32(WorkTimeDBReader[7]);
                        rate[y] = WorkTimeDBReader[8].ToString();
                        nightHours[y] = Convert.ToInt32(WorkTimeDBReader[9]);
                        daysOfWeek[y] = Convert.ToInt32(WorkTimeDBReader[10]);
                        daysType[y] = WorkTimeDBReader[11].ToString();
                        y++;
                    }

                    tmp[i] = new WorkTime(name, status, year, searchWorkTimeMonthComboBox.Text, days, hours, nightHours, multiplier, shift, rate, daysOfWeek, daysType);

                    Array.Resize(ref tmp[i].workDay, maxDays);
                    Array.Resize(ref tmp[i].workHours, maxDays);
                    Array.Resize(ref tmp[i].nightWorkHours, maxDays);
                    Array.Resize(ref tmp[i].paymentMultiplier, maxDays);
                    Array.Resize(ref tmp[i].workShift, maxDays);
                    Array.Resize(ref tmp[i].rate, maxDays);
                    Array.Resize(ref tmp[i].daysOfWeek, maxDays);
                    Array.Resize(ref tmp[i].dayType, maxDays);
                }

            }

            tmp = tmp.Where(temp => temp != null).ToArray();

            for (int i = 0; i < tmp.Length; i++)
            {
                int[] temp = new int[maxDays];
                for (int y = 0; y < tmp[i].workHours.Length; y++)
                {
                    if(tmp[i].workDay[y]!=0)
                    {
                        temp[tmp[i].workDay[y] - 1] = tmp[i].workHours[y];
                    }
                }
                tmp[i].workHours = temp;

                int[] temp1 = new int[maxDays];
                for (int y = 0; y < tmp[i].workShift.Length; y++)
                {
                    if (tmp[i].workDay[y] != 0)
                    {
                        temp1[tmp[i].workDay[y] - 1] = tmp[i].workShift[y];
                    }
                }
                tmp[i].workShift = temp1;

                int[] temp2 = new int[maxDays];
                for (int y = 0; y < tmp[i].paymentMultiplier.Length; y++)
                {
                    if (tmp[i].workDay[y] != 0)
                    {
                        temp2[tmp[i].workDay[y] - 1] = tmp[i].paymentMultiplier[y];
                    }
                }
                tmp[i].paymentMultiplier = temp2;

                string[] temp3 = new string[maxDays];
                for (int y = 0; y < tmp[i].rate.Length; y++)
                {
                    if (tmp[i].workDay[y] != 0)
                    {
                        temp3[tmp[i].workDay[y] - 1] = tmp[i].rate[y];
                    }
                }
                tmp[i].rate = temp3;

                int[] temp4 = new int[maxDays];
                for (int y = 0; y < tmp[i].nightWorkHours.Length; y++)
                {
                    if (tmp[i].workDay[y] != 0)
                    {
                        temp4[tmp[i].workDay[y] - 1] = tmp[i].nightWorkHours[y];
                    }
                }
                tmp[i].nightWorkHours = temp4;

                int[] temp5 = new int[maxDays];
                for (int y = 0; y < tmp[i].daysOfWeek.Length; y++)
                {
                    if (tmp[i].workDay[y] != 0)
                    {
                        temp5[tmp[i].workDay[y] - 1] = tmp[i].daysOfWeek[y];
                    }
                }
                tmp[i].daysOfWeek = temp5;

                string[] temp6 = new string[maxDays];
                for (int y = 0; y < tmp[i].dayType.Length; y++)
                {
                    if (tmp[i].workDay[y] != 0)
                    {
                        temp6[tmp[i].workDay[y] - 1] = tmp[i].dayType[y];
                    }
                }
                tmp[i].dayType = temp6;
            }

            workTimeDataGridView.Rows.Clear();
            workTimeDataGridView.Columns.Clear();

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Name";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.DefaultCellStyle = new DataGridViewCellStyle();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.Resizable = DataGridViewTriState.True;
            workTimeDataGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Position";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            column.DefaultCellStyle = new DataGridViewCellStyle();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.Resizable = DataGridViewTriState.True;
            workTimeDataGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Year/Month";
            column.Width = 80;
            column.DefaultCellStyle = new DataGridViewCellStyle();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.Resizable = DataGridViewTriState.False;
            workTimeDataGridView.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Description";
            column.Width = 70;
            column.DefaultCellStyle = new DataGridViewCellStyle();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            column.Resizable = DataGridViewTriState.False;
            workTimeDataGridView.Columns.Add(column);

            for (int i = 0; i < maxDays; i++)
            {
                column = new DataGridViewTextBoxColumn();
                column.HeaderText = Convert.ToString(i + 1);
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Resizable = DataGridViewTriState.False;
                column.Width = 30;

                workTimeDataGridView.Columns.Add(column);
            }

            column = new DataGridViewTextBoxColumn();
            column.HeaderText = "Total";
            column.Width = 70;
            column.DefaultCellStyle = new DataGridViewCellStyle();
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            workTimeDataGridView.Columns.Add(column);

            


            for (int i = 0; i < tmp.Length; i++)
            {
                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[0].Value = tmp[i].personName;
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[1].Value = tmp[i].personStatus;
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[2].Value = tmp[i].workYear + "/"+ tmp[i].workMonth;
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "Hours";
                for (int y = 4; y < (maxDays + 4); y++)
                {
                    if (tmp[i].workHours[y - 4]!=0)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Value = tmp[i].workHours[y - 4];
                    }
                    if (tmp[i].daysOfWeek[y - 4] == 0 || tmp[i].daysOfWeek[y - 4] == 6)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Style.BackColor = Color.LightGray;
                    }
                }
                int total = 0;
                for (int y = 0; y < tmp[i].workHours.Length; y++)
                {
                    total += tmp[i].workHours[y];
                }
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells.Count - 1].Value = total + "HRS";

                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "Shift";
                for (int y = 4; y < (maxDays + 4); y++)
                {
                    if (tmp[i].workShift[y - 4]!=0)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Value = tmp[i].workShift[y - 4];
                    }
                    if (tmp[i].daysOfWeek[y - 4] == 0 || tmp[i].daysOfWeek[y - 4] == 6)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Style.BackColor = Color.LightGray;
                    }
                }

                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "Night Hours";
                for (int y = 4; y < (maxDays + 4); y++)
                {
                    if (tmp[i].nightWorkHours[y - 4]!=0)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Value = tmp[i].nightWorkHours[y - 4];
                    }
                    if (tmp[i].daysOfWeek[y - 4] == 0 || tmp[i].daysOfWeek[y - 4] == 6)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Style.BackColor = Color.LightGray;
                    }
                }

                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "Multiplier";
                for (int y = 4; y < (maxDays + 4); y++)
                {
                    if (tmp[i].paymentMultiplier[y - 4]!=0)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Value = tmp[i].paymentMultiplier[y - 4];
                    }
                    if (tmp[i].daysOfWeek[y - 4] == 0 || tmp[i].daysOfWeek[y - 4] == 6)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Style.BackColor = Color.LightGray;
                    }
                }

                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "Rate, UAH";
                for (int y = 4; y < (maxDays + 4); y++)
                {
                    workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Value = tmp[i].rate[y - 4];
                    if(workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Value==null)
                    {
                        //workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Value = "0";
                    }
                    if (tmp[i].daysOfWeek[y - 4] == 0 || tmp[i].daysOfWeek[y - 4] == 6)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Style.BackColor = Color.LightGray;
                    }
                }

                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "WeekDay";
                for (int y = 4; y < (maxDays + 4); y++)
                {
                    if (tmp[i].workHours[y - 4] != 0)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Value = tmp[i].daysOfWeek[y - 4];
                    }
                    if (tmp[i].daysOfWeek[y - 4] == 0 || tmp[i].daysOfWeek[y - 4] == 6)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Style.BackColor = Color.LightGray;
                    }


                }

                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "DayType";
                for (int y = 4; y < (maxDays + 4); y++)
                {
                    if (tmp[i].dayType[y - 4] != null)
                    {
                        workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[y].Value = tmp[i].dayType[y - 4];
                    }


                }






                string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                for (int y = 0; y < tmp[i].rate.Length; y++)
                {
                    if (tmp[i].rate[y] != null)
                    {
                        tmp[i].rate[y]=tmp[i].rate[y].Replace(".", decimalSeparator);
                    }
                }



                double totalPayment = 0;
                int bonus = Convert.ToInt32(bonusComboBox.Text)*10;
                int add = Convert.ToInt32(additionalComboBox.Text)*10;




                for (int y = 0; y < tmp[i].workHours.Length; y++)
                {
                    if (tmp[i].dayType[y]=="Wrk")
                    {
                        if (tmp[i].daysOfWeek[y] > 0 && tmp[i].daysOfWeek[y] < 6)
                        {
                            double t = ((tmp[i].workHours[y] - tmp[i].nightWorkHours[y]) * Convert.ToDouble(tmp[i].rate[y])) +
                                ((tmp[i].nightWorkHours[y]) * Convert.ToDouble(tmp[i].rate[y]) * 1.2);
                            totalPayment += t + t / 1000 * (add + bonus);
                        }
                        else
                        {
                            double t = ((tmp[i].workHours[y] - tmp[i].nightWorkHours[y]) * Convert.ToDouble(tmp[i].rate[y])) +
                                ((tmp[i].nightWorkHours[y]) * Convert.ToDouble(tmp[i].rate[y]) * 1.2);
                            totalPayment += t + t / 1000 * bonus;
                            if (tmp[i].paymentMultiplier[y] == 2)
                            {
                                totalPayment += t;
                            }
                        }
                    }
                    if (tmp[i].dayType[y] == "Vac")
                    {
                        totalPayment += Convert.ToDouble(tmp[i].rate[y]);
                    }


                }



                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "Payment, UAH";
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells.Count - 1].Value = totalPayment.ToString() + "UAH";

                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "Tax, UAH";
                double tax = Math.Round(totalPayment / 1000 * 180, 2);
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells.Count - 1].Value = "-" + tax.ToString() + "UAH";

                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "War Tax, UAH";
                double warTax = Math.Round(totalPayment / 1000 * 15, 2);
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells.Count - 1].Value = "-" + warTax.ToString() + "UAH";

                workTimeDataGridView.Rows.Add();
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[3].Value = "Res. Payment, UAH";
                double rest = totalPayment - tax - warTax;
                workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells[workTimeDataGridView.Rows[workTimeDataGridView.Rows.Count - 2].Cells.Count - 1].Value = rest.ToString() + "UAH";

            }

            workTimeDataGridView.ClearSelection();
            Cursor.Current = Cursors.Default;
        }
        private void SetWorkTime()
        {
            bool checkResult = false;

            string personName = ((Person)addWorkTimePersonComboBox.SelectedItem).name;
            string personStatus = ((Person)addWorkTimePersonComboBox.SelectedItem).position;
            int year = Convert.ToInt32(addWorkTimeDateTimePicker.Value.ToString("yyyy"));
            string month = "";
            int day = Convert.ToInt32(addWorkTimeDateTimePicker.Value.ToString("dd"));
            int workHours = 0;
            int multiplier = Convert.ToInt32(addWorkTimeMultiplierComboBox.Text);
            int shift = Convert.ToInt32(workShiftComboBox.Text);
            string rate = rateTextBox.Text;
            int nightHours = Convert.ToInt32(nightHoursTextBox.Text);
            int dayOfWeek = Convert.ToInt32(addWorkTimeDateTimePicker.Value.DayOfWeek);
            string dayType = dayTypeComboBox.Text;

            for (int i = 1; i < 13; i++)
            {
                switch (Convert.ToInt32(addWorkTimeDateTimePicker.Value.ToString("MM")))
                {
                    case 1: month = "January"; break;
                    case 2: month = "February"; break;
                    case 3: month = "March"; break;
                    case 4: month = "April"; break;
                    case 5: month = "May"; break;
                    case 6: month = "June"; break;
                    case 7: month = "July"; break;
                    case 8: month = "August"; break;
                    case 9: month = "September"; break;
                    case 10: month = "October"; break;
                    case 11: month = "November"; break;
                    case 12: month = "December"; break;
                    default: break;
                }
            }

            WorkTimeDBCommand = new OleDbCommand("SELECT workHours FROM WorkTime WHERE personName='" + addWorkTimePersonComboBox.Text +
                "' AND workYear=" + year + " AND workMonth='" + month + "' AND workDay=" + day, WorkTimeDBConnection);
            this.WorkTimeDBReader = WorkTimeDBCommand.ExecuteReader();
            if (WorkTimeDBReader.Read())
            {
                checkResult = false;
                MessageBox.Show("Not Empty Value");
            }
            else
            {
                checkResult = true;
            }
            if (checkResult)
            {
                if (dayTypeComboBox.Text == "Wrk")
                {
                    if (addWorkTimePersonComboBox.Text != "" && addWorkTimeTextBox.Text != "" && addWorkTimeMultiplierComboBox.Text != "" && addWorkTimeDateTimePicker.Value != null && workShiftComboBox.Text != "")
                    {
                        workHours = Convert.ToInt32(addWorkTimeTextBox.Text);

                        WorkTimeDBCommand = new OleDbCommand("INSERT INTO WorkTime VALUES('" + personName + "', '" + personStatus + "', " + year + ", '" + month + "', "
                            + day + ", " + workHours + ", " + multiplier + ", " + shift + ", '" + rate + "', " + nightHours + "," + dayOfWeek + ",'" + dayType + "')", WorkTimeDBConnection);
                        WorkTimeDBCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        if (dayTypeComboBox.Text == "Wrk")
                        {
                            MessageBox.Show("Some Fields Are Empty");
                        }
                    }
                }

                if (dayTypeComboBox.Text == "Vac")
                {

                    DialogResult dialogResult = MessageBox.Show("Entered VACATION day type." + "\r\n" + "Are you sure?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        workHours = 0;
                        multiplier = 0;
                        shift = 0;
                        nightHours = 0;
                        WorkTimeDBCommand = new OleDbCommand("INSERT INTO WorkTime VALUES('" + personName + "', '" + personStatus + "', " + year + ", '" + month + "', "
                            + day + ", " + workHours + ", " + multiplier + ", " + shift + ", '" + rate + "', " + nightHours + "," + dayOfWeek + ",'" + dayType + "')", WorkTimeDBConnection);
                        WorkTimeDBCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private void CreateWorkTimeTable()
        {
            Cursor.Current = Cursors.AppStarting;
            string dir= Environment.CurrentDirectory + "\\Data\\WorkTime\\";
            string file = searchWorkTimeYearComboBox.Text + "_" + searchWorkTimeMonthComboBox.Text;

            using (var p = new ExcelPackage())
            {
                string wsName = "Енергослужба_" + searchWorkTimeMonthComboBox.Text + "_" + searchWorkTimeYearComboBox.Text;
                var ws = p.Workbook.Worksheets.Add(wsName);
                p.SaveAs(new FileInfo(dir + file + ".xlsx"));
            }
            using (var p = new ExcelPackage(new FileInfo(dir + file + ".xlsx")))
            {
                string wsName = "Енергослужба_" + searchWorkTimeMonthComboBox.Text + "_" + searchWorkTimeYearComboBox.Text;
                var ws = p.Workbook.Worksheets[wsName];
                for (int i = 1; i < workTimeDataGridView.ColumnCount+1; i++)
                {
                    ws.Cells[1, i].Value = workTimeDataGridView.Columns[i - 1].HeaderText;
                    ws.Cells[1, i].Style.Font.Bold = true;
                }
                for (int i = 2; i < workTimeDataGridView.Rows.Count+1; i++)
                {
                    for (int y = 0; y < workTimeDataGridView.ColumnCount; y++)
                    {
                        ws.Cells[i, y + 1].Value = workTimeDataGridView.Rows[i - 2].Cells[y].Value;
                        if (workTimeDataGridView.Rows[i - 2].Cells[y].Style.BackColor == Color.LightGray)
                        {
                            ws.Cells[i, y + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            ws.Cells[i, y + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);   
                        }
                        ws.Cells[i, y + 1].Style.WrapText = true;
                    }

                }
                ws.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                ws.Column(1).Width = 20;
                ws.Column(2).Width = 20;
                ws.Column(3).Width = 20;
                ws.Column(4).Width = 15;

                int columnCount = ws.Dimension.Columns;
                for (int i = 5; i < ws.Dimension.Columns; i++)
                {
                    ws.Column(i).Width = 3;
                }

                for (int i = 5; i < ws.Dimension.Columns; i++)
                {
                    ws.Cells[6, i].Style.TextRotation = 90;
                    ws.Row(6).Height = 35;
                }

                for (int i = 5; i < ws.Dimension.Columns; i++)
                {
                    ws.Cells[8, i].Style.TextRotation = 90;
                    ws.Row(8).Height = 25;
                    if (ws.Cells[8, i].Text=="Vac")
                    {
                        ws.Cells[8, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[8, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                    }
                    if (ws.Cells[8, i].Text == "Wrk")
                    {
                        ws.Cells[8, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[8, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCoral);
                    }
                }

                ws.Column(ws.Dimension.Columns).Width = 15;
                for (int i = 1; i < workTimeDataGridView.Rows.Count+1; i++)
                {
                    for (int y = 1; y < workTimeDataGridView.ColumnCount; y++)
                    {
                        ws.Cells[i, y].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        ws.Cells[i, y].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        ws.Cells[i, y].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        ws.Cells[i, y].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    }
                }
                for (int i = 1; i < workTimeDataGridView.Rows.Count + 1; i++)
                {
                    ws.Cells[i, workTimeDataGridView.ColumnCount].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    ws.Cells[i, workTimeDataGridView.ColumnCount].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    ws.Cells[i, workTimeDataGridView.ColumnCount].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    ws.Cells[i, workTimeDataGridView.ColumnCount].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                }
                ws.Protection.IsProtected = true;
                ws.Protection.SetPassword("2");
                p.Save();
            }
            Cursor.Current = Cursors.Default;
        }

        //EVENTS/////////////////////////////////////////////////
        private void searchWorkTimePersonComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void searchWorkTimeYearComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void searchWorkTimeMonthComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void addWorkTimeDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void addWorkTimeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void rateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //MessageBox.Show((Convert.ToInt32(e.KeyChar)).ToString());
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number !=46)
            {
                e.Handled = true;
            }
        }

        private void addWorkTimeMultiplierComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void addWorkTimePersonComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void workShiftComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void searchWorkTimePersonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWorkTime();
            workTimeDataGridView.ClearSelection();
        }

        private void searchWorkTimeYearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWorkTime();
            workTimeDataGridView.ClearSelection();
        }

        private void searchWorkTimeMonthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWorkTime();
            workTimeDataGridView.ClearSelection();
        }

        private void addWorkTimeButton_Click(object sender, EventArgs e)
        {
            SetWorkTime();
            GetWorkTime();
        }

        private void addWorkTimeTextBox_TextChanged(object sender, EventArgs e)
        {
            if(addWorkTimeTextBox.Text=="")
            {
                addWorkTimeTextBox.Text = "0";
            }
            if(Convert.ToInt32(addWorkTimeTextBox.Text)>8)
            {
                addWorkTimeTextBox.Text = "8";
            }
        }
        private void rateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (rateTextBox.Text == "")
            {
                rateTextBox.Text = "0";
            }
            if (rateTextBox.Text == ".")
            {
                rateTextBox.Text = "0";
            }
            if (rateTextBox.Text == "0.")
            {
                rateTextBox.Text = "0";
            }
            if (rateTextBox.Text == ".0")
            {
                rateTextBox.Text = "0";
            }

            string fsvs = rateTextBox.Text;
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            fsvs = fsvs.Replace(".", decimalSeparator);
            

            if (Convert.ToDouble(fsvs) > 1000)
            {
                fsvs = "1000";
            }
            rateTextBox.Text = fsvs.Replace(",", ".");

            string tmp = "";
            tmp = rateTextBox.Text.Substring(0, 1);
            if (tmp == ",")
            {
                rateTextBox.Text = "0" + rateTextBox.Text;
            }
            tmp = rateTextBox.Text.Substring(rateTextBox.Text.Length-1, 1);
            if (tmp == ",")
            {
                rateTextBox.Text = rateTextBox.Text + "0";
            }

        }

        private void showAllPersonsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            GetWorkTime();
        }

        private void workShiftComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(workShiftComboBox.Text=="1")
            {
                nightHoursTextBox.Text = "0";
                nightHoursTextBox.Enabled = false;
            }
            else
            {
                nightHoursTextBox.Enabled = true;
            }
        }

        private void nightHoursTextBox_TextChanged(object sender, EventArgs e)
        {
            if (nightHoursTextBox.Text == "")
            {
                nightHoursTextBox.Text = "0";
            }
            if (Convert.ToInt32(nightHoursTextBox.Text) > 8)
            {
                nightHoursTextBox.Text = "8";
            }
        }

        private void nightHoursTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void additionalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWorkTime();
        }

        private void bonusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWorkTime();
        }

        private void printWorkTimeButton_Click(object sender, EventArgs e)
        {
            CreateWorkTimeTable();
        }

        private void dayTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dayTypeComboBox.Text == "Wrk")
            {
                addWorkTimeTextBox.Enabled = true;
                workShiftComboBox.Enabled = true;
                if (workShiftComboBox.Text == "2" || workShiftComboBox.Text == "3")
                {
                    nightHoursTextBox.Enabled = true;
                }
                addWorkTimeMultiplierComboBox.Enabled = true;

                SetConstant("dayType", "Wrk");
                string tmp = GetConstant("rate");
                rateTextBox.Text = GetConstant("vacRate");
                SetConstant("vacRate", rateTextBox.Text);
                rateTextBox.Text = tmp;
            }
            if (dayTypeComboBox.Text == "Vac")
            {
                addWorkTimeTextBox.Enabled = false;
                workShiftComboBox.Enabled = false;
                nightHoursTextBox.Enabled = false;
                addWorkTimeMultiplierComboBox.Enabled = false;
                SetConstant("dayType", "Vac");
                string tmp = GetConstant("vacRate");
                rateTextBox.Text = GetConstant("rate");
                SetConstant("rate", rateTextBox.Text);
                rateTextBox.Text = tmp;
            }
        }
        private void dayTypeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //WORK TIME********************************************************************************************************************************************************************/

        //BACKUP********************************************************************************************************************************************************************/
        string sourceDirectory = Environment.CurrentDirectory + "\\Data\\";
        string targetDirectory = Environment.CurrentDirectory + "\\bkp\\";


        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }















        //BACKUP********************************************************************************************************************************************************************/



        //CONTRACTS*****************************************************************************************************************************************************************/
        OleDbConnection ContractDBConnection;
        OleDbCommand ContractDBCommand;
        OleDbDataReader ContractDBReader;
        //CLASSES/////////////////////////////////////////////////
        public class Contract
        {
            public Contract()
            {

            }
        }

        //FUNCTIONS////////////////////////////////////////////////

        //EVENTS/////////////////////////////////////////////////
        private void contractObjectTypeL1ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            e.Handled = true;
        }
        private void contractObjectTypeL2ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            e.Handled = true;
        }

        private void contractActionTypeComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(Convert.ToString(e.KeyCode));
        }

        private void contractActionTypeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char number = e.KeyChar;
            e.Handled = true;
        }

        private void contractObjectTypeL1ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] items;
            contractObjectTypeL2ComboBox.Items.Clear();
            contractActionTypeComboBox.Items.Clear();
            contractObjectTypeL2ComboBox.Text = "";
            contractActionTypeComboBox.Text = "";
            switch (contractObjectTypeL1ComboBox.Text)
            {
                case "Resources": items = new string[] { "Gas", "Electricity", "Water"}; contractObjectTypeL2ComboBox.Items.AddRange(items); break;
                case "Equipment": items = new string[]{"Common Equipment"}; contractObjectTypeL2ComboBox.Items.AddRange(items); break;
                default:break;
            }

        }

        private void contractObjectTypeL2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] items;
            contractActionTypeComboBox.Items.Clear();
            contractActionTypeComboBox.Text = "";
            switch (contractObjectTypeL2ComboBox.Text)
            {
                case "Gas": items = new string[] { "Supply", "Distribution", "Service", "Research" }; contractActionTypeComboBox.Items.AddRange(items); break;
                case "Electricity": items = new string[] { "Supply", "Distribution", "Service", "Research" }; contractActionTypeComboBox.Items.AddRange(items); break;
                case "Water": items = new string[] { "Supply", "Research" }; contractActionTypeComboBox.Items.AddRange(items); break;
                case "Common Equipment": items = new string[] { "Supply", "Research", "Service" }; contractActionTypeComboBox.Items.AddRange(items); break;
                default: break;
            }
        }

 




        //CONTRACTS*****************************************************************************************************************************************************************/



    }





}

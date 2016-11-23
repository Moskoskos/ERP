using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Drawing.Printing;


namespace ERP
{
    public partial class frmMain : Form
    {
        //private string folderPath = "";
        //private string fileName = ""; //Name of the selected file.
        private int numRed = 0;
        private int numBlack = 0;
        private int numTall = 0;
        private int numTran = 0;

        private const int red = 1;   //Identifier for DB
        private const int black = 2; //Identifier for DB
        private const int tran = 3;  //Identifier for DB
        private const int tall = 4;  //Identifier for DB

        private int fillBlack = 0;
        private int fillRed = 0;
        private int fillTran = 0;
        private int fillTall = 0;

        //Minimum is 16 for small and 24 for tall.
        //Limits for how much each jar can be filled
        private string smallCupMin = "0";
        private string smallCupMax = "12";
        private string tallCupMin = "0";
        private string tallCupMax = "41";

        private int currentSelectedBatchID;

        Validation val = new Validation();
        DbConnect dcGlob = new DbConnect();

        //private string specialPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string fileReportPath = "0";
        private string fileInvoicePath = "0";

        private int latestBatchRowID = 0;

        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the form loads it will attempt to connect to the database. If successfull it will initialize the datagrids with "ReadOnly==true".
        /// The datagridviews will then be sorted. Additionally the cmbPrinters will be filled with every single installed printer(& -program) that is installed on the machine.
        /// Finally it will update the path variables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ERP_Load(object sender, EventArgs e)
        {
            ConnectToDatabase();

            //Set Datagridviews to readonly
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;

            //Default sort option for grids.
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);

            UpdateCupGrid();

            PrinterList();
            cmbPrinters.SelectedIndex = 0;
            UpdatePaths();
        }


        /// <summary>
        /// Updates currentSelectedBatchID and latestBatchRowID.
        /// </summary>
        private void UpdatePaths()
        {
            latestBatchRowID = dcGlob.GetLatestRow();
            fileReportPath = @".\Order " + currentSelectedBatchID + ".txt";
            fileInvoicePath = @".\Invoice " + latestBatchRowID + ".txt";
        }
        /// <summary>
        /// Will attempt to ping the server, then if it went OK, it will attempt to update the grids
        /// If it fails it will not attempt to update the grids and will show the reconnect button.
        /// </summary>
        private void ConnectToDatabase()
        {

            DbConnect dc = new DbConnect();
            if (dc.PingHost())
            {
                UpdateDataGridViews();
                btnReconnect.Hide();
            }
            else
            {
                MessageBox.Show("Could not find host...");
                btnReconnect.Show();
            }
        }

        /// <summary>
        /// If successfull ping, will update datagrids. 
        /// Ping might be redundant
        /// </summary>
        private void UpdateDataGridViews()
        {

            if (dcGlob.PingHost())
            {
                // TODO: This line of code loads data into the 'batchOrderDataSet.BatchOrdre' table. You can move, or remove it, as needed.
                this.batchOrdreTableAdapter.Fill(this.batchOrderDataSet.BatchOrdre);
                // TODO: This line of code loads data into the 'cupOrderDataSet.CupOrdre' table. You can move, or remove it, as needed.
                this.cupOrdreTableAdapter.Fill(this.cupOrderDataSet.CupOrdre);

                latestBatchRowID = dcGlob.GetLatestRow();
            }
            else
            {
                btnReconnect.Show();
                MessageBox.Show("Unable to retrieve data from the Database. No connection present");
            }

        }
        /// <summary>
        /// "Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed." -MSDN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Starts the default program assosiated with the .pdf extension and displays the user manual.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string help = "\\usermanual.pdf";
            try
            {
                Process.Start(help);
            }
            catch (Exception)
            {
                MessageBox.Show("File not found");
            }
        }

        /// <summary>
        /// Will check all text boxes for valid input. If succesfull, write the values to their assigned variables.
        /// It will then check if the inputs are properly filled out. One cannot order 0 cups with 4 grams, neither 4 cups with 0 grams.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int numOfCups = 0;

            //Checks that all the inputs are in a valid format before assigning values to the variables, creates the DbConnect object and summerizes.
            //Then Create the order
            //PS It aint pretty , BUT it works.

            if (val.TestIntegerInput(txtRed.Text) && val.TestIntegerInput(txtTall.Text) && val.TestIntegerInput(txtTran.Text) && val.TestIntegerInput(txtBlack.Text) &&
            val.CheckGramInput(txtFillBlack.Text, smallCupMin, smallCupMax) && val.CheckGramInput(txtFillRed.Text, smallCupMin, smallCupMax) &&
            val.CheckGramInput(txtFillTran.Text, smallCupMin, smallCupMax) && val.CheckGramInputTall(txtFillTall.Text, tallCupMin, tallCupMax))
            {

                fillBlack = Convert.ToInt32(txtFillBlack.Text);
                fillRed = Convert.ToInt32(txtFillRed.Text);
                fillTran = Convert.ToInt32(txtFillTran.Text);
                fillTall = Convert.ToInt32(txtFillTall.Text);
                numBlack = Convert.ToInt32(txtBlack.Text);
                numRed = Convert.ToInt32(txtRed.Text);
                numTall = Convert.ToInt32(txtTall.Text);
                numTran = Convert.ToInt32(txtTran.Text);

                DbConnect dc = new DbConnect();
                numOfCups = GetTotalCups();

                //Creates CupOrder data for the batchorder and checks if the inputs are corresponding.
                if (numOfCups != 0 && val.TestVadilityOfCupInput(numBlack, fillBlack) && val.TestVadilityOfCupInput(numRed, fillRed)
                    && val.TestVadilityOfCupInput(numTall, fillTall) && val.TestVadilityOfCupInput(numTran, fillTran))
                {
                    //Before queries are run, make sure that the database is responding to pings.
                    if (dcGlob.PingHost())
                    {
                        //Creates the batch order with the total number of cups with a new auto-incremented ID.
                        dc.CreateBatchOrder(numOfCups);
                        //Get number of each type of cup, their color code and how much will be filled in each type.
                        dc.InsertOrderIntoDataTable(numRed, red, fillRed);
                        dc.InsertOrderIntoDataTable(numBlack, black, fillBlack);
                        dc.InsertOrderIntoDataTable(numTran, tran, fillTran);
                        dc.InsertOrderIntoDataTable(numTall, tall, fillTall);
                        UpdatePaths();
                        GenerateInvoice();
                    }

                }
                else
                {
                    MessageBox.Show("Order empty or invalid input");
                }
            }


            UpdateDataGridViews();
            UpdateCupGrid();
        }


        /// <summary>
        /// Calls a method to try to reconnect to the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReconnect_Click(object sender, EventArgs e)
        {
            ConnectToDatabase();
        }

        /// <summary>
        /// Summerizes the values in all num(COLOR) textboxes.
        /// </summary>
        /// <returns></returns>
        private int GetTotalCups()
        {
            int sum = 0;
            sum = numRed + numBlack + numTall + numTran;
            return sum;
        }

        //Display order data as selection of batchorder is changed.
        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            UpdateCupGrid();
        }
        
        /// <summary>
        /// Will populate the cuporder datagridview with cups beloning to a spesific order.
        /// </summary>
        private void UpdateCupGrid()
        {
            try
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    currentSelectedBatchID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    this.cupOrdreTableAdapter.FillWithBatchNumber(this.cupOrderDataSet.CupOrdre, currentSelectedBatchID);
                    txtSelectedOrder.Text = currentSelectedBatchID.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lost connection");
            }
        }



        /// <summary>
        /// Method to show a tooltip over the txtFill-boxes, where minimum and maximum content is displayed.
        /// </summary>
        /// <param name="textbox"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        private void FillMessage(TextBox textbox, string min, string max)
        {
            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(textbox, "Value may be between " + min + "g and " + max + "g. Only positive real numbers allowed. Ex : 1, 2, 3, 4. etc");
        }

        /// <summary>
        /// Displays the minimum and maximum values for the selected cup type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFillBlack_MouseHover(object sender, EventArgs e)
        {
            FillMessage(txtFillBlack, smallCupMin, smallCupMax);
        }

        /// <summary>
        /// Displays the minimum and maximum values for the selected cup type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFillRed_MouseHover(object sender, EventArgs e)
        {
            FillMessage(txtFillRed, smallCupMin, smallCupMax);
        }

        /// <summary>
        /// Displays the minimum and maximum values for the selected cup type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFillTall_MouseHover(object sender, EventArgs e)
        {
            FillMessage(txtFillTall, tallCupMin, tallCupMax);
        }

        /// <summary>
        /// Displays the minimum and maximum values for the selected cup type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFillTran_MouseHover(object sender, EventArgs e)
        {
            FillMessage(txtFillTran, smallCupMin, smallCupMax);
        }

        /// <summary>
        /// Populates the combobox with the names of all printers installed on the computer.
        /// </summary>
        private void PrinterList()
        {
            foreach (string sPrinters in PrinterSettings.InstalledPrinters)
            {
                cmbPrinters.Items.Add(sPrinters);
            }
        }

        /// <summary>
        /// Creates datatables which are filled with vital information to generate a report.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInvoice_Click(object sender, EventArgs e)
        {
            DataTable cupTable = cupOrderDataSet.CupOrdre;
            DataTable batchTable = GetBatchOrderColumnNames();
            DataTable cupHeaders = GetCupOrderColumnNames();
            int batchLength = dataGridView1.Columns.Count;

            //Creates an object of class ReportGeneration.
            ReportGeneration rg = new ReportGeneration();
            //Makes sure that the paths are updated before data collection is processed.
            UpdatePaths();
            //Send information to method Generate()
            rg.Generate(batchTable, cupTable, cupHeaders, GetDGVBatchRoWValues(batchLength), fileReportPath);
            PrintDocument(fileReportPath);
        }

        /// <summary>
        /// Delcares the printing class, gets the current selected printer and file which is to be sent to it.
        /// </summary>
        /// <param name="file">path to file and the name of it.</param>
        private void PrintDocument(string file)
        {
            Printing pr = new Printing();
            string selectedPrinter = "";
            selectedPrinter = cmbPrinters.GetItemText(this.cmbPrinters.SelectedItem);
            pr.PrintDocument(selectedPrinter, file);
            
        }

        /// <summary>
        /// Iterates between every single cell in a selected row and gets its values. 
        /// </summary>
        /// <param name="numOfColumns"></param>
        /// <returns></returns>
        private string[] GetDGVBatchRoWValues(int numOfColumns)
        {
            
            string[] cellValues = new string[numOfColumns];

            for (int i = 0; i < cellValues.Length; i++)
            {
                if (dataGridView1[i, dataGridView1.CurrentRow.Index].Value.ToString() != null)
                {
                    cellValues[i] = dataGridView1[i, dataGridView1.CurrentRow.Index].Value.ToString();
                }

            }
            return cellValues;
        }

        /// <summary>
        /// Gets the column name of each column in the datagridview for batch orders.
        /// </summary>
        /// <returns></returns>
        private DataTable GetBatchOrderColumnNames()
        {

            DataTable dt = new DataTable() ;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
                dt.Columns.Add(column.HeaderText); //better to have cell type

            return dt;
        }

        /// <summary>
        /// Gets the column name of each column in the datagridview for cup data.
        /// </summary>
        /// <returns></returns>
        private DataTable GetCupOrderColumnNames()
        {
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn column in dataGridView2.Columns)
                dt.Columns.Add(column.HeaderText); //better to have cell type

            return dt;
        }

        /// <summary>
        /// Generates a invoice.
        /// </summary>
        private void GenerateInvoice()
        {
            UpdatePaths();
            using (StreamWriter sw = new StreamWriter(fileInvoicePath, false))
            {

                sw.Write("--- BATCH ORDER INVOICE ---");
                sw.WriteLine();
                sw.WriteLine();

                
                sw.Write(dataGridView1.Columns[0].HeaderText);
                sw.WriteLine();
                sw.Write(latestBatchRowID);
                sw.WriteLine();

                sw.Write(dataGridView1.Columns[1].HeaderText);
                sw.WriteLine();
                sw.Write(GetTotalCups());
                sw.WriteLine();
                sw.WriteLine();

                sw.Write("Red Cups\t\t" + numRed);
                sw.WriteLine();
                sw.Write("Weight\t\t\t" + fillRed + "g");
                sw.WriteLine();
                sw.WriteLine();

                sw.Write("Black Cups\t\t" + numBlack);
                sw.WriteLine();
                sw.Write("Weight\t\t\t" + fillBlack + "g");
                sw.WriteLine();
                sw.WriteLine();

                sw.Write("Tall Cups\t\t" + numTall);
                sw.WriteLine();
                sw.Write("Weight\t\t\t" + fillTall + "g");
                sw.WriteLine();
                sw.WriteLine();

                sw.Write("Transparent Cups\t" + numTran);
                sw.WriteLine();
                sw.Write("Weight\t\t\t" + fillTran + "g");
                sw.WriteLine();
                sw.WriteLine();

                sw.Write("----- ORDER CREATED -----");
                sw.WriteLine();
                sw.WriteLine(DateTime.Now);

            }
            PrintDocument(fileInvoicePath);
        }

        /// <summary>
        /// If activated. Will test input in txtSearch, then do another test to see if its not null or whitespace.
        /// If everything checks out, run query to find spesified batchOrder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                int search = 0;
                if (val.TestIntegerInput(txtSearch.Text))
                {
                    search = Convert.ToInt32(txtSearch.Text);
                    if (String.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        MessageBox.Show("Please insert a value");
                    }
                    else
                    {
                        this.batchOrdreTableAdapter.FillSpesificRow(this.batchOrderDataSet.BatchOrdre, search);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Input");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateDataGridViews();

        }


    }
}

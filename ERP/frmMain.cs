using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Media;
using Microsoft.Win32;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
 



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

        private const int black = 1; //Identifier for DB
        private const int red = 2;   //Identifier for DB
        private const int tran = 3;  //Identifier for DB
        private const int tall = 4;  //Identifier for DB

        private int fillBlack = 0;
        private int fillRed = 0;
        private int fillTran = 0;
        private int fillTall = 0;

        //Minimum is 16 for small and 24 for tall.
        //Limits for how much each jar can be filled
        private string smallCupMin = "0";
        private string smallCupMax = "28";
        private string tallCupMin = "0";
        private string tallCupMax = "65";

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

            fileReportPath = @".\Order " + currentSelectedBatchID + ".txt";
            fileInvoicePath = @".\Invoice " + currentSelectedBatchID + ".txt";

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
        /// Ping mightbe redundant
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
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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
        /// It will then check if the inputs are properly filled out. One cannot order 0 cups with 4 grams,
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int numOfCups = 0;

            //Checks that all the inputs are in a valid format before assigning values to the variables, creates the DbConnect object and summerizes.
            //Then Create the order
            //PS I'm not proud of this , BUT it works.

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
                    dc.CreateBatchOrder(numOfCups);
                    dc.InsertOrderIntoDataTable(numBlack, black, fillBlack);
                    dc.InsertOrderIntoDataTable(numRed, red, fillRed);
                    dc.InsertOrderIntoDataTable(numTall, tall, fillTall);
                    dc.InsertOrderIntoDataTable(numTran, tran, fillTran);
                    GenerateInvoice();
                }
            }
            else
            {
                MessageBox.Show("Order empty or invalid input");
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


        //Method to show a tooltip over the txtFill-boxes, where minimum and maximum content is displayed.
        private void FillMessage(TextBox textbox, string min, string max)
        {
            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(textbox, "Value may be between " + min + "g and " + max + "g. Only positive real numbers allowed. Ex : 1, 2, 3, 4. etc");
        }

        private void txtFillBlack_MouseHover(object sender, EventArgs e)
        {
            FillMessage(txtFillBlack, smallCupMin, smallCupMax);
        }

        private void txtFillRed_MouseHover(object sender, EventArgs e)
        {
            FillMessage(txtFillRed, smallCupMin, smallCupMax);
        }

        private void txtFillTall_MouseHover(object sender, EventArgs e)
        {
            FillMessage(txtFillTall, tallCupMin, tallCupMax);
        }

        private void txtFillTran_MouseHover(object sender, EventArgs e)
        {
            FillMessage(txtFillTran, smallCupMin, smallCupMax);
        }
        private void PrinterList()
        {
            // POPULATE THE COMBO BOX.
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

            ReportGeneration rg = new ReportGeneration();
            

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
            using (StreamWriter sw = new StreamWriter(fileInvoicePath, false))
            {

                sw.Write("--- BATCH ORDER INVOICE ---");
                sw.WriteLine();
                sw.WriteLine();

                
                sw.Write(dataGridView1.Columns[0].HeaderText);
                sw.WriteLine();
                sw.Write(currentSelectedBatchID);
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
            

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
                UpdateDataGridViews();
        }
    }
}

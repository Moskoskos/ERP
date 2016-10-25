﻿using System;
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

        private string smallCupMin = "1";
        private string smallCupMax = "100";
        private string tallCupMin = "1";
        private string tallCupMax = "150";

        private int currentSelectedBatchID;

        Validation val = new Validation();
        DbConnect dbGlob = new DbConnect();

        //private string specialPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string fileReportPath = "0";
        private string fileInvoicePath = "0";




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

        private void UpdateDataGridViews()
        {

            // TODO: This line of code loads data into the 'batchOrderDataSet.BatchOrdre' table. You can move, or remove it, as needed.
            this.batchOrdreTableAdapter.Fill(this.batchOrderDataSet.BatchOrdre);
            // TODO: This line of code loads data into the 'cupOrderDataSet.CupOrdre' table. You can move, or remove it, as needed.
            this.cupOrdreTableAdapter.Fill(this.cupOrderDataSet.CupOrdre);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string help = "\\help.pdf";
            try
            {
                Process.Start(help);
            }
            catch (Exception)
            {
                MessageBox.Show("File not found");
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int numOfCups = 0;

            //Checks that all the inputs are in a valid format before assigning values to the variables, creates the DbConnect object and summerizes.
            //Then Create the order
            //PS I'm not proud of this , BUT it works.
            if (val.TestIntegerInput(txtBlack.Text) && val.TestIntegerInput(txtRed.Text) && val.TestIntegerInput(txtTall.Text) && val.TestIntegerInput(txtTran.Text) &&
                val.CheckGramInput(txtFillBlack.Text, smallCupMin, smallCupMax) && val.CheckGramInput(txtFillRed.Text, smallCupMin, smallCupMax) && 
                val.CheckGramInput(txtFillTran.Text, smallCupMin, smallCupMax) && val.CheckGramInputTall(txtTall.Text, tallCupMin, tallCupMax))
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

                //Creates the Batchorder
                if (numOfCups != 0)
                {
                    dc.CreateBatchOrder(numOfCups);

                    //Creates CupOrder data for the batchorder
                    if (numBlack != 0)
                    {
                        dc.InsertOrderIntoDataTable(numBlack, black, fillBlack);
                    }
                    if (numRed != 0)
                    {
                        dc.InsertOrderIntoDataTable(numRed, red, fillRed);
                    }
                    if (numTall != 0)
                    {
                        dc.InsertOrderIntoDataTable(numTall, tall, fillTall);
                    }
                    if (numTran != 0)
                    {
                        dc.InsertOrderIntoDataTable(numTran, tran, fillTran);
                    }
                    InvoiceGeneration();
                }
            }
            else
            {
                MessageBox.Show("Order empty or invalid input");
            }

            UpdateDataGridViews();
            UpdateCupGrid();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

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

        private void btnPrevious_Click(object sender, EventArgs e)
        {
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }

        //Method to show a tooltip over the txtFill-boxes, where minimum and maximum content is displayed.
        private void FillMessage(TextBox textbox, string min, string max)
        {
            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(textbox, "Value may be between " + min + "g and " + max + "g.");
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

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            DataTable cupTable = cupOrderDataSet.CupOrdre;
            DataTable batchTable = GetBatchOrderColumnNames();
            DataTable cupHeaders = GetCupOrderColumnNames();
            int batchLength = dataGridView1.Columns.Count;

            ReportGeneration rg = new ReportGeneration();
            

            rg.Generate(batchTable, cupTable, cupHeaders, GetDGVBatchRoWValues(batchLength), fileReportPath);
            PrintDocument();
        }

        private void PrintDocument()
        {
            Printing pr = new Printing();
            string selectedPrinter = "";
            selectedPrinter = cmbPrinters.GetItemText(this.cmbPrinters.SelectedItem);
            pr.PrintDocument(selectedPrinter, fileReportPath);
            
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

        private DataTable GetBatchOrderColumnNames()
        {

            DataTable dt = new DataTable() ;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
                dt.Columns.Add(column.HeaderText); //better to have cell type

            return dt;
        }
        private DataTable GetCupOrderColumnNames()
        {
            DataTable dt = new DataTable();

            foreach (DataGridViewColumn column in dataGridView2.Columns)
                dt.Columns.Add(column.HeaderText); //better to have cell type

            return dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            int search = 0;
            search = Convert.ToInt32(txtSearch.Text);
            this.batchOrdreTableAdapter.FillSpesificRow(this.batchOrderDataSet.BatchOrdre, search);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
               
                if (String.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    UpdateDataGridViews();
                }
            }
        }


        //------------------------------------------------------------------------TESTING AREA----------------------------------------------------------------------------//

        private void InvoiceGeneration()
        {
            using (StreamWriter sw = new StreamWriter(fileInvoicePath, false))
            {
                DbConnect dc = new DbConnect();

                sw.Write("------------------------------ BATCH ORDER INVOICE --------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.Write("Batch Numnber from dgv");
                sw.Write(dc.batchid);
                sw.WriteLine();
                sw.Write("Total Cups from dgv");
                sw.Write(GetTotalCups());
                sw.WriteLine();
                sw.WriteLine();
                sw.Write("Red Cups");
                sw.Write(numRed);
                sw.WriteLine();
                sw.Write("Black Cups");
                sw.Write(numBlack);
                sw.WriteLine();
                sw.Write("Tall Cups");
                sw.Write(numTall);
                sw.WriteLine();
                sw.Write("Transparent Cups");
                sw.Write(numTran);
                sw.WriteLine();
                sw.WriteLine(DateTime.Now);

            }
        }

    }
}

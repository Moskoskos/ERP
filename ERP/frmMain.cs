﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.io;
using iTextSharp.text.xml;
using iTextSharp.text.pdf;
using iTextSharp.text.api;
using iTextSharp.text.log;
using iTextSharp.text.error_messages;
using iTextSharp.testutils;
using iTextSharp.xmp;
using System.Web;
using System.Web.Configuration;
using System.Web.Management;
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

        Validation val = new Validation();

        


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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings frmsettings = new frmSettings();
            frmsettings.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string help = "\\help.pdf";
            try
            {
                System.Diagnostics.Process.Start(help);
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
            //PS I'm not proud of this 
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
                }
            }
            else
            {
                MessageBox.Show("Order empty or invalid input");
            }

            UpdateDataGridViews();
            UpdateCupGrid();
        }

        //Disables the fill box if selected index = 0


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
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var index = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                this.cupOrdreTableAdapter.FillWithBatchNumber(this.cupOrderDataSet.CupOrdre, index);
            }
        }



        private void btnPrevious_Click(object sender, EventArgs e)
        {
            GetValuesFromGridView();
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
            foreach (string sPrinters in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbPrinters.Items.Add(sPrinters);
            }

        }

        //-------------------------------------------------------TESTING AREA----------------------------------------------------------------------------

        private void GetValuesFromGridView()
        {

        }

        //Source: http://stackoverflow.com/questions/7174077/export-a-c-sharp-dataset-to-a-text-file
        static void Write(DataTable dt)
        {
            string filePath = @".\Orders\Order.txt";
            int[] maxLengths = new int[dt.Columns.Count];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                maxLengths[i] = dt.Columns[i].ColumnName.Length;

                foreach (DataRow row in dt.Rows)
                {
                    if (!row.IsNull(i))
                    {
                        int length = row[i].ToString().Length;

                        if (length > maxLengths[i])
                        {
                            maxLengths[i] = length;
                        }
                    }
                }
            }

            
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName.PadRight(maxLengths[i] + 2));
                }

                sw.WriteLine();

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!row.IsNull(i))
                        {
                            sw.Write(row[i].ToString().PadRight(maxLengths[i] + 2));
                        }
                        else
                        {
                            sw.Write(new string(' ', maxLengths[i] + 2));
                        }
                    }

                    sw.WriteLine();
                }

                sw.Close();
            }
        }
        public void WriteDataToFile(DataTable submittedDataTable)
        {
            int arraysize = dataGridView1.ColumnCount;

            string[] batchOrder = new string[arraysize];
            string[] headerRowTextArray = new string[arraysize];

            int j = 0;
            StreamWriter sw = null;
            string filePath = @".\test.txt";

            sw = new StreamWriter(filePath, false);

            for (int i = 0; i < arraysize; i++)
            {
                headerRowTextArray[i] = dataGridView1.Columns[i].HeaderCell.Value.ToString();
                batchOrder[i] = dataGridView1[i, dataGridView1.CurrentRow.Index].Value.ToString();

            }

            for (j = 0; j < submittedDataTable.Columns.Count - 1; j++)
            {

                sw.Write(submittedDataTable.Columns[j].ColumnName + ";");

            }
            sw.Write(submittedDataTable.Columns[j].ColumnName);
            sw.WriteLine();

            foreach (DataRow row in submittedDataTable.Rows)
            {
                object[] array = row.ItemArray;

                for (j = 0; j < array.Length - 1; j++)
                {
                    sw.Write(array[j].ToString() + ";");
                }
                sw.Write(array[j].ToString());
                sw.WriteLine();

            }
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            DataTable  cupTable = cupOrderDataSet.CupOrdre;
            Write(cupTable);
        }
    }
}

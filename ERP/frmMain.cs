using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
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

        //private string specialPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string filePath = "0";




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

            filePath = @".\Order_" + currentSelectedBatchID + ".txt";

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
            foreach (string sPrinters in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbPrinters.Items.Add(sPrinters);
            }

        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            DataTable cupTable = cupOrderDataSet.CupOrdre;
            DataTable batchTable = GetSelectedBatchOrder();
            Write(batchTable, cupTable);
            PrintDocument();
        }

        



        //Source: http://stackoverflow.com/questions/7174077/export-a-c-sharp-dataset-to-a-text-file
        private void Write(DataTable dtb,DataTable dtc)
        {
          
            
            int batchLength = dtb.Columns.Count;
            string[] cellValues = new string[batchLength];
            for (int i = 0; i < cellValues.Length; i++)
            {
                if (dataGridView1[i, dataGridView1.CurrentRow.Index].Value.ToString() != null)
                {
                    cellValues[i] = dataGridView1[i, dataGridView1.CurrentRow.Index].Value.ToString();
                }

            }

            //----------------------------------------------------THIS IS FOR THE BATCH ORDER DATA---------------------------------------------//  
            //Count columns in datatable
            int[] maxLengthsDtb = new int[dtb.Columns.Count];

            //Create spacing between columns in txt file
            for (int i = 0; i < dtb.Columns.Count; i++)
            {
                maxLengthsDtb[i] = dtb.Columns[i].ColumnName.Length;

                    if (cellValues[i] != null)
                    {
                        int length = cellValues[i].ToString().Length;

                        if (length > maxLengthsDtb[i])
                        {
                            maxLengthsDtb[i] = length;
                        }
                    }
                
            }

            //WRITE TO FILE
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                
                sw.Write("------------------------------------------ORDER DETAILS------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();

                for (int i = 0; i < dtb.Columns.Count; i++)
                {
                    sw.Write(dtb.Columns[i].ColumnName.PadRight(maxLengthsDtb[i] + 2));
                }

                sw.WriteLine();

                
                    for (int i = 0; i < dtb.Columns.Count; i++)
                    {
                        if (cellValues[i] != null)
                        {
                            sw.Write(cellValues[i].ToString().PadRight(maxLengthsDtb[i] + 2));
                        }
                        else
                        {
                            sw.Write(new string(' ', maxLengthsDtb[i] + 2));
                        }
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.Write("--------------------------------------- BATCH ORDER DETAILS---------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                //-------------------------------------------THIS IS FOR THE CUP ORDER DATA-------------------------------------//

                int[] maxLengthsDtc = new int[dtc.Columns.Count];

                //Create spacing between columns in txt file
                for (int i = 0; i < dtc.Columns.Count; i++)
                {
                    maxLengthsDtc[i] = dtc.Columns[i].ColumnName.Length;

                    foreach (DataRow row in dtc.Rows)
                    {
                        if (!row.IsNull(i))
                        {
                            int length = row[i].ToString().Length;

                            if (length > maxLengthsDtc[i])
                            {
                                maxLengthsDtc[i] = length;
                            }
                        }
                    }
                }
                for (int i = 0; i < dtc.Columns.Count; i++)
                {
                    sw.Write(dtc.Columns[i].ColumnName.PadRight(maxLengthsDtc[i] + 2));
                }

                sw.WriteLine();

                foreach (DataRow row in dtc.Rows)
                {
                    for (int i = 0; i < dtc.Columns.Count; i++)
                    {
                        if (!row.IsNull(i))
                        {
                            sw.Write(row[i].ToString().PadRight(maxLengthsDtc[i] + 2));
                        }
                        else
                        {
                            sw.Write(new string(' ', maxLengthsDtc[i] + 2));
                        }
                    }

                    sw.WriteLine();
                }

                sw.Close();
            }
        }
       



        private DataTable GetSelectedBatchOrder()
        {

            DataTable dt = new DataTable() ;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
                dt.Columns.Add(column.HeaderText); //better to have cell type

            return dt;
        }
        //------------------------------------------------------------------------TESTING AREA----------------------------------------------------------------------------//

        //Source: http://stackoverflow.com/questions/1097005/how-to-set-printer-settings-while-printing-pdf
        private void PrintDocument()
        {
            //ProcessStartInfo info = new ProcessStartInfo(filePath.Trim());
            //info.Verb = "Print";

            //info.CreateNoWindow = true;
            //info.WindowStyle = ProcessWindowStyle.Hidden;
            //Process.Start(info);

            string printer = cmbPrinters.GetItemText(this.cmbPrinters.SelectedItem);

            PrintDocument pdoc = new PrintDocument();

            pdoc.DefaultPageSettings.PrinterSettings.PrinterName = printer;
            pdoc.DefaultPageSettings.Landscape = true;
            bool s = pdoc.DefaultPageSettings.Landscape;
            bool sa = s;


            ProcessStartInfo info = new ProcessStartInfo(filePath.Trim());
                    info.Arguments = "\"" + printer + "\"";
                    info.CreateNoWindow = true;
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    info.UseShellExecute = true;
                    info.Verb = "PrintTo";
                    Process.Start(info);
                
            
        }
    }
}

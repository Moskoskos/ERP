using System;
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


        public frmMain()
        {
            InitializeComponent();
        }

        private void ERP_Load(object sender, EventArgs e)
        {
            ConnectToDatabase();

            //Disable fill comboboxes
            cmbFillRed.Enabled = false;
            cmbFillBlack.Enabled = false;
            cmbFillLargeBlack.Enabled = false;
            cmbFillTran.Enabled = false;

            //Set Datagridviews to readonly
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;

            //Set selected indexes to 0;
            cmbFillRed.SelectedIndex = 0;
            cmbFillBlack.SelectedIndex = 0;
            cmbFillLargeBlack.SelectedIndex = 0;
            cmbFillTran.SelectedIndex = 0;
            cmbRed.SelectedIndex = 0;
            cmbBlack.SelectedIndex = 0;
            CmbLargeBlack.SelectedIndex = 0;
            cmbTran.SelectedIndex = 0;

            //Default sort option for grids.
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);


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
                MessageBox.Show("Could not connect to host...");
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

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrint frmprint = new frmPrint();
            frmprint.Show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int numOfCups = 0;
            DbConnect dc = new DbConnect();
            numOfCups = GetTotalCups();
            dc.CreateBatchOrder(numOfCups);

            fillBlack = Convert.ToInt32(cmbFillBlack.SelectedValue);
            if(fillBlack != 0) { dc.InsertOrderIntoDataTable(numBlack, black, fillBlack); }

            fillRed = Convert.ToInt32(cmbFillRed.SelectedValue);
            if(fillRed != 0) { dc.InsertOrderIntoDataTable(numRed, red, fillRed); }

            fillTran = Convert.ToInt32(cmbFillLargeBlack.SelectedValue);
            if(fillTran != 0) { dc.InsertOrderIntoDataTable(numTran, tran, fillTran); }

            fillBlack = Convert.ToInt32(cmbFillTran.SelectedValue);
            if(fillTall != 0) { dc.InsertOrderIntoDataTable(numTall, tall, fillTall); }
            
            dc.DataTableToDB();

            UpdateDataGridViews();
        }


        //Disables the fill box if selected index = 0
        private void cmbRed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRed.SelectedIndex != 0)
            {
                cmbFillRed.Enabled = true;
                numRed = cmbRed.SelectedIndex;
            }
            else
            {
                cmbFillRed.Enabled = false;
            }
        }

        //Disables the fill box if selected index = 0
        private void cmbBlack_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbBlack.SelectedIndex != 0)
            {
                cmbFillBlack.Enabled = true;
                numBlack = cmbBlack.SelectedIndex;
            }
            else
            {
                cmbFillBlack.Enabled = false;
            }
        }

        //Disables the fill box if selected index = 0
        private void CmbLargeBlack_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (CmbLargeBlack.SelectedIndex != 0)
            {
                cmbFillLargeBlack.Enabled = true;
                numTall = CmbLargeBlack.SelectedIndex;
            }
            else
            {
                cmbFillLargeBlack.Enabled = false;
            }
        }

        //Disables the fill box if selected index = 0
        private void cmbTran_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbTran.SelectedIndex != 0)
            {
                cmbFillTran.Enabled = true;
                numTran = cmbTran.SelectedIndex;
            }
            else
            {
                cmbFillTran.Enabled = false;
            }
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

        //|    TYPE      |   FILLLEVEL    |
        //|     RED      |      20        |
        //|     RED      |      20        |
        //|    BLACK     |      40        |
        //|     TALL     |      80        |
        //|_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _|


        //Display order data as selection of batchorder is changed.
        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var index = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                this.cupOrdreTableAdapter.FillWithBatchNumber(this.cupOrderDataSet.CupOrdre, index);
            }
        }
    }
}

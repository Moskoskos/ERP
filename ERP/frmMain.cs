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
        private int red = 0;
        private int black = 0;
        private int tall = 0;
        private int tran = 0;


        public frmMain()
        {
            InitializeComponent();
        }

        private void ERP_Load(object sender, EventArgs e)
        {
            ConnectToDatabase();

            cmbFillRed.Enabled = false;
            cmbFillBlack.Enabled = false;
            cmbFillLargeBlack.Enabled = false;
            cmbFillTran.Enabled = false;
            dataGridView1.ReadOnly = true;
            cmbFillRed.SelectedIndex = 0;
            cmbFillBlack.SelectedIndex = 0;
            cmbFillLargeBlack.SelectedIndex = 0;
            cmbFillTran.SelectedIndex = 0;
            cmbRed.SelectedIndex = 0;
            cmbBlack.SelectedIndex = 0;
            CmbLargeBlack.SelectedIndex = 0;
            cmbTran.SelectedIndex = 0;
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);
            //foreach (Control ctrl in Controls)
            //{
            //    if (ctrl is ComboBox)
            //    {
            //        ((ComboBox)ctrl).SelectedIndex = 0;
            //    }
            //}

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
            int totalCups = 0;
            totalCups = GetTotalCups();
        }

        //Disables the fill box if selected index = 0
        private void cmbRed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRed.SelectedIndex != 0)
            {
                cmbFillRed.Enabled = true;
                red = cmbRed.SelectedIndex;
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
                black = cmbBlack.SelectedIndex;
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
                tall = CmbLargeBlack.SelectedIndex;
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
                tran = cmbTran.SelectedIndex;
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
            sum = red + black + tall + tran;
            return sum;
        }

        private void InsertOrderIntoDataSet()
        {
            
        }

        //Write Order to DB
        private void btnBATCHORDER_TEST_Click(object sender, EventArgs e)
        {
            int numOfCups = 0;
            DbConnect dc = new DbConnect();
            numOfCups =  GetTotalCups();
            dc.CreateBatchOrder(numOfCups);
            UpdateDataGridViews();
        }

        //private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    int selectedRow = 0;
        //    selectedRow = dataGridView1.CurrentCell.RowIndex;
        //    int batchID = 
            
        //}


        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                var index = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                txtBATCHIDFORCELL.Text = index.ToString();
                this.cupOrdreTableAdapter.FillWithBatchNumber(this.cupOrderDataSet.CupOrdre, index);
            }
        }



    }
}

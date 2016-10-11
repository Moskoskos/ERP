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


namespace ERP
{
    public partial class frmMain : Form
    {
        //private string folderPath = "";
        //private string fileName = ""; //Name of the selected file.
        private string optionsTxtFile = ""; //Name of the options.txt file.
        private string[] optionsArray;

        public frmMain()
        {
            InitializeComponent();
            optionsArray = new string[5] { "", "", "", "", "" };
        }

        private void ERP_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cupOrderDataSet.CupOrdre' table. You can move, or remove it, as needed.
            this.cupOrdreTableAdapter.Fill(this.cupOrderDataSet.CupOrdre);
            // TODO: This line of code loads data into the 'batchOrderDataSet.BatchOrdre' table. You can move, or remove it, as needed.
            this.batchOrdreTableAdapter.Fill(this.batchOrderDataSet.BatchOrdre);
            try
            {
                //Finds the default system folder for application data.
                var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                //States the specifed application folder for the application.
                var pathWithName = systemPath + @"\ERP";
                //States the specified application options file.
                var optionFile = pathWithName + "options.txt";
                //In-between-storage of content for optionFile
                optionsTxtFile = optionFile;
                //Check if options file is created.
                if (File.Exists(optionFile))
                {
                    //Reads settings from file.
                    //Checks whether options have file has been initialized, if so, the program gets the path of the monitored folder.
                    if (optionsArray[4] == "1")
                    { 


                    }
                }
                else
                {
                    //if the options file doesnt exists, create file and create default settings.
                    Directory.CreateDirectory(pathWithName);
                    File.WriteAllText(optionFile, ("0\r\n" + "0\r\n" + "0\r\n" + "0\r\n" + "0\r\n"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cmbFillRed.Enabled = false;
            cmbFillBlack.Enabled = false;
            cmbFillLargeBlack.Enabled = false;
            cmbFillTran.Enabled = false;
            dataGridView1.ReadOnly = true;
            //cmbFillRed.SelectedIndex = 0;
            //cmbFillBlack.SelectedIndex = 0;
            //cmbFillLargeBlack.SelectedIndex = 0;
            //cmbFillTran.SelectedIndex = 0;
            //cmbRed.SelectedIndex = 0;
            //cmbBlack.SelectedIndex = 0;
            //CmbLargeBlack.SelectedIndex = 0;
            //cmbTran.SelectedIndex = 0;

            foreach (Control ctrl in Controls)
            {
                if (ctrl is ComboBox)
                {
                    ((ComboBox)ctrl).SelectedIndex = 0;
                }
            }

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            

            string rowCount = dataGridView1.RowCount.ToString();
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

        }
        private void cmbRed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRed.SelectedIndex != 0)
            {
                cmbFillRed.Enabled = true;
            }
            else
            {
                cmbFillRed.Enabled = false;
            }
        }

        private void cmbBlack_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbBlack.SelectedIndex != 0)
            {
                cmbFillBlack.Enabled = true;
            }
            else
            {
                cmbFillBlack.Enabled = false;
            }
        }

        private void CmbLargeBlack_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (CmbLargeBlack.SelectedIndex != 0)
            {
                cmbFillLargeBlack.Enabled = true;
            }
            else
            {
                cmbFillLargeBlack.Enabled = false;
            }
        }

        private void cmbTran_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbTran.SelectedIndex != 0)
            {
                cmbFillTran.Enabled = true;
            }
            else
            {
                cmbFillTran.Enabled = false;
            }
        }
    }
}

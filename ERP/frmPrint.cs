using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Data.EntityClient;
using System.DirectoryServices;
using System.Collections;


namespace ERP
{
    public partial class frmPrint : Form
    {
        public frmPrint()
        {
            InitializeComponent();
        }

        private void PrinterList()
        {
            // POPULATE THE COMBO BOX.
            foreach (string sPrinters in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbPrinterList.Items.Add(sPrinters);
            }

        }
        private void frmPrint_Load(object sender, EventArgs e)
        {
            PrinterList();
            cmbPrinterList.SelectedIndex = 0;
        }
    }
}

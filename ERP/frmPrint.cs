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

        private void button1_Click(object sender, EventArgs e)
        {
            PrinterList();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            PrinterListNetwork();
        }
        private void PrinterListNetwork()
        {
            // USING WMI. (WINDOWS MANAGEMENT INSTRUMENTATION)

            System.Management.ManagementScope objMS =
                new System.Management.ManagementScope(ManagementPath.DefaultPath);
            objMS.Connect();

            SelectQuery objQuery = new SelectQuery("SELECT * FROM Win32_Printer");
            ManagementObjectSearcher objMOS = new ManagementObjectSearcher(objMS, objQuery);
            System.Management.ManagementObjectCollection objMOC = objMOS.Get();

            foreach (ManagementObject Printers in objMOC)
            {
                if (Convert.ToBoolean(Printers["Local"]))       // LOCAL PRINTERS.
                {
                    cmbLocalPrinters.Items.Add(Printers["Name"]);
                }
                if (Convert.ToBoolean(Printers["Network"]))     // ALL NETWORK PRINTERS.
                {
                    cmbNetworkPrinters.Items.Add(Printers["Name"]);
                }
            }
        }

        private void btnNetwork_Click(object sender, EventArgs e)
        {
            ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath);
            objScope.Connect();

            SelectQuery selectQuery = new SelectQuery();
            selectQuery.QueryString = "Select * from win32_Printer";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject mo in MOC)
            {
                listBox1.Items.Add(mo["Name"].ToString().ToUpper());
            }

        }
    }
}

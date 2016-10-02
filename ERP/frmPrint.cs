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



namespace ERP
{
    public partial class frmPrint : Form
    {
        public frmPrint()
        {
            InitializeComponent();
        }

        private void btnGetPrinters_Click(object sender, EventArgs e)
        {
            // Use the ObjectQuery to get the list of configured printers
            System.Management.ObjectQuery oquery =
                new System.Management.ObjectQuery("SELECT * FROM Win32_Printer");

            System.Management.ManagementObjectSearcher mosearcher =
                new System.Management.ManagementObjectSearcher(oquery);

            System.Management.ManagementObjectCollection moc = mosearcher.Get();

            foreach (ManagementObject mo in moc)
            {
                System.Management.PropertyDataCollection pdc = mo.Properties;
                foreach (System.Management.PropertyData pd in pdc)
                {
                    if ((bool)mo["Network"])
                    {
                        cmbPrinters.Items.Add(mo[pd.Name]);
                    }
                }
            }
        }
    }
}

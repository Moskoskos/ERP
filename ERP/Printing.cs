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
namespace ERP
{
    public class Printing
    {
        public Printing()
        {


        }
        //Source: http://stackoverflow.com/questions/1097005/how-to-set-printer-settings-while-printing-pdf
        public void PrintDocument(string printer, string filePath)
        {
            PrintDocument pdoc = new PrintDocument();
            pdoc.DefaultPageSettings.PrinterSettings.PrinterName = printer;

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

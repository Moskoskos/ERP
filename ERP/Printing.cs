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
            //ProcessStartInfo info = new ProcessStartInfo(filePath.Trim());
            //info.Verb = "Print";

            //info.CreateNoWindow = true;
            //info.WindowStyle = ProcessWindowStyle.Hidden;
            //Process.Start(info);


            PrintDocument pdoc = new PrintDocument();

            //PaperSize ps = new PaperSize("Custom", 100, 100);



            pdoc.DefaultPageSettings.PrinterSettings.PrinterName = printer;

            //pdoc.DefaultPageSettings.PaperSize.Height = 104;
            //pdoc.DefaultPageSettings.PaperSize.Width = 140;


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

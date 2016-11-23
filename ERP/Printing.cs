using System.Diagnostics;
using System.Drawing.Printing;

namespace ERP
{
    public class Printing
    {
        public Printing()
        {


        }
        /// <summary>
        /// Constructs a PrintDocument and inserts which printer is to be used, the fieldpath for the file. Will start a process to print.
        /// </summary>
        /// <param name="printer"></param>
        /// <param name="filePath"></param>
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

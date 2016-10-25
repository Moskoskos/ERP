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
    public class ReportGeneration
    {
        public ReportGeneration()
        {

        }

        //Source: http://stackoverflow.com/questions/7174077/export-a-c-sharp-dataset-to-a-text-file
        public void Write(DataTable dtBatchOrderHeaders, DataTable dtCupOrderData, DataTable dtCupOrderHeaders, string[] cellValues, string filePath)
        {


            //----------------------------------------------------THIS IS FOR THE BATCH ORDER DATA---------------------------------------------//  
            //Count columns in datatable
            int[] maxLengthsDtb = new int[dtBatchOrderHeaders.Columns.Count];

            //Create spacing between columns in txt file
            for (int i = 0; i < dtBatchOrderHeaders.Columns.Count; i++)
            {
                maxLengthsDtb[i] = dtBatchOrderHeaders.Columns[i].ColumnName.Length;

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

                sw.Write("------------------------------ BATCH ORDER ----------------------------");
                sw.WriteLine();
                sw.WriteLine();

                for (int i = 0; i < dtBatchOrderHeaders.Columns.Count; i++)
                {
                    sw.Write(dtBatchOrderHeaders.Columns[i].ColumnName.PadRight(maxLengthsDtb[i] + 2));
                }

                sw.WriteLine();


                for (int i = 0; i < dtBatchOrderHeaders.Columns.Count; i++)
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
                sw.Write("----------------------------- UNITS ORDERED ---------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                //-------------------------------------------THIS IS FOR THE CUP ORDER DATA-------------------------------------//

                int[] maxLengthsDtc = new int[dtCupOrderData.Columns.Count];

                //Create spacing between columns in txt file
                for (int i = 0; i < dtCupOrderHeaders.Columns.Count; i++)
                {
                    maxLengthsDtc[i] = dtCupOrderHeaders.Columns[i].ColumnName.Length;

                    foreach (DataRow row in dtCupOrderData.Rows)
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
                for (int i = 0; i < dtCupOrderHeaders.Columns.Count; i++)
                {
                    sw.Write(dtCupOrderHeaders.Columns[i].ColumnName.PadRight(maxLengthsDtc[i] + 2));
                }

                sw.WriteLine();

                foreach (DataRow row in dtCupOrderData.Rows)
                {
                    for (int i = 0; i < dtCupOrderData.Columns.Count - 1; i++)
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
    }
}

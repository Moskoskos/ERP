using System.Data;
using System.IO;

namespace ERP
{
    public class ReportGeneration
    {
        public ReportGeneration()
        {

        }

        /// <summary>
        /// http://stackoverflow.com/questions/7174077/export-a-c-sharp-dataset-to-a-text-file
        /// </summary>
        /// <param name="dtBatchOrderHeaders">Data table containing the names of the headers for a selected datagridview.</param>
        /// <param name="dtCupOrderData">Data table with unlimited rows to be processed.</param>
        /// <param name="dtCupOrderHeaders">Data table containing the names of the headers for a selected datagridview.</param>
        /// <param name="cellValues"></param>
        /// <param name="filePath"></param>
        public void Generate(DataTable dtBatchOrderHeaders, DataTable dtCupOrderData, DataTable dtCupOrderHeaders, string[] cellValues, string filePath)
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

            //Write content to file.
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
                            //Gets the lenght of the string.
                            int length = row[i].ToString().Length;

                            //Applies the longest tring as default spacing.
                            if (length > maxLengthsDtc[i])
                            {
                                maxLengthsDtc[i] = length;
                            }
                        }
                    }
                }
                //Counts the number of collums and itterates.
                for (int i = 0; i < dtCupOrderHeaders.Columns.Count; i++)
                {   
                    //Writes to the text file and creates a fixed spacing between the colums.
                    sw.Write(dtCupOrderHeaders.Columns[i].ColumnName.PadRight(maxLengthsDtc[i] + 2));
                }

                //Next line
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

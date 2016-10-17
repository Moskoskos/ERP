using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace ERP
{
    public class DbConnect
    {
        private SqlConnection connection;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        string batchid = "";


        public DbConnect()
        {
             //Initialze connection with connectionString
            connection = new SqlConnection("Data Source = 192.168.2.15\\SQLEXPRESS; Initial Catalog = IA5-5-16; User ID = sa; Password = " + "netlab_1");
            dt.Columns.Add("TypeOfCup", typeof(int));
            dt.Columns.Add("OrderedWeight", typeof(int));
            dt.Columns.Add("BatchID", typeof(int));
        }
        public bool PingHost()
        {
            string nameOrAddress = "192.168.2.15";
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }

        public bool OpenConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        public bool CloseConnection()
        {

            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }


        public void CreateBatchOrder(int numberOfCups)
        {
            try
            {
                string query = "INSERT INTO BatchOrdre(NumberOfCups)VALUES(@numberOfCups); SELECT SCOPE_IDENTITY();";
                if (OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@numberOfCups", numberOfCups);
                        batchid = cmd.ExecuteScalar().ToString();
                        CloseConnection();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InsertOrderIntoDataTable(int numOfCups, int typeOfCup, int fillLevel)
        {



            for (int i = 0; i <= numOfCups; i++)
            {
                // ADD X-AMOUNT OF ROWS TO COLLUMN "TYPE" WITH COLOR Y, ADD CORRESPONDING FILL LEVEL TO COLLUMN FILLLEVEL
                dt.Rows.Add(typeOfCup, fillLevel, batchid);
            }
        }
       //Source:  http://stackoverflow.com/questions/10405373/insert-entire-datatable-into-database-at-once-instead-of-row-by-row
        public void DataTableToDB()
        {
            // take note of SqlBulkCopyOptions.KeepIdentity , you may or may not want to use this for your situation.  

            using (var bulkCopy = new SqlBulkCopy(connection.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
            {
                // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
                foreach (DataColumn col in dt.Columns)
                {
                    bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                }

                bulkCopy.BulkCopyTimeout = 600;
                bulkCopy.DestinationTableName = "dbo.CupOrdre";
                bulkCopy.WriteToServer(dt);
            }
            dt.Clear();
        }

    }
}


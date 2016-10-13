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

        private string server;
        private string database;
        private string uid;
        private string password;
        private SqlConnection connection;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        public DbConnect()
        {
            server = "192.168.2.15";                                                    //Host
            database = "IA5-5-16";                                                      //Database
            uid = "sa";                                                                 //Username
            password = "netlab_1";                                                      //Password
            string connectionString;                                                    //Declare connectionString
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +                 //Format connectiongString
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            //connection = new SqlConnection(connectionString);                           //Initialze connection with connectionString
            connection = new SqlConnection("Data Source = 192.168.2.15\\SQLEXPRESS; Initial Catalog = IA5-5-16; User ID = sa; Password = " + "netlab_1");
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
                string query = "INSERT INTO BatchOrdre(NumberOfCups)VALUES(@numberOfCups);";
                if (OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@numberOfCups", numberOfCups);
                        cmd.ExecuteScalar();
                        CloseConnection();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public double GetLatestValue()
        {
            string result = "";
            string query = "SELECT BatchID FROM BatchOrdre LIMIT 1;";
            //Sjekker at tilkoblingen er åpen.
            if (this.OpenConnection())
            {
                //Bruker spørringen ovenfor og tilkoblingstrengen i DbConnect.
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    //Kjører spørringen og får en verdi tilbake
                    result = cmd.ExecuteScalar().ToString();
                    CloseConnection();
                }
            }
            return Convert.ToDouble(result);
        }

        //public void FillDataSet()
        //{

        //    dt.Columns.Add();
        //    // take note of SqlBulkCopyOptions.KeepIdentity , you may or may not want to use this for your situation.  

        //    using (var bulkCopy = new SqlBulkCopy(_connection.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
        //    {
        //        // my DataTable column names match my SQL Column names, so I simply made this loop. However if your column names don't match, just pass in which datatable name matches the SQL column name in Column Mappings
        //        foreach (DataColumn col in table.Columns)
        //        {
        //            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
        //        }

        //        bulkCopy.BulkCopyTimeout = 600;
        //        bulkCopy.DestinationTableName = destinationTableName;
        //        bulkCopy.WriteToServer(table);
        //    }
        //}

    }
}


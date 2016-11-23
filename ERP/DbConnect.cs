using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace ERP
{
    public class DbConnect
    {
        //Declares global variables.
        private SqlConnection connection;
        public  string batchid = "";
        public string conString = "";


        /// <summary>
        /// Sets the static connection string with user credidentals.
        /// </summary>
        public DbConnect()
        {
            //Initialze connection with connectionString
            conString = "Data Source = 192.168.12.116\\SQLEXPRESS,1433; Initial Catalog = IA5-5-16; User ID = sa; Password = " + "netlab_1";
            connection = new SqlConnection(conString);

        }
        /// <summary>
        /// Pings 192.168.12.116.
        /// </summary>
        /// <returns></returns>
        public bool PingHost()
        {
            string nameOrAddress = "192.168.12.116";
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException pe)
            {
                MessageBox.Show(pe.Message);
            }
            return pingable;
        }

        /// <summary>
        /// Method to attempt to open an connection to the SQL database. if an connection is already open, it will close it.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Method to close an active connection.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Inserts a new row in the BatchOrdre table with the given number of cups
        /// Returns the identity of the inserted row.
        /// </summary>
        /// <param name="numberOfCups">Integer</param>
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Generate a number of rows containing cup ID and fill level.
        /// </summary>
        /// <param name="numOfCups">The total number of cups for a given type / color.</param>
        /// <param name="typeOfCup">The cup type.</param>
        /// <param name="fillLevel">How many grams the particular cup should contain.</param>
        public void InsertOrderIntoDataTable(int numOfCups, int typeOfCup, double fillLevel)
        {
            try
            {
                for (int i = 0; i < numOfCups; i++)
                {
                    string query = "INSERT INTO CupOrdre(TypeOfCup, OrderedWeight, BatchID)VALUES(@typeOfCup, @orderedWeight, @batchID);";
                    if (OpenConnection())
                    {
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@typeOfCup", typeOfCup);
                            cmd.Parameters.AddWithValue("@orderedWeight", fillLevel);
                            cmd.Parameters.AddWithValue("@batchID", batchid);
                            cmd.ExecuteNonQuery();
                            CloseConnection();
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Gets the latest row inserted, or rather the row with the highest value in the BatchID collumn.
        /// </summary>
        /// <returns></returns>
        public int GetLatestRow()
        {
            try
            {
                int value = 0;
                string query = "SELECT TOP 1 BatchID FROM BatchOrdre ORDER BY BatchID DESC;";
                if (OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        value = Convert.ToInt32(cmd.ExecuteScalar()); 
                        CloseConnection();
                    }
                }
                return value;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
        }

    }
}


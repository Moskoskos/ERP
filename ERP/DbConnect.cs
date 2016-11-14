using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.ComponentModel;

namespace ERP
{
    public class DbConnect
    {
        private SqlConnection connection;
        public DataTable dt = new DataTable();
        public  string batchid = "";
        public string conString = "";



        public DbConnect()
        {
            //Initialze connection with connectionString
            conString = "Data Source = 192.168.12.116\\SQLEXPRESS,1434; Initial Catalog = IA5-5-16; User ID = sa; Password = " + "netlab_1";
            connection = new SqlConnection(conString);
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


        /// <summary>
        /// Inserts a new row in the BatchOrdre table with the given number of cups
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
        public int GetLatestRow()
        {
            try
            {
                int value = 0;
                string query = "SELECT TOP 1 BatchID FROM BatchOrdre;";
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

        public int GetNumberOfCupsInOrder(int batchId)
        {
            try
            {
                int value = 0;
                int value2 = 0;
                string query = "SELECT COUNT(*), SUM(CASE WHEN CompletedApproved = 'true' THEN 1 ELSE 0 END) FROM CupOrdre;";

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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
        }
    }
}


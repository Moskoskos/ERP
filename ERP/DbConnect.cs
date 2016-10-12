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

            public DbConnect()
            {
                server = "192.168.2.15";                                                    //Host
                database = "IA5-5-16";                                                      //Database
                uid = "sa";                                                                 //Username
                password = "netlab_1";                                                      //Password
                string connectionString;                                                    //Declare connectionString
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +                 //Format connectiongString
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connection = new SqlConnection(connectionString);                           //Initialze connection with connectionString
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
            public void InsertIntoUsers(string usernameIn, string firstnameIn, string lastnameIn, string emailIn, int numberIn)
            {
                //Source:
                //http://stackoverflow.com/questions/19527554/inserting-values-into-mysql-database-from-c-sharp-application-text-box
                {
                    try
                    {

                        string query = "INSERT INTO users(username, firstname, lastname, email, phonenumber)VALUES(@username, @firstname, @lastname,@email,@number);";
                        //Sjekker at tilkoblingen er åpen.
                        if (OpenConnection())
                        {
                            //Bruker spørringen ovenfor og tilkoblingstrengen i DbConnect.
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                // Henter inn verdiene fra parameterene og legger de til som en verdi i spørringen query
                                cmd.Parameters.AddWithValue("@username", usernameIn);
                                cmd.Parameters.AddWithValue("@firstname", firstnameIn);
                                cmd.Parameters.AddWithValue("@lastname", lastnameIn);
                                cmd.Parameters.AddWithValue("@email", emailIn);
                                cmd.Parameters.AddWithValue("@number", numberIn);
                                //Kjører en SQL-commando uten å få noen verdi tilbake.
                                cmd.ExecuteNonQuery();
                                CloseConnection();
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Could not process your order\r\r\n" + ex.Message);
                    }
                }
            }
        }
    }


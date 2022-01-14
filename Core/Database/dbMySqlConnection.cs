using Core.Data;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database
{
    public class dbMySqlConnection
    {
        // connection to MySqlDdatabase
        private MySqlConnection _con;

        /// <summary>
        /// Opens a connection to the database
        /// </summary>
        /// <param name="ServerLocation">Location of database</param>
        /// <param name="dbUser">Database user to log in with</param>
        /// <param name="dbPassword">Database user password</param>
        /// <param name="DatabaseName">Database name to open</param>
        /// <returns>True if database was opened sucsefully</returns>
        public bool OpenConnection(string ServerLocation, string dbUser, string dbPassword, string DatabaseName)
        {
            bool WasConnectionOpended = false;

            var builder = new MySqlConnectionStringBuilder
            {
                Server = ServerLocation,
                UserID = dbUser,
                Password = dbPassword,
                Database = DatabaseName,
            };

            try
            {
                this._con = new MySqlConnection(builder.ConnectionString);

                this._con.Open();

                WasConnectionOpended = true;
            }
            catch{}

            return WasConnectionOpended;

        }

        /// <summary>
        /// Opens the connection using the connection data found in <see cref="Settings.DatabaseLogin"/>
        /// </summary>
        /// <returns>True if connection was open</returns>
        public bool OpenConnection()
        {
            return this.OpenConnection(Settings.DatabaseLogin.ServerLocation,
                                                   Settings.DatabaseLogin.dbUser,
                                                   Settings.DatabaseLogin.dbPassword,
                                                   Settings.DatabaseLogin.DatabaseName);
        }

        /// <summary>
        /// Attemps to close the SQLite database connection 
        /// </summary>
        /// <returns>true if sucsefull, else false</returns>
        public bool CloseConnection()
        {
            try
            {
                this._con.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public MySqlDataReader ExecuteSelectCommand(string SQLCommand)
        {
            MySqlCommand MySqlCommand;

            MySqlCommand = this._con.CreateCommand();
            MySqlCommand.CommandText = SQLCommand;

            try
            {
                return MySqlCommand.ExecuteReader();
            }
            catch
            {
                return null;
            }

        }


        public MySqlDataReader ExecuteParameterizedSelectCommand(string SQLCommand, MySqlParameter[] parameters) //$parameters = SQLiteParameter[]
        {
            MySqlCommand MySqlCommand;
            

            MySqlCommand = this._con.CreateCommand();
            MySqlCommand.CommandText = SQLCommand;


            MySqlCommand.Parameters.AddRange(parameters);

            try
            {
                return MySqlCommand.ExecuteReader();
            }
            catch
            {
                return null;
            }
        }


        public int ExecuteParameterizedNoneReader(string SQLCommand, MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand;

            MySqlCommand = this._con.CreateCommand();
            MySqlCommand.CommandText = SQLCommand;


            MySqlCommand.Parameters.AddRange(parameters);

            try
            {
                return MySqlCommand.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
        }



        public int Get_Last_Insert_Id()
        {
            MySqlCommand sqliteCommand;

            sqliteCommand = this._con.CreateCommand();
            sqliteCommand.CommandText = "SELECT LAST_INSERT_ID()";

            try
            {
                return (int)(long)sqliteCommand.ExecuteScalar();
            }
            catch
            {
                return -1;
            }
        }

    }
}

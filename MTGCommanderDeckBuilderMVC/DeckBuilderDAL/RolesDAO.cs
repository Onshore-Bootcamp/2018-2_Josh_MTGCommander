using System;
using System.Collections.Generic;
using DeckBuilderDAL.Models;
using System.Data.SqlClient;
using System.Data;

namespace DeckBuilderDAL
{
    public class RolesDAO
    {
        //Dependencies
        public string currentClass = "RolesDAO";
        private string connectionString;
        private ErrorLogger logAccess;
        private string logPath;

        //Constructor
        public RolesDAO(string connString, string errorPath)
        {
            connectionString = connString;
            logPath = errorPath;
            logAccess = new ErrorLogger(logPath);
        }

        //Method to read a role name from the database with the received roleID
        public string ReadRoleNameAtID(int roleID)
        {
            //Declaring local variables
            string roleName = String.Empty;
            string currentMethod = "ReadRoleNameAtID";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand readCommand = new SqlCommand("READ_ROLENAME_AT_ID", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    readCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    readCommand.Parameters.AddWithValue("@RoleID", roleID);
                    //Opening the connection to the database
                    deckBuilderConnection.Open();

                    //Using SqlDataReader to read a row of the User table.
                    using (SqlDataReader roleReader = readCommand.ExecuteReader())
                    {
                        //Reading column data to a string 
                        roleReader.Read();
                        roleName = roleReader.GetString(0);
                    }
                }

            }
            catch (Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }

            return roleName;
        }
    }
}

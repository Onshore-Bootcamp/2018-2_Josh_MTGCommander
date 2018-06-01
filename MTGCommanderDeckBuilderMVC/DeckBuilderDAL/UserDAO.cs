using System;
using System.Collections.Generic;
using DeckBuilderDAL.Models;
using System.Data.SqlClient;
using System.Data;

namespace DeckBuilderDAL
{
    public class UserDAO
    {
        //Dependencies
        public string currentClass = "UserDAO";
        private string connectionString;
        private ErrorLogger logAccess;
        private string logPath;

        //Constructor
        public UserDAO(string connString, string errorPath)
        {
            connectionString = connString;
            logPath = errorPath;
            logAccess = new ErrorLogger(logPath);
        }

        //Method to add a User to the database
        public void CreateUser(UserDO user)
        {
            //Declaring local variables
            string currentMethod = "CreateUser";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand createCommand = new SqlCommand("CREATE_USER", deckBuilderConnection))
                {
                    //Setting CommandType of SqlCommand
                    createCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    createCommand.Parameters.AddWithValue("@Username", user.Username);
                    createCommand.Parameters.AddWithValue("@Password", user.Password);
                    createCommand.Parameters.AddWithValue("@Firstname", user.FirstName);
                    createCommand.Parameters.AddWithValue("@Lastname", user.LastName);
                    createCommand.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                    createCommand.Parameters.AddWithValue("@RoleID", user.RoleID);

                    //Executing command after opening the connection
                    deckBuilderConnection.Open();
                    createCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        //Method to read all users in the database
        public List<UserDO> ReadUsers()
        {
            //Declaring local variables
            List<UserDO> userList = new List<UserDO>();
            string currentMethod = "ReadUsers";

            try
            {
                //Creating a new connection to the SQL database and a SqlCommand to use a stored procedure
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand readAllCommand = new SqlCommand("READ_USERS", deckBuilderConnection))
                {
                    //Setting CommandType of SqlCommand
                    readAllCommand.CommandType = CommandType.StoredProcedure;
                    //Opening a connection to the database
                    deckBuilderConnection.Open();

                    //Creating an object to read each row of the database table
                    using (SqlDataReader usersReader = readAllCommand.ExecuteReader())
                    {
                        while (usersReader.Read())
                        {
                            //Reading table data to current UserDO and adding it to the list
                            UserDO user = new UserDO();
                            user.UserID = usersReader.GetInt64(0);
                            user.Username = usersReader.GetString(1);
                            user.Password = usersReader.GetString(2);
                            user.FirstName = usersReader.GetString(3);
                            user.LastName = usersReader.GetString(4);
                            user.EmailAddress = usersReader.GetString(5);
                            user.RoleID = usersReader.GetInt32(6);
                            //Adding current UserDO object to the list
                            userList.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
            return userList;
        }

        //Method to read a user from the database with the received userName
        public UserDO ReadUserAtID(long userID)
        {
            //Declaring local variables
            UserDO user = new UserDO();
            string currentMethod = "ReadUserAtID";

            try
            {

                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand readCommand = new SqlCommand("READ_USER_AT_ID", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    readCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    readCommand.Parameters.AddWithValue("@UserID", userID);
                    //Opening the connection to the database
                    deckBuilderConnection.Open();

                    //Using SqlDataReader to read a row of the User table.
                    using (SqlDataReader userReader = readCommand.ExecuteReader())
                    {
                        //Reading table data to a UserDO 
                        userReader.Read();
                        user.UserID = userReader.GetInt64(0);
                        user.Username = userReader.GetString(1);
                        user.Password = userReader.GetString(2);
                        user.FirstName = userReader.GetString(3);
                        user.LastName = userReader.GetString(4);
                        user.EmailAddress = userReader.GetString(5);
                        user.RoleID = userReader.GetInt32(6);
                    }
                }

            }
            catch (Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
            return user;
        }

        //Method to read a user from the database with the received userName
        public UserDO ReadUserAtUsername(string username)
        {
            //Declaring local variables
            UserDO user = new UserDO();
            string currentMethod = "ReadUserAtUsername";

            try
            {

                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand readCommand = new SqlCommand("READ_USER_AT_USERNAME", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    readCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    readCommand.Parameters.AddWithValue("@Username", username);
                    //Opening the connection to the database
                    deckBuilderConnection.Open();

                    //Using SqlDataReader to read a row of the User table.
                    using (SqlDataReader userReader = readCommand.ExecuteReader())
                    {
                        //Reading table data to a UserDO 
                        userReader.Read();
                        user.UserID = userReader.GetInt64(0);
                        user.Username = userReader.GetString(1);
                        user.Password = userReader.GetString(2);
                        user.FirstName = userReader.GetString(3);
                        user.LastName = userReader.GetString(4);
                        user.EmailAddress = userReader.GetString(5);
                        user.RoleID = userReader.GetInt32(6);
                    }
                }

            }
            catch (Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
            return user;
        }

        //Method to update a row of the User table in the database
        public void UpdateUser(UserDO user)
        {
            //Declaring local variables
            string currentMethod = "UpdateUser";

            try
            {
                //Creating a new SqlConnection
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to run a stored procedure
                using (SqlCommand updateCommand = new SqlCommand("UPDATE_USER", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    updateCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    updateCommand.Parameters.AddWithValue("@UserID", user.UserID);
                    updateCommand.Parameters.AddWithValue("@Username", user.Username);
                    updateCommand.Parameters.AddWithValue("@Password", user.Password);
                    updateCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                    updateCommand.Parameters.AddWithValue("@LastName", user.LastName);
                    updateCommand.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
                    updateCommand.Parameters.AddWithValue("@RoleID", user.RoleID);

                    //Executing command after opening the connection
                    deckBuilderConnection.Open();
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        //Method to delete a row from the User table in the database
        public void DeleteUser(long userID)
        {
            //Declaring local variables
            string currentMethod = "DeleteUser";

            try
            {
                //Creating a new SqlConnection
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to run a stored procedure
                using (SqlCommand deleteCommand = new SqlCommand("DELETE_USER", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    deleteCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    deleteCommand.Parameters.AddWithValue("@UserID", userID);

                    //Executing command after opening the connection
                    deckBuilderConnection.Open();
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        //Method to read a RoleName from the database at the
    }
}

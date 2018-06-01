using DeckBuilderDAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckBuilderDAL
{
    public class DeckDAO
    {
        //Dependencies
        public string currentClass = "DeckDAO";
        private string connectionString;
        private ErrorLogger logAccess;
        private string logPath;

        //Constructor
        public DeckDAO(string connString, string errorPath)
        {
            connectionString = connString;
            logPath = errorPath;
            logAccess = new ErrorLogger(logPath);
        }

        //Method to add a deck to the database
        public void CreateDeck(DeckDO deck)
        {
            //Declaring local variables
            string currentMethod = "CreateDeck";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand that uses a stored procedure
                using (SqlCommand createCommand = new SqlCommand("CREATE_DECK", deckBuilderConnection))
                {
                    //Setting the CommandType of the SqlCommand
                    createCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    createCommand.Parameters.AddWithValue("UserID", deck.UserID);
                    createCommand.Parameters.AddWithValue("DeckName", deck.DeckName);
                    createCommand.Parameters.AddWithValue("CommanderName", deck.CommanderName);
                    createCommand.Parameters.AddWithValue("DeckColors", deck.DeckColors);
                    createCommand.Parameters.AddWithValue("DeckArchetype", deck.DeckArchetype);

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

        //Method to read all decks from the database
        public List<DeckDO> ReadDecks()
        {
            //Declaring local variables
            List<DeckDO> deckList = new List<DeckDO>();
            string currentMethod = "ReadDecks";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand readAllCommand = new SqlCommand("READ_DECKS", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    readAllCommand.CommandType = CommandType.StoredProcedure;
                    //Opening a connection to the database
                    deckBuilderConnection.Open();

                    //Creating an object to read each row of the database table
                    using (SqlDataReader decksReader = readAllCommand.ExecuteReader())
                    {
                        while (decksReader.Read())
                        {
                            //Reading table data to current DeckDO
                            DeckDO deck = new DeckDO();
                            deck.DeckID = decksReader.GetInt64(0);
                            deck.UserID = decksReader.GetInt64(1);
                            deck.DeckName = decksReader.GetString(2);
                            deck.CommanderName = decksReader.GetString(3);
                            deck.DeckColors = decksReader.GetString(4);
                            deck.DeckArchetype = decksReader.GetString(5);
                            //Adding the DeckDO object to the list
                            deckList.Add(deck);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
            return deckList;
        }

        //Method to read a deck with the passed deckID
        public DeckDO ReadDeckAtID(long deckID)
        {
            //Declaring local variables
            DeckDO deck = new DeckDO();
            string currentMethod = "ReadDeckAtID";

            try
            {
                //Creating a new SqlConnection
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creatina a SqlCommand to run a stored procedure
                using (SqlCommand readCommand = new SqlCommand("READ_DECK_AT_DECK_ID", deckBuilderConnection))
                {
                    //Setting CommandType of SqlCommand
                    readCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    readCommand.Parameters.AddWithValue("@DeckID", deckID);
                    //Opening a connection to the database
                    deckBuilderConnection.Open();

                    //Creating an object to read the SQL table row when specified deckID is found
                    using (SqlDataReader deckReader = readCommand.ExecuteReader())
                    {
                        //Reading table data to a DeckDO object
                        deckReader.Read();
                        deck.DeckID = deckReader.GetInt64(0);
                        deck.UserID = deckReader.GetInt64(1);
                        deck.DeckName = deckReader.GetString(2);
                        deck.CommanderName = deckReader.GetString(3);
                        deck.DeckColors = deckReader.GetString(4);
                        deck.DeckArchetype = deckReader.GetString(5);
                    }
                }
            }
            catch(Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
            return deck;
        }

        //Method to read all decks from the database with the passed userID
        public List<DeckDO> ReadDecksAtUserID(long userID)
        {
            //Declaring local variables
            List<DeckDO> deckList = new List<DeckDO>();
            string currentMethod = "ReadDecksAtUserID";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand readCommand = new SqlCommand("READ_DECKS_AT_USER_ID", deckBuilderConnection))
                {
                    //Setting CommandType of SqlCommand
                    readCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    readCommand.Parameters.AddWithValue("@UserID", userID);
                    //Opening a connection to the database
                    deckBuilderConnection.Open();

                    //Creating an object to read each row of the database table
                    using (SqlDataReader decksReader = readCommand.ExecuteReader())
                    {
                        while (decksReader.Read())
                        {
                            //Reading table data to current DeckDO object
                            DeckDO deck = new DeckDO();
                            deck.DeckID = decksReader.GetInt64(0);
                            deck.UserID = decksReader.GetInt64(1);
                            deck.DeckName = decksReader.GetString(2);
                            deck.CommanderName = decksReader.GetString(3);
                            deck.DeckColors = decksReader.GetString(4);
                            deck.DeckArchetype = decksReader.GetString(5);
                            //Adding current DeckDO object to the list
                            deckList.Add(deck);
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
            return deckList;
        }

        //Method to update a deck at the passed DeckDO's deckID
        public void UpdateDeck(DeckDO deck)
        {
            //Declaring local variables
            string currentMethod = "UpdateDeck";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand updateCommand = new SqlCommand("UPDATE_DECK", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    updateCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    updateCommand.Parameters.AddWithValue("@DeckID", deck.DeckID);
                    updateCommand.Parameters.AddWithValue("@UserID", deck.UserID);
                    updateCommand.Parameters.AddWithValue("@DeckName", deck.DeckName);
                    updateCommand.Parameters.AddWithValue("@CommanderName", deck.CommanderName);
                    updateCommand.Parameters.AddWithValue("@DeckColors", deck.DeckColors);
                    updateCommand.Parameters.AddWithValue("@DeckArchetype", deck.DeckArchetype);

                    //Executing command after opening the connection
                    deckBuilderConnection.Open();
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        //Method to delete a deck at the passed deckID
        public void DeleteDeck(long deckID)
        {
            //Declaring local variables
            string currentMethod = "DeleteDeck";

            try
            {
                //Creating a new SqlConnection
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to run a stored procedure
                using (SqlCommand deleteCommand = new SqlCommand("DELETE_DECK", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    deleteCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    deleteCommand.Parameters.AddWithValue("@DeckID", deckID);

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
    }
}

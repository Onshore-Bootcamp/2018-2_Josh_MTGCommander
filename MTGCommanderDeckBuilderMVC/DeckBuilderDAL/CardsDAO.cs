using System;
using System.Collections.Generic;
using DeckBuilderDAL.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace DeckBuilderDAL
{
    public class CardsDAO
    {
        //Dependencies
        public string currentClass = "CardsDAO";
        private string connectionString;
        private ErrorLogger logAccess;
        private string logPath;

        //Constructor
        public CardsDAO(string connString, string errorPath)
        {
            connectionString = connString;
            logPath = errorPath;
            logAccess = new ErrorLogger(logPath);
        }

        //Method to add a card to the database
        public void CreateCard(CardDO card)
        {
            //Declaring local variables
            string currentMethod = "CreateCard";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand that uses a stored procedure
                using (SqlCommand createCommand = new SqlCommand("CREATE_CARD", deckBuilderConnection))
                {
                    //Setting the CommandType of the SqlCommand                    
                    createCommand.CommandType = CommandType.StoredProcedure;

                    //Passing values to the stored procedure parameters
                    createCommand.Parameters.AddWithValue("@CardName", card.CardName);
                    createCommand.Parameters.AddWithValue("@ManaCost", card.ManaCost);
                    createCommand.Parameters.AddWithValue("@CardType", card.CardType);
                    createCommand.Parameters.AddWithValue("@ColorIdentity", card.ColorIdentity);
                    createCommand.Parameters.AddWithValue("@Abilities", card.Abilities);
                    createCommand.Parameters.AddWithValue("@CardStats", card.CardStats);

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

        //Method to read each row of Cards table from the database into a list of CardDOs and return it
        public List<CardDO> ReadCards()
        {
            //Declaring local variables
            List<CardDO> cardList = new List<CardDO>();
            string currentMethod = "ReadCards";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand readAllCommand = new SqlCommand("READ_CARDS", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    readAllCommand.CommandType = CommandType.StoredProcedure;
                    //Opening a connection to the database
                    deckBuilderConnection.Open();

                    //Creating an object to read each row of the database table
                    using (SqlDataReader cardsReader = readAllCommand.ExecuteReader())
                    {
                        while (cardsReader.Read())
                        {
                            //Reading table data to current CardDO
                            CardDO card = new CardDO();
                            card.CardID = cardsReader.GetInt64(0);
                            card.CardName = cardsReader.GetString(1);
                            card.ManaCost = cardsReader.GetInt16(2);
                            card.CardType = cardsReader.GetString(3);
                            card.ColorIdentity = cardsReader.GetString(4);
                            card.Abilities = cardsReader.GetString(5);
                            card.CardStats = cardsReader.GetString(6);
                            //Adding CardDO object to the list
                            cardList.Add(card);
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

            return cardList;
        }

        //Method to read and return the Cards table row found at the received cardID as a CardDO
        public CardDO ReadCardAtID(long cardID)
        {
            //Declaring local variables
            CardDO card = new CardDO();
            string currentMethod = "ReadCardAtID";

            try
            {
                //Creating a new SqlConnection
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to run a stored procedure
                using (SqlCommand readCommand = new SqlCommand("READ_CARD_AT_ID", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    readCommand.CommandType = CommandType.StoredProcedure;
                    //Passing a value to the stored procedure
                    readCommand.Parameters.AddWithValue("@CardID", cardID);
                    //Opening a connection to the database
                    deckBuilderConnection.Open();

                    //Creating an object to read the SQL table row when specified cardID is found
                    using (SqlDataReader cardReader = readCommand.ExecuteReader())
                    {
                        //Reading table data to the CardDO object 
                        //Todo: Make an if!
                        cardReader.Read();
                        card.CardID = cardReader.GetInt64(0);
                        card.CardName = cardReader.GetString(1);
                        card.ManaCost = cardReader.GetInt16(2);
                        card.CardType = cardReader.GetString(3);
                        card.ColorIdentity = cardReader.GetString(4);
                        card.Abilities = cardReader.GetString(5);
                        card.CardStats = cardReader.GetString(6);
                    }
                }
            }
            catch (Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
            return card;
        }

        //Method to update the Card table row at received CardDO's cardID
        public void UpdateCard(CardDO card)
        {
            //Declaring local variables
            string currentMethod = "UpdateCard";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand updateCommand = new SqlCommand("UPDATE_CARD", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    updateCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    updateCommand.Parameters.AddWithValue("@CardID", card.CardID);
                    updateCommand.Parameters.AddWithValue("@CardName", card.CardName);
                    updateCommand.Parameters.AddWithValue("@ManaCost", card.ManaCost);
                    updateCommand.Parameters.AddWithValue("@CardType", card.CardType);
                    updateCommand.Parameters.AddWithValue("@ColorIdentity", card.ColorIdentity);
                    updateCommand.Parameters.AddWithValue("@Abilities", card.Abilities);
                    updateCommand.Parameters.AddWithValue("@CardStats", card.CardStats);

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

        //Method to delete the Card table row at the received cardID
        public void DeleteCard(long cardID)
        {
            //Declaring local variables
            string currentMethod = "DeleteCard";

            try
            {
                //Creating a new SqlConnection
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to run a stored procedure
                using (SqlCommand deleteCommand = new SqlCommand("DELETE_CARD", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    deleteCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    deleteCommand.Parameters.AddWithValue("@CardID", cardID);

                    //Executing command after opening the connection
                    deckBuilderConnection.Open();
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
        }
    }
}

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
    public class CardsInDecksDAO
    {
        //Dependencies
        public string currentClass = "CardsInDecksDAO";
        private string connectionString;
        private ErrorLogger logAccess;
        private string logPath;

        //Constructor
        public CardsInDecksDAO(string connString, string errorPath)
        {
            connectionString = connString;
            logPath = errorPath;
            logAccess = new ErrorLogger(logPath);
        }

        //Method to add a CardInDeck to the database
        public void CreateCardInDeck(CardInDeckDO deckCard)
        {
            //Declaring local variables
            string currentMethod = "CreateCardInDeck";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand that uses a stored procedure
                using (SqlCommand createCommand = new SqlCommand("CREATE_CARD_IN_DECK", deckBuilderConnection))
                {
                    //Setting the CommandType of the SqlCommand
                    createCommand.CommandType = CommandType.StoredProcedure;

                    //Passing values to the stored procedure's parameters
                    createCommand.Parameters.AddWithValue("@CardID", deckCard.CardID);
                    createCommand.Parameters.AddWithValue("@DeckID", deckCard.DeckID);

                    //Executing command after the connection is opened
                    deckBuilderConnection.Open();
                    createCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                //Logging error
                logAccess.ErrorLogging("Error", currentClass, currentMethod, ex.Message, ex.StackTrace);
                throw ex;
            }
        }

        //Method that reads the cards in a specific deck
        public List<CardDO> ReadCardsInDeck(long deckID)
        {
            //Declaring local variables
            List<CardDO> cardList = new List<CardDO>();
            string currentMethod = "ReadCardsInDeck";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand to use a stored procedure
                using (SqlCommand readAllCommand = new SqlCommand("READ_CARDS_AT_DECK_ID", deckBuilderConnection))
                {
                    //Setting CommandType of the SqlCommand
                    readAllCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to stored procedure parameters
                    readAllCommand.Parameters.AddWithValue("@DeckID", deckID);
                    //Opening a connection to the database
                    deckBuilderConnection.Open();

                    //Creating an object to read each row of the database table
                    using (SqlDataReader cardsInDeckReader = readAllCommand.ExecuteReader())
                    {
                        while(cardsInDeckReader.Read())
                        {
                            //Reading table data to current CardDO and adding it to the list
                            CardDO cardInDeck = new CardDO();
                            cardInDeck.CardID = cardsInDeckReader.GetInt64(0);
                            cardInDeck.CardName = cardsInDeckReader.GetString(1);
                            cardInDeck.ManaCost = cardsInDeckReader.GetInt16(2);
                            cardInDeck.CardType = cardsInDeckReader.GetString(3);
                            cardInDeck.ColorIdentity = cardsInDeckReader.GetString(4);
                            cardInDeck.Abilities = cardsInDeckReader.GetString(5);
                            cardInDeck.CardStats = cardsInDeckReader.GetString(6);
                            cardList.Add(cardInDeck);
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
            return cardList;
        }

        //Method removes a card from a deck
        public void DeleteCardFromDeck(CardInDeckDO deckCard)
        {
            //Declaring local variables
            string currentMethod = "DeleteCardFromDeck";

            try
            {
                //Creating a new connection to the SQL database
                using (SqlConnection deckBuilderConnection = new SqlConnection(connectionString))
                //Creating a SqlCommand that uses a stored procedure
                using (SqlCommand deleteCommand = new SqlCommand("DELETE_CARD_FROM_DECK", deckBuilderConnection))
                {
                    //Setting the CommandType of the SqlCommand
                    deleteCommand.CommandType = CommandType.StoredProcedure;
                    //Passing values to the stored procedure
                    deleteCommand.Parameters.AddWithValue("@CardID", deckCard.CardID);
                    deleteCommand.Parameters.AddWithValue("@DeckID", deckCard.DeckID);

                    //Executing command after the connection is opened
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

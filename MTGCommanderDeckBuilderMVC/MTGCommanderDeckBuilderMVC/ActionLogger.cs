using DeckBuilderDAL;
using DeckBuilderDAL.Models;
using MTGCommanderDeckBuilderMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MTGCommanderDeckBuilderMVC
{
    //Information logger that records user actions
    public class ActionLogger
    {
        //Dependencies
        private string logPath;

        //Constructor
        public ActionLogger(string filePath)
        {
            logPath = filePath;
        }

        //Method that logs a user action without an associated PO Model
        public void ActionLogging(string level, string className, string methodName, long userID, string userInput)
        {
            DateTime currentDateTime = DateTime.Now;

            try
            {

                using (StreamWriter actionLogger = new StreamWriter(logPath, true))
                {
                    actionLogger.WriteLine(new string('-', 80));
                    actionLogger.WriteLine($"{userID} - {currentDateTime.ToString("MM/dd/yyyy HH:mm:ss")} - {level} - {className} - {methodName}");
                    actionLogger.WriteLine(userInput);
                }
            }
            catch (Exception ex)
            {
            }
        }

        //Method that logs a user action with an associated CardPO Model
        public void ActionLogging(string level, string className, string methodName, long userID, CardPO card)
        {
            DateTime currentDateTime = DateTime.Now;

            try
            {

                using (StreamWriter actionLogger = new StreamWriter(logPath, true))
                {
                    actionLogger.WriteLine(new string('-', 80));
                    actionLogger.WriteLine($"{userID} - {currentDateTime.ToString("MM/dd/yyyy HH:mm:ss")} - {level} - {className} - {methodName}");
                    actionLogger.WriteLine($"Card ID:    {card.CardID}");
                    actionLogger.WriteLine($"Card Name:  {card.CardName}");
                    actionLogger.WriteLine($"Mana Cost:  {card.ManaCost}");
                    actionLogger.WriteLine($"Card Type:  {card.CardType}");
                    actionLogger.WriteLine($"Abilities:  {card.Abilities}");
                    actionLogger.WriteLine($"Card Stats: {card.CardStats}");
                }
            }
            catch (Exception ex)
            {
            }
        }

        //Method that logs a user action with an associated DeckPO Model
        public void ActionLogging(string level, string className, string methodName, long userID, DeckPO deck)
        {
            DateTime currentDateTime = DateTime.Now;

            try
            {

                using (StreamWriter actionLogger = new StreamWriter(logPath, true))
                {
                    actionLogger.WriteLine(new string('-', 80));
                    actionLogger.WriteLine($"{userID} - {currentDateTime.ToString("MM/dd/yyyy HH:mm:ss")} - {level} - {className} - {methodName}");
                    actionLogger.WriteLine($"Deck ID:     {deck.DeckID}");
                    actionLogger.WriteLine($"User ID:     {deck.UserID}");
                    actionLogger.WriteLine($"Deck Name:   {deck.DeckName}");
                    actionLogger.WriteLine($"Commander:   {deck.CommanderName}");
                    actionLogger.WriteLine($"Deck Colors: {deck.DeckColors}");
                    actionLogger.WriteLine($"Archetype:   {deck.DeckArchetype}");
                }
            }
            catch (Exception ex)
            {
            }
        }

        //Method that logs a user action with an associated CardInDeckPO Model
        public void ActionLogging(string level, string className, string methodName, long userID, CardInDeckDO deckCard)
        {
            DateTime currentDateTime = DateTime.Now;

            try
            {

                using (StreamWriter actionLogger = new StreamWriter(logPath, true))
                {
                    actionLogger.WriteLine(new string('-', 80));
                    actionLogger.WriteLine($"{userID} - {currentDateTime.ToString("MM/dd/yyyy HH:mm:ss")} - {level} - {className} - {methodName}");
                    actionLogger.WriteLine($"Deck ID: {deckCard.DeckID}");
                    actionLogger.WriteLine($"Card ID: {deckCard.CardID}");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
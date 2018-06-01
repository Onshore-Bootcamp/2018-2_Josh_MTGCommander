using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using DeckBuilderBAL;
using DeckBuilderDAL;
using DeckBuilderDAL.Models;
using MTGCommanderDeckBuilderMVC.Models;
using MTGCommanderDeckBuilderMVC.Mapping;
using MTGCommanderDeckBuilderMVC.Custom;
using DeckBuilderBAL.Models;

namespace MTGCommanderDeckBuilderMVC.Controllers
{
    public class CardsInDecksController : Controller
    {
        //Dependencies
        private CardsInDecksDAO dataAccess;
        private DeckDAO deckAccess;
        private CalculationsBAO businessAccess;
        private ActionLogger logAccess;
        public string currentClass = "CardsInDecksController";
        private string logPath;

        //Constructor
        public CardsInDecksController()
        {
            //Getting config file variables
            string connectionString = ConfigurationManager.ConnectionStrings["serverConnString"].ConnectionString;
            logPath = ConfigurationManager.AppSettings["ActionLogPath"];
            string errorLogPath = ConfigurationManager.AppSettings["ErrorLogPath"];

            //Calls the constructor for CardsInDecksDAO so this controller has access to the Datalayer
            dataAccess = new CardsInDecksDAO(connectionString, errorLogPath);
            deckAccess = new DeckDAO(connectionString, errorLogPath);
            businessAccess = new CalculationsBAO();
            logAccess = new ActionLogger(logPath);
        }

        //Read/View all cards in deck
        public ActionResult Index(long deckID)
        {
            //Declaring local variables
            List<CardPO> mappedCards = new List<CardPO>();
            string currentMethod = "Index";

            try
            {
                //Calling method to return a list of all cards from the database
                List<CardDO> cardsData = dataAccess.ReadCardsInDeck(deckID);
                //Calling method to return the deck with passed ID from the database
                DeckDO deckData = deckAccess.ReadDeckAtID(deckID);
                //If deck was found, assigning it's UserID to the Viewbag for verification purposes
                if(deckData.DeckID != 0)
                {
                    ViewBag.UserID = deckData.UserID;
                }
                mappedCards = CardMapper.MapDOToPO(cardsData);
                List<CardBO> calcCards = CardMapper.MapPOToBO(mappedCards);
                Session["DeckID"] = deckID;
                ViewBag.ManaCurve = businessAccess.GetManaCurve(calcCards);
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Cannot display the Card List at this time!";
            }

            if (Session["UserId"] == null)
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, 0, $"View Cards In Deck {deckID}");
            }
            else
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], $"View Cards in Deck {deckID}");
            }

            return View(mappedCards);
        }

        //Method that calls the SelectDeck view for a CardInDeck
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult SelectDeck(long cardID)
        {
            //Declaring local variables
            List<DeckPO> mappedDecks = new List<DeckPO>();
            long userID = (long)Session["UserID"];
            ActionResult oResponse = RedirectToAction("Index", "Cards");

            try
            {
                //Calling method to return a list of all cards from the database
                List<DeckDO> decksData = deckAccess.ReadDecksAtUserID(userID);
                if (decksData.Count != 0)
                {
                    mappedDecks = DeckMapper.MapDOToPO(decksData);
                    //Setting currently selected cardID to TempData
                    TempData["CardID"] = cardID;
                    //Setting redirect to current View
                    oResponse = View(mappedDecks);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "You don't have any decks!  Why don't you make one?";
                    oResponse = RedirectToAction("Create", "Deck");
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Cannot display the your decks at this time!";
            }
            return oResponse;
        }

        //Method that start the Create process for a CardInDeck
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Create(long cardID, long deckID)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Cards");
            CardInDeckDO deckCard = new CardInDeckDO();
            string currentMethod = "Create";

            try
            {
                deckCard.CardID = cardID;
                deckCard.DeckID = deckID;
                //Calling method to add a CardInDeck to the database
                dataAccess.CreateCardInDeck(deckCard);

                //Setting confirmation message
                TempData["Message"] = "Card successfully added to deck!";
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Card couldn't be added to the deck at this time!";
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], deckCard);

            return oResponse;
        }

        //Method that starts the Delete process for a CardInDeck
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Delete(long cardID, long deckID)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "CardsInDecks", new { deckID = deckID });
            CardInDeckDO deckCard = new CardInDeckDO();
            string currentMethod = "Delete";

            try
            {
                deckCard.CardID = cardID;
                deckCard.DeckID = deckID;
                //Calling method to delete card with passed ID
                dataAccess.DeleteCardFromDeck(deckCard);
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Card couldn't be removed from the deck at this time.";
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], $"Delete Card {cardID} From Deck {deckID}");

            return oResponse;
        }
    }
}
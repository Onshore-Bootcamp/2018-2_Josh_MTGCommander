using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using DeckBuilderDAL;
using DeckBuilderDAL.Models;
using MTGCommanderDeckBuilderMVC.Models;
using MTGCommanderDeckBuilderMVC.Mapping;
using MTGCommanderDeckBuilderMVC.Custom;

namespace MTGCommanderDeckBuilderMVC.Controllers
{
    public class CardsController : Controller
    {
        //Dependencies
        private CardsDAO dataAccess;
        private ActionLogger logAccess;
        public string currentClass = "CardsController";
        private string logPath;

        //Constructor
        public CardsController()
        {
            //Getting config file variables
            string connectionString = ConfigurationManager.ConnectionStrings["serverConnString"].ConnectionString;
            logPath = ConfigurationManager.AppSettings["ActionLogPath"];
            string errorLogPath = ConfigurationManager.AppSettings["ErrorLogPath"];

            //Calls the constructor for CardsDAO so this controller has access to the DataLayer
            dataAccess = new CardsDAO(connectionString, errorLogPath);
            logAccess = new ActionLogger(logPath);
        }

        //Index/View all cards
        [HttpGet]
        public ActionResult Index()
        {
            //Declaring local variables
            List<CardPO> mappedCards = new List<CardPO>();
            ActionResult oResponse = RedirectToAction("Index", "Home");
            string currentMethod = "Index";

            try
            {
                //Calling method to return a list of all cards from the database
                List<CardDO> cardsData = dataAccess.ReadCards();
                if (cardsData.Count != 0)
                {
                    mappedCards = CardMapper.MapDOToPO(cardsData);
                    oResponse = View(mappedCards);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "No cards are in the catalog!";
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Cannot display the Card Catalogue at this time!";
            }

            //Logging user action
            if (Session["UserId"] == null)
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, 0, "View all");
            }
            else
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], "View all");
            }

            return oResponse;
        }

        //Method that calls the Create view for a Card
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2)]
        public ActionResult Create()
        {
            return View();
        }

        //Method that takes the passed CardPO object and attempts to add it to the database
        [HttpPost]
        [SecurityFilter("RoleID", 1, 2)]
        //Todo: Add security to all post backs!!!
        public ActionResult Create(CardPO form)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Cards");
            string currentMethod = "Create";

            //Model state check
            if (ModelState.IsValid)
            {
                try
                {
                    CardDO card = CardMapper.MapPOToDO(form);
                    //Calling method to add a card to the database
                    dataAccess.CreateCard(card);

                    //Setting confirmation message
                    TempData["Message"] = $"{form.CardName} was added to the catalogue.";
                }
                catch (Exception ex)
                {
                    //Setting error message
                    TempData["Message"] = $"{form.CardName} could not be added to the catalogue.";
                    //Setting redirect
                    oResponse = View(form);
                }
            }
            else
            {
                //Redirecting to current view if the form was filled out incorrectly
                oResponse = View(form);
            }

            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], form);

            return oResponse;
        }

        //Method that takes the user to the Update card page for the passed cardID
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2)]
        public ActionResult Update(long cardID)
        {
            //Declaring local variables
            CardPO viewCard = new CardPO();
            ActionResult oResponse = RedirectToAction("Index", "Cards");

            try
            {
                //Calling a method to read a card to a data obect
                CardDO dataCard = dataAccess.ReadCardAtID(cardID);
                if (dataCard.CardID != 0)
                {
                    viewCard = CardMapper.MapDOToPO(dataCard);
                    //Setting redirect to current view
                    oResponse = View(viewCard);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "Card does not exist!";
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Card couldn't be accessed, please try again later!";
            }
            return oResponse;
        }

        //Method that takes the passed CardPO object and attempts to update the matching object in the database
        [HttpPost]
        [SecurityFilter("RoleID", 1, 2)]
        public ActionResult Update(CardPO form)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Cards");
            string currentMethod = "Update";

            //Model state check
            if (ModelState.IsValid)
            {
                try
                {
                    CardDO dataCard = CardMapper.MapPOToDO(form);
                    //Calling method to update a card in the database
                    dataAccess.UpdateCard(dataCard);
                }
                catch (Exception ex)
                {
                    //Setting error message
                    TempData["Message"] = "There was a problem updating the card, please try again later!";
                    //Setting redirect
                    oResponse = View(form);

                }
            }
            else
            {
                //Setting redirect
                oResponse = View(form);
            }

            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], form);

            return oResponse;
        }

        //Method that starts the Delete process for a Card
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2)]
        public ActionResult Delete(long cardID)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Cards");
            string currentMethod = "Delete";

            try
            {
                //Calling method to delete card with passed ID
                if (cardID != 0)
                {
                    //Calling method to delete the card with the passed cardID
                    dataAccess.DeleteCard(cardID);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "That card doesn't exist!";
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Card couldn't be removed at this time.";
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], $"Delete Card {cardID}");

            return oResponse;
        }
    }
}
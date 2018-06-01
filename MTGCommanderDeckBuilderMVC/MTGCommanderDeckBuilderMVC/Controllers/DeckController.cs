using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeckBuilderDAL;
using DeckBuilderDAL.Models;
using MTGCommanderDeckBuilderMVC.Models;
using MTGCommanderDeckBuilderMVC.Mapping;
using MTGCommanderDeckBuilderMVC.Custom;

namespace MTGCommanderDeckBuilderMVC.Controllers
{
    public class DeckController : Controller
    {
        //Dependencies
        private DeckDAO dataAccess;
        private ActionLogger logAccess;
        public string currentClass = "DeckController";
        private string logPath;

        //Constructor
        public DeckController()
        {
            //Getting config file variables
            string connectionString = ConfigurationManager.ConnectionStrings["serverConnString"].ConnectionString;
            logPath = ConfigurationManager.AppSettings["ActionLogPath"];
            string errorLogPath = ConfigurationManager.AppSettings["ErrorLogPath"];

            //Calls the constructor for CardsDAO so this controller has access to the DataLayer
            dataAccess = new DeckDAO(connectionString, errorLogPath);
            logAccess = new ActionLogger(logPath);
        }

        //Index/View all decks
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3, 4)]
        public ActionResult Index()
        {
            //Declaring local variables
            List<DeckPO> mappedDecks = new List<DeckPO>();
            string currentMethod = "Index";
            ActionResult oResponse = RedirectToAction("Index", "Home");

            try
            {
                //Calling method to return a list of all cards from the database
                List<DeckDO> decksData = dataAccess.ReadDecks();
                if (decksData.Count != 0)
                {
                    mappedDecks = DeckMapper.MapDOToPO(decksData);
                    oResponse = View(mappedDecks);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "No decks are in the catalog";
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Cannot display the Deck Catalogue at this time!";
            }

            //Logging user action
            if (Session["UserId"] == null)
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, 0, "View All");
            }
            else
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], "View All");
            }

            return oResponse;
        }

        //Method that takes the passed username and gets all decks associated with that user
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult UserDecks(long userID)
        {
            //Declaring local variables
            List<DeckPO> mappedDecks = new List<DeckPO>();
            string currentMethod = "UserDecks";
            ActionResult oResponse = RedirectToAction("Index", "Home");

            try
            {
                //Calling method to return a list of all cards from the database
                List<DeckDO> decksData = dataAccess.ReadDecksAtUserID(userID);
                if (decksData.Count != 0)
                {
                    mappedDecks = DeckMapper.MapDOToPO(decksData);
                    //Setting redirect to current View
                    oResponse = View(mappedDecks);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "You don't have any decks! Why don't you make one?";
                    //Setting redirect
                    oResponse = RedirectToAction("Create", "Deck");
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Cannot display the Deck Catalogue at this time!";
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], $"View Decks By User {userID}");

            return oResponse;
        }

        //Method that calls the Create view for a Deck
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Create()
        {
            return View();
        }

        //Method that takes the passed DeckPO object and attempts to add it to the database
        [HttpPost]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Create(DeckPO form)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Deck");
            string currentMethod = "Create";

            //Model state check
            if (ModelState.IsValid)
            {
                try
                {
                    //Todo: replace code to check dropdown value first, then default to Session.
                    if (Session["UserID"] != null)
                    {
                        form.UserID = (long)Session["UserID"];
                    }

                    DeckDO deck = DeckMapper.MapPOToDO(form);
                    //Calling method to add a deck to the database
                    dataAccess.CreateDeck(deck);

                    //Setting confirmation message
                    TempData["Message"] = $"{form.DeckName} was added to the catalogue.";
                }
                catch (Exception ex)
                {
                    //Setting error message
                    TempData["Message"] = $"{form.DeckName} could not be added to the catalogue.";
                    //Setting redirect
                    oResponse = View(form);
                }
            }
            else
            {
                //Setting Redirect
                oResponse = View(form);
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], form);

            return oResponse;
        }

        //Method that takes the user to the Update deck page for the passed deckID
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Update(long deckID)
        {
            //Declaring local variables
            DeckPO viewDeck = new DeckPO();
            ActionResult oResponse = RedirectToAction("Index", "Deck");

            try
            {
                //Calling a method to read a deck to a data object
                DeckDO dataDeck = dataAccess.ReadDeckAtID(deckID);
                //Checking if a deck was found
                if (dataDeck.DeckID != 0)
                {
                    viewDeck = DeckMapper.MapDOToPO(dataDeck);
                    //Setting redirect to current View
                    oResponse = View(viewDeck);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "That deck doesn't exist!";
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Deck couldn't be accessed, please try again later!";
            }
            return oResponse;
        }

        //Method that takes the passed DeckPO object and attempts to update the matching object in the database
        [HttpPost]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Update(DeckPO form)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Deck");
            string currentMethod = "Update";

            //Model state check
            if (ModelState.IsValid)
            {
                try
                {
                    DeckDO dataDeck = DeckMapper.MapPOToDO(form);
                    //Calling method to update a deck in the database
                    dataAccess.UpdateDeck(dataDeck);
                }
                catch (Exception ex)
                {
                    //Setting error message
                    TempData["Message"] = "There was a problem updating the deck, please try again later!";
                    //Setting redirect
                    oResponse = View(form);
                }
            }
            else
            {
                //Setting redirect
                oResponse = View(form);
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], form);

            return oResponse;
        }

        //Method that starts the Delete process for a Deck
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Delete(long deckID)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Deck");
            string currentMethod = "Delete";

            try
            {
                if (deckID != 0)
                {
                    //Calling method to delete deck with passed ID
                    dataAccess.DeleteDeck(deckID);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "That deck doesn't exist!";
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "Card couldn't be removed at this time.";
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], $"Delete Deck {deckID}");

            return oResponse;
        }
    }
}
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
    public class AccountController : Controller
    {
        //Dependencies
        private UserDAO dataAccess;
        private RolesDAO roleAccess;
        private ActionLogger logAccess;
        public string currentClass = "AccountController";
        private string logPath;

        //Constructor
        public AccountController()
        {
            //Getting config file variables
            string connectionString = ConfigurationManager.ConnectionStrings["serverConnString"].ConnectionString;
            logPath = ConfigurationManager.AppSettings["ActionLogPath"];
            string errorLogPath = ConfigurationManager.AppSettings["ErrorLogPath"];

            //Calls the constructor for UserDAO so this contoller has access to the DataLayer
            dataAccess = new UserDAO(connectionString, errorLogPath);
            roleAccess = new RolesDAO(connectionString, errorLogPath);
            logAccess = new ActionLogger(logPath);
        }

        //Index/View all Users
        [HttpGet]
        [SecurityFilter("RoleID", 1)]
        public ActionResult Index()
        {
            //Declaring local variables
            List<UserPO> mappedUsers = new List<UserPO>();
            string currentMethod = "Index";
            ActionResult oResponse = RedirectToAction("Index", "Home");

            try
            {
                //Reading users into a list of data objects
                List<UserDO> usersData = dataAccess.ReadUsers();
                List<string> roleNames = new List<string>();
                //Checking if user was found
                if (usersData.Count != 0)
                {
                    foreach (UserDO item in usersData)
                    {
                        roleNames.Add(roleAccess.ReadRoleNameAtID(item.RoleID));
                        ViewBag.roleNames = roleNames;
                    }
                    //Mapping data objects to a list of presentation objects
                    mappedUsers = UserMapper.MapDOToPO(usersData);
                    //Setting redirect to current View;
                    oResponse = View(mappedUsers);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "There are no accounts registered!";
                }
            }
            catch (Exception ex)
            {
                //Setting an error message if there was a problem reading data
                TempData["Message"] = "Cannot display the Users list at this time!";
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], "View all");

            return oResponse;
        }

        //Method that calls Login page
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Method that sets the Session information depending on whether the user successfully logged in or not.
        [HttpPost]
        public ActionResult Login(LoginPO form)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Home");
            string currentMethod = "Login";

            //Checking model state
            if (ModelState.IsValid)
            {
                try
                {
                    //Calling method to read User table at the provided Username
                    UserDO storedUser = dataAccess.ReadUserAtUsername(form.Username);

                    //Setting session variables if a valid login was provided
                    if (storedUser != null && form.Password.Equals(storedUser.Password))
                    {
                        Session["RoleID"] = storedUser.RoleID;
                        Session["Username"] = storedUser.Username;
                        Session["UserID"] = storedUser.UserID;
                        Session.Timeout = 10;
                    }
                    else
                    {
                        //Setting redirect
                        oResponse = RedirectToAction("Login", "Account");
                        //Setting error message
                        TempData["Message"] = "No matching username and/or password could be found";
                    }
                }
                catch (Exception ex)
                {
                    //Setting redirect
                    oResponse = RedirectToAction("Login", "Account");
                    //Setting error message
                    TempData["Message"] = "There was a problem logging in, please try again later";
                }
            }
            else
            {
                //Setting redirect
                TempData["Message"] = "User does not exist, please check your login information!";
                oResponse = View(form);
            }

            //Logging user action
            if (Session["UserId"] == null)
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, 0, "Failed Login");
            }
            else
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], "Logged In");
            }

            return oResponse;
        }

        //Method that removes the current Session information and redirects to the Home screen
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Logout()
        {
            //Declaring local variables
            string currentMethod = "Logout";

            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], "Logged Out");

            //Clearing current session
            Session.Abandon();
            Session["RoleID"] = 4;
            return RedirectToAction("Index", "Home");
        }

        //Method takes the user to the Register screen
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //Method that takes the passed UserPO object and attempts to add it to the database
        [HttpPost]
        public ActionResult Create(UserPO form)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Login", "Account");
            string currentMethod = "Create";

            //Checking model state
            if (ModelState.IsValid)
            {
                try
                {
                    //Setting role ID
                    form.RoleID = 3;
                    UserDO dataObject = UserMapper.MapPOToDO(form);
                    //Creating user registry in the database
                    dataAccess.CreateUser(dataObject);
                    TempData["Message"] = "New Account Created";
                }
                catch (Exception ex)
                {
                    //Setting redirect
                    oResponse = View(form);
                    //Setting error message
                    TempData["Message"] = "Could not make an account at this time.  Please try again later";
                }
            }
            else
            {
                //Setting redirect
                oResponse = View(form);
            }

            //Logging user action
            if (Session["UserId"] == null)
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, 0, $"Create User {form.UserID}");
            }
            else
            {
                logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], $"Create User {form.UserID}");
            }

            return oResponse;
        }

        //Method that takes the User to the Update profile page
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Update(long userID)
        {
            //Declaring local variables
            UserPO viewUser = new UserPO();
            ActionResult oResponse = RedirectToAction("Index", "Home");

            try
            {
                //Calling method to return user info from the given username
                UserDO dataUser = dataAccess.ReadUserAtID(userID);
                //Checking if user was found
                if (dataUser.UserID != 0)
                {
                    viewUser = UserMapper.MapDOToPO(dataUser);
                    //Setting redirect to current view
                    oResponse = View(viewUser);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "User does not exist!";
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "User couldn't be updated at this time.";
            }
            return oResponse;
        }

        //Method that takes the passed UserPO object and attempts to update the matching one in the database
        [HttpPost]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult Update(UserPO form, FormCollection from)
        {
            //Declaring local variables
            ActionResult oResponse;
            string currentMethod = "Update";

            //Setting redirect back to the Account index if current user is an admin, otherwise the home page
            if (Session["RoleID"] != null && (int)Session["RoleID"] == 1 && Request.Form["roleID"] != null)
            {
                form.RoleID = Convert.ToInt32(Request.Form["roleID"]);
                oResponse = RedirectToAction("Index", "Account");
            }
            else
            {
                oResponse = RedirectToAction("Index", "Home");
            }

            //Checking model state
            if (ModelState.IsValid)
            {
                try
                {
                    UserDO dataUser = UserMapper.MapPOToDO(form);
                    //Calling method to update User data
                    dataAccess.UpdateUser(dataUser);
                    //Setting success message
                    TempData["Message"] = "Account successfully updated";
                }
                catch (Exception ex)
                {
                    //Setting redirect
                    oResponse = View(form);
                    //Setting error message
                    TempData["Message"] = "Could not update user at this time, please try again later";
                }
            }
            else
            {
                //Setting redirect
                oResponse = View(form);
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], $"Update User {form.UserID}");

            return oResponse;
        }

        //Method that starts the Update password process for a User
        [HttpGet]
        [SecurityFilter("RoleID", 1, 2, 3)]
        public ActionResult UpdatePassword(long userID = 0)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Account");
            UserPO viewUser = new UserPO();
            string currentMethod = "UpdatePassword";

            try
            {
                //Calling method to return user info from the given username
                UserDO dataUser = dataAccess.ReadUserAtID(userID);
                //Checking if user was found
                if (dataUser.UserID != 0)
                {
                    viewUser = UserMapper.MapDOToPO(dataUser);
                    //Setting redirect to current view
                    oResponse = View(viewUser);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "User does not exist!";
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "User password couldn't be updated at this time.";
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], $"Update Password of User {userID}");

            return oResponse;
        }

        //Method that takes the passed UserPO object and attempts to update the matching one in the database

        //Method that starts the Delete process for a User
        [HttpGet]
        [SecurityFilter("RoleID", 1)]
        public ActionResult Delete(long userID)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "Account");
            string currentMethod = "Delete";

            try
            {
                //Reading database for specified userID in order to verify their RoleID
                UserDO dataUser = dataAccess.ReadUserAtID(userID);
                //Checking if user was found and it wasn't an Administrator
                if (dataUser.UserID != 0 && dataUser.RoleID != 1)
                {
                    //Calling method to delete specified User
                    dataAccess.DeleteUser(userID);
                }
                else
                {
                    //Setting error message
                    TempData["Message"] = "User does not exist!";
                }
            }
            catch (Exception ex)
            {
                //Setting error message
                TempData["Message"] = "User couldn't be deleted at this time.";
            }

            //Logging user action
            logAccess.ActionLogging("Information", currentClass, currentMethod, (long)Session["UserID"], $"Delete User {userID}");

            return oResponse;
        }
    }
}
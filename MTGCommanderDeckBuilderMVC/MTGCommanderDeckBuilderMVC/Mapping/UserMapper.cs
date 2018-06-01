using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeckBuilderDAL.Models;
using MTGCommanderDeckBuilderMVC.Models;

namespace MTGCommanderDeckBuilderMVC.Mapping
{
    public static class UserMapper
    {
        //Method for mapping a UserDO to a UserPO
        public static UserPO MapDOToPO(UserDO fromItem)
        {
            //Declaring local variables
            UserPO toItem = new UserPO();
            //Null check
            if (fromItem != null)
            {
                //Transferring property values from passed UserDO to a UserPO
                toItem.UserID = fromItem.UserID;
                toItem.Username = fromItem.Username;
                toItem.Password = fromItem.Password;
                toItem.FirstName = fromItem.FirstName;
                toItem.LastName = fromItem.LastName;
                toItem.EmailAddress = fromItem.EmailAddress;
                toItem.RoleID = fromItem.RoleID;
            }

            return toItem;
        }

        //Method for mapping a list of UserDOs to a list of UserPOs
        public static List<UserPO> MapDOToPO(List<UserDO> fromList)
        {
            //Declaring local variable
            List<UserPO> toList = new List<UserPO>();
            //Null check
            if(fromList != null)
            {
                //Adding a UserPO object to toList for each UserDO object in fromList
                foreach (UserDO item in fromList)
                {
                    UserPO mappedItem = MapDOToPO(item);
                    toList.Add(mappedItem);
                }
            }

            return toList;
        }

        //Method for mapping a UserPO to a UserDO
        public static UserDO MapPOToDO(UserPO fromItem)
        {
            //Declaring local variables
            UserDO toItem = new UserDO();
            //Null check
            if (fromItem != null)
            {
                //Transferring property values from UserPO to UserDO
                toItem.UserID = fromItem.UserID;
                toItem.Username = fromItem.Username;
                toItem.Password = fromItem.Password;
                toItem.FirstName = fromItem.FirstName;
                toItem.LastName = fromItem.LastName;
                toItem.EmailAddress = fromItem.EmailAddress;
                toItem.RoleID = fromItem.RoleID;
            }

            return toItem;
        }

        //Method for mapping a list of UserPOs to a list of UserDOs
        public static List<UserDO> MapDOToPO(List<UserPO> fromList)
        {
            //Declaring local variable
            List<UserDO> toList = new List<UserDO>();
            //Null check
            if (fromList != null)
            {
                //Adding a UserDO object to toList for each UserPO object in fromList
                foreach (UserPO item in fromList)
                {
                    UserDO mappedItem = MapPOToDO(item);
                    toList.Add(mappedItem);
                }
            }

            return toList;
        }
    }
}
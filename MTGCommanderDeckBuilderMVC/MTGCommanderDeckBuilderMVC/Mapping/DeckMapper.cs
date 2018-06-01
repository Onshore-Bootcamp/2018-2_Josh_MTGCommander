using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeckBuilderDAL.Models;
using MTGCommanderDeckBuilderMVC.Models;

namespace MTGCommanderDeckBuilderMVC.Mapping
{
    public class DeckMapper
    {
        //Method for mapping a DeckDo to a DeckPO
        public static DeckPO MapDOToPO(DeckDO fromItem)
        {
            //Declaring local variables
            DeckPO toItem = new DeckPO();
            //Null check
            if (fromItem != null)
            {
                //Transferring property values from passed DeckDO to a DeckPO
                toItem.DeckID = fromItem.DeckID;
                toItem.UserID = fromItem.UserID;
                toItem.DeckName = fromItem.DeckName;
                toItem.CommanderName = fromItem.CommanderName;
                toItem.DeckColors = fromItem.DeckColors;
                toItem.DeckArchetype = fromItem.DeckArchetype;
            }

            return toItem;
        }

        //Method for mapping a list of DeckDOs to a list of DeckPOs
        public static List<DeckPO> MapDOToPO(List<DeckDO> fromList)
        {
            //Declaring local variables
            List<DeckPO> toList = new List<DeckPO>();
            //Null check
            if(fromList != null)
            {
                //Adding a DeckPO object to toList for each DeckDO object in fromList
                foreach (DeckDO item in fromList)
                {
                    DeckPO mappedItem = MapDOToPO(item);
                    toList.Add(mappedItem);
                }
            }

            return toList;
        }

        //Method for mapping a DeckPO to a DeckDO
        public static DeckDO MapPOToDO(DeckPO fromItem)
        {
            //Declaring local variables
            DeckDO toItem = new DeckDO();
            //Null check
            if (fromItem != null)
            {
                //Transferring property values from passed DeckPO to a DeckDO
                toItem.DeckID = fromItem.DeckID;
                toItem.UserID = fromItem.UserID;
                toItem.DeckName = fromItem.DeckName;
                toItem.CommanderName = fromItem.CommanderName;
                toItem.DeckColors = fromItem.DeckColors;
                toItem.DeckArchetype = fromItem.DeckArchetype;
            }

            return toItem;
        }

        //Method for mapping a list of DeckPOs to a list of DeckDOs
        public static List<DeckDO> MapPOToDO(List<DeckPO> fromList)
        {
            //Declaring local variables
            List<DeckDO> toList = new List<DeckDO>();
            //Null check
            if (fromList != null)
            {
                //Adding a DeckDO object to toList for each DeckPO object in fromList
                foreach (DeckPO item in fromList)
                {
                    DeckDO mappedItem = MapPOToDO(item);
                    toList.Add(mappedItem);
                }
            }
            return toList;
        }
    }
}
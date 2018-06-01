using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeckBuilderBAL.Models;
using DeckBuilderDAL.Models;
using MTGCommanderDeckBuilderMVC.Models;

namespace MTGCommanderDeckBuilderMVC.Mapping
{
    public class CardMapper
    {
        //Method for mapping a CardDO to a CardPO
        public static CardPO MapDOToPO(CardDO fromItem)
        {
            //Declaring local variable
            CardPO toItem = new CardPO();
            //Null check
            if (fromItem != null)
            {
                //Transferring property values from passed CardDO to a CardPO
                toItem.CardID = fromItem.CardID;
                toItem.CardName = fromItem.CardName;
                toItem.ManaCost = fromItem.ManaCost;
                toItem.CardType = fromItem.CardType;
                toItem.ColorIdentity = fromItem.ColorIdentity;
                toItem.Abilities = fromItem.Abilities;
                toItem.CardStats = fromItem.CardStats;
            }

            return toItem;
        }

        //Method for mapping a list of CardDOs to a list of CardPOs
        public static List<CardPO> MapDOToPO(List<CardDO> fromList)
        {
            //Declaring local variable
            List<CardPO> toList = new List<CardPO>();
            //Null check
            if (fromList != null)
            {
                //Adding a CardPO object to toList for each CardDO object in fromList
                foreach (CardDO item in fromList)
                {
                    CardPO mappedItem = MapDOToPO(item);
                    toList.Add(mappedItem);
                }
            }

            return toList;
        }

        //Method for mapping a CardPO to a CardDO
        public static CardDO MapPOToDO(CardPO fromItem)
        {
            //Declaring local variable
            CardDO toItem = new CardDO();
            //Null check
            if (fromItem != null)
            {
                //Transferring property values from passed CardPO to a CardDO
                toItem.CardID = fromItem.CardID;
                toItem.CardName = fromItem.CardName;
                toItem.ManaCost = fromItem.ManaCost;
                toItem.CardType = fromItem.CardType;
                toItem.ColorIdentity = fromItem.ColorIdentity;
                toItem.Abilities = fromItem.Abilities;
                toItem.CardStats = fromItem.CardStats;
            }

            return toItem;
        }

        //Method for mapping a list of CardPOs to a list of CardDOs
        public static List<CardDO> MapPOToDO(List<CardPO> fromList)
        {
            //Declaring local variable
            List<CardDO> toList = new List<CardDO>();
            //Null check
            if (fromList != null)
            {
                //Adding a CardPO object to toList for each CardDO object in fromList
                foreach (CardPO item in fromList)
                {
                    CardDO mappedItem = MapPOToDO(item);
                    toList.Add(mappedItem);
                }
            }
            return toList;
        }

        //Method for mapping a CardPO to a CardBO
        public static CardBO MapPOToBO(CardPO fromItem)
        {
            //Declaring local variable
            CardBO toItem = new CardBO();
            //Null check
            if (fromItem != null)
            {
                //Transferring property values from passed CardPO to a CardBO
                toItem.CardID = fromItem.CardID;
                toItem.CardName = fromItem.CardName;
                toItem.ManaCost = fromItem.ManaCost;
                toItem.CardType = fromItem.CardType;
                toItem.ColorIdentity = fromItem.ColorIdentity;
                toItem.Abilities = fromItem.Abilities;
                toItem.CardStats = fromItem.CardStats;
            }

            return toItem;
        }

        //Method for mapping a list of CardPOs to a list of CardBOs
        public static List<CardBO> MapPOToBO(List<CardPO> fromList)
        {
            //Declaring local variable
            List<CardBO> toList = new List<CardBO>();
            //Null check
            if (fromList != null)
            {
                //Adding a CardBO object to toList for each CardPO object in fromList
                foreach (CardPO item in fromList)
                {
                    CardBO mappedItem = MapPOToBO(item);
                    toList.Add(mappedItem);
                }
            }

            return toList;
        }

        //Method for mapping a CardBO to a CardPO
        public static CardPO MapBOToPO(CardBO fromItem)
        {
            //Declaring local variable
            CardPO toItem = new CardPO();
            //Null check
            if (fromItem != null)
            {
                //Transferring property values from passed CardBO to a CardPO
                toItem.CardID = fromItem.CardID;
                toItem.CardName = fromItem.CardName;
                toItem.ManaCost = fromItem.ManaCost;
                toItem.CardType = fromItem.CardType;
                toItem.ColorIdentity = fromItem.ColorIdentity;
                toItem.Abilities = fromItem.Abilities;
                toItem.CardStats = fromItem.CardStats;
            }

            return toItem;
        }

        //Method for mapping a list of CardBOs to a list of CardPOs
        public static List<CardPO> MapBOToPO(List<CardBO> fromList)
        {
            //Declaring local variable
            List<CardPO> toList = new List<CardPO>();
            //Null check
            if (fromList != null)
            {
                //Adding a CardPO object to toList for each CardBO object in fromList
                foreach (CardBO item in fromList)
                {
                    CardPO mappedItem = MapBOToPO(item);
                    toList.Add(mappedItem);
                }
            }

            return toList;
        }
    }
}

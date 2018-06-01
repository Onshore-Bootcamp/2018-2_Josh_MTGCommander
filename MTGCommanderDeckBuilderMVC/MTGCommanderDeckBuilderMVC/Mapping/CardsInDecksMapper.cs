using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeckBuilderDAL.Models;
using MTGCommanderDeckBuilderMVC.Models;

namespace MTGCommanderDeckBuilderMVC.Mapping
{
    public class CardsInDecksMapper
    {
        //Method for mapping a CardInDeckDO to a CardInDeckPO
        public static CardInDeckPO MapDOToPO(CardInDeckDO fromItem)
        {
            //Declaring local variable
            CardInDeckPO toItem = new CardInDeckPO();
            //Null check
            if (fromItem != null)
            {
                //Transferring property values from passed CardInDeckDO to a CardInDeckPO
                toItem.CardID = fromItem.CardID;
                toItem.DeckID = fromItem.DeckID;
            }

            return toItem;
        }
    }
}
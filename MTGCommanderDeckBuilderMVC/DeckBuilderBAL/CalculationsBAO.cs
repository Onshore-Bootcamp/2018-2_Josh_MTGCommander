using DeckBuilderBAL.Models;
using System.Collections.Generic;

namespace DeckBuilderBAL
{
    public class CalculationsBAO
    {
        //Constructor
        public CalculationsBAO()
        {

        }

        //Method that calculates the mana curve of the passed list of CardDOs
        public short[] GetManaCurve(List<CardBO> cardList)
        {
            //Declaring local variables
            short[] manaData = new short[17];

            foreach(CardBO item in cardList)
            {
                manaData[item.ManaCost]++;
            }

            return manaData;
        }
    }
}

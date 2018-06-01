using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckBuilderBAL.Models
{
    public class CardBO
    {
        //Declaring all object properties for a Card
        public long CardID;
        public string CardName;
        public short ManaCost;
        public string CardType;
        public string ColorIdentity;
        public string Abilities;
        public string CardStats;
    }
}

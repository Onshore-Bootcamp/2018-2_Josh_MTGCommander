using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckBuilderDAL.Models
{
    public class DeckDO
    {
        //Declaring all object properties for a Deck
        public long DeckID;
        public long UserID;
        public string DeckName;
        public string CommanderName;
        public string DeckColors;
        public string DeckArchetype;
    }
}

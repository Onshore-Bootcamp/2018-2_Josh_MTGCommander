using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MTGCommanderDeckBuilderMVC.Models
{
    public class CardInDeckPO
    {
        //Declaring all object properties of a CardInDeck
        [Required]
        public long CardID { get; set; }

        [Required]
        public long DeckID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MTGCommanderDeckBuilderMVC.Models
{
    public class CardPO
    {
        //Declaring all object properties for a Card
        public long CardID { get; set; }

        [Required]
        [Display(Name = "Card Name")]
        [StringLength(50,MinimumLength =1, ErrorMessage ="A card's name must be between 1 and 50 characters long!")]
        public string CardName { get; set; }

        [Display(Name = "Converted Mana Cost")]
        [Range(0,16)]
        public short ManaCost { get; set; }

        [Required]
        [Display(Name = "Type(s)")]
        [StringLength(50, ErrorMessage ="A card's type must be less than 50 characters long!")]
        public string CardType { get; set; }

        [Display(Name = "Color Identity")]
        [StringLength(20, ErrorMessage ="No card has a color identity that long!")]
        public string ColorIdentity { get; set; }

        [Display(Name = "Abilities/Effects")]
        [StringLength(700, ErrorMessage ="Card text must be less than 700 characters long!")]
        public string Abilities { get; set; }

        [Display(Name = "Power/Toughness")]
        [StringLength(10, ErrorMessage ="No card has that base power and toughness!")]
        public string CardStats { get; set; }
    }
}
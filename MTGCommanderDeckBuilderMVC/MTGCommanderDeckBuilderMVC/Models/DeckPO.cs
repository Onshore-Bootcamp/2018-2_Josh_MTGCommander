using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MTGCommanderDeckBuilderMVC.Models
{
    public class DeckPO
    {
        //Declaring all object properties for a Deck
        public long DeckID { get; set; }
        public long UserID { get; set; }

        [Required]
        [Display(Name = "Deck Name")]
        public string DeckName { get; set; }

        [Required]
        [Display(Name = "Commander Name")]
        public string CommanderName { get; set; }

        [Display(Name = "Color Identity")]
        [StringLength(20, ErrorMessage ="A color identity cannot be that long!")]
        public string DeckColors { get; set; }

        [Display(Name = "Archetype")]
        [StringLength(100, ErrorMessage ="Cannot exceed 100 characters!")]
        public string DeckArchetype { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stripovi.Data.Models
{
    public class Porudzbina
    {
        [Key]
        public int IdPorudzbine { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string Grad { get; set; }
        [Display(Name = "Postanski Broj")]
        public int PostanskiBroj { get; set; }
        public string Ulica { get; set; }
        [Display(Name = "Kucni Broj")]
        public string KucniBroj { get; set; }
        public string Placanje { get; set; }
        public string Pitanje { get; set; }
        public int UkupnaCena { get; set; }
        [Display(Name = "Broj Porucenih Stripova")]
        public int BrojPorucenihStripova { get; set; }
        public string Status { get; set; }
        [Display(Name = "Vreme Posiljke")]
        public DateTime VremePosiljke { get; set; }
    }
}

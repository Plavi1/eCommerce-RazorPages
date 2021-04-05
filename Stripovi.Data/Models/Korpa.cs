using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stripovi.Data.Models
{
    public class Korpa
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        [Column(Order = 2)]
        public int IdStripa { get; set; }
        public string Naziv { get; set; }
        public string Naslov { get; set; }
        public string Izdavac { get; set; }
        [Display(Name = "Vreme Posta")]
        public DateTime VremePosta { get; set; }
        public string Stanje { get; set; }
        public string Jezik { get; set; }
        [Display(Name = "Godina Izdanja")]
        public string GodinaIzdanja { get; set; }
        public int Cena { get; set; }
        public string imgRoute { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stripovi.Data.Models
{
    public class Kontakt
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Ime i Prezime")]
        public string ImePrezime { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Razlog { get; set; }
        [Required]
        public string Naslov { get; set; }
        [Required]
        public string Komentar { get; set; }
    }
}

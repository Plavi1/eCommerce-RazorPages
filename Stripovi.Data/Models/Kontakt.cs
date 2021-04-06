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
        [Required(ErrorMessage = "Niste upisali Ime i Prezime")]
        [Display(Name = "Ime i Prezime")]
        public string ImePrezime { get; set; }
        [Required(ErrorMessage = "Niste upisali Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Niste upisali Razlog")]
        public string Razlog { get; set; }
        [Required(ErrorMessage = "Niste upisali Naslov")]
        public string Naslov { get; set; }
        [Required(ErrorMessage = "Niste napisali Komentar")]
        public string Komentar { get; set; }
    }
}

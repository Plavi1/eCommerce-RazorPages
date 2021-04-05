using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stripovi.Data.Models
{
    public class Strip
    {
        [Key]
        public int IdStripa { get; set; }
        [Required(ErrorMessage = "Niste upisali Naziv!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Niste upisali Naslov!")]
        public string Naslov { get; set; }
        [Required(ErrorMessage = "Niste upisali Izdavaca!")]
        public string Izdavac { get; set; }
        [Required(ErrorMessage = "Niste upisali Stanje Stripa! (polovno,novo)")]
        public string Stanje { get; set; }
        [Required(ErrorMessage = "Niste upisali Jezik na kome je napisan Strip!")]
        public string Jezik { get; set; }
        [Required(ErrorMessage = "Niste upisali godinu izdanja!")]
        [Display(Name = "Godina Izdanja")]
        public string GodinaIzdanja { get; set; }
        [Required(ErrorMessage = "Niste upisali cenu!")]
        public int Cena { get; set; }
        public string imgRoute { get; set; }
    }
}

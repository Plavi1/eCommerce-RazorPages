using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripovi.Data.Models;
using Stripovi.Data.Repositorys.KontaktRepository;

namespace Stripovi.Web.Pages
{
    public class KontaktModel : PageModel
    {
        private readonly IKontaktRepository kontaktRepository;

        public KontaktModel(IKontaktRepository kontaktRepository)
        {
            this.kontaktRepository = kontaktRepository;
        }
        [BindProperty]
        public Kontakt Kontakt { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await kontaktRepository.AddKontakt(Kontakt);
                TempData["message"] = "Uspesno ste poslali poruku!";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

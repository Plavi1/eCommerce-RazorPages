using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripovi.Data.Models;
using Stripovi.Data.Repositorys.PorudzbinaRepository;

namespace Stripovi.Web.Pages
{
    [Authorize]
    public class StatusPorudzbineModel : PageModel
    {
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly SignInManager<IdentityUser> signInManager;

        public StatusPorudzbineModel(IPorudzbinaRepository porudzbinaRepository,
                                      SignInManager<IdentityUser> signInManager)
        {
            this.porudzbinaRepository = porudzbinaRepository;
            this.signInManager = signInManager;
        }
        public List<KompletnaPorudzbini> Porudzbina { get; set; }
        [BindProperty]
        public int IdPorudzbine { get; set; }
        public class KompletnaPorudzbini
        {
            public Porudzbina Porudzbina { get; set; }
            public IEnumerable<Strip> Stripovi { get; set; }
        }

        public async Task OnGet()
        {
            string userId = signInManager.UserManager.GetUserId(User);
            var UserPorudzbine = porudzbinaRepository.GetPorudzbine().Result.Where(e => e.UserId == userId);

            List<KompletnaPorudzbini> kompletnaPorudzbina = new List<KompletnaPorudzbini>();

            foreach (var item in UserPorudzbine)
            {
                KompletnaPorudzbini porudzbina = new KompletnaPorudzbini
                {
                    Porudzbina = item,
                    Stripovi = await porudzbinaRepository.GetSveStripoveuPorudzbini(item.IdPorudzbine)
                };
                kompletnaPorudzbina.Add(porudzbina);
            }
            Porudzbina = kompletnaPorudzbina;
        }
        public async Task OnPost()
        {
            var selektovanaPorudzbina = await porudzbinaRepository.GetPorudzbinu(IdPorudzbine);

            if (selektovanaPorudzbina != null)
            {
                var result = await porudzbinaRepository.DeletePorudzbinu(selektovanaPorudzbina.IdPorudzbine);
                if (result == null)
                {
                    NotFound();
                }
                TempData["message"] = "Porudzbina je uspesno obustavljena!";
            }
            await OnGet();
        }
    }
}

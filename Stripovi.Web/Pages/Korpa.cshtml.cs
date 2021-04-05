using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripovi.Data.Models;
using Stripovi.Data.Repositorys.KorpaRepository;
using Stripovi.Data.Repositorys.PorudzbinaRepository;

namespace Stripovi.Web.Pages
{
    [Authorize]
    public class KorpaModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IKorpaRepository korpaRepository;
        private readonly IPorudzbinaRepository porudzbinaRepository;

        public KorpaModel(SignInManager<IdentityUser> signInManager,
                          IKorpaRepository korpaRepository,
                          IPorudzbinaRepository porudzbinaRepository)
        {
            this.signInManager = signInManager;
            this.korpaRepository = korpaRepository;
            this.porudzbinaRepository = porudzbinaRepository;
        }
        public int IdStripaObrisi { get; set; }
        public int UkupnaCena { get; set; }
        public InputBuyConfirmed BuyConfirmed { get; set; }
        public IEnumerable<Korpa> StripoviuKorpi { get; set; }

        public class InputBuyConfirmed
        {
            [Required(ErrorMessage = "Niste upisali Grad!")]
            public string Grad { get; set; }

            [Required(ErrorMessage = "Niste upisali Postanski broj!")]
            [Display(Name = "Postanski Broj")]
            public int? PostanskiBroj { get; set; }

            [Required(ErrorMessage = "Niste upisali Ulicu!")]
            public string Ulica { get; set; }

            [Required(ErrorMessage = "Niste upisali Kucni Broj!")]
            [Display(Name = "Kucni Broj")]
            public string KucniBroj { get; set; }
            public string Placanje { get; set; }
            public string Pitanje { get; set; }
            [Required]
            [Range(typeof(bool), "true", "true", ErrorMessage = "Nisi stiklirao!")]
            public bool SlozenSaUslovima { get; set; }
            public List<int> IdPorucenihStripova { get; set; }
            public int UkupnaCena { get; set; }
        }
        private async Task GetSveInformacije()
        {
            string ulogvanUser = signInManager.UserManager.GetUserId(User);

            var stripoviuKorpi = await korpaRepository.UserStripoviuKorpi(ulogvanUser);

            int ukupnaCena = 0;

            foreach (var item in stripoviuKorpi)
            {
                ukupnaCena += item.Cena;
            }

            UkupnaCena = ukupnaCena;
            StripoviuKorpi = stripoviuKorpi;
        }
        //-----------------------------------------OnGet------------------------------------------------
        public async Task OnGet()
        {
            await GetSveInformacije();
        }
        //-----------------------------------------OnPostObrisi------------------------------------------------
        public async Task OnPostObrisi(int IdStripaObrisi)
        {
            var selektovanStripuKorpi = await korpaRepository.GetKorpa(IdStripaObrisi);

            if (selektovanStripuKorpi != null)
            {
                var result = await korpaRepository.DeleteStripUKorpi(selektovanStripuKorpi.IdStripa);
                if (result == null)
                {
                    NotFound();
                }
                TempData["message"] = "Strip je obrisan iz korpe!";
                await GetSveInformacije();
            }
            await GetSveInformacije();
        }

        //-----------------------------------------OnPostPostvrdi------------------------------------------------
        public async Task<IActionResult> OnPostPotvrdi(InputBuyConfirmed BuyConfirmed)
        {
            if (ModelState.IsValid)
            {
                string userId = signInManager.UserManager.GetUserId(User);

                Porudzbina porudzbina = new Porudzbina
                {
                    UserId = userId,
                    VremePosiljke = DateTime.Now,
                    BrojPorucenihStripova = BuyConfirmed.IdPorucenihStripova.Count,
                    Pitanje = BuyConfirmed.Pitanje,
                    Grad = BuyConfirmed.Grad,
                    KucniBroj = BuyConfirmed.KucniBroj,
                    Placanje = BuyConfirmed.Placanje,
                    UkupnaCena = BuyConfirmed.UkupnaCena,
                    Ulica = BuyConfirmed.Ulica,
                    PostanskiBroj = BuyConfirmed.PostanskiBroj.Value,
                    Status = "Pakovanje"
                };
                await porudzbinaRepository.AddPorudzbinu(porudzbina, BuyConfirmed.IdPorucenihStripova);
                await korpaRepository.DeleteSveUKorpi(userId);
                TempData["message"] = "Uspesno ste izvrsili porudzbinu!";
                return RedirectToPage("Index");
            }

            await GetSveInformacije();
            return Page();
        }
    }
}

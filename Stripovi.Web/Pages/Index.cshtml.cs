using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Stripovi.Data.Models;
using Stripovi.Data.Repositorys.KorpaRepository;
using Stripovi.Data.Repositorys.PorudzbinaRepository;
using Stripovi.Data.Repositorys.StripRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripovi.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IStripRepository stripRepository;
        private readonly IKorpaRepository korpaRepository;
        private readonly IMapper mapper;
        private readonly IPorudzbinaRepository porudzbinaRepository;

        public IndexModel(ILogger<IndexModel> logger,
                          SignInManager<IdentityUser> signInManager,
                          IStripRepository stripRepository,
                          IKorpaRepository korpaRepository,
                          IMapper mapper,
                          IPorudzbinaRepository porudzbinaRepository)
        {
            _logger = logger;
            this.signInManager = signInManager;
            this.stripRepository = stripRepository;
            this.korpaRepository = korpaRepository;
            this.mapper = mapper;
            this.porudzbinaRepository = porudzbinaRepository;
        }

        public IEnumerable<Strip> Stripovi { get; set; }

        [BindProperty]
        public int IdStripaZaKorpu { get; set; }

        public string Search { get; set; }

        public int brojStripovauProdaji { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("SuperAdmin"))
                {
                    return RedirectToPage("Administrator");
                }
                var ulogvanUser = signInManager.UserManager.GetUserId(User);

                Stripovi = await stripRepository.UserStripoviVanKorpe(ulogvanUser);

            }
            else
            {
                Stripovi = await stripRepository.GetStripove();
            }
            brojStripovauProdaji = Stripovi.Count();

            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (signInManager.IsSignedIn(User))
            {
                string ulogvanUser = signInManager.UserManager.GetUserId(User);
                int brojPorudzbina = porudzbinaRepository.GetPorudzbine().Result.Where(e => e.UserId == ulogvanUser).Count();

                if (brojPorudzbina < 3)
                {
                    var strip = await stripRepository.GetStrip(IdStripaZaKorpu);

                    if (strip != null)
                    {
                        Korpa novaKorpa = new Korpa();
                        novaKorpa = mapper.Map(strip, novaKorpa);
                        novaKorpa.UserId = ulogvanUser;

                        await korpaRepository.AddKorpa(novaKorpa);
                        return RedirectToAction("OnGetAsync");
                    }
                    TempData["error"] = "Strip nije pronadjen!";
                    return RedirectToAction("OnGetAsync");
                }
                else
                {
                    TempData["error"] = "Maksimalni broj porudzbina je 3!";
                    return RedirectToAction("OnGetAsync");
                }

            }
            TempData["message"] = "Mora te se ulogovati pre nego sto izvrsite porudzbinu!";
            return Redirect("/Identity/Account/Login");
        }

        public async Task<IActionResult> OnPostSearch(string Search)
        {
            string ulogvanUser = signInManager.UserManager.GetUserId(User);

            if (Search != null)
            {
                Stripovi = await stripRepository.Search(Search, ulogvanUser);
                brojStripovauProdaji = Stripovi.Count();
                return Page();
            }
            Stripovi = await stripRepository.UserStripoviVanKorpe(ulogvanUser);
            brojStripovauProdaji = Stripovi.Count();
            return Page();
        }
    }
}

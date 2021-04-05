using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stripovi.Data.Data;
using Stripovi.Data.Models;
using Stripovi.Data.Repositorys.PorudzbinaRepository;

namespace Stripovi.Web.Pages.Administrator.Porudzbine
{
    [Authorize(Roles = "SuperAdmin")]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IPorudzbinaRepository porudzbinaRepository;

        public DetailsModel(ApplicationDbContext context,
                            IPorudzbinaRepository porudzbinaRepository)
        {
            _context = context;
            this.porudzbinaRepository = porudzbinaRepository;
        }
        public Porudzbina Porudzbina { get; set; }
        public IEnumerable<Strip> Stripovi { get; set; }
        [BindProperty]
        public string Status { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Porudzbina = await _context.Porudzbina
                .Include(p => p.User).FirstOrDefaultAsync(m => m.IdPorudzbine == id);

            if (Porudzbina == null)
            {
                return NotFound();
            }

            Stripovi = await porudzbinaRepository.GetSveStripoveuPorudzbini(id.Value);

            return Page();
        }
        public async Task<IActionResult> OnPost(int? id)
        {
            if (Status != null)
            {
                var promenaPorudzbina = await _context.Porudzbina
                .Include(p => p.User).FirstOrDefaultAsync(m => m.IdPorudzbine == id);
                if (promenaPorudzbina == null)
                {
                    return NotFound();
                }
                promenaPorudzbina.Status = Status;
                _context.Attach(promenaPorudzbina).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            return RedirectToPage("./Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripovi.Data.Data;
using Stripovi.Data.Models;

namespace Stripovi.Web.Pages.Administrator.Stripovi
{
    [Authorize(Roles = "SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CreateModel(ApplicationDbContext context,
                           IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
            Strip = new Strip();
        }

        public Strip Strip { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public async Task<IActionResult> OnPostAsync(Strip strip)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Photo != null)
            {
                if (strip.imgRoute != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", strip.imgRoute);
                    System.IO.File.Delete(filePath);
                }
                strip.imgRoute = ProcessUploadedFile();
            }
            _context.Strip.Add(strip);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        private string ProcessUploadedFile()      //Svaki uploadovan fajl ce biti razlicito sacuvan
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}

using Stripovi.Data.Data;
using Stripovi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stripovi.Data.Repositorys.KontaktRepository
{
    public class SQLKontaktRepository : IKontaktRepository
    {
        private readonly ApplicationDbContext context;

        public SQLKontaktRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Kontakt> AddKontakt(Kontakt kontakt)
        {
            var result = await context.Kontakt.AddAsync(kontakt);
            await context.SaveChangesAsync();
            return result.Entity;
        }
        public int brojPoruka()
        {
            return context.Kontakt.Count();
        }
    }
}

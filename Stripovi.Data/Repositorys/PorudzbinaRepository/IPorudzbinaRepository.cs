using Stripovi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stripovi.Data.Repositorys.PorudzbinaRepository
{
    public interface IPorudzbinaRepository
    {
        Task<IEnumerable<Porudzbina>> GetPorudzbine();
        Task<Porudzbina> GetPorudzbinu(int porudzbinaId);
        Task<Porudzbina> AddPorudzbinu(Porudzbina porudzbina, List<int> IdstripovaUPorudzbini);
        Task<Porudzbina> DeletePorudzbinu(int porudzbinaId);
        Task<IEnumerable<Strip>> GetSveStripoveuPorudzbini(int porudzbinaId);
        int UserBrojPorudzbina(string userId);
    }
}

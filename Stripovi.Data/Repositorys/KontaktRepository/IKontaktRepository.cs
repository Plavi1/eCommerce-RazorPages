using Stripovi.Data.Models;
using System.Threading.Tasks;

namespace Stripovi.Data.Repositorys.KontaktRepository
{
    public interface IKontaktRepository
    {
        Task<Kontakt> AddKontakt(Kontakt kontakt);
        int brojPoruka();
    }
}

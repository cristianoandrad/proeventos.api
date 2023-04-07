using ProEventos.Domain.Models;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository.Interfaces
{
    public interface IEventoRepository : IBaseRespository
    {
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
        Task<Evento> GetAllEventoByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}

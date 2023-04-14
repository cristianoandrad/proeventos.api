using ProEventos.Domain.Models;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository.Interfaces
{
    public interface IEventoRepository : IBaseRespository
    {
        Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosAsync(int userId, bool includePalestrantes);
        Task<Evento> GetAllEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false);
    }
}

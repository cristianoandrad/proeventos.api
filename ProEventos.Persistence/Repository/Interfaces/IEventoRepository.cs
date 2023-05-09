using ProEventos.Domain.Models;
using ProEventos.Persistence.Models;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository.Interfaces
{
    public interface IEventoRepository : IBaseRespository
    {
        Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false);
        Task<Evento> GetAllEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false);
    }
}

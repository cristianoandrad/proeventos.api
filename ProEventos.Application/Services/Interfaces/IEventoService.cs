using ProEventos.Application.Dtos;
using ProEventos.Domain.Models;
using System.Threading.Tasks;

namespace ProEventos.Application.Services.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> AddEvento(int userId, EventoDto model);
        Task<EventoDto> UpdateEvento(int userId, int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int userId, int eventoId);
        Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosAsync(int userId, bool includePalestrantes = false);
        Task<EventoDto> GetAllEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false);
    }
}

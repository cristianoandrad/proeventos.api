using ProEventos.Application.Dtos;
using ProEventos.Domain.Models;
using System.Threading.Tasks;

namespace ProEventos.Application.Services.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> AddEvento(EventoDto model);
        Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int eventoId);
        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<EventoDto> GetAllEventoByIdAsync(int eventoId, bool includePalestrantes = false);
    }
}

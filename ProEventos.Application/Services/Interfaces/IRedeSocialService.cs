using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Services.Interfaces
{
    public interface IRedeSocialService
    {
        Task<RedeSocialDto[]>SaveByEvento(int eventoId, RedeSocialDto[] redesSociais);
        Task<bool>DeleteByEvento(int eventoId, int redeSocialId);
        Task<RedeSocialDto[]>SaveByPalestrante(int palestranteId, RedeSocialDto[] redesSociais);
        Task<bool>DeleteByPalestrante(int palestranteId, int redeSocialId);
        Task<RedeSocialDto[]>GetAllByEventoIdAsync(int eventoId);
        Task<RedeSocialDto[]>GetAllByPalestranteIdAsync(int palestranteId);
        Task<RedeSocialDto>GetRedeSocialEventoByIdsAsync(int eventoId, int redeSocialId);
        Task<RedeSocialDto> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int redeSocialId);
    }
}

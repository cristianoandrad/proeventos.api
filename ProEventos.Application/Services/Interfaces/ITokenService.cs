using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}

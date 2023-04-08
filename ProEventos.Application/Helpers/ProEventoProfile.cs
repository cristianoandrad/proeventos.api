using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain.Identity;
using ProEventos.Domain.Models;

namespace ProEventos.Application.Helpers
{
    public class ProEventoProfile : Profile
    {
        public ProEventoProfile()
        {
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

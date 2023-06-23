using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Services.Interfaces;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Models;
using ProEventos.Persistence.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application.Services
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IMapper _mapper;

        public PalestranteService(IPalestranteRepository palestranteRepository, IMapper mapper)
        {
            _palestranteRepository = palestranteRepository;
            _mapper = mapper;
        }

        public async Task<PalestranteDto> AddPalestrante(int userId, PalestranteAddDto model)
        {
            try
            {
                var palestrante = _mapper.Map<Palestrante>(model);

                palestrante.UserId = userId;

                _palestranteRepository.Add<Palestrante>(palestrante);

                var sucesso = _palestranteRepository.SaveChangesAsync();

                var palestranteRetorno = _mapper.Map<PalestranteDto>(palestrante);

                return sucesso.Result ? palestranteRetorno : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PalestranteDto> UpdatePalestrante(int userId, PalestranteUpdateDto model)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByUserIdAsync(userId, false);

                if (palestrante is null) return null;

                var eventoUpdate = _mapper.Map<Palestrante>(model);

                eventoUpdate.Id = palestrante.Id;
                eventoUpdate.UserId = userId;

                _palestranteRepository.Update<Palestrante>(eventoUpdate);

                var sucesso = _palestranteRepository.SaveChangesAsync();

                var eventoRetorno = _mapper.Map<PalestranteDto>(eventoUpdate);

                return await sucesso ? eventoRetorno : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PalestranteDto> GetPalestranteByUserIdAsync(int userId, bool includePalestrante = false)
        {
            try
            {
                var palestrante = await _palestranteRepository.GetPalestranteByUserIdAsync(userId, includePalestrante);

                if (palestrante is null) return null;

                return _mapper.Map<PalestranteDto>(palestrante);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<PalestranteDto>> GetAllPaletrantesAsync(PageParams pageParams, bool includePalestrante = false)
        {
            try
            {
                var palestrantes = await _palestranteRepository.GetAllPalestrantesAsync(pageParams, includePalestrante);

                if (palestrantes is null) return null;

                var resultado = _mapper.Map<PageList<PalestranteDto>>(palestrantes);

                resultado.CurrentPage = palestrantes.CurrentPage;
                resultado.TotalPages = palestrantes.TotalPages;
                resultado.PageSize = palestrantes.PageSize;
                resultado.TotalCount = palestrantes.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

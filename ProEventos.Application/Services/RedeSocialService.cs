using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Services.Interfaces;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.Services
{
    public class RedeSocialService : IRedeSocialService
    {
        private readonly IRedeSocialRepository _redeSocialRepository;
        private readonly IMapper _mapper;

        public RedeSocialService(
            IRedeSocialRepository redeSocialRepository, 
            IMapper mapper
            )
        {
            _redeSocialRepository = redeSocialRepository;
            _mapper = mapper;
        }

        public async Task<RedeSocialDto> AddRedeSocial(int id, RedeSocialDto model, bool IsEvento)
        {
            try
            {
                var redeSocial = _mapper.Map<RedeSocial>(model);

                if (IsEvento)
                {
                    redeSocial.EventoId = id;
                    redeSocial.PalestranteId = null;

                }
                else
                {
                    redeSocial.EventoId = null;
                    redeSocial.PalestranteId = id;
                }

                _redeSocialRepository.Add<RedeSocial>(redeSocial);

                var sucesso = _redeSocialRepository.SaveChangesAsync();

                var redeSocialRetorno = _mapper.Map<RedeSocialDto>(redeSocial);

                return sucesso.Result ? redeSocialRetorno : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByEvento(int eventoId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialRepository.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);

                if (redeSocial is null) throw new Exception("Rede Social para delete não encontrado.");

                _redeSocialRepository.Delete(redeSocial);

                return await _redeSocialRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByPalestrante(int palestranteId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialRepository.GetRedeSocialPalestranteByIdsAsync(palestranteId, redeSocialId);

                if (redeSocial is null) throw new Exception("Rede Social para delete não encontrado.");

                _redeSocialRepository.Delete(redeSocial);

                return await _redeSocialRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto> GetRedeSocialEventoByIdsAsync(int eventoId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialRepository.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);

                if (redeSocial is null) return null;

                return _mapper.Map<RedeSocialDto>(redeSocial);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialRepository.GetRedeSocialPalestranteByIdsAsync(palestranteId, redeSocialId);

                if (redeSocial is null) return null;

                return _mapper.Map<RedeSocialDto>(redeSocial);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> GetAllByEventoIdAsync(int eventoId)
        {
            try
            {
                var redesSociais = await _redeSocialRepository.GetAllByEventoIdAsync(eventoId);

                if (redesSociais is null) return null;

                return _mapper.Map<RedeSocialDto[]>(redesSociais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> GetAllByPalestranteIdAsync(int palestranteId)
        {
            try
            {
                var redesSociais = await _redeSocialRepository.GetAllByPalestranteIdAsync(palestranteId);

                if (redesSociais is null) return null;

                return _mapper.Map<RedeSocialDto[]>(redesSociais);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> SaveByEvento(int eventoId, RedeSocialDto[] models)
        {
            try
            {
                var redesSociais = await _redeSocialRepository.GetAllByEventoIdAsync(eventoId);

                if (redesSociais is null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddRedeSocial(eventoId, model, true);
                    }
                    else
                    {
                        await UpdateRedeSocial(eventoId, redesSociais, model, true);
                    }
                }

                var redeSocialRetorno = await _redeSocialRepository.GetAllByEventoIdAsync(eventoId);

                return _mapper.Map<RedeSocialDto[]>(redeSocialRetorno);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> SaveByPalestrante(int palestranteId, RedeSocialDto[] models)
        {
            try
            {
                var redesSociais = await _redeSocialRepository.GetAllByPalestranteIdAsync(palestranteId);

                if (redesSociais is null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddRedeSocial(palestranteId, model, false);
                    }
                    else
                    {
                        await UpdateRedeSocial(palestranteId, redesSociais, model, false);
                    }
                }

                var redeSocialRetorno = await _redeSocialRepository.GetAllByPalestranteIdAsync(palestranteId);

                return _mapper.Map<RedeSocialDto[]>(redeSocialRetorno);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task UpdateRedeSocial(int id, RedeSocial[] redesSociais, RedeSocialDto model, bool isEvento)
        {
            var redeSocial = redesSociais.FirstOrDefault(x => x.Id == model.Id);

            if (isEvento)
                model.EventoId = id;
            else
                model.PalestranteId = id;

            _mapper.Map(model, redeSocial);

            _redeSocialRepository.Update<RedeSocial>(redeSocial);

            await _redeSocialRepository.SaveChangesAsync();
        }
    }
}

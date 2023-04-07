using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Services.Interfaces;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Repository;
using ProEventos.Persistence.Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.Services
{
    public class LoteService : ILoteService
    {
        private readonly ILoteRepository _loteRepository;
        private readonly IMapper _mapper;

        public LoteService(ILoteRepository loteRepository, IMapper mapper)
        {
            _loteRepository = loteRepository;
            _mapper = mapper;
        }

        public async Task<LoteDto> AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);

                lote.EventoId = eventoId;

                _loteRepository.Add<Lote>(lote);

                var sucesso = _loteRepository.SaveChangesAsync();

                var loteRetorno = _mapper.Map<LoteDto>(lote);

                return sucesso.Result ? loteRetorno : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdsAsync(eventoId, loteId);

                if (lote is null) throw new Exception("Lote para delete não encontrado.");

                _loteRepository.Delete(lote);

                return await _loteRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoId, int id)
        {
            try
            {
                var lote = await _loteRepository.GetLoteByIdsAsync(eventoId, id);

                if (lote is null) return null;

                return _mapper.Map<LoteDto>(lote);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _loteRepository.GetLotesByEventoIdAsync(eventoId);

                if (lotes is null) return null;

                return _mapper.Map<LoteDto[]>(lotes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await _loteRepository.GetLotesByEventoIdAsync(eventoId);

                if (lotes is null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        await UpdateLote(eventoId, lotes, model);
                    }
                }

                var loteRetorno = await _loteRepository.GetLotesByEventoIdAsync(eventoId);

                return _mapper.Map<LoteDto[]>(loteRetorno);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task UpdateLote(int eventoId, Lote[] lotes, LoteDto model)
        {
            var lote = lotes.FirstOrDefault(x => x.Id == model.Id);

            model.EventoId = eventoId;

            _mapper.Map(model, lote);

            _loteRepository.Update<Lote>(lote);

            await _loteRepository.SaveChangesAsync();
        }
    }
}

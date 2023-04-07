using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Services.Interfaces;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IMapper _mapper;

        public EventoService(IEventoRepository eventoRepository, IMapper mapper)
        {
            _eventoRepository = eventoRepository;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEvento(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _eventoRepository.Add<Evento>(evento);

                var sucesso = _eventoRepository.SaveChangesAsync();

                var eventoRetorno = _mapper.Map<EventoDto>(evento);

                return sucesso.Result ? eventoRetorno : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoRepository.GetAllEventoByIdAsync(eventoId);

                if (evento is null) return null;

                var eventoUpdate = _mapper.Map<Evento>(model);

                eventoUpdate.Id = evento.Id;

                _eventoRepository.Update<Evento>(eventoUpdate);

                var sucesso = _eventoRepository.SaveChangesAsync();

                var eventoRetorno = _mapper.Map<EventoDto>(eventoUpdate);

                return await sucesso ? eventoRetorno : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoRepository.GetAllEventoByIdAsync(eventoId);

                if (evento is null) throw new Exception("Evento para delete não encontrado.");

                _eventoRepository.Delete(evento);

                return await _eventoRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetAllEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoRepository.GetAllEventoByIdAsync(eventoId, includePalestrantes);

                if (evento is null) return null;

                return _mapper.Map<EventoDto>(evento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync(includePalestrantes);

                if (eventos is null) return null;

                return _mapper.Map<EventoDto[]>(eventos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if (eventos is null) return null;

                return _mapper.Map<EventoDto[]>(eventos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

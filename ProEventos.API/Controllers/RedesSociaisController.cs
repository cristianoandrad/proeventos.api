using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;
using ProEventos.Application.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RedesSociaisController : ControllerBase
    {
        private readonly IRedeSocialService _redeSocialService;
        private readonly IEventoService _eventoService;
        private readonly IPalestranteService _palestranteService;

        public RedesSociaisController(
            IRedeSocialService redeSocialService, 
            IEventoService eventoService, 
            IPalestranteService palestranteService)
        {
            _redeSocialService = redeSocialService;
            _eventoService = eventoService;
            _palestranteService = palestranteService;
        }

        [HttpGet("evento/{eventoId}")]
        public async Task<IActionResult> GetByEvento(int eventoId)
        {
            try
            {
                if (!( await AutorEvento(eventoId)))
                    return Unauthorized();

                var redeSocial = await _redeSocialService.GetAllByEventoIdAsync(eventoId);

                if (redeSocial is null) return NoContent();                

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar rede Social. Erro: {ex.Message}");
            }
        }

        [HttpGet("palestrante")]
        public async Task<IActionResult> GetByPalestrante()
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante is null) return Unauthorized();

                var redeSocial = await _redeSocialService.GetAllByPalestranteIdAsync(palestrante.Id);
                if (redeSocial is null) return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar rede Social. Erro: {ex.Message}");
            }
        }

        [HttpPut("evento/{eventoId}")]
        public async Task<IActionResult> SaveByEvento(int eventoId, RedeSocialDto[] models)
        {
            try
            {
                if (!(await AutorEvento(eventoId)))
                    return Unauthorized();

                var redeSocial = await _redeSocialService.SaveByEvento(eventoId, models);

                if (redeSocial is null) return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar rede social. Erro: {ex.Message}");
            }
        }

        [HttpPut("palestrante")]
        public async Task<IActionResult> SaveByPalestrante(RedeSocialDto[] models)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante is null) return Unauthorized();

                var redeSocial = await _redeSocialService.SaveByPalestrante(palestrante.Id, models);

                if (redeSocial is null) return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar salvar rede social. Erro: {ex.Message}");
            }
        }

        [HttpDelete("evento/{eventoId}/{redeSocialId}")]
        public async Task<IActionResult> DeleteByEvento(int eventoId, int redeSocialId)
        {
            try
            {
                if (!(await AutorEvento(eventoId)))
                    return Unauthorized();

                var redeSocial = await _redeSocialService.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);

                if (redeSocial is null) return NoContent();

                return await _redeSocialService.DeleteByEvento(eventoId, redeSocial.Id) ? 
                    Ok( new { message = "Rede Social Deletada"} ) : 
                    throw new Exception("Ocorreu um problema não específico o tentar deletar a Rede Social.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar rede social. Erro: {ex.Message}");
            }
        }

        [HttpDelete("palestrante/{redeSocialId}")]
        public async Task<IActionResult> DeleteByPalestrante(int redeSocialId)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante is null) return Unauthorized();

                var redeSocial = await _redeSocialService.GetRedeSocialPalestranteByIdsAsync(palestrante.Id, redeSocialId);
                if (redeSocial is null) return NoContent();

                return await _redeSocialService.DeleteByPalestrante(palestrante.Id, redeSocial.Id) ?
                    Ok(new { message = "Rede Social Deletada" }) :
                    throw new Exception("Ocorreu um problema não específico o tentar deletar a Rede Social.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar rede social. Erro: {ex.Message}");
            }
        }

        [NonAction]
        private async Task<bool> AutorEvento(int eventoId)
        {
            var evento = await _eventoService.GetEventoByIdAsync(User.GetUserId(), eventoId, false);

            return evento != null;
        }
    }
}

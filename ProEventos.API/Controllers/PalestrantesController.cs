using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;
using ProEventos.Application.Services.Interfaces;
using ProEventos.Persistence.Models;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PalestrantesController : ControllerBase
    {
        private readonly IPalestranteService _palestranteService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAccountService _accountService;

        public PalestrantesController(
            IPalestranteService palestranteService,
            IWebHostEnvironment webHostEnvironment,
            IAccountService accountService)
        {
            _palestranteService = palestranteService;
            _webHostEnvironment = webHostEnvironment;
            _accountService = accountService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery]PageParams pageParams)
        {
            try
            {
                var palestrantes = await _palestranteService.GetAllPaletrantesAsync(pageParams, true);

                if (palestrantes is null) return NoContent(); 
                
                Response.AddPagination(palestrantes.CurrentPage, palestrantes.PageSize, palestrantes.TotalCount, palestrantes.TotalPages);

                return Ok(palestrantes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar palestrantes. Erro: {ex.Message}");
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetPalestrantes()
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());

                if (palestrante is null) return NoContent();

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar palestrante. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PalestranteAddDto model)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId(), false);

                if(palestrante is null)
                    palestrante = await _palestranteService.AddPalestrante(User.GetUserId(), model);

                if (palestrante is null) return NoContent();

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar palestrante. Erro: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(PalestranteUpdateDto model)
        {
            try
            {
                var palestrante = await _palestranteService.UpdatePalestrante(User.GetUserId(), model);

                if (palestrante is null) return NoContent();

                return Ok(palestrante);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar palestrante. Erro: {ex.Message}");
            }
        }

        
    }
}

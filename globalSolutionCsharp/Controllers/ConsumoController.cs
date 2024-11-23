using globalSolutionCsharp.Consumo.Model;
using globalSolutionCsharp.Consumo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace globalSolutionCsharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoController : ControllerBase
    {
        private readonly IConsumoService _consumoService;

        public ConsumoController(IConsumoService consumoService)
        {
            _consumoService = consumoService;
        }

        [HttpPost("consumo")]
        public async Task<IActionResult> RegistrarConsumo([FromBody] ConsumoRequest request)
        {
            if (request == null || request.Quantidade <= 0)
                return BadRequest("Dados de consumo inválidos.");

            try
            {
                await _consumoService.RegistrarConsumoAsync(request);
                return Created("", new { message = "Consumo registrado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("consumo")]
        public async Task<IActionResult> ConsultarConsumo()
        {
            try
            {
                var consumos = await _consumoService.ObterConsumosAsync();
                if (consumos == null || !consumos.Any())
                    return NotFound("Nenhum consumo encontrado.");

                return Ok(consumos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}

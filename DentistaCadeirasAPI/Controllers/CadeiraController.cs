using DentistaCadeirasAPI.Controllers.DTO;
using DentistaCadeirasAPI.Models;
using DentistaCadeirasAPI.Services;
using DentistaCadeirasAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentistaCadeirasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadeiraController : ControllerBase
    {
        private readonly ICadeiraService _cadeiraService;
        private readonly ILogger<CadeiraController> _logger;

        public CadeiraController(ICadeiraService cadeiraService, ILogger<CadeiraController> logger)
        {
            _cadeiraService = cadeiraService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cadeira>>> GetCadeiras()
        {
            try
            {
                var cadeiras = await _cadeiraService.GetCadeirasAsync();

                if (cadeiras == null)
                {
                    return NotFound();
                }   

                return Ok(cadeiras);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao recuperar a lista de cadeiras: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Um erro ocorreu durante o processamento da requisição");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cadeira>> GetCadeira(int id)
        {
            try
            {
                var cadeira = await _cadeiraService.GetCadeiraByIdAsync(id);

                if (cadeira == null)
                {
                    return NotFound();
                }

                return Ok(cadeira);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao recuperar cadeira por Id: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Um erro ocorreu durante o processamento da requisição");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cadeira>> PostCadeira(Cadeira cadeira)
        {
            try
            {
                await _cadeiraService.AddCadeiraAsync(cadeira);
                return CreatedAtAction(nameof(GetCadeira), new { id = cadeira.Id }, cadeira);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao salvar cadeira: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Um erro ocorreu durante o processamento da requisição");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutCadeira(Cadeira cadeira)
        {
            try
            {
                await _cadeiraService.UpdateCadeiraAsync(cadeira);
            }
            catch (ArgumentException)
            {
                return BadRequest("Id inválido");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _cadeiraService.CadeiraExistsAsync(cadeira.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao alterar cadeira: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Um erro ocorreu durante o processamento da requisição");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCadeira(int id)
        {
            try
            {
                var cadeira = await _cadeiraService.GetCadeiraByIdAsync(id);

                if (cadeira == null)
                {
                    return NotFound();
                }

                await _cadeiraService.DeleteCadeiraAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao alterar cadeira: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Um erro ocorreu durante o processamento da requisição");
            }
        }

        [HttpPost("alocar")]
        public async Task<IActionResult> AlocarCadeira([FromBody] AlocacaoRequest request)
        {
            try
            {
                if (request.DataHoraInicial >= request.DataHoraFinal)
                {
                    return BadRequest("A data de início deve ser anterior à data final");
                }

                var alocacao = await _cadeiraService.AddAlocacaoCadeiraAsync(request.DataHoraInicial, request.DataHoraFinal);

                if (alocacao == null)
                {
                    return BadRequest("Nenhuma cadeira disponível para alocação no período informado.");
                }

                return Ok(alocacao);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao alterar cadeira: {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Um erro ocorreu durante o processamento da requisição");
            }
        }
    }
}

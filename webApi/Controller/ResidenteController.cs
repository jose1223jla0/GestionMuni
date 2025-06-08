using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Repository.Interfaces;
namespace webApi.Controller;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ResidenteController : ControllerBase
{
    private readonly IResidenteRepositorio _residenteRepositorio;
    public ResidenteController(IResidenteRepositorio residenteRepositorio)
    {
        _residenteRepositorio = residenteRepositorio;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Residente>>> GetResidentes()
    {
        IEnumerable<Residente> resultado = await _residenteRepositorio.GetResidentes();
        return Ok(resultado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Residente>> AgregarResidente([FromBody] Residente? residente)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (residente == null)
        {
            return BadRequest("El residente no puede ser nulo.");
        }

        try
        {
            Residente nuevoResidente = await _residenteRepositorio.AgregarResidente(residente);
            return Ok(nuevoResidente);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado." });
        }
    }
    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Residente>> EditarResidente(int id, [FromBody] Residente? residente)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id <= 0)
        {
            return BadRequest($"El id {id}  no válido");
        }

        if (residente == null)
        {
            return BadRequest("El residente no puede ser nulo.");
        }

        try
        {
            Residente residenteEditado = await _residenteRepositorio.EditarResidente(residente);
            return Ok(residenteEditado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { mensaje = "Ocurrió un error inesperado." });
        }
    }
}

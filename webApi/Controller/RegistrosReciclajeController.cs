using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Repository.Interfaces;
namespace webApi.Controller;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RegistrosReciclajeController : ControllerBase
{
    private readonly IRegistroReciclajeRepositorio _registroReciclajeRepositorio;
    public RegistrosReciclajeController(IRegistroReciclajeRepositorio registroReciclajeRepositorio)
    {
        _registroReciclajeRepositorio = registroReciclajeRepositorio;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RegistrosDeReciclaje>>> GetRegistrosDeReciclaje()
    {
        IEnumerable<RegistrosDeReciclaje> resultado = await _registroReciclajeRepositorio.GetRegistrosDeReciclaje();
        return Ok(resultado);
    }
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RegistrosDeReciclaje>> GetRegistroDeReciclaje(int id)
    {
        if (id <= 0)
        {
            return BadRequest($"El id {id} no es válido");
        }

        RegistrosDeReciclaje? resultado = await _registroReciclajeRepositorio.GetRegistrosDeReciclaje(id);
        if (resultado == null)
        {
            return NotFound($"No se encontró el registro de reciclaje con id {id}");
        }

        return Ok(resultado);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RegistrosDeReciclaje>> AgregarRegistroDeReciclaje([FromBody] RegistrosDeReciclaje? registroDeReciclaje)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (registroDeReciclaje == null)
        {
            return BadRequest("El registro de reciclaje no puede ser nulo.");
        }

        try
        {
            RegistrosDeReciclaje nuevoRegistro = await _registroReciclajeRepositorio.CreateRegistrosDeReciclaje(registroDeReciclaje);
            return Ok(nuevoRegistro);
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
    public async Task<ActionResult<RegistrosDeReciclaje>> EditarRegistroDeReciclaje(int id, [FromBody] RegistrosDeReciclaje? registroDeReciclaje)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (id <= 0)
        {
            return BadRequest($"El id {id} no es válido");
        }

        if (registroDeReciclaje == null)
        {
            return BadRequest("El registro de reciclaje no puede ser nulo.");
        }

        try
        {
            registroDeReciclaje.IdRegistrosReciclaje = id;
            RegistrosDeReciclaje editarRegistro = await _registroReciclajeRepositorio.UpdateRegistrosDeReciclaje(registroDeReciclaje);
            return Ok(editarRegistro);
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

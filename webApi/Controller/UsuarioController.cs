using Microsoft.AspNetCore.Mvc;
using webApi.Dto;
using webApi.Models;
using webApi.Repository.Interfaces;
namespace gestionApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetUsuarioDto>>> GetUsuarios()
    {
        IEnumerable<Usuario> usuarios = await _usuarioRepositorio.GetUsuarios();
        if (usuarios == null || !usuarios.Any())
        {
            return NotFound("No se encontraron usuarios.");
        }
        // Convertir a DTOs
        IEnumerable<GetUsuarioDto> usuariosDto = usuarios.Select(u => new GetUsuarioDto
        {
            IdUsuario = u.IdUsuario,
            IdRol = u.IdRol,
            NombreUsuario = u.NombreUsuario,
            EstadoUsuario = u.EstadoUsuario,
            FechaCreacionUsuario = u.FechaCreacionUsuario
        });
        return Ok(usuariosDto);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        if (id <= 0)
        {
            return BadRequest($"El id {id} no es válido");
        }

        Usuario? resultado = await _usuarioRepositorio.GetUsuario(id);
        if (resultado == null)
        {
            return NotFound($"No se encontró el usuario con id {id}");
        }

        return Ok(resultado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CreateUsuarioDto>> AgregarUsuario(CreateUsuarioDto? usuario)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (usuario == null)
        {
            return BadRequest("El usuario no puede ser nulo.");
        }

        try
        {
            Usuario nuevoUsuario = new Usuario
            {
                IdRol = usuario.IdRol,
                NombreUsuario = usuario.NombreUsuario,
                Contrasena = usuario.Contrasena,
            };
            Usuario agregarUsuario = await _usuarioRepositorio.AgregarUsuario(nuevoUsuario);
            return Ok(agregarUsuario);
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UpdateUsuarioDto>> EditarUsuario(int id, UpdateUsuarioDto? usuarioDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id <= 0)
        {
            return BadRequest($"El id {id} no es válido");
        }

        if (usuarioDto == null)
        {
            return BadRequest("El usuario no puede ser nulo.");
        }

        try
        {
            Usuario usuario = new Usuario
            {
                IdUsuario = usuarioDto.IdUsuario,
                IdRol = usuarioDto.IdRol,
                NombreUsuario = usuarioDto.NombreUsuario,
                EstadoUsuario = usuarioDto.EstadoUsuario
            };

            Usuario usuarioEditado = await _usuarioRepositorio.EditarUsuario(usuario);
            return Ok(usuarioEditado);
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

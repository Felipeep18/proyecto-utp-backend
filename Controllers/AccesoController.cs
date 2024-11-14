using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppiPrueba.Custom;
using WebAppiPrueba.Models;
using WebAppiPrueba.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebAppiPrueba.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly DbpruebaContext _dbPruebaContext;
        private readonly Utilidades _utilidades;

        public AccesoController(DbpruebaContext dbpruebaContext, Utilidades utilidades)
        {
            _dbPruebaContext = dbpruebaContext;
            _utilidades = utilidades;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Registrarse(UsuarioDTO usuario)
        {
            var modelUsuario = new Usuario
            {
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Clave = _utilidades.encriptarSHA256(usuario.Clave)
            };

            await _dbPruebaContext.Usuarios.AddAsync(modelUsuario);
            await _dbPruebaContext.SaveChangesAsync();

            if (modelUsuario.IdUsuario != 0)
                return StatusCode(StatusCodes.Status200OK, new { data = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { data = false });
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var usuarioEncontrado = await _dbPruebaContext.Usuarios
                                                            .Where(u =>
                                                            u.Correo == login.Correo &&
                                                            u.Clave == _utilidades.encriptarSHA256(login.Contra)
                                                            ).FirstOrDefaultAsync();
            if (usuarioEncontrado is null)
                return StatusCode(StatusCodes.Status200OK, new { data = false, message = "usuario no encontrado" });
            else
                return StatusCode(StatusCodes.Status200OK, new { data = true, token = _utilidades.generarJWT(usuarioEncontrado) });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppiPrueba.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAppiPrueba.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DbpruebaContext _dbPruebaContext;

        public ProductoController(DbpruebaContext dbpruebaContext)
        {
            _dbPruebaContext = dbpruebaContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _dbPruebaContext.Productos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new { value = lista });
        }
    }
}

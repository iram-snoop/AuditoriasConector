using Microsoft.AspNetCore.Mvc;

namespace Auditorias_Conector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditoriaController : Controller
    {
        [HttpGet]
        public IActionResult Auditoria()
        {
            return Ok(new { Message = "Hello, World!" });
        }
    }
}

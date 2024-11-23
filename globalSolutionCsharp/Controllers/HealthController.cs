using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace globalSolutionCsharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {

        [HttpGet("health")]
        public async Task<IActionResult> ConsultarServidor()
        {
          return StatusCode(200);
        }
    }
}

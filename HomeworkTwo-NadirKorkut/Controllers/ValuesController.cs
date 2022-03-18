using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeworkTwo_NadirKorkut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        [Route("/login")]
        public IActionResult Login()
        {
            return Ok("Giriş Yapıldı.");
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult Register()
        {
            return Ok("Üye olundu.");
        }

        [HttpDelete]
        [Route("/userdelete")]
        public IActionResult UserDelete()
        {
            return Ok("Üye olundu.");
        }

        [HttpPut]
        [Route("/userupdate")]
        public IActionResult UserUpdate()
        {
            return Ok("Üye olundu.");
        }

        [HttpGet]
        [Route("/testuser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetTestUser()
        {
            return Ok("Test kullanıcıları listelendi");
        }
    }
}

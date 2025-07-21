using Microsoft.AspNetCore.Mvc;

namespace TrucoAPI.Controllers
{
    [ApiController]
    [Route("api/turno")]
    public class RoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

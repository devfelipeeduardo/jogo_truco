using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TrucoAPI.Services;

namespace TrucoAPI.Controllers
{
    [ApiController]
    [Route("api/jogo")]
    public class JogoController : Controller
    {
        private readonly JogoService _jogoService;

        public JogoController(JogoService jogoService) { 
            _jogoService = jogoService;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> IniciarPartida([FromBody] List<string> nomesJogadores)
        {
            if (nomesJogadores.Count != 2 && nomesJogadores.Count != 4)
                return BadRequest("O jogo precisa de 2 ou 4 jogadores!");

            await _jogoService.IniciarPartidaAsync(nomesJogadores.ToArray());
            return Ok(_jogoService.GetPartidaState());
        }

        [HttpGet]
        public IActionResult GetPartidaState()
        {
            return Ok(_jogoService.GetPartidaState());
        }
    }
}

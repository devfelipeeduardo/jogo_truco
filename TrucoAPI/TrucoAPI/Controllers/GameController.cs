using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TrucoAPI.Services;

namespace TrucoAPI.Controllers
{
    [ApiController]
    [Route("api/jogo")]
    public class GameController : Controller
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService) { 
            _gameService = gameService;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> StartMatch([FromBody] List<string> players)
        {
            if (players.Count != 2 && players.Count != 4)
                return BadRequest("O jogo precisa de 2 ou 4 jogadores!");

            await _gameService.StartMatchAsync(players.ToArray());
            return Ok(_gameService.GetPartidaState());
        }

        [HttpGet]
        public IActionResult GetPartidaState()
        {
            return Ok(_gameService.GetPartidaState());
        }
    }
}

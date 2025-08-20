using Microsoft.AspNetCore.Mvc;
using TrucoAPI.Models.DTOs;
using TrucoAPI.Services;

namespace Truco.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        //Game
        [HttpPost("startGame")]
        public IActionResult StartGame([FromBody] List<string> playerNames)
        {
            _gameService.StartNewGame(playerNames);
            return Ok(new { message = "Jogo iniciado!", game = _gameService.GetCurrentGameState() });
        }

        //[HttpGet("state")]


        ////Round
        //[HttpPost("round/start")]

        //[HttpGet("round/state")]

        ////Turn

        //[HttpPost("turn/start")]

        //[HttpPost("turn/decide-winner")]

        //[HttpGet("turn/state")]

    }
}

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

        [HttpGet("gameState")]
        public IActionResult GetGameState()
        {
            return Ok(new { message = "Estado do Jogo foi retornado", game = _gameService.GetCurrentGameState() });
        }


        //Round
        [HttpPost("round/start")]
        public IActionResult StartRound()
        {
            _gameService.StartRound();
            return Ok(new { message = "Round iniciado!" });
        }

        [HttpGet("round/state")]
        public IActionResult GetRoundState()
        {
            return Ok(new { message = "Estado do Round foi retornado", game = _gameService.GetCurrentRoundState() });
        }


        //Turn
        [HttpPost("turn/start")]
        public async Task<IActionResult> StartTurn() {
            await _gameService.StartTurnAsync();
            return Ok(new { message = "Turno iniciado!" });
        }

        [HttpGet("turn/state")]
        public IActionResult GetTurnState()
        {
            return Ok(new { message = "Estado do Turno foi retornado", game = _gameService.GetCurrentTurnState() });
        }

        [HttpPost("turn/decide-winner")]
        public IActionResult DecidePlayerWinner([FromBody] List<string> cardsCode)
        {
            _gameService.DecidePlayerWinner(cardsCode);
            return Ok(new { message = "Retornado Jogador Vencedor" });
        }
        

    }
}

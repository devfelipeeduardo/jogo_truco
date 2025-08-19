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
        public async Task<IActionResult> StartGame([FromBody] List<string> playerNames)
        {
            await _gameService.StartNewGameAsync(playerNames);
            return Ok(new { message = "Jogo iniciado!", game = _gameService.GetCurrentGameState() });
        }

        [HttpGet("state")]
        public IActionResult GetGameState()
        {
            var state = _gameService.GetCurrentGameState();
            if (state == null) return NotFound("Nenhum jogo em andamento");
            return Ok(state);
        }

        //Round
        [HttpPost("round/start")]
        public async Task<IActionResult> StartRound()
        {
            await _gameService.StartRound();
            return Ok(new { message = "Rodada iniciada!", round = _gameService.GetCurrentRoundState() });
        }

        [HttpGet("round/state")]
        public IActionResult GetRoundState()
        {
            var state = _gameService.GetCurrentRoundState();
            if (state == null) return NotFound("Nenhuma rodada em andamento");
            return Ok(state);
        }

        //Turn

        [HttpPost("turn/start")]
        public async Task<IActionResult> StartTurn()
        {
            await _gameService.StartTurnAsync();
            return Ok(new { message = "Turno iniciado!", turn = _gameService.GetCurrentTurnState() });
        }

        [HttpPost("turn/decide-winner")]
        public IActionResult DecideTurnWinner([FromBody] List<CardDto> cards)
        {
            _gameService.DecidePlayerWinner(cards);
            return Ok(new { message = "Vencedor do turno definido!", turn = _gameService.GetCurrentTurnState() });
        }

        [HttpGet("turn/state")]
        public IActionResult GetTurnState()
        {
            var state = _gameService.GetCurrentTurnState();
            if (state == null) return NotFound("Nenhum turno em andamento");
            return Ok(state);
        }
    }
}

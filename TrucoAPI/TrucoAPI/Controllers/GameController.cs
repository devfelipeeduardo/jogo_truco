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
        private readonly RoundService _roundService;
        private readonly TurnService _turnService;

        public GameController(GameService gameService, RoundService roundService, TurnService turnService)
        {
            _gameService = gameService;
            _roundService = roundService;
            _turnService = turnService;
        }

        //Game
        [HttpPost("start")]
        public IActionResult StartGame([FromBody] List<string> playerNames)
        {
            _gameService.StartNewGameAsync(playerNames);
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
        public IActionResult StartRound()
        {
            _roundService.StartRound();
            return Ok(new { message = "Rodada iniciada!", round = _roundService.GetCurrentRoundState() });
        }

        [HttpGet("round/state")]
        public IActionResult GetRoundState()
        {
            var state = _roundService.GetCurrentRoundState();
            if (state == null) return NotFound("Nenhuma rodada em andamento");
            return Ok(state);
        }

        //Turn
        [HttpPost("turn/decide-winner")]
        public IActionResult DecideTurnWinner([FromBody] List<CardDto> cards)
        {
            _turnService.DecidePlayerWinner(cards);
            return Ok(new { message = "Vencedor do turno definido!", turn = _turnService.GetCurrentTurnState() });
        }

        [HttpGet("turn/state")]
        public IActionResult GetTurnState()
        {
            var state = _turnService.GetCurrentTurnState();
            if (state == null) return NotFound("Nenhum turno em andamento");
            return Ok(state);
        }
    }
}

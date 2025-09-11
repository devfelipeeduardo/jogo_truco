using Microsoft.AspNetCore.Mvc;
using TrucoAPI.Models.DTOs;
using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Game;
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
        [HttpPost("start")]
        public async Task<IActionResult> StartGame([FromBody] List<string> playerNames)
        {
            _gameService.StartNewGame(playerNames);

            var gameState = _gameService.GetCurrentGameState();
            var turnState = _gameService.GetCurrentTurnState();

            return Ok(new { message = "Jogo iniciado!", gameState, turnState });
        }

        [HttpGet("state")]
        public IActionResult GetGameState()
        {
            var gameState = _gameService.GetCurrentGameState();
            return Ok(new { message = "Estado do Jogo foi retornado", gameState });
        }

        [HttpGet("checkGameWinner")]
        public IActionResult CheckGameWinner()
        {
            var gameState = _gameService.GetCurrentGameState().GetGameWinner();
            return Ok(new { message = "Estado do Jogo foi retornado", gameState});
        }


        [HttpGet("players")]
        public IActionResult GetPlayers()
        {
            var playersState = _gameService.GetCurrentPlayers();
            return Ok(new { message = "Jogadores foram retornados", playersState});
        }

        [HttpGet("turn/state")]
        public IActionResult GetTurnState()
        {
            var turnState = _gameService.GetCurrentTurnState();
            return Ok(new { message = "Estado do Turno foi retornado", turnState });
        }

        [HttpPost("turn/decide-winner")]
        public IActionResult DecidePlayerWinner([FromBody] List<CardDto> cards)
        {
            _gameService.DecidePlayerWinner(cards);
            var playerWinner = _gameService.GetCurrentTurnState().GetPlayerWinner();
            return Ok(new
            {
                playerWinner
            });
        }
    }
} 

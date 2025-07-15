using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TrucoAPI.Models;
using TrucoAPI.Services;

namespace TrucoAPI.Controllers
{
    [ApiController]
    [Route("api/jogo")]
    public class RoundController : Controller
    {
        private readonly RoundService _roundService;

        public RoundController(RoundService gameService) { 
            _roundService = gameService;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> StartRound([FromBody] List<string> players)
        {
            if (players.Count != 2 && players.Count != 4)
                return BadRequest("O jogo precisa de 2 ou 4 jogadores!");

            await _roundService.StartRoundAsync(players.ToArray());
            return Ok(_roundService.GetRoundState());
        }

        [HttpGet]
        public IActionResult GetPartidaState()
        {
            return Ok(_roundService.GetRoundState());
        }

        [HttpPost("decidirVencedor")]
        public IActionResult ReturnWinner([FromBody] List<(Player, Card)> players)
        { 
            _roundService.DecideWinner(players);

            return Ok(_roundService.GetRoundState());

        }

    }
}

using Microsoft.AspNetCore.Mvc;
using TrucoAPI.Services;
using TrucoAPI.Models.Game;


namespace TrucoAPI.Controllers
{
    [ApiController]
    [Route("api/jogoTrue")]
    public class GameController : Controller
    {
        private readonly Game _game;

        public GameController(Game game) {
            _game = game;
        }

        [HttpPost("postPlayers")]
        public async Task<IActionResult> StartGame([FromBody] List<string> players)
        {
            if (players.Count != 2 && players.Count != 4)
                return BadRequest("O jogo precisa de 2 ou 4 jogadores!");

            _game.SetTeams(players);
            return Ok("Jogadores adicionados com sucesso");
        }

        [HttpPost("iniciarRound")]
        public async Task<IActionResult> StartRound(Game _game)
        {;
            return Ok();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

using TrucoAPI.Models.Game;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Services
{
    public class RoundService
    {
        private readonly TurnService _turnService;
        private readonly Round _round = new Round();
        private readonly Game _game = new Game();

        public RoundService(TurnService turnService, Game game)
        {
            _turnService = turnService;
            _game = game;
        }
        public async Task StartRound()
        {
            await _turnService.StartTurn();

        }

        public Round GetRoundState() => _round;
    }
}

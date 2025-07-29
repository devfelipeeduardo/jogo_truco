using TrucoAPI.Models.Game;

namespace TrucoAPI.Services
{
    public class RoundService
    {
        private readonly TurnService _turnService;

        public RoundService(TurnService turnService)
        {
            _turnService = turnService;
        }

        private Round _round = new Round();
        public async Task StartRound()
        {
            await _turnService.StartTurn();
            
        }

        public Round GetRoundState() => _round;

    }
}

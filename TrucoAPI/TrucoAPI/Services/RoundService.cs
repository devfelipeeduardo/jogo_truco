using TrucoAPI.Models.Game;
using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Enums;

namespace TrucoAPI.Services
{
    public class RoundService
    {
        private readonly TurnService _turnService;
        private readonly Game _game;
        public RoundService(TurnService turnService, Game game)
        {
            _turnService = turnService;
            _game = game;
        }

        private readonly Round _round = new Round();


        public async Task StartRound()
        {
            for (int i = 0; i < _round.GetMaxLength(); i++)
            {
                await _turnService.StartTurn();

                var result = _turnService.SetTurnWinner();

                if (result == TurnResult.WinnerSet)
                {
                    if (_game.Teams.Any(t => t.TurnScore == 2))
                        _turnService.SetRoundWinner();
                        break;
                }

                var turn = _turnService.GetTurnState();
                _round.AddTurn(turn);
                _turnService.StopTurn();
            }


        }

        public Round GetRoundState() => _round;
    }
}

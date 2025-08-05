using TrucoAPI.Models.Game;
using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Enums;

namespace TrucoAPI.Services
{
    public class RoundService
    {
        private readonly Game _game;
        private readonly TurnService _turn;
        public RoundService(Game game, TurnService turnService)
        {
            _game = game;
            _turn = turnService;
        }

        private readonly Round _round = new Round();

        public async Task StartRound()
        {
            for (int i = 0; i < _round.GetMaxTurns(); i++)
            {
                await _turn.StartTurn();

                var result = _turn.SetTurnWinner();

                if (result == WinnerResult.WinnerSet)
                {
                    if (_game.Teams.FirstOrDefault(t => t.TurnScore == 2) is Team winnerTeam)
                    {
                        winnerTeam.AddRoundPoint(1);
                        _game.ResetTeamsTurnAtributtes();
                        break;
                    }
                }
                // Adicionei essas linhas, para caso futuramente seja necessário utilizar os states dos turnos.
                var turn = _turn.GetTurnState();
                _round.AddTurnState(turn);
            }
        }

        public WinnerResult GetRoundWinner()
        {
            foreach (var team in _game.Teams)
            {
                if (team.RoundScore == 12)
                {
                    return WinnerResult.WinnerSet;
                }
            }
            return WinnerResult.NoWinner;
        }

        public Round GetRoundState() => _round;
    }
}

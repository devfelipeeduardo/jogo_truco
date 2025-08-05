using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Enums;
using TrucoAPI.Models.Game;

namespace TrucoAPI.Services
{
    public class GameService
    {
        private readonly Game _game;
        private readonly RoundService _round;

        public GameService(RoundService roundService, Game game)
        {
            _round = roundService;
            _game = game;
        }


        public async Task startGameAsync()
        {
            for (int i = 0; i < _game.GetMaxRounds(); i++)
            {
                await _round.StartRound();

                var round = _round.GetRoundState();
                _game.AddRound(round);

                var result = _round.GetRoundWinner();

                if (result == WinnerResult.WinnerSet)
                {
                    if (_game.Teams.FirstOrDefault(t => t.RoundScore == 12) is Team winnerTeam)
                    {
                        Console.WriteLine("A equipe: " + winnerTeam + " venceu o jogo!");
                        _game.ResetTeamsRoundAtributtes();
                    }
                    //Melhora isso pelo amor de Deus!!!
                    _game.ResetTeamsRoundAtributtes();
                    break;
                }
            }
        }

    }
}

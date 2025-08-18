using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Enums;
using TrucoAPI.Models.Game;

namespace TrucoAPI.Services
{
    public class GameService
    {
        private readonly Game _game;
        private readonly RoundService _roundService;

        public GameService(RoundService roundService, Game game)
        {
            _roundService = roundService;
            _game = game;
        }

        public Game GetCurrentGameState() => _game;

        public void StartNewGameAsync(List<string> playersNames)
        {
            _game.SetTeams(playersNames);
            _game.ResetTeamsRoundAtributtes();
            var round = _roundService.GetRoundState();

            _game.AddRound(round);

            var result = _roundService.GetRoundWinner();

            //if (result == TurnResult.HasWinner)
            //{
            //    if (_game.Teams.FirstOrDefault(t => t.RoundScore == 12) is Team winnerTeam)
            //    {
            //        Console.WriteLine("A equipe: " + winnerTeam + " venceu o jogo!");
            //        _game.ResetTeamsRoundAtributtes();
            //    }
            //    //Melhora isso pelo amor de Deus!!!
            //}
        }

    }
}

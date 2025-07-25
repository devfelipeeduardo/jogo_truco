using Microsoft.AspNetCore.Components.Web;
using TrucoAPI.Models.Entities;
using TrucoAPI.Services;

namespace TrucoAPI.Models.Game
{
    public class Game
    {
        public List<Round> Rounds { get; set; } = new List<Round>();
        public List<Team> Teams { get; private set; } = new List<Team>();

        //OBS: PlayerScore será passado via Team.Score

        public Game() {;
            Teams.Add(new Team());
            Teams.Add(new Team());
        }

        public void SetTeams(List<string> playersName)
        {
            Teams[0].AddPlayer(new Player(0, playersName[0]));
            Teams[0].AddPlayer(new Player(1, playersName[1]));

            Teams[1].AddPlayer(new Player(2, playersName[2]));
            Teams[1].AddPlayer(new Player(3, playersName[3]));
        }

        public Team getWhoWin()
        {

            var winningPlayer = Game.Teams.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == highestCard.CardValue));

        }

    //    List<Player> todosJogadores = new List<Player>();
    //    todosJogadores.AddRange(team1.Players);
    //    todosJogadores.AddRange(team2.Players);

    //    int totalCards = 3 * todosJogadores.Count;
    //    var allCards = await _deckService.DrawCardsAsync(deck.DeckId, totalCards);

    //        for (int i=0; i<todosJogadores.Count; i++)
    //        {
    //        var playerCards = allCards.GetRange(i * 3, 3);

    //    playerCards.ForEach(_turn.SetCardValue);

    //            todosJogadores[i].Hand = playerCards;
    //        }

    //    _turn.Players = todosJogadores;
    }
}

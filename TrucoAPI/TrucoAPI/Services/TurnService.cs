using Microsoft.VisualBasic;
using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Game;

namespace TrucoAPI.Services
{
    public class TurnService
    {
        private readonly DeckService _deckService;
        private readonly Game _game;

        public TurnService(DeckService deckService, Game game)
        {
            _deckService = deckService;
            _game = game;
        }

        private Turn _turn = new Turn();

        public async Task StartTurn()
        {
            var deck = await _deckService.CreateDeckAsync();
            _turn = new Turn
            {
                DeckId = deck.DeckId,
            };

            Team team1 = _game.Teams[0];
            Team team2 = _game.Teams[1];

            var trump = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            _turn.Trump = trump[0];

            List<Player> todosJogadores = new List<Player>();
            todosJogadores.AddRange(team1.Players);
            todosJogadores.AddRange(team2.Players);

            int totalCards = 3 * todosJogadores.Count;
            var allCards = await _deckService.DrawCardsAsync(deck.DeckId, totalCards);

            for (int i=0; i < todosJogadores.Count; i++)
            {
                var playerCards = allCards.GetRange(i * 3, 3);

                playerCards.ForEach(_turn.SetCardValue);

                todosJogadores[i].Hand = playerCards;
            }

            _turn.Players = todosJogadores;
        }
        public Player DecideWinner(List<Card> cards)
        {
            _turn = GetTurnState();
            var highestCard = cards.OrderByDescending(c => c.CardValue).FirstOrDefault();

            if (highestCard == null)
                return new Player(); //Corrigir futuramente!

            var winningPlayer = _turn.Players.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == highestCard.CardValue));

            return winningPlayer;
        }

        public Turn GetTurnState() => _turn;
    }
}

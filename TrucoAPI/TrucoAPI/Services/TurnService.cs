using Microsoft.VisualBasic;
using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Game;

namespace TrucoAPI.Services
{
    public class TurnService
    {
        private readonly DeckService _deckService;
        private readonly Game _game;
        private List<Player> _todosOsJogadores = new List<Player>();

        public TurnService(DeckService deckService, Game game)
        {
            _deckService = deckService;
            _game = game;
        }

        private Turn _turn = new Turn();

        public async Task StartTurn()
        {
            Team team1 = _game.Teams[0];
            Team team2 = _game.Teams[1];
            _todosOsJogadores.AddRange(team1.Players);
            _todosOsJogadores.AddRange(team2.Players);

            DeckResponse deck = await _deckService.CreateDeckAsync();
            _turn = new Turn{ DeckId = deck.DeckId };
            Card trump = await GetTrump(deck);

            int totalCards = GetTotalCards();
            var allCards = await getAllCards(deck, totalCards);

            for (int i=0; i < _todosOsJogadores.Count; i++)
            {
                var playerCards = allCards.GetRange(i * 3, 3);

                playerCards.ForEach(_turn.SetCardValue);

                _todosOsJogadores[i].Hand = playerCards;
            }

            _turn.Players = _todosOsJogadores;
        }

        public async Task<List<Card>> getAllCards(DeckResponse deck, int totalCards) {
            return await _deckService.DrawCardsAsync(deck.DeckId, totalCards);
        }

        public int GetTotalCards() => 3 * _todosOsJogadores.Count();

        public async Task<Card> GetTrump(DeckResponse deck)
        {
            List<Card> trump = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            return trump[0];
        }

        public Player DecideWinner(List<Card> cards)
        {
            _turn = GetTurnState();

            var highestCard = _turn.getCardHighestValue(cards);

            var winningPlayer = _turn.Players.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == highestCard.CardValue));

            return winningPlayer;
        }

        public Turn GetTurnState() => _turn;
    }
}

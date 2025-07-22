using Microsoft.VisualBasic;
using TrucoAPI.Models;

namespace TrucoAPI.Services
{
    public class TurnService
    {
        private readonly DeckService _deckService;

        public TurnService(DeckService deckService)
        {
            _deckService = deckService;
        }

        private Turn _turn = new Turn();
        public async Task StartTurnAsync(string[] players)
        {

            var deck = await _deckService.CreateDeckAsync();
            _turn = new Turn
            {
                DeckId = deck.DeckId,
                Players = players.Select(n => new Player { Name = n }).ToList()
            };

            var trump = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            _turn.Trump = trump[0];
            int totalCards = _turn.Players.Count * 3;
            var allCards = await _deckService.DrawCardsAsync(deck.DeckId, totalCards);
            for (int i=0; i < _turn.Players.Count; i++)
            {
                var playerCards = allCards.GetRange(i * 3, 3);

                playerCards.ForEach(_turn.SetCardValue);

                _turn.Players[i].Hand = playerCards;
            }
        }
        public void DecideWinner(List<Card> cards)
        {
            _turn = GetTurnState();
            var highestCard = cards.OrderByDescending(c => c.CardValue).FirstOrDefault();

            if (highestCard == null)
                return;

            var winningPlayer = _turn.Players.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == highestCard.CardValue));
        }

        public Turn GetTurnState() => _turn;
    }
}

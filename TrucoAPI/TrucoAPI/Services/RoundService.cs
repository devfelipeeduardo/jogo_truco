using Microsoft.VisualBasic;
using TrucoAPI.Models;

namespace TrucoAPI.Services
{
    public class RoundService
    {
        private readonly DeckService _deckService;

        public RoundService(DeckService deckService)
        {
            _deckService = deckService;
        }

        private Round _round = new Round();
        public async Task StartRoundAsync(string[] players)
        {
            var deck = await _deckService.CreateDeckAsync();
            _round = new Round
            {
                DeckId = deck.DeckId,
                Players = players.Select(n => new Player { Name = n }).ToList()
            };

            var trump = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            _round.Trump = trump[0];

            int totalCards = _round.Players.Count * 3;
            var allCards = await _deckService.DrawCardsAsync(deck.DeckId, totalCards);

            for (int i=0; i < _round.Players.Count; i++)
            {
                var playerCard = allCards.GetRange(i * 3, 3);

                foreach (var card in playerCard) {_round.SetCardValue(card);}
                _round.Players[i].Hand = playerCard;
            }
        }
        public void DecideWinner(List<Card> cards)
        {
            _round = GetRoundState();

            var highestCard = cards.OrderByDescending(c => c.CardValue).FirstOrDefault();

            if (highestCard == null)
                return;

            var winningPlayer = _round.Players.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == highestCard.CardValue));

        }

        public Round GetRoundState() => _round;
    }
}

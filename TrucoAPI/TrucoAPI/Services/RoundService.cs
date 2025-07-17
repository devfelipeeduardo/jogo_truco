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

            foreach (var player in _round.Players)
            {
                var cards = await _deckService.DrawCardsAsync(deck.DeckId, 3);

                foreach (var card in cards)
                {
                    _round.SetCardValueTest(card);
                }
                player.Hand = cards;
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

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

            int trumpCardValue = _round.Trump.CardValue;
            foreach (var player in _round.Players)
            {
                var cards = await _deckService.DrawCardsAsync(deck.DeckId, 3);

                foreach (var card in cards) {
                    _round.SetCardValue(card, _round.Trump);
                }
                player.Hand = cards;
            }
        }
        public async Task DecideWinnerAsync(string[] players)
        {
            _round = GetRoundState();

            Card highestCardValue = new Card { CardValue = 0};

            foreach (var player in _round.Players)
            {
                foreach (var card in player.Hand)
                {
                    if (card.SelectedByPlayer == true)
                    {
                        if (card.CardValue > highestCardValue.CardValue)
                        {
                            highestCardValue = card;
                            _round.playerWithCardWithHighestValue = player;
                        }
                    }
                }        
            }
        }

        public Round GetRoundState() => _round;
    }
}

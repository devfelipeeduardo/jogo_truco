using Microsoft.VisualBasic;
using TrucoAPI.Models;

namespace TrucoAPI.Services
{
    public class GameService
    {
        private readonly DeckService _deckService;

        public GameService(DeckService deckService)
        {
            _deckService = deckService;
        }

        private Match _match = new Match();
        public async Task StartMatchAsync(string[] players)
        {
            var deck = await _deckService.CreateDeckAsync();
            _match = new Match
            {
                DeckId = deck.DeckId,
                Players = players.Select(n => new Player { Name = n }).ToList()
            };

            var trump = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            _match.Trump = trump[0];


            int trumpCardValue = _match.Trump.CardValue;
            foreach (var player in _match.Players)
            {
                var cards = await _deckService.DrawCardsAsync(deck.DeckId, 3);

                foreach (var card in cards) {
                    _match.SetCardValue(card, _match.Trump);
                }
                player.Hand = cards;
            }
        }

        public Match GetPartidaState() => _match;
    }
}

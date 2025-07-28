using Microsoft.VisualBasic;
using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Game;

namespace TrucoAPI.Services
{
    public class TurnService
    {
        private readonly DeckService _deckService;
        private Turn? _turn;
        private readonly Game _game;

        public TurnService(DeckService deckService, Game game)
        {
            _deckService = deckService;
            _game = game;
        }


        public async Task StartTurn()
        {
            var deck = await _deckService.CreateDeckAsync();
            _turn = new Turn{ DeckId = deck.DeckId };

            SetupPlayers();
            await SetTrumpCard(deck);
            await DistributePlayer(deck);
        }

        private void SetupPlayers()
        {
            if (_turn.Players == null) return;

            _turn.Players.AddRange(_game.Teams[0].getPlayers());
            _turn.Players.AddRange(_game.Teams[1].getPlayers());
        }

        public async Task<List<Card>> GetAllCardsAsync(DeckResponse deck, int totalCards) {
            return await _deckService.DrawCardsAsync(deck.DeckId, totalCards);
        }

        public async Task SetTrumpCard(DeckResponse deck)
        {

            if (_turn == null) return;

            List<Card> trumpCards = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            var trump = trumpCards.FirstOrDefault() ?? throw new Exception("Não foi possível tirar a carta vira.");
        
            _turn.Trump = trump;
            _turn.SetTrumpValue();
        }

        public async Task DistributePlayer(DeckResponse deck)
        {
            if (_turn.Players == null) return;
            int totalCards = 3 * _turn.Players.Count();
            var allCards = await GetAllCardsAsync(deck, totalCards);

            for (int i = 0; i < _turn.Players.Count; i++)
            {
                var playerCards = allCards.GetRange(i * 3, 3);

                foreach (var card in playerCards)
                {
                    _turn.SetCardValue(card);
                }
                _turn.Players[i].SetHand(playerCards);
            }

        }
        public void DecideWinner(List<Card> cards)
        {
            if (_turn.Players == null) return;

            _turn = GetTurnState();

            var highestCard = _turn.getCardHighestValue(cards);
            var winningPlayer = _turn.Players.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == highestCard.CardValue));

            _turn.Winner = winningPlayer;
        }

        public Turn GetTurnState() => _turn;
    }
}

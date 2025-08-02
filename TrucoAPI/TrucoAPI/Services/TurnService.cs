using Microsoft.VisualBasic;
using System.Security.Principal;
using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Game;

namespace TrucoAPI.Services
{
    public class TurnService
    {
        private readonly DeckService _deckService;
        private readonly Game _game;
        private Turn? _turn;

        public TurnService(DeckService deckService, Game game)
        {
            _deckService = deckService;
            _game = game;
        }

        public async Task StartTurn()
        {
            var deck = await _deckService.CreateDeckAsync();
            _turn = new Turn{ DeckId = deck.DeckId };

            await SetTrumpCard(deck);
            await DistributePlayer(deck);
        }

        public async Task<List<Card>> GetAllCardsAsync(DeckResponse deck, int totalCards)
        {
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
            if (_game.Teams == null) return;
            if (_turn == null) return;

            var allPlayers = _game.GetAllPlayers();
            int totalCards = 3 * allPlayers.Count;

            var allCards = await GetAllCardsAsync(deck, totalCards);

            for (int i = 0; i < allPlayers.Count; i++)
            {
                var playerCards = allCards.GetRange(i * 3, 3);

                foreach (var card in playerCards)
                {
                    _turn.SetCardValue(card);
                }
                allPlayers[i].SetHand(playerCards);
            }
        }
        public void DecideWinner(List<Card> cards)
        {
            if (_game.Teams == null) return;
            if (_turn == null)
                throw new InvalidOperationException("Turn não iniciado. Chame StartTurn() antes");

            var allPlayers = _game.GetAllPlayers();
            _turn.SetCardHighestValue(cards);
            _turn.SetTurnWinner(allPlayers);
        }
        public void SetRoundWinner()
        {
            var winnerTeam = _game.Teams.FirstOrDefault(team => team.TurnScore == 2);
            
            if (winnerTeam != null)
            {
                winnerTeam.SetRoundScore(1);
                StopTurn();
            }
        }

        public void SetTurnWinner()
        {
            if (_turn.WinnerPlayer == null) return;

            foreach (var team in _game.Teams)
            {
                if (team.GetPlayers().Contains(_turn.WinnerPlayer))
                {
                    team.SetTurnScore(1);
                    break;
                }
            }

            SetRoundWinner();
        }

        public void StopTurn()
        {
            foreach (var team in _game.Teams)
            {
                team.ResetTurnScore();
                team.ResetPlayersHand();
            }
        }

        public Turn GetTurnState() => _turn;
    }
}

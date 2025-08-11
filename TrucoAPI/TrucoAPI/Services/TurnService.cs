using TrucoAPI.Models.Game;
using TrucoAPI.Models.Enums;
using TrucoAPI.Models.DTOs;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Services
{
    public class TurnService
    {
        private readonly DeckService _deck;
        private readonly Game _game;
        private Turn _turn = new();

        public TurnService(DeckService deckService, Game game)
        { 
            _deck = deckService;
            _game = game;
        }

        public async Task StartTurn()
        {
            var deck = await _deck.CreateDeckAsync();
            _turn = new Turn { DeckId = deck.DeckId };

            await SetTrumpCard(deck);
            await DistributePlayer(deck);
        }
        public TurnResult SetTurnWinner()
        {
            if (_turn.PlayerWinner == null)
                throw new InvalidOperationException("Turn não iniciado ou vencedor não definido. Chame DecideWinner() antes");

            foreach (var team in _game.Teams)
            {
                if (team.GetPlayers().Contains(_turn.PlayerWinner))
                {
                    team.AddTurnPoint(1);
                    break;
                }
            }
            return TurnResult.NoWinner;
        }

        public Turn GetTurnState() => _turn;

        private async Task<List<CardDto>> GetAllCardsAsync(DeckDto deck, int totalCards)
        {
            return await _deck.DrawCardsAsync(deck.DeckId, totalCards);
        }

        private async Task SetTrumpCard(DeckDto deck)
        {
            if (_turn == null) return;

            List<CardDto> trumpCards = await _deck.DrawCardsAsync(deck.DeckId, 1);
            var trump = trumpCards.FirstOrDefault() ?? throw new Exception("Não foi possível tirar a carta vira.");
        
            _turn.SetTrump(trump);
            _turn.SetTrumpValue();
        }

        private async Task DistributePlayer(DeckDto deck)
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

        //Tem que ser repassado via API
        public void DecidePlayerWinner(List<CardDto> cards)
        {
            if (_turn.HighestValueCard == null)
                throw new InvalidOperationException("Turn não iniciado ou carta de maior valor não definida. Chame DecideWinner() antes");

            _turn.SetCardHighestValue(cards);
            var allPlayers = _game.GetAllPlayers();

            var winnerPlayer = allPlayers.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == _turn.HighestValueCard.CardValue))
                ?? throw new Exception("Não foi possível determinar o jogador vencedor do turno.");

            _turn.SetWinnerPlayer(winnerPlayer);
        }
    }
}

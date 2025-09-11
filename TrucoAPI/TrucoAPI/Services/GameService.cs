using TrucoAPI.Models.Domains.Exceptions;
using TrucoAPI.Models.DTOs;
using TrucoAPI.Models.Entities;
using TrucoAPI.Models.Enums;
using TrucoAPI.Models.Game;

namespace TrucoAPI.Services
{
    public class GameService
    {
        private readonly DeckService _deckService;
        private Game? _game;
        private Turn? _turn;
        public bool IsGameInitialized = false;

        //Game
        public Game? GetCurrentGameState() => _game;

        private GameService() { }

        public GameService(DeckService deckService)
        {
            _deckService = deckService;
        }

        public async Task StartNewGame(List<string> playersNames)
        {
            if (IsGameInitialized) return;

            _game = new Game();
            _game.SetTeams(playersNames);
            IsGameInitialized = true;
            await StartTurnAsync();
        }

        public List<Player> GetCurrentPlayers()
        {
            if (_game == null)
                throw new ArgumentNullException("O jogo não foi iniciado!");

            List<Player> players = _game.GetAllPlayers();

            return players;
        }

        //Turn
        public Turn? GetCurrentTurnState() => _turn;

        public async Task StartTurnAsync()
        {
            if (_game == null)
                throw new ArgumentNullException("O jogo não foi iniciado!");

            var deck = await _deckService.CreateDeckAsync();
            _turn = new Turn { DeckId = deck.DeckId };

            await SetTrumpCard(deck);
            await DistributeCardsByPlayer(deck);
        }

        private void CheckGameWinner()
        {
            ValidateGameAndTurn();

            foreach (var team in _game.Teams)
            {
                if (team.RoundScore == 12)
                {
                    _game.SetGameWinner(team);
                }
            }
        }

        private async Task SetTrumpCard(DeckDto deck)
        {
            ValidateGameAndTurn();

            List<CardDto> trumpCards = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            var trump = trumpCards.FirstOrDefault()
                ?? throw new Exception("Não foi possível encontrar a carta vira.");

            _turn.SetTrump(trump);
            _turn.SetTrumpValue();
        }

        private async Task DistributeCardsByPlayer(DeckDto deck)
        {
            ValidateGameAndTurn();
            if (_game.Teams == null)
                throw new ArgumentNullException(nameof(deck), "Os times não foram definidos não foi iniciado!");

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

        public void AddPointsForATeam()
        {
            ValidateGameAndTurn();
            if (_turn.PlayerWinner == null)
                throw new ArgumentNullException("Turn não iniciado ou vencedor não definido. Chame DecideWinner() antes");
            if (_game.Teams == null)
                throw new ArgumentNullException("Não foram setados times!");

            foreach (var team in _game.Teams)
            {
                if (team.GetPlayers().Contains(_turn.PlayerWinner))
                {
                    team.AddTurnPoint(1);
                }
            }
            CheckRoundWinner();
        }

        private void AddPointsForEachTeam()
        {
            ValidateGameAndTurn();
            if (_turn.PlayerWinner == null)
                throw new ArgumentNullException("Turn não iniciado ou vencedor não definido. Chame DecideWinner() antes");
            if (_game.Teams == null)
                throw new ArgumentNullException("Não foram setados times!");

            foreach (var team in _game.Teams)
            {
                    team.AddTurnPoint(1);
            }
            CheckRoundWinner();
        }

        private void CheckRoundWinner()
        {
            ValidateGameAndTurn();
            Team winnerTeam = _game.Teams.FirstOrDefault(t => t.TurnScore == 2);
            if (winnerTeam != null)
            {
                winnerTeam.AddRoundPoint(1);
                _game.ResetTeamsTurnScore();
                StartTurnAsync();
            }
        }

        private async Task<List<CardDto>> GetAllCardsAsync(DeckDto deck, int cardsQuantity)
        {
            return await _deckService.DrawCardsAsync(deck.DeckId, cardsQuantity);
        }

        public void DecidePlayerWinner(List<CardDto> cards)
        {
            ValidateGameAndTurn();
            try
            {
                _turn.SetCardHighestValue(cards);
            }
            catch (Exception ex) {
                throw new Exception("Não foi setado cartas: ", ex);
            }

            List<CardDto> cardsEqualHighestCardValue = cards.Where(c => c.CardValue == _turn.HighestValueCard.CardValue).ToList();

            var allPlayers = _game.GetAllPlayers();

            if (cardsEqualHighestCardValue.Count >= 2)
            {
                AddPointsForEachTeam();
            }
            else if (cardsEqualHighestCardValue.Count < 2)
            {
                var winnerPlayer = allPlayers.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == _turn.HighestValueCard.CardValue))
                    ?? throw new NullReferenceException("Não foi possível determinar o jogador vencedor do turno.");

                _turn.SetPlayerWinner(winnerPlayer);
                AddPointsForATeam();
            }
            CheckGameWinner();
        }

        private void ValidateGameAndTurn()
        {
            if (_game == null)
                throw new ArgumentNullException("O jogo não foi iniciado!");
            if (_turn == null)
                throw new ArgumentNullException("O turno não foi iniciado!");
        }
    }
}

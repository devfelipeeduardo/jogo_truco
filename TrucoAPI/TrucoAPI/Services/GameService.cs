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
        private Round? _round;
        private Turn? _turn;


        //Game
        public Game? GetCurrentGameState() => _game;
        public GameService(DeckService deckService)
        {
            _deckService = deckService;
        }


        public void StartNewGame(List<string> playersNames)
        {
            _game = new Game();
            _game.SetTeams(playersNames);
            _game.ResetTeamsRoundAtributtes();
        }

        //Round
        public Round? GetCurrentRoundState() => _round;
        public void StartRound()
        {
            _round = new Round();
        }

        //Vai rodar sempre que acabar um turno!
        public TurnResult GetGameWinner()
        {

            if (_game == null) throw new GameNotInitialized("O jogo não foi iniciado!"); ;

            foreach (var team in _game.Teams)
            {
                if (team.RoundScore == 12)
                {
                    return TurnResult.HasWinner;
                }
            }
            return TurnResult.NoWinner;
        }


        //Turn
        public Turn? GetCurrentTurnState() => _turn;
        public async Task StartTurnAsync()
        {
            var deck = await _deckService.CreateDeckAsync();
            _turn = new Turn { DeckId = deck.DeckId };

            await SetTrumpCard(deck);
            await DistributePlayer(deck);
        }

        public TurnResult GetTurnWinner()
        {
            if (_game == null) return;
            if (_turn == null) return TurnResult.NoWinner;
            if (_turn.PlayerWinner == null)
                throw new InvalidOperationException("Turn não iniciado ou vencedor não definido. Chame DecideWinner() antes");

            if (_game.Teams.FirstOrDefault(t => t.TurnScore == 2) is Team winnerTeam)
            {
                winnerTeam.AddRoundPoint(1);
                _game.ResetTeamsTurnAtributtes();
            }

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


        private async Task<List<CardDto>> GetAllCardsAsync(DeckDto deck, int totalCards)
        {
            return await _deckService.DrawCardsAsync(deck.DeckId, totalCards);
        }

        private async Task SetTrumpCard(DeckDto deck)
        {
            if (_turn == null) return;

            List<CardDto> trumpCards = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            var trump = trumpCards.FirstOrDefault() ?? throw new Exception("Não foi possível tirar a carta vira.");

            _turn.SetTrump(trump);
            _turn.SetTrumpValue();
        }

        private async Task DistributePlayer(DeckDto deck)
        {
            if (_game == null) return;

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
            if (_turn == null) return;

            _turn.SetCardHighestValue(cards);
            var allPlayers = _game.GetAllPlayers();

            var winnerPlayer = allPlayers.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == _turn.HighestValueCard.CardValue))
                ?? throw new Exception("Não foi possível determinar o jogador vencedor do turno.");

            _turn.SetWinnerPlayer(winnerPlayer);
        }

    }
}

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

            // Seleciona os jogadores pelos nomes.
            var deck = await _deckService.CreateDeckAsync();
            _turn = new Turn
            {
                DeckId = deck.DeckId,
                Players = players.Select(n => new Player { Name = n }).ToList()
            };

            var trump = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            _turn.Trump = trump[0];

            //Para descobrir a quantidade das cartas, decidi multiplicar a quantidade dos jogadores por 3.
            //Assim crio apenas uma requisição com await, melhoro e melhoro o desempenho
            int totalCards = _turn.Players.Count * 3;
            var allCards = await _deckService.DrawCardsAsync(deck.DeckId, totalCards);
            for (int i=0; i < _turn.Players.Count; i++)
            {
                //Interessante notar que, utilizando GetRange, consigo mudar o ponto de partida de escolha das cartas de um jgoador apartir da soma do contador.
                //Assim eu distribuo 3 cartas para cada jogador, pulando de 3 índices em 3 índices.
                var playerCards = allCards.GetRange(i * 3, 3);

                //Pensei em utilizar outros metodos, como: foreach (var card in playerCard) {_round.SetCardValue(card);}
                //Entretanto, buscando um foreach mais limpo, utilizei o método abaixo:
                playerCards.ForEach(_turn.SetCardValue);

                _turn.Players[i].Hand = playerCards;
            }
        }
        public void DecideWinner(List<Card> cards)
        {
            _turn = GetTurnState();
            //Ordena descendente e pega o 1°. Ótimo para pegar o objeto inteiro apartir de uma condição.
            //Outro método que é parecido é o Max(). Entretanto, o max retorna um valor, e não o objeto inteiro.
            var highestCard = cards.OrderByDescending(c => c.CardValue).FirstOrDefault();

            if (highestCard == null)
                return;

            var winningPlayer = _turn.Players.FirstOrDefault(p => p.Hand.Any(c => c.CardValue == highestCard.CardValue));

        }

        public Turn GetTurnState() => _turn;
    }
}

using Microsoft.VisualBasic;
using TrucoAPI.Models;

namespace TrucoAPI.Services
{
    public class JogoService
    {
        private readonly DeckService _deckService;

        public JogoService(DeckService deckService)
        {
            _deckService = deckService;
        }

        private Partida _partida = new Partida();
        public async Task IniciarPartidaAsync(string[] nomesJogadores)
        {
            var deck = await _deckService.CreateDeckAsync();
            _partida = new Partida
            {
                DeckId = deck.DeckId,
                Jogadores = nomesJogadores.Select(n => new Jogador { Nome = n }).ToList()
            };

            var manilha = await _deckService.DrawCardsAsync(deck.DeckId, 1);
            _partida.Manilha = manilha[0];


            int manilhaCardValue = _partida.Manilha.CardValue;
            foreach (var jogador in _partida.Jogadores)
            {
                var cartas = await _deckService.DrawCardsAsync(deck.DeckId, 3);

                for (int i=0; i < cartas.Count; i++) {
                        _partida.SetCardValue(cartas[i], _partida.Manilha);
                        Console.WriteLine(cartas[i].Value);
                }
                jogador.Mao = cartas;
            }
        }

        public Partida GetPartidaState() => _partida;
    }
}

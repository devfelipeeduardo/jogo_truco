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

            _partida.Manilha.CardValue = 20;

            foreach (var jogador in _partida.Jogadores)
            {
                var cartas = await _deckService.DrawCardsAsync(deck.DeckId, 3);

                for (int i=0; i < cartas.Count; i++) {


                    foreach (var carta in _partida.CardsValue)
                    {
                        if (carta.Key == cartas[i].Code)
                        {
                            if (carta.Value == _partida.Manilha.CardValue + 1 && carta.Key[1] == 'D' )
                            {
                                cartas[i].CardValue = _partida.Manilha.CardValue + 1;
                            } 
                            else if (carta.Value == _partida.Manilha.CardValue + 1 && carta.Key[1] == 'S')
                            {
                                cartas[i].CardValue = _partida.Manilha.CardValue + 2;
                            }
                            else if (carta.Value == _partida.Manilha.CardValue + 1 && carta.Key[1] == 'C')
                            {
                                cartas[i].CardValue = _partida.Manilha.CardValue + 3;
                            }
                            else if (carta.Value == _partida.Manilha.CardValue + 1 && carta.Key[1] == 'S')
                            {
                                cartas[i].CardValue = _partida.Manilha.CardValue + 4;
                            }
                        }
                    }
                    jogador.Mao = cartas;
                }

            }

        }

        public Partida GetPartidaState() => _partida;
    }
}

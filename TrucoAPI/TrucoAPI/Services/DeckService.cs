using System.Text.Json;
using TrucoAPI.Models.DTOs;

namespace TrucoAPI.Services
{
    public class DeckService
    {
        private readonly HttpClient _http;

        public DeckService(HttpClient http) => _http = http;

        public async Task<DeckDto> CreateDeckAsync()
        {
            var response = await _http.GetAsync("https://www.deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            if (response == null) throw new HttpRequestException("Não foi possível criar o deck");

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();


            return JsonSerializer.Deserialize<DeckDto>(json);
        }
        
        public async Task<List<CardDto>> DrawCardsAsync(string deckId, int quantity)
        {
            var response = await _http.GetAsync($"https://www.deckofcardsapi.com/api/deck/{deckId}/draw/?count={quantity}");
            if (response == null) throw new HttpRequestException("Não foi possível comprar cartas");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DeckDto>(json);

            return result.Cards;

        }
    }
}

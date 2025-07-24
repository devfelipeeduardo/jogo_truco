using System.Text.Json;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Services
{
    public class DeckService
    {
        private readonly HttpClient _http;

        public DeckService(HttpClient http)
        {
            _http = http;
        }

        public async Task<DeckResponse> CreateDeckAsync()
        {
            var response = await _http.GetAsync("https://www.deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DeckResponse>(json);
        }
        
        public async Task<List<Card>> DrawCardsAsync(string deckId, int quantity)
        {        
            var response = await _http.GetAsync($"https://www.deckofcardsapi.com/api/deck/{deckId}/draw/?count={quantity}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DeckResponse>(json);

            return result.Cards;

        }
    }
}

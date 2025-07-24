using System.Text.Json.Serialization;

namespace TrucoAPI.Models.Entities
{
    public class DeckResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("deck_id")]
        public string DeckId { get; set; } = string.Empty;

        [JsonPropertyName("remaining")]
        public int Remaining { get; set; }
        [JsonPropertyName("cards")]
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}


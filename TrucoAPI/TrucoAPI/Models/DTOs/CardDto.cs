using System.Text.Json.Serialization;

namespace TrucoAPI.Models.DTOs
{
    public class CardDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("suit")]
        public string Suit { get; set; } = string.Empty;
        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        public int CardValue { get; set; } = 0;
    }

}
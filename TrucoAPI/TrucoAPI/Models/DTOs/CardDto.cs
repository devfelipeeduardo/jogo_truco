using System.Text.Json.Serialization;

namespace TrucoAPI.Models.DTOs
{
    public class CardDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("suit")]
        public string Suit { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }

        public int CardValue { get; set; } = 0;
    }

}
﻿using System.Text.Json.Serialization;

namespace TrucoAPI.Models.Entities
{
    public class Card
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("suit")]
        public string Suit { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;
        public int CardValue { get; set; } = 0;
        public bool SelectedByPlayer { get; set; }

    }

}
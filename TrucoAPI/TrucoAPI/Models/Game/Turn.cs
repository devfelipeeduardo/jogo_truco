using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Models.Game
{
    public class Turn
    {
        public string DeckId { get; set; } = string.Empty;
        public Card? Trump { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();

        [JsonPropertyName("playerWithCardWithHighestValue")]
        public Player? PlayerWithCardWithHighestValue { get; set; } //Verificar futuramente!

        public Dictionary<string, int> CardsValue = new Dictionary<string, int>{
            {"3D", 13},{"2D", 12},{"AD", 11},
            {"KD", 10},{"JD", 09},{"QD", 08},
            {"0D", 07},{"9D", 06},{"8D", 05},
            {"7D", 04},{"6D", 03},{"5D", 02},
            {"4D", 01},
            {"3S", 13},{"2S", 12},{"AS", 11},
            {"KS", 10},{"JS", 09},{"QS", 08},
            {"0S", 07},{"9S", 06},{"8S", 05},
            {"7S", 04},{"6S", 03},{"5S", 02},
            {"4S", 01},
            {"3H", 13},{"2H", 12},{"AH", 11},
            {"KH", 10},{"JH", 09},{"QH", 08},
            {"0H", 07},{"9H", 06},{"8H", 05},
            {"7H", 04},{"6H", 03},{"5H", 02},
            {"4H", 01},
            {"3C", 13},{"2C", 12},{"AC", 11},
            {"KC", 10},{"JC", 09},{"QC", 08},
            {"0C", 07},{"9C", 06},{"8C", 05},
            {"7C", 04},{"6C", 03},{"5C", 02},
            {"4C", 01}
        };

        public void SetTrumpValue()
        {
            if (Trump == null)
                return;

            char trumpNumber = Trump.Code[0];

            if (CardsValue.TryGetValue(Trump.Code, out int trumpValues))
            {
                Trump.CardValue = trumpNumber == '3' ? 0 : trumpValues;
            }
        }

        public void SetCardValue(Card card)
        {
            if (!CardsValue.TryGetValue(card.Code, out int cardValue))
                return;

            card.CardValue = cardValue;

            if (Trump == null)
                return;

            if (card.CardValue == Trump.CardValue + 1)
            {
                char suit = card.Code[1];
                switch (suit)
                {
                    case 'D': card.CardValue = 21; break;
                    case 'S': card.CardValue = 22; break;
                    case 'H': card.CardValue = 23; break;
                    case 'C': card.CardValue = 24; break;
                }

            }
        }
    }
}
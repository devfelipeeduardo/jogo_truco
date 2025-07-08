using System.Runtime.CompilerServices;

namespace TrucoAPI.Models
{
    public class Partida
    {
        public string DeckId { get; set; } = string.Empty;
        public List<Jogador> Jogadores { get; set; } = new();
        public Card Manilha { get; set; }
        public int RodadaAtual { get; set; } = 1;

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

        public void SetCardValue(Card card, Card manilha)
        {
            char manilhaNumber = manilha.Code[0];

            if (CardsValue.TryGetValue(manilha.Code, out int manilhaValues))
            {
                manilha.CardValue = manilhaNumber == '3' ? 0 : manilhaValues;

            }

            if (CardsValue.TryGetValue(card.Code, out int cardValue))
            {
                card.CardValue = cardValue;
            }

            if (card.CardValue == manilha.CardValue + 1)
            {
                char suit = card.Code[1];
                switch (suit)
                {
                    case 'D':
                        card.CardValue = 21;
                        break;
                    case 'S':
                        card.CardValue = 22;
                        break;
                    case 'H':
                        card.CardValue = 23;
                        break;
                    case 'C':
                        card.CardValue = 24;
                        break;
                }
                
            }
        }
    }
}
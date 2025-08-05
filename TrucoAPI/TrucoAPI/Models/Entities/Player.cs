using TrucoAPI.Models.DTOs;

namespace TrucoAPI.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<CardDto> Hand { get; private set; } = new List<CardDto>();

        public Player(string name) {
            Name = name;
        }

        public void SetHand(List<CardDto> hand)
        {
            Hand = hand;
        }

        public void ResetHand()
        {
           Hand.Clear();
        }
    }
}
namespace TrucoAPI.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Card> Hand { get; private set; } = new List<Card>();

        public Player(string name) {
            Name = name;
        }

        public void SetHand(List<Card> hand)
        {
            Hand = hand;
        }

        public void ResetHand()
        {
           Hand.Clear();
        }
    }
}
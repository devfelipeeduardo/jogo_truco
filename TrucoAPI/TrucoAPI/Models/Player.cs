namespace TrucoAPI.Models
{
    public class Player
    {
        public string Name { get; set; } = string.Empty;
        public List<Card> Hand { get; set; } = new List<Card>();
        //public bool PlayerWithCardWithHighestValue = false;
    }
}

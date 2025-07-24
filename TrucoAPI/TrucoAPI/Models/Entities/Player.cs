namespace TrucoAPI.Models.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Card> Hand { get; set; } = new List<Card>();

        public Player(int id, string name) {
            Id = id;
            Name = name;
        }
    }
}

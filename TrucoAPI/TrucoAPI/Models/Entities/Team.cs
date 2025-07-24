namespace TrucoAPI.Models.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int Score { get; set; }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }
    }
}

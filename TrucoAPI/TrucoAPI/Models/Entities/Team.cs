namespace TrucoAPI.Models.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        private List<Player> Players { get; set; } = new List<Player>();
        public int Score { get; set; }

        public Team(List<Player> players)
        {
            Players = players;
        }
        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public List<Player> getPlayers()
        {
            return Players;
        }
    }
}

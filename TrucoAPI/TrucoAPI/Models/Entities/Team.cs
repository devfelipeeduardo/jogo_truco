namespace TrucoAPI.Models.Entities
{
    public class Team
    {
        private List<Player> Players { get; set; } = new List<Player>();
        private int TeamId { get; set; }
        private int Score { get; set; }

        public Team(){ }
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

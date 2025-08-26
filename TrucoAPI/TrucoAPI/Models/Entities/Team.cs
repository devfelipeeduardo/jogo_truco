namespace TrucoAPI.Models.Entities
{
    public class Team
    {
        public List<Player> Players { get; private set; } = new List<Player>();
        public int RoundScore { get; private set; }
        public int TurnScore { get; private set; }

        public Team(){ }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public List<Player> GetPlayers()
        {
            return Players;
        }

        public void ResetPlayersHand()
        {
            foreach (var player in Players)
            {
                player.ResetHand();
            }
        }

        public void AddRoundPoint(int pontuation)
        {
            RoundScore += pontuation;
        }
        public void ResetRoundScore()
        {
            RoundScore = 0;
        }

        public void AddTurnPoint(int pontuation)
        {
            TurnScore += pontuation;
        }

        public void ResetTurnScore()
        {
            TurnScore = 0;
        }


    }
}

namespace TrucoAPI.Models.Entities
{
    public class Team
    {
        public List<Player> Players { get; private set; } = new List<Player>();
        public int GameScore { get; private set; } = 0; // Talvez não use isso, apenas se o programa for feito por completo.
        public int RoundScore { get; private set; } = 0;
        public int TurnScore { get; private set; } = 0;

        public Team(){ }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public List<Player> GetPlayers()
        {
            return Players;
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

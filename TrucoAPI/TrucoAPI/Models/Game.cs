namespace TrucoAPI.Models
{
    public class Game
    {
        public List<Round> Rounds { get; set; }
        private List<string> PlayersName { get; set; }
        public int PlayersScore { get; set; }

        public void setPlayers(List<string> playersName) {
            PlayersName = playersName;
        }
    }
}

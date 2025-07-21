using TrucoAPI.DataAnottations;

namespace TrucoAPI.Models
{
    public class Round
    {
        //Armazenar as duplas futuramente.
        [MaxPlayersCount(5)]
        public List<Player> Pairs { get; set; }
        public int PairScore { get; set; }
        public List<Turn> Turns { get; set; }
    }
}

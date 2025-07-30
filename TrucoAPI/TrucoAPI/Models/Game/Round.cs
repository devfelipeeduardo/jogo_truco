using TrucoAPI.Models.Entities;

namespace TrucoAPI.Models.Game
{
    public class Round
    {
        public List<Turn> Turns { get; private set; } = new List<Turn>();
        public int CurrentTurn => Turns.Count;

        public void AddTurn(Turn turn)
        {
            Turns.Add(turn);
        }

        public void ClearRoundScore()
        {
        }
    }
}

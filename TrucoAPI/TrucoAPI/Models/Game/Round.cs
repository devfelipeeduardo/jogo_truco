using TrucoAPI.Models.Entities;

namespace TrucoAPI.Models.Game
{
    public class Round
    {
        public List<Turn> Turns { get; private set; }
        public int CurrentTurn => Turns.Count;

        public Round()
        {
            Turns = new List<Turn>()
            {
                new Turn(),
                new Turn(),
                new Turn()
            };
        }   

        public void AddTurn(Turn turn)
        {
            Turns.Add(turn);
        }
    }
}

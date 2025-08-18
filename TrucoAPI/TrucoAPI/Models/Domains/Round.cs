using System.ComponentModel.DataAnnotations;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Models.Game
{
    public class Round
    {
        private int _maxTurns = 3;
        public List<Turn> Turns { get; private set; } = new List<Turn>();
        //public int? CurrentTurn => Turns.Count;

        public void AddTurnState(Turn turn)
        {
            Turns.Add(turn);
        }

        public int GetMaxTurns() => _maxTurns;
    }
}

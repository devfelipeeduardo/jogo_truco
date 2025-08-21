using System.ComponentModel.DataAnnotations;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Models.Game
{
    public class Round
    {
        public List<Turn> Turns { get; private set; } = new List<Turn>();
        public void AddTurnState(Turn turn)
        {
            Turns.Add(turn);
        }
    }
}

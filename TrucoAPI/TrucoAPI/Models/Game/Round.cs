using System.ComponentModel.DataAnnotations;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Models.Game
{
    public class Round
    {
        private int _maxLength = 3;
        public List<Turn> Turns { get; private set; }
        public int CurrentTurn => Turns.Count;

        public void AddTurn(Turn turn)
        {
            Turns.Add(turn);
        }

        public int GetMaxLength()
        {
            return _maxLength;
        }
    }
}

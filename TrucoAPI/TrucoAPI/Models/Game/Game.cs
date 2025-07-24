using Microsoft.AspNetCore.Components.Web;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Models.Game
{
    public class Game
    {
        public List<Round> Rounds { get; set; } = new List<Round>();
        public List<Team> Teams { get; private set; } = new List<Team>();

        //OBS: PlayerScore será passado via Team.Score

        public Game() {;
            Teams.Add(new Team());
            Teams.Add(new Team());
        }

        public void SetTeams(List<string> playersName)
        {
            Teams[0].AddPlayer(new Player(0, playersName[0]));
            Teams[0].AddPlayer(new Player(1, playersName[1]));

            Teams[1].AddPlayer(new Player(2, playersName[2]));
            Teams[1].AddPlayer(new Player(3, playersName[3]));
        }
    }
}

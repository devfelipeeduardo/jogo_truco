using Microsoft.AspNetCore.Components.Web;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Models.Game
{
    public class Game
    {
        public List<Round> Rounds { get; set; } = new List<Round>();
        public List<Team> Teams { get; private set; } = new List<Team>();

        //OBS: PlayerScore será passado via Team.Score

        public Game() {}

        public void SetTeams(List<string> playersName)
        {

            if (playersName.Count != 4)
                throw new ArgumentException("São necessários 4 jogadores para o jogo começar!");

            Teams.Clear();
            Teams.Add(new Team());
            Teams.Add(new Team());

            Teams[0].AddPlayer(new Player(playersName[0]));
            Teams[0].AddPlayer(new Player(playersName[1]));

            Teams[1].AddPlayer(new Player(playersName[2]));
            Teams[1].AddPlayer(new Player(playersName[3]));

        }
        public List<Player> GetAllPlayers(){
            return Teams.SelectMany(team => team.getPlayers()).ToList(); 
        }
    }
}

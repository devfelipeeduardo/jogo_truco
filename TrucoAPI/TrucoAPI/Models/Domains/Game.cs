using Microsoft.AspNetCore.Components.Web;
using System.Xml.Linq;
using TrucoAPI.Models.Entities;

namespace TrucoAPI.Models.Game
{
    public class Game
    {
        private int _maxRounds = 23;
        public List<Round> Rounds { get; private set; } = new List<Round>();
        public List<Team> Teams { get; private set; } = new List<Team>();

        public Game() {}

        public void SetTeams(List<string> playersName)
        {

            if (playersName == null) throw new ArgumentNullException(nameof(playersName), "Os jogadores não foram repassados");

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
        public void AddRound(Round round)
        {
            Rounds.Add(round);
        }

        public List<Player> GetAllPlayers(){
            return Teams.SelectMany(team => team.GetPlayers()).ToList(); 
        }

        public void ResetTeamsTurnAtributtes()
        {
            foreach (var team in Teams)
            {
                team.ResetTurnScore();
                team.ResetPlayersHand();
            }
        }

        public void ResetTeamsRoundAtributtes()
        {
            foreach (var team in Teams)
            {
                team.ResetRoundScore();
            }
        }

        public int GetMaxRounds()
        {
            return _maxRounds;
        }

    }
}

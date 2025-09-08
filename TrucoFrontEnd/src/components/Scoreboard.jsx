import '../styles/Scoreboard.css'

function Scoreboard({  team1TurnScore = 0, team1RoundScore = 0, team2TurnScore = 0, team2RoundScore = 0}) {
    return (
        <>
            <div className="scoreboard-frame">
                <div>Turno: </div>
                <div className="team1-scoreboard">Nós: <span id="team1-score">{team1TurnScore}</span></div>
                <div className="team2-scoreboard">Eles: <span id="team2-score">{team2TurnScore}</span></div>
                <br></br>
                <div>Rodada: </div>
                <div className="team1-scoreboard">Nós: <span id="team1-score">{team1RoundScore}</span></div>
                <div className="team2-scoreboard">Eles: <span id="team2-score">{team2RoundScore}</span></div>
            </div>
            <div className="play-frame">
                
            </div>
        </>
    )
}

export default Scoreboard;
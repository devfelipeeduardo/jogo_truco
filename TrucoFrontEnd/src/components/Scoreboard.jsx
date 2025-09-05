import '../styles/Scoreboard.css'

function Scoreboard({  team1Score = 0, team2Score = 0}) {
    return (
        <>
            <div className="scoreboard-frame">
                <div className="team1-scoreboard">Nós: <span id="team1-score">{team1Score}</span></div>
                <div className="team2-scoreboard">Eles: <span id="team2-score">{team2Score}</span></div>
            </div>
            <div className="play-frame">
                
            </div>
        </>
    )
}

export default Scoreboard;
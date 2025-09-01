import '../styles/Scoreboard.css'

function Scoreboard() {
    return (
        <>
            <div className="scoreboard-frame">
                <div className="team1-scoreboard">Team 1: <span id="team1-score">0</span></div>
                <div className="team2-scoreboard">Team 2: <span id="team2-score">0</span></div>
            </div>
            <div className="play-frame">
                
            </div>
        </>
    )
}

export default Scoreboard;
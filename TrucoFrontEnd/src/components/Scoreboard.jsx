import '../styles/Scoreboard.css'

function Scoreboard() {
    return (
        <>
            <div className="scoreboard-frame">
                <div class="player1-scoreboard">Player1: <span id="player1-score">0</span></div>
                <div class="player2-scoreboard">Player2: <span id="player2-score">0</span></div>
                <div class="player3-scoreboard">Player3: <span id="player3-score">0</span></div>
                <div class="player4-scoreboard">Player4: <span id="player4-score">0</span></div>
            </div>
        </>
    )
}

export default Scoreboard;
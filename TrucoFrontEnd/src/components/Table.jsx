import mesaTruco from '../assets/imgs/mesa_truco.png';
import '../styles/Table.css'

function Table({
    player1, player2, player3, player4,
    cardsSelectedByPlayers, chooseCard,
    opacityAnimate, playerWinnerData, turnStateData
}) {

    return (
        <div className="table">
            <img className="table-img" src={mesaTruco} alt="Mesa do truco" />
            <div className={"player1"}>
                <div className="player-name">{player1.name}</div>
                {player1.hand.map((card, index) => (
                    <img
                        key={index}
                        src={card.image}
                        alt={`Carta ${index}`}
                        className={`card ${cardsSelectedByPlayers[0] === card ? "cardSelected" : ""}`}
                        onClick={() => { chooseCard(0, card); }}
                    />
                ))}
            </div>

            <div className="player2">
                {player2.hand.map((card, index) => (
                    <img
                    key={index}
                    src={card.image}
                    alt={`Carta ${index}`}
                    className={`card ${cardsSelectedByPlayers[1] === card ? "cardSelected" : ""}`}
                    onClick={() => chooseCard(1, card)}
                    />
                ))}
                <div className="player-name">{player2.name}</div>
            </div>

            <div className="player3">
                {player3.hand.map((card, index) => (
                    <img
                    key={index}
                    src={card.image}
                    alt={`Carta ${index}`}
                    className={`card ${cardsSelectedByPlayers[2] === card ? "cardSelected" : ""}`}
                    onClick={() => chooseCard(2, card)}
                    />
                ))}
                <div className="player-name">{player3.name}</div>
            </div>

            <div className="player4">
                {player4.hand.map((card, index) => (
                    <img
                    key={index}
                    src={card.image}
                    alt={`Carta ${index}`}
                    className={`card ${cardsSelectedByPlayers[3] === card ? "cardSelected" : ""}`}
                    onClick={() => chooseCard(3, card)}
                    />
                ))}
                <div className="player-name">{player4.name}</div>
            </div>
            <div className="trump">
                <img
                    key={5}
                    src={turnStateData.turnState.trump.image}
                    alt="Carta: Manilha"
                    className="card"
                />
            </div>
            <div className={`warningsAtCenter ${opacityAnimate ? "changesOpacity" : ""}`}>
                {"O jogador: " + playerWinnerData?.playerWinner?.name + " venceu a rodada!"}
            </div>
        </div>
    )
}

export default Table;
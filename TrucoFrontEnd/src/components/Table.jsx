import mesaTruco from '../assets/imgs/mesa_truco.png';
import '../styles/Table.css'

function Table({
    player1, player2, player3, player4,
    cardsSelectedByPlayers, chooseCard,
    opacityAnimate, playerWinnerData, data
}) {
    return (
        <div className="table">
            <img className="table-img" src={mesaTruco} alt="Mesa do truco" />
            <div className={"player1"}>
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
            </div>
            <div className="trump">
                <img key={5} src={data.turn.trump.image} alt={`Carta: Manilha`} className="card" />
            </div>
            <div className={`playerWinner ${opacityAnimate ? "changesOpacity" : ""}`}>
                {"O jogador: " + playerWinnerData?.playerWinner?.name + " venceu a rodada!"}
            </div>
        </div>
    )
}

export default Table;
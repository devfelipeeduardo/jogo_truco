import { useEffect, useState } from "react";

function Players() {
  const [data, setData] = useState(null);
  const [playerWinnerData, setPlayerWinnerData] = useState(null);
  const [cardsSelectedByPlayers, setCardsSelectedByPlayers] = useState([null, null, null, null]);

  function getPlayer(playerName) {
    for (const team of data.teams) {
      const player = team.players.find(p => p.name.toLowerCase() === playerName)
      if (player) return player;
    }
    return null;
  }


  function chooseCard(playerIndex, card) {
    setCardsSelectedByPlayers(prevLista =>
      prevLista.map((item, index) => (index === playerIndex ? card : item))
    );
  }


  useEffect(() => {
    async function initGame() {
      try {
        // 1 - inicia o jogo
        await fetch('http://localhost:5150/api/game/start', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(["felipe", "pedro", "jonathan", "gabriel"])
        });

        // 2 - inicia o round
        await fetch('http://localhost:5150/api/game/round/start', {
          method: 'POST'
        });

        // 3 - inicia o turno
        const turnResponse = await fetch('http://localhost:5150/api/game/turn/start', { method: 'GET' });
        const turnData = await turnResponse.json();
        console.log("Turno completo:", turnData);
        const turnState = turnData.turnState; // pega só o estado do turno


        // 4 - pega o estado atualizado (com trump e mãos)
        const stateResponse = await fetch('http://localhost:5150/api/game/state');
        const stateData = await stateResponse.json();
        setData({
          ...stateData.gameState,
          turn: turnState
        });

      } catch (error) {
        console.error("Erro ao iniciar jogo:", error);
      }
    }
    initGame();
  }, []);


  async function getWinner() {
    try {
      const playerWinnerResponse = await fetch('http://localhost:5150/api/turn/decide-winner', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(cardsSelectedByPlayers)
      })
      // const playerWinnerData = await playerWinnerResponse.json();
      setPlayerWinnerData(playerWinnerData);
    } catch (error) {
      console.error("Erro ao definir o vencedor:", error);
    }
  }

  useEffect(() => {
    const everyCardFilled = cardsSelectedByPlayers.every(pair => pair !== null);
    if (everyCardFilled) {
      getWinner();
    }
  }, [cardsSelectedByPlayers]);

  if (!data) return <p>Carregando...</p>;

  const player1 = getPlayer("felipe")
  const player2 = getPlayer("pedro")
  const player3 = getPlayer("jonathan")
  const player4 = getPlayer("gabriel")

  return (
    <>
      <div className="player1">
        {player1.hand.map((card, index) => (
          <img
            key={index}
            src={card.image}
            alt={`Carta ${index}`}
            className="card"
            onClick={() => chooseCard(0, card)} // índice 0 = player1
          />
        ))}
      </div>

      <div className="player2">
        {player2.hand.map((card, index) => (
          <img
            key={index}
            src={card.image}
            alt={`Carta ${index}`}
            className="card"
            onClick={() => chooseCard(1, card)} // índice 1 = player2
          />
        ))}
      </div>

      <div className="player3">
        {player3.hand.map((card, index) => (
          <img
            key={index}
            src={card.image}
            alt={`Carta ${index}`}
            className="card"
            onClick={() => chooseCard(2, card)} // índice 2 = player3
          />
        ))}
      </div>

      <div className="player4">
        {player4.hand.map((card, index) => (
          <img
            key={index}
            src={card.image}
            alt={`Carta ${index}`}
            className="card"
            onClick={() => chooseCard(3, card)} // índice 3 = player4
          />
        ))}
      </div>
      <div className="trump">
        <img key={5} src={data.turn.trump.image} alt={`Carta: Manilha`} className="card" />
      </div>
      <div className="teste">
        {playerWinnerData?.playerWithCardWithHighestValue.name}
      </div>
    </>
  );
};

export default Players;

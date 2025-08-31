import { useCallback, useEffect, useState } from "react";

function Game() {
  const [data, setData] = useState(null);
  const [playerWinnerData, setPlayerWinnerData] = useState(null);
  const [cardsSelectedByPlayers, setCardsSelectedByPlayers] = useState([null, null, null, null]);
  const [opacityAnimate, setOpacityAnimate] = useState(false);

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


  const getWinner = useCallback(async () => {
    try {
      const response = await fetch('http://localhost:5150/api/game/turn/decide-winner', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(cardsSelectedByPlayers)
      });

      const data = await response.json();
      setPlayerWinnerData(data);
    } catch (error) {
      console.error("Erro ao definir o vencedor:", error);
    }
  }, [cardsSelectedByPlayers]);

  useEffect(() => {
    const everyCardFilled = cardsSelectedByPlayers.every(pair => pair !== null);
    if (everyCardFilled) {
      getWinner();
      setOpacity();
    }
  }, [cardsSelectedByPlayers, getWinner]);
  
  function setOpacity() {
    setOpacityAnimate(false);
    setTimeout(() => setOpacityAnimate(true), 10);
  };
  
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
            onClick={() => chooseCard(0, card)}
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
            className="card"
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
            className="card"
            onClick={() => chooseCard(3, card)}
          />
        ))}
      </div>
      <div className="trump">
        <img key={5} src={data.turn.trump.image} alt={`Carta: Manilha`} className="card" />
      </div>
      <div className={`playerWinner ${opacityAnimate ? "changesOpacity" : ""}`}>
        {"O jogador: " + playerWinnerData?.playerWinner?.name + " venceu a rodada!" }
      </div>
    </>
  );
};

export default Game;

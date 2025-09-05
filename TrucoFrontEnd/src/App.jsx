import { useCallback, useEffect, useState } from "react";
import Scoreboard from './components/Scoreboard';
import Table from './components/Table';
import './styles/App.css'
import Warnings from "./components/Warnings";

function App() {
  const [data, setData] = useState(null);
  const [playerWinnerData, setPlayerWinnerData] = useState(null);
  const [cardsSelectedByPlayers, setCardsSelectedByPlayers] = useState([null, null, null, null]);
  const [opacityAnimate, setOpacityAnimate] = useState(false);

  //Inicia jogo e pega o state inicial.
  useEffect(() => {
    async function initGame() {
      try {
        await fetch('http://localhost:5150/api/game/start', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(["felipe", "pedro", "jonathan", "gabriel"])
        });

        const turnResponse = await fetch('http://localhost:5150/api/game/turn/start', { method: 'GET' });
        const turnData = await turnResponse.json();
        console.log("Turno completo:", turnData);
        const turnState = turnData.turnState;

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

  //Pega o estado dos jogadores baseado na data setada!
  function getPlayer(playerName) {
    for (const team of data.teams) {
      const player = team.players.find(p => p.name.toLowerCase() === playerName)
      if (player) return player;
    }
    return null;
  }

  //Pega a carta escolhida por cada jogador
  function chooseCard(playerIndex, card) {
    setCardsSelectedByPlayers(prevLista =>
      prevLista.map((item, index) => (index === playerIndex ? card : item))
    );
  }

  //Guarda o método de pegar o vencedor, para que só pegue ele quando o useEffect repara que todos os jogadores selecionaram uma carta.
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

  //Funcao de piscar na tela o vencedor. [PRECISA SER AJUSTADA]
  function setOpacity() {
    setOpacityAnimate(false);
    setTimeout(() => setOpacityAnimate(true), 10);
  };

  //Função que confere se todos os jogadores selecionaram cartas.
  useEffect(() => {
    const everyCardFilled = cardsSelectedByPlayers.every(pair => pair !== null);
    if (everyCardFilled) {
      getWinner();
      setOpacity();
    }
  }, [cardsSelectedByPlayers, getWinner]);

  //Limite de código de requests
  if (!data) return <p>Carregando...</p>;

  const player1 = getPlayer("felipe");
  const player2 = getPlayer("pedro");
  const player3 = getPlayer("jonathan");
  const player4 = getPlayer("gabriel");

  return (
    <>
      <Table
        player1={player1}
        player2={player2}
        player3={player3}
        player4={player4}
        cardsSelectedByPlayers={cardsSelectedByPlayers}
        chooseCard={chooseCard}
        opacityAnimate={opacityAnimate}
        playerWinnerData={playerWinnerData}
        data={data}
      />

      <Scoreboard
        team1Score={data.teams[0].score}
        team2Score={data.teams[1].score}
      />

      {/* <Warnings
        message={playerWinnerData}
      /> */}
    </>
  )
}

export default App

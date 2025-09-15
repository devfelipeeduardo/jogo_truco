import { useCallback, useEffect, useState } from "react";
import Scoreboard from './components/Scoreboard';
import Table from './components/Table';
import './styles/App.css'
// import Warnings from "./components/Warnings";

function App() {
  const [gameStateData, setGameStateData] = useState(null);
  const [turnStateData, setTurnStateData] = useState(null);
  const [playerWinnerData, setPlayerWinnerData] = useState(null);
  const [teamGameWinnerData, setTeamGameWinnerData] = useState(null);
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

        const gameStateResponse = await fetch('http://localhost:5150/api/game/state');
        const gameStateData = await gameStateResponse.json();
        setGameStateData(gameStateData.gameState);
        updateTurnState();

      } catch (error) {
        console.error("Erro ao iniciar jogo:", error);
      }
    }
    initGame();
  }, []);

  async function updateGameState() {
    try {
      const gameStateResponse = await fetch('http://localhost:5150/api/game/state');
      const gameStateData = await gameStateResponse.json();

      // Utilize o "..." quando você quiser atualizar atributos de uma determinada classe, antes de setar no state
      // Descobrir isso é muito bom kkkkkk
      setGameStateData({
        ...gameStateData.gameState
      });

      console.log("Jogo completo:", gameStateData);

    } catch (error) {
      console.error("Erro ao iniciar jogo:", error);
    }
  }

  async function updateTurnState() {
    try {
      const turnStateResponse = await fetch('http://localhost:5150/api/game/turn/state');
      const turnStateData = await turnStateResponse.json();

      setTurnStateData(turnStateData);

      console.log("Turno Completo:" + JSON.stringify(turnStateData, null, 2));
      console.log("Manilha: " + JSON.stringify(turnStateData.turnState.trump.image, null, 2));

    } catch (error) {
      console.error("Erro ao iniciar jogo:", error);
    }
  }

  //Pega o estado dos jogadores baseado na data setada!
  function getPlayer(playerName) {
    for (const team of gameStateData.teams) {
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

  const resetSelectedCardsByPlayer = useCallback(async () => {
    cardsSelectedByPlayers.fill(null);
  }, [cardsSelectedByPlayers]);

  //Guarda o método de pegar o vencedor, para que só pegue ele quando o useEffect repara que todos os jogadores selecionaram uma carta.
  const getWinner = useCallback(async () => {
    try {
      const response = await fetch('http://localhost:5150/api/game/turn/decide-winner', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(cardsSelectedByPlayers)
      });

      const winnerData = await response.json();
      setPlayerWinnerData(winnerData);

    } catch (error) {
      console.error("Erro ao definir o vencedor:", error);
    }
  }, [cardsSelectedByPlayers]);

  //Funcao de piscar na tela o vencedor. [PRECISA SER AJUSTADA]
  function setOpacity() {
    setOpacityAnimate(false);
    setTimeout(() => setOpacityAnimate(true), 10);
  };


  const checkGameWinner = useCallback(async () => {
    if (gameStateData.gameWinner != null) {
      setTeamGameWinnerData(gameStateData.gameWinner)
    }
  }, [gameStateData]);

  //Função que confere se todos os jogadores selecionaram cartas.
  useEffect(() => {
    const everyCardFilled = cardsSelectedByPlayers.every(pair => pair !== null);
    if (everyCardFilled) {
      getWinner();
      resetSelectedCardsByPlayer();
      checkGameWinner();
      updateGameState();
      updateTurnState();
      setOpacity();
    }
  }, [cardsSelectedByPlayers, getWinner, resetSelectedCardsByPlayer, checkGameWinner]);


  if (!gameStateData || !turnStateData) return <p>Carregando...</p>;

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
        turnStateData={turnStateData}
        teamGameWinnerData={teamGameWinnerData}
      />

      <Scoreboard
        team1TurnScore={gameStateData.teams[0].turnScore}
        team1RoundScore={gameStateData.teams[0].roundScore}
        team2TurnScore={gameStateData.teams[1].turnScore}
        team2RoundScore={gameStateData.teams[1].roundScore}
      />

      {/* <Warnings
        message={playerWinnerData}
      /> */}
    </>
  )
}

export default App;
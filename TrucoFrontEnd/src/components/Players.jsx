import { useCallback, useEffect, useState } from "react";

function Players() {
  const [data, setData] = useState(null);
  const [winnerData, setWinnerData] = useState(null);
  const [cardsSelectedByPlayers, setCardsSelectedByPlayers] = useState([null, null, null, null]);

  function getPlayer(playerName) {
    return data.players.find(j => j.name.toLowerCase() === playerName);
  }

  function chooseCard(player, card) {
    switch (player) {
      case player1:
        setCardsSelectedByPlayers(prevLista =>
          prevLista.map((item, index) =>
            index === 0 ? card : item)
        )
        break;
      case player2:
        setCardsSelectedByPlayers(prevLista =>
          prevLista.map((item, index) =>
            index === 1 ? card : item)
        )
        break;
      case player3:
        setCardsSelectedByPlayers(prevLista =>
          prevLista.map((item, index) =>
            index === 2 ? card : item)
        )
        break;
      case player4:
        setCardsSelectedByPlayers(prevLista =>
          prevLista.map((item, index) =>
            index === 3 ? card : item)
        )
        break;
    }
  }

  useEffect(() => {
    fetch('http://localhost:5150/api/jogo/iniciar', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(["felipe", "pedro", "jonathan", "gabriel"])
    })
      .then(response => response.json())
      .then(data => {
        console.log("Resposta:", data);
        setData(data);
      })
      .catch(error => console.error("Deu erro:", error));
  }, [])

  function getWinner() {
    fetch('http://localhost:5150/api/jogo/decidirVencedor', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(cardsSelectedByPlayers)
    })
      .then(response => response.json())
      .then(data => {
        console.log("Resposta:", data);
        setWinnerData(data);
      })
      .catch(error => console.error("Deu erro:", error));
  };

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
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" onClick={() => chooseCard(player1, card)} />
        ))}
      </div>
      <div className="player2">
        {player2.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" onClick={() => chooseCard(player2, card)} />
        ))}
      </div>
      <div className="player3">
        {player3.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" onClick={() => chooseCard(player3, card)} />
        ))}
      </div>
      <div className="player4">
        {player4.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" onClick={() => chooseCard(player4, card)} />
        ))}
      </div>
      <div className="trump">
        <img key={5} src={data.trump.image} alt={`Carta: Manilha`} className="card" />
      </div>
      <div className="teste">
        {winnerData?.playerWithCardWithHighestValue.name}
      </div>
    </>
  );
}

export default Players;

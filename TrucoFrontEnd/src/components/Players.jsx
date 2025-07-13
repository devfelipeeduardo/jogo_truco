import { useEffect, useState } from "react";

function Players() {
  const [data, setGame] = useState(null);
  const [cardsSelected, setCardsSelected] = useState([null, null, null, null]);

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
        setGame(data);
      })
      .catch(error => console.error("Deu erro:", error));
  }, []);

  function decideVencedor(cardsSelectedPlayer1, cardsSelectedPlayer2, cardsSelectedPlayer3, cardsSelectedPlayer4) {
    fetch('http://localhost:5150/api/jogo/decidirVencedor', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify([cardsSelectedPlayer1, cardsSelectedPlayer2, cardsSelectedPlayer3, cardsSelectedPlayer4])
    })
      .then(response => response.json())
      .then(data => {
        console.log("Resposta:", data);
        setGame(data);
      })
      .catch(error => console.error("Deu erro:", error));

    return data;
  }

  function getPlayer(playerName) {
    return data.players.find(j => j.name.toLowerCase() === playerName);

  }
  function chooseCard(playerIndex, card) {
        setCardOnList(card, playerIndex);
  }

  function setCardOnList(card, i) {
    setCardsSelected(prevLista =>
      prevLista.map((item, index) =>
        index === i ? card : item)
    )
    console.log("card: ", card)
  }

  useEffect(() => {
    console.log("Cards Atualizados", cardsSelected);
  }, [cardsSelected]);

  if (!data) return <p>Carregando...</p>;

  const player1 = getPlayer("felipe")
  const player2 = getPlayer("pedro")
  const player3 = getPlayer("jonathan")
  const player4 = getPlayer("gabriel")

  return (
    <>
      <div className="player1">
        {player1.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" onClick={() => chooseCard(0, card)} />
        ))}
      </div>
      <div className="player2">
        {player2.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" onClick={() => chooseCard(1, card)} />
        ))}
      </div>
      <div className="player3">
        {player3.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" onClick={() => chooseCard(2, card)} />
        ))}
      </div>
      <div className="player4">
        {player4.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" onClick={() => chooseCard(3, card)} />
        ))}
      </div>
      <div className="trump">
        <img key={5} src={data.trump.image} alt={`Carta: Manilha`} className="card" />
      </div>
      <div>
        <button onClick={() => decideVencedor(...cardsSelected)}>
          play
        </button>
      </div>
    </>
  );
}

export default Players;

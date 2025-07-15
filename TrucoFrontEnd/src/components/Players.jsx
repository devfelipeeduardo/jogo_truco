import { startTransition, useEffect, useState } from "react";

function Players() {
  const [data, setData] = useState(null);
  const [cardsSelected, setCardsSelected] = useState([[null, null], [null, null], [null, null], [null, null]]);

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
  }, []);

  function getWinner(pairPlayerCard1, pairPlayerCard2, pairPlayerCard3, pairPlayerCard4) {
    fetch('http://localhost:5150/api/jogo/decidirVencedor', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify([pairPlayerCard1, pairPlayerCard2, pairPlayerCard3, pairPlayerCard4])
    })
      .then(response => response.json())
      .then(data => {
        console.log("Resposta:", data);
        setData(data);
      })
      .catch(error => console.error("Deu erro:", error));

    return data;
  }

  function getPlayer(playerName) {
    return data.players.find(j => j.name.toLowerCase() === playerName);
  }

  function chooseCard(player, card) {
    setCardOnList(player, card);
  }

  function setCardOnList(player, card) {
    switch (player) {
      case player1:
        setCardsSelected(prevLista =>
          prevLista.map((item, index) =>
            index === 0 ? [player, card] : item)
        )
        break;
      case player2:
        setCardsSelected(prevLista =>
          prevLista.map((item, index) =>
            index === 1 ? [player, card] : item)
        )
        break;
      case player3:
        setCardsSelected(prevLista =>
          prevLista.map((item, index) =>
            index === 2 ? [player, card] : item)
        )
        break;
      case player4:
        setCardsSelected(prevLista =>
          prevLista.map((item, index) =>
            index === 3 ? [player, card] : item)
        )
        break;
    }
  }

  useEffect(() => {
    const everyCardFilled = cardsSelected.every(card => card !== null);
    if (everyCardFilled) {
      getWinner();
    }
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
        <button onClick={() => getWinner(...cardsSelected)}>
          play
        </button>
      </div>
    </>
  );
}

export default Players;

import { useEffect, useState } from "react";

function Players() {
  const [game, setGame] = useState(null);

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

  if (!game) return <p>Carregando...</p>;

  const player1 = game.players.find(j => j.name.toLowerCase() === "felipe");
  const player2 = game.players.find(j => j.name.toLowerCase() === "pedro");
  const player3 = game.players.find(j => j.name.toLowerCase() === "jonathan");
  const player4 = game.players.find(j => j.name.toLowerCase() === "gabriel");

  return (
    <>
      <div className="player1">
        {player1.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" />
        ))}
      </div>
      <div className="player2">
        {player2.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" />
        ))}
      </div>
      <div className="player3">
        {player3.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" />
        ))}
      </div>
      <div className="player4">
        {player4.hand.map((card, index) => (
          <img key={index} src={card.image} alt={`Carta ${index}`} className="card" />
        ))}
      </div>
      <div className="trump">
          <img key={5} src={game.trump.image} alt={`Carta: Manilha`} className="card" />
      </div>
    </>
  );
}

export default Players;

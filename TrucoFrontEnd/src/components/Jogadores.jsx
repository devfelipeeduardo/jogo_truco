import { useEffect, useState } from "react";

function Jogadores() {
  const [carta, setCartas] = useState(null);

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
        setCartas(data);
      })
      .catch(error => console.error("Deu erro:", error));
  }, []);

  if (!carta) return <p>Carregando...</p>;

  const jogador1 = carta.jogadores.find(j => j.nome.toLowerCase() === "felipe");
  const jogador2 = carta.jogadores.find(j => j.nome.toLowerCase() === "pedro");
  const jogador3 = carta.jogadores.find(j => j.nome.toLowerCase() === "jonathan");
  const jogador4 = carta.jogadores.find(j => j.nome.toLowerCase() === "gabriel");

  return (
    <>
      <div className="player1">
        {jogador1.mao.map((carta, index) => (
          <img key={index} src={carta.image} alt={`Carta ${index}`} className="card" />
        ))}
      </div>
      <div className="player2">
        {jogador2.mao.map((carta, index) => (
          <img key={index} src={carta.image} alt={`Carta ${index}`} className="card" />
        ))}
      </div>
      <div className="player3">
        {jogador3.mao.map((carta, index) => (
          <img key={index} src={carta.image} alt={`Carta ${index}`} className="card" />
        ))}
      </div>
      <div className="player4">
        {jogador4.mao.map((carta, index) => (
          <img key={index} src={carta.image} alt={`Carta ${index}`} className="card" />
        ))}
      </div>
      <div className="trump">
          <img key={5} src={carta.manilha.image} alt={`Carta: Manilha`} className="card" />
      </div>
    </>
  );
}

export default Jogadores;

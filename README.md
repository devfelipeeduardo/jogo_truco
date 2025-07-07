![Logo Truco](https://s.zst.com.br/cms-assets/2024/11/como-jogar-truco-capa-buscape.webp)
# Truco

Welcome to **Truco**, a fun card game where you can play versus AI. This project is a non-commercial game, made for entertainment purposes only.

## Demonstration:

You can test the game demo at: Demo: 🚧 Not deployed yet – coming soon!

## Project Overview

### Description

"Truco" is a traditional card game, played by 2 or 4 people, where players compete to win rounds by comparing cards values and bluffing to increase the current stakes.

### Features

- **Scoreboard**: It counts the player's score and shows it.
- **Player and Cards**: It shows the player's icon, and cards that are used to play.
- **Table**: It shows the deck and trump.
- **Sound Control**: Mute and unmute the game sounds. [Not Implemented Yet]

### Stacks Used

- **HTML**: Markup language used for structuring the game interface.
- **CSS**: Styling language used for designing the game interface.
- **JavaScript**: Programming language used for DOM interactions.
- **ASP.NET WEBAPI(C#)**: Framework for game logic and requisitons to API DeckOfCards.
- **API DeckOfCards**: Api used to get cards from decks and shuffle: "https://www.deckofcardsapi.com/".

## Project Structure

The project structure is as follows:
```
Game/
├──TrucoAPI
│ └──.vs
│ └── TrucoAPI.sln
│ └── TrucoAPI
│ │ └── Controllers
│ │ │ └── JogoController.cs
│ │ └── Models
│ │ │ └── Card.cs
│ │ │ └── DeckResponse.cs
│ │ │ └── Jogador.cs
│ │ │ └── Partida.cs
│ │ └── Services
│ │ │ └── DeckService.cs
│ │ │ └── JogoService.cs
│ │ └── Program.cs
├──TrucoFrontEnd
│ └── src
│ │ └── components
│ │ │ └── Jogadores.jsx
│ │ │ └── Table.jsx
│ │ └── styles
│ │ │ └── App.css
│ │ │ └── Table.css
│ │ │ └── index.css
│ │ └── App.jsx
│ │ └── Main.jsx
└── README.md
```

## Logic Explanation

The game logic is implemented using ASP.NET WEBAPI. Here is a brief overview of the main components:

- **JogoController.cs**: Handles the game logic, uniting the Services. Not using Dependency Injection yet, it will be implemented.
  The game state will be stored in-memory within the JogoService. 
- **DeckService.cs**: Contains the Deck Requisitions, to get the deck and cards from decks.
- **JogoService.cs**: Manages the Game Logic.

## Getting Started

To run the project locally, follow these steps:

### Prerequisites

Make sure you have the following installed:

- [Git](https://git-scm.com/)
- [Node.js](https://nodejs.org/)
- [.NET/C#](https://dotnet.microsoft.com/pt-br/download/visual-studio-sdks/)
- [React](https://react.dev/)

### Installation

1. **Clone the repository**

```bash
  git clone https://github.com/devfelipeeduardo/jogo_truco.git
```

2. **Navigate to the project directory**

```bash
cd jogo_truco
```

### Running the Project

After installing all dependencies and starting the localhost server, runs the game in your preferred browser:

Runs Localhost
```Console
cd TrucoAPI
dotnet run
```


Install Dependencies:
```Console
npm install
```

Run:
```Console
npm run dev
```

### Contributing

Contributions are welcome! If you have suggestions or improvements, feel free to open an issue or submit a pull request.

### License

This project is licensed under the MIT License.

### Disclaimer

© This is a non-commercial project. All cards images and assets are used for educational purposes only.

### Contact

[Github](https://github.com/felipefreitasdev)
[Linkedin]((https://www.linkedin.com/in/felipefreitasof/))

![Build](https://img.shields.io/badge/build-passing-brightgreen)
### [This project is a work in progress]
![Build](https://img.shields.io/badge/build-passing-brightgreen)


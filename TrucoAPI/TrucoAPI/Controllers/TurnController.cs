﻿using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TrucoAPI.Models.Entities;
using TrucoAPI.Services;

namespace TrucoAPI.Controllers
{
    [ApiController]
    [Route("api/jogo")]
    public class TurnController : Controller
    {
        private readonly TurnService _turnService;

        public TurnController(TurnService gameService) { 
            _turnService = gameService;
        }

        [HttpPost("iniciar")]
        public async Task<IActionResult> StartTurn([FromBody] List<string> players)
        {
            if (players.Count != 2 && players.Count != 4)
                return BadRequest("O jogo precisa de 2 ou 4 jogadores!");

            //await _turnService.StartTurn(players.ToArray());
            //Primeiro estou fazendo a lógica, depois crio o endpoint.
            return Ok(_turnService.GetTurnState());
        }

        [HttpGet]
        public IActionResult GetPartidaState()
        {
            return Ok(_turnService.GetTurnState());
        }

        [HttpPost("decidirVencedor")]
        public IActionResult ReturnWinner([FromBody] List<Card> cards)
        { 
            _turnService.DecideWinner(cards);
            return Ok(_turnService.GetTurnState());
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using TrucoAPI.Models;

namespace TrucoAPI.Services
{
    public class RoundService
    {
        private readonly TurnService _turnService;

        public RoundService(TurnService turnService)
        {
            _turnService = turnService;
        }

        private Round _round = new Round();
        public async Task StartRoundAsync(string[] players)
        {
            await _turnService.StartTurnAsync(players.ToArray());
            
        }

        public Round GetRoundState() => _round;

    }
}

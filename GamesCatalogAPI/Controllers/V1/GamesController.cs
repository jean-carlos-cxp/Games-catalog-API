using GamesCatalogAPI.InputModel;
using GamesCatalogAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogAPI.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> FindAll()
        {
            return Ok();
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> FindById(Guid idGame)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame(GameInputModel game)
        {
            return Ok();
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame(Guid idGame, GameInputModel game)
        {
            return Ok();
        }

        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGame(Guid idGame, double price)
        {
            return Ok();
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<AcceptedResult> DeleteGame(Guid idGame)
        {
            return Ok();
        }
    }
}

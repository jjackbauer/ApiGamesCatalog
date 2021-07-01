using ApiGamesCatalog.Exceptions;
using ApiGamesCatalog.InputModel;
using ApiGamesCatalog.Services;
using ApiGamesCatalog.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }
        /// <summary>
        /// Get all games registered in the system, in list ordered by one of the collumns 
        /// </summary>
        /// <param name="page">Indicate the current page. Minimum is 1</param>
        /// <param name="quantity">Indicate the quantity of itens by page. Minimum is 1 item e Maximum is 50 itens</param>
        /// <param name="orderBy">Selects the element that is used to sort the list. Must be Id, Name, Producer or Price </param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5, [FromQuery,RegularExpression(@"^(?:Id|Name|Producer|Price)$", ErrorMessage = "You must order by Id, Name, Producer or Price.")] string orderBy = "Name")
        {
            var games = await _gameService.Get(page, quantity, orderBy);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }
        /// <summary>
        /// Get a game registered in the system
        /// </summary>
        /// <param name="gameId">Unique Id of a game registred in the system</param>
        /// <returns></returns>
        [HttpGet("{gameId:guid}")]
        public async Task<ActionResult<List<GameViewModel>>> Get([FromRoute]Guid gameId)
        {
            var game = await _gameService.Get(gameId);

            return game == null ? NoContent() : Ok(game);
        }
        /// <summary>
        /// Register a game in the system
        /// </summary>
        /// <param name="gameInput">Game object to be registered</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> Insert([FromBody] GameInputModel gameInput)
        {
            try
            {
                var game = await _gameService.Insert(gameInput);
                return Ok(game);
            }
            catch (GameAlreadyRegisteredException)
            {
                return UnprocessableEntity("There is a register of this game already in the database");
            }
        }
        /// <summary>
        /// Update the informations about a game registered in the system
        /// </summary>
        /// <param name="gameId">Unique Id of a game registered in the system</param>
        /// <param name="gameInput">Game object with updates wanted</param>
        /// <returns></returns>
        [HttpPut("{gameId:guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid gameId, [FromBody] GameInputModel gameInput)
        {
            try
            {
                await _gameService.Update(gameId, gameInput);
                return Ok();
            }
            catch (GameNotRegisteredException)
            {
                return NotFound("This game doesn't exists");
            }
        }
        /// <summary>
        /// Update the price of a game registered in the system
        /// </summary>
        /// <param name="gameId">Unique Id of a game registered in the system</param>
        /// <param name="price">New Price of this game, Mininum is 0 and Maximum is 1000</param>
        /// <returns></returns>
        [HttpPatch("{gameId:guid}/price/{price:double}")]
        public async Task<ActionResult> Update([FromRoute] Guid gameId, [FromRoute] double price)
        {
            try
            {
                await _gameService.Update(gameId, price);
                return Ok();
            }
            catch (GameNotRegisteredException)
            {
                return NotFound("This game doesn't exists");
            }
        }
        /// <summary>
        /// Remove a registered game of the system
        /// </summary>
        /// <param name="gameId">Unique Id of a game registered in the system</param>
        /// <returns></returns>
        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid gameId)
        {
            try
            {
                await _gameService.Delete(gameId);
                return Ok();
            }
            catch (GameNotRegisteredException)
            {
                return NotFound("This game doesn't exists");
            }
        }
    }
}

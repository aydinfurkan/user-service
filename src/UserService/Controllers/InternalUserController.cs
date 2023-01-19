using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Controllers.ViewModels.RequestModels;
using UserService.Controllers.ViewModels.ResponseModels;
using UserService.Exceptions;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Authorize("Internal")]
    [Route("/user/internal")]
    public class InternalUserController : ControllerBase
    {
        private readonly ILogger<InternalUserController> _logger;
        private readonly IUserService _service;

        public InternalUserController(ILogger<InternalUserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Replace a user character
        /// </summary>
        /// <param name="userId">Character id</param>
        /// <param name="replaceCharacterRequestModel">Character to be replaced</param>
        /// <returns>Replaced character id.</returns>
        /// <response code="200">Returns character id.</response>
        /// <response code="400">The id is not valid.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("character")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReplaceCharacter([FromQuery] Guid userId,[FromBody] ReplaceCharacterRequestModel replaceCharacterRequestModel)
        {
            var character = await _service.ReplaceCharacter(userId, replaceCharacterRequestModel);
            if (character == null)
                throw new UserNotFound(userId.ToString());
            
            return Ok(new ReplaceCharacterResponseModel(character.Id, true));
        }
    }
}
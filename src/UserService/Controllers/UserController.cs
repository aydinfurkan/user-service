using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Controllers.ViewModels.RequestModels;
using UserService.Controllers.ViewModels.ResponseModels;
using UserService.Exceptions;
using UserService.Helpers.Authorize.PToken;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Authorize("External")]
    [Route("/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private readonly IToken _token;
        private readonly Guid _randomUserGenerateId = new Guid("f5a7b845-2a44-4c88-a89f-6a5ab5892015");

        public UserController(ILogger<UserController> logger, IUserService service, IToken token)
        {
            _logger = logger;
            _service = service;
            _token = token;
        }
        
        /// <summary>
        /// Get a single user by given PToken
        /// </summary>
        /// <returns>A user model.</returns>
        /// <response code="200">Returns user model.</response>
        /// <response code="401">The p_token is not valid.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            //var pTokenClaims = _token.ParseToken(Request.Cookies[PTokenHelper.PTokenKey]);
            var pTokenClaims = _token.GetClaims(HttpContext.User);

            if (pTokenClaims.UserId == _randomUserGenerateId)
            {
                return Ok(UserResponseModel.Random());
            }
            
            var user = await _service.GetUserById(pTokenClaims.UserId);
            if (user == null)
                throw new UserNotFound(pTokenClaims.UserId.ToString());

            return Ok(new UserResponseModel(user));
        }
        
        /// <summary>
        /// Create a character
        /// </summary>
        /// <param name="createCharacterRequestModel">Character to be created</param>
        /// <returns>Created character id.</returns>
        /// <response code="200">Returns created character id.</response>
        /// <response code="400">The id is not valid.</response>
        /// <response code="404">Character not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("character")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequestModel createCharacterRequestModel)
        {
            //var pTokenClaims = _token.ParseToken(Request.Cookies[PTokenHelper.PTokenKey]);
            var pTokenClaims = _token.GetClaims(HttpContext.User);
            
            var character = await _service.CreateCharacter(pTokenClaims.UserId, createCharacterRequestModel);
            if (character == null)
                throw new UserNotFound(pTokenClaims.UserId.ToString());
            
            return Ok(new CreateCharacterResponseModel(character.Id, true));
        }
        
        /// <summary>
        /// Delete character
        /// </summary>
        /// <param name="deleteCharacterRequestModel">The Character Id of the character to delete.</param>
        /// <returns>Deleted character's id.</returns>
        /// <response code="200">Returns deleted character's id.</response>
        /// <response code="400">The id was invalid.</response>
        /// <response code="404">Character not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("character")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DeleteUserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCharacter([FromBody] DeleteCharacterRequestModel deleteCharacterRequestModel)
        {
            //var pTokenClaims = _token.ParseToken(Request.Cookies[PTokenHelper.PTokenKey]);
            var pTokenClaims = _token.GetClaims(HttpContext.User);
            
            var success = await _service.DeleteCharacter(pTokenClaims.UserId, deleteCharacterRequestModel.CharacterId);
            return Ok(new DeleteCharacterResponseModel(deleteCharacterRequestModel.CharacterId, success));
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">The id of the user to delete.</param>
        /// <returns>Deleted user's id.</returns>
        /// <response code="200">Returns deleted user's id.</response>
        /// <response code="400">The id was invalid.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DeleteUserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete()
        {
            //var pTokenClaims = _token.ParseToken(Request.Cookies[PTokenHelper.PTokenKey]);
            var pTokenClaims = _token.GetClaims(HttpContext.User);
            
            var id = pTokenClaims.UserId;
            
            var result = await _service.DeleteUser(id);
            if(!result)
                throw new UserNotFound(id.ToString());
            return Ok(new DeleteUserResponseModel(id, true));

        }

    }
}
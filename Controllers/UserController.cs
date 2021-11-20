using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Authorize;
using UserService.Exceptions;
using UserService.Services;
using UserService.ViewModels.GooglePayload;
using UserService.ViewModels.RequestModels;
using UserService.ViewModels.ResponseModels;

namespace UserService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private readonly IToken _token;

        public UserController(ILogger<UserController> logger, IUserService service, IToken token)
        {
            _logger = logger;
            _service = service;
            _token = token;
        }
        
        /// <summary>
        /// Check if the id is invalid
        /// </summary>
        /// <returns>Whether the given id is valid or not.</returns>
        /// <response code="200">Id is valid.</response>
        /// <response code="404">Id is not valid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("authorize")]
        [AllowAnonymous]
        public async Task<IActionResult> Authorize([FromQuery] string googleIdToken) // Google token
        {
            var googleUser =  await GoogleJsonWebSignature.ValidateAsync(googleIdToken);
            
            var user = await _service.GetUserByEmail(googleUser.Email);
            if (user == null)
                throw new UserNotFound(googleUser.Email);

            var token = _token.CreateToken(user);
            return Ok(token);
        }
        
        
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="createUserRequestModel">Google Id Token to be created</param>
        /// <returns>The created user's id.</returns>
        /// <response code="201">Returns created user's id.</response>
        /// <response code="400">The model is not valid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateUserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestModel createUserRequestModel)  // Google token
        {
            var googleUser =  await GoogleJsonWebSignature.ValidateAsync(createUserRequestModel.GoogleIdToken);
            
            var newUser = googleUser.ToModel();
            var userId = await _service.CreateUser(newUser);
            
            var token = _token.CreateToken(newUser);
            
            var createdAtPath = $"/verify";
            return Created(createdAtPath, new CreateUserResponseModel(token, true));
        }

        /// <summary>
        /// Get a single user by id
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>A user with the given id.</returns>
        /// <response code="200">Returns user with the given id.</response>
        /// <response code="400">The id is not valid.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("verify")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Verify()
        {
            var userTokenClaims = _token.GetClaims(HttpContext.User);

            var user = await _service.GetUserByEmail(userTokenClaims.Email);
            if (user == null)
                throw new UserNotFound(userTokenClaims.UserId.ToString());
            
            return Ok(new UserResponseModel(user));
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
            var userTokenClaims = _token.GetClaims(HttpContext.User);
            var id = userTokenClaims.UserId;
            
            var result = await _service.DeleteUser(id);
            if(!result)
                throw new UserNotFound(id.ToString());
            return Ok(new DeleteUserResponseModel(id, true));

        }
        
        /// <summary>
        /// Hard delete user
        /// </summary>
        /// <param name="id">The id of the user to delete.</param>
        /// <returns>Deleted user's id.</returns>
        /// <response code="200">Returns deleted user's id.</response>
        /// <response code="400">The id was invalid.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("hard")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(DeleteUserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> HardDelete()
        {
            var userTokenClaims = _token.GetClaims(HttpContext.User);
            var id = userTokenClaims.UserId;
            
            var result = await _service.HardDeleteUser(id);
            if(!result)
                throw new UserNotFound(id.ToString());
            return Ok(new DeleteUserResponseModel(id, true));
        }

    }
}
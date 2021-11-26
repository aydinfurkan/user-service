using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Controllers.ViewModels.RequestModels;
using UserService.Controllers.ViewModels.ResponseModels;
using UserService.Exceptions;
using UserService.Helpers.Authorize;
using UserService.Helpers.Extensions;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("/user")]
    public class AuthorizeController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        private readonly IToken _token;

        public AuthorizeController(ILogger<UserController> logger, IUserService service, IToken token)
        {
            _logger = logger;
            _service = service;
            _token = token;
        }
        
        /// <summary>
        /// Check if the id is invalid
        /// </summary>
        /// <returns>Whether the given id is valid or not.</returns>
        /// <response code="200">Returns user token.</response>
        /// <response code="404">Id is not valid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("authorize")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AuthorizeUserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authorize([FromQuery] string googleIdToken)
        {
            var googleUser =  await GoogleJsonWebSignature.ValidateAsync(googleIdToken); // Burdan 500 donuyo her turlu
            
            var user = await _service.GetUserByEmail(googleUser.Email);
            if (user == null)
                throw new UserNotFound(googleUser.Email);
            
            var token = _token.CreateToken(user);
            Response.AddCookie("p_token", token);
            return Ok(new AuthorizeUserResponseModel(token, true));
        }
        
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="createUserRequestModel">Google Id Token to be created</param>
        /// <returns>The created user's id.</returns>
        /// <response code="201">Returns user token.</response>
        /// <response code="400">The model is not valid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CreateUserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestModel createUserRequestModel)
        {
            var googleUser =  await GoogleJsonWebSignature.ValidateAsync(createUserRequestModel.GoogleIdToken);
            
            var newUser = googleUser.ToModel();
            var userId = await _service.CreateUser(newUser);
            
            var token = _token.CreateToken(newUser);
            Response.AddCookie(PTokenHelper.PTokenKey, token);
            
            var createdAtPath = $"/";
            return Created(createdAtPath, new CreateUserResponseModel(token, true));
        }
    }
}
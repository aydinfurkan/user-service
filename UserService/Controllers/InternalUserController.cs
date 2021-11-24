using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserService.Controllers.ViewModels.ResponseModels;
using UserService.Exceptions;
using UserService.Helpers.Authorize;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/user/internal")]
    public class InternalUserController : ControllerBase
    {
        private readonly ILogger<InternalUserController> _logger;
        private readonly IUserService _service;
        private readonly IToken _token;

        public InternalUserController(ILogger<InternalUserController> logger, IUserService service, IToken token)
        {
            _logger = logger;
            _service = service;
            _token = token;
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
            var pTokenClaims = _token.GetClaims(HttpContext.User);

            var user = await _service.GetUserByEmail(pTokenClaims.Email);
            if (user == null)
                throw new UserNotFound(pTokenClaims.UserId.ToString());
            
            return Ok(new UserResponseModel(user));
        }
    }
}
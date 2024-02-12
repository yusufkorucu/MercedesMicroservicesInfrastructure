using IdentityService.Api.Core.Application.Services;
using IdentityService.Api.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Field

        private readonly IAuthService _authService;

        #endregion

        #region Ctor

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region Methods

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result =  _authService.Login(loginRequestDto);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var result = await _authService.Register(registerRequestDto);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        #endregion
    }
}

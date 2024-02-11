using IdentityService.Api.Core.Domain;
using IdentityService.Api.Core.Domain.Base;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Core.Application.Services
{
    public interface IAuthService
    {
        ResultModel<LoginResponseDto> Login([FromBody] LoginRequestDto loginRequestDto);
        ResultModel<bool> Register([FromBody] RegisterRequestDto registerRequestDto);
    }
}

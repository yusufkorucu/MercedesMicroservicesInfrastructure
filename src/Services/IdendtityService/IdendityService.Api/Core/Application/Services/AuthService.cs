using IdentityService.Api.Core.Application.Repository;
using IdentityService.Api.Core.Domain;
using IdentityService.Api.Core.Domain.Base;
using IdentityService.Api.Infrastructure.Data;
using IdentityService.Api.Infrastructure.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Core.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ResultModel<LoginResponseDto> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }

        public ResultModel<bool> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var isExist = _userRepository.IsExist(registerRequestDto.UserName);
            if (!isExist.IsSuccess)
                return new ResultModel<bool> { IsSuccess = isExist.IsSuccess, ErrorText = isExist.ErrorText };

            registerRequestDto.Password = registerRequestDto.Password.ToSHA256Hash();


            //automapper
            var user = new User
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                Password = registerRequestDto.Password
            };

            _userRepository.AddAsync(user);

            return new ResultModel<bool> { IsSuccess = true, Result = true };
        }
    }
}

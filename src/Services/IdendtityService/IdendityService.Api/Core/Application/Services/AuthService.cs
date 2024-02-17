using IdentityService.Api.Core.Application.Repository;
using IdentityService.Api.Core.Domain;
using IdentityService.Api.Core.Domain.Base;
using IdentityService.Api.Infrastructure.Data;
using IdentityService.Api.Infrastructure.Extentions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Shared.Events.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Api.Core.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _publishEndpoint = publishEndpoint;
        }

        public ResultModel<LoginResponseDto> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = _userRepository.GetUserByUsernamePassword(loginRequestDto);

            if (!user.IsSuccess)
                return new ResultModel<LoginResponseDto>() { IsSuccess = user.IsSuccess, ErrorText = user.ErrorText };


            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, loginRequestDto.UserName),
                new Claim(ClaimTypes.Name, user.Result.FirstName+user.Result.LastName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["SecretKey"])); ;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(2);

            var token = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: creds, notBefore: DateTime.Now);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new LoginResponseDto()
            {
                Token = encodedJwt,
                UserName = loginRequestDto.UserName
            };

            return new ResultModel<LoginResponseDto>() { Result = response, IsSuccess = true };
        }

        public async Task<ResultModel<bool>> Register([FromBody] RegisterRequestDto registerRequestDto)
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

            await _userRepository.AddAsync(user);
            await _publishEndpoint.Publish(new UserCreatedEvent
            {
                Email = user.Email
            });
            return new ResultModel<bool> { IsSuccess = true, Result = true };
        }
    }
}

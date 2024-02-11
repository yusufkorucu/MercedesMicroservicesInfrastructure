using IdentityService.Api.Core.Application.Repository.Base;
using IdentityService.Api.Core.Domain;
using IdentityService.Api.Core.Domain.Base;
using IdentityService.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Core.Application.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        ResultModel<UserInfoDto> GetUserByUsernamePassword([FromBody] LoginRequestDto dto);
        ResultModel<bool> IsExist(string username);
    }
}

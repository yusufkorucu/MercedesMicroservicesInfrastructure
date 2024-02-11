using IdentityService.Api.Core.Application.Repository.Base;
using IdentityService.Api.Core.Domain;
using IdentityService.Api.Core.Domain.Base;
using IdentityService.Api.Infrastructure.Constants;
using IdentityService.Api.Infrastructure.Context;
using IdentityService.Api.Infrastructure.Data;
using IdentityService.Api.Infrastructure.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Core.Application.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IdentityApiDbContext context) : base(context)
        {
        }

        public ResultModel<UserInfoDto> GetUserByUsernamePassword([FromBody] LoginRequestDto dto)
        {
            var user = GetBy(x => x.UserName == dto.UserName && x.Password == dto.Password.ToSHA256Hash() && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();

            if (user != null)
            {
                var result = new UserInfoDto
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                };

                return new ResultModel<UserInfoDto> { Result = result, IsSuccess = true };

            }

            return new ResultModel<UserInfoDto> { ErrorText = UserCoreMessage.LoginError, IsSuccess = false };
        }

        public ResultModel<bool> IsExist(string username)
        {
            var user = GetBy(x => x.UserName == username && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();

            if (user != null)
                return new ResultModel<bool> { ErrorText = UserCoreMessage.UserExist, IsSuccess = false };

            return new ResultModel<bool> { IsSuccess = true };
        }
    }
}

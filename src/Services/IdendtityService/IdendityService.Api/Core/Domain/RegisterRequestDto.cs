﻿namespace IdentityService.Api.Core.Domain
{
    public class RegisterRequestDto
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

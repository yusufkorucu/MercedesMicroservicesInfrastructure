using System.Text.Json.Serialization;

namespace IdentityService.Api.Infrastructure.Data
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

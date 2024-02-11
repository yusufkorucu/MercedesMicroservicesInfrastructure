using System.ComponentModel;
using System.Text.Json.Serialization;

namespace IdentityService.Api.Infrastructure.Data
{
    public class Role:BaseEntity
    {
        public Guid ContentId { get; set; }
        [DisplayName("Adı")]
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [JsonIgnore]
        public virtual ICollection<PageRole> PageRoles { get; set; }
    }
}

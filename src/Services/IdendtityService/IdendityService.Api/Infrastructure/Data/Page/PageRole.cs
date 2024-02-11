using System.ComponentModel;

namespace IdentityService.Api.Infrastructure.Data
{
    public class PageRole:BaseEntity
    {
        [DisplayName("Sayfa Adı")]
        public Guid PageId { get; set; }
        public Guid ContentId { get; set; }
        [DisplayName("Okuma")]
        public bool Read { get; set; }
        [DisplayName("Yazma")]
        public bool Write { get; set; }
        [DisplayName("Silme")]
        public bool Delete { get; set; }
        [DisplayName("Rol Adı")]
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Page Page { get; set; }
    }
}

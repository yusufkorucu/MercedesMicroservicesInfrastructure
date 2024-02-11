namespace IdentityService.Api.Infrastructure.Data
{
    public class UserRole:BaseEntity
    {
        public Guid StaffId { get; set; }
        public Guid RoleId { get; set; }    
        public Guid ContentId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}

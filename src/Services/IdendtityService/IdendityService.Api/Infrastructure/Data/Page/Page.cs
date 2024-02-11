namespace IdentityService.Api.Infrastructure.Data
{
    public class Page:BaseEntity
    {
        public string Name { get; set; }
        public Guid? ParentPageId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string MenuIcon { get; set; }
        public int DisplayOrder { get; set; }
        public int PageLevel { get; set; }
        public bool IsHidden { get; set; }
        public string RouteValue { get; set; }
        public bool IsPage { get; set; }
        public virtual Page ParentPage { get; set; }
    }
}

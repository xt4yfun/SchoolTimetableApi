namespace Core.Entities.Concrete
{
    public class Role : IEntity
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public ICollection<PermissionRole> PermissionRoles { get; set; }
    }
}

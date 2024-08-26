using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Permission : IEntity
    {
        public int ID { get; set; }
        public string PermissionName { get; set; }
        public ICollection<PermissionRole> PermissionRoles { get; set; }
    }
}

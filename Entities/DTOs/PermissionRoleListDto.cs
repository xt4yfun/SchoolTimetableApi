using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PermissionRoleListDto : IDto
    {
        public int ID { get; set; }
        public int roleId { get; set; }
        public int permissonId { get; set; }
    }
}

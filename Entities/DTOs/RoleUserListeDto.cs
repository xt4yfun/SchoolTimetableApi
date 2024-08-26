using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RoleUserListeDto:IDto
    {
        public string RoleName { get; set; }
        public string UserName { get; set; }
    }
}

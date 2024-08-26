using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PermissionRoleDto:IDto
    {
        public int ID;
        public int RoleID;
        public int PermissonID;
    }
}

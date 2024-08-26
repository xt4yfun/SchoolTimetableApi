using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPermissionRoleDal: IEntityRepository<PermissionRole>
   {
        List<PermissionRoleListDto> GetAllList();
        PermissionRoleListDto GetID(int ID);
        List<PermissionRoleListDto> GetRole(int roleID);
        List<PermissionRoleListDto> GetPerm(int permID);
    }
}

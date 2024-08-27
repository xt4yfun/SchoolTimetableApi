using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRoleUserDal: IEntityRepository<UserRole>
    {
        List<RoleUserListeDto> GetAllList();
        RoleUserListeDto GetID(int rolId,int userId);
        List<RoleUserListeDto> GetRole(int roleID);
        List<RoleUserListeDto> GetUser(int userID);
    }
}

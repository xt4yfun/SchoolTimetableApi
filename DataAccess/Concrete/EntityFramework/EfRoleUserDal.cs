using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRoleUserDal : EfEntityRepositoryBase<UserRole, DataContext>, IRoleUserDal
    {
        public List<RoleUserListeDto> GetAllList()
        {
            using (var vt = new DataContext())
            {
                var result = from ru in vt.UserRole
                             join r in vt.Role on
                             ru.RoleId equals r.ID
                             join u in vt.User on
                             ru.UserId equals u.ID
                             select new RoleUserListeDto
                             {
                                 RoleName = r.RoleName,
                                 UserName = u.LastName + " " + u.FirstName
                             };
                return result.ToList();
            }
        }

        public RoleUserListeDto GetID(int ID)
        {
            using (var vt = new DataContext())
            {
                var result = from ru in vt.UserRole
                             join r in vt.Role on
                             ru.RoleId equals r.ID
                             join u in vt.User on
                             ru.UserId equals u.ID
                             where ru.ID == ID
                             select new RoleUserListeDto
                             {
                                 RoleName = r.RoleName,
                                 UserName = u.LastName + " " + u.FirstName
                             };
                return result.FirstOrDefault();
            }
        }
    }
}

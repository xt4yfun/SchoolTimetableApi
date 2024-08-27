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
    public class EfKullaniciDal : EfEntityRepositoryBase<User, DataContext>, IKullaniciDal
    {
        public List<UserListDto> GetList()
        {
            using (var vt = new DataContext())
            {
                var result = from u in vt.User
                             join ur in vt.UserRole
                             on u.ID equals ur.UserId
                             join r in vt.Role
                             on ur.RoleId equals r.ID
                             group new { u, r } by new { u.ID, u.FirstName, u.LastName, u.Email } into g
                             select new UserListDto
                             {
                                 ID = g.Key.ID,
                                 FirstName = g.Key.FirstName ,
                                 LastName=g.Key.LastName,
                                 EMail = g.Key.Email,
                                 RolName = string.Join(", ", g.Select(x => x.r.RoleName))
                             };
                return result.ToList();
            }
        }

        public List<Role> GetClaims(User user)
        {
            using (var context = new DataContext())
            {
                var result = from role in context.Role
                             join KullaniciRol in context.UserRole
                                 on role.ID equals KullaniciRol.RoleId
                             where KullaniciRol.UserId == user.ID
                             select new Role { ID = role.ID, RoleName = role.RoleName};
                return result.ToList();

            }
        }
    }
}

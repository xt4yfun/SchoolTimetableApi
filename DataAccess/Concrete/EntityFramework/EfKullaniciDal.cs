using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfKullaniciDal : EfEntityRepositoryBase<User, DataContext>, IKullaniciDal
    {
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

using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class PermissionDal : IPermissionDal
    {
        public bool HasPermission(List<string> roles, string permissionName)
        {
            bool rolBool=false;
            using (var context = new DataContext())
            {
                foreach (var rol in roles)
                {
                    var role = context.Role.Where(x=>x.RoleName==rol).FirstOrDefault();
                    var perm= context.Permission.Where(x=>x.PermissionName==permissionName).FirstOrDefault();
                    rolBool = context.PermissionRol.Any(pr=>pr.RoleId==role.ID && pr.PermissionId==perm.ID);
                    if (rolBool)
                    {
                        break;
                    }
                }
            }
            return rolBool;
        }
    }
}

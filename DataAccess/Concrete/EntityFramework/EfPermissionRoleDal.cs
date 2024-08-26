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
    public class EfPermissionRoleDal : EfEntityRepositoryBase<PermissionRole, DataContext>, IPermissionRoleDal
    {
        public List<PermissionRoleListDto> GetAllList()
        {
            using (DataContext vt = new DataContext())
            {
                var result = from pr in vt.PermissionRol
                             join p in vt.Permission on
                             pr.PermissionId equals p.ID
                             join r in vt.Role on pr.RoleId
                             equals r.ID
                             select new PermissionRoleListDto
                             {
                                 ID = pr.ID,
                                 permissonName = p.PermissionName,
                                 roleName = r.RoleName,
                             };
                return result.ToList();
            }
        }

        public PermissionRoleListDto GetID(int ID)
        {
            using (DataContext vt = new DataContext())
            {
                var result = from pr in vt.PermissionRol
                             join p in vt.Permission on
                             pr.PermissionId equals p.ID
                             join r in vt.Role on pr.RoleId
                             equals r.ID where pr.ID==ID
                             select new PermissionRoleListDto
                             {
                                 ID = pr.ID,
                                 permissonName = p.PermissionName,
                                 roleName = r.RoleName,
                             };
                return result.FirstOrDefault();
            }
        }

        public List<PermissionRoleListDto> GetPerm(int permID)
        {
            using (DataContext vt = new DataContext())
            {
                var result = from pr in vt.PermissionRol
                             join p in vt.Permission on
                             pr.PermissionId equals p.ID
                             join r in vt.Role on pr.RoleId
                             equals r.ID
                             where pr.PermissionId == permID
                             select new PermissionRoleListDto
                             {
                                 ID = pr.ID,
                                 permissonName = p.PermissionName,
                                 roleName = r.RoleName,
                             };
                return result.ToList();
            }
        }

        public List<PermissionRoleListDto> GetRole(int roleID)
        {
            using (DataContext vt = new DataContext())
            {
                var result = from pr in vt.PermissionRol
                             join p in vt.Permission on
                             pr.PermissionId equals p.ID
                             join r in vt.Role on pr.RoleId
                             equals r.ID
                             where pr.RoleId == roleID
                             select new PermissionRoleListDto
                             {
                                 ID = pr.ID,
                                 permissonName = p.PermissionName,
                                 roleName = r.RoleName,
                             };
                return result.ToList();
            }
        }
    }
}

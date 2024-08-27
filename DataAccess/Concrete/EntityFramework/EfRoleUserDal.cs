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
            using (DataContext vt = new DataContext())
            {
                var result = from ru in vt.UserRole
                             join r in vt.Role on
                             ru.RoleId equals r.ID
                             join u in vt.User on
                             ru.UserId equals u.ID
                             select new RoleUserListeDto
                             {
                                 ID = ru.ID,
                                 roleID = r.ID,
                                 userID = u.ID
                             };
                return result.ToList();
            }
        }

        public RoleUserListeDto GetID(int rolId, int userId)
        {
            using (DataContext vt = new DataContext())
            {
                var result = from ru in vt.UserRole
                             join r in vt.Role on
                             ru.RoleId equals r.ID
                             join u in vt.User on
                             ru.UserId equals u.ID
                             where ru.RoleId == rolId
                             where ru.UserId == userId
                             select new RoleUserListeDto
                             {
                                 ID = ru.ID,
                                 roleID = r.ID,
                                 userID = u.ID
                             };
                return result.FirstOrDefault();
            }
        }

        public List<RoleUserListeDto> GetRole(int roleID)
        {
            using (DataContext vt = new DataContext())
            {
                var result = from ru in vt.UserRole
                             join r in vt.Role on
                             ru.RoleId equals r.ID
                             join u in vt.User on
                             ru.UserId equals u.ID
                             where ru.RoleId == roleID
                             select new RoleUserListeDto
                             {
                                 ID = ru.ID,
                                 roleID = r.ID,
                                 userID = u.ID
                             };
                return result.ToList();
            }
        }

        public List<RoleUserListeDto> GetUser(int userID)
        {
            using (DataContext vt = new DataContext())
            {
                var result = from ru in vt.UserRole
                             join r in vt.Role on
                             ru.RoleId equals r.ID
                             join u in vt.User on
                             ru.UserId equals u.ID
                             where ru.UserId == userID
                             select new RoleUserListeDto
                             {
                                 ID = ru.ID,
                                 roleID = r.ID,
                                 userID = u.ID
                             };
                return result.ToList();
            }
        }
    }
}

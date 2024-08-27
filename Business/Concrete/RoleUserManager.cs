using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Contents;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleUserManager : IRoleUserService
    {
        IRoleUserDal _roleUser;

        public RoleUserManager(IRoleUserDal roleUser)
        {
            _roleUser = roleUser;
        }
        [SecuredOperation("roleUserAdd")]
        public IResult Add(RoleUserDto roleUserDto)
        {
            var result = new UserRole
            {
                RoleId = roleUserDto.roleID,
                UserId = roleUserDto.userID,
            };
            _roleUser.Add(result);
            return new SuccessResult(Messages.roleUserAdd);
        }
        [SecuredOperation("roleUserDelete")]
        public IResult Delete(int ID)
        {
            var result=_roleUser.Get(x=>x.ID==ID);
            if (result==null)
            {
                return new ErrorResult(Messages.roleUserNotDelete);
            }
            _roleUser.Delete(result);
            return new SuccessResult(Messages.roleUserDelete);
        }
        [SecuredOperation("roleUserGet")]
        public IResult Get(int rolId, int userId)
        {
            var result = _roleUser.GetID(rolId, userId);
            return new SuccessDataResult<RoleUserListeDto>(result, "Rol kullanıcı bilgisi");
        }
        [SecuredOperation("roleUserGet")]
        public IResult GetAll()
        {
            var result = _roleUser.GetAllList();
            return new SuccessDataResult<List<RoleUserListeDto>>(result, "Rol kullanıcı listesi");
        }
        [SecuredOperation("roleUserGet")]
        public IResult GetRole(int roleID)
        {
            return new SuccessDataResult<List<RoleUserListeDto>>(_roleUser.GetRole(roleID));
        }
        [SecuredOperation("roleUserGet")]
        public IResult GetUser(int userID)
        {
            return new SuccessDataResult<List<RoleUserListeDto>>(_roleUser.GetUser(userID));
        }
    }
}

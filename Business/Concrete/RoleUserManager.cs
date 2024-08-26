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
            return new SuccessResult(Messages.CoursesAdded);
        }
        [SecuredOperation("roleUserDelete")]
        public IResult Delete(int ID)
        {
            var result=_roleUser.Get(x=>x.ID==ID);
            if (result==null)
            {
                return new ErrorResult(Messages.CoursesAdded);
            }
            _roleUser.Delete(result);
            return new SuccessResult(Messages.CoursesAdded);
        }
        [SecuredOperation("roleUserGet")]
        public IResult Get(int ID)
        {
            var result = _roleUser.GetID(ID);
            return new SuccessDataResult<RoleUserListeDto>(result, "Rol kullanıcı bilgisi");
        }
        [SecuredOperation("roleUserGet")]
        public IResult GetAll()
        {
            var result = _roleUser.GetAllList();
            return new SuccessDataResult<List<RoleUserListeDto>>(result, "Rol kullanıcı listesi");
        }
    }
}

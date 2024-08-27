using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Contents;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IKullaniciDal _userDal;

        public UserManager(IKullaniciDal userDal)
        {
            _userDal = userDal;
        }

        public List<Role> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
        [SecuredOperation("userList")]
        public IDataResult<List<UserListDto>> GetList()
        {
            return new SuccessDataResult<List<UserListDto>>(_userDal.GetList());
        }
        [SecuredOperation("userDelete")]
        public IResult Delete(int id)
        {
            var result = _userDal.Get(x => x.ID == id);
            if (result == null)
            {
                return new ErrorResult(Messages.UserNotDeleted);
            }
            _userDal.Delete(result);
            return new SuccessResult(Messages.UserDeleted);
        }
        [SecuredOperation("userUpdate")]
        public IResult Update(UserUpdateDto userDto)
        {
            var result = _userDal.Get(x => x.ID == userDto.ID);

            if (result == null)
            {
                return new ErrorResult("User not found.");
            }

            var select = new User
            {
                ID = result.ID,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PasswordHash = result.PasswordHash,
                PasswordSalt = result.PasswordSalt,
            };

            _userDal.Update(select);
            return new SuccessResult(Messages.userUpdate);
        }
    }
}

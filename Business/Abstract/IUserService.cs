using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<Role> GetClaims(User user);
        IDataResult<List<UserListDto>> GetList();
        IResult Delete(int id);
        IResult Update(UserUpdateDto userDto);
        void Add(User user);
        User GetByMail(string email);
    }
}

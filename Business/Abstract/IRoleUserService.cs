using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoleUserService
    {
        IResult Add(RoleUserDto roleUserDto);
        IResult Delete(int ID);
        IResult GetAll();
        IResult GetUser(int userID);
        IResult GetRole(int roleID);
        IResult Get(int ID);
    }
}

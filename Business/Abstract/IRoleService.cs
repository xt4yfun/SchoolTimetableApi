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
    public interface IRoleService
    {
        IResult Add(RoleDto roleDto);
        IDataResult<List<RoleDto>> GetAll();
        IResult Delete(int ID);
        public IDataResult<Role> GetNameId(string rolName);
    }
}

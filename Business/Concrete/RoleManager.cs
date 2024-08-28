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
    public class RoleManager : IRoleService
    {
        IRoleDal _role;

        public RoleManager(IRoleDal role)
        {
            _role = role;
        }
        [SecuredOperation("roleAdd")]
        public IResult Add(RoleDto roleDto)
        {
            var result = new Role
            {
                RoleName=roleDto.RolName
            };
            _role.Add(result);
            return new SuccessResult(Messages.RoleAdd);
        }
        [SecuredOperation("roleDelete")]
        public IResult Delete(int ID)
        {
            var result = _role.Get(x => x.ID == ID);
            _role.Delete(result);
            return new SuccessResult(Messages.RoleDelete);
        }
        [SecuredOperation("roleGet")]
        public IDataResult<List<RoleDto>> GetAll()
        {
            var result=_role.GetAll();
            var s = from ss in _role.GetAll()
                    select new RoleDto
                    {
                        ID = ss.ID,
                        RolName = ss.RoleName
                    };
            return new SuccessDataResult<List<RoleDto>>(s.ToList());
        }

        public IDataResult<Role> GetNameId(string rolName)
        {
            return new SuccessDataResult<Role>(_role.Get(x => x.RoleName == rolName));
        }
    }
}

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
    public class PermissionRoleManager : IPermissionRoleService
    {
        IPermissionRoleDal _permRoleDal;

        public PermissionRoleManager(IPermissionRoleDal permRoleDal)
        {
            _permRoleDal = permRoleDal;
        }
        [SecuredOperation("permissionRolAdd")]
        public IResult Add(PermissionRoleDto permissionRole)
        {
            var result = new PermissionRole
            {
                PermissionId = permissionRole.PermissonID,
                RoleId= permissionRole.RoleID
            };
            _permRoleDal.Add(result);
            return new SuccessResult(Messages.permRolAdd);
        }
        [SecuredOperation("permissionRolDelete")]
        public IResult Delete(int id)
        {
            var result = _permRoleDal.Get(x=>x.ID== id);
            _permRoleDal.Delete(result);
            return new SuccessResult(Messages.permRolDelete);
        }
        [SecuredOperation("permissionRolGet")]
        public IDataResult<List<PermissionRoleListDto>> GetAll()
        {
            return new SuccessDataResult<List<PermissionRoleListDto>>(_permRoleDal.GetAllList());
        }
        [SecuredOperation("permissionRolGet")]
        public IDataResult<PermissionRoleListDto> GetByID(int rolID,int permId)
        {
            return new SuccessDataResult<PermissionRoleListDto>(_permRoleDal.GetID(rolID,permId));
        }
        [SecuredOperation("permissionRolGet")]
        public IDataResult<List<PermissionRoleListDto>> GetPerm(int permId)
        {
            return new SuccessDataResult<List<PermissionRoleListDto>>(_permRoleDal.GetPerm(permId));
        }
        [SecuredOperation("permissionRolGet")]
        public IDataResult<List<PermissionRoleListDto>> GetRole(int roleId)
        {
            return new SuccessDataResult<List<PermissionRoleListDto>>(_permRoleDal.GetRole(roleId));
        }
    }
}

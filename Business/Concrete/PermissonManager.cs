using Business.Abstract;
using Business.BusinessAspects.Autofac;
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
    public class PermissonManager : IPermissionService
    {
        IPermissionDal _permisson;

        public PermissonManager(IPermissionDal permisson)
        {
            _permisson = permisson;
        }
        [SecuredOperation("permissionGet")]
        public IDataResult<List<PermissonListDto>> GetAllList()
        {
            var result=from p in _permisson.GetAll()
                       select new PermissonListDto
                       {
                           ID = p.ID,
                           Name=p.PermissionName
                       };
            return new SuccessDataResult<List<PermissonListDto>>(result.ToList());
        }
    }
}

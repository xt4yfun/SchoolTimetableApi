﻿using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPermissionRoleService
    {
        IResult Add(PermissionRoleDto permissionRole);
        IResult Delete(int id);
        IDataResult<PermissionRoleListDto> GetByID(int ID);
        IDataResult<List<PermissionRoleListDto>> GetAll();
    }
}
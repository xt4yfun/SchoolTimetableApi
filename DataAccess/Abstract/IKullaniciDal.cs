using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IKullaniciDal : IEntityRepository<User>
    {
        List<Role> GetClaims(User kullanici);
        List<UserListDto> GetList();
    }
}

using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Contents;
using DataAccess.Abstract;
using System.Security.Claims;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using System.Data;

namespace Business.BusinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string _perm;
        private IHttpContextAccessor _httpContextAccessor;
        public SecuredOperation(string perm)
        {
            _perm = perm;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

            bool rolBool = false;
            using (var context = new DataContext())
            {
                foreach (var rol in roleClaims)
                {
                    var role = context.Role.Where(x => x.RoleName == rol).FirstOrDefault();
                    var perm = context.Permission.Where(x => x.PermissionName == _perm).FirstOrDefault();
                    rolBool = context.PermissionRol.Any(pr => pr.RoleId == role.ID && pr.PermissionId == perm.ID);
                    if (rolBool)
                    {
                        break;
                    }
                }
            }

            if (!rolBool)
            {
                throw new Exception(Messages.AuthorizationDenied);
            }

            return;
        }
    }
}

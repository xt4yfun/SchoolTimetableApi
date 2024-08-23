using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessAspects.Autofac
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException() : base("Yetkiniz yok")
        {
        }
    }
}

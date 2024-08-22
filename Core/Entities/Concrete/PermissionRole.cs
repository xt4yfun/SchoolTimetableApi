using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class PermissionRole:IEntity
    {
        public int ID { get; set; }
        public int RolID { get; set; }
        public int PermissionID { get; set; }
    }
}

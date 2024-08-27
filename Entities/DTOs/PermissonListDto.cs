using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class PermissonListDto:IDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}

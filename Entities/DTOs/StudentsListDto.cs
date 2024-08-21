using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class StudentsListDto:IDto
    {
        public int ID {  get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ClassName { get; set; }
    }
}

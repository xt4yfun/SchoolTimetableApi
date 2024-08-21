using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ClassCourseDto:IDto
    {
        public int ClassID { get; set; }
        public int CourseID { get; set; }
    }
}

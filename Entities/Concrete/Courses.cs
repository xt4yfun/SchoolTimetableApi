using Core.Entities;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Courses:BaseEntity,IEntity 
    {
        public string CoursesName { get; set; }
        public int Credit { get; set; }
        public ICollection<ClassCourse> ClassCourses { get; set; }
    }
}

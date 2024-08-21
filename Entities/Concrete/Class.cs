using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Class:BaseEntity,IEntity
    {
        public string ClassName { get; set; }
        public ICollection<Students> Students { get; set; }
        public ICollection<ClassCourse> ClassCourses { get; set; }
    }
}
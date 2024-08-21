using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class ClassCourse:BaseEntity,IEntity
    {
        public int ClassID { get; set; }
        public Class Class { get; set; }
        public int CourseID { get; set; }
        public Courses Course { get; set; }
        public ICollection<Timetable> Timetables { get; set; }
    }
}
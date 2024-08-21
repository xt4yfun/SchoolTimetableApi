using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete
{
    public class Timetable : BaseEntity, IEntity
    {
        public int ClassCourseID { get; set; }
        public ClassCourse ClassCourse { get; set; }
        public int AcademicsID { get; set; }
        public Academics Academics { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
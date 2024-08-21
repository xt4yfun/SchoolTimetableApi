using Core.Entities;
using Entities.Abstract;
using System.Xml;

namespace Entities.Concrete
{
    public class Academics : BaseEntity, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public ICollection<Timetable> Timetables { get; set; }
    }
}
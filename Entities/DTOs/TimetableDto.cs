using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class TimetableDto:IDto
    {
        public int ID { get; set; }
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string ClassName { get; set; }
        public string CoursesName { get; set; }
        public string AcademicsFullName { get; set; }
    }
}

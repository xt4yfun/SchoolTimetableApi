using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class DashboardDto:IDto
    {
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public int TotalClass { get; set; }
        public int TotalAcademic { get; set; }
        public int Timetable { get; set; }
        public int TotalCredi { get; set; }
        public int DailyHours { get; set; }
        public int WeeklyDays { get; set; }
        public bool LunchBreak { get; set; }
        public int LessonDuration { get; set; }
        public int BreakDuration { get; set; }
        public int LunchBreakDuration { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}

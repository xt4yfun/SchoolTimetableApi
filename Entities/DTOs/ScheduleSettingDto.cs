using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ScheduleSettingDto:IDto
    {
        public int DailyHours { get; set; }
        public int WeeklyDays { get; set; }
        public bool LunchBreak { get; set; }
        public int LessonDuration { get; set; }
        public int BreakDuration { get; set; }
        public int LunchBreakDuration { get; set; }
        public TimeSpan StartTime { get; set; }
    }
}

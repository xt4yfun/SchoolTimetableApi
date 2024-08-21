using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Migrations;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStudentsDal : EfEntityRepositoryBase<Students, DataContext>, IStudentsDal
    {
        public List<StudentsListDto> GetAllList()
        {
            using (DataContext vt = new DataContext())
            {
                var result = from s in vt.Students
                             join c in vt.Classes
                             on s.ClassID equals c.ID
                             where s.Status != DataStatus.Deleted
                             select new StudentsListDto
                             {
                                 ID=s.ID,
                                 FullName=s.FirstName+" "+s.LastName,
                                 Email=s.Email,
                                 ClassName=c.ClassName
                             };
                return result.ToList();
            }
        }

        public List<StudentsListDto> GetClassList(int classID)
        {
            using (DataContext vt = new DataContext())
            {
                var result = from s in vt.Students
                             join c in vt.Classes
                             on s.ClassID equals c.ID
                             where s.Status != DataStatus.Deleted
                             where c.ID== classID
                             select new StudentsListDto
                             {
                                 FullName = s.FirstName + " " + s.LastName,
                                 Email = s.Email,
                                 ClassName = c.ClassName
                             };
                return result.ToList();
            }
        }

        public DashboardDto GetDashboard()
        {
            using(DataContext vt = new DataContext())
            {
                var student = from s in vt.Students where s.Status!=DataStatus.Deleted select s.ID;
                var courses = from co in vt.Courses where co.Status != DataStatus.Deleted select co.ID;
                var classs = from c in vt.Classes where c.Status != DataStatus.Deleted select c.ID;
                var academics = from a in vt.Academics where a.Status != DataStatus.Deleted select a.ID;
                var timetable = from t in vt.Timetables where t.Status != DataStatus.Deleted select t.ID;
                var ssetting = from ss in vt.ScheduleSettings where ss.Status!=DataStatus.Deleted select new
                {
                    ss.DailyHours,
                    ss.WeeklyDays,
                    ss.LunchBreak,
                    ss.LessonDuration,
                    ss.BreakDuration,
                    ss.LunchBreakDuration,
                    ss.StartTime
                };

                var result = from s in ssetting
                             select new DashboardDto
                             {
                                 TotalStudents=student.Count(),
                                 TotalCourses=courses.Count(),
                                 TotalClass=classs.Count(),
                                 TotalAcademic=academics.Count(),
                                 Timetable=timetable.Count(),
                                 TotalCredi=s.WeeklyDays*s.DailyHours,
                                 DailyHours=s.DailyHours,
                                 WeeklyDays=s.WeeklyDays,
                                 LunchBreak=s.LunchBreak,
                                 LessonDuration=s.LessonDuration,
                                 BreakDuration= s.BreakDuration,
                                 LunchBreakDuration= s.LunchBreakDuration,
                                 StartTime= s.StartTime
                             };


                return result.FirstOrDefault();
            }
        }
    }
}

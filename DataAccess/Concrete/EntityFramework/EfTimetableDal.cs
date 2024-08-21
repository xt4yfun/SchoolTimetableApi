using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTimetableDal : EfEntityRepositoryBase<Timetable, DataContext>, ITimetableDal
    {
        public List<TimetableDto> GetAllList()
        {
            using (DataContext vt = new DataContext())
            {
                var result = from t in vt.Timetables
                             join cc in vt.ClassCourses on t.ClassCourseID equals cc.ID
                             join a in vt.Academics on t.AcademicsID equals a.ID
                             join c in vt.Classes on cc.ClassID equals c.ID
                             join co in vt.Courses on cc.CourseID equals co.ID
                             where t.Status != DataStatus.Deleted
                             select new TimetableDto
                             {
                                 ID = t.ID,
                                 Day = t.Day,
                                 StartTime = t.StartTime,
                                 EndTime = t.EndTime,
                                 ClassName = c.ClassName,
                                 CoursesName = co.CoursesName,
                                 AcademicsFullName = a.FirstName + " " + a.LastName
                             };
                return result.ToList();
            }
        }

        public List<TimetableDto> GetClassList(int classID)
        {
            using (DataContext vt = new DataContext())
            {
                var result = from t in vt.Timetables
                             join cc in vt.ClassCourses on t.ClassCourseID equals cc.ID
                             join a in vt.Academics on t.AcademicsID equals a.ID
                             join c in vt.Classes on cc.ClassID equals c.ID
                             join co in vt.Courses on cc.CourseID equals co.ID
                             where c.ID == classID
                             where t.Status != DataStatus.Deleted
                             select new TimetableDto
                             {
                                 ID = t.ID,
                                 Day = t.Day,
                                 StartTime = t.StartTime,
                                 EndTime = t.EndTime,
                                 ClassName = c.ClassName,
                                 CoursesName = co.CoursesName,
                                 AcademicsFullName = a.FirstName + " " + a.LastName
                             };
                return result.ToList();
            }
        }

        public List<TimetableDto> GetDayList(string day)
        {
            using (DataContext vt = new DataContext())
            {
                var result = from t in vt.Timetables
                             join cc in vt.ClassCourses on t.ClassCourseID equals cc.ID
                             join a in vt.Academics on t.AcademicsID equals a.ID
                             join c in vt.Classes on cc.ClassID equals c.ID
                             join co in vt.Courses on cc.CourseID equals co.ID
                             where t.Day == day
                             where t.Status != DataStatus.Deleted
                             // Grupla ve her gruptan ilk öğeyi al
                             group new { t, c, co, a } by new { t.StartTime, t.EndTime } into g
                             select new TimetableDto
                             {
                                 ID = g.First().t.ID,
                                 Day = g.First().t.Day,
                                 StartTime = g.Key.StartTime,
                                 EndTime = g.Key.EndTime,
                                 ClassName = g.First().c.ClassName,
                                 CoursesName = g.First().co.CoursesName,
                                 AcademicsFullName = g.First().a.FirstName + " " + g.First().a.LastName
                             };

                // Sonuçları listeye çevir
                return result.ToList();

            }
        }
    }
}

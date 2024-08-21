using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
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
    public class EfClassCourseDal : EfEntityRepositoryBase<ClassCourse, DataContext>, IClassCourseDal
    {
        public List<CoursesClassListDto> GetAllList()
        {
            using (var vt = new DataContext())
            {
                var result=from cc in vt.ClassCourses 
                           join c in vt.Classes on cc.ClassID equals c.ID 
                           join co in vt.Courses on cc.CourseID equals co.ID
                           where cc.Status != DataStatus.Deleted
                           select new CoursesClassListDto
                           {
                               ID = cc.ID,
                               ClassName = c.ClassName,
                               CoursesName = co.CoursesName,
                               CoursesCredi = co.Credit,
                           };
                return result.ToList();
            }
        }
        public List<CoursesClassListDto> GetAllClass(int classID)
        {
            using (var vt = new DataContext())
            {
                var result=from cc in vt.ClassCourses 
                           join c in vt.Classes on cc.ClassID equals c.ID 
                           join co in vt.Courses on cc.CourseID equals co.ID
                           where cc.Status != DataStatus.Deleted
                           where cc.ClassID ==classID
                           select new CoursesClassListDto
                           {
                               ID = cc.ID,
                               ClassName = c.ClassName,
                               CoursesName = co.CoursesName,
                               CoursesCredi = co.Credit,
                           };
                return result.ToList();
            }
        }
    }
}

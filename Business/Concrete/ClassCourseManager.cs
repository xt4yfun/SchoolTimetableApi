using Business.Abstract;
using Business.Contents;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ClassCourseManager : IClassCourseService
    {
        IClassCourseDal _classCourseDal;
        ICoursesDal _coursesDal;
        IClassDal _classDal;

        public ClassCourseManager(IClassCourseDal classCourseDal, ICoursesDal coursesDal, IClassDal classDal)
        {
            _classCourseDal = classCourseDal;
            _coursesDal = coursesDal;
            _classDal = classDal;
        }

        public IResult Add(ClassCourseDto classCourse)
        {

            var result = new ClassCourse
            {
                ClassID = classCourse.ClassID,
                CourseID = classCourse.CourseID
            };
            if (_classCourseDal.GetAll(x=>x.ClassID==result.ClassID&&x.CourseID==result.CourseID && x.Status != DataStatus.Deleted).Any())
            {
                return new ErrorResult(Messages.ClassCourseFind);
            }
            _classCourseDal.Add(result);
            return new SuccessResult(Messages.ClassCourseAdded);
        }

        public IResult AllClassCourse()
        {
            var courses = _coursesDal.GetAll(x => x.Status != DataStatus.Deleted);
            var classs = _classDal.GetAll(x => x.Status != DataStatus.Deleted);

            foreach (var item in classs)
            {
                foreach (var ders in courses)
                {
                    var result = new ClassCourse
                    {
                        ClassID = item.ID,
                        CourseID = ders.ID
                    };
                    if (_classCourseDal.GetAll(x=>x.ClassID==result.ClassID&&x.CourseID==result.CourseID&&x.Status!=DataStatus.Deleted).Any())
                    {
                        continue;
                    }
                    _classCourseDal.Add(result);
                }
            }
            return new SuccessResult(Messages.AllClassCourse);
        }

        public IResult ClassAllAddCourse(int classID)
        {
            var courses = _coursesDal.GetAll(x => x.Status != DataStatus.Deleted);
            foreach (var ders in courses)
            {
                var result = new ClassCourse
                {
                    ClassID = classID,
                    CourseID = ders.ID
                };
                if (_classCourseDal.GetAll(x => x.ClassID == result.ClassID && x.CourseID == result.CourseID && x.Status!=DataStatus.Deleted).Any())
                {
                    continue;
                }
                _classCourseDal.Add(result);
            }
            return new SuccessResult(Messages.ClassAllCourseAdded);
        }

        public IResult Deleted(int classCourseID)
        {
            var result = _classCourseDal.Get(x => x.ID == classCourseID && x.Status != DataStatus.Deleted);
            var select = new ClassCourse
            {
                ID = result.ID,
                ClassID = result.ClassID,
                CourseID = result.CourseID,
                DeletedDate = DateTime.Now,
                Status = DataStatus.Deleted
            };
            _classCourseDal.Update(select);
            return new SuccessResult(Messages.ClassCourseDeleted);
        }

        public IDataResult<List<ClassCourse>> GetAll()
        {
            return new SuccessDataResult<List<ClassCourse>>(_classCourseDal.GetAll(x => x.Status != DataStatus.Deleted));
        }
        public IDataResult<List<CoursesClassListDto>> GetAllList()
        {
            return new SuccessDataResult<List<CoursesClassListDto>>(_classCourseDal.GetAllList());
        }
        public IDataResult<List<CoursesClassListDto>> GetAllClass(int classID)
        {
            return new SuccessDataResult<List<CoursesClassListDto>>(_classCourseDal.GetAllClass(classID));
        }
    }
}

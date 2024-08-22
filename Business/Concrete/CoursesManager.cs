using Business.Abstract;
using Business.BusinessAspects.Autofac;
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CoursesManager : ICoursesService
    {
        ICoursesDal _coursesDal;
        IClassCourseDal _classCourseDal;

        public CoursesManager(ICoursesDal coursesDal, IClassCourseDal classCourseDal)
        {
            _coursesDal = coursesDal;
            _classCourseDal = classCourseDal;
        }
        [SecuredOperation("coursesAdd")]
        public IResult Add(CoursesDto courses)
        {
            var result = new Courses
            {
                CoursesName = courses.CoursesName,
                Credit= courses.Credit
            };
            _coursesDal.Add(result);
            return new SuccessResult(Messages.CoursesAdded);
        }
        [SecuredOperation("coursesDelete")]
        public IResult Delete(int coursesID)
        {
            var result = _coursesDal.Get(x=>x.ID==coursesID&&x.Status!=DataStatus.Deleted);

            if (_classCourseDal.GetAll(x=>x.CourseID==result.ID && x.Status != DataStatus.Deleted).Any())
            {
                return new ErrorResult(Messages.CoursesClass);
            }

            var select = new Courses
            {
                ID=result.ID,
                CoursesName=result.CoursesName,
                Credit=result.Credit,
                DeletedDate=DateTime.Now,
                Status=DataStatus.Deleted
            };
            _coursesDal.Update(select);
            return new SuccessResult(Messages.CoursesDeleted);
        }
        [SecuredOperation("coursesUpdate")]
        public IResult Update(CoursesDto courses)
        {
            var result = _coursesDal.Get(x => x.ID == courses.ID && x.Status != DataStatus.Deleted);
            var select = new Courses
            {
                ID = result.ID,
                CoursesName = courses.CoursesName,
                Credit = courses.Credit,
                ModifiedDate = DateTime.Now,
                Status = DataStatus.Upserted
            };
            _coursesDal.Update(select);
            return new SuccessResult(Messages.CoursesDeleted);
        }
        [SecuredOperation("coursesGetAll")]
        public IDataResult<List<Courses>> GetAll()
        {
            return new SuccessDataResult<List<Courses>>(_coursesDal.GetAll(x => x.Status != DataStatus.Deleted));
        }
        [SecuredOperation("coursesGetByID")]
        public IDataResult<Courses> GetByID(int ID)
        {
            return new SuccessDataResult<Courses>(_coursesDal.GetAll(x => x.ID == ID && x.Status != DataStatus.Deleted).FirstOrDefault());
        }
    }
}

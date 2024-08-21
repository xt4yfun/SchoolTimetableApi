using Business.Abstract;
using Business.Contents;
using Core.Utilities.Business;
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
    public class ClassManager : IClassService
    {
        IStudentsDal _studentsDal;

        IClassDal _classDal;
        IClassCourseDal _classCourseDal;

        public ClassManager(IClassDal classDal, IStudentsDal studentsDal, IClassCourseDal classCourseDal)
        {
            _classDal = classDal;
            _studentsDal = studentsDal;
            _classCourseDal = classCourseDal;
        }

        public IResult Add(ClassDto nclass)
        {
            if (_classDal.GetAll(x=>x.ClassName.Equals(nclass.ClassName) && x.Status!=DataStatus.Deleted).Any())
            {
                return new ErrorResult(Messages.ClassNameFind);
            }

            var result = new Class
            {
                ClassName = nclass.ClassName
            };
            _classDal.Add(result);
            return new SuccessResult(Messages.ClassAdded);
        }

        public IResult Delete(int classID)
        {
            var result = _classDal.Get(x => x.ID == classID && x.Status!=DataStatus.Deleted);

            if (result==null)
            {
                return new ErrorResult(Messages.ClassNotFind);
            }
            if(_studentsDal.GetAll(x => x.ClassID == result.ID && x.Status!=DataStatus.Deleted).Any())
            {
                return new ErrorResult(Messages.ClassStudents);
            }
            if (_classCourseDal.GetAll(x=>x.ClassID==result.ID && x.Status != DataStatus.Deleted).Any())
            {
                return new ErrorResult(Messages.CoursesClass);
            }
            var select = new Class
            {
                ID = result.ID,
                ClassName = result.ClassName,
                DeletedDate=DateTime.Now,
                Status=DataStatus.Deleted
            };
                
            _classDal.Update(select);
            return new SuccessResult(Messages.ClassDeleted);
        }

        public IDataResult<List<Class>> GetAll()
        {
            return new SuccessDataResult<List<Class>>(_classDal.GetAll(x => x.Status != DataStatus.Deleted));
        }

        public IResult Update(ClassDto nclass)
        {
            var result = _classDal.Get(x => x.ID == nclass.ID && x.Status != DataStatus.Deleted);

            if (result == null)
            {
                return new ErrorResult(Messages.ClassNotFind);
            }
            if (_classDal.GetAll(x => x.ClassName.Equals(nclass.ClassName) && x.Status != DataStatus.Deleted).Any())
            {
                return new ErrorResult(Messages.ClassNameFind);
            }

            var select = new Class
            {
                ID = result.ID,
                ClassName = nclass.ClassName,
                ModifiedDate = DateTime.Now,
                Status = DataStatus.Upserted
            };

            _classDal.Update(select);
            return new SuccessResult(Messages.ClassUpdate);
        }
    }
}

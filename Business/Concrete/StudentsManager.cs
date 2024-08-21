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
    public class StudentsManager:IStudentsService
    {
        IStudentsDal _studentsDal;
        IClassDal _classDal;

        public StudentsManager(IStudentsDal studentsDal, IClassDal classDal)
        {
            _studentsDal = studentsDal;
            _classDal = classDal;
        }

        public IResult Add(StudentsDto students)
        {
            if (!_classDal.GetAll(x=>x.ID==students.ClassID && x.Status!=DataStatus.Deleted).Any())
            {
                return new ErrorResult(Messages.ClassNotFind);
            }

            var result = new Students
            {
                FirstName = students.FirstName,
                LastName = students.LastName,
                Email = students.Email,
                ClassID = students.ClassID
            };

            _studentsDal.Add(result);
            return new SuccessResult(Messages.StudentsAdded);
        }

        public IResult Update(StudentsDto students)
        {
            if (!_classDal.GetAll(x => x.ID != students.ClassID && x.Status != DataStatus.Deleted).Any())
            {
                return new ErrorResult(Messages.ClassNotFind);
            }

            var result = _studentsDal.Get(x=>x.ID==students.ID);
            var select = new Students
            {
                ID = result.ID,
                FirstName = students.FirstName,
                LastName = students.LastName,
                Email = students.Email,
                ClassID = students.ClassID,
                ModifiedDate = DateTime.Now,
                Status=DataStatus.Upserted
            };

            _studentsDal.Update(select);
            return new SuccessResult(Messages.StudentsModified);
        }

        public IResult Delete(int studentsID)
        {

            var result = _studentsDal.Get(x=>x.ID==studentsID&&x.Status!=DataStatus.Deleted);

            if (result==null)
            {
                return new ErrorResult(Messages.NotInput);
            }

            var select = new Students
            {
                ID=result.ID, 
                FirstName=result.FirstName, 
                LastName=result.LastName,
                Email = result.Email,
                ClassID = result.ClassID,
                DeletedDate=DateTime.Now,
                Status = DataStatus.Deleted
            };

            _studentsDal.Update(select);
            return new SuccessResult(Messages.StudentsDelete);
        }

        public IResult DeleteClassAll(int classID)
        {

            var result = _studentsDal.GetAll(x=>x.ClassID==classID && x.Status!=DataStatus.Deleted).ToList();

            foreach (var students in result)
            {
                var select = new Students
                {
                    ID = students.ID,
                    FirstName = students.FirstName,
                    LastName = students.LastName,
                    Email = students.Email,
                    ClassID = students.ClassID,
                    DeletedDate = DateTime.Now,
                    Status = DataStatus.Deleted
                };

                _studentsDal.Update(select);
            }

            return new SuccessResult(Messages.StudentsClassAllDelete);
        }


        public IDataResult<List<StudentsListDto>> GetAllList()
        {
            return new SuccessDataResult<List<StudentsListDto>>(_studentsDal.GetAllList());
        }

        public IDataResult<List<StudentsListDto>> GetClassList(int classID)
        {
            return new SuccessDataResult<List<StudentsListDto>>(_studentsDal.GetClassList(classID));
        }

        public IDataResult<List<Students>> GetByID(int studentsID)
        {
            return new SuccessDataResult<List<Students>>(_studentsDal.GetAll(x=>x.ID== studentsID));
        }

        public IDataResult<DashboardDto> GetDashboard()
        {
            return new SuccessDataResult<DashboardDto>(_studentsDal.GetDashboard());
        }
    }
}

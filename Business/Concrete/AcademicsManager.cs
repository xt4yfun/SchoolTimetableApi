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
    public class AcademicsManager:IAcademicsService
    {
        IAcademicsDal _academicsDal;

        public AcademicsManager(IAcademicsDal academicsDal)
        {
            _academicsDal = academicsDal;
        }

        public IResult Add(AcademicsDto academics)
        {
            var result = new Academics
            {
                FirstName = academics.FirstName,
                LastName = academics.LastName,
                EMail = academics.EMail,
                Phone= academics.Phone
            };
            _academicsDal.Add(result);
            return new SuccessResult(Messages.AcademicAdded);
        }

        public IResult Update(AcademicsDto academics)
        {
            var result = new Academics
            {
                ID= academics.ID,
                FirstName = academics.FirstName,
                LastName = academics.LastName,
                EMail = academics.EMail,
                Phone= academics.Phone,
                ModifiedDate=DateTime.Now,
                Status=DataStatus.Upserted
            };
            _academicsDal.Update(result);
            return new SuccessResult(Messages.AcademicModified);
        }

        public IResult Delete(int academicsID)
        {
            var result=_academicsDal.Get(x => x.ID == academicsID && x.Status != DataStatus.Deleted);

            if (result == null)
            {
                return new ErrorResult(Messages.NotInput);
            }

            var selected = new Academics
            {
                ID=result.ID,
                FirstName = result.FirstName,
                LastName = result.LastName,
                EMail = result.EMail,
                Phone = result.Phone,
                DeletedDate = DateTime.Now,
                Status = DataStatus.Deleted
            };
            _academicsDal.Update(selected);
            return new SuccessResult(Messages.AcademicDeleted);
        }

        public IDataResult<List<Academics>> GetAll()
        {
            return new SuccessDataResult<List<Academics>>(_academicsDal.GetAll(x=>x.Status!=DataStatus.Deleted));
        }
        public IDataResult<Academics> GetById(int academicID)
        {
            return new SuccessDataResult<Academics>(_academicsDal.Get(x=>x.ID==academicID && x.Status!=DataStatus.Deleted));
        }
    }
}

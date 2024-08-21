using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAcademicsService 
    {
        IResult Add(AcademicsDto academics);
        IResult Update(AcademicsDto academics);
        IResult Delete(int academicsID);
        IDataResult<List<Academics>> GetAll();
        IDataResult<Academics> GetById(int academicID);
    }
}

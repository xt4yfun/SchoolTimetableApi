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
    public interface ICoursesService
    {
        IResult Add(CoursesDto courses);
        IResult Update(CoursesDto courses);
        IResult Delete(int coursesID);
        IDataResult<Courses> GetByID(int ID);
        IDataResult<List<Courses>> GetAll();
    }
}

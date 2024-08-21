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
    public interface IClassCourseService
    {
        IResult Add(ClassCourseDto classCourse);
        IResult ClassAllAddCourse(int classID);
        IResult AllClassCourse();
        IResult Deleted(int classCourseID);
        IDataResult<List<ClassCourse>> GetAll();
        public IDataResult<List<CoursesClassListDto>> GetAllList();
        public IDataResult<List<CoursesClassListDto>> GetAllClass(int classID);
    }
}

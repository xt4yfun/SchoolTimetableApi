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
    public interface IStudentsService 
    { 
        IResult Add(StudentsDto students);
        IResult Update(StudentsDto students);
        IResult Delete(int studentsID);
        IResult DeleteClassAll(int classID);
        IDataResult<List<StudentsListDto>> GetAllList();
        IDataResult<DashboardDto> GetDashboard();
        IDataResult<List<Students>> GetByID(int studentsID);
        IDataResult<List<StudentsListDto>> GetClassList(int classID);
    }
}

using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStudentsDal:IEntityRepository<Students>
    {
        List<StudentsListDto> GetAllList();
        DashboardDto GetDashboard();
        List<StudentsListDto> GetClassList(int classID);
    }
}

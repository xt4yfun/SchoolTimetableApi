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
    public interface ITimetableService 
    {
        IResult Add();
        IResult TimetableClassAdd(int classID);
        IResult DeleteAll();
        IResult DeleteClass(int classID);
        IDataResult<List<TimetableDto>> GetAllList();
        IDataResult<List<TimetableDto>> GetClassList(int classID);
        IDataResult<List<TimetableDto>> GetDayList(string day);
    }
}

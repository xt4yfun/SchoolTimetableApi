using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ITimetableDal:IEntityRepository<Timetable>
    {
        List<TimetableDto> GetAllList();
        List<TimetableDto> GetClassList(int classID);
        List<TimetableDto> GetDayList(string day);
    }
}

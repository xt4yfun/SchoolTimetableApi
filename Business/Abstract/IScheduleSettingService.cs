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
    public interface IScheduleSettingService
    {
        IResult Updates(ScheduleSettingDto scheduleSetting);
        IResult Add(ScheduleSettingDto scheduleSetting);
        IDataResult<ScheduleSetting> GetBy();
        IDataResult<List<ScheduleSetting>> GetAll();
    }
}

using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Contents;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Migrations;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Business.Concrete
{
    public class ScheduleSettingManager : IScheduleSettingService
    {
        IScheduleSettingDal _scheduleSettingDal;
        IClassCourseService _classcourseService;
        ICoursesService _courseService;
        

        public ScheduleSettingManager(IScheduleSettingDal scheduleSettingDal, IClassCourseService classcourseService, ICoursesService courseService)
        {
            _scheduleSettingDal = scheduleSettingDal;
            _classcourseService = classcourseService;
            _courseService = courseService;
        }

        [SecuredOperation("scheduleSettingUpdates")]
        public IResult Updates(ScheduleSettingDto scheduleSetting)
        {
            var courseClassList = _classcourseService.GetAll().Data;
            int totalCredi = 0;
            foreach (var courseClass in courseClassList.FindAll(x=>x.ClassID==1))
            {
                int courseCredits = _courseService.GetByID(courseClass.CourseID).Data.Credit;
                totalCredi = courseCredits + totalCredi;
            }

            if ((scheduleSetting.DailyHours * scheduleSetting.WeeklyDays) != totalCredi)
            {
                return new ErrorResult(Messages.ClassCourseTotalLimited);
            }


            var setting = _scheduleSettingDal.GetAll(x => x.Status != DataStatus.Deleted).FirstOrDefault();
            if (setting == null)
            {
                return new ErrorResult(Messages.ScheduleSttingDontFind);
            }
            var result = new ScheduleSetting
            {
                ID = setting.ID,
                DailyHours = scheduleSetting.DailyHours,
                WeeklyDays = scheduleSetting.WeeklyDays,
                LunchBreak = scheduleSetting.LunchBreak,
                LessonDuration = scheduleSetting.LessonDuration,
                BreakDuration = scheduleSetting.BreakDuration,
                LunchBreakDuration = scheduleSetting.LunchBreakDuration,
                StartTime = scheduleSetting.StartTime,
                Status = DataStatus.Upserted,
                ModifiedDate=DateTime.Now                
            };
            _scheduleSettingDal.Update(result);
            return new SuccessResult(Messages.ScheduleSttingUpdated);
        }
        [SecuredOperation("scheduleSettingGetAll")]
        public IDataResult<ScheduleSetting> GetBy()
        {
            return new SuccessDataResult<ScheduleSetting>(_scheduleSettingDal.GetAll(x => x.Status != DataStatus.Deleted).FirstOrDefault());
        }
        [SecuredOperation("scheduleSettingGetAll")]
        public IDataResult<List<ScheduleSetting>> GetAll()
        {
            return new SuccessDataResult<List<ScheduleSetting>>(_scheduleSettingDal.GetAll());
        }
        [SecuredOperation("scheduleSettingAdd")]
        public IResult Add(ScheduleSettingDto scheduleSetting)
        {
            var courseClassList = _classcourseService.GetAll().Data;
            int totalCredi = 0;
            foreach (var courseClass in courseClassList.FindAll(x => x.ClassID == 1))
            {
                int courseCredits = _courseService.GetByID(courseClass.CourseID).Data.Credit;
                totalCredi = courseCredits + totalCredi;
            }

            if ((scheduleSetting.DailyHours * scheduleSetting.WeeklyDays) != totalCredi)
            {
                return new ErrorResult(Messages.ClassCourseTotalLimited);
            }


            var setting = _scheduleSettingDal.GetAll(x => x.Status != DataStatus.Deleted).ToList();
            if (setting.Count >= 1)
            {
                return new ErrorResult(Messages.ScheduleSttingDontAdded);
            }

            var result = new ScheduleSetting
            {
                DailyHours = scheduleSetting.DailyHours,
                WeeklyDays = scheduleSetting.WeeklyDays,
                LunchBreak = scheduleSetting.LunchBreak,
                LessonDuration = scheduleSetting.LessonDuration,
                BreakDuration = scheduleSetting.BreakDuration,
                LunchBreakDuration = scheduleSetting.LunchBreakDuration,
                StartTime = scheduleSetting.StartTime,
            };
            _scheduleSettingDal.Add(result);
            return new SuccessResult(Messages.ScheduleSttingAdded);
        }
    }
}

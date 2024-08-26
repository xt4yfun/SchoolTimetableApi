using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Contents;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Migrations;
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
    public class TimetableManager : ITimetableService
    {
        ITimetableDal _timetableDal;
        IScheduleSettingService _scheduleSettingService;
        IClassDal _classService;
        IClassCourseDal _classcourseService;
        ICoursesService _courseService;
        IAcademicsService _academicsService;

        public TimetableManager(ITimetableDal timetableDal, IScheduleSettingService scheduleSettingService, IClassDal classService, IAcademicsService academicsService, ICoursesService coursesService, IClassCourseDal classCourseService)
        {
            _timetableDal = timetableDal;
            _classService = classService;
            _scheduleSettingService = scheduleSettingService;
            _academicsService = academicsService;
            _courseService = coursesService;
            _classcourseService = classCourseService;
        }
        [SecuredOperation("timeTableAdd")]
        public IResult Add()
        {
            IResult result = BusinessRules.Run(AutoCreate());

            if (result != null)
            {
                return result;
            }

            return new SuccessResult(Messages.TimetableAdded);
        }


        public IResult AutoCreate()
        {
            var scheduleSetting = _scheduleSettingService.GetBy().Data;
            var classList = _classService.GetAll(x=>x.Status!=DataStatus.Deleted);
            var courseClassList = _classcourseService.GetAll(x => x.Status != DataStatus.Deleted);
            var academics = _academicsService.GetAll().Data;
            var dayNames = new string[] { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma" };
            Random rand = new Random();
            int academicIndex = 0;

            if (_timetableDal.GetAllList().Any())
            {
                return new ErrorResult(Messages.timetableFull);
            }

            foreach (var clas in classList)
            {
                var courseHoursMap = new Dictionary<int, int>();
                int totalCredi = 0;

                var classCourses = courseClassList.Where(x => x.ClassID == clas.ID && x.Status != DataStatus.Deleted).ToList();

                foreach (var courseClass in classCourses)
                {
                    int courseCredits = _courseService.GetByID(courseClass.CourseID).Data.Credit;
                    totalCredi += courseCredits;
                    courseHoursMap[courseClass.ID] = courseCredits;
                }

                if ((scheduleSetting.DailyHours * scheduleSetting.WeeklyDays) != totalCredi)
                {
                    return new ErrorResult(Messages.ClassCourseTotalLimited);
                }

                foreach (int day in Enumerable.Range(0, scheduleSetting.WeeklyDays))
                {
                    TimeSpan currentMinute = scheduleSetting.StartTime;
                    bool lunchBreakAdded = false;
                    int dailyTotalCredits = 0;
                    var dailyCourses = courseHoursMap
                        .Where(x => x.Value > 0)
                        .OrderBy(x => rand.Next())
                        .ToList();

                    foreach (var selectedCourse in dailyCourses)
                    {
                        int classCourseID = selectedCourse.Key;
                        int remainingCredits = selectedCourse.Value;
                        int sayac = 0;  // Sayacı her ders ekleme döngüsünün başında sıfırla
                        while (remainingCredits > 0 && currentMinute < scheduleSetting.StartTime + TimeSpan.FromHours(scheduleSetting.DailyHours))
                        {
                            TimeSpan lessonDuration = TimeSpan.FromMinutes(scheduleSetting.LessonDuration);
                            TimeSpan breakDuration = TimeSpan.FromMinutes(scheduleSetting.BreakDuration);
                            TimeSpan lunchBreakDuration = TimeSpan.FromMinutes(scheduleSetting.LunchBreakDuration);

                            if (scheduleSetting.LunchBreak && !lunchBreakAdded && dailyTotalCredits >= scheduleSetting.DailyHours / 2)
                            {
                                lunchBreakAdded = true;
                                currentMinute += lunchBreakDuration - breakDuration;
                            }

                            if (dailyTotalCredits + 1 <= scheduleSetting.DailyHours)
                            {
                                // Ders programına ekle
                                var timeTable = new Timetable
                                {
                                    ClassCourseID = classCourseID,
                                    AcademicsID = academics[academicIndex].ID,
                                    Day = dayNames[day],
                                    StartTime = currentMinute,
                                    EndTime = currentMinute + lessonDuration
                                };
                                _timetableDal.Add(timeTable);

                                currentMinute += lessonDuration + breakDuration;
                                remainingCredits--;
                                dailyTotalCredits++;
                                sayac++;  // Ders eklendiğinde sayacı artır

                                if (remainingCredits > 0 && sayac >= 2)
                                {
                                    break;  // İki ardışık dersten sonra döngüyü kır
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        courseHoursMap[classCourseID] = remainingCredits;
                        academicIndex = (academicIndex + 1) % academics.Count;
                    }

                    // Eğer son güne eksik ders kalmışsa, buradaki kontrol ile eksik dersleri tamamla
                    if (day == scheduleSetting.WeeklyDays - 1 && courseHoursMap.Values.Sum() > 0)
                    {
                        // Kalan dersleri cuma gününe ekle
                        foreach (var selectedCourse in dailyCourses)
                        {
                            int classCourseID = selectedCourse.Key;
                            int remainingCredits = courseHoursMap[classCourseID];
                            while (remainingCredits > 0 && currentMinute < scheduleSetting.StartTime + TimeSpan.FromHours(scheduleSetting.DailyHours))
                            {
                                TimeSpan lessonDuration = TimeSpan.FromMinutes(scheduleSetting.LessonDuration);
                                TimeSpan breakDuration = TimeSpan.FromMinutes(scheduleSetting.BreakDuration);

                                var timeTable = new Timetable
                                {
                                    ClassCourseID = classCourseID,
                                    AcademicsID = academics[academicIndex].ID,
                                    Day = dayNames[day],
                                    StartTime = currentMinute,
                                    EndTime = currentMinute + lessonDuration
                                };
                                _timetableDal.Add(timeTable);

                                currentMinute += lessonDuration + breakDuration;
                                remainingCredits--;
                                dailyTotalCredits++;
                            }
                            courseHoursMap[classCourseID] = remainingCredits;
                        }
                    }
                }
            }

            return new SuccessDataResult<IResult>();
        }


        [SecuredOperation("timeTableDelete")]
        public IResult DeleteAll()
        {
            var silList = _timetableDal.GetAll(x => x.Status != DataStatus.Deleted);
            
            foreach (var sil in silList)
            {
                var selected = new Timetable
                {
                    ID=sil.ID,
                    ClassCourseID = sil.ClassCourseID,
                    AcademicsID = sil.AcademicsID,
                    Day = sil.Day,
                    StartTime = sil.StartTime,
                    EndTime = sil.EndTime,
                    DeletedDate =DateTime.Now,
                    Status=DataStatus.Deleted
                };
                _timetableDal.Update(selected);
            }

            return new SuccessResult(Messages.TimetableAllDeleted);
        }
        [SecuredOperation("timeTableDelete")]
        public IResult DeleteClass(int classID)
        {
            var deletedClassList = _classcourseService.GetAll(x => x.ClassID == classID && x.Status != DataStatus.Deleted);
            if (deletedClassList == null)
            {
                return new ErrorResult(Messages.ClassNotFind);
            }
            foreach (var item in deletedClassList)
            {
                var deletedClass = _timetableDal.GetAll(x => x.ClassCourseID == item.ID && x.Status != DataStatus.Deleted);
                if (deletedClass==null)
                {
                    continue;
                }
                foreach (var item1 in deletedClass)
                {
                    var selected = new Timetable
                    {
                        ID = item1.ID,
                        ClassCourseID = item1.ClassCourseID,
                        AcademicsID = item1.AcademicsID,
                        Day = item1.Day,
                        StartTime = item1.StartTime,
                        EndTime = item1.EndTime,
                        DeletedDate = DateTime.Now,
                        Status = DataStatus.Deleted
                    };
                    _timetableDal.Update(selected);
                }
                
            }
            return new SuccessDataResult<IResult>(Messages.timeTableClassDeleted);
        }


        [SecuredOperation("timeTableGetList")]
        public IDataResult<List<TimetableDto>> GetAllList()
        {
            return new SuccessDataResult<List<TimetableDto>>(_timetableDal.GetAllList());
        }
        [SecuredOperation("timeTableGetList")]
        public IDataResult<List<TimetableDto>> GetClassList(int classID)
        {
            return new SuccessDataResult<List<TimetableDto>>(_timetableDal.GetClassList(classID));
        }
        [SecuredOperation("timeTableGetList")]
        public IDataResult<List<TimetableDto>> GetDayList(string day)
        {
            return new SuccessDataResult<List<TimetableDto>>(_timetableDal.GetDayList(day));
        }

        public IResult AutoClassCreate(int classID)
        {

            var scheduleSetting = _scheduleSettingService.GetBy().Data;
            var classList = _classService.Get(x => x.ID == classID && x.Status != DataStatus.Deleted);
            if (classList == null)
            {
                return new ErrorResult(Messages.ClassNotFind);
            }
            var courseClassList = _classcourseService.GetAll(x => x.Status != DataStatus.Deleted).Where(x=>x.ClassID== classID).ToList();
            var academics = _academicsService.GetAll().Data;
            var dayNames = new string[] { "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma" };
            Random rand = new Random();
            int academicIndex = 0;

            if (_timetableDal.GetClassList(classID).Any())
            {
                return new ErrorResult(Messages.timetableFull);
            }

            var courseHoursMap = new Dictionary<int, int>();
            int totalCredi = 0;

            var classCourses = courseClassList.Where(x => x.ClassID == classList.ID && x.Status != DataStatus.Deleted).ToList();

            foreach (var courseClass in classCourses)
            {
                int courseCredits = _courseService.GetByID(courseClass.CourseID).Data.Credit;
                totalCredi += courseCredits;
                courseHoursMap[courseClass.ID] = courseCredits;
            }

            if ((scheduleSetting.DailyHours * scheduleSetting.WeeklyDays) != totalCredi)
            {
                return new ErrorResult(Messages.ClassCourseTotalLimited);
            }

            foreach (int day in Enumerable.Range(0, scheduleSetting.WeeklyDays))
            {
                TimeSpan currentMinute = scheduleSetting.StartTime;
                bool lunchBreakAdded = false;
                int dailyTotalCredits = 0;
                var dailyCourses = courseHoursMap
                    .Where(x => x.Value > 0)
                    .OrderBy(x => rand.Next())
                    .ToList();

                foreach (var selectedCourse in dailyCourses)
                {
                    int classCourseID = selectedCourse.Key;
                    int remainingCredits = selectedCourse.Value;
                    int sayac = 0;
                    while (remainingCredits > 0 && currentMinute < scheduleSetting.StartTime + TimeSpan.FromHours(scheduleSetting.DailyHours))
                    {
                        TimeSpan lessonDuration = TimeSpan.FromMinutes(scheduleSetting.LessonDuration);
                        TimeSpan breakDuration = TimeSpan.FromMinutes(scheduleSetting.BreakDuration);
                        TimeSpan lunchBreakDuration = TimeSpan.FromMinutes(scheduleSetting.LunchBreakDuration);

                        if (scheduleSetting.LunchBreak && !lunchBreakAdded && dailyTotalCredits >= scheduleSetting.DailyHours / 2)
                        {
                            lunchBreakAdded = true;
                            currentMinute += lunchBreakDuration - breakDuration;
                        }



                        if (dailyTotalCredits + 1 <= scheduleSetting.DailyHours)
                        {

                            // Ders programına ekle
                            var timeTable = new Timetable
                            {
                                ClassCourseID = classCourseID,
                                AcademicsID = academics[academicIndex].ID,
                                Day = dayNames[day],
                                StartTime = currentMinute,
                                EndTime = currentMinute + lessonDuration
                            };
                            _timetableDal.Add(timeTable);

                            currentMinute += lessonDuration + breakDuration;
                            remainingCredits--;
                            dailyTotalCredits++;
                            if (remainingCredits >= 2)
                            {
                                sayac++;
                                if (sayac >= 2)
                                {
                                    break;
                                }
                            }

                        }
                        else
                        {
                            break;
                        }
                    }
                    courseHoursMap[classCourseID] = remainingCredits;
                    academicIndex = (academicIndex + 1) % academics.Count;
                }
            }

            return new SuccessDataResult<IResult>();
        }
        [SecuredOperation("timeTableAdd")]
        public IResult TimetableClassAdd(int classID)
        {
            IResult result = BusinessRules.Run(AutoClassCreate(classID));

            if (result != null)
            {
                return result;
            }

            return new SuccessResult(Messages.TimetableClassAdded);
        }
    }
}

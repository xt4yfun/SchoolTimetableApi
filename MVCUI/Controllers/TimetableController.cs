using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCUI.Models;
using System.Diagnostics;

namespace MVCUI.Controllers
{
    public class TimetableController : Controller
    {
        private readonly ITimetableService _timetableService;
        private readonly IScheduleSettingDal _scheduleSettingDal;
        private readonly IScheduleSettingService _scheduleSettingService;
        private readonly IClassService _classService;
        public TimetableController(ITimetableService timetableService, IScheduleSettingDal scheduleSettingDal, IScheduleSettingService scheduleSettingService, IClassService classService)
        {
            _timetableService = timetableService;
            _scheduleSettingDal = scheduleSettingDal;
            _scheduleSettingService = scheduleSettingService;
            _classService = classService;
        }

        public IActionResult Index(string selectedValue)
        {
            var clas = _classService.GetAll().Data;
            var clasList = clas.Select(e => new SelectListItem
            {
                Value = e.ID.ToString(),
                Text = e.ClassName.ToString()
            });

            ViewBag.Options = new SelectList(clasList, "Value", "Text");


            var message = TempData["Message"];
            var error = TempData["Error"];
            if (message != null)
            {
                ViewBag.Message = message;
            }
            if (error != null)
            {
                ViewBag.Error = error;
            }

            int sinifID;
            if (int.TryParse(selectedValue, out sinifID))
            {
                ViewBag.setting = _scheduleSettingDal.GetAll().FirstOrDefault();
                var result = _timetableService.GetClassList(sinifID);
                if (result.IsSuccess)
                {
                    return View(result.Data); // Sadece Data'yı View'e gönderiyoruz
                }
                return View("Error", result.Message);
            }
            ViewBag.setting = _scheduleSettingDal.GetAll().FirstOrDefault();

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            var result = await Task.Run(() => _timetableService.Add());
            if (result.IsSuccess)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            var result = await Task.Run(() => _timetableService.DeleteAll());
            if (result.IsSuccess)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ScheduleSetting(int DailyHours, int WeeklyDays, bool LunchBreak,int LessonDuration,int BreakDuration,int LunchBreakDuration,string StartTime)
        {
            TimeSpan time = TimeSpan.Parse(StartTime);
            var reslt = new ScheduleSettingDto
            {
                DailyHours = DailyHours,
                WeeklyDays = WeeklyDays,
                LunchBreak = LunchBreak,
                LessonDuration= LessonDuration,
                BreakDuration= BreakDuration,
                LunchBreakDuration= LunchBreakDuration,
                StartTime= time
            };

            var result = await Task.Run(() => _scheduleSettingService.Updates(reslt));
            if (result.IsSuccess)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }
        }

    }
}

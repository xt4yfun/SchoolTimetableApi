using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleSettingController : ControllerBase
    {
        IScheduleSettingService _scheduleSettingService;

        public ScheduleSettingController(IScheduleSettingService scheduleSettingService)
        {
            _scheduleSettingService = scheduleSettingService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var result = _scheduleSettingService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet]
        public IActionResult GetBy()
        {
            var result = _scheduleSettingService.GetBy();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public IActionResult Updates(ScheduleSettingDto scheduleSetting)
        {
            var result = _scheduleSettingService.Updates(scheduleSetting);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult Add(ScheduleSettingDto scheduleSetting)
        {
            var result = _scheduleSettingService.Add(scheduleSetting);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

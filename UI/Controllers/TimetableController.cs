using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        ITimetableService _timetableService;
        

        public TimetableController(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        [HttpPost]
        public IActionResult Add()
        {
            var result = _timetableService.Add();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("classBy")]
        public IActionResult TimetableClassAdd(int classID)
        {
            var result = _timetableService.TimetableClassAdd(classID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _timetableService.GetAllList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getClassList")]
        public IActionResult GetClassList(int classID)
        {
            var result=_timetableService.GetClassList(classID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getDayList")]
        public IActionResult GetDayList(string dayName)
        {
            var result=_timetableService.GetDayList(dayName);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("deleteAll")]
        public IActionResult DeleteAll()
        {
            var result=_timetableService.DeleteAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("deleteClass")]
        public IActionResult DeleteClass(int classID)
        {
            var result=_timetableService.DeleteClass(classID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}

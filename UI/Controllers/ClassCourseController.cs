using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassCourseController : ControllerBase
    {
        IClassCourseService _courseService;

        public ClassCourseController(IClassCourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpPost]
        public IActionResult Add(ClassCourseDto classCourse)
        {
            var result = _courseService.Add(classCourse);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPost("allClassCourse")]
        public IActionResult AllClassCourse()
        {
            var result = _courseService.AllClassCourse();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPost("ClassAllAddCourse")]
        public IActionResult ClassAllAddCourse(int classID)
        {
            var result = _courseService.ClassAllAddCourse(classID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPut("Deleted")]
        public IActionResult Deleted(int classCourseID)
        {
            var result = _courseService.Deleted(classCourseID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _courseService.GetAllList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getClassAll")]
        public IActionResult getClassAll(int classID)
        {
            var result = _courseService.GetAllClass(classID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}

using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _studentsService.GetAllList();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("Dashboard")]
        public IActionResult GetDashboard()
        {
            var result = _studentsService.GetDashboard();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int studentID)
        {
            var result = _studentsService.GetByID(studentID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("class")]
        public IActionResult GetClassAll(int classID)
        {
            var result = _studentsService.GetClassList(classID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult Add(StudentsDto students)
        {
            var result = _studentsService.Add(students);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("delete")]
        public IActionResult Delete(int studentsID)
        {
            var result = _studentsService.Delete(studentsID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPut("classDeleteAll")]
        public IActionResult DeleteClassAll(int classId)
        {
            var result = _studentsService.DeleteClassAll(classId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPut("update")]
        public IActionResult Update(StudentsDto students)
        {
            var result = _studentsService.Update(students);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

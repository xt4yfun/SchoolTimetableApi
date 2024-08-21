using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicsController : ControllerBase
    {
        IAcademicsService _academicsService;

        public AcademicsController(IAcademicsService academicsService)
        {
            _academicsService = academicsService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _academicsService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int academicID)
        {
            var result = _academicsService.GetById(academicID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult Add(AcademicsDto academics)
        {
            var result = _academicsService.Add(academics);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(AcademicsDto academics)
        {
            var result = _academicsService.Update(academics);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("delete")]
        public IActionResult Delete(int academicsID)
        {
            var result = _academicsService.Delete(academicsID);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}

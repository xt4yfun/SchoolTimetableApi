using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        IRoleUserService _roleUserService;

        public RoleUserController(IRoleUserService roleUserService)
        {
            _roleUserService = roleUserService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleUserService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("Get")]
        public IActionResult Get(int id)
        {
            var result = _roleUserService.Get(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult Add(RoleUserDto roleUserDto)
        {
            var result = _roleUserService.Add(roleUserDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _roleUserService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

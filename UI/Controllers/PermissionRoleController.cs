using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionRoleController : ControllerBase
    {
        IPermissionRoleService _permissionRoleService;

        public PermissionRoleController(IPermissionRoleService permissionRoleService)
        {
            _permissionRoleService = permissionRoleService;
        }

        [HttpPost]
        public IActionResult Add(PermissionRoleDto permissionRole)
        {
            var result = _permissionRoleService.Add(permissionRole);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _permissionRoleService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _permissionRoleService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("Get")]
        public IActionResult GetBy(int id)
        {
            var result = _permissionRoleService.GetByID(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}

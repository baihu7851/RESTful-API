using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;

        public RoleController(IRole role)
        {
            _role = role;
        }

        // GET: api/Role/All
        [HttpGet("All")]
        public ActionResult<ViewRole> GetAll()
        {
            var result = _role.GetRoles();
            if (result == null)
            {
                return Ok(null);
            }
            return Ok(_role);
        }

        // GET api/Role/5
        [HttpGet("{id}")]
        public ActionResult<ViewRole> Get(int id)
        {
            var result = _role.GetRole(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/Role/Add
        [HttpPost("Add")]
        public ActionResult<ViewRole> Post(List<ViewRole> roles)
        {
            var result = _role.AddRole(roles);
            if (result == null)
            {
                return BadRequest("新增失敗");
            }
            return CreatedAtAction(nameof(Post), result);
        }

        // PUT api/Role/Update
        [HttpPut("Update")]
        public ActionResult<ViewRole> Put(List<ViewRole> roles)
        {
            var result = _role.UpdateRole(roles);
            if (result == null)
            {
                return BadRequest("更新失敗");
            }
            return Ok(result);
        }

        // DELETE api/Role/Delete
        [HttpDelete("Delete")]
        public ActionResult<ViewRole> Delete(int id)
        {
            var result = _role.DeleteRole(id);
            if (result == null)
            {
                return BadRequest("刪除失敗");
            }
            return Ok(result);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Repository.Interfaces;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleInterface _role;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleInterface roleInterface, ILogger<RoleController> logger)
        {
            _role = roleInterface;
            _logger = logger;
        }

        // GET: api/<RoleController>
        [HttpGet("All")]
        public ActionResult<Role> Get()
        {
            return Ok(_role.View<Role>());
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public ActionResult<Role> Get(int id)
        {
            var role = _role.View<Role>(id);
            if (role != null) return Ok(role);
            _logger.LogError("角色 ID 錯誤");
            return NotFound();
        }

        // POST api/<RoleController>
        [HttpPost("Add")]
        public ActionResult<Role> Post(List<Role> role)
        {
            _role.Add(role);
            return CreatedAtAction(nameof(Post), role);
        }

        // PUT api/<RoleController>/5
        [HttpPut("Update")]
        public ActionResult<Role> Put(List<Role> role)
        {
            _role.Update(role);
            return NoContent();
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("Delete")]
        public ActionResult<Role> Delete(List<int> id)
        {
            _role.Delete(id);
            _logger.LogError($"角色 {id} 被刪除");
            return NoContent();
        }
    }
}
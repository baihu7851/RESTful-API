using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;
        private readonly ILogger<RoleController> _logger;
        private readonly IMemoryCache _cache;

        public RoleController(IRole role, ILogger<RoleController> logger, IMemoryCache cache)
        {
            _role = role;
            _logger = logger;
            _cache = cache;
        }

        // GET: api/<RoleController>
        [HttpGet("All")]
        public ActionResult<ViewRole> Get()
        {
            if (!_cache.TryGetValue("GetRoleAll", out List<ViewRole> roles))
            {
                roles = _role.GetRoles();
                var cacheTime = DateTimeOffset.Now.AddHours(1);
                _cache.Set("GetRoleAll", roles, cacheTime);
            }
            return Ok(roles);
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public ActionResult<ViewRole> Get(int id)
        {
            if (!_cache.TryGetValue($"GetRole{id}", out ViewRole role))
            {
                role = _role.GetRole(id);
                var cacheTime = DateTimeOffset.Now.AddHours(1);
                _cache.Set($"GetRole{id}", role, cacheTime);
            }
            if (role != null)
            {
                return Ok(role);
            }
            _logger.LogError("角色 ID 錯誤");
            return NotFound();
        }

        // POST api/<RoleController>
        [HttpPost("Add")]
        public ActionResult<ViewRole> Post(List<ViewRole> roles)
        {
            _role.AddRole(roles);
            return CreatedAtAction(nameof(Post), roles);
        }

        // PUT api/<RoleController>/5
        [HttpPut("Update")]
        public ActionResult<ViewRole> Put(List<ViewRole> roles)
        {
            _role.UpdateRole(roles);
            return NoContent();
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("Delete")]
        public ActionResult<ViewRole> Delete(List<int> id)
        {
            _role.DeleteRole(id);
            _logger.LogError($"角色 {id} 被刪除");
            return NoContent();
        }
    }
}
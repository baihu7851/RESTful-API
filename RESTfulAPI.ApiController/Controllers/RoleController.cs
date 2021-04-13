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
        private readonly IRoleUser _roleUser;
        private readonly ILogger<RoleController> _logger;
        private readonly IMemoryCache _cache;

        public RoleController(IRole role, IRoleUser roleUser, ILogger<RoleController> logger, IMemoryCache cache)
        {
            _role = role;
            _roleUser = roleUser;
            _logger = logger;
            _cache = cache;
        }

        // GET: api/Role/All
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

        // GET api/Role/5
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

        // POST api/Role/Add
        [HttpPost("Add")]
        public ActionResult<ViewRole> Post(List<ViewRole> roles)
        {
            _role.AddRole(roles);
            return CreatedAtAction(nameof(Post), roles);
        }

        // PUT api/Role/Update
        [HttpPut("Update")]
        public ActionResult<ViewRole> Put(List<ViewRole> roles)
        {
            _role.UpdateRole(roles);
            return NoContent();
        }

        // DELETE api/Role/Delete
        [HttpDelete("Delete")]
        public ActionResult<ViewRole> Delete(List<int> id)
        {
            _role.DeleteRole(id);
            _logger.LogError($"角色 {id} 被刪除");
            return NoContent();
        }

        // GET api/Role/Users/5
        [HttpGet("Users/{roleId}")]
        public ActionResult<List<int>> GetUserId(int roleId)
        {
            return _roleUser.GetRoleUser(roleId);
        }

        // POST api/Role/Users/Add
        [HttpPost("Users/Add")]
        public ActionResult Post(int roleId, List<int> usersId)
        {
            _roleUser.AddRoleUser(roleId, usersId);
            return Ok();
        }

        // DELETE api/Role/Users/Delete
        [HttpDelete("Users/Delete")]
        public ActionResult Delete(int roleId, List<int> usersId)
        {
            _roleUser.DeleteRoleUser(roleId, usersId);
            _logger.LogError($"角色 {roleId} 刪除 {usersId}");
            return NoContent();
        }
    }
}
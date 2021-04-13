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
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IRoleUser _roleUser;
        private readonly ILogger<UserController> _logger;
        private readonly IMemoryCache _cache;

        public UserController(IUser user, IRoleUser roleUser, ILogger<UserController> logger, IMemoryCache cache)
        {
            _user = user;
            _roleUser = roleUser;
            _logger = logger;
            _cache = cache;
        }

        // GET: api/User/All
        [HttpGet("All", Name = "GetUsers")]
        public ActionResult<ViewUser> Get()
        {
            if (!_cache.TryGetValue("GetUserAll", out List<ViewUser> users))
            {
                users = _user.GetUsers();
                var cacheTime = DateTimeOffset.Now.AddHours(1);
                _cache.Set("GetUserAll", users, cacheTime);
            }
            return Ok(users);
        }

        // GET api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<ViewUser> Get(int id)
        {
            if (!_cache.TryGetValue($"GetUser{id}", out ViewUser user))
            {
                user = _user.GetUser(id);
                var cacheTime = DateTimeOffset.Now.AddHours(1);
                _cache.Set($"GetUser{id}", user, cacheTime);
            }
            if (user != null)
            {
                return Ok(user);
            }
            _logger.LogError("使用者 ID 錯誤");
            return NotFound();
        }

        // POST api/User/Add
        [HttpPost("Add")]
        public ActionResult<ViewUser> Post(List<ViewUser> user)
        {
            _user.AddUser(user);
            return CreatedAtAction(nameof(Post), user);
        }

        // PUT api/User/Update
        [HttpPut("Update")]
        public ActionResult<ViewUser> Put(List<ViewUser> user)
        {
            _user.UpdateUser(user);
            return NoContent();
        }

        // DELETE api/User/Delete
        [HttpDelete("Delete")]
        public ActionResult<ViewUser> Delete(List<int> id)
        {
            _user.DeleteUser(id);
            _logger.LogError($"使用者 {id} 被刪除");
            return NoContent();
        }

        // GET api/User/Roles/5
        [HttpGet("Roles/{userId}")]
        public ActionResult<List<int>> GetRoleId(int userId)
        {
            return _roleUser.GetUserRole(userId);
        }

        // POST api/User/Roles/Add
        [HttpPost("Roles/Add")]
        public ActionResult Post(int userId, List<int> rolesId)
        {
            _roleUser.AddUserRole(userId, rolesId);
            return Ok();
        }

        // DELETE api/User/Roles/Delete
        [HttpDelete("Roles/Delete")]
        public ActionResult Delete(int userId, List<int> rolesId)
        {
            _roleUser.DeleteUserRole(userId, rolesId);
            _logger.LogError($"角色 {userId} 刪除 {rolesId}");
            return NoContent();
        }
    }
}
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
        private readonly ILogger<UserController> _logger;
        private readonly IMemoryCache _cache;

        public UserController(IUser userInterface, ILogger<UserController> logger, IMemoryCache cache)
        {
            _user = userInterface;
            _logger = logger;
            _cache = cache;
        }

        // GET: api/<UserController>
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

        // GET api/<UserController>/5
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

        // POST api/<UserController>
        [HttpPost("Add")]
        public ActionResult<ViewUser> Post(List<ViewUser> user)
        {
            _user.AddUser(user);
            return CreatedAtAction(nameof(Post), user);
        }

        // PUT api/<UserController>/5
        [HttpPut("Update")]
        public ActionResult<ViewUser> Put(List<ViewUser> user)
        {
            _user.UpdateUser(user);
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete")]
        public ActionResult<ViewUser> Delete(List<int> id)
        {
            _user.DeleteUser(id);
            _logger.LogError($"使用者 {id} 被刪除");
            return NoContent();
        }
    }
}
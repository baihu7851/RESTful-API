using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.Repository.Interfaces;
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
        public ActionResult<User> Get()
        {
            if (!_cache.TryGetValue("GetUserAll", out List<User> users))
            {
                users = _user.GetUsers();
                var cacheTime = DateTimeOffset.Now.AddHours(1);
                _cache.Set("GetUserAll", users, cacheTime);
            }
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> Get(int id)
        {
            if (!_cache.TryGetValue($"User{id}", out User user))
            {
                user = _user.GetUser(id);
                var cacheTime = DateTimeOffset.Now.AddHours(1);
                _cache.Set("UserAll", user, cacheTime);
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
        public ActionResult<User> Post(List<User> user)
        {
            //_user.Add(user);
            //return CreatedAtAction(nameof(Post), user);
            return null;
 
        }

        // PUT api/<UserController>/5
        [HttpPut("Update")]
        public ActionResult<User> Put(List<User> user)
        {
            //_user.Update(user);
            //return NoContent();
            return null;

        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete")]
        public ActionResult<User> Delete(List<int> id)
        {
            //_user.Delete(id);
            //_logger.LogError($"使用者 {id} 被刪除");
            //return NoContent();
            return null;
        }
    }
}
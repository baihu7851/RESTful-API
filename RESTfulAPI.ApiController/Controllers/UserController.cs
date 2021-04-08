using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Middleware.ViewModel;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        private readonly ILogger<UserController> _logger;

        public UserController(IUser userRepository, ILogger<UserController> logger)
        {
            _user = userRepository;
            _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet("All")]
        public ActionResult<User> Get()
        {
            return Ok(_user.Users());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}", Name = "GetUserRoute")]
        public ActionResult<User> Get(int id)
        {
            User user = _user.User(id);

            if (user != null)
            {
                return Ok(user);
            }
            _logger.LogError("使用者 ID 錯誤");
            return NotFound();
        }

        // POST api/<UserController>
        [HttpPost("Add")]
        public ActionResult<User> Post(IEnumerable<User> user)
        {
            _user.Add(user);
            return Ok("新增使用者成功");
        }

        // PUT api/<UserController>/5
        [HttpPut("Update")]
        public ActionResult<User> Put(IEnumerable<User> user)
        {
            _user.Update(user);
            return Ok("更新使用者成功");
        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete")]
        public ActionResult<User> Delete(IEnumerable<int> id)
        {
            _user.Delete(id);
            _logger.LogError($"使用者 {id} 被刪除");
            return Ok();
        }
    }
}
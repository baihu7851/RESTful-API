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
        private readonly IDataInterface _user;
        private readonly ILogger<UserController> _logger;

        public UserController(IDataInterface userRepository, ILogger<UserController> logger)
        {
            _user = userRepository;
            _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet("All")]
        public ActionResult<User> Get()
        {
            return Ok(_user.View<User>());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _user.View<User>(id);
            if (user != null) return Ok(user);
            _logger.LogError("使用者 ID 錯誤");
            return NotFound();
        }

        // POST api/<UserController>
        [HttpPost("Add")]
        public ActionResult<User> Post(List<User> user)
        {
            _user.Add(user);
            return CreatedAtAction(nameof(Post), user);
        }

        // PUT api/<UserController>/5
        [HttpPut("Update")]
        public ActionResult<User> Put(List<User> user)
        {
            _user.Update(user);
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete")]
        public ActionResult<User> Delete(List<int> id)
        {
            _user.Delete(id);
            _logger.LogError($"使用者 {id} 被刪除");
            return NoContent();
        }
    }
}
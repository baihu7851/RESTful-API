using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        // GET: api/User/All
        [HttpGet("All", Name = nameof(GetAll))]
        public ActionResult GetAll()
        {
            var result = _user.GetUsers();
            return Ok(result);
        }

        // GET api/User/5
        [HttpGet("{id}", Name = nameof(Get))]
        public ActionResult Get(int id)
        {
            var result = _user.GetUser(id);
            return result == null ? NotFound() : Ok(result);
        }

        // POST api/User/Add
        [HttpPost("Add", Name = nameof(Post))]
        public ActionResult<ViewUser> Post(List<ViewUser> user)
        {
            var result = _user.AddUser(user);
            return CreatedAtAction(nameof(Post), result);
        }

        // PUT api/User/Update
        [HttpPut("Update", Name = nameof(Put))]
        public ActionResult Put(List<ViewUser> user)
        {
            List<ViewUser> result = _user.UpdateUser(user);
            return result == null ? NotFound() : Ok(result);
        }

        // DELETE api/User/Delete
        [HttpDelete("Delete/{id}", Name = nameof(Delete))]
        public ActionResult<ViewUser> Delete(int id)
        {
            var result = _user.DeleteUser(id);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
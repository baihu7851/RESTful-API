using System;
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
        public ActionResult<ViewUser> GetAll()
        {
            var users = _user.GetUsers();
            var cacheTime = DateTimeOffset.Now.AddMinutes(1);

            return Ok(users);
        }

        // GET api/User/5
        [HttpGet("{id}", Name = nameof(Get))]
        public ActionResult<object> Get(int id)
        {
            var user = _user.GetUser(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST api/User/Add
        [HttpPost("Add", Name = nameof(Post))]
        public ActionResult<ViewUser> Post(List<ViewUser> user)
        {
            _user.AddUser(user);
            return CreatedAtAction(nameof(Post), user);
        }

        // PUT api/User/Update
        [HttpPut("Update", Name = nameof(Put))]
        public ActionResult<ViewUser> Put(List<ViewUser> user)
        {
            //try
            //{
            _user.UpdateUser(user);
            //var res = Get(user.id)
            var res = new ViewUser();
            return Ok(res);
            //}
            //catch (Exception e)
            //{
            //    throw BadRequest(e.Message);
            //}
        }

        // DELETE api/User/Delete
        [HttpDelete("Delete", Name = nameof(Delete))]
        public ActionResult<ViewUser> Delete(List<int> id)
        {
            _user.DeleteUser(id);
            return NoContent();
        }

        //private IEnumerable<ViewLink> CreateLinks(int? id, List<ViewUser> user, List<int?> listId, int? userId, List<int?> rolesId)
        //{
        //    var links = new List<ViewLink>
        //    {
        //        //new(Url.Link(nameof(GetAll), null)),
        //        new(Url.Link(nameof(Get),  id )),
        //        new(Url.Link(nameof(RoleController.Get), id)),
        //        new(Url.Link(nameof(UserRoleController.Post),new {id=userId,rolesId})),
        //        new(Url.Link(nameof(UserRoleController.Delete),new {id=userId,rolesId}))
        //    };

        //    return links;
        //}
    }
}
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

        /// <summary>
        /// 使用者列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("All", Name = nameof(GetAll))]
        public ActionResult GetAll()
        {
            var result = _user.GetUsers();
            return Ok(result);
        }

        /// <summary>
        /// 使用者資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = nameof(Get))]
        public ActionResult Get(int id)
        {
            var result = _user.GetUser(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost("Add", Name = nameof(Post))]
        public ActionResult Post(List<ViewUser> users)
        {
            var result = _user.AddUser(users);
            return CreatedAtAction(nameof(Post), result);
        }

        /// <summary>
        /// 更新使用者資料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("Update", Name = nameof(Put))]
        public ActionResult Put(List<ViewUser> user)
        {
            List<ViewUser> result = _user.UpdateUser(user);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// 刪除使用者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}", Name = nameof(Delete))]
        public ActionResult Delete(int id)
        {
            var result = _user.DeleteUser(id);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
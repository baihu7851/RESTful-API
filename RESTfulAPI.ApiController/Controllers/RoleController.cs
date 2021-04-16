using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _role;

        public RoleController(IRole role)
        {
            _role = role;
        }

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("All")]
        public ActionResult GetAll()
        {
            var result = _role.GetRoles();
            return Ok(result);
        }

        /// <summary>
        /// 角色資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _role.GetRole(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public ActionResult Post(List<ViewRole> roles)
        {
            var result = _role.AddRole(roles);
            return CreatedAtAction(nameof(Post), result);
        }

        /// <summary>
        /// 更新角色資料
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public ActionResult Put(List<ViewRole> roles)
        {
            List<ViewRole> result = _role.UpdateRole(roles);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// 刪除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            var result = _role.DeleteRole(id);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
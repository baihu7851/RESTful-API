using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Repository.Interfaces;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly IRoleUserInterface _roleUser;
        private readonly ILogger<RoleUserController> _logger;

        public RoleUserController(IRoleUserInterface roleUserInterface, ILogger<RoleUserController> logger)
        {
            _roleUser = roleUserInterface;
            _logger = logger;
        }

        // GET: api/<RoleUserController>
        [HttpGet("All")]
        public ActionResult<ViewRoleUser> Get()
        {
            return Ok(_roleUser);
        }

        // GET api/<RoleUserController>/5
        [HttpGet("{roleId}")]
        public ActionResult<ViewUser> Get(int roleId)
        {
            var roleUser = _roleUser;
            if (roleUser != null) return Ok(roleUser);
            _logger.LogError("使用者 ID 錯誤");
            return NotFound();
        }

        // POST api/<RoleUserController>
        [HttpPost("Add")]
        public ActionResult Post(int roleId, List<int> usersId)
        {
            _roleUser.Add(roleId, usersId);
            return Ok();
        }

        // DELETE api/<RoleUserController>/5
        [HttpDelete]
        public ActionResult Delete(int roleId, List<int> usersId)
        {
            _roleUser.Delete(roleId, usersId);
            _logger.LogError($"角色 {roleId} 刪除 {usersId}");
            return NoContent();
        }
    }
}
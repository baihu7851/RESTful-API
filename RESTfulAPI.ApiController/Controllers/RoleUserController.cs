using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Middleware.ViewModel;
using RESTfulAPI.Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public ActionResult<RoleUser> Get()
        {
            return Ok(_roleUser.View());
        }

        // GET api/<RoleUserController>/5
        [HttpGet("{roleId}")]
        public ActionResult<User> Get(int roleId)
        {
            var roleUser = _roleUser.View(roleId);
            if (roleUser != null) return Ok(roleUser);
            _logger.LogError("使用者 ID 錯誤");
            return NotFound();
        }

        // POST api/<RoleUserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoleUserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoleUserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
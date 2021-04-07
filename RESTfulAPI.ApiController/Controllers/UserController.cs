﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Model.Models;
using RESTfulAPI.Repository.Interfaces;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser userRepository)
        {
            _user = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet("All")]
        public IEnumerable<User> Get()
        {
            return _user.Users();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User user = _user.User(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
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
            return Ok();
        }
    }
}
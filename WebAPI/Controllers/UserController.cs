using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussines.Abstract;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [HttpGet("getall")]
        public List<User> GetAll()
        {
            return _userService.getList();
        }

        [HttpGet("getbyid/{userId}")]
        public User GetUserById(int userId)
        {
            return _userService.findById(userId);
        }
        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            _userService.save(user);
            return CreatedAtAction("GetUserById", new { userId = user.id }, user);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.findById(id);
            if (user==null)
            {
                return NotFound();
            }
            _userService.delete(user);
            return NoContent();
        }
        [HttpPut("update/{id}")]
        public IActionResult Update(int id,User user)
        {
            if (id!=user.id)
            {
                return BadRequest();
            }
            _userService.update(user);
            return NoContent();
        }


    }
}
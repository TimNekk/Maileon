using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Maileon.Models;
using Maileon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Maileon.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// User controller constructor
        /// </summary>
        /// <param name="userRepository">User Repository</param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <param name="limit">Amount of users</param>
        /// <param name="offset">Offset from beginning</param>
        /// <returns>Filtered users</returns>
        [HttpGet("get/all")]
        public ActionResult<IReadOnlyList<User>> GetUsers(
            int? limit = null, 
            int? offset = null)
        {
            try
            {
                var users = _userRepository.GetUsers(limit, offset);
                return Ok(users);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Gets user
        /// </summary>
        /// <param name="id">User email</param>
        /// <returns>User</returns>
        [HttpGet("get")]
        public ActionResult<User> GetUser([Required] string id)
        {
            User user = _userRepository.GetUser(id);
            return user is null ? NotFound() : Ok(user);
        }
        
        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="email">User id</param>
        /// <returns>Action Result</returns>
        [HttpPost("create")]
        public ActionResult CreateUser(
            [Required] string userName, 
            [Required] [EmailAddress (ErrorMessage = "Invalid email")] string email)
        {
            try
            {
                _userRepository.CreateUser(userName, email);
                return Ok("User created");
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
using Maileon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Maileon.Controllers
{
    /// <summary>
    /// Initialization controller
    /// Creates random data
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InitController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        /// <summary>
        /// Initialization controller constructor
        /// </summary>
        /// <param name="userRepository">User Repository</param>
        /// <param name="messageRepository">Message Repository</param>
        public InitController(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        /// <summary>
        /// Creates random users and messages
        /// </summary>
        /// <returns>Action Result</returns>
        [HttpPost]
        public ActionResult InitUsersAndMessages()
        {
            _userRepository.Initialize();
            _messageRepository.Initialize();
            return Ok("Random users and messages created successfully");
        }
    }
}
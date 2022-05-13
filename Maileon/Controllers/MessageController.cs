using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Maileon.Models;
using Maileon.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Maileon.Controllers
{
    /// <summary>
    /// Message controller
    /// Gets and sends messages
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Message controller constructor
        /// </summary>
        /// <param name="messageRepository">Message Repository</param>
        /// <param name="userRepository">User Repository</param>
        public MessageController(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }
        
        /// <summary>
        /// Gets messages filtered by sender and receiver
        /// </summary>
        /// <param name="senderId">Sender email</param>
        /// <param name="receiverId">Receiver email</param>
        /// <returns>Filtered messages</returns>
        [HttpGet("get")]
        public ActionResult<IReadOnlyList<MessageData>> GetMessages(
            [EmailAddress (ErrorMessage = "Invalid email")] [Required] string senderId, 
            [EmailAddress (ErrorMessage = "Invalid email")] [Required] string receiverId)
        {
            IReadOnlyList<MessageData> messages = _messageRepository.GetMessages(senderId, receiverId);
            return messages is null ? NotFound() : Ok(messages);
        }
        
        /// <summary>
        /// Gets messages filtered by sender
        /// </summary>
        /// <param name="senderId">Sender email</param>
        /// <returns>Filtered messages</returns>
        [HttpGet("get/sender")]
        public ActionResult<IReadOnlyList<MessageData>> GetMessagesBySender(
            [EmailAddress (ErrorMessage = "Invalid email")] [Required] string senderId)
        {
            IReadOnlyList<MessageData> messages = _messageRepository.GetMessages(senderId: senderId);
            return messages is null ? NotFound() : Ok(messages);
        }
        
        /// <summary>
        /// Gets messages filtered by receiver
        /// </summary>
        /// <param name="receiverId">Receiver email</param>
        /// <returns>Filtered messages</returns>
        [HttpGet("get/receiver")]
        public ActionResult<IReadOnlyList<MessageData>> GetMessagesByReceiver(
            [EmailAddress (ErrorMessage = "Invalid email")] [Required] string receiverId)
        {
            IReadOnlyList<MessageData> messages = _messageRepository.GetMessages(receiverId: receiverId);
            return messages is null ? NotFound() : Ok(messages);
        }
        
        /// <summary>
        /// Gets all messages
        /// </summary>
        /// <returns>Filtered messages</returns>
        [HttpGet("get/all")]
        public ActionResult<IReadOnlyList<MessageData>> GetAllMessages()
        {
            IReadOnlyList<MessageData> messages = _messageRepository.GetMessages();
            return messages is null ? NotFound() : Ok(messages);
        }
        
        /// <summary>
        /// Sends message
        /// </summary>
        /// <param name="subject">Message title</param>
        /// <param name="text">Message body</param>
        /// <param name="senderId">Sender</param>
        /// <param name="receiverId">Receiver</param>
        /// <returns>Action Result</returns>
        [HttpPost("send")]
        public ActionResult AddMessage(
            [Required] string subject, 
            [Required] string text, 
            [Required] string senderId, 
            [Required] string receiverId)
        {
            try
            {
                _messageRepository.CreateMessage(_userRepository.GetUsers(int.MaxValue, 0), subject, text, senderId, receiverId);
                return Ok("Message sent");
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
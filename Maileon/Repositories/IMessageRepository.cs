using System.Collections.Generic;
using Maileon.Models;

namespace Maileon.Repositories
{
    /// <summary>
    /// Interface of message repository
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Initializes messages
        /// </summary>
        void Initialize();

        /// <summary>
        /// Gets messages
        /// </summary>
        /// <param name="senderId">Sender email</param>
        /// <param name="receiverId">Receiver email</param>
        /// <returns>Filtered messages</returns>
        IReadOnlyList<MessageData> GetMessages(string senderId = null, string receiverId = null);
        
        /// <summary>
        /// Creates new message
        /// </summary>
        /// <param name="users">Available users</param>
        /// <param name="subject">Message title</param>
        /// <param name="text">Message body</param>
        /// <param name="senderId">Sender email</param>
        /// <param name="receiverId">Receiver email</param>
        void CreateMessage(IReadOnlyList<User> users, string subject, string text, string senderId, string receiverId);
    }
}
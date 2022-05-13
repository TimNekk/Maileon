using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Maileon.Models;
using Maileon.Services;

namespace Maileon.Repositories
{
    /// <inheritdoc />
    public class MessageRepository : IMessageRepository
    {
        private readonly string _messagesFilePath = "Data/messages.json";

        /// <inheritdoc />
        public void Initialize()
        {
            FilesHandler.WriteFile(_messagesFilePath, JsonSerializer.Serialize(DataManager.Instance.CreateRandomMessages()));
        }

        /// <inheritdoc />
        public IReadOnlyList<MessageData> GetMessages(string senderId = null, string receiverId = null)
        {
            string text = FilesHandler.ReadFile(_messagesFilePath);
            var messages = JsonSerializer.Deserialize<IReadOnlyList<MessageData>>(text);
            return DataManager.Instance.FilterMessages(messages, senderId, receiverId);
        }

        /// <inheritdoc />
        public void CreateMessage(IReadOnlyList<User> users, string subject, string text, string senderId, string receiverId)
        {
            DataManager.Instance.UpdateValidEmails(users);
            var messages = DataManager.Instance.AddMessage(GetMessages().ToList(), subject, text, senderId, receiverId);
            FilesHandler.WriteFile(_messagesFilePath, JsonSerializer.Serialize(messages));
        }
    }
}
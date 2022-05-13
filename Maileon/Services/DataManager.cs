using System;
using System.Collections.Generic;
using System.Linq;
using Maileon.Extensions;
using Maileon.Models;

namespace Maileon.Services
{
    /// <summary>
    /// Manages data generation and filtering
    /// </summary>
    public sealed class DataManager
    {
        private readonly Random _random = new();
        private IReadOnlyList<string> _validEmails;
        private static DataManager _instance;

        /// <summary>
        /// Singleton implementation 
        /// </summary>
        public static DataManager Instance => _instance ??= new DataManager();

        /// <summary>
        /// Empty private contractor from singleton
        /// </summary>
        private DataManager()
        {
        }

        /// <summary>
        /// Generates list of users with random data
        /// </summary>
        /// <returns>List of users</returns>
        public IReadOnlyList<User> CreateRandomUsers()
        {
            int amount = _random.Next(10, 20);
            var users = Enumerable
                .Range(0, amount)
                .Select(_ => new User(RandomName, RandomEmail))
                .OrderBy(user => user.Email)
                .ToList();
            _validEmails = users.Select(user => user.Email).ToList();
            return users;
        }
        
        /// <summary>
        /// Generates list of messages with random data
        /// </summary>
        /// <returns>List of messages</returns>
        public IReadOnlyList<MessageData> CreateRandomMessages()
        {
            int amount = _random.Next(25, 40);
            var messages = Enumerable
                .Range(0, amount)
                .Select(_ => new MessageData(RandomWord.Capitalize(), RandomText.Capitalize(), _validEmails.PickRandom(), _validEmails.PickRandom()))
                .ToList();
            return messages;
        }

        /// <summary>
        /// Filters list of messages
        /// </summary>
        /// <param name="messages">List of messages</param>
        /// <param name="senderId">Sender email</param>
        /// <param name="receiverId">Receiver email</param>
        /// <returns>List of filtered messages</returns>
        public IReadOnlyList<MessageData> FilterMessages(IReadOnlyList<MessageData> messages, string senderId, string receiverId)
        {
            if (senderId is not null) messages = messages?.Where(message => message.SenderId == senderId).ToList();
            if (receiverId is not null) messages = messages?.Where(message => message.ReceiverId == receiverId).ToList();
            return messages;
        }

        /// <summary>
        /// Adds new message to the list of messages
        /// </summary>
        /// <param name="messages">List of messages</param>
        /// <param name="subject">Message title</param>
        /// <param name="text">Message body</param>
        /// <param name="senderId">Sender email</param>
        /// <param name="receiverId">Receiver email</param>
        /// <returns>List of messages with added message</returns>
        /// <exception cref="ArgumentException">Unknown email</exception>
        public IReadOnlyList<MessageData> AddMessage(List<MessageData> messages, string subject, string text, string senderId, string receiverId)
        {
            if (!_validEmails.Contains(senderId) || !_validEmails.Contains(receiverId)) throw new ArgumentException("Unknown email");
            messages.Add(new MessageData(subject, text, senderId, receiverId));
            return messages;
        }
        
        /// <summary>
        /// Adds new user to the list of users
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="userName">User name</param>
        /// <param name="email">User email</param>
        /// <returns>List of users with added user</returns>
        /// <exception cref="ArgumentException"></exception>
        public IReadOnlyList<User> AddUser(List<User> users, string userName, string email)
        {
            UpdateValidEmails(users);
            if (_validEmails.Contains(email)) throw new ArgumentException("Email already exists");
            users.Add(new User(userName, email));
            return users.OrderBy(user => user.Email).ToList();
        }

        /// <summary>
        /// Updates list of valid emails
        /// </summary>
        /// <param name="users">List of users</param>
        public void UpdateValidEmails(IReadOnlyList<User> users)
        {
            _validEmails = users.Select(user => user.Email).ToList();
        }

        /// <summary>
        /// Trims and limits list of users
        /// </summary>
        /// <param name="users">List of users</param>
        /// <param name="limit">Amount of users</param>
        /// <param name="offset">Offset from beginning</param>
        /// <returns>Selected list of users</returns>
        /// <exception cref="ArgumentException">Limit of offset should be > 0</exception>
        public IReadOnlyList<User> SelectUsers(IReadOnlyList<User> users, int? limit, int? offset)
        {
            if (limit is not null)
            {
                if (limit <= 0) throw new ArgumentException("Limit should be > 0");
                users = users.Take((int) limit).ToList();
            }
            
            if (offset is not null)
            {
                if (offset < 0) throw new ArgumentException("Offset should be >= 0");
                users = users.Skip((int) offset).ToList();
            }

            return users;
        }

        private string RandomName => FilesHandler.Names.PickRandom();
        private string RandomEmail => FilesHandler.Words.PickRandom() + "_" + FilesHandler.Words.PickRandom() + _random.Next(1950, 2010) + "@gmail.com";
        private string RandomWord => FilesHandler.Words.PickRandom();
        private string RandomText => string.Join(" ", FilesHandler.Words.PickRandom(_random.Next(2, 7)));
    }
}
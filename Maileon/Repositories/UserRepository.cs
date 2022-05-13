using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Maileon.Models;
using Maileon.Services;

namespace Maileon.Repositories
{
    /// <inheritdoc />
    public class UserRepository : IUserRepository
    {
        private readonly string _usersFilePath = "Data/users.json";

        /// <inheritdoc />
        public void Initialize()
        {
            FilesHandler.WriteFile(_usersFilePath, JsonSerializer.Serialize(DataManager.Instance.CreateRandomUsers()));
        }

        /// <inheritdoc />
        public IReadOnlyList<User> GetUsers(int? limit = null, int? offset = null)
        {
            string text = FilesHandler.ReadFile(_usersFilePath);
            var users = JsonSerializer.Deserialize<IReadOnlyList<User>>(text);
            return DataManager.Instance.SelectUsers(users, limit, offset);
        }

        /// <inheritdoc />
        public User GetUser(string email)
        {
            string text = FilesHandler.ReadFile(_usersFilePath);
            return JsonSerializer
                .Deserialize<IReadOnlyList<User>>(text)?
                .FirstOrDefault(user => user.Email == email);
        }

        /// <inheritdoc />
        public void CreateUser(string userName, string email)
        {
            var users = DataManager.Instance.AddUser(GetUsers().ToList(), userName, email);
            FilesHandler.WriteFile(_usersFilePath, JsonSerializer.Serialize(users));
        }
    }
}
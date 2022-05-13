using System.Collections.Generic;
using Maileon.Models;

namespace Maileon.Repositories
{
    /// <summary>
    /// Interface of user repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Initializes users
        /// </summary>
        void Initialize();
        
        /// <summary>
        /// Gets users
        /// </summary>
        /// <param name="limit">Amount of users</param>
        /// <param name="offset">Offset from beginning</param>
        /// <returns>Filtered users</returns>
        IReadOnlyList<User> GetUsers(int? limit, int? offset);
        
        /// <summary>
        /// Gets user
        /// </summary>
        /// <param name="email">User id</param>
        /// <returns>User</returns>
        User GetUser(string email);
        
        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="userName">User name</param>
        /// <param name="email">User id</param>
        void CreateUser(string userName, string email);
    }
}
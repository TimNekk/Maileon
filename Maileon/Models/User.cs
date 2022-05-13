namespace Maileon.Models
{
    /// <summary>
    /// User model
    /// </summary>
    /// <param name="UserName">User name</param>
    /// <param name="Email">User id</param>
    public record User(string UserName, string Email);
}
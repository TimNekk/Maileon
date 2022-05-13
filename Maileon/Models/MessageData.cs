namespace Maileon.Models
{
    /// <summary>
    /// Message model
    /// </summary>
    /// <param name="Subject">Message title</param>
    /// <param name="Message">Message body</param>
    /// <param name="SenderId">Sender email</param>
    /// <param name="ReceiverId">Receiver email</param>
    public record MessageData(string Subject, string Message, string SenderId, string ReceiverId);
}
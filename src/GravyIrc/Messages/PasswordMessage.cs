using System.Collections.Generic;

namespace GravyIrc.Messages
{
    /// <summary>
    /// A message sent to identify the current user via password
    /// </summary>
    public class PasswordMessage : IrcMessage, IClientMessage
    {
        private readonly string password;

        /// <summary>
        /// Creates an outbound message instance
        /// </summary>
        /// <param name="password">Password to send</param>
        public PasswordMessage(string password)
        {
            this.password = password;
        }

        public IEnumerable<string> Tokens => new[] { "PASS", password };
    }
}

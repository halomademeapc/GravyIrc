using System.Collections.Generic;

namespace GravyIrc.Messages
{
    /// <summary>
    /// Message sent on connection providing details about the client
    /// </summary>
    public class UserMessage : IrcMessage, IClientMessage
    {
        /// <summary>
        /// Username to provide to IRC server
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Real name of client
        /// </summary>
        public string RealName { get; }

        /// <summary>
        /// Create an outbound user message
        /// </summary>
        /// <param name="userName">Username to use for connection</param>
        /// <param name="realName">Real name of user</param>
        public UserMessage(string userName, string realName)
        {
            UserName = userName;
            RealName = realName;
        }

        public IEnumerable<string> Tokens => new[] { "USER", UserName, "0", "-", RealName };
    }
}

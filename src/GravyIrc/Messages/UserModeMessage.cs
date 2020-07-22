using System.Collections.Generic;

namespace GravyIrc.Messages
{
    /// <summary>
    /// A message used to set the mode of a user
    /// </summary>
    /// <remarks>See https://tools.ietf.org/html/rfc1459#section-4.2.3.2 for available modes</remarks>
    public class UserModeMessage : IrcMessage, IClientMessage
    {
        private readonly string mode;
        private readonly string nick;

        /// <summary>
        /// Creates a new outbound message
        /// </summary>
        /// <param name="nick">Nick of user to set mode for</param>
        /// <param name="mode">Mode to set on user</param>
        public UserModeMessage(string nick, string mode)
        {
            this.mode = mode;
            this.nick = nick;
        }

        public IEnumerable<string> Tokens => new[] { "MODE", nick, mode };
    }
}

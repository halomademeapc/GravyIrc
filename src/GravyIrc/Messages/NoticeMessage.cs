using GravyIrc.Attributes;
using System.Collections.Generic;

namespace GravyIrc.Messages
{
    /// <summary>
    /// A notice messgage sent to a user or channel
    /// </summary>
    [ServerMessage("NOTICE")]
    public class NoticeMessage : IrcMessage, IServerMessage, IClientMessage, IChannelMessage
    {
        /// <summary>
        /// Nick of user who sent the notice
        /// </summary>
        public string From { get; }

        /// <summary>
        /// Nick or channel name notice was sent to
        /// </summary>
        public string Target { get; }

        /// <summary>
        /// Content of the message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Creates a new instance from an incoming server message
        /// </summary>
        /// <param name="parsedMessage">Incoming message</param>
        public NoticeMessage(ParsedIrcMessage parsedMessage)
        {
            From = parsedMessage.Prefix.From;
            Target = parsedMessage.Parameters[0];
            Message = parsedMessage.Trailing;
        }

        /// <summary>
        /// Creates and outbound message instance
        /// </summary>
        /// <param name="target">Nick or channel to notify</param>
        /// <param name="text">Content of notice</param>
        public NoticeMessage(string target, string text)
        {
            Target = target;
            Message = text;
        }

        public IEnumerable<string> Tokens => new[] { "NOTICE", Target, Message };

        public bool IsChannelMessage => Target.StartsWith("#");
    }
}

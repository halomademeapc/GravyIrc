using GravyIrc.Attributes;
using System.Collections.Generic;

namespace GravyIrc.Messages
{
    /// <summary>
    /// A private message sent to a channel or directly to a user
    /// </summary>
    [ServerMessage("PRIVMSG")]
    public class PrivateMessage : IrcMessage, IServerMessage, IClientMessage
    {
        /// <summary>
        /// Nick of the sender
        /// </summary>
        public string From { get; }

        /// <summary>
        /// Additional information about the sender
        /// </summary>
        public IrcPrefix Prefix { get; }

        /// <summary>
        /// Channel or nick message was sent to
        /// </summary>
        public string To { get; }

        /// <summary>
        /// Body of the message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Creates a new instance from an incoming server message
        /// </summary>
        /// <param name="parsedMessage">Incoming message</param>
        public PrivateMessage(ParsedIrcMessage parsedMessage)
        {
            From = parsedMessage.Prefix.From;
            Prefix = parsedMessage.Prefix;
            To = parsedMessage.Parameters[0];
            Message = parsedMessage.Trailing;
        }

        /// <summary>
        /// Creates a new instance for an outbound message
        /// </summary>
        /// <param name="target">Channel or nick to send to</param>
        /// <param name="text">Message content</param>
        public PrivateMessage(string target, string text)
        {
            To = target;
            Message = text;
        }

        public void TriggerEvent(EventHub eventHub)
        {
            eventHub.TriggerEvent(new IrcMessageEventArgs<PrivateMessage>(this));
        }

        /// <summary>
        /// Indicates if the message was sent to a channel, not a person
        /// </summary>
        public bool IsChannelMessage => To.StartsWith("#");

        public IEnumerable<string> Tokens => new[] { "PRIVMSG", To, Message };
    }
}

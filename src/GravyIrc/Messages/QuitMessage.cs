using GravyIrc.Attributes;
using System.Collections.Generic;

namespace GravyIrc.Messages
{
    /// <summary>
    /// Indicates that a user has disconnected from the server or notifying that the client is disconnecting
    /// </summary>
    [ServerMessage("QUIT")]
    public class QuitMessage : IrcMessage, IServerMessage, IClientMessage
    {
        /// <summary>
        /// Nick of user disconnecting
        /// </summary>
        public string Nick { get; }

        /// <summary>
        /// Accompanying message for quit reason
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Creates a new instance from an incoming server message
        /// </summary>
        /// <param name="parsedMessage">Incoming message</param>
        public QuitMessage(ParsedIrcMessage parsedMessage)
        {
            Nick = parsedMessage.Prefix.From;
            Message = parsedMessage.Trailing;
        }

        /// <summary>
        /// Create a new outbound quit message
        /// </summary>
        /// <param name="message">Reason for quitting</param>
        public QuitMessage(string message)
        {
            Message = message;
        }

        public IEnumerable<string> Tokens => new[] { "QUIT", Message };

        public void TriggerEvent(EventHub eventHub)
        {
            eventHub.Trigger(new IrcMessageEventArgs<QuitMessage>(this));
        }
    }
}

using GravyIrc.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace GravyIrc.Messages
{
    /// <summary>
    /// A message received when a user joins an active channel or used to have the client connect to a new channel
    /// </summary>
    [ServerMessage("JOIN")]
    public class JoinMessage : IrcMessage, IServerMessage, IClientMessage
    {
        private readonly string channels;
        private readonly string keys = "";

        /// <summary>
        /// Nick of joining user
        /// </summary>
        public string Nick { get; }

        /// <summary>
        /// Channel that user joined
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Creates a new instance from an incoming server message
        /// </summary>
        /// <param name="parsedMessage">Incoming message</param>
        public JoinMessage(ParsedIrcMessage parsedMessage)
        {
            Nick = parsedMessage.Prefix.From;
            Channel = parsedMessage.Parameters[0];
        }

        /// <summary>
        /// Creates a new instance for an outbound message
        /// </summary>
        /// <param name="channels">Channels to join, comma-separated</param>
        /// <param name="keys">Passwords for channels, comma-separated</param>
        public JoinMessage(string channels, string keys = "")
        {
            this.channels = channels;
            this.keys = keys;
        }

        /// <summary>
        /// Creates a new instance for an outbound message
        /// </summary>
        /// <param name="channels">Channels to join</param>
        public JoinMessage(IEnumerable<string> channels)
        {
            this.channels = string.Join(",", channels);
        }

        /// <summary>
        /// Creates a new instance for an outbound message
        /// </summary>
        /// <param name="channels">Channels to join</param>
        public JoinMessage(params string[] channels) => new JoinMessage(channels.AsEnumerable());

        /// <summary>
        /// Creates a new instance for an outbound message
        /// </summary>
        /// <param name="channels">Channels to join, in name-password pairs</param>
        public JoinMessage(IEnumerable<(string, string)> channels)
        {
            this.channels = string.Join(",", channels.Select(c => c.Item1));
            keys = string.Join(",", channels.Select(c => c.Item2 ?? string.Empty));
        }

        public IEnumerable<string> Tokens => new[] { "JOIN", channels, keys };

        public void TriggerEvent(EventHub eventHub)
        {
            eventHub.Trigger(new IrcMessageEventArgs<JoinMessage>(this));
        }
    }
}

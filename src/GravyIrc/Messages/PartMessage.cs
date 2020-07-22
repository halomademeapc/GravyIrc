using GravyIrc.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace GravyIrc.Messages
{
    /// <summary>
    /// A message indicating when a user leaves a channel or used to leave a channel
    /// </summary>
    [ServerMessage("PART")]
    public class PartMessage : IrcMessage, IServerMessage, IClientMessage
    {
        private readonly string channels;

        /// <summary>
        /// Nick of user that left
        /// </summary>
        public string Nick { get; }

        /// <summary>
        /// Channel that the user left
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Creates a new instance from an incoming server message
        /// </summary>
        /// <param name="parsedMessage">Incoming message</param>
        public PartMessage(ParsedIrcMessage parsedMessage)
        {
            Nick = parsedMessage.Prefix.From;
            Channel = parsedMessage.Parameters[0];
        }

        /// <summary>
        /// Create a new request to leave a channel
        /// </summary>
        /// <param name="channels">List of channels to leave, comma-separated</param>
        public PartMessage(string channels)
        {
            this.channels = channels;
        }

        /// <summary>
        /// Create a new request to leave a channel
        /// </summary>
        /// <param name="channels">List of channels to leave</param>
        public PartMessage(IEnumerable<string> channels)
        {
            this.channels = string.Join(",", channels);
        }

        /// <summary>
        /// Creates a new instance for an outbound message
        /// </summary>
        /// <param name="channels">Channels to leave</param>
        public PartMessage(params string[] channels) => new PartMessage(channels.AsEnumerable());

        public IEnumerable<string> Tokens => new[] { "PART", channels };

        public void TriggerEvent(EventHub eventHub)
        {
            eventHub.Trigger(new IrcMessageEventArgs<PartMessage>(this));
        }
    }
}

using GravyIrc.Attributes;

namespace GravyIrc.Messages
{
    /// <summary>
    /// Incoming message used to verify connection state
    /// </summary>
    [ServerMessage("PING")]
    public class PingMessage : IrcMessage, IServerMessage
    {
        /// <summary>
        /// Target to send pong response message to
        /// </summary>
        public string Target { get; }

        /// <summary>
        /// Creates a new instance from an incoming server message
        /// </summary>
        /// <param name="parsedMessage">Incoming message</param>
        public PingMessage(ParsedIrcMessage parsedMessage)
        {
            Target = parsedMessage.Trailing ?? parsedMessage.Parameters[0];
        }

        public void TriggerEvent(EventHub eventHub)
        {
            eventHub.OnPing(new IrcMessageEventArgs<PingMessage>(this));
        }
    }
}

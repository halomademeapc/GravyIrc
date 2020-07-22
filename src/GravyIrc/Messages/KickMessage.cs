using GravyIrc.Attributes;

namespace GravyIrc.Messages
{
    /// <summary>
    /// A message received when a user is kicked from a channel
    /// </summary>
    [ServerMessage("KICK")]
    public class KickMessage : IrcMessage, IServerMessage
    {
        /// <summary>
        /// Name of the channel that the user was kicked from
        /// </summary>
        public string Channel { get; }

        /// <summary>
        /// Nick of the user that was kicked
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// Nick of the user that performed the kick
        /// </summary>
        public string KickedBy { get; set; }

        /// <summary>
        /// Creates a new instance from an incoming server message
        /// </summary>
        /// <param name="parsedMessage">Incoming message</param>
        public KickMessage(ParsedIrcMessage parsedMessage)
        {
            Channel = parsedMessage.Parameters[0];
            Nick = parsedMessage.Parameters[1];
            KickedBy = parsedMessage.Parameters[2];
        }

        public void TriggerEvent(EventHub eventHub)
        {
            eventHub.OnKick(new IrcMessageEventArgs<KickMessage>(this));
        }
    }
}

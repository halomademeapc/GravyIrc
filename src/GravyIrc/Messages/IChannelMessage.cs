namespace GravyIrc.Messages
{
    /// <summary>
    /// Represents a message that the client needs to be in a channel for before sending
    /// </summary>
    public interface IChannelMessage : IClientMessage
    {
        /// <summary>
        /// Indicates if message is being sent to a channel
        /// </summary>
        bool IsChannelMessage { get; }

        /// <summary>
        /// Target channel/person for the message
        /// </summary>
        string Target { get; }
    }
}

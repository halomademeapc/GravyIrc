namespace GravyIrc.Messages
{
    /// <summary>
    /// Represents an incoming message from the server
    /// </summary>
    public interface IServerMessage
    {
        /// <summary>
        /// Action to take on when a message is received
        /// </summary>
        /// <param name="eventHub">The events hub for the IRC client</param>
        void TriggerEvent(EventHub eventHub);
    }
}

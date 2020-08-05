using System;

namespace GravyIrc.Messages
{
    /// <summary>
    /// Represents an incoming message from the server
    /// </summary>
    public interface IServerMessage
    {
        /// <summary>
        /// Date when the message came in
        /// </summary>

        DateTime DateReceived { get; }
    }
}

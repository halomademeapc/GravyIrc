using System.Collections.Generic;

namespace GravyIrc.Messages
{
    /// <summary>
    /// Represents an outbound message being sent by the client
    /// </summary>
    public interface IClientMessage
    {
        /// <summary>
        /// A list of tokens to be serialized into the message content
        /// </summary>
        IEnumerable<string> Tokens { get; }
    }
}

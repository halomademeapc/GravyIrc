using System.Collections.Generic;

namespace GravyIrc.Messages
{
    /// <summary>
    /// A message sent by the client in response to a ping request
    /// </summary>
    public class PongMessage : IrcMessage, IClientMessage
    {
        /// <summary>
        /// Destination for pong response
        /// </summary>
        /// <remarks>Target is specified in incoming ping request</remarks>
        public string Target { get; }

        /// <summary>
        /// Create an outbound message
        /// </summary>
        /// <param name="target">Destination for response</param>
        public PongMessage(string target)
        {
            Target = target;
        }

        public IEnumerable<string> Tokens => new[] { "PONG", Target };
    }
}

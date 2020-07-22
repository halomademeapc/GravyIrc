using System;

namespace GravyIrc.Attributes
{
    /// <summary>
    /// Decorates an IServerMessage
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ServerMessageAttribute : Attribute
    {
        /// <summary>
        /// IRC Command
        /// </summary>
        /// <remarks>
        /// Available commands are specified in the IRC spec: https://tools.ietf.org/html/rfc1459
        /// </remarks>
        public string Command { get; }

        /// <summary>
        /// Decorate an IServerMessage
        /// </summary>
        /// <param name="command">IRC command</param>
        public ServerMessageAttribute(string command)
        {
            Command = command;
        }
    }
}

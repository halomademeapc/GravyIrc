using GravyIrc.Attributes;
using GravyIrc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GravyIrc.Messages
{
    public abstract class IrcMessage
    {
        private static readonly Dictionary<string, Type> ServerMessageTypes;

        static IrcMessage()
        {
            var interfaceType = typeof(IServerMessage);
            ServerMessageTypes = typeof(IrcMessage).Assembly
                .GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t))
                .Where(t => t.HasCommand())
                .ToDictionary(t => t.GetCommand(), t => t);
        }

        /// <summary>
        /// Register an additional server message type
        /// </summary>
        /// <remarks>Used to extend built-in message types</remarks>
        /// <typeparam name="TMessage">Type of message to add</typeparam>
        public static void RegisterServerMessageType<TMessage>() where TMessage : IServerMessage => RegisterServerMessageType(typeof(TMessage));

        /// <summary>
        /// Register an additional server message type
        /// </summary>
        /// <remarks>Used to extend built-in message types</remarks>
        /// <typeparam name="TMessage">Type of message to add</typeparam>
        /// <param name="command">IRC command to associate message type with</param>
        public static void RegisterServerMessageType<TMessage>(string command) where TMessage : IServerMessage => RegisterServerMessageType(typeof(TMessage), command);

        /// <summary>
        /// Register an additional server message type
        /// </summary>
        /// <remarks>Used to extend built-in message types</remarks>
        /// <param name="messageType">Type of message to add</param>
        public static void RegisterServerMessageType(Type messageType)
        {
            if (!messageType.HasCommand())
                throw new ArgumentException($"{messageType.FullName} must be annotated with {typeof(ServerMessageAttribute).FullName} or command must be passed to registration call.");

            RegisterServerMessageType(messageType, messageType.GetCommand());
        }

        /// <summary>
        /// Register an additional server message type
        /// </summary>
        /// <remarks>Used to extend built-in message types</remarks>
        /// <param name="messageType">Type of message to add</param>
        /// <param name="command">IRC command to associate message type with</param>
        public static void RegisterServerMessageType(Type messageType, string command)
        {
            var interfaceType = typeof(IServerMessage);
            if (!interfaceType.IsAssignableFrom(messageType))
                throw new ArgumentException($"{messageType.FullName} must implement {interfaceType.FullName}");

            ServerMessageTypes[command] = messageType;
        }

        public static IServerMessage Create(ParsedIrcMessage parsedMessage)
        {
            var messageType = ServerMessageTypes.ContainsKey(parsedMessage.Command)
                ? ServerMessageTypes[parsedMessage.Command]
                : null;

            return messageType != null
                ? Activator.CreateInstance(messageType, new object[] { parsedMessage }) as IServerMessage
                : null;
        }

        public override string ToString()
        {
            var clientMessage = this as IClientMessage;

            if (clientMessage == null)
            {
                return base.ToString();
            }

            var tokens = clientMessage.Tokens.ToArray();

            if (!tokens.Any())
            {
                return string.Empty;
            }

            return string.Join(" ", tokens.Select(t => t == tokens.LastOrDefault() && t.Contains(' ') ? $":{t}" : t)).Trim();
        }

        /// <summary>
        /// Date when message came in or was spawned
        /// </summary>
        public DateTime DateReceived { get; private set; } = DateTime.Now;
    }
}

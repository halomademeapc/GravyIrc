using GravyIrc.Messages;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GravyIrc
{
    /// <summary>
    /// Hub for all events on IRC client that can be subscribed to
    /// </summary>
    public class EventHub
    {
        private readonly Client client;
        private readonly Dictionary<Type, object> eventHandlers = new Dictionary<Type, object>();

        internal EventHub(Client client)
        {
            this.client = client;
        }

        private ServerMessageEventHandler<TMessage> GetHandler<TMessage>() where TMessage : IrcMessage, IServerMessage
        {
            var messageType = typeof(TMessage);
            if (eventHandlers.ContainsKey(messageType))
            {
                return (eventHandlers[messageType] as ServerMessageEventHandler<TMessage>);
            }

            var handler = new ServerMessageEventHandler<TMessage>(client);
            eventHandlers[messageType] = handler;
            return handler;
        }

        /// <summary>
        /// Subscribe to incoming messages
        /// </summary>
        /// <typeparam name="TMessage">Type of message to subscribe to</typeparam>
        /// <param name="handler">Action to take when message is received</param>
        public void Subscribe<TMessage>(IrcMessageEventHandler<TMessage> handler) where TMessage : IrcMessage, IServerMessage
        {
            var @event = GetHandler<TMessage>();
            @event.Received += handler;
        }

        /// <summary>
        /// Unsubscribe from incoming messages
        /// </summary>
        /// <typeparam name="TMessage">Type of message to remove subscription on</typeparam>
        /// <param name="handler">Action to remove</param>
        public void Unsubscribe<TMessage>(IrcMessageEventHandler<TMessage> handler) where TMessage : IrcMessage, IServerMessage
        {
            var @event = GetHandler<TMessage>();
            @event.Received -= handler;
        }

        /// <summary>
        /// Trigger event handlers for an incoming message
        /// </summary>
        /// <typeparam name="TMessage">Type of message</typeparam>
        /// <param name="args">Information about incoming message</param>
        public void Trigger<TMessage>(IrcMessageEventArgs<TMessage> args) where TMessage : IrcMessage, IServerMessage
        {
            GetHandler<TMessage>()?.OnReceived(args);
        }

        /// <summary>
        /// Trigger event handlers for an incoming message
        /// </summary>
        /// <typeparam name="TMessage">Type of message</typeparam>
        /// <param name="message">Incoming message</param>
        public void Trigger<TMessage>(TMessage message) where TMessage : IrcMessage, IServerMessage
        {
            Trigger(new IrcMessageEventArgs<TMessage>(message));
        }

        /// <summary>
        /// Trigger event handlers for an incoming message
        /// </summary>
        /// <param name="message">Incoming message from IRC server</param>
        /// <remarks>Automatically determines types using reflection</remarks>
        public void Trigger(IServerMessage message)
        {
            if (message == null)
                return;

            var messageType = message.GetType();

            var getHandlerMethod = typeof(EventHub).GetMethod(nameof(GetHandler), BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(messageType);
            var handler = getHandlerMethod.Invoke(this, null);

            var genericArgsType = typeof(IrcMessageEventArgs<>).MakeGenericType(messageType);
            var args = Activator.CreateInstance(genericArgsType, message);

            var handlerType = typeof(ServerMessageEventHandler<>).MakeGenericType(messageType);

            var receivedEvent = handlerType.GetMethod("OnReceived", BindingFlags.NonPublic | BindingFlags.Instance);
            receivedEvent.Invoke(handler, new object[] { args });
        }

        private class ServerMessageEventHandler<TMessage> where TMessage : IrcMessage, IServerMessage
        {
            private readonly Client client;

            internal ServerMessageEventHandler(Client client)
            {
                this.client = client;
            }

            public event IrcMessageEventHandler<TMessage> Received;
            internal void OnReceived(IrcMessageEventArgs<TMessage> args)
            {
                Received?.Invoke(client, args);
            }
        }
    }
}

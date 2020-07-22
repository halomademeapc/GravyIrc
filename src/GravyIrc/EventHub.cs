using GravyIrc.Messages;
using System;
using System.Collections.Generic;

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

        public void AddEventListener<TMessage>(Action<Client, IrcMessageEventArgs<TMessage>> handler) where TMessage : IrcMessage, IServerMessage
        {
            var @event = GetHandler<TMessage>();
            @event.Received += (client, args) => handler(client, args);
        }

        public void TriggerEvent<TMessage>(IrcMessageEventArgs<TMessage> args) where TMessage : IrcMessage, IServerMessage
        {
            GetHandler<TMessage>()?.Received?.Invoke(client, args);
        }

        private class ServerMessageEventHandler<TMessage> where TMessage : IrcMessage, IServerMessage
        {
            private readonly Client client;

            internal ServerMessageEventHandler(Client client)
            {
                this.client = client;
            }

            public IrcMessageEventHandler<TMessage> Received = (sender, args) => { };
            internal void OnReceived(IrcMessageEventArgs<TMessage> args)
            {
                Received?.Invoke(client, args);
            }
        }
    }
}

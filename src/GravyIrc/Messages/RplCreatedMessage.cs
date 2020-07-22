﻿using GravyIrc.Attributes;

namespace GravyIrc.Messages
{
    [ServerMessage("003")]
    public class RplCreatedMessage : IrcMessage, IServerMessage
    {
        public string Text { get; }

        public RplCreatedMessage(ParsedIrcMessage parsedMessage)
        {
            Text = parsedMessage.Trailing;
        }

        public void TriggerEvent(EventHub eventHub)
        {
            eventHub.TriggerEvent(new IrcMessageEventArgs<RplCreatedMessage>(this));
        }
    }
}

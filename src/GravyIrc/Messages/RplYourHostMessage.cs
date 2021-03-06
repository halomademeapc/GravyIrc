﻿using GravyIrc.Attributes;

namespace GravyIrc.Messages
{
    [ServerMessage("002")]
    public class RplYourHostMessage : IrcMessage, IServerMessage
    {
        public string Text { get; }

        public RplYourHostMessage(ParsedIrcMessage parsedMessage)
        {
            Text = parsedMessage.Trailing;
        }
    }
}

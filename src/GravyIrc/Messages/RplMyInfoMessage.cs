﻿using GravyIrc.Attributes;

namespace GravyIrc.Messages
{
    [ServerMessage("004")]
    public class RplMyInfoMessage : IrcMessage, IServerMessage
    {
        public string[] Parameters { get; }

        public RplMyInfoMessage(ParsedIrcMessage parsedMessage)
        {
            Parameters = parsedMessage.Parameters;
        }
    }
}

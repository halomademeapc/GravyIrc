﻿using GravyIrc.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace GravyIrc.Messages
{
    [ServerMessage("353")]
    public class RplNamReplyMessage : IrcMessage, IServerMessage
    {
        public string Channel { get; }
        public Dictionary<string, string> Nicks { get; }

        private static char[] userStatuses = new[] { '@', '+' };

        public RplNamReplyMessage(ParsedIrcMessage parsedMessage)
        {
            Nicks = new Dictionary<string, string>();

            Channel = parsedMessage.Parameters[2];
            var nicks = parsedMessage.Trailing.Split(' ');

            foreach (var nick in nicks)
            {
                if (!string.IsNullOrWhiteSpace(nick) && userStatuses.Contains(nick[0]))
                {
                    Nicks.Add(nick.Substring(1), nick.Substring(0, 1));
                }
                else
                {
                    Nicks.Add(nick, string.Empty);
                }
            }
        }
    }
}

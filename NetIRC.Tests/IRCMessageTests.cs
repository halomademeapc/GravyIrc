﻿using Xunit;

namespace NetIRC.Tests
{
    public class IRCMessageTests
    {
        [Fact]
        public void CanSetRawData()
        {
            var rawData = "raw";
            var ircMessage = new IRCMessage(rawData);

            Assert.Equal(rawData, ircMessage.Raw);
        }

        [Fact]
        public void CanParseThePrefix()
        {
            var prefix = "prefix";
            var ircMessage = new IRCMessage($":{prefix} ");

            Assert.Equal(prefix, ircMessage.Prefix.From);
        }

        [Fact]
        public void CanParseCommandWithSingleParameter()
        {
            var command = "PING";
            var parameter = "tolsun.oulu.fi";
            var ircMessage = new IRCMessage($"{command} {parameter}");
            Assert.Equal(command, ircMessage.Command);
            Assert.Equal(parameter, ircMessage.Parameters[0]);
        }

        [Fact]
        public void CanParseCommandWithTwoParameters()
        {
            var command = "MODE";
            var nick = "Angel";
            var mode = "+i";
            var ircMessage = new IRCMessage($"{command} {nick} {mode}");
            Assert.Equal(command, ircMessage.Command);
            Assert.Equal(nick, ircMessage.Parameters[0]);
            Assert.Equal(mode, ircMessage.Parameters[1]);
        }

        [Fact]
        public void CanParseCommandWithTrailing()
        {
            var prefix = "Angel!wings@irc.org";
            var command = "PRIVMSG";
            var target = "Wiz";
            var text = "Are you receiving this message ?";
            var ircMessage = new IRCMessage($":{prefix} {command} {target} :{text}");
            Assert.Equal(prefix, ircMessage.Prefix.Raw);
            Assert.Equal(command, ircMessage.Command);
            Assert.Equal(target, ircMessage.Parameters[0]);
            Assert.Equal(text, ircMessage.Trailing);
        }

        [Fact]
        public void GoodToStringOverride()
        {
            var prefix = "Angel!wings@irc.org";
            var command = "PRIVMSG";
            var target = "Wiz";
            var text = "Are you receiving this message ?";
            var ircMessage = new IRCMessage($":{prefix} {command} {target} :{text}");
            Assert.Equal($"Prefix: {prefix}, Command: {command}, Params: {{ Wiz }}, Trailing: {text}", ircMessage.ToString());
        }
    }
}
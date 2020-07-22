# Sending messages

Messages can be sent asynchronously via [IrcClient](/api/GravyIrc.IrcClient.html).  You can send any implementation of [IClientMessage](/api/GravyIrc.Messages.IClientMessage.html).  See [GravyIrc.Messages](/api/GravyIrc.Messages.html) for included types.  

Most normal (channel or direct) messages sent over IRC will be a [PrivateMessage](/api/GravyIrc.Messages.PrivateMessage.html).

```csharp
var message = new PrivateMessage("#potato", "Hello, I'm a bot!");
await client.SendAsync(message);
```

You can use a [JoinMessage](/api/GravyIrc.Messages.JoinMessage.html) to join channels.

```csharp
var channelName = "#potato";
if (!client.Channels.Any(c => c.Name == channelName))
    await client.SendAsync(new JoinMessage(channelName));
```
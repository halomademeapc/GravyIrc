# Listening for incoming messages

The [IrcClient](/api/GravyIrc.IrcClient.html) exposes an [EventHub](/api/GravyIrc.EventHub.html) that can be used to subscribe to incoming messages. 

```csharp
client.Subscribe<PrivateMessage>((client, args) => Console.WriteLine(args.IrcMessage.Message));
```

If you want to remove an event, you can call the `Unsubscribe` method.

```csharp
MyCallback(IrcClient client, IrcMessageEventArgs<PrivateMessage> args)
{
    Console.WriteLine(args.IrcMessage.Message);
}

client.Subscribe<PrivateMessage>(MyCallback);
client.Unsubscribe<PrivateMessage>(MyCallback);
```
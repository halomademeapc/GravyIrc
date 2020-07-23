# Adding a server message type

If the type of message you want to listen for isn't in GravyIrc yet, open a pull request!  Seriously, please open a pull request :)

GravyIrc also lets you register your own server message types at runtime for anything it doesn't handle.  To use this, you'll need to first register the type so that incoming messages will be cast to it when necessary.  After that, you should be able to subscribe to events for it like any of the built-in message types. 

## Creating a custom server message type

Let's start by creating our message type.  This class will need to extend [IrcMessage](/api/GravyIrc.Messages.IrcMessage.html) and implement [IServerMessage](/api/GravyIrc.Messages.IServerMessage.html).  Crucially, it must contain a constructor that accepts a [ParsedIrcMessage](/api/GravyIrc.ParsedIrcMessage.html).  This functionality will probably be moved to an initializer method in a future release so that it can be appropriately reflected in the the [IServerMessage](/api/GravyIrc.Messages.IServerMessage.html) interface.  Optionally, you can decorate the class with a [ServerMessageAttribute](/api/GravyIrc.Attributes.ServerMessageAttribute.html) to specify the IRC command associated with it.

```csharp
[ServerMessage("POTATO")]
public class PotatoMessage : IrcMessage, IServerMessage
{
    public string Spud { get; private set; }
    public bool IsBaked { get; private set; }

    public PotatoMessage(ParsedIrcMessage parsedMessage)
    {
        Spud = parsedMessage.Parameters[0];
        IsBaked = parsedMessage.Parameters[1] == "BAKED";
    }
}
```

## Registering the custom type

Once you have your custom type ready, you need to register it so that events will be triggered for it.  You can do this by calling the static `RegisterServerMessageType` method on [IrcMessage](/api/GravyIrc.Messages.IrcMessage.html).  There are several overloads of this provided for your convenience.  If you try to register a message type for a command that is already associated, the previous type will be overridden with your new binding.  

### Register using attribute

If you decorated your message class with [ServerMessageAttribute](/api/GravyIrc.Attributes.ServerMessageAttribute.html), the command specified will automatically be fetched on registration.  You can register your type one of these two ways:

```csharp
IrcMessage.RegisterServerMessageType<PotatoMessage>();
// or
IrcMessage.RegisterServerMessageType(typeof(PotatoMessage));
```

### Register specifying command

If you did not decorate your message class, you will need to specify the IRC command while registering.  This is similar to registering with the annotation.

```csharp
IrcMessage.RegisterServerMessageType<PotatoMessage>("POTATO");
// or
IrcMessage.RegisterServerMessageType(typeof(PotatoMessage), "POTATO");
```

## Subscribing to events

Once the message type is registered, you should be able to subscribe to events for when they come in just like with any built-in message type.

```csharp
ircClient.EventHub.Subscribe<PotatoMessage>((client, args) => Console.WriteLine($"Got a {args.IrcMessage.IsBaked ? "baked" : "raw"} potato!"))
```

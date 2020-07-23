# The Event Hub

## Methodology
The [EventHub](/api/GravyIrc.EventHub.html) has been substantially rewritten from its origins in NetIRC but provides similar functionality.  

### The Problem
The original hub had an event for each implementation of [IServerMessage](/api/GravyIrc.Messages.IServerMessage.html) that was present in the library.  This had the drawbacks of limiting available events to only messages defined within the NetIRC library and added the need for a lot of additional, mostly copy-pasted code to be added for each type of message that is added.  

### My Approach
To avoid this shortcomings, I changed the event hub to dynamically create and provide event handlers as needed.  This cuts back on the amount of code that must be added for each type of message and means that users of the library can extend its in-built message types with additional ones at runtime.  My background is in web development with MVC so I'm not especially familiar with C# events.  If you have an idea for a nicer way of handling this, please let me know.  

#### Subscribing to an Incoming Message Event
You can add actions to be taken when a message of a certain type comes in with lambdas or method references.
```csharp
myClient.EventHub.Subscribe<PrivateMessage>((client, args) => Console.WriteLine(args.IrcMessage.Message));
```

### Registering Additional Message Types
If the type of message you want to listen for isn't in GravyIrc yet, open a pull request!  Please open a pull request :)

If you want to extend things on your end, I've made it easy to do that was well.  You'll need a class that extends [IrcMessage](/api/GravyIrc.Messages.IrcMessage.html) and implements [IServerMessage](/api/GravyIrc.Messages.IServerMessage.html). You can decorate it with a [ServerMessageAttribute](/api/GravyIrc.Attributes.ServerMessageAttribute.html) to specify which IRC command it should be bound to.
```csharp
[ServerMessage("POTATO")]
public class PotatoMessage : IrcMessage, IServerMessage
{
    ...
}
```

Once you have your class ready, register it to [IrcMessage](/api/GravyIrc.Messages.IrcMessage.html) so that incoming messages with that command will be converted to your type and you'll be able to subscribe to events for it.
```csharp
IrcMessage.RegisterServerMessageType<PotatoMessage>();
myClient.EventHub.Subscribe<PotatoMessage>((client, args) => Console.WriteLine("Potatoes are cool!"));
```

After that you should be good to go!

If you don't want to use the annotation to specify command, overloads are provided on IrcMessage.
```csharp
IrcMessage.RegisterServerMessageType<CarrotMessage>("CARROT");
```
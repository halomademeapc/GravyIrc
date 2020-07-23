# Adding a client message type

If you want to send a type of message that isn't included in GravyIrc, you can make your own type.  Please consider opening a pull request to share your work with everyone else if you do this. ;)

## Creating a custom client message type

The process for adding a custom outbound message type is simpler than that for incoming messages.  Simply make a class extending [IrcMessage](/api/GravyIrc.Messages.IrcMessage.html) and implementing [IClientMessage](/api/GravyIrc.Messages.IClientMessage.html).

You'll need to create a getter called `Tokens` that returns an `IEnumerable<string>` to implement the interface.  This should be a list of strings starting with the IRC command to send and followed by any arguments. 

```csharp
public class UserModeMessage : IrcMessage, IClientMessage
{
    private readonly string mode;
    private readonly string nick;

    public UserModeMessage(string nick, string mode)
    {
        this.mode = mode;
        this.nick = nick;
    }

    public IEnumerable<string> Tokens => new[] { "MODE", nick, mode };
}
```

## Send it

Once you have your custom type, that's about all you need to do; it should be ready to ship for a low, flat rate.  

```csharp
var banHammer = new UserModeMessage("someNick", "+b");
await ircClient.SendAsync(banHammer);
```

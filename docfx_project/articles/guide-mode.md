# Set the user's mode

If you're building a bot on GravyIrc, many IRC server admins will ask you to have the bot set mode `+B` to identify itself after connecting.  This can be hooked onto events for the message type [RplWelcomeMessage](/api/GravyIrc.Messages.RplWelcomeMessage.html) to take effect after connection.

```csharp
client.EventHub.Subscribe<RplWelcomeMessage>(Client_OnRegistered);

private async void Client_OnRegistered(object sender, EventArgs e)
{
    await client.SendAsync(new UserModeMessage(config.Nick, "+B"));
}
```
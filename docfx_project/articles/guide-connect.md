# Connect to an IRC server

To connect to an IRC server, you'll need to provide a [User](/api/GravyIrc.User.html) and [IConnection](/api/GravyIrc.Connection.IConnection.html) to instantiate a new [IrcClient](/api/GravyIrc.IrcClient.html).

The user object can be created with a nick and an optional real name.

```csharp
var user = new User("myBot", "Fun Bot");
```

For the connection, you'll want to use a [TcpClientConnection](/api/GravyIrc.Connection.TcpClientConnection.html).

```csharp
var conn = new TcpClientConnection();
```

Once you have those ready, you can create your client and connect.  You'll need to specify and address and port when connecting.

```csharp
var client = new IrcClient(user, conn);
await client.ConnectAsync("irc.mysite.com", 6667);
```

The client will automatically send `PASS`, `NICK`, and `USER` messages to the server once connected. 
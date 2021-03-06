<img src="https://raw.githubusercontent.com/halomademeapc/GravyIrc/master/docfx_project/logo.png" alt="GravyIrc Logo" width="128"/>

# GravyIrc
[![Build Status](https://img.shields.io/travis/halomademeapc/GravyIrc?style=flat-square)](https://travis-ci.org/github/halomademeapc/GravyIrc) [![GitHub issues](https://img.shields.io/github/issues/halomademeapc/GravyIrc?style=flat-square)](https://github.com/halomademeapc/GravyIrc/issues) [![Nuget](https://img.shields.io/nuget/dt/GravyIrc?style=flat-square)](https://www.nuget.org/packages/GravyIrc/) [![Nuget](https://img.shields.io/nuget/v/GravyIrc?style=flat-square)](https://www.nuget.org/packages/GravyIrc/)

IRC client library targetting .NET Standard 2.0.  Built off of the work done in [fredimachado/NetIRC](https://github.com/fredimachado/NetIRC).  

## Goals
* Up-to-date builds on NuGet
* More complete coverage of IRC message types
* Improved documentation

**See the main documentation [here](https://gravyirc.halomademeapc.com)**

# Quick Start
```bash
dotnet add package GravyIrc
```

```csharp
public async void TestClient() {
    var user = new User(config.Nick, config.Identity);
    var client = new IrcClient(user, new TcpClientConnection());
    client.EventHub.Subscribe<RplWelcomeMessage>(Client_OnRegistered);
    client.EventHub.Subscribe<PrivateMessage>((client, args) => Console.WriteLine(args.IrcMessage.Message));
    await client.ConnectAsync(config.Server, config.Port);
}

private async void Client_OnRegistered(object sender, EventArgs e) {
    if (!string.IsNullOrEmpty(config.NickServ)) {
        await client.SendAsync(new PrivMsgMessage("NickServ", $"identify {config.NickServ}"));
    }
    await client.SendAsync(new ModeMessage(config.Nick, "+B"));
    await JoinDefaultChannels();
}
```

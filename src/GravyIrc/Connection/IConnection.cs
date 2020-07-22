using System;
using System.Threading.Tasks;

namespace GravyIrc.Connection
{
    /// <summary>
    /// Represents an interface for a connection
    /// </summary>
    public interface IConnection : IDisposable
    {
        /// <summary>
        /// Open a TCP connection
        /// </summary>
        /// <param name="address">Address to connect to</param>
        /// <param name="port">TCP port to open connection on</param>
        /// <returns></returns>
        Task ConnectAsync(string address, int port);

        /// <summary>
        /// Send a message over an open connection
        /// </summary>
        /// <param name="data">Raw string data to send</param>
        Task SendAsync(string data);

        /// <summary>
        /// Event triggered when incoming data is receieved over the connection
        /// </summary>
        event EventHandler<DataReceivedEventArgs> DataReceived;

        /// <summary>
        /// Event triggered upon successful connection
        /// </summary>
        event EventHandler Connected;

        /// <summary>
        /// Event triggered when the connection is terminated
        /// </summary>
        event EventHandler Disconnected;
    }
}

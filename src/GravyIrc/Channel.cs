using System.Collections.ObjectModel;
using System.Linq;

namespace GravyIrc
{
    /// <summary>
    /// Represents an IRC channel with its users and messages.
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Name of channel
        /// </summary>
        /// <remarks>Typically prefixed with a '#'</remarks>
        public string Name { get; }

        /// <summary>
        /// Collection of users currently in channel
        /// </summary>
        public ObservableCollection<ChannelUser> Users { get; }

        /// <summary>
        /// Messages received in channel since client connected
        /// </summary>
        public ObservableCollection<ChatMessage> Messages { get; }

        public Channel(string name)
        {
            Name = name;
            Users = new ObservableCollection<ChannelUser>();
            Messages = new ObservableCollection<ChatMessage>();
        }

        internal void AddUser(User user, string status)
        {
            Users.Add(new ChannelUser(user, status));
        }

        internal void RemoveUser(string nick)
        {
            var user = Users.FirstOrDefault(u => u.Nick == nick);
            if (user != null)
                Users.Remove(user);
        }

        /// <summary>
        /// Get a user in the channel
        /// </summary>
        /// <param name="nick">Nick of user</param>
        /// <returns>User, if found</returns>
        public ChannelUser GetUser(string nick) => Users.FirstOrDefault(u => u.Nick.ToLower() == nick.ToLower());
    }
}

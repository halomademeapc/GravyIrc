using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GravyIrc
{
    /// <summary>
    /// Represents an IRC user. Implements INotifyPropertyChanged
    /// </summary>
    public class User : INotifyPropertyChanged
    {
        private string nick;

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="nick">Nick of user</param>
        public User(string nick)
        {
            Nick = nick;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="nick">Nick of user</param>
        /// <param name="realName">Real name of user</param>
        public User(string nick, string realName)
        {
            Nick = nick;
            RealName = realName;
        }

        /// <summary>
        /// User's nick
        /// </summary>
        public string Nick
        {
            get { return nick; }
            set
            {
                if (nick != value)
                {
                    nick = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Real name of user
        /// </summary>
        /// <remarks>Very rarely actually the person's real name</remarks>
        public string RealName { get; }

        /// <summary>
        /// Event for nick changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

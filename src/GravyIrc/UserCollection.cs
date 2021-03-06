﻿using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GravyIrc
{
    /// <summary>
    /// An observable collection that represents all users the client knows about
    /// </summary>
    public class UserCollection : ObservableCollection<User>
    {
        /// <summary>
        /// Get a user
        /// </summary>
        /// <param name="nick">Nick of user</param>
        /// <returns></returns>
        public User GetUser(string nick)
        {
            var user = Items.FirstOrDefault(u => u.Nick == nick);

            if (user == null)
            {
                user = new User(nick);
                Add(user);
            }

            return user;
        }
    }
}

/*** ---------------------------------------------------------------------------
/// Player.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 24th, 2017</date>
/// ------------------------------------------------------------------------***/

using System;
using System.Collections.Generic;

namespace core.player
{
    /// <summary>
    /// Stores the player state.
    /// </summary>
    public class Player
    {
        private const string DEFAULT_NAME = "NO_NAME";

        /// <summary>
        /// The player's name that they have chosen at the beginning.
        /// </summary>
        public string PlayerName;

        /// <summary>
        /// Saves all of the user's data stored in key value.
        /// </summary>
        private Dictionary<string, object> scalarMap;

        public Player()
        {
            scalarMap = new Dictionary<string, object>();
            PlayerName = DEFAULT_NAME;
        }
        
        public void SetValue<T>(string key, T value)
        {
            scalarMap[key] = value;
        }

        public T GetValue<T>(string key)
        {
            return (T)Convert.ChangeType(scalarMap[key], typeof(T));
        }
    }
}
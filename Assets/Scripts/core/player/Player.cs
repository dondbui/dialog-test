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

        public int IncrementValue(string key, int amount)
        {
            if (!scalarMap.ContainsKey(key))
            {
                scalarMap[key] = 0;
            }

            int value = GetValue<int>(key);
            value += amount;

            SetValue<int>(key, value);

            return value;
        }

        public Dictionary<string, object> GetScalarMap()
        {
            return scalarMap;
        }

        public void LoadFromSaveData(SaveData saveData)
        {
            scalarMap.Clear();

            PlayerName = saveData.PlayerName;

            int i = 0;
            int count = 0;

            if (saveData.int_scalars != null)
            {
                for (i = 0, count = saveData.int_scalars.Count; i < count; i++)
                {
                    ScalarSaveIntEntry entry = saveData.int_scalars[i];

                    scalarMap[entry.key] = entry.val;
                }
            }

            if (saveData.str_scalars != null)
            {
                for (i = 0, count = saveData.str_scalars.Count; i < count; i++)
                {
                    ScalarSaveStrEntry entry = saveData.str_scalars[i];

                    scalarMap[entry.key] = entry.val;
                }
            }

        }
    }
}
/*** ---------------------------------------------------------------------------
/// SaveData.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 24th, 2017</date>
/// ------------------------------------------------------------------------***/

using System;
using System.Collections.Generic;

namespace core.player
{
    [Serializable]
    public class SaveData
    {
        public string PlayerName;
        public List<ScalarSaveIntEntry> int_scalars;
        public List<ScalarSaveStrEntry> str_scalars;

        public SaveData(Player player)
        {
            PlayerName = player.PlayerName;
            Dictionary<string, object> scalarMap = player.GetScalarMap();

            foreach (KeyValuePair<string, object> entry in scalarMap)
            {
                if (entry.Value is int)
                {
                    if (int_scalars == null)
                    {
                        int_scalars = new List<ScalarSaveIntEntry>();
                    }

                    int_scalars.Add(new ScalarSaveIntEntry(entry.Key, entry.Value));

                    continue;
                }

                if (entry.Value is string)
                {
                    if (str_scalars == null)
                    {
                        str_scalars = new List<ScalarSaveStrEntry>();
                    }

                    str_scalars.Add(new ScalarSaveStrEntry(entry.Key, entry.Value));
                }
            }
        }
    }
}
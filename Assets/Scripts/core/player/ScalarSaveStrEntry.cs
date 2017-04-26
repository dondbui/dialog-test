/*** ---------------------------------------------------------------------------
/// ScalarSaveStrEntry.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 25th, 2017</date>
/// ------------------------------------------------------------------------***/

using System;

namespace core.player
{
    [Serializable]
    public class ScalarSaveStrEntry
    {
        public string key;
        public string val;

        public ScalarSaveStrEntry(string key, object value)
        {
            this.key = key;
            this.val = (string)value;
        }
    }
}
/*** ---------------------------------------------------------------------------
/// ScalarSaveIntEntry.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 25th, 2017</date>
/// ------------------------------------------------------------------------***/

using System;

namespace core.player
{
    [Serializable]
    public class ScalarSaveIntEntry
    {
        public string key;
        public int val;

        public ScalarSaveIntEntry(string key, object value)
        {
            this.key = key;
            this.val = (int)value;
        }
    }
}
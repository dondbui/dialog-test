/*** ---------------------------------------------------------------------------
/// ConversationParamModifier.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 23rd, 2017</date>
/// ------------------------------------------------------------------------***/

using UnityEngine;

namespace core.dialog
{
    /// <summary>
    /// Represents a variable modification that may occur upon viewing
    /// a conversation node. Conversation Modifiers that have already
    /// been applied will not be applied again. This progress tracking
    /// is saved elsewhere.
    /// </summary>
    public class ConversationParamModifier
    {
        public enum ModifierType
        {
            /// <summary>
            /// We want to do a hard setting of the value.
            /// </summary>
            Set,

            /// <summary>
            /// We want to increment the integer a set amount. 
            /// </summary>
            Increment,

            /// <summary>
            /// We want to decrement the integer a set amount.
            /// </summary>
            Decrement,
        }

        private const string SET_STR = "=";
        private const string INCRE_STR = "+=";
        private const string DECRE_STR = "-=";

        public string paramName;
        public int intValue;
        public string strValue;
        public ModifierType type = ModifierType.Set;

        public ConversationParamModifier(string rawString)
        {
            string cleanedString = rawString.Replace(">", string.Empty);

            // Split it on the spaces
            string[] pieces = cleanedString.Split(' ');

            if (pieces.Length < 3)
            {
                Debug.LogError("Could not parse param modifier: " + rawString);
                return;
            }

            // First piece is the variable name
            paramName = pieces[0];

            // The modifier string
            string modStr = pieces[1];
            // Based off of the modifier chars we determine which enum to use.
            switch (modStr)
            {
                case SET_STR:
                    type = ModifierType.Set;
                    break;
                case INCRE_STR:
                    type = ModifierType.Increment;
                    break;
                case DECRE_STR:
                    type = ModifierType.Decrement;
                    break;
            }

            // value is the third piece
            string rawValue = pieces[2];

            // If we got quotes then we're dealing with a string Value
            if (rawValue.Contains("\""))
            {
                intValue = 0;
                strValue = rawValue.Replace("\"", string.Empty);

                // Strings can only have set as a valid modifier.
                type = ModifierType.Set;
            }
            // Otherwise attempt to parse the integer
            else
            {
                intValue = int.Parse(pieces[2]);
                strValue = string.Empty;
            }
        }
    }
}
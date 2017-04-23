/*** ---------------------------------------------------------------------------
/// ConversationNode.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 22nd, 2017</date>
/// ------------------------------------------------------------------------***/

using System;

namespace core.dialog
{
    /// <summary>
    /// Contains a single node of a conversation. Keep things Serializable
    /// to allow use with JSONUtility. Must run Process() to make sure all
    /// data fields are initialized.
    /// </summary>
    [Serializable]
    public class ConversationNode
    {
        /// <summary>
        /// Treated as the unique identifier for the conversation node
        /// </summary>
        public string title;

        /// <summary>
        /// The body of text for the dialog
        /// </summary>
        public string body;

        /// <summary>
        /// The RAW string that contains all the tags which are applied
        /// to this ConversationNode. Space Delimited.
        /// </summary>
        public string tags;

        /// <summary>
        /// The tags split up into a string array.
        /// </summary>
        public string[] tagArray;

        public ConversationNode()
        {
        }

        public void Process()
        {
            tagArray = tags.Split(' ');
        }
    }
}
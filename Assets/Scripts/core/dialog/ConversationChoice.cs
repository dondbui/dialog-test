/*** ---------------------------------------------------------------------------
/// ConversationChoice.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 22nd, 2017</date>
/// ------------------------------------------------------------------------***/

namespace core.dialog
{
    /// <summary>
    /// Contains the data for conversation choices. The text
    /// to display as well as the link to the next node.
    /// </summary>
    public class ConversationChoice
    {
        public string nextNodeTitle;
        public string text;

        public ConversationChoice(string text, string nextNodeTitle)
        {
            this.text = text;
            this.nextNodeTitle = nextNodeTitle.Trim();
        }
    }
}
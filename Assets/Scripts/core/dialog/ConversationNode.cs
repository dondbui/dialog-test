﻿/*** ---------------------------------------------------------------------------
/// ConversationNode.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 22nd, 2017</date>
/// ------------------------------------------------------------------------***/

using System;
using System.Collections.Generic;
using UnityEngine;

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
        private const string CHOICE_DELIM = "[[";
        private const char CHOICE_ELEMENT_DELIM = '|';
        private const string CHOICE_END_BRACKET = "]";
        private const string PARAM_DELIM = "<<";

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
        /// The actual text we want to display
        /// </summary>
        public string displayBody;

        public List<ConversationChoice> choices;

        public List<ConversationParamModifier> paramMods;

        /// <summary>
        /// The tags split up into a string array.
        /// </summary>
        public string[] tagArray;

        public ConversationNode()
        {
        }

        public void Process()
        {
            ProcessTags();

            ProcessChoices();

            ProcessParams();
        }

        private void ProcessTags()
        {
            tagArray = tags.Split(' ');
        }

        private void ProcessChoices()
        {
            string[] choiceSplit = body.Split(CHOICE_DELIM.ToCharArray());

            displayBody = choiceSplit[0];

            if (choiceSplit.Length < 2)
            {
                return;
            }
            choices = new List<ConversationChoice>();

            // Iterate through the choice elements and attempt to parse them.
            for (int i = 1, count = choiceSplit.Length; i < count; i++)
            {
                string rawChoice = choiceSplit[i];

                if (string.IsNullOrEmpty(rawChoice))
                {
                    continue;
                }

                // Remove the end brackets
                rawChoice = rawChoice.Replace(CHOICE_END_BRACKET, string.Empty);

                string[] choicePieces = rawChoice.Split(CHOICE_ELEMENT_DELIM);

                // In case the structure looks odd let's log it out and skip
                if (choicePieces.Length < 2)
                {
                    Debug.LogError("Could not parse choice for: " +
                        title + " : " + rawChoice);
                    continue;
                }

                choices.Add(new ConversationChoice(choicePieces[0], choicePieces[1]));
            }
        }

        private void ProcessParams()
        {
            string[] paramSplit = displayBody.Split(PARAM_DELIM.ToCharArray());

            displayBody = paramSplit[0];

            if (paramSplit.Length < 2)
            {
                return;
            }

            paramMods = new List<ConversationParamModifier>();

            for (int i = 1, count = paramSplit.Length; i < count; i++)
            {
                string rawPMod = paramSplit[i];

                if (string.IsNullOrEmpty(rawPMod))
                {
                    continue;
                }

                ConversationParamModifier pMod = new ConversationParamModifier(rawPMod);
                paramMods.Add(pMod);
            }
        }
    }
}

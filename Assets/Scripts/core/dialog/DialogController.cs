/*** ---------------------------------------------------------------------------
/// DialogController.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 22nd, 2017</date>
/// ------------------------------------------------------------------------***/


using core.player;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace core.dialog
{
    /// <summary>
    /// The main controller for handling dialog and conversations.
    /// </summary>
    public class DialogController
    {
        private static DialogController instance;

        private Conversation currConv;
        private ConversationNode currNode;

        private Text bodyTextField;
        private Image portrait;

        private Dictionary<string, Sprite> portraitSprites;

        private DialogController()
        {
            portraitSprites = new Dictionary<string, Sprite>();
        }

        public static DialogController GetInstance()
        {
            if (instance == null)
            {
                instance = new DialogController();
            }

            return instance;
        }

        /// <summary>
        /// Loads the given dialog JSON file and processes all of the data.
        /// </summary>
        /// <param name="path"></param>
        public void LoadDialogJSON(string path)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(path);

            StringBuilder sb = new StringBuilder();

            sb.Append("{ ");
            sb.Append("\"nodes\":");
            sb.Append(textAsset.text);
            sb.Append("}");

            Conversation conv = JsonUtility.FromJson<Conversation>(sb.ToString());
            conv.uid = textAsset.name;
            conv.Process();

            // Set this new conversation to the current.
            currConv = conv;
        }

        public void StartConversation()
        {
            if (currConv == null)
            {
                Debug.LogError("Cannot start a conversation if one was not loaded.");
                return;
            }

            Debug.Log("Start Conversation.");

            InitializeUI();

            DisplayConversationNode(currConv.startNodeTitle);
        }

        public void SelectChoice(int choiceIndex)
        {
            ConversationChoice choice = currNode.choices[choiceIndex];

            SaveDecision(currNode, choiceIndex);

            Debug.Log("Index Chosen: " + choiceIndex);
            DisplayConversationNode(choice.nextNodeTitle);
        }

        private void DisplayConversationNode(string titleID)
        {
            currNode = currConv.nodeMap[titleID];

            Debug.Log(currNode.displayBody);

            bodyTextField.text = currNode.displayBody;
            portrait.sprite = portraitSprites[currNode.image];

            DisplayChoices(currNode);

            ApplyParamModifiers(currNode);
        }

        private void DisplayChoices(ConversationNode node)
        {
            if (node.choices == null || node.choices.Count < 1)
            {
                Debug.Log("End Conversation.");
                SaveConversationComplete(currConv);
                return;
            }

            int count = node.choices.Count;

            StringBuilder sb = new StringBuilder();

            if (count > 1)
            {
                sb.Append("Choose:  \n");
            }

            for (int i = 0; i < count; i++)
            {
                sb.Append(i + "): " + node.choices[i].text);
                sb.Append("\n");
            }

            Debug.Log(sb.ToString());
        }

        /// <summary>
        /// We save a conversation is complete by saving the starting node id.
        /// </summary>
        /// <param name="conv"></param>
        private void SaveConversationComplete(Conversation conv)
        {
            PlayerAccountManager.GetInstance().GetPlayer().SetValue<int>(conv.startNodeTitle, 1);
        }

        private void SaveDecision(ConversationNode node, int choice)
        {
            PlayerAccountManager.GetInstance().GetPlayer().SetValue<int>(node.title, choice);
        }

        private void ApplyParamModifiers(ConversationNode node)
        {
            if (node.paramMods == null || node.paramMods.Count < 1)
            {
                return;
            }

            PlayerAccountManager pm = PlayerAccountManager.GetInstance();
            Player player = pm.GetPlayer();

            for (int i = 0, count = node.paramMods.Count; i < count; i++)
            {
                ConversationParamModifier mod = node.paramMods[i];

                // Set the string or integer to the given value.
                if (mod.action == ConversationParamModifier.ModifierActionType.Set)
                {
                    if (mod.type == ConversationParamModifier.ModifierType.Integer)
                    {
                        player.SetValue<int>(mod.paramName, mod.intValue);
                    }
                    else
                    {
                        player.SetValue<string>(mod.paramName, mod.strValue);
                    }
                    continue;
                }

                if (mod.action == ConversationParamModifier.ModifierActionType.Increment)
                {
                    int value = player.GetValue<int>(mod.paramName);
                    value += mod.intValue;
                    player.SetValue<int>(mod.paramName, value);
                    continue;
                }

                if (mod.action == ConversationParamModifier.ModifierActionType.Decrement)
                {
                    int value = player.GetValue<int>(mod.paramName);
                    value -= mod.intValue;
                    player.SetValue<int>(mod.paramName, value);
                    continue;
                }
            }

            // Save the player progress at this point. 
            pm.SavePlayer();
        }

        private void InitializeUI()
        {
            GameObject textObj = GameObject.Find("bodyText");
            bodyTextField = textObj.GetComponent<Text>();

            GameObject imgObj = GameObject.Find("portrait");
            portrait = imgObj.GetComponent<Image>();

            // Initialize the portrait sets
            Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/fire_emblem");
            for (int i = 0, count = sprites.Length; i < count; i++)
            {
                Sprite portrait = sprites[i];
                portraitSprites[portrait.name] = portrait;
            }
        }
    }
}
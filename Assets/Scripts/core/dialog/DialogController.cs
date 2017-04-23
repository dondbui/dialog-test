/*** ---------------------------------------------------------------------------
/// DialogController.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 22nd, 2017</date>
/// ------------------------------------------------------------------------***/


using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace core.dialog
{
    public class DialogController
    {
        private static DialogController instance;


        private DialogController()
        {

        }

        public static DialogController GetInstance()
        {
            if (instance == null)
            {
                instance = new DialogController();
            }

            return instance;
        }

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
        }
    }
}
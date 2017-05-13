/*** ---------------------------------------------------------------------------
/// UIInputController.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>May 13th, 2017</date>
/// ------------------------------------------------------------------------***/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace core.ui
{
    /// <summary>
    /// This acts as the central Observer for all of the button
    /// clicks for the UI
    /// </summary>
    public class UIInputController
    {
        public delegate void ButtonCallback(Button buttonClicked);

        private static UIInputController instance;

        private Dictionary<string, List<ButtonCallback>> callbackMap;

        private UIInputController()
        {
            callbackMap = new Dictionary<string, List<ButtonCallback>>();
        }

        public static UIInputController GetInstance()
        {
            if (instance == null)
            {
                instance = new UIInputController();
            }

            return instance;
        }

        /// <summary>
        /// On clicking a button try to call all the callbacks registered with this button name
        /// </summary>
        /// <param name="button"></param>
        public void ButtonClicked(Button button)
        {
            Debug.Log("ButtonClicked: " + button.name);

            string name = button.name;

            // no mapping no problem
            if (!callbackMap.ContainsKey(name))
            {
                return;
            }

            List<ButtonCallback> callbackList = callbackMap[name];

            for (int i = 0, count = callbackList.Count; i < count; i++)
            {
                ButtonCallback callback = callbackList[i];
                callback(button);
            }

        }

        /// <summary>
        /// Registers a callback for a given button name
        /// </summary>
        public void RegisterButtonListener(string buttonName, ButtonCallback callback)
        {
            if (!callbackMap.ContainsKey(buttonName))
            {
                callbackMap[buttonName] = new List<ButtonCallback>();
            }

            List<ButtonCallback> callbackList = callbackMap[buttonName];

            // Already contains then bounce out
            if (callbackList.Contains(callback))
            {
                return;
            }

            callbackList.Add(callback);
        }

        /// <summary>
        /// Unregisters a callback from a given button name
        /// </summary>
        public void UnregisterButtonListener(string buttonName, ButtonCallback callback)
        {
            // No mapping no problem
            if (!callbackMap.ContainsKey(buttonName))
            {
                return;
            }

            List<ButtonCallback> callbackList = callbackMap[buttonName];
            if (callbackList.Contains(callback))
            {
                callbackList.Remove(callback);
            }
        }
        
    }
}
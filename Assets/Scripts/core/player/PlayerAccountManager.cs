/*** ---------------------------------------------------------------------------
/// PlayerAccountManager.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 24th, 2017</date>
/// ------------------------------------------------------------------------***/

using System;
using System.IO;
using UnityEngine;

namespace core.player
{
    /// <summary>
    /// Handles anything related to the player account.
    /// </summary>
    public class PlayerAccountManager
    {
        private const string SAVE_DIR = "Saves/";
        private const string SAVE_FILE = "savefile.sav";

        private static PlayerAccountManager instance;

        private Player currentPlayer;

        private PlayerAccountManager()
        {
            currentPlayer = new Player();
        }

        public static PlayerAccountManager GetInstance()
        {
            if (instance == null)
            {
                instance = new PlayerAccountManager();
            }

            return instance;
        }

        public Player GetPlayer()
        {
            return currentPlayer;
        }

        public void LoadPlayer()
        {
            string rawSaveText = File.ReadAllText(SAVE_DIR + SAVE_FILE);
            Debug.Log(rawSaveText);

            SaveData save = JsonUtility.FromJson<SaveData>(rawSaveText);

            currentPlayer.LoadFromSaveData(save);
        }

        public void SavePlayer()
        {
            DateTime startDate = DateTime.Now;
            Debug.Log("Staring Save");

            if (!Directory.Exists(SAVE_DIR))
            {
                Directory.CreateDirectory(SAVE_DIR);
            }

            SaveData save = new SaveData(currentPlayer);

            string json = JsonUtility.ToJson(save, true);

            File.WriteAllText(SAVE_DIR + SAVE_FILE, json);

            save = null;
            json = null;

            DateTime endDate = DateTime.Now;

            Debug.Log("Total Saving Time: " +
                endDate.Subtract(startDate).TotalMilliseconds + " MS");
        }
    }
}
/*** ---------------------------------------------------------------------------
/// PlayerAccountManager.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 24th, 2017</date>
/// ------------------------------------------------------------------------***/

namespace core.player
{
    /// <summary>
    /// Handles anything related to the player account.
    /// </summary>
    public class PlayerAccountManager
    {
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

        }

        public void SavePlayer()
        {

        }
    }
}
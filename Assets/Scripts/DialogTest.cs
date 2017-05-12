/*** ---------------------------------------------------------------------------
/// DialogTest.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 22nd, 2017</date>
/// ------------------------------------------------------------------------***/

using core.dialog;
using core.player;
using System;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    private const string EP01_JSON = "JSON/ep01";

    private int[] unitTest01 = {0,0};

    // Use this for initialization
    public void Start ()
    {
        DateTime startDate = DateTime.Now;
        Debug.Log("Welcome to the Dialog Test");

        Debug.Log("Begin Loading JSON.");
        // Begin loading of the JSON.

        DialogController.GetInstance().LoadDialogJSON(EP01_JSON);

        Debug.Log("JSON Loading Complete.");
        Debug.Log("End Initialization");

        DateTime endDate = DateTime.Now;

        Debug.Log("Total Initialization Time: " +
            endDate.Subtract(startDate).TotalMilliseconds + " MS");

        StartConversationTest();

        PlayerAccountManager.GetInstance().LoadPlayer();
    }
    
    // Update is called once per frame
    public void Update ()
    {

    }

    private void StartConversationTest()
    {
        DialogController dc = DialogController.GetInstance();

        dc.StartConversation();

        //for (int i = 0, count = unitTest01.Length; i < count; i++)
        //{
        //    dc.SelectChoice(unitTest01[i]);
        //}
    }
}

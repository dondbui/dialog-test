/*** ---------------------------------------------------------------------------
/// DialogTest.cs
/// 
/// <author>Don Duy Bui</author>
/// <date>April 22nd, 2017</date>
/// ------------------------------------------------------------------------***/

using core.dialog;
using System;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    private const string EP01_JSON = "JSON/ep01";


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
    }
    
    // Update is called once per frame
    public void Update ()
    {

    }
}

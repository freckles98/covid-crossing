using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages setup procedure - checking if its the first time the player is starting and activates the necessary unity objects.
/// </summary>
public class SetupManager : MonoBehaviour
{
    public GameObject tutorialScreen;
    public GameObject usernameScreen;

    public bool settingUp; //so player not move during setup

    /// <summary>
    /// when starting up the game, checks if first time, then starts up username screen.
    /// </summary>
    void Start()
    {
        // If you want to clear the existing save for testing purposes, uncomment these lines.
        // SharedCanvas.Instance.saveManager.record = new PlayerRecord();
        // SharedCanvas.Instance.saveManager.Flush();

        if (SharedCanvas.Instance.saveManager.record.nickname == null) //if no username exist 
        {
            usernameScreen.gameObject.SetActive(true);
            settingUp = true;
        }
    }
    
    /// <summary>
    /// Called by button when username confirmed, closes username screen and starts tutorial.
    /// </summary>
    public void FinishUsername()
    {
        usernameScreen.gameObject.SetActive(false);
        tutorialScreen.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called by TutorialManager when tutorial finished, to close deactivate/close tutorial.
    /// </summary>
    public void FinishTutorial()
    {
        tutorialScreen.gameObject.SetActive(false);
        settingUp = false;
    }

    
}

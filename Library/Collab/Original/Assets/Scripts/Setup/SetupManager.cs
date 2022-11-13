using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour
{
    public GameObject tutorialScreen;
    public GameObject usernameScreen;

    public bool settingUp; //so player not move during setup

    // Start is called before the first frame update
    void Start()
    {
        // If you want to clear the existing save for testing purposes, uncomment these lines.
        //SharedCanvas.Instance.saveManager.record = new PlayerRecord();
        // SharedCanvas.Instance.saveManager.Flush();

        if (SharedCanvas.Instance.saveManager.record.nickname == null) //if no username exist 
        {
            usernameScreen.gameObject.SetActive(true);
            settingUp = true;
        }
    }

    public void FinishUsername()
    {//finished entering username, now activate tutorial
        usernameScreen.gameObject.SetActive(false);
        tutorialScreen.gameObject.SetActive(true);
    }

    public void FinishTutorial()
    {
        tutorialScreen.gameObject.SetActive(false);
        settingUp = false;
    }

    
}

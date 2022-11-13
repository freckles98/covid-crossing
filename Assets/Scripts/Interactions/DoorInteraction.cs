using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Allows player to interact with the door and change rooms, which sets the new scene.
/// </summary>
public class DoorInteraction : Interaction
{
    public string toLoad;

    protected override void Interact()
    {
        SharedCanvas.Instance.previousRoom = SceneManager.GetActiveScene().name; //set to current room
        SceneManager.LoadScene(toLoad, LoadSceneMode.Single); //then change to new room  
    }
}


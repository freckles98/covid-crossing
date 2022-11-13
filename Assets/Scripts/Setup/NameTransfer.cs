using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles transferring user input to saveManager's nickname field.
/// </summary>
public class NameTransfer : MonoBehaviour
{
    public string userName;
    public GameObject inputField; //where user types name

    /// <summary>
    /// Method called by button after username is entered, transferring user input to saveManager's nickname field. If no input is given, username is set to "Anonymous".
    /// </summary>
    public void StoreName()
    {
        userName = inputField.GetComponent<Text>().text;
        if (userName.Equals("") || userName.Equals(null)) //no username given
        {
            userName = "Anonymous";
        }
        SharedCanvas.Instance.saveManager.record.nickname = userName; //set to what user asked for 
        SharedCanvas.Instance.saveManager.Flush(); //to save changes
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages where player should appear in living room based off what the previous room was.
/// </summary>
public class PlayerStartingPoint : MonoBehaviour
{
    /// <summary>
    /// Places player in a position based off what the previous room was.
    /// </summary>
    void Start()
    {
       if (SharedCanvas.Instance.previousRoom.Equals("Study Room")) 
        {
            this.gameObject.transform.position = new Vector3(8f, -1.0f, 0); //near stairs
        }
       else if ((SharedCanvas.Instance.previousRoom.Equals("Living Room")))
        {
            this.gameObject.transform.position = new Vector3(-5.5f, -1.4f, 0); //near bed
        }
       //else do nothing, start near shop door
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Adds mask to player in the shop scene if they chose to wear a mask during the LeaveHomeDoorInteraction
/// </summary>
public class MaskShopActivation : MonoBehaviour
{
    public GameObject mask;

    // Start is called before the first frame update
    void Start()
    {
        mask.SetActive(LeaveHomeDoorInteraction.choseWear);
    }
}

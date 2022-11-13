using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// Manages bubble surrounding NPC: the size, distance calculation, and decrement of score.
/// </summary>
public class NPCBubbleBehaviour : MonoBehaviour
{
    public GameObject mask;
    private float socialDistanceDistance; //how far before infection danger
    public GameObject bubble;
    public GameObject player;
    ScoreManager scoreManager;
    public PlayerImmunityBehaviour playerImmunityBehaviour;

    /// <summary>
    /// Before first frame, randomises whether NPC should wear a mask, and if so make bubble smaller than if not. Set socialDistanceDistance based off of bubble size.
    /// </summary>
    void Start()
    {
        scoreManager = SharedCanvas.Instance.scoreManager;

        //randomise wear mask:
        int maskRandomNum = UnityEngine.Random.Range(0, 2); //0 for no, 1 for yes

        if (maskRandomNum == 1) //wearing mask
        {
            mask.SetActive(true);
            //make bubble a bit smaller, player can get nearer 
            socialDistanceDistance = 2.7f; 
            bubble.transform.localScale = new Vector3(2.5f, 2.5f, 1); //.2 less than distance for visual appeal

        }
        else //no mask. so bubble larger and set socialDistanceDistance to a bigger value
        {
            mask.SetActive(false);
            bubble.transform.localScale = new Vector3(3f, 3f, 1); 
            socialDistanceDistance = 3.2f; 
        }

    }

    /// <summary>
    /// Decrements scores if player is near and player is not immune (from sanitising station).
    /// </summary>
    void FixedUpdate()
    {
        if (IsNearby() && !playerImmunityBehaviour.IsImmune())
        {
            scoreManager.ChangeCommunityScore(-0.5f);
            scoreManager.ChangePersonalScore(-0.5f);
        }
    }

    /// <summary>
    /// Checks if player is significantly nearby to current object.
    /// </summary>
    /// <returns> True if significantly near, else false. </returns>
    public bool IsNearby() 
    {
        float PlayerX = player.gameObject.transform.position.x;
        float PlayerY = player.gameObject.transform.position.y;
        float ThisX = gameObject.transform.position.x;
        float ThisY = gameObject.transform.position.y;

        float distance = math.sqrt(math.pow((PlayerX - ThisX), 2) + math.pow((PlayerY - ThisY), 2));

        if (distance < socialDistanceDistance)
        {
            return true;
        }

        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class InteractionBehaviour : MonoBehaviour
{
    private static float RGB_MIN = 0.5f;
    private static float RGB_MAX = 1.0f;
    private static float PERIOD = 1.2f;

    public GameObject player; //drag player here
    private float glowDistance = 2.4f; // how close before glow
    private static bool isCalled;
    private int dayLastUsed = -1;


    enum GlowState
    {
        None,
        Glow,
        Afterglow,
    }

    

    float glowStartTime;
    float afterglowStartTime;
    GlowState glowState = GlowState.None;

    public bool IsNearby() //checks if player is significantly nearby to current object
    {
        float PlayerX = player.gameObject.transform.position.x;
        float PlayerY = player.gameObject.transform.position.y;
        float ThisX = gameObject.transform.position.x;
        float ThisY = gameObject.transform.position.y;

        float distance = math.sqrt(math.pow((PlayerX - ThisX), 2) + math.pow((PlayerY - ThisY), 2));

        if (distance < glowDistance)
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;

        if (glowState == GlowState.None) //not glowing, but nearby
        {
            if (IsNearby())
            {
                glowState = GlowState.Glow;
                glowStartTime = time;
            }
        }
        else if (glowState == GlowState.Glow) //glowing, but far away
        {
            if (!IsNearby())
            {
                glowState = GlowState.Afterglow;
                afterglowStartTime = time;
            }
        }
        else if (glowState == GlowState.Afterglow) //busy ending glow
        {
            if (IsNearby()) //gotten near, start glowing
            {
                glowState = GlowState.Glow;
            }
            else
            {
                // Do we need to stop glowing at this point?:
                float afterglowOscillationIndex = math.floor((afterglowStartTime - glowStartTime) / PERIOD);
                float currentOscillationIndex = math.floor((time - glowStartTime) / PERIOD);
                if (currentOscillationIndex > afterglowOscillationIndex)
                {
                    glowState = GlowState.None;
                }
            }
        }

        ApplyVisualEffect(time); //preview state

    }

    void ApplyVisualEffect(float time)
    {
        if (glowState == GlowState.Glow || glowState == GlowState.Afterglow)
        {
            float newRGBval = OscillateSinusoidalPure(RGB_MIN, RGB_MAX, PERIOD, (float)(time - glowStartTime));
            gameObject.GetComponent<Renderer>().material.color = new Color(newRGBval, newRGBval, newRGBval);
        }
        else if (glowState == GlowState.None)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }
    }

    public void OnMouseDown() //if clicked
    {
        if (glowState == GlowState.Glow)
        {
            Debug.Log(dayLastUsed);
            if (!WasUsedToday())
            {
                Interact();
            }
            else{
                DialogueElement dialogueELement = new DialogueElement(null, "You may only use this once a day.", null, new DialogueTerminal("Ok"));
                SharedCanvas.Instance.dialogueManager.RunDialogue(dialogueELement);
            }

        }
        
    }

    protected abstract void Interact(); //to be overridden

    protected bool WasUsedToday() 
    {
        return dayLastUsed == SharedCanvas.Instance.GetDay();
       
    }
    protected void SetWasUsedToday()
    {
        dayLastUsed = SharedCanvas.Instance.GetDay();
    }
   
   
 
    private static float OscillateSinusoidalPure(float min, float max, float period, float t)  //calculates values new r,g,b values should be 
    {
        return (float) ((max - min) * (0.5 * math.cos(2 * math.PI * t / period) + 0.5) + min);
    }

    public static bool IsCalled()
    {
        return isCalled;
        
    }

    public static void ResetCall()
    {
        isCalled = false;
    }
}

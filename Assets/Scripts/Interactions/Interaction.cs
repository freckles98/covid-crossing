using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Parent class for all objects player can interact with. 
/// Makes object glow if player is sufficiently clear, and calls child's interact class when interactions should be triggered.
/// </summary>
public abstract class Interaction : MonoBehaviour
{
    private static float RGB_MIN = 0.5f;
    private static float RGB_MAX = 1.0f;
    private static float PERIOD = 1.2f;
    private static Dictionary<string, int> dayLastUsedMap = new Dictionary<string, int>();
    public GameObject player; //drag player here
    private float glowDistance = 2.4f; // how close before glow
    
    enum GlowState
    {
        None,
        Glow,
        Afterglow,
    }

    private float glowStartTime;
    private float afterglowStartTime;
    GlowState glowState = GlowState.None;

    /// <summary>
    /// Checks if player is significantly nearby to current object
    /// </summary>
    /// <returns>True if sufficiently nearby, else false.</returns>
    public bool IsNearby() 
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

    /// <summary>
    /// Changes glowstate once per frame based on current state and distance. 
    /// </summary>
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

    /// <summary>
    /// Changes appearance (colour or "glow") of object based on current glowState.
    /// </summary>
    /// <param name="time">Current time.</param>
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

    /// <summary>
    /// If object is clicked, calls child Interact method if glowing, not used today, and no other canvas elements up on screen.
    /// </summary>
    public void OnMouseDown() //if clicked
    {
        if (SharedCanvas.Instance.dialogueManager.IsActive ||
            SharedCanvas.Instance.leaderboardManager.IsActive || 
            SharedCanvas.Instance.setupManager.settingUp) // if busy with dialogue/leaderboard/setup
        {
            return;
        }

        if (glowState == GlowState.Glow)
        {
            if (!WasUsedToday())
            {
                Interact();
            }
            else{
                DialogueElement dialogueELement = new DialogueElement(null, "You may only use this once a day.", null, new DialogueTerminal("OK"));
                SharedCanvas.Instance.dialogueManager.RunDialogue(dialogueELement);
            }

        }
        
    }

    /// <summary>
    /// To be overridden by child. Runs what should happen when object is interacted with.
    /// </summary>
    protected abstract void Interact();

    /// <summary>
    /// Provides a name for the object that remains the same between scene transitions, allowing us to track its identity after scene changes. Null by default, but changed by objects that may only be used once per day.
    /// </summary>
    /// <returns>Name of object.</returns>
    protected virtual string GetName() { return null; }

    /// <summary>
    /// Checks if gameObject has been used today.
    /// </summary>
    /// <returns></returns>
    public bool WasUsedToday() 
    {
        if (GetName() != null && dayLastUsedMap.ContainsKey(GetName()))
        {
            return dayLastUsedMap[GetName()] == SharedCanvas.Instance.dayTracker.GetDay();
        }
        else
        {
            return false;
        }
    }

    protected void SetWasUsedToday()
    {
        if (GetName() == null) { throw new InvalidOperationException("Object does not override Name"); }
        dayLastUsedMap[GetName()] = SharedCanvas.Instance.dayTracker.GetDay();
    }

    /// <summary>
    /// Calculates values new r,g,b values should be in order to look visually appealing.
    /// </summary>
    /// <param name="min">Min value it may be.</param>
    /// <param name="max">Max value it may be.</param>
    /// <param name="period">Period of cos graph.</param>
    /// <param name="t">Time since glow started</param>
    /// <returns>Value "r,g,b" values become.</returns>
    private static float OscillateSinusoidalPure(float min, float max, float period, float t)  
    {
        return (float) ((max - min) * (0.5 * math.cos(2 * math.PI * t / period) + 0.5) + min);
    }
}

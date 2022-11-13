using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedCanvas : MonoBehaviour
{
    public ScoreManager scoreManager;
    public DialogueManager dialogueManager;
    public ButtonBehaviour LeftArrow;
    public ButtonBehaviour RightArrow;
    public ButtonBehaviour UpArrow;
    public ButtonBehaviour DownArrow;
    private int dayTracker = 0;

    public static SharedCanvas Instance { get; private set; } = null;

    void Start()
    {
        
        if (Instance != null)
        {
            Instance.LeftArrow.Reset();
            Instance.RightArrow.Reset();
            Instance.UpArrow.Reset();
            Instance.DownArrow.Reset();
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

       
    }

    public bool IsLeftArrowPressed()
    {
        return LeftArrow.IsPressed();
    }

    public bool IsRightArrowPressed()
    {
        return RightArrow.IsPressed();
    }

    public bool IsUpArrowPressed()
    {
        return UpArrow.IsPressed();
    }

    public bool IsDownArrowPressed()
    {
        return DownArrow.IsPressed();
    }
   public void NextDay()
    {
        dayTracker++;
    }
    public int GetDay()
    {
        return dayTracker;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to ensure that the SharedCanvas remains the same between scenes and that its contents are accessible between scenes. Typically
/// objects in each scene will be destroyed and recreated between scene transitions but this behaviour is not desirable for elements under the canvas
/// in the scene hierarchy. Many game services are implemented as MonoBehaviours that exist as a child of the canvas and can be accessed from this singleton.
/// </summary>
public class SharedCanvas : MonoBehaviour
{
    public ScoreManager scoreManager;
    public DialogueManager dialogueManager;
    public ButtonManager buttonManager;
    public SaveManager saveManager;
    public SetupManager setupManager;
    public LeaderboardManager leaderboardManager;
    public DayTracker dayTracker = new DayTracker();

    public string previousRoom;

    /// <summary>
    /// Publically accessible static singleton instance of this class.
    /// </summary>
    public static SharedCanvas Instance { get; private set; } = null;

    /// <summary>
    /// Called when this object is created in a scene. If there is a preexisting shared canvas, destroy this object so that thhe
    /// canvas is not recreated. Otherwise set the static instance property to reference this instance.
    /// </summary>
    void Start()
    {
        previousRoom = "Living Room";

        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
   
}

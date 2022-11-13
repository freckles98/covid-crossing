using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of both scores
/// </summary>
public class ScoreManager : MonoBehaviour 
{
    public HealthBar personalHealthBar;  
    public HealthBar communityHealthBar;

    /// <summary>
    /// Calculates final score after finishing the day.
    /// </summary>
    /// <returns>Final weighted average score.</returns>
    public float FinalScore()
    {
        return (float) 0.5*personalHealthBar.GetHealth()+ (float) 0.5*communityHealthBar.GetHealth(); 
    }

    /// <summary>
    /// Changes community score.
    /// </summary>
    /// <param name="change">Value to change by.</param>
    public void ChangeCommunityScore(float change)
    {
        communityHealthBar.ChangeHealth(change);
    }

    /// <summary>
    /// Changes personal score.
    /// </summary>
    /// <param name="change">Value to change by.</param>
    public void ChangePersonalScore(float change)
    {
        personalHealthBar.ChangeHealth(change);
    }

    /// <summary>
    /// Changes both personal and community score.
    /// </summary>
    /// <param name="personal">Value to change personal score by.</param>
    /// <param name="community">Value to change community score by.</param>
    public void ChangeScores(float personal, float community)
    {
        ChangePersonalScore(personal);
        ChangeCommunityScore(community);
    }

    /// <summary>
    /// Saves score to saveManager if its higher than the currently recorded highscore, and resets both healths.
    /// </summary>
    public void EndDayAndResetScores()
    {
        var record = SharedCanvas.Instance.saveManager.record;
        int finalScore = (int) FinalScore();
        if (finalScore > record.highScore)
        {
            record.highScore = finalScore;
            SharedCanvas.Instance.saveManager.Flush();
        }

        personalHealthBar.ResetHealth();
        communityHealthBar.ResetHealth();
    }

    /// <summary>
    /// Get highest recorded score.
    /// </summary>
    /// <returns>Highscore.</returns>
    public int GetHighScore()
    {
        return SharedCanvas.Instance.saveManager.record.highScore;
    }

    /// <summary>
    /// Gets current community score.
    /// </summary>
    /// <returns>Current comminity score.</returns>
    public float GetCommunityScore()
    {
        return communityHealthBar.GetHealth();
    }

    /// <summary>
    /// Gets current personal score.
    /// </summary>
    /// <returns>Current personal score.</returns>
    public float GetPersonalScore()
    {
        return personalHealthBar.GetHealth();
    }
}

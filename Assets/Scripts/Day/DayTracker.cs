using UnityEngine;
/// <summary>
/// Tracks the current day the player is on. Used to check whether the player has used a certain oject on that particular day.
/// </summary>
public class DayTracker
{
    private int dayTracker = 0;
   /// <summary>
   /// Increments the current day
   /// </summary>
    public void NextDay()
    {
        dayTracker++;
    }
    /// <summary>
    /// Returns the current day that the player is on.
    /// </summary>
    /// <returns>the currents day</returns>
    public int GetDay()
    {
        return dayTracker;
    }
}
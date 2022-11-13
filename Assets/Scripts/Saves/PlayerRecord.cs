using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a record for a player. Records can be serialized in order to send them over the network
/// or save them to the disk. These are used to save the high score for the player as well as to represent
/// each entry on the leaderboard. The GUID is generated at install time and uniquely identifies players.
/// </summary>
[Serializable]
public class PlayerRecord
{
    public string guid = Guid.NewGuid().ToString();
    public string nickname = null;
    public int highScore = 0;
}

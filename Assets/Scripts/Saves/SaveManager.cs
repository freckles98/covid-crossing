using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Save manager is a game service that saves the player record to secondary storage using JSON 
/// serialization and deserialization. This script must run before other scripts that expect to
/// be able to access the save provided by this class. The script guarantees that there will be
/// a valid record available if it has been initialized.
/// </summary>
public class SaveManager : MonoBehaviour
{
    public PlayerRecord record;

    private static string Path() => Application.persistentDataPath + "/score.json";

    /// <summary>
    /// Unity Start method for this behaviour. Will check if a save is available and try to read it; otherwise 
    /// a new save is initialized and written to the file system. These operations are synchronous but should
    /// ideally be made asynchronous.
    /// </summary>
    void Start()
    {
        try
        {
            record = JsonUtility.FromJson<PlayerRecord>(File.ReadAllText(Path()));
        }
        catch (FileNotFoundException)
        {
            record = new PlayerRecord();
            Flush();
        }
    }

    /// <summary>
    /// Write the PlayerRecord to secondary storage. This is currently done synchronously which is not ideal,
    /// although the operation does complete very quickly.
    /// </summary>
    public void Flush()
    {
        if (record == null) { throw new ArgumentNullException(); }
        File.WriteAllText(Path(), JsonUtility.ToJson(record));
    }
}

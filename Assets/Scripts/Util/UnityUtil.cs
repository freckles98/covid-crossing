using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static helper methods for Unity APIs
/// </summary>
public static class UnityUtil
{
    /// <summary>
    /// Helper method to read a text resource from the resources asset directory.
    /// </summary>
    /// <param name="path">The path of the resource relative to the resource directory. Exclude the '.txt' file extension (which is always expected).</param>
    /// <returns></returns>
    public static string ReadTextResource(string path)
    {
        TextAsset resource = Resources.Load(path) as TextAsset;
        if (resource == null) { throw new ArgumentException("Could not open resource"); }
        return resource.text;
    }
}

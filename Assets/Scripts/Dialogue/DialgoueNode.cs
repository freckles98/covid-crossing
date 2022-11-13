using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class representing an element within a dialogue tree.
/// </summary>
public abstract class DialogueNode
{
    public string title;
    public Action onReached;

    /// <summary>
    /// Construct the dialogue node (used by subclasses).
    /// </summary>
    /// <param name="title">The title for the node, used if it appears on a button. Possibly null.</param>
    /// <param name="onReached">A callback to be executed when the dialogue node is reached. Possibly null.</param>
    public DialogueNode(string title, Action onReached)
    {
        this.title = title;
        this.onReached = onReached;
    }

    /// <summary>
    /// Called by the dialogue manager when the node is reached by the player. Calls the onReached callback
    /// for the node if it is not null.
    /// </summary>
    public void Reach()
    {
        if (onReached != null)
        {
            onReached();
        }
    }
}

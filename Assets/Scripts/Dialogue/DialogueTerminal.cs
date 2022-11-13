using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A dialogue terminal element. These appear on the leaves of dialogue trees. They contain no actual dialogue text or children (i.e. further dialogue options).
/// Rather, they correspond to options at the end of the dialogue. onReached will often be used here to run arbitrary code according to which answer was selected.
/// </summary>
public class DialogueTerminal : DialogueNode
{
    /// <summary>
    /// Construct a dialogue terminal with the given arguments.
    /// </summary>
    /// <param name="title">Title to be used for this node if it appears as a button. Possibly null.</param>
    /// <param name="onReached">A callback to be executed when the node is reached. Possibly null.</param>
    public DialogueTerminal(string title, Action onReached = null) : base(title, onReached)
    {
    }
}

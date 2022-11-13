using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a dialogue node where text is displayed to the player along with a set of responses given by the children on the node.
/// </summary>
public class DialogueElement : DialogueNode
{
    public string text;
    public List<DialogueNode> children;

    /// <summary>
    /// Construct a dialogue node with the given arguments.
    /// </summary>
    /// <param name="title">Title to be used for this node if it appears as a button. Possibly null.</param>
    /// <param name="text">Text to be displayed on the screen when the node is reached.</param>
    /// <param name="onReached">A callback to be executed when the node is reached. Possibly null.</param>
    /// <param name="children">Direct children to this node as an Enumerable. Each represents an option to the player.</param>
    public DialogueElement(string title, string text, Action onReached = null, IEnumerable<DialogueNode> children = null) : base(title, onReached)
    {
        this.text = text;
        this.children = children != null ? children.ToList() : new List<DialogueNode>();
    }

    /// <summary>
    /// Construct a dialogue node with the given arguments.
    /// </summary>
    /// <param name="title">Title to be used for this node if it appears as a button. Possibly null.</param>
    /// <param name="text">Text to be displayed on the screen when the node is reached.</param>
    /// <param name="onReached">A callback to be executed when the node is reached. Possibly null.</param>
    /// <param name="children">Direct children to this node as an array (including implicitly). Each represents an option to the player.</param>
    public DialogueElement(string title, string text, Action onReached = null, params DialogueNode[] children) : base(title, onReached)
    {
        this.text = text;
        this.children = children.ToList();
    }

    /// <summary>
    /// Appends a dialogue node as a child element.
    /// </summary>
    /// <param name="node">The node to add</param>
    /// <returns>The node that was passed and added</returns>
    public DialogueNode Add(DialogueNode node)
    {
        this.children.Add(node);
        return node;
    }

    /// <summary>
    /// Appends a dialogue element as a child element using the given initialization arguments.
    /// </summary>
    /// <param name="title">Title to be used for this node if it appears as a button. Possibly null.</param>
    /// <param name="text">Text to be displayed on the screen when the node is reached.</param>
    /// <param name="onReached">A callback to be executed when the node is reached. Possibly null.</param>
    /// <param name="children">Direct children to this node as an Enumerable. Each represents an option to the player.</param>
    /// <returns>The element that was added.</returns>
    public DialogueElement AddElement(string title, string text, Action onReached = null, IEnumerable<DialogueNode> children = null)
    {
        DialogueElement element = new DialogueElement(title, text, onReached, children);
        this.children.Add(element);
        return element;
    }

    /// <summary>
    /// Appends a dialogue element as a child element using the given initialization arguments.
    /// </summary>
    /// <param name="title">Title to be used for this node if it appears as a button. Possibly null.</param>
    /// <param name="text">Text to be displayed on the screen when the node is reached.</param>
    /// <param name="onReached">A callback to be executed when the node is reached. Possibly null.</param>
    /// <param name="children">Direct children to this node as an array (including implicitly). Each represents an option to the player.</param>
    /// <returns>The element that was added.</returns>
    public DialogueElement AddElement(string title, string text, Action onReached = null, params DialogueNode[] children)
    {
        DialogueElement element = new DialogueElement(title, text, onReached, children);
        this.children.Add(element);
        return element;
    }

    /// <summary>
    /// Appends a dialogue terminal as a child element using the given initialization arguments.
    /// </summary>
    /// <param name="title">Title to be used for this node if it appears as a button. Possibly null.</param>
    /// <param name="onReached">A callback to be executed when the node is reached. Possibly null.</param>
    /// <returns>The terminal node that was added.</returns>
    public DialogueTerminal AddTerminal(string title, Action onReached = null)
    {
        DialogueTerminal terminal = new DialogueTerminal(title, onReached);
        this.children.Add(terminal);
        return terminal;
    }
}

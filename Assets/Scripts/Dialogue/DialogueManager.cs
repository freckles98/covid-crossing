using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dialogue manager game service. Used throughout the game to display dialouge choices to the player.
/// Accepts a dialogue tree using the DialogueElement and DialogueTerminal structures and allows calling
/// code to respond to particular nodes being reached using callbacks.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public Text dialogueText;
    private float delay = 0.0005f;
    private int buttonRangeStart = 0;
    private int buttonRangeEnd = 3;
    private DialogueNode currentNode;

    /// <summary>
    /// A property returning all of the buttons as a List.
    /// </summary>
    private List<GameObject> Buttons => new List<GameObject>() { button1, button2, button3, button4 };

    /// <summary>
    /// The property returning all of the currently active buttons as a list (in their natural ordering).
    /// </summary>
    private List<GameObject> CurrentButtons
    {
        get
        {
            List<GameObject> buttons = new List<GameObject>();
            for (int i = buttonRangeStart; i <= buttonRangeEnd; i++)
            {
                buttons.Add(Buttons[i]);
            }
            return buttons;
        }
    }
    private CancellationToken typingCancellationToken;

    public bool IsActive => dialogueBox.activeSelf;

    /// <summary>
    /// Run a dialogue tree. Displays dialogue to the player, beginning with the root node and traversing the tree according to player selections.
    /// onReached callbacks are executed when the corresponding nodes are reached.
    /// </summary>
    /// <param name="node">The root of the dialogue tree to run.</param>
    public void RunDialogue(DialogueNode node)
    {
        dialogueBox.SetActive(true);
        HandleNode(node);
    }

    /// <summary>
    /// An internal method to do handle UI processing and tree traversal when we reach a particular node. The current node updated according to the
    /// node that is passed here. The onReached callback for this node is also immediately executed (if it exists).
    /// </summary>
    /// <param name="node">The node to handle</param>
    private void HandleNode(DialogueNode node)
    {
        currentNode = node;
        node.Reach();
        if (currentNode != node) // Did reaching the node run some new dialogue?
        {
            return;
        }
        if (node is DialogueElement element)
        {
            if (typingCancellationToken != null)
            {
                typingCancellationToken.RequestCancellation();
            }
            typingCancellationToken = new CancellationToken();
            StartCoroutine(TypeText(element.text, typingCancellationToken));
            UseButtons(element.children.Count);
            List<GameObject> buttons = CurrentButtons;
            for (int i = 0; i < element.children.Count; i++)
            {
                Text text = buttons[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                text.text = element.children[i].title;
            }
        }
        else if (node is DialogueTerminal terminal)
        {
            dialogueBox.SetActive(false);
        }
        else { throw new InvalidOperationException("Could not recognise node type."); }
    }

    // Types out each dialogue letter by letter
    IEnumerator TypeText(string text, CancellationToken cancellationToken)
    {
        if (cancellationToken.CancellationRequested) { yield break; }
        dialogueText.text = "";
        foreach (char c in text)
        {
            if (cancellationToken.CancellationRequested) { yield break; }
            yield return new WaitForSeconds(delay);
            if (cancellationToken.CancellationRequested) { yield break; }
            dialogueText.text += c;
            yield return null;
        }
    }

    /// <summary>
    /// Internal method used to activate (thus display) a particular number of buttons. Only the bottom two buttons will be used if there are 1-2 choices,
    /// otherwise for 3-4 choices we start with the top two buttons and then use the bottom two. After calling this method, the CurrentButtons property
    /// can be used to find the set of buttons that are currently active.
    /// </summary>
    /// <param name="howMany">The number of buttons to activate</param>
    private void UseButtons(int howMany)
    {
        if (howMany == 1) { buttonRangeStart = 3; buttonRangeEnd = 3; }
        else if (howMany == 2) { buttonRangeStart = 2; buttonRangeEnd = 3; }
        else if (howMany == 3) { buttonRangeStart = 0; buttonRangeEnd = 2; }
        else if (howMany == 4) { buttonRangeStart = 0; buttonRangeEnd = 3; }
        else { throw new ArgumentOutOfRangeException("Too many or too few buttons requested!"); }

        var buttons = Buttons;
        for (int i = 0; i < 4; i++)
        {
            buttons[i].SetActive(i >= buttonRangeStart && i <= buttonRangeEnd);
        }
    }

    /// <summary>
    /// Should only be called by click handlers defined on the buttons.
    /// </summary>
    /// <param name="index">The index of the button that was clicked</param>
    public void ButtonClicked(int index)
    {
        DialogueElement element = (DialogueElement)currentNode;
        HandleNode(element.children[index - buttonRangeStart]);
    }
}

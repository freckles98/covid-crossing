using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    public GameObject dialogueBox;

    public Text dialogueText;
    private float delay = 0.002f;
    private bool finishedTyping = true;

    //Used to initialize variable and states before game begins
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("fix this" + gameObject.name);
        }
        else
        {
            instance = this;
        }
    }



    public Queue<string> dialogueInfo = new Queue<string>();

    //Adds each dialogue to a queue
    public void EnqueueDialogue(string db)
    {
        dialogueBox.SetActive(true);
        dialogueInfo.Clear();

        foreach (string info in db)
        {
            dialogueInfo.Enqueue(info);
        }
        DequeueDialogue();
    }

    //Unqueues the dialogue so that it can be displayed
    public void DequeueDialogue()
    {
        if (dialogueInfo.Count == 0)
        {
            EndofDialogue();
            return;
        }
        DialogueBase.DialogueInfo info = dialogueInfo.Dequeue();

        dialogueText.text = info.text;

        StartCoroutine(TypeText(info));

    }

    //Used to check if the dialogue has been displayed before starting the next one
    public bool GetFinishedTyping()
    {
        return finishedTyping;
    }

    //Types out each dialogue letter by letter
    IEnumerator TypeText(DialogueBase.DialogueInfo info)
    {
        finishedTyping = false;
        dialogueText.text = "";
        foreach (char c in info.text.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            yield return null;
        }
        finishedTyping = true;
    }

    //A check to see if the sequence of dialogue is finished
    public void EndofDialogue()
    {
        dialogueBox.SetActive(false);
    }
}

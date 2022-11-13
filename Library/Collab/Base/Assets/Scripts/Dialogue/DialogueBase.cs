using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues", order = 0)]
public class DialogueBase : ScriptableObject
{

    //A class to store dialogue info only conaining text
    [System.Serializable]
    public class DialogueInfo
    {
        [TextArea(4, 8)]
        public string text;
    }

    public DialogueInfo[] dialogueInfo;
}